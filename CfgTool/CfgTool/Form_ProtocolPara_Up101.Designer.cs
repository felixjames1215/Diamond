namespace CfgTool
{
    partial class Form_ProtocolPara_Up101
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.cmb_bHaveSafetyLayer = new System.Windows.Forms.ComboBox();
            this.cmb_u8Mode = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmb_u8StationType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_u16LinkAddr = new System.Windows.Forms.TextBox();
            this.cmb_u8LinkAddrLen = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_u8ResendInterval = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_u8ResendNum = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmb_u8Dir = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tb_u16AppAddr = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmb_u8AppAddrLen = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmb_u8CotLen = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmb_u8ObjAddrLen = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tb_u16YkValidTime = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.tb_u16IC_Time = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tb_u16CI_Time = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.lbl_Title = new System.Windows.Forms.Label();
            this.cmb_u8YcSendTYP = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.tb_u16CmdTime = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.tb_u16TimeDiffLimit = new System.Windows.Forms.TextBox();
            this.cb_Res1 = new System.Windows.Forms.CheckBox();
            this.tb_YcChgSendCycle = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.pb_Dir = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pb_LinkMode = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Dir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_LinkMode)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(87, 24);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "是否加密";
            // 
            // cmb_bHaveSafetyLayer
            // 
            this.cmb_bHaveSafetyLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_bHaveSafetyLayer.FormattingEnabled = true;
            this.cmb_bHaveSafetyLayer.Items.AddRange(new object[] {
            "否",
            "是"});
            this.cmb_bHaveSafetyLayer.Location = new System.Drawing.Point(143, 21);
            this.cmb_bHaveSafetyLayer.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_bHaveSafetyLayer.Name = "cmb_bHaveSafetyLayer";
            this.cmb_bHaveSafetyLayer.Size = new System.Drawing.Size(109, 20);
            this.cmb_bHaveSafetyLayer.TabIndex = 1;
            this.cmb_bHaveSafetyLayer.SelectedIndexChanged += new System.EventHandler(this.cmb_bHaveSafetyLayer_SelectedIndexChanged);
            // 
            // cmb_u8Mode
            // 
            this.cmb_u8Mode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_u8Mode.FormattingEnabled = true;
            this.cmb_u8Mode.Items.AddRange(new object[] {
            "非平衡式",
            "平衡式"});
            this.cmb_u8Mode.Location = new System.Drawing.Point(143, 94);
            this.cmb_u8Mode.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_u8Mode.Name = "cmb_u8Mode";
            this.cmb_u8Mode.Size = new System.Drawing.Size(109, 20);
            this.cmb_u8Mode.TabIndex = 3;
            this.cmb_u8Mode.SelectedIndexChanged += new System.EventHandler(this.cmb_u8Mode_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(87, 97);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "链路模式";
            // 
            // cmb_u8StationType
            // 
            this.cmb_u8StationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_u8StationType.FormattingEnabled = true;
            this.cmb_u8StationType.Items.AddRange(new object[] {
            "主控站",
            "被控站"});
            this.cmb_u8StationType.Location = new System.Drawing.Point(143, 129);
            this.cmb_u8StationType.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_u8StationType.Name = "cmb_u8StationType";
            this.cmb_u8StationType.Size = new System.Drawing.Size(109, 20);
            this.cmb_u8StationType.TabIndex = 5;
            this.cmb_u8StationType.SelectedIndexChanged += new System.EventHandler(this.cmb_u8StationType_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(99, 133);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "站类型";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(39, 169);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "链路地址(1～254)";
            // 
            // tb_u16LinkAddr
            // 
            this.tb_u16LinkAddr.Location = new System.Drawing.Point(143, 164);
            this.tb_u16LinkAddr.Margin = new System.Windows.Forms.Padding(2);
            this.tb_u16LinkAddr.MaxLength = 3;
            this.tb_u16LinkAddr.Name = "tb_u16LinkAddr";
            this.tb_u16LinkAddr.Size = new System.Drawing.Size(109, 21);
            this.tb_u16LinkAddr.TabIndex = 7;
            this.tb_u16LinkAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_u16LinkAddr.TextChanged += new System.EventHandler(this.tb_u16LinkAddr_TextChanged);
            this.tb_u16LinkAddr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_u16LinkAddr_KeyPress);
            // 
            // cmb_u8LinkAddrLen
            // 
            this.cmb_u8LinkAddrLen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_u8LinkAddrLen.FormattingEnabled = true;
            this.cmb_u8LinkAddrLen.Items.AddRange(new object[] {
            "1",
            "2"});
            this.cmb_u8LinkAddrLen.Location = new System.Drawing.Point(143, 201);
            this.cmb_u8LinkAddrLen.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_u8LinkAddrLen.Name = "cmb_u8LinkAddrLen";
            this.cmb_u8LinkAddrLen.Size = new System.Drawing.Size(109, 20);
            this.cmb_u8LinkAddrLen.TabIndex = 9;
            this.cmb_u8LinkAddrLen.SelectedIndexChanged += new System.EventHandler(this.cmb_u8LinkAddrLen_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(27, 205);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "链路地址长度(1～2)";
            // 
            // tb_u8ResendInterval
            // 
            this.tb_u8ResendInterval.Location = new System.Drawing.Point(143, 236);
            this.tb_u8ResendInterval.Margin = new System.Windows.Forms.Padding(2);
            this.tb_u8ResendInterval.MaxLength = 2;
            this.tb_u8ResendInterval.Name = "tb_u8ResendInterval";
            this.tb_u8ResendInterval.Size = new System.Drawing.Size(109, 21);
            this.tb_u8ResendInterval.TabIndex = 11;
            this.tb_u8ResendInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_u8ResendInterval.TextChanged += new System.EventHandler(this.tb_u8ResendInterval_TextChanged);
            this.tb_u8ResendInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_u8ResendInterval_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(39, 241);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 12);
            this.label6.TabIndex = 10;
            this.label6.Text = "重发间隔(1～30s)";
            // 
            // tb_u8ResendNum
            // 
            this.tb_u8ResendNum.Location = new System.Drawing.Point(143, 273);
            this.tb_u8ResendNum.Margin = new System.Windows.Forms.Padding(2);
            this.tb_u8ResendNum.MaxLength = 1;
            this.tb_u8ResendNum.Name = "tb_u8ResendNum";
            this.tb_u8ResendNum.Size = new System.Drawing.Size(109, 21);
            this.tb_u8ResendNum.TabIndex = 13;
            this.tb_u8ResendNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_u8ResendNum.TextChanged += new System.EventHandler(this.tb_u8ResendNum_TextChanged);
            this.tb_u8ResendNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_u8ResendNum_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(51, 277);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 12;
            this.label7.Text = "重发次数(0～5)";
            // 
            // cmb_u8Dir
            // 
            this.cmb_u8Dir.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_u8Dir.FormattingEnabled = true;
            this.cmb_u8Dir.Items.AddRange(new object[] {
            "0",
            "1"});
            this.cmb_u8Dir.Location = new System.Drawing.Point(143, 310);
            this.cmb_u8Dir.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_u8Dir.Name = "cmb_u8Dir";
            this.cmb_u8Dir.Size = new System.Drawing.Size(109, 20);
            this.cmb_u8Dir.TabIndex = 15;
            this.cmb_u8Dir.SelectedIndexChanged += new System.EventHandler(this.cmb_u8Dir_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(105, 313);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "DIR位";
            // 
            // tb_u16AppAddr
            // 
            this.tb_u16AppAddr.Location = new System.Drawing.Point(477, 57);
            this.tb_u16AppAddr.Margin = new System.Windows.Forms.Padding(2);
            this.tb_u16AppAddr.MaxLength = 3;
            this.tb_u16AppAddr.Name = "tb_u16AppAddr";
            this.tb_u16AppAddr.Size = new System.Drawing.Size(109, 21);
            this.tb_u16AppAddr.TabIndex = 17;
            this.tb_u16AppAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_u16AppAddr.TextChanged += new System.EventHandler(this.tb_u16AppAddr_TextChanged);
            this.tb_u16AppAddr.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_u16AppAddr_KeyPress);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(373, 60);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(101, 12);
            this.label9.TabIndex = 16;
            this.label9.Text = "应用地址(1～254)";
            // 
            // cmb_u8AppAddrLen
            // 
            this.cmb_u8AppAddrLen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_u8AppAddrLen.FormattingEnabled = true;
            this.cmb_u8AppAddrLen.Items.AddRange(new object[] {
            "1",
            "2"});
            this.cmb_u8AppAddrLen.Location = new System.Drawing.Point(477, 92);
            this.cmb_u8AppAddrLen.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_u8AppAddrLen.Name = "cmb_u8AppAddrLen";
            this.cmb_u8AppAddrLen.Size = new System.Drawing.Size(109, 20);
            this.cmb_u8AppAddrLen.TabIndex = 19;
            this.cmb_u8AppAddrLen.SelectedIndexChanged += new System.EventHandler(this.cmb_u8AppAddrLen_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(349, 96);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(125, 12);
            this.label10.TabIndex = 18;
            this.label10.Text = "应用层地址长度(1～2)";
            // 
            // cmb_u8CotLen
            // 
            this.cmb_u8CotLen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_u8CotLen.FormattingEnabled = true;
            this.cmb_u8CotLen.Items.AddRange(new object[] {
            "1",
            "2"});
            this.cmb_u8CotLen.Location = new System.Drawing.Point(477, 127);
            this.cmb_u8CotLen.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_u8CotLen.Name = "cmb_u8CotLen";
            this.cmb_u8CotLen.Size = new System.Drawing.Size(109, 20);
            this.cmb_u8CotLen.TabIndex = 21;
            this.cmb_u8CotLen.SelectedIndexChanged += new System.EventHandler(this.cmb_u8CotLen_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(361, 132);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(113, 12);
            this.label11.TabIndex = 20;
            this.label11.Text = "传送原因长度(1～2)";
            // 
            // cmb_u8ObjAddrLen
            // 
            this.cmb_u8ObjAddrLen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_u8ObjAddrLen.FormattingEnabled = true;
            this.cmb_u8ObjAddrLen.Items.AddRange(new object[] {
            "2",
            "3"});
            this.cmb_u8ObjAddrLen.Location = new System.Drawing.Point(477, 164);
            this.cmb_u8ObjAddrLen.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_u8ObjAddrLen.Name = "cmb_u8ObjAddrLen";
            this.cmb_u8ObjAddrLen.Size = new System.Drawing.Size(109, 20);
            this.cmb_u8ObjAddrLen.TabIndex = 23;
            this.cmb_u8ObjAddrLen.SelectedIndexChanged += new System.EventHandler(this.cmb_u8ObjAddrLen_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(337, 168);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(137, 12);
            this.label12.TabIndex = 22;
            this.label12.Text = "信息对象地址长度(2～3)";
            // 
            // tb_u16YkValidTime
            // 
            this.tb_u16YkValidTime.Location = new System.Drawing.Point(477, 241);
            this.tb_u16YkValidTime.Margin = new System.Windows.Forms.Padding(2);
            this.tb_u16YkValidTime.MaxLength = 4;
            this.tb_u16YkValidTime.Name = "tb_u16YkValidTime";
            this.tb_u16YkValidTime.Size = new System.Drawing.Size(109, 21);
            this.tb_u16YkValidTime.TabIndex = 25;
            this.tb_u16YkValidTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_u16YkValidTime.TextChanged += new System.EventHandler(this.tb_u8YkValidTime_TextChanged);
            this.tb_u16YkValidTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_u16YkValidTime_KeyPress);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(331, 246);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(143, 12);
            this.label13.TabIndex = 24;
            this.label13.Text = "遥控有效时间(10～3600s)";
            // 
            // tb_u16IC_Time
            // 
            this.tb_u16IC_Time.Location = new System.Drawing.Point(477, 275);
            this.tb_u16IC_Time.Margin = new System.Windows.Forms.Padding(2);
            this.tb_u16IC_Time.MaxLength = 4;
            this.tb_u16IC_Time.Name = "tb_u16IC_Time";
            this.tb_u16IC_Time.Size = new System.Drawing.Size(109, 21);
            this.tb_u16IC_Time.TabIndex = 27;
            this.tb_u16IC_Time.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_u16IC_Time.TextChanged += new System.EventHandler(this.tb_u8IC_Time_TextChanged);
            this.tb_u16IC_Time.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_u16IC_Time_KeyPress);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(307, 280);
            this.label14.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(167, 12);
            this.label14.TabIndex = 26;
            this.label14.Text = "站总召唤间隔时间(10～3600s)";
            // 
            // tb_u16CI_Time
            // 
            this.tb_u16CI_Time.Location = new System.Drawing.Point(477, 311);
            this.tb_u16CI_Time.Margin = new System.Windows.Forms.Padding(2);
            this.tb_u16CI_Time.MaxLength = 4;
            this.tb_u16CI_Time.Name = "tb_u16CI_Time";
            this.tb_u16CI_Time.Size = new System.Drawing.Size(109, 21);
            this.tb_u16CI_Time.TabIndex = 29;
            this.tb_u16CI_Time.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_u16CI_Time.TextChanged += new System.EventHandler(this.tb_u8CI_Time_TextChanged);
            this.tb_u16CI_Time.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_u16CI_Time_KeyPress);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(283, 316);
            this.label15.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(191, 12);
            this.label15.TabIndex = 28;
            this.label15.Text = "电度量总召唤间隔时间(10～3600s)";
            // 
            // lbl_Title
            // 
            this.lbl_Title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lbl_Title.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_Title.Location = new System.Drawing.Point(368, 13);
            this.lbl_Title.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(217, 30);
            this.lbl_Title.TabIndex = 30;
            this.lbl_Title.Text = "101规约配置参数";
            this.lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmb_u8YcSendTYP
            // 
            this.cmb_u8YcSendTYP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_u8YcSendTYP.FormattingEnabled = true;
            this.cmb_u8YcSendTYP.Items.AddRange(new object[] {
            "ASDU9归一化值",
            "ASDU11标度化值",
            "ASDU13短浮点数",
            "ASDU21归一化值",
            "ASDU34归一化值(时标)",
            "ASDU35标度化值(时标)",
            "ASDU36短浮点数(时标)"});
            this.cmb_u8YcSendTYP.Location = new System.Drawing.Point(477, 202);
            this.cmb_u8YcSendTYP.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_u8YcSendTYP.Name = "cmb_u8YcSendTYP";
            this.cmb_u8YcSendTYP.Size = new System.Drawing.Size(109, 20);
            this.cmb_u8YcSendTYP.TabIndex = 32;
            this.cmb_u8YcSendTYP.SelectedIndexChanged += new System.EventHandler(this.cmb_u8YcSendTYP_SelectedIndexChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(397, 206);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(77, 12);
            this.label16.TabIndex = 31;
            this.label16.Text = "遥测上送类型";
            // 
            // tb_u16CmdTime
            // 
            this.tb_u16CmdTime.Location = new System.Drawing.Point(476, 345);
            this.tb_u16CmdTime.Margin = new System.Windows.Forms.Padding(2);
            this.tb_u16CmdTime.MaxLength = 4;
            this.tb_u16CmdTime.Name = "tb_u16CmdTime";
            this.tb_u16CmdTime.Size = new System.Drawing.Size(109, 21);
            this.tb_u16CmdTime.TabIndex = 34;
            this.tb_u16CmdTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_u16CmdTime.TextChanged += new System.EventHandler(this.tb_u16CmdTime_TextChanged);
            this.tb_u16CmdTime.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_u16CmdTime_KeyPress);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(330, 350);
            this.label17.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(143, 12);
            this.label17.TabIndex = 33;
            this.label17.Text = "对时时间间隔(10～3600s)";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(9, 60);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(131, 12);
            this.label18.TabIndex = 35;
            this.label18.Text = "超时重放限值(10～60s)";
            // 
            // tb_u16TimeDiffLimit
            // 
            this.tb_u16TimeDiffLimit.Location = new System.Drawing.Point(143, 57);
            this.tb_u16TimeDiffLimit.Margin = new System.Windows.Forms.Padding(2);
            this.tb_u16TimeDiffLimit.MaxLength = 4;
            this.tb_u16TimeDiffLimit.Name = "tb_u16TimeDiffLimit";
            this.tb_u16TimeDiffLimit.Size = new System.Drawing.Size(109, 21);
            this.tb_u16TimeDiffLimit.TabIndex = 36;
            this.tb_u16TimeDiffLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_u16TimeDiffLimit.TextChanged += new System.EventHandler(this.tb_u16TimeDiffLimit_TextChanged);
            this.tb_u16TimeDiffLimit.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_u16TimeDiffLimit_KeyPress);
            // 
            // cb_Res1
            // 
            this.cb_Res1.AutoSize = true;
            this.cb_Res1.Location = new System.Drawing.Point(168, 369);
            this.cb_Res1.Name = "cb_Res1";
            this.cb_Res1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cb_Res1.Size = new System.Drawing.Size(120, 16);
            this.cb_Res1.TabIndex = 97;
            this.cb_Res1.Text = "变化遥测一问一答";
            this.cb_Res1.UseVisualStyleBackColor = true;
            this.cb_Res1.Visible = false;
            this.cb_Res1.CheckedChanged += new System.EventHandler(this.cb_Res1_CheckedChanged);
            // 
            // tb_YcChgSendCycle
            // 
            this.tb_YcChgSendCycle.Location = new System.Drawing.Point(476, 375);
            this.tb_YcChgSendCycle.Margin = new System.Windows.Forms.Padding(2);
            this.tb_YcChgSendCycle.MaxLength = 4;
            this.tb_YcChgSendCycle.Name = "tb_YcChgSendCycle";
            this.tb_YcChgSendCycle.Size = new System.Drawing.Size(109, 21);
            this.tb_YcChgSendCycle.TabIndex = 99;
            this.tb_YcChgSendCycle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tb_YcChgSendCycle.TextChanged += new System.EventHandler(this.tb_YcChgSendCycle_TextChanged);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(311, 380);
            this.label19.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(161, 12);
            this.label19.TabIndex = 98;
            this.label19.Text = "变化遥测上送周期(0~1000ms)";
            // 
            // pb_Dir
            // 
            this.pb_Dir.Image = global::CfgTool.Properties.Resources.tip;
            this.pb_Dir.Location = new System.Drawing.Point(255, 310);
            this.pb_Dir.Name = "pb_Dir";
            this.pb_Dir.Size = new System.Drawing.Size(22, 22);
            this.pb_Dir.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_Dir.TabIndex = 100;
            this.pb_Dir.TabStop = false;
            this.pb_Dir.MouseEnter += new System.EventHandler(this.pb_Dir_MouseEnter);
            // 
            // pb_LinkMode
            // 
            this.pb_LinkMode.Image = global::CfgTool.Properties.Resources.tip;
            this.pb_LinkMode.Location = new System.Drawing.Point(255, 94);
            this.pb_LinkMode.Name = "pb_LinkMode";
            this.pb_LinkMode.Size = new System.Drawing.Size(22, 22);
            this.pb_LinkMode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pb_LinkMode.TabIndex = 101;
            this.pb_LinkMode.TabStop = false;
            this.pb_LinkMode.MouseEnter += new System.EventHandler(this.pb_LinkMode_MouseEnter);
            // 
            // Form_ProtocolPara_Up101
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(642, 457);
            this.Controls.Add(this.pb_LinkMode);
            this.Controls.Add(this.pb_Dir);
            this.Controls.Add(this.tb_YcChgSendCycle);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.cb_Res1);
            this.Controls.Add(this.tb_u16TimeDiffLimit);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.tb_u16CmdTime);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.cmb_u8YcSendTYP);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.lbl_Title);
            this.Controls.Add(this.tb_u16CI_Time);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.tb_u16IC_Time);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.tb_u16YkValidTime);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.cmb_u8ObjAddrLen);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.cmb_u8CotLen);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.cmb_u8AppAddrLen);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.tb_u16AppAddr);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.cmb_u8Dir);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tb_u8ResendNum);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tb_u8ResendInterval);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmb_u8LinkAddrLen);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tb_u16LinkAddr);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmb_u8StationType);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cmb_u8Mode);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmb_bHaveSafetyLayer);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_ProtocolPara_Up101";
            this.Text = "Form_ProtocolPara_Up101";
            this.Load += new System.EventHandler(this.Form_ProtocolPara_Up101_Load);
            this.Click += new System.EventHandler(this.Form_ProtocolPara_Up101_Click);
            ((System.ComponentModel.ISupportInitialize)(this.pb_Dir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_LinkMode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmb_bHaveSafetyLayer;
        private System.Windows.Forms.ComboBox cmb_u8Mode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmb_u8StationType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_u16LinkAddr;
        private System.Windows.Forms.ComboBox cmb_u8LinkAddrLen;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_u8ResendInterval;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_u8ResendNum;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmb_u8Dir;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tb_u16AppAddr;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmb_u8AppAddrLen;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmb_u8CotLen;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmb_u8ObjAddrLen;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tb_u16YkValidTime;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox tb_u16IC_Time;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox tb_u16CI_Time;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.ComboBox cmb_u8YcSendTYP;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox tb_u16CmdTime;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox tb_u16TimeDiffLimit;
        private System.Windows.Forms.CheckBox cb_Res1;
        private System.Windows.Forms.TextBox tb_YcChgSendCycle;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.PictureBox pb_Dir;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.PictureBox pb_LinkMode;
    }
}