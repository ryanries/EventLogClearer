namespace EventLogClearer
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.computersLabel = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.computersListBox = new System.Windows.Forms.ListBox();
            this.addComputerButton = new System.Windows.Forms.Button();
            this.removeComputerButton = new System.Windows.Forms.Button();
            this.clearListButton = new System.Windows.Forms.Button();
            this.autoPopulateButton = new System.Windows.Forms.Button();
            this.logTypesGroupBox = new System.Windows.Forms.GroupBox();
            this.appsAndSvcsLogsCheckBox = new System.Windows.Forms.CheckBox();
            this.systemEventLogCheckBox = new System.Windows.Forms.CheckBox();
            this.setupEventLogCheckBox = new System.Windows.Forms.CheckBox();
            this.securityEventLogCheckBox = new System.Windows.Forms.CheckBox();
            this.applicationEventLogCheckBox = new System.Windows.Forms.CheckBox();
            this.goButton = new System.Windows.Forms.Button();
            this.statusMessagesLabel = new System.Windows.Forms.Label();
            this.statusMessagesListBox = new System.Windows.Forms.ListBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.helpLinkLabel = new System.Windows.Forms.LinkLabel();
            this.alternateCredsGroupBox = new System.Windows.Forms.GroupBox();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.statusStrip1.SuspendLayout();
            this.logTypesGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.alternateCredsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // computersLabel
            // 
            this.computersLabel.AutoSize = true;
            this.computersLabel.Location = new System.Drawing.Point(12, 16);
            this.computersLabel.Name = "computersLabel";
            this.computersLabel.Size = new System.Drawing.Size(60, 13);
            this.computersLabel.TabIndex = 0;
            this.computersLabel.Text = "Computers:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 419);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(624, 22);
            this.statusStrip1.TabIndex = 1;
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(42, 17);
            this.toolStripStatusLabel3.Text = "Ready.";
            // 
            // computersListBox
            // 
            this.computersListBox.FormattingEnabled = true;
            this.computersListBox.Location = new System.Drawing.Point(15, 32);
            this.computersListBox.Name = "computersListBox";
            this.computersListBox.Size = new System.Drawing.Size(153, 381);
            this.computersListBox.TabIndex = 2;
            // 
            // addComputerButton
            // 
            this.addComputerButton.Location = new System.Drawing.Point(175, 32);
            this.addComputerButton.Name = "addComputerButton";
            this.addComputerButton.Size = new System.Drawing.Size(105, 23);
            this.addComputerButton.TabIndex = 3;
            this.addComputerButton.Text = "Add Computer";
            this.addComputerButton.UseVisualStyleBackColor = true;
            this.addComputerButton.Click += new System.EventHandler(this.addComputerButton_Click);
            // 
            // removeComputerButton
            // 
            this.removeComputerButton.Enabled = false;
            this.removeComputerButton.Location = new System.Drawing.Point(175, 62);
            this.removeComputerButton.Name = "removeComputerButton";
            this.removeComputerButton.Size = new System.Drawing.Size(105, 23);
            this.removeComputerButton.TabIndex = 4;
            this.removeComputerButton.Text = "Remove Computer";
            this.removeComputerButton.UseVisualStyleBackColor = true;
            this.removeComputerButton.Click += new System.EventHandler(this.removeComputerButton_Click);
            // 
            // clearListButton
            // 
            this.clearListButton.Location = new System.Drawing.Point(175, 92);
            this.clearListButton.Name = "clearListButton";
            this.clearListButton.Size = new System.Drawing.Size(105, 23);
            this.clearListButton.TabIndex = 5;
            this.clearListButton.Text = "Clear List";
            this.clearListButton.UseVisualStyleBackColor = true;
            this.clearListButton.Click += new System.EventHandler(this.clearListButton_Click);
            // 
            // autoPopulateButton
            // 
            this.autoPopulateButton.Location = new System.Drawing.Point(175, 122);
            this.autoPopulateButton.Name = "autoPopulateButton";
            this.autoPopulateButton.Size = new System.Drawing.Size(105, 23);
            this.autoPopulateButton.TabIndex = 6;
            this.autoPopulateButton.Text = "Populate From AD";
            this.autoPopulateButton.UseVisualStyleBackColor = true;
            this.autoPopulateButton.Click += new System.EventHandler(this.autoPopulateButton_Click);
            // 
            // logTypesGroupBox
            // 
            this.logTypesGroupBox.Controls.Add(this.appsAndSvcsLogsCheckBox);
            this.logTypesGroupBox.Controls.Add(this.systemEventLogCheckBox);
            this.logTypesGroupBox.Controls.Add(this.setupEventLogCheckBox);
            this.logTypesGroupBox.Controls.Add(this.securityEventLogCheckBox);
            this.logTypesGroupBox.Controls.Add(this.applicationEventLogCheckBox);
            this.logTypesGroupBox.Location = new System.Drawing.Point(286, 31);
            this.logTypesGroupBox.Name = "logTypesGroupBox";
            this.logTypesGroupBox.Size = new System.Drawing.Size(139, 143);
            this.logTypesGroupBox.TabIndex = 7;
            this.logTypesGroupBox.TabStop = false;
            this.logTypesGroupBox.Text = "Event Logs";
            // 
            // appsAndSvcsLogsCheckBox
            // 
            this.appsAndSvcsLogsCheckBox.AutoSize = true;
            this.appsAndSvcsLogsCheckBox.Location = new System.Drawing.Point(7, 114);
            this.appsAndSvcsLogsCheckBox.Name = "appsAndSvcsLogsCheckBox";
            this.appsAndSvcsLogsCheckBox.Size = new System.Drawing.Size(124, 17);
            this.appsAndSvcsLogsCheckBox.TabIndex = 4;
            this.appsAndSvcsLogsCheckBox.Text = "Apps and Svcs Logs";
            this.appsAndSvcsLogsCheckBox.UseVisualStyleBackColor = true;
            this.appsAndSvcsLogsCheckBox.CheckedChanged += new System.EventHandler(this.appsAndSvcsLogsCheckBox_CheckedChanged);
            // 
            // systemEventLogCheckBox
            // 
            this.systemEventLogCheckBox.AutoSize = true;
            this.systemEventLogCheckBox.Location = new System.Drawing.Point(7, 91);
            this.systemEventLogCheckBox.Name = "systemEventLogCheckBox";
            this.systemEventLogCheckBox.Size = new System.Drawing.Size(60, 17);
            this.systemEventLogCheckBox.TabIndex = 3;
            this.systemEventLogCheckBox.Text = "System";
            this.systemEventLogCheckBox.UseVisualStyleBackColor = true;
            this.systemEventLogCheckBox.CheckedChanged += new System.EventHandler(this.systemEventLogCheckBox_CheckedChanged);
            // 
            // setupEventLogCheckBox
            // 
            this.setupEventLogCheckBox.AutoSize = true;
            this.setupEventLogCheckBox.Location = new System.Drawing.Point(7, 68);
            this.setupEventLogCheckBox.Name = "setupEventLogCheckBox";
            this.setupEventLogCheckBox.Size = new System.Drawing.Size(54, 17);
            this.setupEventLogCheckBox.TabIndex = 2;
            this.setupEventLogCheckBox.Text = "Setup";
            this.setupEventLogCheckBox.UseVisualStyleBackColor = true;
            this.setupEventLogCheckBox.CheckedChanged += new System.EventHandler(this.setupEventLogCheckBox_CheckedChanged);
            // 
            // securityEventLogCheckBox
            // 
            this.securityEventLogCheckBox.AutoSize = true;
            this.securityEventLogCheckBox.Location = new System.Drawing.Point(7, 44);
            this.securityEventLogCheckBox.Name = "securityEventLogCheckBox";
            this.securityEventLogCheckBox.Size = new System.Drawing.Size(64, 17);
            this.securityEventLogCheckBox.TabIndex = 1;
            this.securityEventLogCheckBox.Text = "Security";
            this.securityEventLogCheckBox.UseVisualStyleBackColor = true;
            this.securityEventLogCheckBox.CheckedChanged += new System.EventHandler(this.securityEventLogCheckBox_CheckedChanged);
            // 
            // applicationEventLogCheckBox
            // 
            this.applicationEventLogCheckBox.AutoSize = true;
            this.applicationEventLogCheckBox.Location = new System.Drawing.Point(7, 20);
            this.applicationEventLogCheckBox.Name = "applicationEventLogCheckBox";
            this.applicationEventLogCheckBox.Size = new System.Drawing.Size(78, 17);
            this.applicationEventLogCheckBox.TabIndex = 0;
            this.applicationEventLogCheckBox.Text = "Application";
            this.applicationEventLogCheckBox.UseVisualStyleBackColor = true;
            this.applicationEventLogCheckBox.CheckedChanged += new System.EventHandler(this.applicationEventLogCheckBox_CheckedChanged);
            // 
            // goButton
            // 
            this.goButton.Enabled = false;
            this.goButton.Location = new System.Drawing.Point(175, 151);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(105, 23);
            this.goButton.TabIndex = 8;
            this.goButton.Text = "Go";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // statusMessagesLabel
            // 
            this.statusMessagesLabel.AutoSize = true;
            this.statusMessagesLabel.Location = new System.Drawing.Point(175, 208);
            this.statusMessagesLabel.Name = "statusMessagesLabel";
            this.statusMessagesLabel.Size = new System.Drawing.Size(91, 13);
            this.statusMessagesLabel.TabIndex = 9;
            this.statusMessagesLabel.Text = "Status Messages:";
            // 
            // statusMessagesListBox
            // 
            this.statusMessagesListBox.FormattingEnabled = true;
            this.statusMessagesListBox.HorizontalScrollbar = true;
            this.statusMessagesListBox.Location = new System.Drawing.Point(177, 225);
            this.statusMessagesListBox.Name = "statusMessagesListBox";
            this.statusMessagesListBox.Size = new System.Drawing.Size(435, 186);
            this.statusMessagesListBox.TabIndex = 10;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(515, 208);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(97, 13);
            this.linkLabel1.TabIndex = 11;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "2012 by Ryan Ries";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Image = global::EventLogClearer.Properties.Resources.evt;
            this.pictureBox1.Location = new System.Drawing.Point(539, 32);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(54, 53);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // helpLinkLabel
            // 
            this.helpLinkLabel.AutoSize = true;
            this.helpLinkLabel.Location = new System.Drawing.Point(550, 190);
            this.helpLinkLabel.Name = "helpLinkLabel";
            this.helpLinkLabel.Size = new System.Drawing.Size(62, 13);
            this.helpLinkLabel.TabIndex = 13;
            this.helpLinkLabel.TabStop = true;
            this.helpLinkLabel.Text = "Help/About";
            this.helpLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.helpLinkLabel_LinkClicked);
            // 
            // alternateCredsGroupBox
            // 
            this.alternateCredsGroupBox.Controls.Add(this.passwordTextBox);
            this.alternateCredsGroupBox.Controls.Add(this.usernameTextBox);
            this.alternateCredsGroupBox.Location = new System.Drawing.Point(432, 92);
            this.alternateCredsGroupBox.Name = "alternateCredsGroupBox";
            this.alternateCredsGroupBox.Size = new System.Drawing.Size(180, 81);
            this.alternateCredsGroupBox.TabIndex = 14;
            this.alternateCredsGroupBox.TabStop = false;
            this.alternateCredsGroupBox.Text = "Alternate Credentials";
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Location = new System.Drawing.Point(7, 20);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(167, 20);
            this.usernameTextBox.TabIndex = 0;
            this.usernameTextBox.TextChanged += new System.EventHandler(this.usernameTextBox_TextChanged);
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(6, 50);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(167, 20);
            this.passwordTextBox.TabIndex = 1;
            this.passwordTextBox.TextChanged += new System.EventHandler(this.passwordTextBox_TextChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.helpLinkLabel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.statusMessagesListBox);
            this.Controls.Add(this.statusMessagesLabel);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.logTypesGroupBox);
            this.Controls.Add(this.autoPopulateButton);
            this.Controls.Add(this.clearListButton);
            this.Controls.Add(this.removeComputerButton);
            this.Controls.Add(this.addComputerButton);
            this.Controls.Add(this.computersListBox);
            this.Controls.Add(this.computersLabel);
            this.Controls.Add(this.alternateCredsGroupBox);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(640, 480);
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "MainForm";
            this.Text = "to be filled out at runtime";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.logTypesGroupBox.ResumeLayout(false);
            this.logTypesGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.alternateCredsGroupBox.ResumeLayout(false);
            this.alternateCredsGroupBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label computersLabel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ListBox computersListBox;
        private System.Windows.Forms.Button addComputerButton;
        private System.Windows.Forms.Button removeComputerButton;
        private System.Windows.Forms.Button clearListButton;
        private System.Windows.Forms.Button autoPopulateButton;
        private System.Windows.Forms.GroupBox logTypesGroupBox;
        private System.Windows.Forms.CheckBox appsAndSvcsLogsCheckBox;
        private System.Windows.Forms.CheckBox systemEventLogCheckBox;
        private System.Windows.Forms.CheckBox setupEventLogCheckBox;
        private System.Windows.Forms.CheckBox securityEventLogCheckBox;
        private System.Windows.Forms.CheckBox applicationEventLogCheckBox;
        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.Label statusMessagesLabel;
        private System.Windows.Forms.ListBox statusMessagesListBox;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel helpLinkLabel;
        private System.Windows.Forms.GroupBox alternateCredsGroupBox;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.TextBox usernameTextBox;        
    }
}

