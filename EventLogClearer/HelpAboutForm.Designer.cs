namespace EventLogClearer
{
    partial class HelpAboutForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HelpAboutForm));
            this.titleLabel = new System.Windows.Forms.Label();
            this.helpTextLabel1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // titleLabel
            // 
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(12, 9);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(360, 23);
            this.titleLabel.TabIndex = 0;
            this.titleLabel.Text = "to be filled out runtime";
            this.titleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // helpTextLabel1
            // 
            this.helpTextLabel1.Location = new System.Drawing.Point(12, 49);
            this.helpTextLabel1.Name = "helpTextLabel1";
            this.helpTextLabel1.Size = new System.Drawing.Size(410, 353);
            this.helpTextLabel1.TabIndex = 1;
            this.helpTextLabel1.Text = resources.GetString("helpTextLabel1.Text");
            // 
            // HelpAboutForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 411);
            this.Controls.Add(this.helpTextLabel1);
            this.Controls.Add(this.titleLabel);
            this.Name = "HelpAboutForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Help/About";
            this.Load += new System.EventHandler(this.HelpAboutForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label helpTextLabel1;
    }
}