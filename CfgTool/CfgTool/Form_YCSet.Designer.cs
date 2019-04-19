namespace CfgTool
{
    partial class Form_YCSet
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
            this.label1 = new System.Windows.Forms.Label();
            this.tb_I_MAX = new System.Windows.Forms.TextBox();
            this.tb_V_MAX = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_DC_MAX = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_P_MAX = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_FR_MAX = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_COS_MAX = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tb_COS_COE = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_FR_COE = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tb_P_COE = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tb_DC_COE = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tb_V_COE = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tb_I_COE = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Exit = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tb_COS_MAX);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.tb_FR_MAX);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.tb_P_MAX);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.tb_DC_MAX);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tb_V_MAX);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tb_I_MAX);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(276, 341);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "最大值";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "电流";
            // 
            // tb_I_MAX
            // 
            this.tb_I_MAX.Location = new System.Drawing.Point(104, 27);
            this.tb_I_MAX.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tb_I_MAX.Name = "tb_I_MAX";
            this.tb_I_MAX.Size = new System.Drawing.Size(132, 26);
            this.tb_I_MAX.TabIndex = 1;
            this.tb_I_MAX.Text = "60000";
            this.tb_I_MAX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_V_MAX
            // 
            this.tb_V_MAX.Location = new System.Drawing.Point(104, 77);
            this.tb_V_MAX.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tb_V_MAX.Name = "tb_V_MAX";
            this.tb_V_MAX.Size = new System.Drawing.Size(132, 26);
            this.tb_V_MAX.TabIndex = 3;
            this.tb_V_MAX.Text = "60000";
            this.tb_V_MAX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 81);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "交流电压";
            // 
            // tb_DC_MAX
            // 
            this.tb_DC_MAX.Location = new System.Drawing.Point(104, 127);
            this.tb_DC_MAX.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tb_DC_MAX.Name = "tb_DC_MAX";
            this.tb_DC_MAX.Size = new System.Drawing.Size(132, 26);
            this.tb_DC_MAX.TabIndex = 5;
            this.tb_DC_MAX.Text = "60000";
            this.tb_DC_MAX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 131);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "直流电压";
            // 
            // tb_P_MAX
            // 
            this.tb_P_MAX.Location = new System.Drawing.Point(104, 177);
            this.tb_P_MAX.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tb_P_MAX.Name = "tb_P_MAX";
            this.tb_P_MAX.Size = new System.Drawing.Size(132, 26);
            this.tb_P_MAX.TabIndex = 7;
            this.tb_P_MAX.Text = "60000";
            this.tb_P_MAX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(60, 182);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "功率";
            // 
            // tb_FR_MAX
            // 
            this.tb_FR_MAX.Location = new System.Drawing.Point(104, 227);
            this.tb_FR_MAX.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tb_FR_MAX.Name = "tb_FR_MAX";
            this.tb_FR_MAX.Size = new System.Drawing.Size(132, 26);
            this.tb_FR_MAX.TabIndex = 9;
            this.tb_FR_MAX.Text = "60000";
            this.tb_FR_MAX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(60, 231);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "频率";
            // 
            // tb_COS_MAX
            // 
            this.tb_COS_MAX.Location = new System.Drawing.Point(104, 277);
            this.tb_COS_MAX.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tb_COS_MAX.Name = "tb_COS_MAX";
            this.tb_COS_MAX.Size = new System.Drawing.Size(132, 26);
            this.tb_COS_MAX.TabIndex = 11;
            this.tb_COS_MAX.Text = "60000";
            this.tb_COS_MAX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(28, 281);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 16);
            this.label6.TabIndex = 10;
            this.label6.Text = "功率因数";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tb_COS_COE);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.tb_FR_COE);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.tb_P_COE);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.tb_DC_COE);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.tb_V_COE);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.tb_I_COE);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Location = new System.Drawing.Point(338, 13);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(276, 341);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "系数";
            // 
            // tb_COS_COE
            // 
            this.tb_COS_COE.Location = new System.Drawing.Point(104, 277);
            this.tb_COS_COE.Margin = new System.Windows.Forms.Padding(4);
            this.tb_COS_COE.Name = "tb_COS_COE";
            this.tb_COS_COE.Size = new System.Drawing.Size(132, 26);
            this.tb_COS_COE.TabIndex = 11;
            this.tb_COS_COE.Text = "1000";
            this.tb_COS_COE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(28, 281);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 16);
            this.label7.TabIndex = 10;
            this.label7.Text = "功率因数";
            // 
            // tb_FR_COE
            // 
            this.tb_FR_COE.Location = new System.Drawing.Point(104, 227);
            this.tb_FR_COE.Margin = new System.Windows.Forms.Padding(4);
            this.tb_FR_COE.Name = "tb_FR_COE";
            this.tb_FR_COE.Size = new System.Drawing.Size(132, 26);
            this.tb_FR_COE.TabIndex = 9;
            this.tb_FR_COE.Text = "1000";
            this.tb_FR_COE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(60, 231);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 16);
            this.label8.TabIndex = 8;
            this.label8.Text = "频率";
            // 
            // tb_P_COE
            // 
            this.tb_P_COE.Location = new System.Drawing.Point(104, 177);
            this.tb_P_COE.Margin = new System.Windows.Forms.Padding(4);
            this.tb_P_COE.Name = "tb_P_COE";
            this.tb_P_COE.Size = new System.Drawing.Size(132, 26);
            this.tb_P_COE.TabIndex = 7;
            this.tb_P_COE.Text = "1000";
            this.tb_P_COE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(60, 182);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 16);
            this.label9.TabIndex = 6;
            this.label9.Text = "功率";
            // 
            // tb_DC_COE
            // 
            this.tb_DC_COE.Location = new System.Drawing.Point(104, 127);
            this.tb_DC_COE.Margin = new System.Windows.Forms.Padding(4);
            this.tb_DC_COE.Name = "tb_DC_COE";
            this.tb_DC_COE.Size = new System.Drawing.Size(132, 26);
            this.tb_DC_COE.TabIndex = 5;
            this.tb_DC_COE.Text = "1000";
            this.tb_DC_COE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(28, 131);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 16);
            this.label10.TabIndex = 4;
            this.label10.Text = "直流电压";
            // 
            // tb_V_COE
            // 
            this.tb_V_COE.Location = new System.Drawing.Point(104, 77);
            this.tb_V_COE.Margin = new System.Windows.Forms.Padding(4);
            this.tb_V_COE.Name = "tb_V_COE";
            this.tb_V_COE.Size = new System.Drawing.Size(132, 26);
            this.tb_V_COE.TabIndex = 3;
            this.tb_V_COE.Text = "1000";
            this.tb_V_COE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(28, 81);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(72, 16);
            this.label11.TabIndex = 2;
            this.label11.Text = "交流电压";
            // 
            // tb_I_COE
            // 
            this.tb_I_COE.Location = new System.Drawing.Point(104, 27);
            this.tb_I_COE.Margin = new System.Windows.Forms.Padding(4);
            this.tb_I_COE.Name = "tb_I_COE";
            this.tb_I_COE.Size = new System.Drawing.Size(132, 26);
            this.tb_I_COE.TabIndex = 1;
            this.tb_I_COE.Text = "1000";
            this.tb_I_COE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(60, 32);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 16);
            this.label12.TabIndex = 0;
            this.label12.Text = "电流";
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(338, 387);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(105, 51);
            this.btn_OK.TabIndex = 2;
            this.btn_OK.Text = "确定";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Exit
            // 
            this.btn_Exit.Location = new System.Drawing.Point(509, 387);
            this.btn_Exit.Name = "btn_Exit";
            this.btn_Exit.Size = new System.Drawing.Size(105, 51);
            this.btn_Exit.TabIndex = 3;
            this.btn_Exit.Text = "退出";
            this.btn_Exit.UseVisualStyleBackColor = true;
            this.btn_Exit.Click += new System.EventHandler(this.btn_Exit_Click);
            // 
            // Form_YCSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(636, 458);
            this.Controls.Add(this.btn_Exit);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "Form_YCSet";
            this.Text = "遥测最大值和系数";
            this.Load += new System.EventHandler(this.Form_YCSet_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tb_I_MAX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_COS_MAX;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_FR_MAX;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_P_MAX;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_DC_MAX;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_V_MAX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox tb_COS_COE;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_FR_COE;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tb_P_COE;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tb_DC_COE;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tb_V_COE;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tb_I_COE;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Exit;
    }
}