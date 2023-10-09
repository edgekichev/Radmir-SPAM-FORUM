namespace RadmirSpammer
{
	partial class Main
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			loadAccounts = new Button();
			label1 = new Label();
			loadedAccountsLabel = new Label();
			groupBox1 = new GroupBox();
			groupBox3 = new GroupBox();
			messagesSendLabel = new Label();
			label9 = new Label();
			loadProxy = new Button();
			proxyLoadedLabel = new Label();
			label6 = new Label();
			stopBots = new Button();
			startBots = new Button();
			bodyTextBox = new TextBox();
			label5 = new Label();
			label4 = new Label();
			headerTextBox = new TextBox();
			loadLinks = new Button();
			linksLoadedLabel = new Label();
			label3 = new Label();
			groupBox2 = new GroupBox();
			groupBox4 = new GroupBox();
			emailsConfirmedLabel = new Label();
			label11 = new Label();
			stopEmailBots = new Button();
			startEmailBots = new Button();
			loadEmails = new Button();
			emailsLoadedLabel = new Label();
			label2 = new Label();
			ofd = new OpenFileDialog();
			groupBox1.SuspendLayout();
			groupBox3.SuspendLayout();
			groupBox2.SuspendLayout();
			groupBox4.SuspendLayout();
			SuspendLayout();
			// 
			// loadAccounts
			// 
			loadAccounts.Location = new Point(6, 37);
			loadAccounts.Name = "loadAccounts";
			loadAccounts.Size = new Size(195, 23);
			loadAccounts.TabIndex = 0;
			loadAccounts.Text = "Загрузить аккаунты";
			loadAccounts.UseVisualStyleBackColor = true;
			loadAccounts.Click += loadAccounts_Click;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(6, 19);
			label1.Name = "label1";
			label1.Size = new Size(128, 15);
			label1.TabIndex = 1;
			label1.Text = "Аккаунтов загружено:";
			// 
			// loadedAccountsLabel
			// 
			loadedAccountsLabel.AutoSize = true;
			loadedAccountsLabel.Location = new Point(131, 19);
			loadedAccountsLabel.Name = "loadedAccountsLabel";
			loadedAccountsLabel.Size = new Size(13, 15);
			loadedAccountsLabel.TabIndex = 2;
			loadedAccountsLabel.Text = "0";
			// 
			// groupBox1
			// 
			groupBox1.Controls.Add(groupBox3);
			groupBox1.Controls.Add(loadProxy);
			groupBox1.Controls.Add(proxyLoadedLabel);
			groupBox1.Controls.Add(label6);
			groupBox1.Controls.Add(stopBots);
			groupBox1.Controls.Add(startBots);
			groupBox1.Controls.Add(bodyTextBox);
			groupBox1.Controls.Add(label5);
			groupBox1.Controls.Add(label4);
			groupBox1.Controls.Add(headerTextBox);
			groupBox1.Controls.Add(loadLinks);
			groupBox1.Controls.Add(linksLoadedLabel);
			groupBox1.Controls.Add(label3);
			groupBox1.Controls.Add(label1);
			groupBox1.Controls.Add(loadedAccountsLabel);
			groupBox1.Controls.Add(loadAccounts);
			groupBox1.Location = new Point(12, 12);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(558, 252);
			groupBox1.TabIndex = 3;
			groupBox1.TabStop = false;
			groupBox1.Text = "Спаммер";
			// 
			// groupBox3
			// 
			groupBox3.Controls.Add(messagesSendLabel);
			groupBox3.Controls.Add(label9);
			groupBox3.Location = new Point(6, 155);
			groupBox3.Name = "groupBox3";
			groupBox3.Size = new Size(195, 61);
			groupBox3.TabIndex = 18;
			groupBox3.TabStop = false;
			groupBox3.Text = "Статистика:";
			// 
			// messagesSendLabel
			// 
			messagesSendLabel.AutoSize = true;
			messagesSendLabel.Location = new Point(148, 19);
			messagesSendLabel.Name = "messagesSendLabel";
			messagesSendLabel.Size = new Size(13, 15);
			messagesSendLabel.TabIndex = 20;
			messagesSendLabel.Text = "0";
			// 
			// label9
			// 
			label9.AutoSize = true;
			label9.Location = new Point(6, 19);
			label9.Name = "label9";
			label9.Size = new Size(145, 15);
			label9.TabIndex = 17;
			label9.Text = "Сообщений отправлено:";
			// 
			// loadProxy
			// 
			loadProxy.Location = new Point(6, 126);
			loadProxy.Name = "loadProxy";
			loadProxy.Size = new Size(195, 23);
			loadProxy.TabIndex = 14;
			loadProxy.Text = "Загрузить прокси";
			loadProxy.UseVisualStyleBackColor = true;
			loadProxy.Click += loadProxy_Click;
			// 
			// proxyLoadedLabel
			// 
			proxyLoadedLabel.AutoSize = true;
			proxyLoadedLabel.Location = new Point(118, 108);
			proxyLoadedLabel.Name = "proxyLoadedLabel";
			proxyLoadedLabel.Size = new Size(13, 15);
			proxyLoadedLabel.TabIndex = 13;
			proxyLoadedLabel.Text = "0";
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Location = new Point(6, 108);
			label6.Name = "label6";
			label6.Size = new Size(113, 15);
			label6.TabIndex = 12;
			label6.Text = "Прокси загружено:";
			// 
			// stopBots
			// 
			stopBots.Enabled = false;
			stopBots.Location = new Point(6, 222);
			stopBots.Name = "stopBots";
			stopBots.Size = new Size(89, 23);
			stopBots.TabIndex = 11;
			stopBots.Text = "Остановить";
			stopBots.UseVisualStyleBackColor = true;
			stopBots.Click += stopBots_Click;
			// 
			// startBots
			// 
			startBots.Location = new Point(463, 222);
			startBots.Name = "startBots";
			startBots.Size = new Size(89, 23);
			startBots.TabIndex = 10;
			startBots.Text = "Запустить";
			startBots.UseVisualStyleBackColor = true;
			startBots.Click += startBots_Click;
			// 
			// bodyTextBox
			// 
			bodyTextBox.Location = new Point(207, 82);
			bodyTextBox.Multiline = true;
			bodyTextBox.Name = "bodyTextBox";
			bodyTextBox.Size = new Size(345, 134);
			bodyTextBox.TabIndex = 9;
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Location = new Point(207, 64);
			label5.Name = "label5";
			label5.Size = new Size(159, 15);
			label5.TabIndex = 8;
			label5.Text = "Тело сообщения или темы:";
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new Point(207, 19);
			label4.Name = "label4";
			label4.Size = new Size(309, 15);
			label4.TabIndex = 7;
			label4.Text = "Заголовок темы (только для ссылок с созданием тем):";
			// 
			// headerTextBox
			// 
			headerTextBox.Location = new Point(207, 37);
			headerTextBox.Name = "headerTextBox";
			headerTextBox.Size = new Size(345, 23);
			headerTextBox.TabIndex = 6;
			// 
			// loadLinks
			// 
			loadLinks.Location = new Point(6, 82);
			loadLinks.Name = "loadLinks";
			loadLinks.Size = new Size(195, 23);
			loadLinks.TabIndex = 5;
			loadLinks.Text = "Загрузить ссылки";
			loadLinks.UseVisualStyleBackColor = true;
			loadLinks.Click += loadLinks_Click;
			// 
			// linksLoadedLabel
			// 
			linksLoadedLabel.AutoSize = true;
			linksLoadedLabel.Location = new Point(118, 64);
			linksLoadedLabel.Name = "linksLoadedLabel";
			linksLoadedLabel.Size = new Size(13, 15);
			linksLoadedLabel.TabIndex = 4;
			linksLoadedLabel.Text = "0";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(6, 64);
			label3.Name = "label3";
			label3.Size = new Size(114, 15);
			label3.TabIndex = 3;
			label3.Text = "Ссылок загружено:";
			// 
			// groupBox2
			// 
			groupBox2.Controls.Add(groupBox4);
			groupBox2.Controls.Add(stopEmailBots);
			groupBox2.Controls.Add(startEmailBots);
			groupBox2.Controls.Add(loadEmails);
			groupBox2.Controls.Add(emailsLoadedLabel);
			groupBox2.Controls.Add(label2);
			groupBox2.Location = new Point(576, 12);
			groupBox2.Name = "groupBox2";
			groupBox2.Size = new Size(235, 252);
			groupBox2.TabIndex = 4;
			groupBox2.TabStop = false;
			groupBox2.Text = "Подтверждение почт";
			// 
			// groupBox4
			// 
			groupBox4.Controls.Add(emailsConfirmedLabel);
			groupBox4.Controls.Add(label11);
			groupBox4.Location = new Point(6, 66);
			groupBox4.Name = "groupBox4";
			groupBox4.Size = new Size(223, 39);
			groupBox4.TabIndex = 21;
			groupBox4.TabStop = false;
			groupBox4.Text = "Статистика:";
			// 
			// emailsConfirmedLabel
			// 
			emailsConfirmedLabel.AutoSize = true;
			emailsConfirmedLabel.Location = new Point(122, 20);
			emailsConfirmedLabel.Name = "emailsConfirmedLabel";
			emailsConfirmedLabel.Size = new Size(13, 15);
			emailsConfirmedLabel.TabIndex = 19;
			emailsConfirmedLabel.Text = "0";
			// 
			// label11
			// 
			label11.AutoSize = true;
			label11.Location = new Point(6, 19);
			label11.Name = "label11";
			label11.Size = new Size(120, 15);
			label11.TabIndex = 15;
			label11.Text = "Почт подтверждено:";
			// 
			// stopEmailBots
			// 
			stopEmailBots.Enabled = false;
			stopEmailBots.Location = new Point(6, 223);
			stopEmailBots.Name = "stopEmailBots";
			stopEmailBots.Size = new Size(89, 23);
			stopEmailBots.TabIndex = 13;
			stopEmailBots.Text = "Остановить";
			stopEmailBots.UseVisualStyleBackColor = true;
			stopEmailBots.Click += stopEmailBots_Click;
			// 
			// startEmailBots
			// 
			startEmailBots.Location = new Point(140, 223);
			startEmailBots.Name = "startEmailBots";
			startEmailBots.Size = new Size(89, 23);
			startEmailBots.TabIndex = 12;
			startEmailBots.Text = "Запустить";
			startEmailBots.UseVisualStyleBackColor = true;
			startEmailBots.Click += startEmailBots_Click;
			// 
			// loadEmails
			// 
			loadEmails.Location = new Point(6, 37);
			loadEmails.Name = "loadEmails";
			loadEmails.Size = new Size(219, 23);
			loadEmails.TabIndex = 3;
			loadEmails.Text = "Загрузить почты для подтверждения";
			loadEmails.UseVisualStyleBackColor = true;
			loadEmails.Click += loadEmails_Click;
			// 
			// emailsLoadedLabel
			// 
			emailsLoadedLabel.AutoSize = true;
			emailsLoadedLabel.Location = new Point(212, 19);
			emailsLoadedLabel.Name = "emailsLoadedLabel";
			emailsLoadedLabel.Size = new Size(13, 15);
			emailsLoadedLabel.TabIndex = 3;
			emailsLoadedLabel.Text = "0";
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(6, 19);
			label2.Name = "label2";
			label2.Size = new Size(209, 15);
			label2.TabIndex = 3;
			label2.Text = "Почт для подтверждения загружено:";
			// 
			// Main
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(825, 277);
			Controls.Add(groupBox2);
			Controls.Add(groupBox1);
			Name = "Main";
			Text = "Main";
			groupBox1.ResumeLayout(false);
			groupBox1.PerformLayout();
			groupBox3.ResumeLayout(false);
			groupBox3.PerformLayout();
			groupBox2.ResumeLayout(false);
			groupBox2.PerformLayout();
			groupBox4.ResumeLayout(false);
			groupBox4.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private Button loadAccounts;
		private Label label1;
		private Label loadedAccountsLabel;
		private GroupBox groupBox1;
		private GroupBox groupBox2;
		private Label label2;
		private Button stopBots;
		private Button startBots;
		private TextBox bodyTextBox;
		private Label label5;
		private Label label4;
		private TextBox headerTextBox;
		private Button loadLinks;
		private Label linksLoadedLabel;
		private Label label3;
		private Button loadEmails;
		private Label emailsLoadedLabel;
		private Button stopEmailBots;
		private Button startEmailBots;
		private Button loadProxy;
		private Label proxyLoaded;
		private Label label6;
		private OpenFileDialog ofd;
		private Label label9;
		private GroupBox groupBox3;
		private Label messagesSendLabel;
		private Label proxyLoadedLabel;
		private GroupBox groupBox4;
		private Label emailsConfirmedLabel;
		private Label label11;
	}
}