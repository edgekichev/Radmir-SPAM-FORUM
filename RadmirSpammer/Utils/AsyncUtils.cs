public static class AsyncUtils
{
    /// <summary>
    ///     Blocks while condition is true or task is canceled.
    /// </summary>
    /// <param name="CT">
    ///     Cancellation token.
    /// </param>
    /// <param name="Condition">
    ///     The condition that will perpetuate the block.
    /// </param>
    /// <param name="PollDelay">
    ///     The delay at which the condition will be polled, in milliseconds.
    /// </param>
    /// <returns>
    ///     <see cref="Task" />.
    /// </returns>
    public static async Task WaitWhileAsync(Func<bool> Condition, int PollDelay = 25, CancellationToken CT = default)
    {
        try
        {
            while (Condition())
                await Task.Delay(PollDelay, CT).ConfigureAwait(true);
        }
        catch (TaskCanceledException)
        {
            // ignore: Task.Delay throws this exception when ct.IsCancellationRequested = true
            // In this case, we only want to stop polling and finish this async Task.
        }
    }

    /// <summary>
    ///     Blocks until condition is true or task is canceled.
    /// </summary>
    /// <param name="CT">
    ///     Cancellation token.
    /// </param>
    /// <param name="Condition">
    ///     The condition that will perpetuate the block.
    /// </param>
    /// <param name="PollDelay">
    ///     The delay at which the condition will be polled, in milliseconds.
    /// </param>
    /// <returns>
    ///     <see cref="Task" />.
    /// </returns>
    public static async Task WaitUntilAsync(Func<bool> Condition, int PollDelay = 25, CancellationToken CT = default)
    {
        try
        {
            while (!Condition())
                await Task.Delay(PollDelay, CT).ConfigureAwait(true);
        }
        catch (TaskCanceledException)
        {
            // ignore: Task.Delay throws this exception when ct.IsCancellationRequested = true
            // In this case, we only want to stop polling and finish this async Task.
        }
    }

    /// <summary>
    ///     Blocks while condition is true or timeout occurs.
    /// </summary>
    /// <param name="CT">
    ///     The cancellation token.
    /// </param>
    /// <param name="Condition">
    ///     The condition that will perpetuate the block.
    /// </param>
    /// <param name="PollDelay">
    ///     The delay at which the condition will be polled, in milliseconds.
    /// </param>
    /// <param name="Timeout">
    ///     Timeout in milliseconds.
    /// </param>
    /// <exception cref="TimeoutException">
    ///     Thrown after timeout milliseconds
    /// </exception>
    /// <returns>
    ///     <see cref="Task" />.
    /// </returns>
    public static async Task WaitWhileAsync(Func<bool> Condition, int PollDelay, int Timeout, CancellationToken CT = default)
    {
        if (CT.IsCancellationRequested) return;

        using (CancellationTokenSource CTS = CancellationTokenSource.CreateLinkedTokenSource(CT))
        {
            Task waitTask = WaitWhileAsync(Condition, PollDelay, CTS.Token);
            Task timeoutTask = Task.Delay(Timeout, CTS.Token);
            Task finishedTask = await Task.WhenAny(waitTask, timeoutTask).ConfigureAwait(true);

            if (!CT.IsCancellationRequested)
            {
                CTS.Cancel();                            // Cancel unfinished task
                await finishedTask.ConfigureAwait(true); // Propagate exceptions
                if (finishedTask == timeoutTask)
                {
                    throw new TimeoutException();
                }
            }
        }
    }

    /// <summary>
    ///     Blocks until condition is true or timeout occurs.
    /// </summary>
    /// <param name="CT">
    ///     Cancellation token
    /// </param>
    /// <param name="Condition">
    ///     The condition that will perpetuate the block.
    /// </param>
    /// <param name="PollDelay">
    ///     The delay at which the condition will be polled, in milliseconds.
    /// </param>
    /// <param name="Timeout">
    ///     Timeout in milliseconds.
    /// </param>
    /// <exception cref="TimeoutException">
    ///     Thrown after timeout milliseconds
    /// </exception>
    /// <returns>
    ///     <see cref="Task" />.
    /// </returns>
    public static async Task WaitUntilAsync(Func<bool> Condition, int PollDelay, int Timeout, CancellationToken CT = default)
    {
        if (CT.IsCancellationRequested) return;

        using (CancellationTokenSource CTS = CancellationTokenSource.CreateLinkedTokenSource(CT))
        {
            Task WaitTask = WaitUntilAsync(Condition, PollDelay, CTS.Token);
            Task TimeoutTask = Task.Delay(Timeout, CTS.Token);
            Task FinishedTask = await Task.WhenAny(WaitTask, TimeoutTask).ConfigureAwait(true);

            if (!CT.IsCancellationRequested)
            {
                CTS.Cancel();                            // Cancel unfinished task
                await FinishedTask.ConfigureAwait(true); // Propagate exceptions
                if (FinishedTask == TimeoutTask)
                    throw new TimeoutException();
            }
        }
    }
    public static async Task<bool> TryUntilAsync(Action ActionToTry, int PollDelay = 25, int maxTrys = int.MaxValue)
    {
        var trys = 0;
        while (trys <= maxTrys)
        {
            await Task.Delay(PollDelay);
            try { ActionToTry(); return true; }
            catch { trys++; continue; }
        }
		return false;
	}

	public static async Task<bool> TryUntilAsync(Func<Task> ActionToTry, int PollDelay = 25, int maxTrys = int.MaxValue)
    {
		var trys = 0;
		while (trys <= maxTrys)
        {
            await Task.Delay(PollDelay);
            try { await ActionToTry(); return true; }
            catch { trys++; continue; }
        }
        return false;
    }

    public static async Task DoUntilAsync(Func<bool> ActionToDoUntil, int PollDelay = 25)
    {
        while (!ActionToDoUntil())
        {
            await Task.Delay(PollDelay);
        }
    }

    public static async Task DoUntilAsync(Func<Task<bool>> ActionToDoUntil, int PollDelay = 25)
    {
        while (!await ActionToDoUntil())
        {
            await Task.Delay(PollDelay);
        }
    }

}