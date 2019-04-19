namespace CfgTool
{
    partial class Form_CfgTool
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_CfgTool));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsb_New = new System.Windows.Forms.ToolStripButton();
            this.tsb_Open = new System.Windows.Forms.ToolStripButton();
            this.tsb_Save = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_NaviState = new System.Windows.Forms.ToolStripButton();
            this.tsb_Setup = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_CfgImport = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmi_Import_Para = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Import_Setting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Import_Model = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Import_Device = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Import_FDB = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Import_Port = new System.Windows.Forms.ToolStripMenuItem();
            this.tsb_CftExport = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmi_Export_Para = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Export_Setting = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Export_Model = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Export_Device = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Export_FDB = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Export_Port = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_SaveAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_Exit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_About = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssl_Tip = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tvNavi = new System.Windows.Forms.TreeView();
            this.cms_Para = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_RecoveryDefaultPara = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ImportParaConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_GenerateParaConfigFile = new System.Windows.Forms.ToolStripMenuItem();
            this.cms_Model = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_Template_Import = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_GenerateModelConfigFile = new System.Windows.Forms.ToolStripMenuItem();
            this.cms_Device = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_AddDevice = new System.Windows.Forms.ToolStripMenuItem();
            this.tmi_SaveDeviceConfigFile = new System.Windows.Forms.ToolStripMenuItem();
            this.cms_DeviceTable_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.cms_RTDB = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_AfreshGenerateRealData = new System.Windows.Forms.ToolStripMenuItem();
            this.cms_FWT = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_AddFDB = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_GenerateFDBConfigFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_YCSet = new System.Windows.Forms.ToolStripMenuItem();
            this.cms_Port = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_GeneratePortConfigFile = new System.Windows.Forms.ToolStripMenuItem();
            this.cms_Model_Delete = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_Template_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.cms_Device_Delete = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_Device_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.cms_FWT_Delete = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_FDB_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_FDB_AllFW = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_FDB_AllNotFW = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_FDB_Model_Fwt = new System.Windows.Forms.ToolStripMenuItem();
            this.cms_Setting = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_Setting_RecoveryDefaultPara = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Setting_ImportParaConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Setting_GenerateParaConfigFile = new System.Windows.Forms.ToolStripMenuItem();
            this.cms_DeviceTable = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_AddDeviceTable = new System.Windows.Forms.ToolStripMenuItem();
            this.tmi_SaveDeviceTableConfigFile = new System.Windows.Forms.ToolStripMenuItem();
            this.timer_telnet = new System.Windows.Forms.Timer(this.components);
            this.tsb_AtuoConfig = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.cms_Para.SuspendLayout();
            this.cms_Model.SuspendLayout();
            this.cms_Device.SuspendLayout();
            this.cms_RTDB.SuspendLayout();
            this.cms_FWT.SuspendLayout();
            this.cms_Port.SuspendLayout();
            this.cms_Model_Delete.SuspendLayout();
            this.cms_Device_Delete.SuspendLayout();
            this.cms_FWT_Delete.SuspendLayout();
            this.cms_Setting.SuspendLayout();
            this.cms_DeviceTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_New,
            this.tsb_Open,
            this.tsb_Save,
            this.toolStripSeparator6,
            this.tsb_NaviState,
            this.tsb_Setup,
            this.toolStripSeparator1,
            this.tsb_CfgImport,
            this.tsb_CftExport,
            this.tsb_AtuoConfig,
            this.toolStripSeparator2,
            this.tsb_Exit,
            this.toolStripSeparator3,
            this.tsb_About});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1113, 78);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsb_New
            // 
            this.tsb_New.Image = ((System.Drawing.Image)(resources.GetObject("tsb_New.Image")));
            this.tsb_New.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_New.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_New.Name = "tsb_New";
            this.tsb_New.Size = new System.Drawing.Size(55, 75);
            this.tsb_New.Text = "新建";
            this.tsb_New.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_New.Click += new System.EventHandler(this.tsb_New_Click);
            // 
            // tsb_Open
            // 
            this.tsb_Open.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Open.Image")));
            this.tsb_Open.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_Open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Open.Name = "tsb_Open";
            this.tsb_Open.Size = new System.Drawing.Size(55, 75);
            this.tsb_Open.Text = "打开";
            this.tsb_Open.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Open.Click += new System.EventHandler(this.tsb_Open_Click);
            // 
            // tsb_Save
            // 
            this.tsb_Save.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Save.Image")));
            this.tsb_Save.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Save.Name = "tsb_Save";
            this.tsb_Save.Size = new System.Drawing.Size(55, 75);
            this.tsb_Save.Text = "保存";
            this.tsb_Save.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Save.Click += new System.EventHandler(this.tsb_Save_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 78);
            // 
            // tsb_NaviState
            // 
            this.tsb_NaviState.Image = global::CfgTool.Properties.Resources.hidetv;
            this.tsb_NaviState.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_NaviState.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_NaviState.Name = "tsb_NaviState";
            this.tsb_NaviState.Size = new System.Drawing.Size(56, 75);
            this.tsb_NaviState.Text = "隐藏";
            this.tsb_NaviState.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_NaviState.Click += new System.EventHandler(this.tsb_NaviState_Click);
            // 
            // tsb_Setup
            // 
            this.tsb_Setup.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Setup.Image")));
            this.tsb_Setup.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_Setup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Setup.Name = "tsb_Setup";
            this.tsb_Setup.Size = new System.Drawing.Size(55, 75);
            this.tsb_Setup.Text = "设置";
            this.tsb_Setup.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Setup.Click += new System.EventHandler(this.tsb_Setup_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 78);
            // 
            // tsb_CfgImport
            // 
            this.tsb_CfgImport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_Import_Para,
            this.tsmi_Import_Setting,
            this.tsmi_Import_Model,
            this.tsmi_Import_Device,
            this.tsmi_Import_FDB,
            this.tsmi_Import_Port});
            this.tsb_CfgImport.Image = ((System.Drawing.Image)(resources.GetObject("tsb_CfgImport.Image")));
            this.tsb_CfgImport.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_CfgImport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_CfgImport.Name = "tsb_CfgImport";
            this.tsb_CfgImport.Size = new System.Drawing.Size(69, 75);
            this.tsb_CfgImport.Text = "配置导入";
            this.tsb_CfgImport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsmi_Import_Para
            // 
            this.tsmi_Import_Para.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_Import_Para.Image")));
            this.tsmi_Import_Para.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmi_Import_Para.Name = "tsmi_Import_Para";
            this.tsmi_Import_Para.Size = new System.Drawing.Size(156, 42);
            this.tsmi_Import_Para.Text = "参数和定值";
            this.tsmi_Import_Para.Click += new System.EventHandler(this.tsmi_Import_Para_Click);
            // 
            // tsmi_Import_Setting
            // 
            this.tsmi_Import_Setting.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_Import_Setting.Image")));
            this.tsmi_Import_Setting.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmi_Import_Setting.Name = "tsmi_Import_Setting";
            this.tsmi_Import_Setting.Size = new System.Drawing.Size(156, 42);
            this.tsmi_Import_Setting.Text = "定值";
            this.tsmi_Import_Setting.Visible = false;
            this.tsmi_Import_Setting.Click += new System.EventHandler(this.tsmi_Import_Setting_Click);
            // 
            // tsmi_Import_Model
            // 
            this.tsmi_Import_Model.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_Import_Model.Image")));
            this.tsmi_Import_Model.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmi_Import_Model.Name = "tsmi_Import_Model";
            this.tsmi_Import_Model.Size = new System.Drawing.Size(156, 42);
            this.tsmi_Import_Model.Text = "模板";
            this.tsmi_Import_Model.Click += new System.EventHandler(this.tsmi_Import_Model_Click);
            // 
            // tsmi_Import_Device
            // 
            this.tsmi_Import_Device.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_Import_Device.Image")));
            this.tsmi_Import_Device.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmi_Import_Device.Name = "tsmi_Import_Device";
            this.tsmi_Import_Device.Size = new System.Drawing.Size(156, 42);
            this.tsmi_Import_Device.Text = "设备";
            this.tsmi_Import_Device.Click += new System.EventHandler(this.tsmi_Import_Device_Click);
            // 
            // tsmi_Import_FDB
            // 
            this.tsmi_Import_FDB.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_Import_FDB.Image")));
            this.tsmi_Import_FDB.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmi_Import_FDB.Name = "tsmi_Import_FDB";
            this.tsmi_Import_FDB.Size = new System.Drawing.Size(156, 42);
            this.tsmi_Import_FDB.Text = "转发表";
            this.tsmi_Import_FDB.Click += new System.EventHandler(this.tsmi_Import_FDB_Click);
            // 
            // tsmi_Import_Port
            // 
            this.tsmi_Import_Port.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_Import_Port.Image")));
            this.tsmi_Import_Port.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmi_Import_Port.Name = "tsmi_Import_Port";
            this.tsmi_Import_Port.Size = new System.Drawing.Size(156, 42);
            this.tsmi_Import_Port.Text = "端口";
            this.tsmi_Import_Port.Click += new System.EventHandler(this.tsmi_Import_Port_Click);
            // 
            // tsb_CftExport
            // 
            this.tsb_CftExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_Export_Para,
            this.tsmi_Export_Setting,
            this.tsmi_Export_Model,
            this.tsmi_Export_Device,
            this.tsmi_Export_FDB,
            this.tsmi_Export_Port,
            this.toolStripSeparator4,
            this.tsmi_SaveAll});
            this.tsb_CftExport.Image = ((System.Drawing.Image)(resources.GetObject("tsb_CftExport.Image")));
            this.tsb_CftExport.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_CftExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_CftExport.Name = "tsb_CftExport";
            this.tsb_CftExport.Size = new System.Drawing.Size(69, 75);
            this.tsb_CftExport.Text = "配置导出";
            this.tsb_CftExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            // 
            // tsmi_Export_Para
            // 
            this.tsmi_Export_Para.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_Export_Para.Image")));
            this.tsmi_Export_Para.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmi_Export_Para.Name = "tsmi_Export_Para";
            this.tsmi_Export_Para.Size = new System.Drawing.Size(156, 42);
            this.tsmi_Export_Para.Text = "参数和定值";
            this.tsmi_Export_Para.Click += new System.EventHandler(this.tsmi_Export_Para_Click);
            // 
            // tsmi_Export_Setting
            // 
            this.tsmi_Export_Setting.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_Export_Setting.Image")));
            this.tsmi_Export_Setting.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmi_Export_Setting.Name = "tsmi_Export_Setting";
            this.tsmi_Export_Setting.Size = new System.Drawing.Size(156, 42);
            this.tsmi_Export_Setting.Text = "定值";
            this.tsmi_Export_Setting.Visible = false;
            this.tsmi_Export_Setting.Click += new System.EventHandler(this.tsmi_Export_Setting_Click);
            // 
            // tsmi_Export_Model
            // 
            this.tsmi_Export_Model.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_Export_Model.Image")));
            this.tsmi_Export_Model.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmi_Export_Model.Name = "tsmi_Export_Model";
            this.tsmi_Export_Model.Size = new System.Drawing.Size(156, 42);
            this.tsmi_Export_Model.Text = "模板";
            this.tsmi_Export_Model.Click += new System.EventHandler(this.tsmi_Export_Model_Click);
            // 
            // tsmi_Export_Device
            // 
            this.tsmi_Export_Device.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_Export_Device.Image")));
            this.tsmi_Export_Device.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmi_Export_Device.Name = "tsmi_Export_Device";
            this.tsmi_Export_Device.Size = new System.Drawing.Size(156, 42);
            this.tsmi_Export_Device.Text = "设备";
            this.tsmi_Export_Device.Click += new System.EventHandler(this.tsmi_Export_Device_Click);
            // 
            // tsmi_Export_FDB
            // 
            this.tsmi_Export_FDB.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_Export_FDB.Image")));
            this.tsmi_Export_FDB.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmi_Export_FDB.Name = "tsmi_Export_FDB";
            this.tsmi_Export_FDB.Size = new System.Drawing.Size(156, 42);
            this.tsmi_Export_FDB.Text = "转发表";
            this.tsmi_Export_FDB.Click += new System.EventHandler(this.tsmi_Export_FDB_Click);
            // 
            // tsmi_Export_Port
            // 
            this.tsmi_Export_Port.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_Export_Port.Image")));
            this.tsmi_Export_Port.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmi_Export_Port.Name = "tsmi_Export_Port";
            this.tsmi_Export_Port.Size = new System.Drawing.Size(156, 42);
            this.tsmi_Export_Port.Text = "端口";
            this.tsmi_Export_Port.Click += new System.EventHandler(this.tsmi_Export_Port_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(153, 6);
            // 
            // tsmi_SaveAll
            // 
            this.tsmi_SaveAll.Image = ((System.Drawing.Image)(resources.GetObject("tsmi_SaveAll.Image")));
            this.tsmi_SaveAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmi_SaveAll.Name = "tsmi_SaveAll";
            this.tsmi_SaveAll.Size = new System.Drawing.Size(156, 42);
            this.tsmi_SaveAll.Text = "一键保存";
            this.tsmi_SaveAll.Click += new System.EventHandler(this.tsmi_SaveAll_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 78);
            // 
            // tsb_Exit
            // 
            this.tsb_Exit.Image = ((System.Drawing.Image)(resources.GetObject("tsb_Exit.Image")));
            this.tsb_Exit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_Exit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_Exit.Name = "tsb_Exit";
            this.tsb_Exit.Size = new System.Drawing.Size(55, 75);
            this.tsb_Exit.Text = "退出";
            this.tsb_Exit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_Exit.Click += new System.EventHandler(this.tsb_Exit_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 78);
            // 
            // tsb_About
            // 
            this.tsb_About.Image = ((System.Drawing.Image)(resources.GetObject("tsb_About.Image")));
            this.tsb_About.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_About.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_About.Name = "tsb_About";
            this.tsb_About.Size = new System.Drawing.Size(55, 75);
            this.tsb_About.Text = "关于";
            this.tsb_About.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_About.Click += new System.EventHandler(this.tsb_About_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssl_Tip});
            this.statusStrip1.Location = new System.Drawing.Point(0, 412);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 9, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1113, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssl_Tip
            // 
            this.tssl_Tip.Name = "tssl_Tip";
            this.tssl_Tip.Size = new System.Drawing.Size(0, 17);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 78);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(2);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tvNavi);
            this.splitContainer1.Size = new System.Drawing.Size(1113, 334);
            this.splitContainer1.SplitterDistance = 268;
            this.splitContainer1.SplitterWidth = 3;
            this.splitContainer1.TabIndex = 2;
            // 
            // tvNavi
            // 
            this.tvNavi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvNavi.ItemHeight = 30;
            this.tvNavi.Location = new System.Drawing.Point(0, 0);
            this.tvNavi.Margin = new System.Windows.Forms.Padding(2);
            this.tvNavi.Name = "tvNavi";
            this.tvNavi.Size = new System.Drawing.Size(268, 334);
            this.tvNavi.TabIndex = 0;
            this.tvNavi.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvNavi_AfterSelect);
            this.tvNavi.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tvNavi_MouseClick);
            // 
            // cms_Para
            // 
            this.cms_Para.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_RecoveryDefaultPara,
            this.tsmi_ImportParaConfig,
            this.tsmi_GenerateParaConfigFile});
            this.cms_Para.Name = "cms_Para";
            this.cms_Para.Size = new System.Drawing.Size(173, 70);
            // 
            // tsmi_RecoveryDefaultPara
            // 
            this.tsmi_RecoveryDefaultPara.Name = "tsmi_RecoveryDefaultPara";
            this.tsmi_RecoveryDefaultPara.Size = new System.Drawing.Size(172, 22);
            this.tsmi_RecoveryDefaultPara.Text = "恢复默认参数";
            this.tsmi_RecoveryDefaultPara.Click += new System.EventHandler(this.tsmi_RecoveryDefaultPara_Click);
            // 
            // tsmi_ImportParaConfig
            // 
            this.tsmi_ImportParaConfig.Name = "tsmi_ImportParaConfig";
            this.tsmi_ImportParaConfig.Size = new System.Drawing.Size(172, 22);
            this.tsmi_ImportParaConfig.Text = "导入参数配置";
            this.tsmi_ImportParaConfig.Visible = false;
            this.tsmi_ImportParaConfig.Click += new System.EventHandler(this.tsmi_ImportParaConfig_Click);
            // 
            // tsmi_GenerateParaConfigFile
            // 
            this.tsmi_GenerateParaConfigFile.Name = "tsmi_GenerateParaConfigFile";
            this.tsmi_GenerateParaConfigFile.Size = new System.Drawing.Size(172, 22);
            this.tsmi_GenerateParaConfigFile.Text = "生成参数配置文件";
            this.tsmi_GenerateParaConfigFile.Visible = false;
            this.tsmi_GenerateParaConfigFile.Click += new System.EventHandler(this.tsmi_GenerateParaConfigFile_Click);
            // 
            // cms_Model
            // 
            this.cms_Model.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_Template_Import,
            this.tsmi_GenerateModelConfigFile});
            this.cms_Model.Name = "cms_Template";
            this.cms_Model.Size = new System.Drawing.Size(173, 48);
            // 
            // tsmi_Template_Import
            // 
            this.tsmi_Template_Import.Name = "tsmi_Template_Import";
            this.tsmi_Template_Import.Size = new System.Drawing.Size(172, 22);
            this.tsmi_Template_Import.Text = "导入模板";
            this.tsmi_Template_Import.Click += new System.EventHandler(this.tsmi_Template_Import_Click);
            // 
            // tsmi_GenerateModelConfigFile
            // 
            this.tsmi_GenerateModelConfigFile.Name = "tsmi_GenerateModelConfigFile";
            this.tsmi_GenerateModelConfigFile.Size = new System.Drawing.Size(172, 22);
            this.tsmi_GenerateModelConfigFile.Text = "生成模板配置文件";
            this.tsmi_GenerateModelConfigFile.Click += new System.EventHandler(this.tsmi_GenerateModelConfigFile_Click);
            // 
            // cms_Device
            // 
            this.cms_Device.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_AddDevice,
            this.tmi_SaveDeviceConfigFile,
            this.cms_DeviceTable_Delete});
            this.cms_Device.Name = "cms_Device";
            this.cms_Device.Size = new System.Drawing.Size(173, 70);
            // 
            // tsmi_AddDevice
            // 
            this.tsmi_AddDevice.Name = "tsmi_AddDevice";
            this.tsmi_AddDevice.Size = new System.Drawing.Size(172, 22);
            this.tsmi_AddDevice.Text = "新增设备";
            this.tsmi_AddDevice.Click += new System.EventHandler(this.tsmi_AddDevice_Click);
            // 
            // tmi_SaveDeviceConfigFile
            // 
            this.tmi_SaveDeviceConfigFile.Name = "tmi_SaveDeviceConfigFile";
            this.tmi_SaveDeviceConfigFile.Size = new System.Drawing.Size(172, 22);
            this.tmi_SaveDeviceConfigFile.Text = "保存设备配置文件";
            this.tmi_SaveDeviceConfigFile.Visible = false;
            this.tmi_SaveDeviceConfigFile.Click += new System.EventHandler(this.tmi_SaveDeviceConfigFile_Click);
            // 
            // cms_DeviceTable_Delete
            // 
            this.cms_DeviceTable_Delete.Name = "cms_DeviceTable_Delete";
            this.cms_DeviceTable_Delete.Size = new System.Drawing.Size(172, 22);
            this.cms_DeviceTable_Delete.Text = "删除设备表";
            this.cms_DeviceTable_Delete.Click += new System.EventHandler(this.cms_DeviceTable_Delete_Click);
            // 
            // cms_RTDB
            // 
            this.cms_RTDB.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_AfreshGenerateRealData});
            this.cms_RTDB.Name = "cms_RealData";
            this.cms_RTDB.Size = new System.Drawing.Size(161, 26);
            // 
            // tsmi_AfreshGenerateRealData
            // 
            this.tsmi_AfreshGenerateRealData.Name = "tsmi_AfreshGenerateRealData";
            this.tsmi_AfreshGenerateRealData.Size = new System.Drawing.Size(160, 22);
            this.tsmi_AfreshGenerateRealData.Text = "重新生成实时库";
            this.tsmi_AfreshGenerateRealData.Click += new System.EventHandler(this.tsmi_AfreshGenerateRealData_Click);
            // 
            // cms_FWT
            // 
            this.cms_FWT.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_AddFDB,
            this.tsmi_GenerateFDBConfigFile,
            this.tsmi_YCSet});
            this.cms_FWT.Name = "cms_FDB";
            this.cms_FWT.Size = new System.Drawing.Size(197, 70);
            // 
            // tsmi_AddFDB
            // 
            this.tsmi_AddFDB.Name = "tsmi_AddFDB";
            this.tsmi_AddFDB.Size = new System.Drawing.Size(196, 22);
            this.tsmi_AddFDB.Text = "新增转发表";
            this.tsmi_AddFDB.Click += new System.EventHandler(this.tsmi_AddFDB_Click);
            // 
            // tsmi_GenerateFDBConfigFile
            // 
            this.tsmi_GenerateFDBConfigFile.Name = "tsmi_GenerateFDBConfigFile";
            this.tsmi_GenerateFDBConfigFile.Size = new System.Drawing.Size(196, 22);
            this.tsmi_GenerateFDBConfigFile.Text = "生成转发表配置文件";
            this.tsmi_GenerateFDBConfigFile.Click += new System.EventHandler(this.tsmi_GenerateFDBConfigFile_Click);
            // 
            // tsmi_YCSet
            // 
            this.tsmi_YCSet.Name = "tsmi_YCSet";
            this.tsmi_YCSet.Size = new System.Drawing.Size(196, 22);
            this.tsmi_YCSet.Text = "遥测最大值和系数设置";
            this.tsmi_YCSet.Visible = false;
            this.tsmi_YCSet.Click += new System.EventHandler(this.tsmi_YCSet_Click);
            // 
            // cms_Port
            // 
            this.cms_Port.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_GeneratePortConfigFile});
            this.cms_Port.Name = "cms_Port";
            this.cms_Port.Size = new System.Drawing.Size(173, 26);
            // 
            // tsmi_GeneratePortConfigFile
            // 
            this.tsmi_GeneratePortConfigFile.Name = "tsmi_GeneratePortConfigFile";
            this.tsmi_GeneratePortConfigFile.Size = new System.Drawing.Size(172, 22);
            this.tsmi_GeneratePortConfigFile.Text = "生成端口配置文件";
            this.tsmi_GeneratePortConfigFile.Click += new System.EventHandler(this.tsmi_GeneratePortConfigFile_Click);
            // 
            // cms_Model_Delete
            // 
            this.cms_Model_Delete.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_Template_Delete});
            this.cms_Model_Delete.Name = "cms_Template_Delete";
            this.cms_Model_Delete.Size = new System.Drawing.Size(125, 26);
            // 
            // tsmi_Template_Delete
            // 
            this.tsmi_Template_Delete.Name = "tsmi_Template_Delete";
            this.tsmi_Template_Delete.Size = new System.Drawing.Size(124, 22);
            this.tsmi_Template_Delete.Text = "删除模板";
            this.tsmi_Template_Delete.Click += new System.EventHandler(this.tsmi_Template_Delete_Click);
            // 
            // cms_Device_Delete
            // 
            this.cms_Device_Delete.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_Device_Delete});
            this.cms_Device_Delete.Name = "cms_Device_2";
            this.cms_Device_Delete.Size = new System.Drawing.Size(125, 26);
            // 
            // tsmi_Device_Delete
            // 
            this.tsmi_Device_Delete.Name = "tsmi_Device_Delete";
            this.tsmi_Device_Delete.Size = new System.Drawing.Size(124, 22);
            this.tsmi_Device_Delete.Text = "删除设备";
            this.tsmi_Device_Delete.Click += new System.EventHandler(this.tsmi_Device_Delete_Click);
            // 
            // cms_FWT_Delete
            // 
            this.cms_FWT_Delete.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_FDB_Delete,
            this.toolStripSeparator5,
            this.tsmi_FDB_AllFW,
            this.tsmi_FDB_AllNotFW,
            this.toolStripSeparator7,
            this.tsmi_FDB_Model_Fwt});
            this.cms_FWT_Delete.Name = "cms_FDB_2";
            this.cms_FWT_Delete.Size = new System.Drawing.Size(161, 104);
            // 
            // tsmi_FDB_Delete
            // 
            this.tsmi_FDB_Delete.Name = "tsmi_FDB_Delete";
            this.tsmi_FDB_Delete.Size = new System.Drawing.Size(160, 22);
            this.tsmi_FDB_Delete.Text = "删除转发表";
            this.tsmi_FDB_Delete.Click += new System.EventHandler(this.tsmi_FDB_Delete_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(157, 6);
            // 
            // tsmi_FDB_AllFW
            // 
            this.tsmi_FDB_AllFW.Name = "tsmi_FDB_AllFW";
            this.tsmi_FDB_AllFW.Size = new System.Drawing.Size(160, 22);
            this.tsmi_FDB_AllFW.Text = "全部转发";
            this.tsmi_FDB_AllFW.Click += new System.EventHandler(this.tsmi_FDB_AllFW_Click);
            // 
            // tsmi_FDB_AllNotFW
            // 
            this.tsmi_FDB_AllNotFW.Name = "tsmi_FDB_AllNotFW";
            this.tsmi_FDB_AllNotFW.Size = new System.Drawing.Size(160, 22);
            this.tsmi_FDB_AllNotFW.Text = "全不转发";
            this.tsmi_FDB_AllNotFW.Click += new System.EventHandler(this.tsmi_FDB_AllNotFW_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(157, 6);
            // 
            // tsmi_FDB_Model_Fwt
            // 
            this.tsmi_FDB_Model_Fwt.Name = "tsmi_FDB_Model_Fwt";
            this.tsmi_FDB_Model_Fwt.Size = new System.Drawing.Size(160, 22);
            this.tsmi_FDB_Model_Fwt.Text = "生成转发表模板";
            this.tsmi_FDB_Model_Fwt.Click += new System.EventHandler(this.tsmi_FDB_Model_Fwt_Click);
            // 
            // cms_Setting
            // 
            this.cms_Setting.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_Setting_RecoveryDefaultPara,
            this.tsmi_Setting_ImportParaConfig,
            this.tsmi_Setting_GenerateParaConfigFile});
            this.cms_Setting.Name = "cms_Para";
            this.cms_Setting.Size = new System.Drawing.Size(173, 70);
            // 
            // tsmi_Setting_RecoveryDefaultPara
            // 
            this.tsmi_Setting_RecoveryDefaultPara.Name = "tsmi_Setting_RecoveryDefaultPara";
            this.tsmi_Setting_RecoveryDefaultPara.Size = new System.Drawing.Size(172, 22);
            this.tsmi_Setting_RecoveryDefaultPara.Text = "恢复默认定值";
            this.tsmi_Setting_RecoveryDefaultPara.Click += new System.EventHandler(this.tsmi_Setting_RecoveryDefaultPara_Click);
            // 
            // tsmi_Setting_ImportParaConfig
            // 
            this.tsmi_Setting_ImportParaConfig.Name = "tsmi_Setting_ImportParaConfig";
            this.tsmi_Setting_ImportParaConfig.Size = new System.Drawing.Size(172, 22);
            this.tsmi_Setting_ImportParaConfig.Text = "导入定值配置";
            this.tsmi_Setting_ImportParaConfig.Visible = false;
            this.tsmi_Setting_ImportParaConfig.Click += new System.EventHandler(this.tsmi_Setting_ImportParaConfig_Click);
            // 
            // tsmi_Setting_GenerateParaConfigFile
            // 
            this.tsmi_Setting_GenerateParaConfigFile.Name = "tsmi_Setting_GenerateParaConfigFile";
            this.tsmi_Setting_GenerateParaConfigFile.Size = new System.Drawing.Size(172, 22);
            this.tsmi_Setting_GenerateParaConfigFile.Text = "生成定值配置文件";
            this.tsmi_Setting_GenerateParaConfigFile.Visible = false;
            this.tsmi_Setting_GenerateParaConfigFile.Click += new System.EventHandler(this.tsmi_Setting_GenerateParaConfigFile_Click);
            // 
            // cms_DeviceTable
            // 
            this.cms_DeviceTable.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_AddDeviceTable,
            this.tmi_SaveDeviceTableConfigFile});
            this.cms_DeviceTable.Name = "cms_Device";
            this.cms_DeviceTable.Size = new System.Drawing.Size(185, 48);
            // 
            // tsmi_AddDeviceTable
            // 
            this.tsmi_AddDeviceTable.Name = "tsmi_AddDeviceTable";
            this.tsmi_AddDeviceTable.Size = new System.Drawing.Size(184, 22);
            this.tsmi_AddDeviceTable.Text = "新增设备表";
            this.tsmi_AddDeviceTable.Click += new System.EventHandler(this.tsmi_AddDeviceTable_Click);
            // 
            // tmi_SaveDeviceTableConfigFile
            // 
            this.tmi_SaveDeviceTableConfigFile.Name = "tmi_SaveDeviceTableConfigFile";
            this.tmi_SaveDeviceTableConfigFile.Size = new System.Drawing.Size(184, 22);
            this.tmi_SaveDeviceTableConfigFile.Text = "生成设备表配置文件";
            this.tmi_SaveDeviceTableConfigFile.Click += new System.EventHandler(this.tmi_SaveDeviceTableConfigFile_Click);
            // 
            // tsb_AtuoConfig
            // 
            this.tsb_AtuoConfig.Image = global::CfgTool.Properties.Resources.auto;
            this.tsb_AtuoConfig.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_AtuoConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_AtuoConfig.Name = "tsb_AtuoConfig";
            this.tsb_AtuoConfig.Size = new System.Drawing.Size(60, 75);
            this.tsb_AtuoConfig.Text = "自动配置";
            this.tsb_AtuoConfig.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsb_AtuoConfig.Click += new System.EventHandler(this.tsb_AtuoConfig_Click);
            // 
            // Form_CfgTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 434);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form_CfgTool";
            this.Text = "配置工具";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_CfgTool_FormClosing);
            this.Load += new System.EventHandler(this.Form_CfgTool_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_CfgTool_KeyDown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.cms_Para.ResumeLayout(false);
            this.cms_Model.ResumeLayout(false);
            this.cms_Device.ResumeLayout(false);
            this.cms_RTDB.ResumeLayout(false);
            this.cms_FWT.ResumeLayout(false);
            this.cms_Port.ResumeLayout(false);
            this.cms_Model_Delete.ResumeLayout(false);
            this.cms_Device_Delete.ResumeLayout(false);
            this.cms_FWT_Delete.ResumeLayout(false);
            this.cms_Setting.ResumeLayout(false);
            this.cms_DeviceTable.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsb_New;
        private System.Windows.Forms.ToolStripButton tsb_Open;
        private System.Windows.Forms.ToolStripButton tsb_Save;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsb_Exit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsb_About;
        private System.Windows.Forms.TreeView tvNavi;
        private System.Windows.Forms.ToolStripDropDownButton tsb_CfgImport;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Import_Para;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Import_Device;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Import_FDB;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Import_Port;
        private System.Windows.Forms.ToolStripDropDownButton tsb_CftExport;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Export_Para;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Export_Device;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Export_FDB;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Export_Port;
        private System.Windows.Forms.ContextMenuStrip cms_Para;
        private System.Windows.Forms.ToolStripMenuItem tsmi_RecoveryDefaultPara;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ImportParaConfig;
        private System.Windows.Forms.ToolStripMenuItem tsmi_GenerateParaConfigFile;
        private System.Windows.Forms.ContextMenuStrip cms_Model;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Template_Import;
        private System.Windows.Forms.ContextMenuStrip cms_Device;
        private System.Windows.Forms.ToolStripMenuItem tsmi_AddDevice;
        private System.Windows.Forms.ToolStripMenuItem tmi_SaveDeviceConfigFile;
        private System.Windows.Forms.ContextMenuStrip cms_RTDB;
        private System.Windows.Forms.ToolStripMenuItem tsmi_AfreshGenerateRealData;
        private System.Windows.Forms.ContextMenuStrip cms_FWT;
        private System.Windows.Forms.ContextMenuStrip cms_Port;
        private System.Windows.Forms.ToolStripMenuItem tsmi_AddFDB;
        private System.Windows.Forms.ToolStripMenuItem tsmi_GenerateFDBConfigFile;
        private System.Windows.Forms.ToolStripMenuItem tsmi_GeneratePortConfigFile;
        private System.Windows.Forms.ContextMenuStrip cms_Model_Delete;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Template_Delete;
        private System.Windows.Forms.ContextMenuStrip cms_Device_Delete;
        private System.Windows.Forms.ContextMenuStrip cms_FWT_Delete;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Device_Delete;
        private System.Windows.Forms.ToolStripMenuItem tsmi_FDB_Delete;
        private System.Windows.Forms.ContextMenuStrip cms_Setting;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Setting_RecoveryDefaultPara;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Setting_ImportParaConfig;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Setting_GenerateParaConfigFile;
        private System.Windows.Forms.ToolStripMenuItem tsmi_GenerateModelConfigFile;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Import_Model;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Export_Model;
        private System.Windows.Forms.ContextMenuStrip cms_DeviceTable;
        private System.Windows.Forms.ToolStripMenuItem tsmi_AddDeviceTable;
        private System.Windows.Forms.ToolStripMenuItem tmi_SaveDeviceTableConfigFile;
        private System.Windows.Forms.ToolStripMenuItem cms_DeviceTable_Delete;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Import_Setting;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Export_Setting;
        private System.Windows.Forms.ToolStripStatusLabel tssl_Tip;
        private System.Windows.Forms.ToolStripMenuItem tsmi_YCSet;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tsmi_SaveAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem tsmi_FDB_AllFW;
        private System.Windows.Forms.ToolStripMenuItem tsmi_FDB_AllNotFW;
        private System.Windows.Forms.ToolStripButton tsb_Setup;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem tsmi_FDB_Model_Fwt;
        private System.Windows.Forms.Timer timer_telnet;
        private System.Windows.Forms.ToolStripButton tsb_NaviState;
        private System.Windows.Forms.ToolStripButton tsb_AtuoConfig;
    }
}

