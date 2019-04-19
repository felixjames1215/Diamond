namespace CfgTool
{
    partial class Form_Table_Port_Name
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tp_PhysicalPara = new System.Windows.Forms.TabPage();
            this.cb_Enabled = new System.Windows.Forms.CheckBox();
            this.tb_u16PortAddr = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lbl_PortName_1 = new System.Windows.Forms.Label();
            this.gb_PhysicalPara_Network = new System.Windows.Forms.GroupBox();
            this.tb_u8MAC_6 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tb_u8MAC_5 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tb_u8MAC_4 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tb_u8MAC_3 = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.tb_u8MAC_2 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tb_u8MAC_1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_Network_OK = new System.Windows.Forms.Button();
            this.tb_Network_Mask = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_Network_IP = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.gb_PhysicalPara_Serial = new System.Windows.Forms.GroupBox();
            this.cmb_SerialPort_CheckBit = new System.Windows.Forms.ComboBox();
            this.cmb_SerialPort_Check = new System.Windows.Forms.Label();
            this.cmb_SerialPort_StopBit = new System.Windows.Forms.ComboBox();
            this.cmb_SerialPort_StopLen = new System.Windows.Forms.Label();
            this.cmb_SerialPort_DataBit = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_SerialPort_BaudRate = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tp_LogicPara = new System.Windows.Forms.TabPage();
            this.lbl_PortName_2 = new System.Windows.Forms.Label();
            this.gb_LogicPara = new System.Windows.Forms.GroupBox();
            this.cmb_LogicPara_Protocol = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmb_LogicPara_Property = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tp_ProtocolPara = new System.Windows.Forms.TabPage();
            this.lbl_PortName_3 = new System.Windows.Forms.Label();
            this.gb_ProtocolPara = new System.Windows.Forms.GroupBox();
            this.tabControl1.SuspendLayout();
            this.tp_PhysicalPara.SuspendLayout();
            this.gb_PhysicalPara_Network.SuspendLayout();
            this.gb_PhysicalPara_Serial.SuspendLayout();
            this.tp_LogicPara.SuspendLayout();
            this.gb_LogicPara.SuspendLayout();
            this.tp_ProtocolPara.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tp_PhysicalPara);
            this.tabControl1.Controls.Add(this.tp_LogicPara);
            this.tabControl1.Controls.Add(this.tp_ProtocolPara);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.ItemSize = new System.Drawing.Size(144, 40);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(825, 663);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 3;
            // 
            // tp_PhysicalPara
            // 
            this.tp_PhysicalPara.BackColor = System.Drawing.SystemColors.Control;
            this.tp_PhysicalPara.Controls.Add(this.cb_Enabled);
            this.tp_PhysicalPara.Controls.Add(this.tb_u16PortAddr);
            this.tp_PhysicalPara.Controls.Add(this.label9);
            this.tp_PhysicalPara.Controls.Add(this.lbl_PortName_1);
            this.tp_PhysicalPara.Controls.Add(this.gb_PhysicalPara_Network);
            this.tp_PhysicalPara.Controls.Add(this.gb_PhysicalPara_Serial);
            this.tp_PhysicalPara.Location = new System.Drawing.Point(4, 4);
            this.tp_PhysicalPara.Margin = new System.Windows.Forms.Padding(2);
            this.tp_PhysicalPara.Name = "tp_PhysicalPara";
            this.tp_PhysicalPara.Padding = new System.Windows.Forms.Padding(2);
            this.tp_PhysicalPara.Size = new System.Drawing.Size(817, 615);
            this.tp_PhysicalPara.TabIndex = 1;
            this.tp_PhysicalPara.Text = "物理参数";
            this.tp_PhysicalPara.Click += new System.EventHandler(this.tp_PhysicalPara_Click);
            // 
            // cb_Enabled
            // 
            this.cb_Enabled.AutoSize = true;
            this.cb_Enabled.Location = new System.Drawing.Point(125, 13);
            this.cb_Enabled.Margin = new System.Windows.Forms.Padding(2);
            this.cb_Enabled.Name = "cb_Enabled";
            this.cb_Enabled.Size = new System.Drawing.Size(72, 16);
            this.cb_Enabled.TabIndex = 21;
            this.cb_Enabled.Text = "是否使用";
            this.cb_Enabled.UseVisualStyleBackColor = true;
            this.cb_Enabled.CheckedChanged += new System.EventHandler(this.cb_Enabled_CheckedChanged);
            // 
            // tb_u16PortAddr
            // 
            this.tb_u16PortAddr.Location = new System.Drawing.Point(315, 10);
            this.tb_u16PortAddr.Margin = new System.Windows.Forms.Padding(2);
            this.tb_u16PortAddr.Name = "tb_u16PortAddr";
            this.tb_u16PortAddr.Size = new System.Drawing.Size(109, 21);
            this.tb_u16PortAddr.TabIndex = 19;
            this.tb_u16PortAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_u16PortAddr.TextChanged += new System.EventHandler(this.tb_u16PortAddr_TextChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(223, 13);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(89, 12);
            this.label9.TabIndex = 18;
            this.label9.Text = "地址(1～65535)";
            // 
            // lbl_PortName_1
            // 
            this.lbl_PortName_1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lbl_PortName_1.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_PortName_1.Location = new System.Drawing.Point(3, 3);
            this.lbl_PortName_1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_PortName_1.Name = "lbl_PortName_1";
            this.lbl_PortName_1.Size = new System.Drawing.Size(107, 30);
            this.lbl_PortName_1.TabIndex = 4;
            this.lbl_PortName_1.Text = "串口1";
            this.lbl_PortName_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gb_PhysicalPara_Network
            // 
            this.gb_PhysicalPara_Network.Controls.Add(this.tb_u8MAC_6);
            this.gb_PhysicalPara_Network.Controls.Add(this.label14);
            this.gb_PhysicalPara_Network.Controls.Add(this.tb_u8MAC_5);
            this.gb_PhysicalPara_Network.Controls.Add(this.label13);
            this.gb_PhysicalPara_Network.Controls.Add(this.tb_u8MAC_4);
            this.gb_PhysicalPara_Network.Controls.Add(this.label12);
            this.gb_PhysicalPara_Network.Controls.Add(this.tb_u8MAC_3);
            this.gb_PhysicalPara_Network.Controls.Add(this.label11);
            this.gb_PhysicalPara_Network.Controls.Add(this.tb_u8MAC_2);
            this.gb_PhysicalPara_Network.Controls.Add(this.label10);
            this.gb_PhysicalPara_Network.Controls.Add(this.tb_u8MAC_1);
            this.gb_PhysicalPara_Network.Controls.Add(this.label4);
            this.gb_PhysicalPara_Network.Controls.Add(this.btn_Network_OK);
            this.gb_PhysicalPara_Network.Controls.Add(this.tb_Network_Mask);
            this.gb_PhysicalPara_Network.Controls.Add(this.label3);
            this.gb_PhysicalPara_Network.Controls.Add(this.tb_Network_IP);
            this.gb_PhysicalPara_Network.Controls.Add(this.label6);
            this.gb_PhysicalPara_Network.Location = new System.Drawing.Point(323, 55);
            this.gb_PhysicalPara_Network.Margin = new System.Windows.Forms.Padding(2);
            this.gb_PhysicalPara_Network.Name = "gb_PhysicalPara_Network";
            this.gb_PhysicalPara_Network.Padding = new System.Windows.Forms.Padding(2);
            this.gb_PhysicalPara_Network.Size = new System.Drawing.Size(321, 187);
            this.gb_PhysicalPara_Network.TabIndex = 3;
            this.gb_PhysicalPara_Network.TabStop = false;
            this.gb_PhysicalPara_Network.Text = "网口参数";
            // 
            // tb_u8MAC_6
            // 
            this.tb_u8MAC_6.Location = new System.Drawing.Point(237, 122);
            this.tb_u8MAC_6.Margin = new System.Windows.Forms.Padding(2);
            this.tb_u8MAC_6.MaxLength = 2;
            this.tb_u8MAC_6.Name = "tb_u8MAC_6";
            this.tb_u8MAC_6.Size = new System.Drawing.Size(28, 21);
            this.tb_u8MAC_6.TabIndex = 19;
            this.tb_u8MAC_6.Tag = "100";
            this.tb_u8MAC_6.Text = "39";
            this.tb_u8MAC_6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_u8MAC_6.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_u8MAC_6_KeyPress);
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.SystemColors.Control;
            this.label14.Location = new System.Drawing.Point(229, 124);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(11, 14);
            this.label14.TabIndex = 18;
            this.label14.Tag = "100";
            this.label14.Text = "-";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tb_u8MAC_5
            // 
            this.tb_u8MAC_5.Location = new System.Drawing.Point(203, 122);
            this.tb_u8MAC_5.Margin = new System.Windows.Forms.Padding(2);
            this.tb_u8MAC_5.MaxLength = 2;
            this.tb_u8MAC_5.Name = "tb_u8MAC_5";
            this.tb_u8MAC_5.Size = new System.Drawing.Size(28, 21);
            this.tb_u8MAC_5.TabIndex = 17;
            this.tb_u8MAC_5.Tag = "100";
            this.tb_u8MAC_5.Text = "39";
            this.tb_u8MAC_5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_u8MAC_5.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_u8MAC_5_KeyPress);
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.SystemColors.Control;
            this.label13.Location = new System.Drawing.Point(195, 124);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(11, 14);
            this.label13.TabIndex = 16;
            this.label13.Tag = "100";
            this.label13.Text = "-";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tb_u8MAC_4
            // 
            this.tb_u8MAC_4.Location = new System.Drawing.Point(169, 122);
            this.tb_u8MAC_4.Margin = new System.Windows.Forms.Padding(2);
            this.tb_u8MAC_4.MaxLength = 2;
            this.tb_u8MAC_4.Name = "tb_u8MAC_4";
            this.tb_u8MAC_4.Size = new System.Drawing.Size(28, 21);
            this.tb_u8MAC_4.TabIndex = 15;
            this.tb_u8MAC_4.Tag = "100";
            this.tb_u8MAC_4.Text = "39";
            this.tb_u8MAC_4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_u8MAC_4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_u8MAC_4_KeyPress);
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.SystemColors.Control;
            this.label12.Location = new System.Drawing.Point(161, 124);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(11, 14);
            this.label12.TabIndex = 14;
            this.label12.Tag = "100";
            this.label12.Text = "-";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tb_u8MAC_3
            // 
            this.tb_u8MAC_3.Location = new System.Drawing.Point(135, 122);
            this.tb_u8MAC_3.Margin = new System.Windows.Forms.Padding(2);
            this.tb_u8MAC_3.MaxLength = 2;
            this.tb_u8MAC_3.Name = "tb_u8MAC_3";
            this.tb_u8MAC_3.Size = new System.Drawing.Size(28, 21);
            this.tb_u8MAC_3.TabIndex = 13;
            this.tb_u8MAC_3.Tag = "100";
            this.tb_u8MAC_3.Text = "39";
            this.tb_u8MAC_3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_u8MAC_3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_u8MAC_3_KeyPress);
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.SystemColors.Control;
            this.label11.Location = new System.Drawing.Point(127, 124);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(11, 14);
            this.label11.TabIndex = 12;
            this.label11.Tag = "100";
            this.label11.Text = "-";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tb_u8MAC_2
            // 
            this.tb_u8MAC_2.Location = new System.Drawing.Point(101, 122);
            this.tb_u8MAC_2.Margin = new System.Windows.Forms.Padding(2);
            this.tb_u8MAC_2.MaxLength = 2;
            this.tb_u8MAC_2.Name = "tb_u8MAC_2";
            this.tb_u8MAC_2.Size = new System.Drawing.Size(28, 21);
            this.tb_u8MAC_2.TabIndex = 11;
            this.tb_u8MAC_2.Tag = "100";
            this.tb_u8MAC_2.Text = "39";
            this.tb_u8MAC_2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_u8MAC_2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_u8MAC_2_KeyPress);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.SystemColors.Control;
            this.label10.Location = new System.Drawing.Point(93, 124);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(11, 14);
            this.label10.TabIndex = 10;
            this.label10.Tag = "100";
            this.label10.Text = "-";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tb_u8MAC_1
            // 
            this.tb_u8MAC_1.Location = new System.Drawing.Point(67, 122);
            this.tb_u8MAC_1.Margin = new System.Windows.Forms.Padding(2);
            this.tb_u8MAC_1.MaxLength = 2;
            this.tb_u8MAC_1.Name = "tb_u8MAC_1";
            this.tb_u8MAC_1.Size = new System.Drawing.Size(28, 21);
            this.tb_u8MAC_1.TabIndex = 9;
            this.tb_u8MAC_1.Tag = "100";
            this.tb_u8MAC_1.Text = "39";
            this.tb_u8MAC_1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_u8MAC_1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_u8MAC_1_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 125);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 8;
            this.label4.Tag = "100";
            this.label4.Text = "MAC地址";
            // 
            // btn_Network_OK
            // 
            this.btn_Network_OK.Location = new System.Drawing.Point(227, 147);
            this.btn_Network_OK.Margin = new System.Windows.Forms.Padding(2);
            this.btn_Network_OK.Name = "btn_Network_OK";
            this.btn_Network_OK.Size = new System.Drawing.Size(59, 30);
            this.btn_Network_OK.TabIndex = 7;
            this.btn_Network_OK.Text = "确认";
            this.btn_Network_OK.UseVisualStyleBackColor = true;
            this.btn_Network_OK.Click += new System.EventHandler(this.btn_Network_OK_Click);
            // 
            // tb_Network_Mask
            // 
            this.tb_Network_Mask.Location = new System.Drawing.Point(67, 83);
            this.tb_Network_Mask.Margin = new System.Windows.Forms.Padding(2);
            this.tb_Network_Mask.Name = "tb_Network_Mask";
            this.tb_Network_Mask.Size = new System.Drawing.Size(130, 21);
            this.tb_Network_Mask.TabIndex = 6;
            this.tb_Network_Mask.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_Network_Mask.TextChanged += new System.EventHandler(this.tb_Network_Mask_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 85);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "子网掩码";
            // 
            // tb_Network_IP
            // 
            this.tb_Network_IP.Location = new System.Drawing.Point(67, 50);
            this.tb_Network_IP.Margin = new System.Windows.Forms.Padding(2);
            this.tb_Network_IP.Name = "tb_Network_IP";
            this.tb_Network_IP.Size = new System.Drawing.Size(130, 21);
            this.tb_Network_IP.TabIndex = 3;
            this.tb_Network_IP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_Network_IP.TextChanged += new System.EventHandler(this.tb_Network_IP_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 53);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "IP地址";
            // 
            // gb_PhysicalPara_Serial
            // 
            this.gb_PhysicalPara_Serial.Controls.Add(this.cmb_SerialPort_CheckBit);
            this.gb_PhysicalPara_Serial.Controls.Add(this.cmb_SerialPort_Check);
            this.gb_PhysicalPara_Serial.Controls.Add(this.cmb_SerialPort_StopBit);
            this.gb_PhysicalPara_Serial.Controls.Add(this.cmb_SerialPort_StopLen);
            this.gb_PhysicalPara_Serial.Controls.Add(this.cmb_SerialPort_DataBit);
            this.gb_PhysicalPara_Serial.Controls.Add(this.label2);
            this.gb_PhysicalPara_Serial.Controls.Add(this.cmb_SerialPort_BaudRate);
            this.gb_PhysicalPara_Serial.Controls.Add(this.label1);
            this.gb_PhysicalPara_Serial.Location = new System.Drawing.Point(59, 55);
            this.gb_PhysicalPara_Serial.Margin = new System.Windows.Forms.Padding(2);
            this.gb_PhysicalPara_Serial.Name = "gb_PhysicalPara_Serial";
            this.gb_PhysicalPara_Serial.Padding = new System.Windows.Forms.Padding(2);
            this.gb_PhysicalPara_Serial.Size = new System.Drawing.Size(252, 187);
            this.gb_PhysicalPara_Serial.TabIndex = 2;
            this.gb_PhysicalPara_Serial.TabStop = false;
            this.gb_PhysicalPara_Serial.Text = "串口参数";
            // 
            // cmb_SerialPort_CheckBit
            // 
            this.cmb_SerialPort_CheckBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_SerialPort_CheckBit.FormattingEnabled = true;
            this.cmb_SerialPort_CheckBit.Items.AddRange(new object[] {
            "None-无校验",
            "Odd-奇校验",
            "Even-偶校验"});
            this.cmb_SerialPort_CheckBit.Location = new System.Drawing.Point(95, 152);
            this.cmb_SerialPort_CheckBit.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_SerialPort_CheckBit.Name = "cmb_SerialPort_CheckBit";
            this.cmb_SerialPort_CheckBit.Size = new System.Drawing.Size(119, 20);
            this.cmb_SerialPort_CheckBit.TabIndex = 7;
            this.cmb_SerialPort_CheckBit.SelectedIndexChanged += new System.EventHandler(this.cmb_SerialPort_Parity_SelectedIndexChanged);
            // 
            // cmb_SerialPort_Check
            // 
            this.cmb_SerialPort_Check.AutoSize = true;
            this.cmb_SerialPort_Check.Location = new System.Drawing.Point(51, 155);
            this.cmb_SerialPort_Check.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.cmb_SerialPort_Check.Name = "cmb_SerialPort_Check";
            this.cmb_SerialPort_Check.Size = new System.Drawing.Size(41, 12);
            this.cmb_SerialPort_Check.TabIndex = 6;
            this.cmb_SerialPort_Check.Text = "校验位";
            // 
            // cmb_SerialPort_StopBit
            // 
            this.cmb_SerialPort_StopBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_SerialPort_StopBit.FormattingEnabled = true;
            this.cmb_SerialPort_StopBit.Items.AddRange(new object[] {
            "1",
            "2"});
            this.cmb_SerialPort_StopBit.Location = new System.Drawing.Point(95, 111);
            this.cmb_SerialPort_StopBit.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_SerialPort_StopBit.Name = "cmb_SerialPort_StopBit";
            this.cmb_SerialPort_StopBit.Size = new System.Drawing.Size(119, 20);
            this.cmb_SerialPort_StopBit.TabIndex = 5;
            this.cmb_SerialPort_StopBit.SelectedIndexChanged += new System.EventHandler(this.cmb_SerialPort_StopBits_SelectedIndexChanged);
            // 
            // cmb_SerialPort_StopLen
            // 
            this.cmb_SerialPort_StopLen.AutoSize = true;
            this.cmb_SerialPort_StopLen.Location = new System.Drawing.Point(51, 114);
            this.cmb_SerialPort_StopLen.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.cmb_SerialPort_StopLen.Name = "cmb_SerialPort_StopLen";
            this.cmb_SerialPort_StopLen.Size = new System.Drawing.Size(41, 12);
            this.cmb_SerialPort_StopLen.TabIndex = 4;
            this.cmb_SerialPort_StopLen.Text = "停止位";
            // 
            // cmb_SerialPort_DataBit
            // 
            this.cmb_SerialPort_DataBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_SerialPort_DataBit.FormattingEnabled = true;
            this.cmb_SerialPort_DataBit.Items.AddRange(new object[] {
            "8",
            "7",
            "6",
            "5"});
            this.cmb_SerialPort_DataBit.Location = new System.Drawing.Point(95, 69);
            this.cmb_SerialPort_DataBit.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_SerialPort_DataBit.Name = "cmb_SerialPort_DataBit";
            this.cmb_SerialPort_DataBit.Size = new System.Drawing.Size(119, 20);
            this.cmb_SerialPort_DataBit.TabIndex = 3;
            this.cmb_SerialPort_DataBit.SelectedIndexChanged += new System.EventHandler(this.cmb_SerialPort_DataBits_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 73);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "数据位";
            // 
            // cmb_SerialPort_BaudRate
            // 
            this.cmb_SerialPort_BaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_SerialPort_BaudRate.FormattingEnabled = true;
            this.cmb_SerialPort_BaudRate.Items.AddRange(new object[] {
            "300",
            "600",
            "1200",
            "2400",
            "4800",
            "9600",
            "14400",
            "19200",
            "28800",
            "33600",
            "38400",
            "43000",
            "56000",
            "57600",
            "115200"});
            this.cmb_SerialPort_BaudRate.Location = new System.Drawing.Point(95, 28);
            this.cmb_SerialPort_BaudRate.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_SerialPort_BaudRate.Name = "cmb_SerialPort_BaudRate";
            this.cmb_SerialPort_BaudRate.Size = new System.Drawing.Size(119, 20);
            this.cmb_SerialPort_BaudRate.TabIndex = 1;
            this.cmb_SerialPort_BaudRate.SelectedIndexChanged += new System.EventHandler(this.cmb_SerialPort_BaudRate_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "波特率";
            // 
            // tp_LogicPara
            // 
            this.tp_LogicPara.BackColor = System.Drawing.SystemColors.Control;
            this.tp_LogicPara.Controls.Add(this.lbl_PortName_2);
            this.tp_LogicPara.Controls.Add(this.gb_LogicPara);
            this.tp_LogicPara.Location = new System.Drawing.Point(4, 4);
            this.tp_LogicPara.Margin = new System.Windows.Forms.Padding(2);
            this.tp_LogicPara.Name = "tp_LogicPara";
            this.tp_LogicPara.Size = new System.Drawing.Size(817, 615);
            this.tp_LogicPara.TabIndex = 2;
            this.tp_LogicPara.Text = "逻辑参数";
            this.tp_LogicPara.Click += new System.EventHandler(this.tp_LogicPara_Click);
            // 
            // lbl_PortName_2
            // 
            this.lbl_PortName_2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lbl_PortName_2.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_PortName_2.Location = new System.Drawing.Point(3, 3);
            this.lbl_PortName_2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_PortName_2.Name = "lbl_PortName_2";
            this.lbl_PortName_2.Size = new System.Drawing.Size(107, 30);
            this.lbl_PortName_2.TabIndex = 5;
            this.lbl_PortName_2.Text = "串口1";
            this.lbl_PortName_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gb_LogicPara
            // 
            this.gb_LogicPara.Controls.Add(this.cmb_LogicPara_Protocol);
            this.gb_LogicPara.Controls.Add(this.label7);
            this.gb_LogicPara.Controls.Add(this.cmb_LogicPara_Property);
            this.gb_LogicPara.Controls.Add(this.label8);
            this.gb_LogicPara.Location = new System.Drawing.Point(59, 55);
            this.gb_LogicPara.Margin = new System.Windows.Forms.Padding(2);
            this.gb_LogicPara.Name = "gb_LogicPara";
            this.gb_LogicPara.Padding = new System.Windows.Forms.Padding(2);
            this.gb_LogicPara.Size = new System.Drawing.Size(349, 187);
            this.gb_LogicPara.TabIndex = 3;
            this.gb_LogicPara.TabStop = false;
            this.gb_LogicPara.Text = "逻辑参数";
            // 
            // cmb_LogicPara_Protocol
            // 
            this.cmb_LogicPara_Protocol.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmb_LogicPara_Protocol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_LogicPara_Protocol.FormattingEnabled = true;
            this.cmb_LogicPara_Protocol.ItemHeight = 36;
            this.cmb_LogicPara_Protocol.Items.AddRange(new object[] {
            "对上101规约",
            "对上104规约",
            "对上MODBUS规约",
            "对上CDT规约",
            "对下101规约",
            "对下104规约",
            "对下MODBUS规约",
            "对下CDT规约",
            "对上101规约V1",
            "对上104规约V1"});
            this.cmb_LogicPara_Protocol.Location = new System.Drawing.Point(124, 103);
            this.cmb_LogicPara_Protocol.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_LogicPara_Protocol.Name = "cmb_LogicPara_Protocol";
            this.cmb_LogicPara_Protocol.Size = new System.Drawing.Size(180, 42);
            this.cmb_LogicPara_Protocol.TabIndex = 3;
            this.cmb_LogicPara_Protocol.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmb_LogicPara_Protocol_DrawItem);
            this.cmb_LogicPara_Protocol.SelectedIndexChanged += new System.EventHandler(this.cmb_LogicPara_Protocol_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(34, 114);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(85, 19);
            this.label7.TabIndex = 2;
            this.label7.Text = "规约选择";
            // 
            // cmb_LogicPara_Property
            // 
            this.cmb_LogicPara_Property.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmb_LogicPara_Property.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_LogicPara_Property.FormattingEnabled = true;
            this.cmb_LogicPara_Property.ItemHeight = 36;
            this.cmb_LogicPara_Property.Items.AddRange(new object[] {
            "对上",
            "对下"});
            this.cmb_LogicPara_Property.Location = new System.Drawing.Point(124, 37);
            this.cmb_LogicPara_Property.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_LogicPara_Property.Name = "cmb_LogicPara_Property";
            this.cmb_LogicPara_Property.Size = new System.Drawing.Size(180, 42);
            this.cmb_LogicPara_Property.TabIndex = 1;
            this.cmb_LogicPara_Property.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmb_LogicPara_Property_DrawItem);
            this.cmb_LogicPara_Property.SelectedIndexChanged += new System.EventHandler(this.cmb_LogicPara_Property_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(34, 49);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(85, 19);
            this.label8.TabIndex = 0;
            this.label8.Text = "逻辑属性";
            // 
            // tp_ProtocolPara
            // 
            this.tp_ProtocolPara.AutoScroll = true;
            this.tp_ProtocolPara.BackColor = System.Drawing.SystemColors.Control;
            this.tp_ProtocolPara.Controls.Add(this.lbl_PortName_3);
            this.tp_ProtocolPara.Controls.Add(this.gb_ProtocolPara);
            this.tp_ProtocolPara.Location = new System.Drawing.Point(4, 4);
            this.tp_ProtocolPara.Margin = new System.Windows.Forms.Padding(2);
            this.tp_ProtocolPara.Name = "tp_ProtocolPara";
            this.tp_ProtocolPara.Size = new System.Drawing.Size(817, 615);
            this.tp_ProtocolPara.TabIndex = 3;
            this.tp_ProtocolPara.Text = "规约参数";
            // 
            // lbl_PortName_3
            // 
            this.lbl_PortName_3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lbl_PortName_3.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_PortName_3.Location = new System.Drawing.Point(3, 3);
            this.lbl_PortName_3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_PortName_3.Name = "lbl_PortName_3";
            this.lbl_PortName_3.Size = new System.Drawing.Size(107, 30);
            this.lbl_PortName_3.TabIndex = 6;
            this.lbl_PortName_3.Text = "串口1";
            this.lbl_PortName_3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gb_ProtocolPara
            // 
            this.gb_ProtocolPara.Location = new System.Drawing.Point(59, 39);
            this.gb_ProtocolPara.Margin = new System.Windows.Forms.Padding(2);
            this.gb_ProtocolPara.Name = "gb_ProtocolPara";
            this.gb_ProtocolPara.Padding = new System.Windows.Forms.Padding(2);
            this.gb_ProtocolPara.Size = new System.Drawing.Size(615, 548);
            this.gb_ProtocolPara.TabIndex = 4;
            this.gb_ProtocolPara.TabStop = false;
            this.gb_ProtocolPara.Text = "规约参数";
            // 
            // Form_Table_Port_Name
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 663);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form_Table_Port_Name";
            this.Text = "Form_Table_Port_Name";
            this.Load += new System.EventHandler(this.Form_Table_Port_Name_Load);
            this.tabControl1.ResumeLayout(false);
            this.tp_PhysicalPara.ResumeLayout(false);
            this.tp_PhysicalPara.PerformLayout();
            this.gb_PhysicalPara_Network.ResumeLayout(false);
            this.gb_PhysicalPara_Network.PerformLayout();
            this.gb_PhysicalPara_Serial.ResumeLayout(false);
            this.gb_PhysicalPara_Serial.PerformLayout();
            this.tp_LogicPara.ResumeLayout(false);
            this.gb_LogicPara.ResumeLayout(false);
            this.gb_LogicPara.PerformLayout();
            this.tp_ProtocolPara.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tp_PhysicalPara;
        private System.Windows.Forms.TabPage tp_LogicPara;
        private System.Windows.Forms.TabPage tp_ProtocolPara;
        private System.Windows.Forms.GroupBox gb_PhysicalPara_Serial;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_SerialPort_CheckBit;
        private System.Windows.Forms.Label cmb_SerialPort_Check;
        private System.Windows.Forms.ComboBox cmb_SerialPort_StopBit;
        private System.Windows.Forms.Label cmb_SerialPort_StopLen;
        private System.Windows.Forms.ComboBox cmb_SerialPort_DataBit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_SerialPort_BaudRate;
        private System.Windows.Forms.GroupBox gb_PhysicalPara_Network;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_Network_IP;
        private System.Windows.Forms.GroupBox gb_LogicPara;
        private System.Windows.Forms.ComboBox cmb_LogicPara_Protocol;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmb_LogicPara_Property;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox gb_ProtocolPara;
        private System.Windows.Forms.Label lbl_PortName_1;
        private System.Windows.Forms.Label lbl_PortName_2;
        private System.Windows.Forms.Label lbl_PortName_3;
        private System.Windows.Forms.TextBox tb_u16PortAddr;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox cb_Enabled;
        private System.Windows.Forms.TextBox tb_Network_Mask;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Network_OK;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_u8MAC_1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tb_u8MAC_6;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tb_u8MAC_5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tb_u8MAC_4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tb_u8MAC_3;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox tb_u8MAC_2;
    }
}