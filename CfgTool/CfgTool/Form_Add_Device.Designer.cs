namespace CfgTool
{
    partial class Form_Add_Device
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
            this.label1 = new System.Windows.Forms.Label();
            this.tb_DeviceName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_Model = new System.Windows.Forms.ComboBox();
            this.tb_CommAddr = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Add = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(112, 68);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "设备名称";
            // 
            // tb_DeviceName
            // 
            this.tb_DeviceName.Location = new System.Drawing.Point(215, 63);
            this.tb_DeviceName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tb_DeviceName.Name = "tb_DeviceName";
            this.tb_DeviceName.Size = new System.Drawing.Size(237, 23);
            this.tb_DeviceName.TabIndex = 1;
            this.tb_DeviceName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(112, 168);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "指定模板";
            // 
            // cmb_Model
            // 
            this.cmb_Model.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_Model.FormattingEnabled = true;
            this.cmb_Model.Location = new System.Drawing.Point(215, 163);
            this.cmb_Model.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cmb_Model.Name = "cmb_Model";
            this.cmb_Model.Size = new System.Drawing.Size(234, 22);
            this.cmb_Model.TabIndex = 3;
            // 
            // tb_CommAddr
            // 
            this.tb_CommAddr.Location = new System.Drawing.Point(215, 252);
            this.tb_CommAddr.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.tb_CommAddr.Name = "tb_CommAddr";
            this.tb_CommAddr.Size = new System.Drawing.Size(237, 23);
            this.tb_CommAddr.TabIndex = 5;
            this.tb_CommAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 257);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 14);
            this.label3.TabIndex = 4;
            this.label3.Text = "设备地址(1～254)";
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(318, 352);
            this.btn_Add.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(136, 63);
            this.btn_Add.TabIndex = 8;
            this.btn_Add.Text = "新增";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // Form_Add_Device
            // 
            this.AcceptButton = this.btn_Add;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 454);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.tb_CommAddr);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmb_Model);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_DeviceName);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_Add_Device";
            this.Text = "新增设备";
            this.Load += new System.EventHandler(this.Form_Device_Add_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_DeviceName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_Model;
        private System.Windows.Forms.TextBox tb_CommAddr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Add;
    }
}