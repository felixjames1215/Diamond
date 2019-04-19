namespace CfgTool
{
    partial class Form_Table_FWT_FastDrag
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
            this.btnFastDrag = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbIDString
            // 
            this.tbIDString.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbIDString.Location = new System.Drawing.Point(0, 3);
            this.tbIDString.Multiline = true;
            this.tbIDString.Name = "tbIDString";
            this.tbIDString.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbIDString.Size = new System.Drawing.Size(578, 92);
            this.tbIDString.TabIndex = 0;
            // 
            // btnFastDrag
            // 
            this.btnFastDrag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFastDrag.Location = new System.Drawing.Point(479, 113);
            this.btnFastDrag.Name = "btnFastDrag";
            this.btnFastDrag.Size = new System.Drawing.Size(86, 37);
            this.btnFastDrag.TabIndex = 1;
            this.btnFastDrag.Text = "配置";
            this.btnFastDrag.UseVisualStyleBackColor = true;
            this.btnFastDrag.Click += new System.EventHandler(this.btnFastDrag_Click);
            // 
            // Form_Table_FWT_FastDrag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 162);
            this.Controls.Add(this.btnFastDrag);
            this.Controls.Add(this.tbIDString);
            this.Name = "Form_Table_FWT_FastDrag";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "转发表快捷配置";
            this.Load += new System.EventHandler(this.Form_Table_FWT_FastDrag_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbIDString;
        private System.Windows.Forms.Button btnFastDrag;
    }
}