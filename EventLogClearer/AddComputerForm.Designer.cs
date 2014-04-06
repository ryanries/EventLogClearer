namespace EventLogClearer
{
    partial class AddComputerForm
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
            this.addComputerTextBox = new System.Windows.Forms.TextBox();
            this.addComputerButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // addComputerTextBox
            // 
            this.addComputerTextBox.Location = new System.Drawing.Point(13, 12);
            this.addComputerTextBox.Name = "addComputerTextBox";
            this.addComputerTextBox.Size = new System.Drawing.Size(278, 20);
            this.addComputerTextBox.TabIndex = 0;
            this.addComputerTextBox.TextChanged += new System.EventHandler(this.addComputerTextBox_TextChanged);
            // 
            // addComputerButton
            // 
            this.addComputerButton.Enabled = false;
            this.addComputerButton.Location = new System.Drawing.Point(297, 11);
            this.addComputerButton.Name = "addComputerButton";
            this.addComputerButton.Size = new System.Drawing.Size(75, 23);
            this.addComputerButton.TabIndex = 1;
            this.addComputerButton.Text = "Add";
            this.addComputerButton.UseVisualStyleBackColor = true;
            this.addComputerButton.Click += new System.EventHandler(this.addComputerButton_Click);
            // 
            // AddComputerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 41);
            this.Controls.Add(this.addComputerButton);
            this.Controls.Add(this.addComputerTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddComputerForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Computer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox addComputerTextBox;
        private System.Windows.Forms.Button addComputerButton;
    }
}