namespace CfgTool
{
    partial class Form_Table_FWT_SNString
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
            this.tbIDString = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbIDString
            // 
            this.tbIDString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbIDString.Location = new System.Drawing.Point(0, 0);
            this.tbIDString.Multiline = true;
            this.tbIDString.Name = "tbIDString";
            this.tbIDString.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbIDString.Size = new System.Drawing.Size(881, 160);
            this.tbIDString.TabIndex = 1;
            // 
            // Form_Table_FWT_SNString
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(881, 160);
            this.Controls.Add(this.tbIDString);
            this.Name = "Form_Table_FWT_SNString";
            this.Text = "总序号字符串";
            this.Load += new System.EventHandler(this.Form_Table_FWT_SNString_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbIDString;
    }
}