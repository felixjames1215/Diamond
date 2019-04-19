namespace CfgTool
{
    partial class Form_Setup
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
            this.tb_SelfName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.btn_OK = new System.Windows.Forms.Button();
            this.cb_SoftYK_2 = new System.Windows.Forms.CheckBox();
            this.cb_SoftYK_1 = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbButton_GYkCfg = new LBSoft.IndustrialCtrls.Buttons.LBButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tb_SelfName
            // 
            this.tb_SelfName.Location = new System.Drawing.Point(135, 43);
            this.tb_SelfName.Margin = new System.Windows.Forms.Padding(5);
            this.tb_SelfName.Name = "tb_SelfName";
            this.tb_SelfName.Size = new System.Drawing.Size(336, 26);
            this.tb_SelfName.TabIndex = 3;
            this.tb_SelfName.Text = "本体";
            this.tb_SelfName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(53, 48);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "本体名称";
            // 
            // btn_Exit
            // 
            this.btn_Exit.Location = new System.Drawing.Point(360, 218);
            this.btn_Exit.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(89, 46);
            this.btn_Exit.TabIndex = 5;
            this.btn_Exit.Text = "退出";
            this.btn_Exit.UseVisualStyleBackColor = true;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(96, 218);
            this.btn_OK.Margin = new System.Windows.Forms.Padding(4);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(89, 46);
            this.btn_OK.TabIndex = 4;
            this.btn_OK.Text = "确定";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // cb_SoftYK_2
            // 
            this.cb_SoftYK_2.AutoSize = true;
            this.cb_SoftYK_2.Location = new System.Drawing.Point(186, 37);
            this.cb_SoftYK_2.Name = "cb_SoftYK_2";
            this.cb_SoftYK_2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cb_SoftYK_2.Size = new System.Drawing.Size(139, 20);
            this.cb_SoftYK_2.TabIndex = 106;
            this.cb_SoftYK_2.Text = "软遥控不判压板";
            this.cb_SoftYK_2.UseVisualStyleBackColor = true;
            this.cb_SoftYK_2.CheckedChanged += new System.EventHandler(this.cb_SoftYK_2_CheckedChanged);
            // 
            // cb_SoftYK_1
            // 
            this.cb_SoftYK_1.AutoSize = true;
            this.cb_SoftYK_1.Location = new System.Drawing.Point(20, 37);
            this.cb_SoftYK_1.Name = "cb_SoftYK_1";
            this.cb_SoftYK_1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cb_SoftYK_1.Size = new System.Drawing.Size(139, 20);
            this.cb_SoftYK_1.TabIndex = 105;
            this.cb_SoftYK_1.Text = "软遥控不判远方";
            this.cb_SoftYK_1.UseVisualStyleBackColor = true;
            this.cb_SoftYK_1.CheckedChanged += new System.EventHandler(this.cb_SoftYK_1_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbButton_GYkCfg);
            this.groupBox1.Controls.Add(this.cb_SoftYK_2);
            this.groupBox1.Controls.Add(this.cb_SoftYK_1);
            this.groupBox1.Location = new System.Drawing.Point(56, 101);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(452, 77);
            this.groupBox1.TabIndex = 107;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "软遥控配置，生成YK.cfg";
            // 
            // lbButton_GYkCfg
            // 
            this.lbButton_GYkCfg.BackColor = System.Drawing.Color.Transparent;
            this.lbButton_GYkCfg.ButtonColor = System.Drawing.Color.LawnGreen;
            this.lbButton_GYkCfg.Font = new System.Drawing.Font("Microsoft Sans Serif", 39.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbButton_GYkCfg.ForeColor = System.Drawing.Color.Red;
            this.lbButton_GYkCfg.Label = "生成配置";
            this.lbButton_GYkCfg.Location = new System.Drawing.Point(356, 20);
            this.lbButton_GYkCfg.Name = "lbButton_GYkCfg";
            this.lbButton_GYkCfg.Renderer = null;
            this.lbButton_GYkCfg.RepeatInterval = 100;
            this.lbButton_GYkCfg.RepeatState = false;
            this.lbButton_GYkCfg.Size = new System.Drawing.Size(90, 51);
            this.lbButton_GYkCfg.StartRepeatInterval = 500;
            this.lbButton_GYkCfg.State = LBSoft.IndustrialCtrls.Buttons.LBButton.ButtonState.Normal;
            this.lbButton_GYkCfg.Style = LBSoft.IndustrialCtrls.Buttons.LBButton.ButtonStyle.Elliptical;
            this.lbButton_GYkCfg.TabIndex = 107;
            this.lbButton_GYkCfg.Click += new System.EventHandler(this.lbButton_GYkCfg_Click);
            // 
            // Form_Setup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 297);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Exit);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.tb_SelfName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_Setup";
            this.Text = "设置";
            this.Load += new System.EventHandler(this.Form_Setup_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_SelfName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Exit;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.CheckBox cb_SoftYK_2;
        private System.Windows.Forms.CheckBox cb_SoftYK_1;
        private System.Windows.Forms.GroupBox groupBox1;
        private LBSoft.IndustrialCtrls.Buttons.LBButton lbButton_GYkCfg;
    }
}