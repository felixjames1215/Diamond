namespace CfgTool
{
    partial class Form_Table_FWT_CopyFlagSet
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_Meter = new System.Windows.Forms.CheckBox();
            this.cb_YK = new System.Windows.Forms.CheckBox();
            this.cb_DYX = new System.Windows.Forms.CheckBox();
            this.cb_SYX = new System.Windows.Forms.CheckBox();
            this.cb_YC = new System.Windows.Forms.CheckBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cb_Meter);
            this.groupBox1.Controls.Add(this.cb_YK);
            this.groupBox1.Controls.Add(this.cb_DYX);
            this.groupBox1.Controls.Add(this.cb_SYX);
            this.groupBox1.Controls.Add(this.cb_YC);
            this.groupBox1.Location = new System.Drawing.Point(2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(139, 187);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // cb_Meter
            // 
            this.cb_Meter.AutoSize = true;
            this.cb_Meter.Location = new System.Drawing.Point(41, 162);
            this.cb_Meter.Name = "cb_Meter";
            this.cb_Meter.Size = new System.Drawing.Size(60, 16);
            this.cb_Meter.TabIndex = 4;
            this.cb_Meter.Text = "计量值";
            this.cb_Meter.UseVisualStyleBackColor = true;
            // 
            // cb_YK
            // 
            this.cb_YK.AutoSize = true;
            this.cb_YK.Location = new System.Drawing.Point(41, 127);
            this.cb_YK.Name = "cb_YK";
            this.cb_YK.Size = new System.Drawing.Size(48, 16);
            this.cb_YK.TabIndex = 3;
            this.cb_YK.Text = "遥控";
            this.cb_YK.UseVisualStyleBackColor = true;
            // 
            // cb_DYX
            // 
            this.cb_DYX.AutoSize = true;
            this.cb_DYX.Location = new System.Drawing.Point(41, 91);
            this.cb_DYX.Name = "cb_DYX";
            this.cb_DYX.Size = new System.Drawing.Size(72, 16);
            this.cb_DYX.TabIndex = 2;
            this.cb_DYX.Text = "双点遥信";
            this.cb_DYX.UseVisualStyleBackColor = true;
            // 
            // cb_SYX
            // 
            this.cb_SYX.AutoSize = true;
            this.cb_SYX.Location = new System.Drawing.Point(41, 53);
            this.cb_SYX.Name = "cb_SYX";
            this.cb_SYX.Size = new System.Drawing.Size(72, 16);
            this.cb_SYX.TabIndex = 1;
            this.cb_SYX.Text = "单点遥信";
            this.cb_SYX.UseVisualStyleBackColor = true;
            // 
            // cb_YC
            // 
            this.cb_YC.AutoSize = true;
            this.cb_YC.Location = new System.Drawing.Point(41, 21);
            this.cb_YC.Name = "cb_YC";
            this.cb_YC.Size = new System.Drawing.Size(48, 16);
            this.cb_YC.TabIndex = 0;
            this.cb_YC.Text = "遥测";
            this.cb_YC.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(2, 196);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(139, 42);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确 认";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // Form_Table_FWT_CopyFlagSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(143, 240);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form_Table_FWT_CopyFlagSet";
            this.Load += new System.EventHandler(this.Form_Table_FWT_CopyFlagSet_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cb_Meter;
        private System.Windows.Forms.CheckBox cb_YK;
        private System.Windows.Forms.CheckBox cb_DYX;
        private System.Windows.Forms.CheckBox cb_SYX;
        private System.Windows.Forms.CheckBox cb_YC;
        private System.Windows.Forms.Button btnOK;
    }
}