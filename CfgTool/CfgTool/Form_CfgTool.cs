using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace CfgTool
{
    public partial class Form_CfgTool : Form
    {
        ////////////////////////////////////////////////////
        #region "定义"
        #region "树形视图"
        TreeNode tn_set_Para;//参数
        TreeNode tn_set_Setting;//定值
        TreeNode tn_set_Model;//模板
        TreeNode tn_set_DeviceTable;//设备
        TreeNode tn_set_RTDB;//实时库
        TreeNode tn_set_FWT;//转发表
        TreeNode tn_set_Port;//端口
        #endregion
        public static Form_CfgTool pMainForm;
        public Form_Info formInfo = new Form_Info();
        //参数格式化成字符串时，统一使用
        public const int iSize = 96;
        public byte[] bName = new byte[iSize];
        public string sName = "";
        #region "常量"
        const char splitChar = ';';
        class CST_SET
        {
            public const short Index_Para = 1;
            public const short Index_Setting = 2;
            public const short Index_YC = 3;
            public const short Index_SYX = 4;
            public const short Index_DYX = 5;
            public const short Index_YK = 6;
            public const short Index_Meter = 7;
            public const short Index_Port = 8;
            public const short Index_GYX = 9;
        }
        #endregion

        public delegate void MyInvoke(string str1, string str2);
        public delegate void MyInvokeTelnet(string str1, string str2);

        static string strBaseFolder = "D:\\CfgFile\\";
        static string strBaseFolderServer = "D:\\CfgFile\\Server\\";
        static string strBaseFolderClient = "D:\\CfgFile\\Client\\";
        string strModelPath = "";
        List<string> listFile = new List<string>();
        public int giComId = 0;
        public int giNetId = 0;
        public byte[] gbytServerIP = new byte[4];
        public byte[] gbytClientIP = new byte[4];
        public bool gbCopyFileFlag = false;
        //将串口配置从单选框 改成 复选框，jifeng，2018-9-26 10:12
        public bool[] gbComChecked = new bool[4];
        public int[] giComType = new int[4];
        #endregion

        #region "构造"
        public Form_CfgTool()
        {
            InitializeComponent();
            pMainForm = this;
        }
        #endregion

        #region "Load和Closing"
        private void Form_CfgTool_Load(object sender, EventArgs e)
        {
            Global.readINI();
            initFormSize();
            initTreeView();
        }

        private void Form_CfgTool_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;
            Environment.Exit(0);
        }
        #endregion

        #region "初始化窗体大小和位置"
        void initFormSize()
        {
            //1.窗体长和宽是屏幕的80%
            //这个区域不包括任务栏的
            //Rectangle ScreenArea = System.Windows.Forms.Screen.GetWorkingArea(this);
            //这个区域包括任务栏，就是屏幕显示的物理范围
            Rectangle ScreenArea = System.Windows.Forms.Screen.GetBounds(this);
            int width = ScreenArea.Width; //屏幕宽度 
            int height = ScreenArea.Height; //屏幕高度
            this.Width = (int)(width * 0.8);
            this.Height = (int)(height * 0.8);
            //2.窗体最大化
            this.WindowState = FormWindowState.Maximized;
            this.CenterToScreen();
        }
        #endregion

        #region "initTreeView(初始化左侧树)"
        private void initTreeView()
        {
            tvNavi.Font = new Font(tvNavi.Font.FontFamily, 14.5f);
            //----------------------------------------------------
            tn_set_Para = new TreeNode(Global.cst_Set_Para);
            tvNavi.Nodes.Add(tn_set_Para);
            //----
            tn_set_Setting = new TreeNode(Global.cst_Set_Setting);
            tvNavi.Nodes.Add(tn_set_Setting);
            //----
            tn_set_Model = new TreeNode(Global.cst_Set_Model);
            tvNavi.Nodes.Add(tn_set_Model);
            //----
            tn_set_DeviceTable = new TreeNode(Global.cst_Set_DeviceTable);
            tvNavi.Nodes.Add(tn_set_DeviceTable);
            //----
            tn_set_RTDB = new TreeNode(Global.cst_Set_RTDB);
            tvNavi.Nodes.Add(tn_set_RTDB);
            //----
            tn_set_FWT = new TreeNode(Global.cst_Set_FWT);
            tvNavi.Nodes.Add(tn_set_FWT);
            //----
            tn_set_Port = new TreeNode(Global.cst_Set_Port);
            tvNavi.Nodes.Add(tn_set_Port);
            //----------------------------------------------------
            tvNavi.ExpandAll();
            tn_set_Para.Collapse();
            tn_set_Setting.Collapse();
        }
        #endregion

        #region "操作日志子窗体依附"
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            formInfo.Show(this);
            formInfo.Visible = false;
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            formInfo.Height = this.Height / 4;
            formInfo.Width = this.Width;
            formInfo.Top = this.Top + this.Height * 3 / 4;
            formInfo.Left = this.Left;
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            formInfo.Height = this.Height / 4;
            formInfo.Width = this.Width;
            formInfo.Top = this.Top + this.Height * 3 / 4;
            formInfo.Left = this.Left;
        }
        #endregion

        #region "KeyDown"
        private void Form_CfgTool_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Control) && e.KeyCode == Keys.S)//显示/隐藏日志信息窗口
            {
                if (formInfo.Visible == true)
                {
                    formInfo.Visible = false;
                }
                else
                {
                    formInfo.Visible = true;
                }
            }
            else if ((e.Control) && e.KeyCode == Keys.D)
            {
                try
                {
                    if (Global.g_FtpEnable == 0) { return; };
                    if (Global.g_FtpIp.Trim() == "") { return; };

                    this.Text = this.Text + ".";

                    //Thread threadA = new Thread(ftpSend);
                    //threadA.Start();

                    Thread thread = new Thread(new ThreadStart(ftpSend));
                    thread.Start();
                }
                catch
                {
                }
            }
            if ((e.Control) && e.KeyCode == Keys.F)
            {
                Thread threadTelnetInit = new Thread(new ThreadStart(telnetInit));
                threadTelnetInit.Start();
            }
            //--------------------------------------
        }

        private void ftpSend()
        {
            FtpWeb gFtp1 = new FtpWeb(Global.g_FtpIp, "/ram/", "lyjr", "Ok0001@Njlyjr");
            FtpWeb gFtp2 = new FtpWeb(Global.g_FtpIp, "/C/", "lyjr", "Ok0001@Njlyjr");

            if (Global.g_FtpDspCore == 1)
            {
                gFtp1.Upload("D:\\DspCore.bin");
            }
            if (Global.g_FtpArmCore == 1)
            {
                gFtp2.Upload("D:\\ArmCore.out");
            }
            if (Global.g_FtpArmCore1 == 1)
            {
                gFtp2.Upload("D:\\ArmCore-1.out");
            }
            MyInvoke mi = new MyInvoke(UpdateForm);
            this.BeginInvoke(mi, new Object[] { "", "" });
        }

        public void UpdateForm(string param1, string parm2)
        {
            this.Text = "配置工具";
        }

        public void UpdateForm2(string param1, string parm2)
        {
            this.Text = "配置工具" + param1;
        }

        private void telnetInit()
        {
            Global.g_telnetConn = new TelnetConnection("192.168.1.151", 23);

            //login with user "root",password "rootpassword", using a timeout of 100ms, and show server output
            string s = Global.g_telnetConn.Login("lyjr", "Ok0001@Njlyjr", 900);
            //Console.Write(s);

            // server output should end with "$" or ">", otherwise the connection failed
            string prompt = s.TrimEnd();
            prompt = s.Substring(prompt.Length - 1, 1);
            if (prompt != "$" && prompt != ">")
            {
                throw new Exception("open prj failed...");
            }
            else
            {
                Thread threadTelnetProcess = new Thread(new ThreadStart(telnetProcess));
                threadTelnetProcess.Start();

                if (Global.g_FtpDspCore == 1)
                {
                    Global.g_telnetQueue.Enqueue("wd");
                    Global.g_telnetQueue.Enqueue("rep");
                }
                else
                {
                    Global.g_telnetQueue.Enqueue("rep");
                }

                MyInvoke mi = new MyInvoke(UpdateForm2);
                this.BeginInvoke(mi, new Object[] { "..", "" });
            }
        }
        private void telnetProcess()
        {
            string prompt = "";
            int iCount = 0;
            // while connected
            while (Global.g_telnetConn.IsConnected && prompt.Trim() != "exit")
            {
                // display server output
                //Console.Write(Global.g_telnetConn.Read());

                // send client input to server
                //prompt = Console.ReadLine();
                prompt = "";
                if (Global.g_telnetQueue.Count > 0 && iCount == 0)
                {
                    prompt = Global.g_telnetQueue.Dequeue();
                    iCount = 100;
                }
                if (prompt != "")
                {
                    Global.g_telnetConn.WriteLine(prompt);
                    if (prompt == "rep")
                    {
                        prompt = "exit";
                    }
                }
                if (iCount > 0)
                {
                    iCount -= 1;
                }
                Thread.Sleep(100);
                // display server output
                //Console.Write(Global.g_telnetConn.Read());
            }

            MyInvoke mi = new MyInvoke(UpdateForm2);
            this.BeginInvoke(mi, new Object[] { "", "" });
        }
        #endregion

        #region "工具栏功能按钮"
        #region "新建"
        private void tsb_New_Click(object sender, EventArgs e)
        {
            string path = System.Environment.CurrentDirectory + @"\Text";//@是取消转义字符的意思
            OpenFileDialog fd = new OpenFileDialog();
            fd.Title = "请选择模板";
            //fd.InitialDirectory = path;
            //fd.RestoreDirectory = true;
            fd.Filter = "文本文件(*.txt)|*.txt";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string strAbsolutePath = System.IO.Path.GetFullPath(fd.FileName);//绝对路径
                string strFileName = System.IO.Path.GetFileNameWithoutExtension(fd.FileName);//文件名没有扩展名
                //因为文件比较大，所有使用StreamReader的效率要比使用File.ReadLines高
                Global.g_Model.init();
                Global.g_Model.ModelName = strFileName;//模板文件名称
                formInfo.LogMessage(string.Format("新建工程，选择了模板：{0}", strAbsolutePath));//日志记录
                short iSet = 0;
                using (StreamReader sr = new StreamReader(strAbsolutePath, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        string readStr = sr.ReadLine();
                        readStr.Trim();
                        if (readStr == "") { iSet = 0; continue; }
                        if (readStr.Contains(Global.cst_Table_Para) == true)
                        {
                            iSet = CST_SET.Index_Para;
                        }
                        else if (readStr.Contains(Global.cst_Table_Setting) == true)
                        {
                            iSet = CST_SET.Index_Setting;
                        }
                        else if (readStr.Contains(Global.cst_Table_YC) == true)
                        {
                            iSet = CST_SET.Index_YC;
                        }
                        else if (readStr.Contains(Global.cst_Table_SYX) == true)
                        {
                            iSet = CST_SET.Index_SYX;
                        }
                        else if (readStr.Contains(Global.cst_Table_DYX) == true)
                        {
                            iSet = CST_SET.Index_DYX;
                        }
                        else if (readStr.Contains(Global.cst_Table_YK) == true)
                        {
                            iSet = CST_SET.Index_YK;
                        }
                        else if (readStr.Contains(Global.cst_Table_Meter) == true)
                        {
                            iSet = CST_SET.Index_Meter;
                        }
                        else if (readStr.Contains(Global.cst_Table_Port) == true)
                        {
                            iSet = CST_SET.Index_Port;
                        }
                        else if (readStr.Contains(Global.cst_Table_GYX) == true)
                        {
                            iSet = CST_SET.Index_GYX;
                        }
                        if (iSet == CST_SET.Index_Para) //参数表
                        {
                            if (readStr.Contains("//") == false && readStr.Contains(Global.cst_Table_Para) == false)
                            {
                                string[] sa = readStr.Split(Global.g_SplitChar, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                                CTablePara obj = get_Para(sa);
                                Global.g_Model.lst_Table_Para.Add(obj.Id, obj);
                            }
                        }
                        else if (iSet == CST_SET.Index_Setting) //定值表
                        {
                            if (readStr.Contains("//") == false && readStr.Contains(Global.cst_Table_Setting) == false)
                            {
                                string[] sa = readStr.Split(Global.g_SplitChar, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                                CTableSetting obj = get_Setting(sa);
                                Global.g_Model.lst_Table_Setting.Add(obj.Id, obj);
                            }
                        }
                        else if (iSet == CST_SET.Index_YC) //遥测表
                        {
                            if (readStr.Contains("//") == false && readStr.Contains(Global.cst_Table_YC) == false)
                            {
                                string[] sa = readStr.Split(Global.g_SplitChar, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                                CTableYC obj = get_YC(sa);
                                Global.g_Model.lst_Table_YC.Add(obj.Id, obj);
                            }
                        }
                        else if (iSet == CST_SET.Index_SYX) //单点遥信表
                        {
                            if (readStr.Contains("//") == false && readStr.Contains(Global.cst_Table_SYX) == false)
                            {
                                string[] sa = readStr.Split(Global.g_SplitChar, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                                CTableSYX obj = get_SYX(sa);
                                Global.g_Model.lst_Table_SYX.Add(obj.Id, obj);
                            }
                        }
                        else if (iSet == CST_SET.Index_DYX) //双点遥信表
                        {
                            if (readStr.Contains("//") == false && readStr.Contains(Global.cst_Table_DYX) == false)
                            {
                                string[] sa = readStr.Split(Global.g_SplitChar, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                                CTableDYX obj = get_DYX(sa);
                                Global.g_Model.lst_Table_DYX.Add(obj.Id, obj);
                            }
                        }
                        else if (iSet == CST_SET.Index_YK) //遥控表
                        {
                            if (readStr.Contains("//") == false && readStr.Contains(Global.cst_Table_YK) == false)
                            {
                                string[] sa = readStr.Split(Global.g_SplitChar, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                                CTableYK obj = get_YK(sa);
                                Global.g_Model.lst_Table_YK.Add(obj.Id, obj);
                            }
                        }
                        else if (iSet == CST_SET.Index_Meter) //计量值表
                        {
                            if (readStr.Contains("//") == false && readStr.Contains(Global.cst_Table_Meter) == false)
                            {
                                string[] sa = readStr.Split(Global.g_SplitChar, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                                CTableMeter obj = get_Meter(sa);
                                Global.g_Model.lst_Table_Meter.Add(obj.Id, obj);
                            }
                        }
                        else if (iSet == CST_SET.Index_Port) //端口表
                        {
                            if (readStr.Contains("//") == false && readStr.Contains(Global.cst_Table_Port) == false)
                            {
                                string[] sa = readStr.Split(Global.g_SplitChar, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                                CTablePort obj = get_Port(sa);
                                Global.g_Model.lst_Table_Port.Add(obj.Id, obj);
                            }
                        }
                        else if (iSet == CST_SET.Index_GYX) //组合遥信表
                        {
                            readStr = readStr.Trim();
                            if (readStr.Contains("//") == false && readStr.Contains(Global.cst_Table_GYX) == false && readStr != "")
                            {
                                string[] sa = readStr.Split(Global.g_SplitChar, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                                CTableGYX obj = get_GYX(sa);
                                Global.g_Model.lst_Table_GYX.Add(obj.Id, obj);
                            }
                        }
                    }
                    //----
                    init_Set_Para();
                    init_Set_Setting();
                    init_Set_Port();
                    //----
                    tvNavi.ExpandAll();
                    tvNavi.Nodes[0].EnsureVisible();
                    tn_set_Para.Collapse();
                    tn_set_Setting.Collapse();
                    //END
                }
                fd.InitialDirectory = fd.FileName.Substring(0, fd.FileName.LastIndexOf("\\") + 1);
            }
        }

        //新建时，初始化 参数集
        private void init_Set_Para()
        {
            List<String> ls = new List<String>();
            foreach (var t in Global.g_Model.lst_Table_Para.Values)
            {
                if (!ls.Contains(t.GroupName))
                {
                    ls.Add(t.GroupName);
                }
            }
            foreach (var t in ls)
            {
                TreeNode tn = new TreeNode(t);
                tn_set_Para.Nodes.Add(tn);
            }
        }
        //新建时，初始化 定值集
        private void init_Set_Setting()
        {
            List<String> ls = new List<String>();
            foreach (var t in Global.g_Model.lst_Table_Setting.Values)
            {
                if (!ls.Contains(t.GroupName))
                {
                    ls.Add(t.GroupName);
                }
            }
            foreach (var t in ls)
            {
                TreeNode tn = new TreeNode(t);
                tn_set_Setting.Nodes.Add(tn);
            }
        }
        //新建时，初始化 端口集
        private void init_Set_Port()
        {
            foreach (var t in Global.g_Model.lst_Table_Port.Values)
            {
                TreeNode tn = new TreeNode(t.PortName);
                tn_set_Port.Nodes.Add(tn);
            }
        }
        #endregion

        #region "打开"
        private void tsb_Open_Click(object sender, EventArgs e)
        {
            if (tn_set_Para.Nodes.Count > 0)
            {
                DialogResult res = MessageBox.Show("打开新工程，当前工程将被清空，您确认要这样操作吗？或，您可以点取消，先保存当前工程！",
                                                   "确认操作", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (res == System.Windows.Forms.DialogResult.Cancel) { return; }
                init();//初始化，清空当前工程
            }
            //-------------------------------------------------------------
            string path = System.Environment.CurrentDirectory + @"\Binary";
            OpenFileDialog fd = new OpenFileDialog();
            fd.Title = "打开工程文件";
            //fd.InitialDirectory = path;
            fd.Filter = "工程文件(*.prj)|*.prj";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                read_prj(fd.FileName);
                fd.InitialDirectory = fd.FileName.Substring(0, fd.FileName.LastIndexOf("\\") + 1);
                formInfo.LogMessage(string.Format("成功打开工程文件：{0}", fd.FileName));//日志记录
            }
        }

        //读prj文件
        void read_prj(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            //日志记录
            formInfo.LogMessage(string.Format("工程文件[{0}]大小：{1}字节", filename, fs.Length));
            try
            {
                //while (true)
                //{
                read_prj_ParaSet(br);
                read_prj_ModelSet(br);
                read_prj_DeviceSet(br);
                read_prj_RTDBSet(br);
                read_prj_FWTSet(br);
                read_prj_PortSet(br);
                //-----------------------
                foreach (var t in Global.g_list_FWT.Values)
                {
                    addNode_FWT(t);
                }
                foreach (var t in Global.g_list_DeviceTable.Values)
                {
                    addNode_DeviceTable(t);
                }
                //}
            }
            catch (Exception)
            {
                MessageBox.Show(string.Format("读取[{0}]结束！", filename), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            br.Close();
            fs.Close();
        }

        //1、读[参数集](包含定值集)
        void read_prj_ParaSet(BinaryReader br)
        {
            try
            {
                string s = "";
                //----
                Global.g_Model.init();
                //----
                s = readChineseString(br);
                Global.g_Model.ModelName = s;//模板名称
                //----1、参数
                UInt32 iRecordCount = br.ReadUInt32();
                for (int m = 0; m < iRecordCount; m++)
                {
                    s = readChineseString(br);
                    string[] sa = s.Split(splitChar);
                    CTablePara obj = get_Para(sa);
                    Global.g_Model.lst_Table_Para.Add(obj.Id, obj);
                }
                //----2、定值
                iRecordCount = br.ReadUInt32();
                for (int m = 0; m < iRecordCount; m++)
                {
                    s = readChineseString(br);
                    string[] sa = s.Split(splitChar);
                    CTableSetting obj = get_Setting(sa);
                    Global.g_Model.lst_Table_Setting.Add(obj.Id, obj);
                }
                //----3、遥测
                iRecordCount = br.ReadUInt32();
                for (int m = 0; m < iRecordCount; m++)
                {
                    s = readChineseString(br);
                    string[] sa = s.Split(splitChar);
                    CTableYC obj = get_YC(sa);
                    Global.g_Model.lst_Table_YC.Add(obj.Id, obj);
                }
                //----4、单点遥信
                iRecordCount = br.ReadUInt32();
                for (int m = 0; m < iRecordCount; m++)
                {
                    s = readChineseString(br);
                    string[] sa = s.Split(splitChar);
                    CTableSYX obj = get_SYX(sa);
                    Global.g_Model.lst_Table_SYX.Add(obj.Id, obj);
                }
                //----5、双点遥信
                iRecordCount = br.ReadUInt32();
                for (int m = 0; m < iRecordCount; m++)
                {
                    s = readChineseString(br);
                    string[] sa = s.Split(splitChar);
                    CTableDYX obj = get_DYX(sa);
                    Global.g_Model.lst_Table_DYX.Add(obj.Id, obj);
                }
                //----6、遥控
                iRecordCount = br.ReadUInt32();
                for (int m = 0; m < iRecordCount; m++)
                {
                    s = readChineseString(br);
                    string[] sa = s.Split(splitChar);
                    CTableYK obj = get_YK(sa);
                    Global.g_Model.lst_Table_YK.Add(obj.Id, obj);
                }
                //----7、计量
                iRecordCount = br.ReadUInt32();
                for (int m = 0; m < iRecordCount; m++)
                {
                    s = readChineseString(br);
                    string[] sa = s.Split(splitChar);
                    CTableMeter obj = get_Meter(sa);
                    Global.g_Model.lst_Table_Meter.Add(obj.Id, obj);
                }
                //----8、端口
                iRecordCount = br.ReadUInt32();
                for (int m = 0; m < iRecordCount; m++)
                {
                    s = readChineseString(br);
                    string[] sa = s.Split(splitChar);
                    CTablePort obj = get_Port(sa);
                    Global.g_Model.lst_Table_Port.Add(obj.Id, obj);
                }
                //-------------刷新TreeView--------------
                init_Set_Para();
                init_Set_Setting();
                init_Set_Port();
                //END
            }
            catch (Exception ex)
            {
                formInfo.LogError(string.Format("读[参数集]异常: {0}", ex.ToString()));
            }
        }

        //2、读[模板集]
        void read_prj_ModelSet(BinaryReader br)
        {
            //1、4字节模板数目
            //2、遍历模板List：
            //模板名称
            //[参数数目，参数]
            //[遥测数目，遥测]
            //[单点遥信数目，单点遥信]
            //[双点遥信数目，双点遥信]
            //[遥控数目，遥控]
            //[计量数目，计量]
            //[端口数目，端口]
            try
            {
                //----
                Global.g_list_Model.Clear();
                CModel.Accu = 1;
                //----
                string s = "";
                string[] sa;
                UInt32 iRecordCount = 0;
                //---------------------------------------------
                UInt32 iModelCount = br.ReadUInt32();//模板数目
                if (iModelCount == 0) { return; };
                for (int m = 0; m < iModelCount; m++)
                {
                    CModel mdl = new CModel();
                    s = readChineseString(br);
                    mdl.ModelName = s;//模板名称
                    //-------------------------------------------1、参数
                    iRecordCount = br.ReadUInt32();
                    for (int i = 0; i < iRecordCount; i++)
                    {
                        s = readChineseString(br);
                        sa = s.Split(splitChar);
                        CTablePara obj = get_Para(sa);
                        mdl.lst_Table_Para.Add(obj.Id, obj);
                    }
                    //-------------------------------------------2、定值
                    iRecordCount = br.ReadUInt32();
                    for (int i = 0; i < iRecordCount; i++)
                    {
                        s = readChineseString(br);
                        sa = s.Split(splitChar);
                        CTableSetting obj = get_Setting(sa);
                        mdl.lst_Table_Setting.Add(obj.Id, obj);
                    }
                    //-------------------------------------------3、遥测
                    iRecordCount = br.ReadUInt32();
                    for (int i = 0; i < iRecordCount; i++)
                    {
                        s = readChineseString(br);
                        sa = s.Split(splitChar);
                        CTableYC obj = get_YC(sa);
                        mdl.lst_Table_YC.Add(obj.Id, obj);
                    }
                    //-------------------------------------------4、单点遥信
                    iRecordCount = br.ReadUInt32();
                    for (int i = 0; i < iRecordCount; i++)
                    {
                        s = readChineseString(br);
                        sa = s.Split(splitChar);
                        CTableSYX obj = get_SYX(sa);
                        mdl.lst_Table_SYX.Add(obj.Id, obj);
                    }
                    //-------------------------------------------5、双点遥信
                    iRecordCount = br.ReadUInt32();
                    for (int i = 0; i < iRecordCount; i++)
                    {
                        s = readChineseString(br);
                        sa = s.Split(splitChar);
                        CTableDYX obj = get_DYX(sa);
                        mdl.lst_Table_DYX.Add(obj.Id, obj);
                    }
                    //-------------------------------------------6、遥控
                    iRecordCount = br.ReadUInt32();
                    for (int i = 0; i < iRecordCount; i++)
                    {
                        s = readChineseString(br);
                        sa = s.Split(splitChar);
                        CTableYK obj = get_YK(sa);
                        mdl.lst_Table_YK.Add(obj.Id, obj);
                    }
                    //-------------------------------------------7、计量
                    iRecordCount = br.ReadUInt32();
                    for (int i = 0; i < iRecordCount; i++)
                    {
                        s = readChineseString(br);
                        sa = s.Split(splitChar);
                        CTableMeter obj = get_Meter(sa);
                        mdl.lst_Table_Meter.Add(obj.Id, obj);
                    }
                    //-------------------------------------------8、端口
                    iRecordCount = br.ReadUInt32();//数目
                    for (int i = 0; i < iRecordCount; i++)
                    {
                        s = readChineseString(br);
                        sa = s.Split(splitChar);
                        CTablePort obj = get_Port(sa);
                        mdl.lst_Table_Port.Add(obj.Id, obj);
                    }
                    /////////////////////////////////////////////
                    mdl.Id = CModel.Accu;
                    CModel.Accu += 1;
                    Global.g_list_Model.Add(mdl.Id, mdl);
                    Global.sorted_list_Model();
                    //----
                    TreeNode tn = new TreeNode(mdl.ModelName);
                    tn_set_Model.Nodes.Add(tn);
                    //END
                }
            }
            catch (Exception ex)
            {
                formInfo.LogError(string.Format("读[模板集]异常: {0}", ex.ToString()));
            }
        }

        //3、读[设备表集]
        void read_prj_DeviceSet(BinaryReader br)
        {
            //1、4字节设备数目
            //2、遍历设备List：
            try
            {
                //----
                Global.g_list_DeviceTable.Clear();
                CDevice.Accu = 1;
                CDeviceTable.Accu = 1;
                //----
                string s = "";
                string[] sa;
                //---------------------------------------------
                UInt32 iDeviceTableCount = br.ReadUInt32();//设备表数目
                if (iDeviceTableCount == 0) { return; };
                for (int m = 0; m < iDeviceTableCount; m++)
                {
                    CDeviceTable DT = new CDeviceTable();
                    //----
                    DT.Id = CDeviceTable.Accu;
                    CDeviceTable.Accu += 1;
                    s = readChineseString(br);
                    DT.DeviceTableName = s;//设备表 名称（设备表_1）
                    //----
                    UInt32 iDeviceCount = br.ReadUInt32();//设备数目
                    for (int j = 0; j < iDeviceCount; j++)
                    {
                        //string DeviceName = "";
                        //for (int k = 0; k < iSize; k++) { cName[k] = br.ReadChar(); }
                        //DeviceName = charArray2String(cName);//设备名称(设备_1)
                        //--
                        s = readChineseString(br);
                        sa = s.Split(splitChar);
                        CDevice obj = get_Device(sa);//设备
                        DT.lst_Device.Add(obj.Id, obj);
                    }
                    Global.g_list_DeviceTable.Add(DT.Id, DT);
                }
                Global.sorted_list_DeviceTable();
            }
            catch (Exception ex)
            {
                formInfo.LogError(string.Format("读[设备表集]异常: {0}", ex.ToString()));
            }
        }

        //4、读[实时数据集]
        void read_prj_RTDBSet(BinaryReader br)
        {
            ;//不需要
        }

        //5、读[转发表集]
        void read_prj_FWTSet(BinaryReader br)
        {
            //1、4字节转发表数目
            //2、遍历设备List
            try
            {
                //----
                Global.g_list_FWT.Clear();
                CFWT.Accu = 1;
                //----
                string s = "";
                string[] sa;
                UInt32 iRecordCount = 0;
                //---------------------------------------------
                UInt32 iFWTCount = br.ReadUInt32();//转发表数目
                if (iFWTCount == 0) { return; };
                for (int m = 0; m < iFWTCount; m++)
                {
                    s = readChineseString(br);
                    sa = s.Split(splitChar);
                    CFWT obj = get_FWT(sa);
                    obj.Id = m + 1;
                    //一、左侧List
                    //--------------------------------1、遥测
                    iRecordCount = br.ReadUInt32();
                    for (int i = 0; i < iRecordCount; i++)
                    {
                        s = readChineseString(br);
                        sa = s.Split(splitChar);
                        CTableYC ct = get_YC_2(sa);
                        obj.lst_Table_YC_1.Add(ct.SN, ct);
                    }
                    //--------------------------------2、单点遥信
                    iRecordCount = br.ReadUInt32();
                    for (int i = 0; i < iRecordCount; i++)
                    {
                        s = readChineseString(br);
                        sa = s.Split(splitChar);
                        CTableSYX ct = get_SYX_2(sa);
                        obj.lst_Table_SYX_1.Add(ct.SN, ct);
                    }
                    //--------------------------------3、双点遥信
                    iRecordCount = br.ReadUInt32();
                    for (int i = 0; i < iRecordCount; i++)
                    {
                        s = readChineseString(br);
                        sa = s.Split(splitChar);
                        CTableDYX ct = get_DYX_2(sa);
                        obj.lst_Table_DYX_1.Add(ct.SN, ct);
                    }
                    //--------------------------------4、遥控
                    iRecordCount = br.ReadUInt32();
                    for (int i = 0; i < iRecordCount; i++)
                    {
                        s = readChineseString(br);
                        sa = s.Split(splitChar);
                        CTableYK ct = get_YK_2(sa);
                        obj.lst_Table_YK_1.Add(ct.SN, ct);
                    }
                    //--------------------------------5、计量
                    iRecordCount = br.ReadUInt32();
                    for (int i = 0; i < iRecordCount; i++)
                    {
                        s = readChineseString(br);
                        sa = s.Split(splitChar);
                        CTableMeter ct = get_Meter_2(sa);
                        obj.lst_Table_Meter_1.Add(ct.SN, ct);
                    }
                    //二、右侧List
                    //--------------------------------1、遥测
                    iRecordCount = br.ReadUInt32();
                    for (int i = 0; i < iRecordCount; i++)
                    {
                        s = readChineseString(br);
                        sa = s.Split(splitChar);
                        CTableYC ct = get_YC_2(sa);
                        //obj.lst_Table_YC_2.Add(ct.SN, ct);
                        obj.lst_Table_YC_2.Add(i, ct);
                    }
                    //--------------------------------2、单点遥信
                    iRecordCount = br.ReadUInt32();
                    for (int i = 0; i < iRecordCount; i++)
                    {
                        s = readChineseString(br);
                        sa = s.Split(splitChar);
                        CTableSYX ct = get_SYX_2(sa);
                        obj.lst_Table_SYX_2.Add(i, ct);
                        //obj.lst_Table_SYX_2.Add(ct.SN, ct);//bug修正，jifeng，201709112008
                    }
                    //--------------------------------3、双点遥信
                    iRecordCount = br.ReadUInt32();
                    for (int i = 0; i < iRecordCount; i++)
                    {
                        s = readChineseString(br);
                        sa = s.Split(splitChar);
                        CTableDYX ct = get_DYX_2(sa);
                        obj.lst_Table_DYX_2.Add(i, ct);
                        //obj.lst_Table_DYX_2.Add(ct.SN, ct);//bug修正，jifeng，201709112008
                    }
                    //--------------------------------4、遥控
                    iRecordCount = br.ReadUInt32();
                    for (int i = 0; i < iRecordCount; i++)
                    {
                        s = readChineseString(br);
                        sa = s.Split(splitChar);
                        CTableYK ct = get_YK_2(sa);
                        //obj.lst_Table_YK_2.Add(ct.SN, ct);//bug修正，jifeng，201709112008
                        obj.lst_Table_YK_2.Add(i, ct);
                    }
                    //--------------------------------5、计量
                    iRecordCount = br.ReadUInt32();
                    for (int i = 0; i < iRecordCount; i++)
                    {
                        s = readChineseString(br);
                        sa = s.Split(splitChar);
                        CTableMeter ct = get_Meter_2(sa);
                        //obj.lst_Table_Meter_2.Add(ct.SN, ct);
                        obj.lst_Table_Meter_2.Add(i, ct);
                    }
                    //三、遥测最大值 和 系数
                    obj.I_MAX = Convert.ToInt32(br.ReadUInt32());
                    obj.V_MAX = Convert.ToInt32(br.ReadUInt32());
                    obj.DC_MAX = Convert.ToInt32(br.ReadUInt32());
                    obj.P_MAX = Convert.ToInt32(br.ReadUInt32());
                    obj.FR_MAX = Convert.ToInt32(br.ReadUInt32());
                    obj.COS_MAX = Convert.ToInt32(br.ReadUInt32());
                    //--
                    obj.I_COE = Convert.ToInt32(br.ReadUInt32());
                    obj.V_COE = Convert.ToInt32(br.ReadUInt32());
                    obj.DC_COE = Convert.ToInt32(br.ReadUInt32());
                    obj.P_COE = Convert.ToInt32(br.ReadUInt32());
                    obj.FR_COE = Convert.ToInt32(br.ReadUInt32());
                    obj.COS_COE = Convert.ToInt32(br.ReadUInt32());
                    //--------------------------------
                    Global.g_list_FWT.Add(obj.Id, obj);
                    //addNode_FWT(obj);//移到最后
                }
                Global.sorted_list_FWT();
                //END
            }
            catch (Exception ex)
            {
                formInfo.LogError(string.Format("读[转发表集]异常: {0}", ex.ToString()));
            }
        }

        //6、读[端口集]
        void read_prj_PortSet(BinaryReader br)
        {
            try
            {
                foreach (var t in Global.g_Model.lst_Table_Port.Values)
                {
                    //-----------------------------------------
                    t.cfg_Port.read_bin(br);
                    //-----------------------------------------
                    if (t.cfg_Port.tPort.u8PhyAttr == 0x01)
                    {
                        t.PhysicalAttribute = "串口";
                    }
                    else if (t.cfg_Port.tPort.u8PhyAttr == 0x02)
                    {
                        t.PhysicalAttribute = "网口";
                    }
                    else if (t.cfg_Port.tPort.u8PhyAttr == 0x03)
                    {
                        t.PhysicalAttribute = "虚拟口";
                    }
                    //----
                    if (t.cfg_Port.tPort.u8LogicAttr < 3 || t.cfg_Port.tPort.u8LogicAttr > 4)
                    {
                        MessageBox.Show(string.Format("逻辑属性数值不正确，应该是3或4，这里读到了{0}", t.cfg_Port.tPort.u8LogicAttr));
                    }
                    t.LogicAttribute = (t.cfg_Port.tPort.u8LogicAttr == 0x03 ? "对上" : "对下");
                    t.ProtocolName = Global.Protocol_Support[((int)(t.cfg_Port.eProtocol))];
                    t.ProtocolInstanceNum = t.cfg_Port.u8ProtocolInstNo;
                    t.Addr = t.cfg_Port.tPort.u16PortAddr;
                    t.Enabled = t.cfg_Port.bUsed;
                    //----
                    t.FWTName = t.cfg_Port.getFWTName();
                    t.DeviceTableName = t.cfg_Port.getDeviceTableName();
                }
                //END
            }
            catch (Exception ex)
            {
                formInfo.LogError(string.Format("读[端口集]异常: {0}", ex.ToString()));
            }
        }
        #endregion

        #region "保存"
        private void tsb_Save_Click(object sender, EventArgs e)
        {
            if (true == Check_Fwt())
            {
                formInfo.Visible = true;
                MessageBox.Show(string.Format("转发表集有误，请检查确认！"),
                    "告警", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string path = System.Environment.CurrentDirectory + @"\Binary";
            SaveFileDialog fd = new SaveFileDialog();
            fd.Title = "保存工程文件";
            //fd.InitialDirectory = path;
            DateTime dt = DateTime.Now;
            fd.FileName = string.Format("cfg_{0:D4}{1:D2}{2:D2}", dt.Year, dt.Month, dt.Day);
            fd.Filter = "工程文件(*.prj)|*.prj";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                write_prj(fd.FileName);
                fd.InitialDirectory = fd.FileName.Substring(0, fd.FileName.LastIndexOf("\\") + 1);
                //----
                string strpath = "";
                strpath = fd.FileName.Substring(0, fd.FileName.Length - 16);
                SaveCfg(strpath);
            }
        }

        //写prj文件
        void write_prj(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            //---------
            //1、参数集
            write_prj_ParaSet(bw);
            //2、模板集
            write_prj_ModelSet(bw);
            //3、设备集
            write_prj_DeviceSet(bw);
            //4、实时数据集
            write_prj_RTDBSet(bw);
            //5、转发表集
            write_prj_FWTSet(bw);
            //6、端口集
            write_prj_PortSet(bw);
            //----
            //日志记录
            formInfo.LogMessage(string.Format("保存的工程文件[{0}]大小：{1}字节", filename, fs.Length));
            //---------
            bw.Close();
            fs.Close();
            MessageBox.Show(string.Format("保存[{0}]成功！", filename), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //1、写[参数集](包含定值集)
        void write_prj_ParaSet(BinaryWriter bw)
        {
            try
            {
                //----
                writeChineseString(Global.g_Model.ModelName, bw);
                //----1、参数
                bw.Write(Convert.ToUInt32(Global.g_Model.lst_Table_Para.Count));
                foreach (var t in Global.g_Model.lst_Table_Para.Values)
                {
                    sName = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10}",
                        t.Id, t.GroupName, t.ItemName, t.DataType, t.Unit,
                        t.ValueMax, t.ValueMin, t.ValueDefault, t.Ratio, t.Addr, t.BytePos);
                    writeChineseString(sName, bw);
                }
                //----2、定值
                bw.Write(Convert.ToUInt32(Global.g_Model.lst_Table_Setting.Count));
                foreach (var t in Global.g_Model.lst_Table_Setting.Values)
                {
                    sName = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10}",
                        t.Id, t.GroupName, t.ItemName, t.DataType, t.Unit,
                        t.ValueMax, t.ValueMin, t.ValueDefault, t.Ratio, t.Addr, t.BytePos);
                    writeChineseString(sName, bw);
                }
                //----3、遥测
                bw.Write(Convert.ToUInt32(Global.g_Model.lst_Table_YC.Count));
                foreach (var t in Global.g_Model.lst_Table_YC.Values)
                {
                    sName = string.Format("{0};{1};{2};{3};{4};{5};{6};{7}",
                        t.Id, t.GroupName, t.ItemName, t.DataType, t.Unit,
                        t.Ratio, t.iGroup, t.Addr);
                    writeChineseString(sName, bw);
                }
                //----4、单点遥信
                bw.Write(Convert.ToUInt32(Global.g_Model.lst_Table_SYX.Count));
                foreach (var t in Global.g_Model.lst_Table_SYX.Values)
                {
                    sName = string.Format("{0};{1};{2};{3};{4}",
                        t.Id, t.GroupName, t.ItemName, t.DataType, t.Addr);
                    writeChineseString(sName, bw);
                }
                //----5、双点遥信
                bw.Write(Convert.ToUInt32(Global.g_Model.lst_Table_DYX.Count));
                foreach (var t in Global.g_Model.lst_Table_DYX.Values)
                {
                    sName = string.Format("{0};{1};{2};{3};{4}",
                        t.Id, t.GroupName, t.ItemName, t.DataType, t.Addr);
                    writeChineseString(sName, bw);
                }
                //----6、遥控
                bw.Write(Convert.ToUInt32(Global.g_Model.lst_Table_YK.Count));
                foreach (var t in Global.g_Model.lst_Table_YK.Values)
                {
                    sName = string.Format("{0};{1};{2};{3};{4}",
                        t.Id, t.GroupName, t.ItemName, t.DataType, t.Addr);
                    writeChineseString(sName, bw);
                }
                //----7、计量
                bw.Write(Convert.ToUInt32(Global.g_Model.lst_Table_Meter.Count));
                foreach (var t in Global.g_Model.lst_Table_Meter.Values)
                {
                    sName = string.Format("{0};{1};{2};{3};{4};{5}",
                        t.Id, t.GroupName, t.ItemName, t.DataType, t.Unit,
                        t.Addr);
                    writeChineseString(sName, bw);
                }
                //----8、端口
                bw.Write(Convert.ToUInt32(Global.g_Model.lst_Table_Port.Count));
                foreach (var t in Global.g_Model.lst_Table_Port.Values)
                {
                    sName = string.Format("{0};{1};{2}",
                        t.Id, t.PortName, t.PhysicalAttribute);
                    writeChineseString(sName, bw);
                }
            }
            catch (Exception ex)
            {
                formInfo.LogError(string.Format("写[参数集]异常：{0}", ex.Message.ToString()));
            }
        }

        //2、写[模板集]
        void write_prj_ModelSet(BinaryWriter bw)
        {
            //1、4字节模板数目
            //2、遍历模板List：
            //模板名称
            //[参数数目，参数]
            //[定值数目，定值]
            //[遥测数目，遥测]
            //[单点遥信数目，单点遥信]
            //[双点遥信数目，双点遥信]
            //[遥控数目，遥控]
            //[计量数目，计量]
            //[端口数目，端口]
            try
            {
                bw.Write(Convert.ToUInt32(Global.g_list_Model.Count));//模板数量
                foreach (var m in Global.g_list_Model)
                {
                    writeChineseString(m.Value.ModelName, bw);//模板名称
                    //----1、参数
                    bw.Write(Convert.ToUInt32(m.Value.lst_Table_Para.Count));
                    foreach (var t in m.Value.lst_Table_Para.Values)
                    {
                        sName = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10}",
                            t.Id, t.GroupName, t.ItemName, t.DataType, t.Unit,
                            t.ValueMax, t.ValueMin, t.ValueDefault, t.Ratio, t.Addr, t.BytePos);
                        writeChineseString(sName, bw);
                    }
                    //----2、定值
                    bw.Write(Convert.ToUInt32(m.Value.lst_Table_Setting.Count));
                    foreach (var t in m.Value.lst_Table_Setting.Values)
                    {
                        sName = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10}",
                            t.Id, t.GroupName, t.ItemName, t.DataType, t.Unit,
                            t.ValueMax, t.ValueMin, t.ValueDefault, t.Ratio, t.Addr, t.BytePos);
                        writeChineseString(sName, bw);
                    }
                    //----3、遥测
                    bw.Write(Convert.ToUInt32(m.Value.lst_Table_YC.Count));
                    foreach (var t in m.Value.lst_Table_YC.Values)
                    {
                        sName = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9}",
                            t.Id, t.GroupName, t.ItemName, t.DataType, t.Unit,
                            t.Ratio, t.iGroup, t.Addr,
                            t.fYcCoe, t.fYcZone);//新增yccoe和yczone，jifeng，2018-9-11
                        writeChineseString(sName, bw);
                    }
                    //----4、单点遥信
                    bw.Write(Convert.ToUInt32(m.Value.lst_Table_SYX.Count));
                    foreach (var t in m.Value.lst_Table_SYX.Values)
                    {
                        sName = string.Format("{0};{1};{2};{3};{4}",
                            t.Id, t.GroupName, t.ItemName, t.DataType, t.Addr);
                        writeChineseString(sName, bw);
                    }
                    //----5、双点遥信
                    bw.Write(Convert.ToUInt32(m.Value.lst_Table_DYX.Count));
                    foreach (var t in m.Value.lst_Table_DYX.Values)
                    {
                        sName = string.Format("{0};{1};{2};{3};{4}",
                            t.Id, t.GroupName, t.ItemName, t.DataType, t.Addr);
                        writeChineseString(sName, bw);
                    }
                    //----6、遥控
                    bw.Write(Convert.ToUInt32(m.Value.lst_Table_YK.Count));
                    foreach (var t in m.Value.lst_Table_YK.Values)
                    {
                        sName = string.Format("{0};{1};{2};{3};{4}",
                            t.Id, t.GroupName, t.ItemName, t.DataType, t.Addr);
                        writeChineseString(sName, bw);
                    }
                    //----7、计量
                    bw.Write(Convert.ToUInt32(m.Value.lst_Table_Meter.Count));
                    foreach (var t in m.Value.lst_Table_Meter.Values)
                    {
                        sName = string.Format("{0};{1};{2};{3};{4};{5};",
                            t.Id, t.GroupName, t.ItemName, t.DataType, t.Unit,
                            t.Addr);
                        writeChineseString(sName, bw);
                    }
                    //----8、端口
                    bw.Write(Convert.ToUInt32(m.Value.lst_Table_Port.Count));
                    foreach (var t in m.Value.lst_Table_Port.Values)
                    {
                        sName = string.Format("{0};{1};{2}",
                            t.Id, t.PortName, t.PhysicalAttribute);
                        writeChineseString(sName, bw);
                    }
                }
            }
            catch (Exception ex)
            {
                formInfo.LogError(string.Format("写[模板集]异常：{0}", ex.Message.ToString()));
            }
        }

        //3、写[设备表集]
        void write_prj_DeviceSet(BinaryWriter bw)
        {
            //1、设备数目
            //2、遍历设备List
            try
            {
                bw.Write(Convert.ToUInt32(Global.g_list_DeviceTable.Count));//设备表数目
                foreach (var dt in Global.g_list_DeviceTable.Values)
                {
                    writeChineseString(dt.DeviceTableName, bw);//设备表名称
                    //----
                    bw.Write(dt.lst_Device.Count);//设备数目
                    foreach (var dev in dt.lst_Device.Values)
                    {
                        sName = string.Format("{0};{1};{2};{3}",
                                dev.Id, dev.DeviceName, dev.ModelName, dev.CommAddr);
                        writeChineseString(sName, bw);//设备
                    }
                }
            }
            catch (Exception ex)
            {
                formInfo.LogError(string.Format("写[设备表集]异常：{0}", ex.Message.ToString()));
            }
            //END
        }

        //4、写[实时数据集]
        void write_prj_RTDBSet(BinaryWriter bw)
        {
            ;//不需要
        }

        //5、写[转发表集]
        void write_prj_FWTSet(BinaryWriter bw)
        {
            //1、转发表数目
            //2、遍历转发表List:
            //(1)、基本参数
            //(2)、转发表中左侧List：遥测、单点遥信、双点遥信、遥控、计量
            //(3)、转发表中右侧List：遥测、单点遥信、双点遥信、遥控、计量
            try
            {
                UInt32 iRecordCount = 0;
                bw.Write(Convert.ToUInt32(Global.g_list_FWT.Count));
                foreach (var fwt in Global.g_list_FWT.Values)
                {
                    //---------------------------------------------
                    sName = string.Format("{0};{1}", fwt.Id, fwt.FWTName);
                    writeChineseString(sName, bw);
                    //---------------------------------------------
                    //一、左侧List
                    //1、遥测
                    iRecordCount = Convert.ToUInt32(fwt.lst_Table_YC_1.Count);
                    bw.Write(iRecordCount);
                    foreach (var t in fwt.lst_Table_YC_1.Values)
                    {
                        sName = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14}",
                        t.Id, t.GroupName, t.ItemName, t.DataType, t.Unit,
                        t.Ratio, t.iGroup, t.Addr,
                        t.SN, t.DeviceName, t.ModelName, t.Group, t.FlagDelete, //新增5个
                        t.fYcZone, t.fYcCoe);//新增2个，南网需求，2017-10-27 15:13
                        writeChineseString(sName, bw);
                    }
                    //2、单点遥信
                    iRecordCount = Convert.ToUInt32(fwt.lst_Table_SYX_1.Count);
                    bw.Write(iRecordCount);
                    foreach (var t in fwt.lst_Table_SYX_1.Values)
                    {
                        sName = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9}",
                        t.Id, t.GroupName, t.ItemName, t.DataType, t.Addr,
                        t.SN, t.DeviceName, t.ModelName, t.Group, t.FlagDelete);//新增5个
                        writeChineseString(sName, bw);
                    }
                    //3、双点遥信
                    iRecordCount = Convert.ToUInt32(fwt.lst_Table_DYX_1.Count);
                    bw.Write(iRecordCount);
                    foreach (var t in fwt.lst_Table_DYX_1.Values)
                    {
                        sName = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9}",
                        t.Id, t.GroupName, t.ItemName, t.DataType, t.Addr,
                        t.SN, t.DeviceName, t.ModelName, t.Group, t.FlagDelete);//新增5个
                        writeChineseString(sName, bw);
                    }
                    //4、遥控
                    iRecordCount = Convert.ToUInt32(fwt.lst_Table_YK_1.Count);
                    bw.Write(iRecordCount);
                    foreach (var t in fwt.lst_Table_YK_1.Values)
                    {
                        sName = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9}",
                        t.Id, t.GroupName, t.ItemName, t.DataType, t.Addr,
                        t.SN, t.DeviceName, t.ModelName, t.Group, t.FlagDelete);//新增5个
                        writeChineseString(sName, bw);
                    }
                    //5、计量
                    iRecordCount = Convert.ToUInt32(fwt.lst_Table_Meter_1.Count);
                    bw.Write(iRecordCount);
                    foreach (var t in fwt.lst_Table_Meter_1.Values)
                    {
                        sName = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10}",
                        t.Id, t.GroupName, t.ItemName, t.DataType, t.Unit, t.Addr,
                        t.SN, t.DeviceName, t.ModelName, t.Group, t.FlagDelete);//新增5个
                        writeChineseString(sName, bw);
                    }
                    //---------------------------------------------
                    //二、右侧List
                    //1、遥测
                    iRecordCount = Convert.ToUInt32(fwt.lst_Table_YC_2.Count);
                    bw.Write(iRecordCount);
                    foreach (var t in fwt.lst_Table_YC_2.Values)
                    {
                        sName = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14}",
                        t.Id, t.GroupName, t.ItemName, t.DataType, t.Unit,
                        t.Ratio, t.iGroup, t.Addr,
                        t.SN, t.DeviceName, t.ModelName, t.Group, t.FlagDelete,
                        t.fYcZone, t.fYcCoe);//新增5个
                        writeChineseString(sName, bw);
                    }
                    //2、单点遥信
                    iRecordCount = Convert.ToUInt32(fwt.lst_Table_SYX_2.Count);
                    bw.Write(iRecordCount);
                    foreach (var t in fwt.lst_Table_SYX_2.Values)
                    {
                        sName = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9}",
                        t.Id, t.GroupName, t.ItemName, t.DataType, t.Addr,
                        t.SN, t.DeviceName, t.ModelName, t.Group, t.FlagDelete);//新增5个
                        writeChineseString(sName, bw);
                    }
                    //3、双点遥信
                    iRecordCount = Convert.ToUInt32(fwt.lst_Table_DYX_2.Count);
                    bw.Write(iRecordCount);
                    foreach (var t in fwt.lst_Table_DYX_2.Values)
                    {
                        sName = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9}",
                        t.Id, t.GroupName, t.ItemName, t.DataType, t.Addr,
                        t.SN, t.DeviceName, t.ModelName, t.Group, t.FlagDelete);//新增5个
                        writeChineseString(sName, bw);
                    }
                    //4、遥控
                    iRecordCount = Convert.ToUInt32(fwt.lst_Table_YK_2.Count);
                    bw.Write(iRecordCount);
                    foreach (var t in fwt.lst_Table_YK_2.Values)
                    {
                        sName = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9}",
                        t.Id, t.GroupName, t.ItemName, t.DataType, t.Addr,
                        t.SN, t.DeviceName, t.ModelName, t.Group, t.FlagDelete);//新增5个
                        writeChineseString(sName, bw);
                    }
                    //5、计量
                    iRecordCount = Convert.ToUInt32(fwt.lst_Table_Meter_2.Count);
                    bw.Write(iRecordCount);
                    foreach (var t in fwt.lst_Table_Meter_2.Values)
                    {
                        sName = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10}",
                        t.Id, t.GroupName, t.ItemName, t.DataType, t.Unit, t.Addr,
                        t.SN, t.DeviceName, t.ModelName, t.Group, t.FlagDelete);//新增5个
                        writeChineseString(sName, bw);
                    }
                    //--------------------------------------------
                    //三、遥测最大值 和 系数, jifeng, 2017-6-26
                    bw.Write(Convert.ToUInt32(fwt.I_MAX));
                    bw.Write(Convert.ToUInt32(fwt.V_MAX));
                    bw.Write(Convert.ToUInt32(fwt.DC_MAX));
                    bw.Write(Convert.ToUInt32(fwt.P_MAX));
                    bw.Write(Convert.ToUInt32(fwt.FR_MAX));
                    bw.Write(Convert.ToUInt32(fwt.COS_MAX));
                    //--
                    bw.Write(Convert.ToUInt32(fwt.I_COE));
                    bw.Write(Convert.ToUInt32(fwt.V_COE));
                    bw.Write(Convert.ToUInt32(fwt.DC_COE));
                    bw.Write(Convert.ToUInt32(fwt.P_COE));
                    bw.Write(Convert.ToUInt32(fwt.FR_COE));
                    bw.Write(Convert.ToUInt32(fwt.COS_COE));
                    //----END
                }
            }
            catch (Exception ex)
            {
                formInfo.LogError(string.Format("写[转发表集]异常：{0}", ex.Message.ToString()));
            }
            //END
        }

        //6、写[端口集]
        void write_prj_PortSet(BinaryWriter bw)
        {
            try
            {
                foreach (var t in Global.g_Model.lst_Table_Port.Values)
                {
                    t.cfg_Port.assignValue_FWT(t.FWTName);
                    Global.sorted_list_DeviceTable();
                    t.cfg_Port.assignValue_Device(t.DeviceTableName);
                    t.cfg_Port.write_bin(bw);
                }
            }
            catch (Exception ex)
            {
                formInfo.LogError(string.Format("写[端口集]异常：{0}", ex.Message.ToString()));
            }
            //END
        }
        #endregion

        #region "显示/隐藏"
        private void tsb_NaviState_Click(object sender, EventArgs e)
        {
            string s = tsb_NaviState.Text;
            if (s == "隐藏")
            {
                tsb_NaviState.Text = "显示";
                tsb_NaviState.Image = Properties.Resources.showtv;
                splitContainer1.Panel1Collapsed = true;
            }
            else if (s == "显示")
            {
                tsb_NaviState.Text = "隐藏";
                tsb_NaviState.Image = Properties.Resources.hidetv;
                splitContainer1.Panel1Collapsed = false;
            }
        }
        #endregion

        #region "设置"
        //新增设置界面，添加对“本体”名称的修改。jifeng, 2017-11-21 周二 雾
        private void tsb_Setup_Click(object sender, EventArgs e)
        {
            Form_Setup dlg = new Form_Setup();
            dlg.Show(this);
        }
        #endregion

        #region "配置导入"
        #region "参数和定值"
        private void tsmi_Import_Para_Click(object sender, EventArgs e)
        {
            string path = System.Environment.CurrentDirectory + @"\Binary";
            OpenFileDialog fd = new OpenFileDialog();
            fd.Title = "导入参数配置";
            //fd.InitialDirectory = path;
            fd.Filter = "二进制文件(*.cfg)|*.cfg";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                read_Cfg_Para_Setting(fd.FileName);
                fd.InitialDirectory = fd.FileName.Substring(0, fd.FileName.LastIndexOf("\\") + 1);
            }
        }
        #endregion
        #region "定值(不用了)"
        private void tsmi_Import_Setting_Click(object sender, EventArgs e)
        {
            string path = System.Environment.CurrentDirectory + @"\Binary";
            OpenFileDialog fd = new OpenFileDialog();
            fd.Title = "导入定值配置";
            //fd.InitialDirectory = path;
            fd.Filter = "二进制文件(*.cfg)|*.cfg";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                read_Cfg_Setting(fd.FileName);
                fd.InitialDirectory = fd.FileName.Substring(0, fd.FileName.LastIndexOf("\\") + 1);
            }
        }
        #endregion
        #region "模板"
        private void tsmi_Import_Model_Click(object sender, EventArgs e)
        {
            string path = System.Environment.CurrentDirectory + @"\Binary";
            OpenFileDialog fd = new OpenFileDialog();
            fd.Title = "导入模板配置";
            //fd.InitialDirectory = path;
            fd.Filter = "二进制文件(*.cfg)|*.cfg";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                read_Cfg_Model(fd.FileName);
                fd.InitialDirectory = fd.FileName.Substring(0, fd.FileName.LastIndexOf("\\") + 1);
            }
        }

        void read_Cfg_Model(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            formInfo.LogMessage(string.Format("读取的模板文件[{0}]大小：{1}字节", filename, fs.Length));
            try
            {
                ;
            }
            catch (Exception)
            {
                MessageBox.Show(string.Format("读取[{0}]结束！", filename), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            br.Close();
            fs.Close();
            //-------------------------------------
            Global.g_list_FWT.Clear();
            foreach (var t in Global.g_list_FWT_Import)
            {

            }
            //--------------------------------------
        }
        #endregion
        #region "设备"
        private void tsmi_Import_Device_Click(object sender, EventArgs e)
        {
            string path = System.Environment.CurrentDirectory + @"\Binary";
            OpenFileDialog fd = new OpenFileDialog();
            fd.Title = "导入设备配置";
            //fd.InitialDirectory = path;
            fd.Filter = "二进制文件(*.cfg)|*.cfg";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                read_Cfg_Device(fd.FileName);
            }
        }

        void read_Cfg_Device(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            formInfo.LogMessage(string.Format("读取的设备文件[{0}]大小：{1}字节", filename, fs.Length));
            DevsCfgFile_t dev_cfg = new DevsCfgFile_t();
            try
            {
                dev_cfg.read_bin(br);
            }
            catch (Exception)
            {
                MessageBox.Show(string.Format("读取[{0}]结束！", filename), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            br.Close();
            fs.Close();
            //-------------------------------------
            dev_cfg.restoreFromCfg();
            tn_set_DeviceTable.Nodes.Clear();
            foreach (var dt in Global.g_list_DeviceTable.Values)
            {
                TreeNode tn = tn_set_DeviceTable.Nodes.Add(dt.DeviceTableName);
                foreach (var dev in dt.lst_Device.Values)
                {
                    tn.Nodes.Add(dev.DeviceName);
                }
            }
            //--------------------------------------
        }
        #endregion
        #region "转发表"
        private void tsmi_Import_FDB_Click(object sender, EventArgs e)
        {
            string path = System.Environment.CurrentDirectory + @"\Binary";
            OpenFileDialog fd = new OpenFileDialog();
            fd.Title = "导入转发表配置";
            //fd.InitialDirectory = path;
            fd.Filter = "二进制文件(*.cfg)|*.cfg";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                read_Cfg_FWT(fd.FileName);
                fd.InitialDirectory = fd.FileName.Substring(0, fd.FileName.LastIndexOf("\\") + 1);
            }
        }

        void read_Cfg_FWT(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            formInfo.LogMessage(string.Format("读取的转发表文件[{0}]大小：{1}字节", filename, fs.Length));
            try
            {
                Global.g_list_FWT_Import.Clear();
                UInt32 fwtCount = br.ReadUInt32();
                for (int m = 0; m < fwtCount; m++)
                {
                    CFWT obj = new CFWT();
                    obj.cfg_fwt.read_bin(br);
                    obj.FWTName = obj.cfg_fwt.sName;
                    obj.getYC(ref obj.cfg_fwt);
                    Global.g_list_FWT_Import.Add(Global.g_list_FWT_Import.Count, obj);
                }
            }
            catch (Exception)
            {
                MessageBox.Show(string.Format("读取[{0}]结束！", filename), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            br.Close();
            fs.Close();
            //-------------------------------------
            Global.g_list_FWT.Clear();
            CFWT.Accu = 0;
            tn_set_FWT.Nodes.Clear();
            foreach (var t in Global.g_list_FWT_Import)
            {
                CFWT obj = new CFWT();
                obj.Id = CFWT.Accu;
                CFWT.Accu += 1;
                obj.FWTName = t.Value.FWTName;
                obj.sourceClone();
                //----
                for (int k = 0; k < t.Value.cfg_fwt.u32YcNum; k++)
                {
                    int key = Convert.ToInt32(t.Value.cfg_fwt.u32YcIndex[k]);
                    if (obj.lst_Table_YC_1.ContainsKey(key) == true)
                    {
                        CTableYC clone = obj.lst_Table_YC_1[key].MyClone();
                        //obj.lst_Table_YC_2.Add(key, clone);
                        obj.lst_Table_YC_2.Add(obj.lst_Table_YC_2.Count, clone);
                        obj.lst_Table_YC_1[key].FlagDelete = true;
                    }
                }
                for (int k = 0; k < t.Value.cfg_fwt.u32SyxNum; k++)
                {
                    int key = Convert.ToInt32(t.Value.cfg_fwt.u32SyxIndex[k]);
                    if (obj.lst_Table_SYX_1.ContainsKey(key) == true)
                    {
                        CTableSYX clone = obj.lst_Table_SYX_1[key].MyClone();
                        //obj.lst_Table_SYX_2.Add(key, clone);
                        obj.lst_Table_SYX_2.Add(obj.lst_Table_SYX_2.Count, clone);
                        obj.lst_Table_SYX_1[key].FlagDelete = true;
                    }
                }
                for (int k = 0; k < t.Value.cfg_fwt.u32DyxNum; k++)
                {
                    int key = Convert.ToInt32(t.Value.cfg_fwt.u32DyxIndex[k]);
                    if (obj.lst_Table_DYX_1.ContainsKey(key) == true)
                    {
                        CTableDYX clone = obj.lst_Table_DYX_1[key].MyClone();
                        //obj.lst_Table_DYX_2.Add(key, clone);
                        obj.lst_Table_DYX_2.Add(obj.lst_Table_DYX_2.Count, clone);
                        obj.lst_Table_DYX_1[key].FlagDelete = true;
                    }
                }
                for (int k = 0; k < t.Value.cfg_fwt.u32YkNum; k++)
                {
                    int key = Convert.ToInt32(t.Value.cfg_fwt.u32YkIndex[k]);
                    if (obj.lst_Table_YK_1.ContainsKey(key) == true)
                    {
                        CTableYK clone = obj.lst_Table_YK_1[key].MyClone();
                        //obj.lst_Table_YK_2.Add(key, clone);
                        obj.lst_Table_YK_2.Add(obj.lst_Table_YK_2.Count, clone);
                        obj.lst_Table_YK_1[key].FlagDelete = true;
                    }
                }
                for (int k = 0; k < t.Value.cfg_fwt.u32PowNum; k++)
                {
                    int key = Convert.ToInt32(t.Value.cfg_fwt.u32PowIndex[k]);
                    if (obj.lst_Table_Meter_1.ContainsKey(key) == true)
                    {
                        CTableMeter clone = obj.lst_Table_Meter_1[key].MyClone();
                        //obj.lst_Table_Meter_2.Add(key, clone);
                        obj.lst_Table_Meter_2.Add(obj.lst_Table_Meter_2.Count, clone);
                        obj.lst_Table_Meter_1[key].FlagDelete = true;
                    }
                }
                //----
                int iAddr = Convert.ToInt32(t.Value.cfg_fwt.u32YcStart);
                foreach (var addr in obj.lst_Table_YC_2)
                {
                    addr.Value.Addr = iAddr;
                    iAddr += 1;
                }
                iAddr = Convert.ToInt32(t.Value.cfg_fwt.u32SyxStart);
                foreach (var addr in obj.lst_Table_SYX_2)
                {
                    addr.Value.Addr = iAddr;
                    iAddr += 1;
                }
                iAddr = Convert.ToInt32(t.Value.cfg_fwt.u32DyxStart);
                foreach (var addr in obj.lst_Table_DYX_2)
                {
                    addr.Value.Addr = iAddr;
                    iAddr += 1;
                }
                iAddr = Convert.ToInt32(t.Value.cfg_fwt.u32YkStart);
                foreach (var addr in obj.lst_Table_YK_2)
                {
                    addr.Value.Addr = iAddr;
                    iAddr += 1;
                }
                iAddr = Convert.ToInt32(t.Value.cfg_fwt.u32PowStart);
                foreach (var addr in obj.lst_Table_Meter_2)
                {
                    addr.Value.Addr = iAddr;
                    iAddr += 1;
                }
                //----
                Global.g_list_FWT.Add(obj.Id, obj);
                //----
                tn_set_FWT.Nodes.Add(obj.FWTName);
            }
            Global.sorted_list_FWT();
            tn_set_FWT.Expand();
            //--------------------------------------
        }
        #endregion
        #region "端口"
        private void tsmi_Import_Port_Click(object sender, EventArgs e)
        {
            string path = System.Environment.CurrentDirectory + @"\Binary";
            OpenFileDialog fd = new OpenFileDialog();
            fd.Title = "导入端口配置文件";
            //fd.InitialDirectory = path;
            fd.Filter = "二进制文件(*.cfg)|*.cfg";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                read_Cfg_Port(fd.FileName);
                fd.InitialDirectory = fd.FileName.Substring(0, fd.FileName.LastIndexOf("\\") + 1);
            }
        }
        #endregion
        #endregion

        #region "配置导出"
        #region "参数和定值"
        private void tsmi_Export_Para_Click(object sender, EventArgs e)
        {
            string path = System.Environment.CurrentDirectory + @"\Binary";
            SaveFileDialog fd = new SaveFileDialog();
            fd.Title = "生成参数和定值配置文件";
            fd.FileName = "Para";
            //fd.InitialDirectory = path;
            fd.Filter = "二进制文件(*.cfg)|*.cfg";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string strres = "";
                write_Cfg_Para_Setting(fd.FileName, ref strres);//先后顺序，导出“参数”和“定值”，jifeng，2017-6-15
                fd.InitialDirectory = fd.FileName.Substring(0, fd.FileName.LastIndexOf("\\") + 1);
                MessageBox.Show(strres);
            }
        }
        #endregion
        #region "定值(不用了)"
        private void tsmi_Export_Setting_Click(object sender, EventArgs e)
        {
            string path = System.Environment.CurrentDirectory + @"\Binary";
            SaveFileDialog fd = new SaveFileDialog();
            fd.Title = "生成定值配置文件";
            fd.FileName = "Setting";
            //fd.InitialDirectory = path;
            fd.Filter = "二进制文件(*.bin)|*.bin";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                write_Cfg_Setting(fd.FileName);
                fd.InitialDirectory = fd.FileName.Substring(0, fd.FileName.LastIndexOf("\\") + 1);
            }
        }
        #endregion
        #region "模板"
        private void tsmi_Export_Model_Click(object sender, EventArgs e)
        {
            string path = System.Environment.CurrentDirectory + @"\Binary";
            SaveFileDialog fd = new SaveFileDialog();
            fd.Title = "生成模板配置文件";
            fd.FileName = "Model";
            //fd.InitialDirectory = path;
            fd.Filter = "二进制文件(*.cfg)|*.cfg";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string strres = "";
                write_Cfg_Model(fd.FileName, ref strres);
                fd.InitialDirectory = fd.FileName.Substring(0, fd.FileName.LastIndexOf("\\") + 1);
                MessageBox.Show(strres);
            }
        }
        #endregion
        #region "设备"
        private void tsmi_Export_Device_Click(object sender, EventArgs e)
        {
            string path = System.Environment.CurrentDirectory + @"\Binary";
            SaveFileDialog fd = new SaveFileDialog();
            fd.Title = "生成设备表配置文件";
            fd.FileName = "Dev";
            //fd.InitialDirectory = path;
            fd.Filter = "二进制文件(*.cfg)|*.cfg";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string strres = "";
                write_Cfg_Device(fd.FileName, ref strres);
                fd.InitialDirectory = fd.FileName.Substring(0, fd.FileName.LastIndexOf("\\") + 1);
                MessageBox.Show(strres);
            }
        }
        #endregion
        #region "转发表"
        private void tsmi_Export_FDB_Click(object sender, EventArgs e)
        {
            string path = System.Environment.CurrentDirectory + @"\Binary";
            SaveFileDialog fd = new SaveFileDialog();
            fd.Title = "生成转发表配置文件";
            fd.FileName = "Fwt";
            //fd.InitialDirectory = path;
            fd.Filter = "二进制文件(*.cfg)|*.cfg";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string strres = "";
                write_Cfg_FWT(fd.FileName, ref strres);
                fd.InitialDirectory = fd.FileName.Substring(0, fd.FileName.LastIndexOf("\\") + 1);
                MessageBox.Show(strres);
            }
        }
        #endregion
        #region "端口"
        private void tsmi_Export_Port_Click(object sender, EventArgs e)
        {
            string path = System.Environment.CurrentDirectory + @"\Binary";
            SaveFileDialog fd = new SaveFileDialog();
            fd.Title = "生成端口配置文件";
            fd.FileName = "Port";
            //fd.InitialDirectory = path;
            fd.Filter = "二进制文件(*.cfg)|*.cfg";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string strres = "";
                write_Cfg_Port(fd.FileName, ref strres);
                fd.InitialDirectory = fd.FileName.Substring(0, fd.FileName.LastIndexOf("\\") + 1);
                //formInfo.LogMessage(string.Format("生成端口配置文件：{0}", fd.FileName));
                MessageBox.Show(strres);
            }
        }
        #endregion
        #endregion

        #region "自动配置"
        public bool gAutoCfgFlag = false;
        private void tsb_AtuoConfig_Click(object sender, EventArgs e)
        {
            gAutoCfgFlag = false;
            DialogResult res = MessageBox.Show("您确实要进行自动配置吗？", "确认操作", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (res == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            Form_AutoCfgSet dlg = new Form_AutoCfgSet(this);
            dlg.ShowDialog();
            if (gAutoCfgFlag == false)
            {
                return;
            }
            dlg.Close();
            dlg.Dispose();
            bool bres = Generate_Server();
            if(true == bres)
            {
                Generate_Client();
            }
        }
        #endregion

        #region "退出"
        private void tsb_Exit_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("您是否确认要退出软件？",
                                                   "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (res == System.Windows.Forms.DialogResult.Cancel) { return; }
            Environment.Exit(0);
        }
        #endregion

        #region "关于"
        private void tsb_About_Click(object sender, EventArgs e)
        {
            AboutDlg dlg = new AboutDlg();
            dlg.ShowDialog();
        }
        #endregion
        #endregion

        #region "树形控件MouseClick"
        private void tvNavi_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)//判断你点的是不是右键
            {
                Point ClickPoint = new Point(e.X, e.Y);
                TreeNode CurrentNode = tvNavi.GetNodeAt(ClickPoint);
                if (CurrentNode != null)//判断你点的是不是一个节点
                {
                    TreeNode parentNode = CurrentNode.Parent;
                    if (parentNode == null) //说明右键点击的是“root节点”
                    {
                        switch (CurrentNode.Text)
                        {
                            case Global.cst_Set_Para:
                                CurrentNode.ContextMenuStrip = cms_Para;
                                break;
                            case Global.cst_Set_Setting:
                                CurrentNode.ContextMenuStrip = cms_Setting;
                                break;
                            case Global.cst_Set_Model:
                                CurrentNode.ContextMenuStrip = cms_Model;
                                break;
                            case Global.cst_Set_DeviceTable:
                                CurrentNode.ContextMenuStrip = cms_DeviceTable;
                                break;
                            case Global.cst_Set_FWT:
                                CurrentNode.ContextMenuStrip = cms_FWT;
                                break;
                            case Global.cst_Set_RTDB:
                                CurrentNode.ContextMenuStrip = null;//cms_RTDB;//点击节点，即刷新，此处多余操作
                                break;
                            case Global.cst_Set_Port:
                                CurrentNode.ContextMenuStrip = cms_Port;
                                break;
                        }
                    }
                    else //说明右键点击的是“root的子节点”
                    {
                        TreeNode parentparentNode = parentNode.Parent;
                        switch (parentNode.Text)
                        {
                            case Global.cst_Set_Model:
                                CurrentNode.ContextMenuStrip = cms_Model_Delete;
                                break;
                            case Global.cst_Set_DeviceTable:
                                CurrentNode.ContextMenuStrip = cms_Device;//新增设备
                                break;
                            case Global.cst_Set_FWT:
                                CurrentNode.ContextMenuStrip = cms_FWT_Delete;
                                break;
                        }
                        if (parentparentNode != null)
                        {
                            if (parentparentNode.Text == Global.cst_Set_DeviceTable)
                            {
                                CurrentNode.ContextMenuStrip = cms_Device_Delete;//删除设备
                            }
                        }
                    }
                    tvNavi.SelectedNode = CurrentNode;//选中这个节点
                }
            }///////////////////////////////////////////////////////////////////////////////////
            else if (e.Button == MouseButtons.Left)
            {
                Point ClickPoint = new Point(e.X, e.Y);
                TreeNode CurrentNode = tvNavi.GetNodeAt(ClickPoint);
                if (CurrentNode != null)//判断你点的是不是一个节点
                {
                    TreeNode parentNode = CurrentNode.Parent;
                    if (parentNode == null) //点击了“root节点”
                    {
                        switch (CurrentNode.Text)
                        {
                            case Global.cst_Set_Para:
                                splitContainer1.Panel2.Controls.Clear();
                                break;
                            case Global.cst_Set_Model:
                                splitContainer1.Panel2.Controls.Clear();
                                Form_Table_Model dlg = new Form_Table_Model();
                                dlg.Dock = DockStyle.Fill;
                                dlg.TopLevel = false;
                                dlg.Show();
                                splitContainer1.Panel2.Controls.Add(dlg);
                                break;
                            case Global.cst_Set_DeviceTable:
                                splitContainer1.Panel2.Controls.Clear();
                                Form_Table_DeviceTable dlg3 = new Form_Table_DeviceTable();
                                dlg3.Dock = DockStyle.Fill;
                                dlg3.TopLevel = false;
                                dlg3.Show();
                                splitContainer1.Panel2.Controls.Add(dlg3);
                                break;
                            case Global.cst_Set_RTDB:
                                splitContainer1.Panel2.Controls.Clear();
                                Form_Table_RTDB dlg4 = new Form_Table_RTDB();
                                dlg4.Dock = DockStyle.Fill;
                                dlg4.TopLevel = false;
                                dlg4.Show();
                                splitContainer1.Panel2.Controls.Add(dlg4);
                                break;
                            case Global.cst_Set_FWT:
                                splitContainer1.Panel2.Controls.Clear();
                                Form_Table_FWT dlg5 = new Form_Table_FWT();
                                dlg5.Dock = DockStyle.Fill;
                                dlg5.TopLevel = false;
                                dlg5.Show();
                                splitContainer1.Panel2.Controls.Add(dlg5);
                                break;
                            case Global.cst_Set_Port:
                                splitContainer1.Panel2.Controls.Clear();
                                Form_Table_Port dlg6 = new Form_Table_Port();
                                dlg6.Dock = DockStyle.Fill;
                                dlg6.TopLevel = false;
                                dlg6.Show();
                                splitContainer1.Panel2.Controls.Add(dlg6);
                                break;
                        }
                    }
                    else //点击了“root的子节点”
                    {
                        TreeNode ppNode = parentNode.Parent;
                        switch (parentNode.Text)
                        {
                            case Global.cst_Set_Para:
                                splitContainer1.Panel2.Controls.Clear();
                                Form_Table_Para dlg20 = new Form_Table_Para(CurrentNode.Text);
                                dlg20.Dock = DockStyle.Fill;
                                dlg20.TopLevel = false;
                                dlg20.Show();
                                splitContainer1.Panel2.Controls.Add(dlg20);
                                break;
                            case Global.cst_Set_Setting:
                                splitContainer1.Panel2.Controls.Clear();
                                Form_Table_Setting dlg29 = new Form_Table_Setting(CurrentNode.Text);
                                dlg29.Dock = DockStyle.Fill;
                                dlg29.TopLevel = false;
                                dlg29.Show();
                                splitContainer1.Panel2.Controls.Add(dlg29);
                                break;
                            case Global.cst_Set_Model:
                                splitContainer1.Panel2.Controls.Clear();
                                Form_Table_Model_Name dlg2 = new Form_Table_Model_Name(CurrentNode.Text);
                                dlg2.Dock = DockStyle.Fill;
                                dlg2.TopLevel = false;
                                dlg2.Show();
                                splitContainer1.Panel2.Controls.Add(dlg2);
                                break;
                            case Global.cst_Set_DeviceTable:
                                splitContainer1.Panel2.Controls.Clear();
                                foreach (var t in Global.g_list_DeviceTable.Values)
                                {
                                    if (t.DeviceTableName == CurrentNode.Text)
                                    {
                                        Form_Table_Device dlg50 = new Form_Table_Device(t);
                                        dlg50.Dock = DockStyle.Fill;
                                        dlg50.TopLevel = false;
                                        dlg50.Show();
                                        splitContainer1.Panel2.Controls.Add(dlg50);
                                        break;
                                    }
                                }
                                break;
                            case Global.cst_Set_FWT:
                                string Name = "";
                                foreach (var t in Global.g_list_FWT.Values)
                                {
                                    if (t.FWTName == CurrentNode.Text)
                                    {
                                        Name = t.FWTName;
                                        break;
                                    }
                                }
                                splitContainer1.Panel2.Controls.Clear();
                                Form_Table_FWT_Name dlg8 = new Form_Table_FWT_Name(Name);
                                dlg8.Dock = DockStyle.Fill;
                                dlg8.TopLevel = false;
                                dlg8.Show();
                                splitContainer1.Panel2.Controls.Add(dlg8);
                                break;
                            case Global.cst_Set_Port:
                                string PortName = "";
                                foreach (var t in Global.g_Model.lst_Table_Port.Values)
                                {
                                    if (t.PortName == CurrentNode.Text)
                                    {
                                        PortName = t.PortName;
                                        break;
                                    }
                                }
                                splitContainer1.Panel2.Controls.Clear();
                                Form_Table_Port_Name dlg9 = new Form_Table_Port_Name(PortName);
                                dlg9.Dock = DockStyle.Fill;
                                dlg9.TopLevel = false;
                                dlg9.Show();
                                splitContainer1.Panel2.Controls.Add(dlg9);
                                break;
                        }
                        //----
                        if (ppNode != null)
                        {
                            if (ppNode.Text == Global.cst_Set_DeviceTable)
                            {
                                foreach (var t in Global.g_list_DeviceTable.Values)
                                {
                                    foreach (var dev in t.lst_Device.Values)
                                    {
                                        if (dev.DeviceName == CurrentNode.Text)
                                        {
                                            splitContainer1.Panel2.Controls.Clear();
                                            Form_Table_Model_Name dlg90 = new Form_Table_Model_Name(dev.ModelName);
                                            dlg90.Dock = DockStyle.Fill;
                                            dlg90.TopLevel = false;
                                            dlg90.Show();
                                            splitContainer1.Panel2.Controls.Add(dlg90);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }
        }
        #endregion

        #region "树形控件右键菜单项响应"
        #region "右键菜单_参数集(3个)"
        #region "恢复默认参数"
        private void tsmi_RecoveryDefaultPara_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("您确实要恢复参数集默认参数吗？", "确认操作", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                foreach (var t in Global.g_Model.lst_Table_Para.Values)
                {
                    t.ValueCurrent = t.ValueDefault;
                    t.strValueCurrent = t.ValueDefault.ToString();
                }
                formInfo.LogMessage("参数集恢复默认参数");
            }
        }
        #endregion
        #region "导入参数配置(不用了)"
        //工具栏上，导入 参数和定值。jifeng，2017-6-15
        private void tsmi_ImportParaConfig_Click(object sender, EventArgs e)
        {
            string path = System.Environment.CurrentDirectory + @"\Binary";
            OpenFileDialog fd = new OpenFileDialog();
            fd.Title = "导入参数配置";
            //fd.InitialDirectory = path;
            fd.Filter = "二进制文件(*.cfg)|*.cfg";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                //read_Cfg_Para(fd.FileName);
                fd.InitialDirectory = fd.FileName.Substring(0, fd.FileName.LastIndexOf("\\") + 1);
            }
        }

        void read_Cfg_Para_Setting(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            formInfo.LogMessage(string.Format("读取的【参数和定值】文件[{0}]大小：{1}字节", filename, fs.Length));
            Global.g_Model.read_bin_Para_Setting(br);
            MessageBox.Show(string.Format("读取[{0}]结束！", filename), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            br.Close();
            fs.Close();
        }
        #endregion
        #region "生成参数配置文件(不用了)"
        private void tsmi_GenerateParaConfigFile_Click(object sender, EventArgs e)
        {
            string path = System.Environment.CurrentDirectory + @"\Binary";
            SaveFileDialog fd = new SaveFileDialog();
            fd.Title = "生成参数配置文件";
            fd.FileName = "Para";
            //fd.InitialDirectory = path;
            fd.Filter = "二进制文件(*.cfg)|*.cfg";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                //write_Cfg_Para_(fd.FileName);
                fd.InitialDirectory = fd.FileName.Substring(0, fd.FileName.LastIndexOf("\\") + 1);
            }
        }

        bool write_Cfg_Para_Setting(string filename, ref string strres)
        {
            try
            {
                FileStream fs = new FileStream(filename, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                //---------
                Global.g_Model.write_bin_Para_Setting(bw);
                formInfo.LogMessage(string.Format("生成【参数和定值】配置文件[{0}]大小：{1}字节", filename, fs.Length));
                //---------
                bw.Close();
                fs.Close();
                //MessageBox.Show(string.Format("保存[{0}]成功！", filename), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                strres = string.Format("保存[{0}]成功！", filename);
                return true;
            }
            catch (Exception ex)
            {
                strres = string.Format("保存[{0}]失败...失败原因：{1}", filename, ex.Message.ToString());
                //MessageBox.Show(ex.Message.ToString());
                return false;
            }
        }
        #endregion
        #endregion

        #region "右键菜单_定值集(3个)"
        #region "恢复默认参数"
        private void tsmi_Setting_RecoveryDefaultPara_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("您确实要恢复定值集默认参数吗？", "确认操作", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                foreach (var t in Global.g_Model.lst_Table_Setting.Values)
                {
                    t.ValueCurrent = t.ValueDefault;
                    t.strValueCurrent = t.ValueDefault.ToString();
                }
                formInfo.LogMessage("定值集恢复默认参数");
            }
        }
        #endregion
        #region "导入参数配置(不用了)"
        private void tsmi_Setting_ImportParaConfig_Click(object sender, EventArgs e)
        {
            string path = System.Environment.CurrentDirectory + @"\Binary";
            OpenFileDialog fd = new OpenFileDialog();
            fd.Title = "导入定值配置";
            //fd.InitialDirectory = path;
            fd.Filter = "二进制文件(*.cfg)|*.cfg";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                read_Cfg_Setting(fd.FileName);
                fd.InitialDirectory = fd.FileName.Substring(0, fd.FileName.LastIndexOf("\\") + 1);
            }
        }

        void read_Cfg_Setting(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            formInfo.LogMessage(string.Format("读取的定值文件[{0}]大小：{1}字节", filename, fs.Length));
            try
            {
                //while (true)
                //{
                //    Global.g_Model.read_bin_Setting(br);
                //}
            }
            catch (Exception)
            {
                MessageBox.Show(string.Format("读取[{0}]结束！", filename), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            br.Close();
            fs.Close();
        }
        #endregion
        #region "生成参数配置文件(不用了)"
        private void tsmi_Setting_GenerateParaConfigFile_Click(object sender, EventArgs e)
        {
            string path = System.Environment.CurrentDirectory + @"\Binary";
            SaveFileDialog fd = new SaveFileDialog();
            fd.Title = "生成定值配置文件";
            fd.FileName = "Setting";
            //fd.InitialDirectory = path;
            fd.Filter = "二进制文件(*.cfg)|*.cfg";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                write_Cfg_Setting(fd.FileName);
                fd.InitialDirectory = fd.FileName.Substring(0, fd.FileName.LastIndexOf("\\") + 1);
            }
        }

        void write_Cfg_Setting(string filename)
        {
            try
            {
                FileStream fs = new FileStream(filename, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                //---------
                //Global.g_Model.write_bin_Setting(bw);
                formInfo.LogMessage(string.Format("生成定值配置文件[{0}]大小：{1}字节", filename, fs.Length));
                //---------
                bw.Close();
                fs.Close();
                MessageBox.Show(string.Format("保存[{0}]成功！", filename), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        #endregion
        #endregion

        #region "右键菜单_模板集(1+1个)"
        #region "导入模板"
        private void tsmi_Template_Import_Click(object sender, EventArgs e)
        {
            string path = System.Environment.CurrentDirectory + @"\Text";//@是取消转义字符的意思 
            OpenFileDialog fd = new OpenFileDialog();
            fd.Title = "请选择模板";
            //fd.InitialDirectory = path; 
            fd.Filter = "文本文件(*.txt)|*.txt";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string strAbsolutePath = System.IO.Path.GetFullPath(fd.FileName);//绝对路径
                formInfo.LogMessage(string.Format("导入模板：{0}", fd.FileName));
                string strFileName = System.IO.Path.GetFileNameWithoutExtension(fd.FileName);//文件名没有扩展名
                //重复文件名判断-start
                foreach (var t in Global.g_list_Model.Values)
                {
                    if (t.ModelName == strFileName)
                    {
                        MessageBox.Show("模板名称重复！请重新选择。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                //重复文件名判断-end
                CModel mdl = new CModel();
                mdl.init();
                mdl.ModelName = strFileName;
                short iSet = 0;
                //using (StreamReader sr = new StreamReader(strAbsolutePath, Encoding.UTF8))
                using (StreamReader sr = new StreamReader(strAbsolutePath, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        string readStr = sr.ReadLine();
                        readStr.Trim(); readStr.TrimStart(); readStr.TrimEnd();
                        if (readStr == "") { iSet = 0; continue; }
                        if (readStr.Contains(Global.cst_Table_Para) == true)
                        {
                            iSet = CST_SET.Index_Para;
                        }
                        else if (readStr.Contains(Global.cst_Table_Setting) == true)
                        {
                            iSet = CST_SET.Index_Setting;
                        }
                        else if (readStr.Contains(Global.cst_Table_YC) == true)
                        {
                            iSet = CST_SET.Index_YC;
                        }
                        else if (readStr.Contains(Global.cst_Table_SYX) == true)
                        {
                            iSet = CST_SET.Index_SYX;
                        }
                        else if (readStr.Contains(Global.cst_Table_DYX) == true)
                        {
                            iSet = CST_SET.Index_DYX;
                        }
                        else if (readStr.Contains(Global.cst_Table_YK) == true)
                        {
                            iSet = CST_SET.Index_YK;
                        }
                        else if (readStr.Contains(Global.cst_Table_Meter) == true)
                        {
                            iSet = CST_SET.Index_Meter;
                        }
                        else if (readStr.Contains(Global.cst_Table_Port) == true)
                        {
                            iSet = CST_SET.Index_Port;
                        }
                        if (iSet == CST_SET.Index_Para) //参数表
                        {
                            if (readStr.Contains("//") == false && readStr.Contains(Global.cst_Table_Para) == false)
                            {
                                string[] sa = readStr.Split(Global.g_SplitChar, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                                CTablePara obj = get_Para(sa);
                                mdl.lst_Table_Para.Add(obj.Id, obj);
                            }
                        }
                        else if (iSet == CST_SET.Index_Setting) //定值表
                        {
                            if (readStr.Contains("//") == false && readStr.Contains(Global.cst_Table_Setting) == false)
                            {
                                string[] sa = readStr.Split(Global.g_SplitChar, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                                CTableSetting obj = get_Setting(sa);
                                mdl.lst_Table_Setting.Add(obj.Id, obj);
                            }
                        }
                        else if (iSet == CST_SET.Index_YC) //遥测表
                        {
                            if (readStr.Contains("//") == false && readStr.Contains(Global.cst_Table_YC) == false)
                            {
                                string[] sa = readStr.Split(Global.g_SplitChar, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                                CTableYC obj = get_YC(sa);
                                mdl.lst_Table_YC.Add(obj.Id, obj);
                            }
                        }
                        else if (iSet == CST_SET.Index_SYX) //单点遥信表
                        {
                            if (readStr.Contains("//") == false && readStr.Contains(Global.cst_Table_SYX) == false)
                            {
                                string[] sa = readStr.Split(Global.g_SplitChar, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                                CTableSYX obj = get_SYX(sa);
                                mdl.lst_Table_SYX.Add(obj.Id, obj);
                            }
                        }
                        else if (iSet == CST_SET.Index_DYX) //双点遥信表
                        {
                            if (readStr.Contains("//") == false && readStr.Contains(Global.cst_Table_DYX) == false)
                            {
                                string[] sa = readStr.Split(Global.g_SplitChar, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                                CTableDYX obj = get_DYX(sa);
                                mdl.lst_Table_DYX.Add(obj.Id, obj);
                            }
                        }
                        else if (iSet == CST_SET.Index_YK) //遥控表
                        {
                            if (readStr.Contains("//") == false && readStr.Contains(Global.cst_Table_YK) == false)
                            {
                                string[] sa = readStr.Split(Global.g_SplitChar, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                                CTableYK obj = get_YK(sa);
                                mdl.lst_Table_YK.Add(obj.Id, obj);
                            }
                        }
                        else if (iSet == CST_SET.Index_Meter) //计量值表
                        {
                            if (readStr.Contains("//") == false && readStr.Contains(Global.cst_Table_Meter) == false)
                            {
                                string[] sa = readStr.Split(Global.g_SplitChar, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                                CTableMeter obj = get_Meter(sa);
                                mdl.lst_Table_Meter.Add(obj.Id, obj);
                            }
                        }
                        else if (iSet == CST_SET.Index_Port) //端口表
                        {
                            if (readStr.Contains("//") == false && readStr.Contains(Global.cst_Table_Port) == false)
                            {
                                string[] sa = readStr.Split(Global.g_SplitChar, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                                CTablePort obj = get_Port(sa);
                                mdl.lst_Table_Port.Add(obj.Id, obj);
                            }
                        }
                    }
                }
                mdl.Id = CModel.Accu;
                CModel.Accu += 1;
                Global.g_list_Model.Add(mdl.Id, mdl);
                Global.sorted_list_Model();
                TreeNode tn = new TreeNode(mdl.ModelName);
                tn_set_Model.Nodes.Add(tn);
                tvNavi.ExpandAll();
                tn_set_Para.Collapse();
                tn_set_Setting.Collapse();
                fd.InitialDirectory = fd.FileName.Substring(0, fd.FileName.LastIndexOf("\\") + 1);
            }
        }
        #endregion
        #region "删除模板"
        private void tsmi_Template_Delete_Click(object sender, EventArgs e)
        {
            string name = tvNavi.SelectedNode.Text;
            bool bused = false;
            string DeviceName = "";
            foreach (var t in Global.g_list_DeviceTable)
            {
                foreach (var dev in t.Value.lst_Device.Values)
                {
                    if (dev.ModelName == name)
                    {
                        bused = true;
                        DeviceName = dev.DeviceName;
                        break;
                    }
                }
            }
            //----
            if (bused == true)
            {
                MessageBox.Show(string.Format("模板已经{0}被设备{1}使用，请您检查确认！", name, DeviceName), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //----
            DialogResult result = MessageBox.Show(string.Format("您确实要删除模板[{0}]吗？", name), "删除模板", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                int key = -1;
                foreach (var t in Global.g_list_Model)
                {
                    if (t.Value.ModelName == name)
                    {
                        key = t.Key;
                        break;
                    }
                }
                Global.g_list_Model.Remove(key);
                Global.sorted_list_Model();
                foreach (TreeNode tn in tn_set_Model.Nodes)
                {
                    if (tn.Text == name)
                    {
                        tn.Remove();
                        splitContainer1.Panel2.Controls.Clear();
                        formInfo.LogMessage(string.Format("删除模板：{0}", name));
                        break;
                    }
                }
            }
        }
        #endregion
        #region "生成模板配置文件"
        private void tsmi_GenerateModelConfigFile_Click(object sender, EventArgs e)
        {
            string path = System.Environment.CurrentDirectory + @"\Binary";
            SaveFileDialog fd = new SaveFileDialog();
            fd.Title = "生成模板配置文件";
            fd.FileName = "Model";
            //fd.InitialDirectory = path;
            fd.Filter = "二进制文件(*.cfg)|*.cfg";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string strres = "";
                write_Cfg_Model(fd.FileName, ref strres);
                fd.InitialDirectory = fd.FileName.Substring(0, fd.FileName.LastIndexOf("\\") + 1);
                MessageBox.Show(strres);
            }
        }

        bool write_Cfg_Model(string filename, ref string strres)
        {
            try
            {
                FileStream fs = new FileStream(filename, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                //---------
                ModelCfgFile_t cfg_model = new ModelCfgFile_t();
                cfg_model.form_Piece();
                cfg_model.write_bin(bw);
                formInfo.LogMessage(string.Format("保存的模板文件[{0}]大小：{1}字节", filename, fs.Length));
                //---------
                bw.Close();
                fs.Close();
                //MessageBox.Show(string.Format("保存[{0}]成功！", filename), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                strres = string.Format("保存[{0}]成功！", filename);
                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
                strres = string.Format("保存[{0}]失败...失败原因：{1}", filename, ex.Message.ToString());
                return false;
            }
        }
        #endregion
        #endregion

        #region "右键菜单_设备表集(2+1个)"
        #region "新增设备表"
        private void tsmi_AddDeviceTable_Click(object sender, EventArgs e)
        {
            Form_Add_DeviceTable dlg = new Form_Add_DeviceTable(this);
            dlg.Show(this);
        }
        #endregion
        #region "生成设备表配置文件"
        private void tmi_SaveDeviceTableConfigFile_Click(object sender, EventArgs e)
        {
            string path = System.Environment.CurrentDirectory + @"\Binary";
            SaveFileDialog fd = new SaveFileDialog();
            fd.Title = "生成设备表配置文件";
            fd.FileName = "Dev";
            //fd.InitialDirectory = path;
            fd.Filter = "二进制文件(*.cfg)|*.cfg";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string strres = "";
                write_Cfg_Device(fd.FileName, ref strres);
                fd.InitialDirectory = fd.FileName.Substring(0, fd.FileName.LastIndexOf("\\") + 1);
                MessageBox.Show(strres);
            }
        }
        #endregion
        #region "删除设备表"
        private void cms_DeviceTable_Delete_Click(object sender, EventArgs e)
        {
            TreeNode tnDev = tvNavi.SelectedNode;
            string name = tnDev.Text;
            DialogResult result = MessageBox.Show(string.Format("您确实要删除设备表[{0}]吗？", name), "删除设备表", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                int key = -1;
                foreach (var t in Global.g_list_DeviceTable)
                {
                    if (t.Value.DeviceTableName == name)
                    {
                        key = t.Key;
                        break;
                    }
                }
                Global.g_list_DeviceTable.Remove(key);
                Global.sorted_list_DeviceTable();
                //-------------------------------
                tnDev.Remove();
                splitContainer1.Panel2.Controls.Clear();
                formInfo.LogMessage(string.Format("删除设备表：{0}", name));
            }
        }
        #endregion
        //---------------------------------------------------------------------------
        #region "新增设备"
        private void tsmi_AddDevice_Click(object sender, EventArgs e)
        {
            TreeNode tn = tvNavi.SelectedNode;
            string DeviceTableName = tn.Text;
            //----
            foreach (var t in Global.g_list_DeviceTable)
            {
                if (t.Value.DeviceTableName == DeviceTableName)
                {
                    Form_Add_Device dlg = new Form_Add_Device(this, t.Value);
                    dlg.Show(this);
                }
            }
        }

        public void addNode_DeviceTable(CDeviceTable c)
        {
            TreeNode t = tn_set_DeviceTable.Nodes.Add(c.DeviceTableName);
            t.Tag = c.Id;
            foreach (var dev in c.lst_Device.Values)
            {
                addNode_Device(t, dev);
            }
            tvNavi.ExpandAll();
            tn_set_Para.Collapse();
            tn_set_Setting.Collapse();
        }

        public void addNode_Device(TreeNode tn, CDevice c)
        {
            TreeNode t = tn.Nodes.Add(c.DeviceName);
            t.Tag = c.Id;
            tvNavi.ExpandAll();
            tn_set_Para.Collapse();
            tn_set_Setting.Collapse();
        }

        public void addNode_Device(CDeviceTable dt, CDevice c)
        {
            foreach (TreeNode tn in tn_set_DeviceTable.Nodes)
            {
                if (tn.Text == dt.DeviceTableName)
                {
                    TreeNode t = tn.Nodes.Add(c.DeviceName);
                    t.Tag = c.Id;
                    break;
                }
            }
            tvNavi.ExpandAll();
            tn_set_Para.Collapse();
            tn_set_Setting.Collapse();
        }
        #endregion
        #region "保存设备配置文件(删除)"
        private void tmi_SaveDeviceConfigFile_Click(object sender, EventArgs e)
        {
            string path = System.Environment.CurrentDirectory + @"\Binary";
            SaveFileDialog fd = new SaveFileDialog();
            fd.Title = "保存设备配置文件";
            fd.FileName = "Dev";
            //fd.InitialDirectory = path;
            fd.Filter = "二进制文件(*.cfg)|*.cfg";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string strres = "";
                write_Cfg_Device(fd.FileName, ref strres);
                fd.InitialDirectory = fd.FileName.Substring(0, fd.FileName.LastIndexOf("\\") + 1);
                MessageBox.Show(strres);
            }
        }

        bool write_Cfg_Device(string filename, ref string strres)
        {
            try
            {
                FileStream fs = new FileStream(filename, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                //---------
                DevsCfgFile_t cfg_dev = new DevsCfgFile_t();
                cfg_dev.formDevCfg();
                //----
                cfg_dev.write_bin(bw);
                //----
                formInfo.LogMessage(string.Format("保存的设备配置文件[{0}]大小：{1}字节", filename, fs.Length));
                //---------
                bw.Close();
                fs.Close();
                //MessageBox.Show(string.Format("保存[{0}]成功！", filename), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                strres = string.Format("保存[{0}]成功！", filename);
                return true;
            }
            catch (Exception ex)
            {
                strres = string.Format("保存[{0}]失败...失败原因：{1}", filename, ex.Message.ToString());
                return false;
            }
        }
        #endregion
        #region "删除设备"
        private void tsmi_Device_Delete_Click(object sender, EventArgs e)
        {
            TreeNode tnDev = tvNavi.SelectedNode;
            string name = tnDev.Text;
            DialogResult result = MessageBox.Show(string.Format("您确实要删除设备[{0}]吗？", name), "删除设备", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                int key = -1;
                CDeviceTable DeviceTable = null;
                foreach (var t in Global.g_list_DeviceTable)
                {
                    foreach (var dev in t.Value.lst_Device)
                    {
                        if (dev.Value.DeviceName == name)
                        {
                            //key = t.Key;//bug，删除不掉设备。jifeng，2017-12-03 12:43
                            key = dev.Key;
                            DeviceTable = t.Value;
                            break;
                        }
                    }
                }
                DeviceTable.lst_Device.Remove(key);
                Global.sorted_list_DeviceTable();
                //-------------------------------
                tnDev.Remove();
                splitContainer1.Panel2.Controls.Clear();
                formInfo.LogMessage(string.Format("删除设备：{0}", name));
            }
        }
        #endregion
        #endregion

        #region "右键菜单_转发表集(2+1个)"
        #region "新增转发表"
        private void tsmi_AddFDB_Click(object sender, EventArgs e)
        {
            if (Global.g_list_FWT.Count == Global.iniComm_CN_COMM_FWT_MAX)
            {
                MessageBox.Show(string.Format("转发表最大数量是{0}！", Global.iniComm_CN_COMM_FWT_MAX), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Form_Add_FWT dlg = new Form_Add_FWT(this);
            dlg.Show(this);
        }

        public void addNode_FWT(CFWT c)
        {
            TreeNode t = tn_set_FWT.Nodes.Add(c.FWTName);
            t.Tag = c.Id;
            tvNavi.ExpandAll();
            tn_set_Para.Collapse();
            tn_set_Setting.Collapse();
        }
        #endregion
        #region "生成转发表配置文件"
        private void tsmi_GenerateFDBConfigFile_Click(object sender, EventArgs e)
        {
            string path = System.Environment.CurrentDirectory + @"\Binary";
            SaveFileDialog fd = new SaveFileDialog();
            fd.Title = "生成转发表配置文件";
            fd.FileName = "Fwt";
            //fd.InitialDirectory = path;
            fd.Filter = "二进制文件(*.cfg)|*.cfg";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string strres = "";
                write_Cfg_FWT(fd.FileName, ref strres);
                fd.InitialDirectory = fd.FileName.Substring(0, fd.FileName.LastIndexOf("\\") + 1);
                MessageBox.Show(strres);
            }
        }

        bool write_Cfg_FWT(string filename, ref string strres)
        {
            try
            {
                FileStream fs = new FileStream(filename, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                //---------
                bw.Write(Convert.ToUInt32(Global.g_list_FWT.Count));//增加 转发表数量，jifeng，2017-6-15
                if (Global.bPrintFwt == true)
                {
                    byte[] bt = BitConverter.GetBytes(Global.g_list_FWT.Count);
                    string ssss = string.Format("转发表数量: {0:X2} {1:X2} {2:X2} {3:X2}", bt[0], bt[1], bt[2], bt[3]);
                    formInfo.LogMessage(ssss);
                }
                foreach (var t in Global.g_list_FWT)
                {
                    t.Value.convertData();
                    t.Value.cfg_fwt.write_bin(bw);
                }
                formInfo.LogMessage(string.Format("保存的转发表文件[{0}]大小：{1}字节", filename, fs.Length));
                //---------
                bw.Close();
                fs.Close();
                //MessageBox.Show(string.Format("保存[{0}]成功！", filename), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                strres = string.Format("保存[{0}]成功！", filename);
                return true;
            }
            catch (Exception ex)
            {
                strres = string.Format("保存[{0}]失败...失败原因：{1}", filename, ex.Message.ToString());
                return false;
            }
        }
        #endregion
        #region "删除转发表"
        private void tsmi_FDB_Delete_Click(object sender, EventArgs e)
        {
            string name = tvNavi.SelectedNode.Text;
            DialogResult result = MessageBox.Show(string.Format("您确实要删除转发表[{0}]吗？", name), "删除转发表", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                int key = -1;
                foreach (var t in Global.g_list_FWT)
                {
                    if (t.Value.FWTName == name)
                    {
                        key = t.Key;
                        break;
                    }
                }
                Global.g_list_FWT.Remove(key);
                //----
                //Global.refresh_list_FWT();
                //----
                Global.sorted_list_FWT();
                foreach (TreeNode tn in tn_set_FWT.Nodes)
                {
                    if (tn.Text == name)
                    {
                        tn.Remove();
                        splitContainer1.Panel2.Controls.Clear();
                        formInfo.LogMessage(string.Format("删除转发表：{0}", name));
                        break;
                    }
                }
            }
        }
        #endregion
        #region "遥测最大值和系数设置"
        private void tsmi_YCSet_Click(object sender, EventArgs e)
        {
            string name = tvNavi.SelectedNode.Text;
            foreach (var obj in Global.g_list_FWT)
            {
                if (obj.Value.FWTName == name)
                {
                    CFWT f = obj.Value;
                    Form_YCSet dlg = new Form_YCSet(ref f);
                    dlg.ShowDialog(this);
                }
            }
        }
        #endregion
        #region "全部转发"
        private void tsmi_FDB_AllFW_Click(object sender, EventArgs e)
        {
            string name = tvNavi.SelectedNode.Text;
            foreach (var t in Global.g_list_FWT)
            {
                if (t.Value.FWTName == name)
                {
                    t.Value.lst_Table_YC_2.Clear();
                    t.Value.lst_Table_SYX_2.Clear();
                    t.Value.lst_Table_DYX_2.Clear();
                    t.Value.lst_Table_YK_2.Clear();
                    t.Value.lst_Table_Meter_2.Clear();
                    //----
                    foreach (var t_2 in t.Value.lst_Table_YC_1)
                    {
                        CTableYC obj = t_2.Value.MyClone();
                        t.Value.lst_Table_YC_2.Add(t.Value.lst_Table_YC_2.Count, obj);
                        t_2.Value.FlagDelete = true;
                    }
                    foreach (var t_2 in t.Value.lst_Table_SYX_1)
                    {
                        CTableSYX obj = t_2.Value.MyClone();
                        t.Value.lst_Table_SYX_2.Add(t.Value.lst_Table_SYX_2.Count, obj);
                        t_2.Value.FlagDelete = true;
                    }
                    foreach (var t_2 in t.Value.lst_Table_DYX_1)
                    {
                        CTableDYX obj = t_2.Value.MyClone();
                        t.Value.lst_Table_DYX_2.Add(t.Value.lst_Table_DYX_2.Count, obj);
                        t_2.Value.FlagDelete = true;
                    }
                    foreach (var t_2 in t.Value.lst_Table_YK_1)
                    {
                        CTableYK obj = t_2.Value.MyClone();
                        t.Value.lst_Table_YK_2.Add(t.Value.lst_Table_YK_2.Count, obj);
                        t_2.Value.FlagDelete = true;
                    }
                    foreach (var t_2 in t.Value.lst_Table_Meter_1)
                    {
                        CTableMeter obj = t_2.Value.MyClone();
                        t.Value.lst_Table_Meter_2.Add(t.Value.lst_Table_Meter_2.Count, obj);
                        t_2.Value.FlagDelete = true;
                    }
                    //------------------------------------------------------------------------
                    int iAddr = (int)0x4001;
                    foreach (var obj in t.Value.lst_Table_YC_2.Values)
                    {
                        obj.Addr = iAddr;
                        iAddr += 1;
                    }
                    iAddr = (int)0x0001;
                    foreach (var obj in t.Value.lst_Table_SYX_2.Values)
                    {
                        obj.Addr = iAddr;
                        iAddr += 1;
                    }
                    //----
                    iAddr = 0;
                    int iSYXCount = t.Value.lst_Table_SYX_2.Count;
                    if (iSYXCount == 0)
                    {
                        iAddr = (int)0x0001;
                    }
                    else
                    {
                        int iAcc = 0;
                        foreach (var obj in t.Value.lst_Table_SYX_2.Values)
                        {
                            iAcc += 1;
                            if (iAcc == iSYXCount)
                            {
                                iAddr = obj.Addr + 1;
                            }
                        }
                    }
                    foreach (var obj in t.Value.lst_Table_DYX_2.Values)
                    {
                        obj.Addr = iAddr;
                        iAddr += 1;
                    }
                    iAddr = (int)0x6001;
                    foreach (var obj in t.Value.lst_Table_YK_2.Values)
                    {
                        obj.Addr = iAddr;
                        iAddr += 1;
                    }
                    iAddr = (int)0x6401;
                    foreach (var obj in t.Value.lst_Table_Meter_2.Values)
                    {
                        obj.Addr = iAddr;
                        iAddr += 1;
                    }
                    break;
                }
            }
        }
        #endregion
        #region "全不转发"
        private void tsmi_FDB_AllNotFW_Click(object sender, EventArgs e)
        {
            string name = tvNavi.SelectedNode.Text;
            foreach (var t in Global.g_list_FWT)
            {
                if (t.Value.FWTName == name)
                {
                    foreach (var t_2 in t.Value.lst_Table_YC_2)
                    {
                        t.Value.lst_Table_YC_1[t_2.Value.SN].FlagDelete = false;
                    }
                    t.Value.lst_Table_YC_2.Clear();
                    //----
                    foreach (var t_2 in t.Value.lst_Table_SYX_2)
                    {
                        t.Value.lst_Table_SYX_1[t_2.Value.SN].FlagDelete = false;
                    }
                    t.Value.lst_Table_SYX_2.Clear();
                    //----
                    foreach (var t_2 in t.Value.lst_Table_DYX_2)
                    {
                        t.Value.lst_Table_DYX_1[t_2.Value.SN].FlagDelete = false;
                    }
                    t.Value.lst_Table_DYX_2.Clear();
                    //----
                    foreach (var t_2 in t.Value.lst_Table_YK_2)
                    {
                        t.Value.lst_Table_YK_1[t_2.Value.SN].FlagDelete = false;
                    }
                    t.Value.lst_Table_YK_2.Clear();
                    //----
                    foreach (var t_2 in t.Value.lst_Table_Meter_2)
                    {
                        t.Value.lst_Table_Meter_1[t_2.Value.SN].FlagDelete = false;
                    }
                    t.Value.lst_Table_Meter_2.Clear();
                }
            }
        }
        #endregion
        #region "生成转发表模板"
        //2017-11-21 19:31
        private void tsmi_FDB_Model_Fwt_Click(object sender, EventArgs e)
        {
            string strFWTName = tvNavi.SelectedNode.Text;
            string s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11;
            int iCount = 0;
            string strTemp = "";
            string path = System.Environment.CurrentDirectory + @"\Text";
            SaveFileDialog fd = new SaveFileDialog();
            fd.Title = "生成转发表模板model_fwt";
            fd.Filter = "文本文件|*.txt";
            //fd.AddExtension = false;
            fd.FileName = string.Format("{0}_fwt", Global.g_Model.ModelName);
            //fd.Filter = "文本文件(*.txt)|*.txt";
            //fd.AddExtension = false;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(fd.FileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                //开始写入
                sw.Write("[参数表]\r\n");
                foreach (var t in Global.g_Model.lst_Table_Para.Values)
                {
                    s1 = ParaConfig.alignmentStrFunc(string.Format("{0}", t.Id), 0);
                    s2 = ParaConfig.alignmentStrFunc(t.GroupName, 4);
                    s3 = ParaConfig.alignmentStrFunc(t.ItemName, 4);
                    s4 = ParaConfig.alignmentStrFunc(t.DataType, 1);
                    s5 = ParaConfig.alignmentStrFunc(t.Unit, 1);
                    s6 = ParaConfig.alignmentStrFunc(t.ValueMax.ToString("0.000"), 1);//最大值
                    s7 = ParaConfig.alignmentStrFunc(t.ValueMin.ToString("0.000"), 1);//最小值
                    s8 = ParaConfig.alignmentStrFunc(t.ValueDefault.ToString("0.000"), 1);//默认值
                    s9 = ParaConfig.alignmentStrFunc(t.Ratio.ToString("0.000"), 1);//通道系数值
                    s10 = ParaConfig.alignmentStrFunc(string.Format("0x{0:X8}", t.Addr), 1);//信息对象地址
                    s11 = ParaConfig.alignmentStrFunc(string.Format("0x{0:X8}", t.BytePos), 1);//信息对象地址
                    //----
                    strTemp = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}\r\n", s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11);
                    sw.Write(strTemp);
                }
                sw.Write("\r\n");
                sw.Write("[定值表]\r\n");
                foreach (var t in Global.g_Model.lst_Table_Setting.Values)
                {
                    s1 = ParaConfig.alignmentStrFunc(string.Format("{0}", t.Id), 0);
                    s2 = ParaConfig.alignmentStrFunc(t.GroupName, 4);
                    s3 = ParaConfig.alignmentStrFunc(t.ItemName, 4);
                    s4 = ParaConfig.alignmentStrFunc(t.DataType, 1);
                    s5 = ParaConfig.alignmentStrFunc(t.Unit, 1);
                    s6 = ParaConfig.alignmentStrFunc(t.ValueMax.ToString("0.000"), 1);//最大值
                    s7 = ParaConfig.alignmentStrFunc(t.ValueMin.ToString("0.000"), 1);//最小值
                    s8 = ParaConfig.alignmentStrFunc(t.ValueDefault.ToString("0.000"), 1);//默认值
                    s9 = ParaConfig.alignmentStrFunc(t.Ratio.ToString("0.000"), 1);//通道系数值
                    s10 = ParaConfig.alignmentStrFunc(string.Format("0x{0:X8}", t.Addr), 1);//信息对象地址
                    s11 = ParaConfig.alignmentStrFunc(string.Format("0x{0:X8}", t.BytePos), 1);//信息对象地址
                    //----
                    strTemp = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}{10}\r\n", s1, s2, s3, s4, s5, s6, s7, s8, s9, s10, s11);
                    sw.Write(strTemp);
                }
                sw.Write("\r\n");
                //----
                foreach (var fwt in Global.g_list_FWT.Values)
                {
                    if (fwt.FWTName == strFWTName)
                    {
                        sw.Write("[遥测表]\r\n");
                        iCount = 0;
                        foreach (var t in fwt.lst_Table_YC_2.Values)
                        {
                            s1 = ParaConfig.alignmentStrFunc(string.Format("{0}", iCount + 1), 0);
                            s2 = ParaConfig.alignmentStrFunc(t.GroupName, 4);
                            s3 = ParaConfig.alignmentStrFunc(t.DeviceName + "_" + t.ItemName, 4);
                            s4 = ParaConfig.alignmentStrFunc(t.DataType, 1);
                            s5 = ParaConfig.alignmentStrFunc(t.Unit, 1);
                            s6 = ParaConfig.alignmentStrFunc(t.Ratio.ToString(), 1);
                            s7 = ParaConfig.alignmentStrFunc(t.iGroup.ToString(), 1);
                            s8 = ParaConfig.alignmentStrFunc(string.Format("0x{0:X8}", t.Addr), 1);//信息对象地址
                            s9 = ParaConfig.alignmentStrFunc(t.fYcCoe.ToString(), 1);//增加2列：遥测coe
                            s10 = ParaConfig.alignmentStrFunc(t.fYcZone.ToString(), 1);//遥测zone, jifeng, 2018-9-11
                            //----
                            //strTemp = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}\r\n", s1, s2, s3, s4, s5, s6, s7, s8);
                            strTemp = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}\r\n", s1, s2, s3, s4, s5, s6, s7, s8,s9,s10);
                            sw.Write(strTemp);
                            iCount++;
                        }
                        sw.Write("\r\n");

                        sw.Write("[单点遥信表]\r\n");
                        iCount = 0;
                        foreach (var t in fwt.lst_Table_SYX_2.Values)
                        {
                            s1 = ParaConfig.alignmentStrFunc(string.Format("{0}", iCount + 1), 0);
                            s2 = ParaConfig.alignmentStrFunc(t.GroupName, 4);
                            s3 = ParaConfig.alignmentStrFunc(t.DeviceName + "_" + t.ItemName, 5);
                            s4 = ParaConfig.alignmentStrFunc(t.DataType, 1);
                            s5 = ParaConfig.alignmentStrFunc(string.Format("0x{0:X8}", t.Addr), 1);//信息对象地址
                            //----
                            strTemp = string.Format("{0}{1}{2}{3}{4}\r\n", s1, s2, s3, s4, s5);
                            sw.Write(strTemp);
                            iCount++;
                        }
                        sw.Write("\r\n");

                        sw.Write("[双点遥信表]\r\n");
                        iCount = 0;
                        foreach (var t in fwt.lst_Table_DYX_2.Values)
                        {
                            s1 = ParaConfig.alignmentStrFunc(string.Format("{0}", iCount + 1), 0);
                            s2 = ParaConfig.alignmentStrFunc(t.GroupName, 4);
                            s3 = ParaConfig.alignmentStrFunc(t.DeviceName + "_" + t.ItemName, 5);
                            s4 = ParaConfig.alignmentStrFunc(t.DataType, 1);
                            s5 = ParaConfig.alignmentStrFunc(string.Format("0x{0:X8}", t.Addr), 1);//信息对象地址
                            //----
                            strTemp = string.Format("{0}{1}{2}{3}{4}\r\n", s1, s2, s3, s4, s5);
                            sw.Write(strTemp);
                            iCount++;
                        }
                        sw.Write("\r\n");

                        sw.Write("[遥控表]\r\n");
                        iCount = 0;
                        foreach (var t in fwt.lst_Table_YK_2.Values)
                        {
                            s1 = ParaConfig.alignmentStrFunc(string.Format("{0}", iCount + 1), 0);
                            s2 = ParaConfig.alignmentStrFunc(t.GroupName, 4);
                            s3 = ParaConfig.alignmentStrFunc(t.DeviceName + "_" + t.ItemName, 4);
                            s4 = ParaConfig.alignmentStrFunc(t.DataType, 1);
                            s5 = ParaConfig.alignmentStrFunc(string.Format("0x{0:X8}", t.Addr), 1);//信息对象地址
                            //----
                            strTemp = string.Format("{0}{1}{2}{3}{4}\r\n", s1, s2, s3, s4, s5);
                            sw.Write(strTemp);
                            iCount++;
                        }
                        sw.Write("\r\n");

                        sw.Write("[计量值表]\r\n");
                        iCount = 0;
                        foreach (var t in fwt.lst_Table_Meter_2.Values)
                        {
                            s1 = ParaConfig.alignmentStrFunc(string.Format("{0}", iCount + 1), 0);
                            s2 = ParaConfig.alignmentStrFunc(t.GroupName, 4);
                            s3 = ParaConfig.alignmentStrFunc(t.DeviceName + "_" + t.ItemName, 6);
                            s4 = ParaConfig.alignmentStrFunc(t.DataType, 1);
                            s5 = ParaConfig.alignmentStrFunc(t.Unit, 1);
                            s6 = ParaConfig.alignmentStrFunc(string.Format("0x{0:X8}", t.Addr), 1);//信息对象地址
                            //----
                            strTemp = string.Format("{0}{1}{2}{3}{4}{5}\r\n", s1, s2, s3, s4, s5, s6);
                            sw.Write(strTemp);
                            iCount++;
                        }
                        sw.Write("\r\n");
                        break;
                    }
                }

                sw.Write("[端口表]\r\n");
                foreach (var t in Global.g_Model.lst_Table_Port.Values)
                {
                    s1 = ParaConfig.alignmentStrFunc(string.Format("{0}", t.Id), 0);
                    s2 = ParaConfig.alignmentStrFunc(t.PortName, 4);
                    if (t.PortName.Contains("虚拟"))
                    {
                        t.PhysicalAttribute = "虚拟口";
                    }
                    s3 = ParaConfig.alignmentStrFunc(t.PhysicalAttribute, 4);
                    //----
                    strTemp = string.Format("{0}{1}{2}\r\n", s1, s2, s3);
                    sw.Write(strTemp);
                }
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();
            }
        }
        #endregion
        #endregion

        #region "右键菜单_实时数据集(1个)"
        private void tsmi_AfreshGenerateRealData_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #region "右键菜单_端口集(1个)"
        private void tsmi_GeneratePortConfigFile_Click(object sender, EventArgs e)
        {
            string path = System.Environment.CurrentDirectory + @"\Binary";
            SaveFileDialog fd = new SaveFileDialog();
            fd.Title = "生成端口配置文件";
            fd.FileName = "Port";
            //fd.InitialDirectory = path;
            fd.Filter = "二进制文件(*.cfg)|*.cfg";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string strres = "";
                write_Cfg_Port(fd.FileName, ref strres);
                fd.InitialDirectory = fd.FileName.Substring(0, fd.FileName.LastIndexOf("\\") + 1);
                MessageBox.Show(strres);
            }
        }

        bool write_Cfg_Port(string filename, ref string strres)
        {
            try
            {
                FileStream fs = new FileStream(filename, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                //---------
                bw.Write(Convert.ToUInt32(Global.g_Model.lst_Table_Port.Count));//端口数量
                int iCount = 0;
                foreach (var t in Global.g_Model.lst_Table_Port.Values)
                {
                    t.cfg_Port.assignValue_FWT(t.FWTName);
                    t.cfg_Port.assignValue_Device(t.DeviceTableName);
                    //----
                    if (Global.bPrintPort == true)
                    {
                        string ssss = string.Format("port {0}", iCount);
                        formInfo.LogMessage2(ssss);
                    }
                    t.cfg_Port.write_bin(bw);
                    iCount += 1;
                }
                formInfo.LogMessage(string.Format("保存的端口文件[{0}]大小：{1}字节", filename, fs.Length));
                //---------
                bw.Close();
                fs.Close();
                //MessageBox.Show(string.Format("保存[{0}]成功！", filename), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                strres = string.Format("保存[{0}]成功！", filename);
                return true;
            }
            catch (Exception ex)
            {
                strres = string.Format("保存[{0}]失败...失败原因：{1}", filename, ex.Message.ToString());
                //MessageBox.Show(ex.Message.ToString());
                return false;
            }
        }

        void read_Cfg_Port(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);
            formInfo.LogMessage(string.Format("读取的端口文件[{0}]大小：{1}字节", filename, fs.Length));
            try
            {
                UInt32 iPortCount = br.ReadUInt32();//端口数目，暂时不用，jifeng，2017-6-18
                //while (true)
                //{
                foreach (var t in Global.g_Model.lst_Table_Port.Values)
                {
                    t.cfg_Port.read_bin(br);
                    //----
                    if (t.cfg_Port.tPort.u8PhyAttr == 0x01)
                    {
                        t.PhysicalAttribute = "串口";
                    }
                    else if (t.cfg_Port.tPort.u8PhyAttr == 0x02)
                    {
                        t.PhysicalAttribute = "网口";
                    }
                    else if (t.cfg_Port.tPort.u8PhyAttr == 0x03)
                    {
                        t.PhysicalAttribute = "虚拟口";
                    }
                    //----
                    if (t.cfg_Port.tPort.u8LogicAttr < 3 || t.cfg_Port.tPort.u8LogicAttr > 4)
                    {
                        MessageBox.Show(string.Format("逻辑属性数值不正确，应该是3或4，这里读到了{0}", t.cfg_Port.tPort.u8LogicAttr));
                    }
                    t.LogicAttribute = (t.cfg_Port.tPort.u8LogicAttr == 0x03 ? "对上" : "对下");
                    t.ProtocolName = Global.Protocol_Support[((int)(t.cfg_Port.eProtocol))];
                    t.ProtocolInstanceNum = t.cfg_Port.u8ProtocolInstNo;
                    t.Addr = t.cfg_Port.tPort.u16PortAddr;
                    t.Enabled = t.cfg_Port.bUsed;
                    //----
                    t.FWTName = t.cfg_Port.getFWTName();
                    t.DeviceTableName = t.cfg_Port.getDeviceTableName();
                }
                // }
            }
            catch (Exception)
            {
                MessageBox.Show(string.Format("读取[{0}]结束！", filename), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            br.Close();
            fs.Close();
        }
        #endregion
        #endregion

        #region "刷新树节点(Device和FWT子节点)"
        public void refresh_TreeNode_DeviceTableName(CDeviceTable dt)
        {
            foreach (TreeNode t in tn_set_DeviceTable.Nodes)
            {
                int id = Convert.ToInt32(t.Tag);
                if (id == dt.Id)
                {
                    t.Text = dt.DeviceTableName;
                    break;
                }
            }
        }

        public void refresh_TreeNode_DeviceName(CDeviceTable dt, CDevice dev)
        {
            string strOriginName = "";
            foreach (TreeNode t in tn_set_DeviceTable.Nodes)
            {
                if (t.Text == dt.DeviceTableName)
                {
                    foreach (TreeNode t2 in t.Nodes)
                    {
                        int id = Convert.ToInt32(t2.Tag);
                        if (id == dev.Id)
                        {
                            strOriginName = t2.Text;
                            t2.Text = dev.DeviceName;
                            break;
                        }
                    }
                    break;
                }
            }
            //----转发表中的名称，也要同步修改。jifeng，2017-11-21 18：29
            if (strOriginName == "") { return; };
            foreach (var t in Global.g_list_FWT.Values)
            {
                foreach (var t2 in t.lst_Table_YC_1.Values)
                {
                    if (t2.DeviceName == strOriginName) { t2.DeviceName = dev.DeviceName; };
                }
                foreach (var t2 in t.lst_Table_YC_2.Values)
                {
                    if (t2.DeviceName == strOriginName) { t2.DeviceName = dev.DeviceName; };
                }
                //----
                foreach (var t2 in t.lst_Table_SYX_1.Values)
                {
                    if (t2.DeviceName == strOriginName) { t2.DeviceName = dev.DeviceName; };
                }
                foreach (var t2 in t.lst_Table_SYX_2.Values)
                {
                    if (t2.DeviceName == strOriginName) { t2.DeviceName = dev.DeviceName; };
                }
                //----
                foreach (var t2 in t.lst_Table_DYX_1.Values)
                {
                    if (t2.DeviceName == strOriginName) { t2.DeviceName = dev.DeviceName; };
                }
                foreach (var t2 in t.lst_Table_DYX_2.Values)
                {
                    if (t2.DeviceName == strOriginName) { t2.DeviceName = dev.DeviceName; };
                }
                //----
                foreach (var t2 in t.lst_Table_YK_1.Values)
                {
                    if (t2.DeviceName == strOriginName) { t2.DeviceName = dev.DeviceName; };
                }
                foreach (var t2 in t.lst_Table_YK_2.Values)
                {
                    if (t2.DeviceName == strOriginName) { t2.DeviceName = dev.DeviceName; };
                }
                //----
                foreach (var t2 in t.lst_Table_Meter_1.Values)
                {
                    if (t2.DeviceName == strOriginName) { t2.DeviceName = dev.DeviceName; };
                }
                foreach (var t2 in t.lst_Table_Meter_2.Values)
                {
                    if (t2.DeviceName == strOriginName) { t2.DeviceName = dev.DeviceName; };
                }
            }
        }

        public void refresh_TreeNode_FWTName(CFWT fwt)
        {
            foreach (TreeNode t in tn_set_FWT.Nodes)
            {
                int id = Convert.ToInt32(t.Tag);
                if (id == fwt.Id)
                {
                    t.Text = fwt.FWTName;
                    break;
                }
            }
        }
        #endregion

        #region "获取Table对象"
        #region "获取Table对象"
        CTablePara get_Para(string[] sa)
        {
            CTablePara obj = new CTablePara();
            obj.Id = Convert.ToInt32(sa[0]);//序号
            obj.GroupName = sa[1];//组名
            obj.ItemName = sa[2];//条目名
            obj.DataType = sa[3];//数据类型
            obj.Unit = sa[4];//单位
            obj.ValueMax = Convert.ToSingle(sa[5]);//最大值
            obj.ValueMin = Convert.ToSingle(sa[6]);//最小值
            obj.ValueDefault = Convert.ToSingle(sa[7]);//默认值
            obj.Ratio = Convert.ToSingle(sa[8]);//系数
            if (sa[9].Contains("0x") == true || sa[9].Contains("0X") == true)
            {
                obj.Addr = Convert.ToInt32(sa[9], 16);//信息对象地址
            }
            else
            {
                obj.Addr = Convert.ToInt32(sa[9]);
            }
            if (sa[10].Contains("0x") == true || sa[10].Contains("0X") == true)
            {
                obj.BytePos = Convert.ToInt32(sa[10], 16);//字节数组中的位置，jifeng，2017-6-15
            }
            else
            {
                obj.BytePos = Convert.ToInt32(sa[10]);
            }
            //----
            obj.ValueCurrent = obj.ValueDefault;
            obj.strValueCurrent = obj.ValueDefault.ToString();
            return obj;
        }

        CTableSetting get_Setting(string[] sa)
        {
            CTableSetting obj = new CTableSetting();
            obj.Id = Convert.ToInt32(sa[0]);//序号
            obj.GroupName = sa[1];//组名
            obj.ItemName = sa[2];//条目名
            obj.DataType = sa[3];//数据类型
            obj.Unit = sa[4];//单位
            obj.ValueMax = Convert.ToSingle(sa[5]);//最大值
            obj.ValueMin = Convert.ToSingle(sa[6]);//最小值
            obj.ValueDefault = Convert.ToSingle(sa[7]);//默认值
            obj.Ratio = Convert.ToSingle(sa[8]);//系数
            if (sa[9].Contains("0x") == true || sa[9].Contains("0X") == true)
            {
                obj.Addr = Convert.ToInt32(sa[9], 16);//信息对象地址
            }
            else
            {
                obj.Addr = Convert.ToInt32(sa[9]);
            }
            if (sa[10].Contains("0x") == true || sa[10].Contains("0X") == true)
            {
                obj.BytePos = Convert.ToInt32(sa[10], 16);//字节数组中的位置，jifeng，2017-6-15
            }
            else
            {
                obj.BytePos = Convert.ToInt32(sa[10]);
            }
            //----
            obj.ValueCurrent = obj.ValueDefault;
            obj.strValueCurrent = obj.ValueDefault.ToString();
            return obj;
        }

        CTableYC get_YC(string[] sa)
        {
            CTableYC obj = new CTableYC();
            obj.Id = Convert.ToInt32(sa[0]);//序号
            obj.GroupName = sa[1];//组名
            obj.ItemName = sa[2];//条目名
            obj.DataType = sa[3];//数据类型
            obj.Unit = sa[4];//单位
            obj.Ratio = Convert.ToInt32(sa[5]);//系数
            obj.iGroup = Convert.ToInt32(sa[6]);//分组
            //----南网需求：遥测死区、遥测调整系数
            obj.setYcZone(obj.iGroup);
            obj.setYcCoe(obj.iGroup, 2);//默认：浮点数
            //----
            if (sa[7].Contains("0x") == true || sa[7].Contains("0X") == true)
            {
                obj.Addr = Convert.ToInt32(sa[7], 16);//信息对象地址
            }
            else
            {
                obj.Addr = Convert.ToInt32(sa[7]);
            }
            if(sa.Length == 10)
            {//新增了2列：遥测coe 和 遥测zone，给 调试工具 使用(只有转发表模板，会有)，为了向下兼容，这里判断sa的length是否等于10.jifeng，2018-9-11 15:48
                obj.fYcCoe = Convert.ToSingle(sa[8]);
                obj.fYcZone = Convert.ToSingle(sa[9]);
            }
            return obj;
        }

        CTableSYX get_SYX(string[] sa)
        {
            CTableSYX obj = new CTableSYX();
            obj.Id = Convert.ToInt32(sa[0]);//序号
            obj.GroupName = sa[1];//组名
            obj.ItemName = sa[2];//条目名
            obj.DataType = sa[3];//数据类型
            if (sa[4].Contains("0x") == true || sa[4].Contains("0X") == true)
            {
                obj.Addr = Convert.ToInt32(sa[4], 16);//信息对象地址
            }
            else
            {
                obj.Addr = Convert.ToInt32(sa[4]);
            }
            return obj;
        }
        CTableGYX get_GYX(string[] sa)
        {
            CTableGYX obj = new CTableGYX();
            obj.Id = Convert.ToInt32(sa[0]);//序号
            obj.ItemName = sa[1];//条目名
            return obj;
        }

        CTableDYX get_DYX(string[] sa)
        {
            CTableDYX obj = new CTableDYX();
            obj.Id = Convert.ToInt32(sa[0]);//序号
            obj.GroupName = sa[1];//组名
            obj.ItemName = sa[2];//条目名
            obj.DataType = sa[3];//数据类型
            if (sa[4].Contains("0x") == true || sa[4].Contains("0X") == true)
            {
                obj.Addr = Convert.ToInt32(sa[4], 16);//信息对象地址
            }
            else
            {
                obj.Addr = Convert.ToInt32(sa[4]);
            }
            return obj;
        }

        CTableYK get_YK(string[] sa)
        {
            CTableYK obj = new CTableYK();
            obj.Id = Convert.ToInt32(sa[0]);//序号
            obj.GroupName = sa[1];//组名
            obj.ItemName = sa[2];//条目名
            obj.DataType = sa[3];//数据类型
            if (sa[4].Contains("0x") == true || sa[4].Contains("0X") == true)
            {
                obj.Addr = Convert.ToInt32(sa[4], 16);//信息对象地址
            }
            else
            {
                obj.Addr = Convert.ToInt32(sa[4]);
            }
            return obj;
        }

        CTableMeter get_Meter(string[] sa)
        {
            CTableMeter obj = new CTableMeter();
            obj.Id = Convert.ToInt32(sa[0]);//序号
            obj.GroupName = sa[1];//组名
            obj.ItemName = sa[2];//条目名
            obj.DataType = sa[3];//数据类型
            obj.Unit = sa[4];//单位
            if (sa[5].Contains("0x") == true || sa[5].Contains("0X") == true)
            {
                obj.Addr = Convert.ToInt32(sa[5], 16);//信息对象地址
            }
            else
            {
                obj.Addr = Convert.ToInt32(sa[5]);
            }
            return obj;
        }

        CTablePort get_Port(string[] sa)
        {
            CTablePort obj = new CTablePort();
            obj.Id = Convert.ToInt32(sa[0]);//序号
            obj.PortName = sa[1];//端口名
            obj.PhysicalAttribute = sa[2];//端口属性
            obj.Addr = 1/*obj.Id*/;//jifeng， 2018-6-4 17:31
            obj.setAttribute();//设置属性
            return obj;
        }

        CDevice get_Device(string[] sa)
        {
            CDevice obj = new CDevice();
            obj.Id = Convert.ToInt32(sa[0]);//序号
            CDevice.Accu += 1;
            obj.DeviceName = sa[1];//设备名称
            obj.ModelName = sa[2];//模板名称
            obj.CommAddr = Convert.ToInt32(sa[3]);//通信地址
            return obj;
        }

        CFWT get_FWT(string[] sa)
        {
            CFWT obj = new CFWT();
            obj.Id = Convert.ToInt32(sa[0]);
            CFWT.Accu += 1;
            obj.FWTName = sa[1];
            return obj;
        }
        #endregion

        //比上一个要多5个字段，主要是转发表需要使用到
        #region "获取Table对象_2"
        CTableYC get_YC_2(string[] sa)
        {
            CTableYC obj = new CTableYC();
            obj.Id = Convert.ToInt32(sa[0]);//序号
            obj.GroupName = sa[1];//组名
            obj.ItemName = sa[2];//条目名
            obj.DataType = sa[3];//数据类型
            obj.Unit = sa[4];//单位
            obj.Ratio = Convert.ToInt32(sa[5]);//系数
            obj.iGroup = Convert.ToInt32(sa[6]);//分组
            if (sa[7].Contains("0x") == true || sa[7].Contains("0X") == true)
            {
                obj.Addr = Convert.ToInt32(sa[7], 16);//信息对象地址
            }
            else
            {
                obj.Addr = Convert.ToInt32(sa[7]);
            }
            //--------新增5个：t.SN, t.DeviceName, t.ModelName, t.Group, t.FlagDelete
            obj.SN = Convert.ToInt32(sa[8]);
            obj.DeviceName = sa[9];
            obj.ModelName = sa[10];
            obj.Group = Convert.ToInt32(sa[11]);
            obj.FlagDelete = Convert.ToBoolean(sa[12]);
            //--------新增2个：t.fYcZone，t.fYcCoe
            obj.fYcZone = Convert.ToSingle(sa[13]);
            obj.fYcCoe = Convert.ToSingle(sa[14]);
            return obj;
        }

        CTableSYX get_SYX_2(string[] sa)
        {
            CTableSYX obj = new CTableSYX();
            obj.Id = Convert.ToInt32(sa[0]);//序号
            obj.GroupName = sa[1];//组名
            obj.ItemName = sa[2];//条目名
            obj.DataType = sa[3];//数据类型
            if (sa[4].Contains("0x") == true || sa[4].Contains("0X") == true)
            {
                obj.Addr = Convert.ToInt32(sa[4], 16);//信息对象地址
            }
            else
            {
                obj.Addr = Convert.ToInt32(sa[4]);
            }
            //--------新增5个：t.SN, t.DeviceName, t.ModelName, t.Group, t.FlagDelete
            obj.SN = Convert.ToInt32(sa[5]);
            obj.DeviceName = sa[6];
            obj.ModelName = sa[7];
            obj.Group = Convert.ToInt32(sa[8]);
            obj.FlagDelete = Convert.ToBoolean(sa[9]);
            return obj;
        }

        CTableDYX get_DYX_2(string[] sa)
        {
            CTableDYX obj = new CTableDYX();
            obj.Id = Convert.ToInt32(sa[0]);//序号
            obj.GroupName = sa[1];//组名
            obj.ItemName = sa[2];//条目名
            obj.DataType = sa[3];//数据类型
            if (sa[4].Contains("0x") == true || sa[4].Contains("0X") == true)
            {
                obj.Addr = Convert.ToInt32(sa[4], 16);//信息对象地址
            }
            else
            {
                obj.Addr = Convert.ToInt32(sa[4]);
            }
            //--------新增5个：t.SN, t.DeviceName, t.ModelName, t.Group, t.FlagDelete
            obj.SN = Convert.ToInt32(sa[5]);
            obj.DeviceName = sa[6];
            obj.ModelName = sa[7];
            obj.Group = Convert.ToInt32(sa[8]);
            obj.FlagDelete = Convert.ToBoolean(sa[9]);
            return obj;
        }

        CTableYK get_YK_2(string[] sa)
        {
            CTableYK obj = new CTableYK();
            obj.Id = Convert.ToInt32(sa[0]);//序号
            obj.GroupName = sa[1];//组名
            obj.ItemName = sa[2];//条目名
            obj.DataType = sa[3];//数据类型
            if (sa[4].Contains("0x") == true || sa[4].Contains("0X") == true)
            {
                obj.Addr = Convert.ToInt32(sa[4], 16);//信息对象地址
            }
            else
            {
                obj.Addr = Convert.ToInt32(sa[4]);
            }
            //--------新增5个：t.SN, t.DeviceName, t.ModelName, t.Group, t.FlagDelete
            obj.SN = Convert.ToInt32(sa[5]);
            obj.DeviceName = sa[6];
            obj.ModelName = sa[7];
            obj.Group = Convert.ToInt32(sa[8]);
            obj.FlagDelete = Convert.ToBoolean(sa[9]);
            return obj;
        }

        CTableMeter get_Meter_2(string[] sa)
        {
            CTableMeter obj = new CTableMeter();
            obj.Id = Convert.ToInt32(sa[0]);//序号
            obj.GroupName = sa[1];//组名
            obj.ItemName = sa[2];//条目名
            obj.DataType = sa[3];//数据类型
            obj.Unit = sa[4];//单位
            //obj.Ratio = Convert.ToInt32(sa[5]);//系数
            if (sa[5].Contains("0x") == true || sa[5].Contains("0X") == true)
            {
                obj.Addr = Convert.ToInt32(sa[5], 16);//信息对象地址
            }
            else
            {
                obj.Addr = Convert.ToInt32(sa[5]);
            }
            //--------新增5个：t.SN, t.DeviceName, t.ModelName, t.Group, t.FlagDelete
            obj.SN = Convert.ToInt32(sa[6]);
            obj.DeviceName = sa[7];
            obj.ModelName = sa[8];
            obj.Group = Convert.ToInt32(sa[9]);
            obj.FlagDelete = Convert.ToBoolean(sa[10]);
            return obj;
        }
        #endregion
        #endregion
        ///////////////////////////////////////////////////
        #region "初始化"
        void init()
        {
            //-------------------------
            tn_set_Para.Nodes.Clear();
            tn_set_Setting.Nodes.Clear();
            tn_set_Model.Nodes.Clear();
            tn_set_DeviceTable.Nodes.Clear();
            tn_set_RTDB.Nodes.Clear();
            tn_set_FWT.Nodes.Clear();
            tn_set_Port.Nodes.Clear();
            //-------------------------
            Global.g_Model.init();
            //-------------------------
            CModel.Accu = 1;
            Global.g_list_Model.Clear();
            CDevice.Accu = 1;
            CDeviceTable.Accu = 1;
            Global.g_list_DeviceTable.Clear();
            CFWT.Accu = 1;
            Global.g_list_FWT.Clear();
            Global.g_list_FWT_Import.Clear();
            //-------------------------
        }
        #endregion
        ///////////////////////////////////////////////////
        #region "树形控件AfterSelect"
        private void tvNavi_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode tn = tvNavi.SelectedNode;
            TreeNode tnParent = tn.Parent;
            if (tnParent == null)
            {
                tssl_Tip.Text = "";
                return;
            }
            if (tnParent.Text == Global.cst_Set_Para)
            {
                tssl_Tip.Text = "[参数集界面]Alt+A: 当前值列，焦点单元格下方，全选";
            }
            else
            {
                tssl_Tip.Text = "";
            }
        }
        #endregion
        //////////////////////////////////////////////////
        #region "写GB2312中文字符串"
        public void writeChineseString(string chinese, BinaryWriter bw)
        {
            Byte[] encodedBytes = Encoding.GetEncoding("gb2312").GetBytes(chinese);
            byte bt = 0x00;
            for (int k = 0; k < iSize; k++)
            {
                if ((k + 1) <= encodedBytes.Length)
                {
                    bw.Write(encodedBytes[k]);
                }
                else
                {
                    bw.Write(bt);
                }
            }
        }
        #endregion
        #region "读GB2312中文字符串"
        public string readChineseString(BinaryReader br)
        {
            for (int k = 0; k < bName.Length; k++) { bName[k] = br.ReadByte(); }
            return Encoding.GetEncoding("gb2312").GetString(bName).Replace("\0", "");
        }
        #endregion
        //////////////////////////////////////////////////
        #region "一键保存所有Cfg配置"
        private void tsmi_SaveAll_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择文件夹";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    MessageBox.Show("文件夹路径不能为空", "提示");
                    return;
                }
                //Action<string> a;
                //a.BeginInvoke(dialog.SelectedPath, asyncCallback, a);
                string strBasePath = dialog.SelectedPath;
                //----
                string strPara = "\\Para.cfg";
                string strModel = "\\Model.cfg";
                string strDev = "\\Dev.cfg";
                string strFwt = "\\Fwt.cfg";
                string strPort = "\\Port.cfg";
                string strYK = "\\YK.cfg";
                //----
                string strres1 = "";
                string strres2 = "";
                string strres3 = "";
                string strres4 = "";
                string strres5 = "";
                string strres6 = "";
                bool b1 = write_Cfg_Para_Setting(strBasePath + strPara, ref strres1);
                bool b2 = write_Cfg_Model(strBasePath + strModel, ref strres2);
                bool b3 = write_Cfg_Device(strBasePath + strDev, ref strres3);
                bool b4 = write_Cfg_FWT(strBasePath + strFwt, ref strres4);
                bool b5 = write_Cfg_Port(strBasePath + strPort, ref strres5);
                bool b6 = write_Cfg_YK(strBasePath + strYK, ref strres6);
                string str1 = "";
                str1 += strres1 + "\r\n";
                str1 += strres2 + "\r\n";
                str1 += strres3 + "\r\n";
                str1 += strres4 + "\r\n";
                str1 += strres5 + "\r\n";
                str1 += strres6 + "\r\n";
                MessageBox.Show(str1);
            }
        }

        private void SaveCfg(string SavePath)
        {
            string strBasePath = SavePath;
            //----
            string strPara = "\\Para.cfg";
            string strModel = "\\Model.cfg";
            string strDev = "\\Dev.cfg";
            string strFwt = "\\Fwt.cfg";
            string strPort = "\\Port.cfg";
            string strYK = "\\YK.cfg";
            //----
            string strres1 = "";
            string strres2 = "";
            string strres3 = "";
            string strres4 = "";
            string strres5 = "";
            string strres6 = "";
            bool b1 = write_Cfg_Para_Setting(strBasePath + strPara, ref strres1);
            bool b2 = write_Cfg_Model(strBasePath + strModel, ref strres2);
            bool b3 = write_Cfg_Device(strBasePath + strDev, ref strres3);
            bool b4 = write_Cfg_FWT(strBasePath + strFwt, ref strres4);
            bool b5 = write_Cfg_Port(strBasePath + strPort, ref strres5);
            bool b6 = write_Cfg_YK(strBasePath + strYK, ref strres6);
            string str1 = "";
            str1 += strres1 + "\r\n";
            str1 += strres2 + "\r\n";
            str1 += strres3 + "\r\n";
            str1 += strres4 + "\r\n";
            str1 += strres5 + "\r\n";
            str1 += strres6 + "\r\n";
            MessageBox.Show(str1);
        }

        bool write_Cfg_YK(string filename, ref string strres)
        {
            try
            {
                FileStream fs = new FileStream(filename, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                //---------
                bw.Write(Convert.ToByte(Global.g_bSoftYk_1));
                bw.Write(Convert.ToByte(Global.g_bSoftYk_2));

                formInfo.LogMessage(string.Format("保存的YK文件[{0}]大小：{1}字节", filename, fs.Length));
                //---------
                bw.Close();
                fs.Close();
                //MessageBox.Show(string.Format("保存[{0}]成功！", filename), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                strres = string.Format("保存[{0}]成功！", filename);
                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
                strres = string.Format("保存[{0}]失败...失败原因：{1}", filename, ex.Message.ToString());
                return false;
            }
        }
        #endregion

        #region "保存前的检查"
        bool Check_Fwt()
        {
            bool bres = false;
            string str = "";

            SortedList<int, string> lst_Temp = new SortedList<int, string>();
            foreach (var obj in Global.g_list_FWT.Values)
            {
                lst_Temp.Clear();
                foreach (var v in obj.lst_Table_YC_2.Values)
                {
                    if (lst_Temp.ContainsKey(v.Addr) == false)
                    {
                        lst_Temp.Add(v.Addr, "");
                    }
                    else
                    {
                        //重复key
                        str = string.Format("{0}, 遥测, 地址重复：{1}, 0x{2:X}", obj.FWTName, v.ItemName, v.Addr);
                        formInfo.LogError(str);
                        bres = true;
                    }
                }

                lst_Temp.Clear();
                foreach (var v in obj.lst_Table_SYX_2.Values)
                {
                    if (lst_Temp.ContainsKey(v.Addr) == false)
                    {
                        lst_Temp.Add(v.Addr, "");
                    }
                    else
                    {
                        //重复key
                        str = string.Format("{0}, 单点遥信, 地址重复：{1}, 0x{2:X}", obj.FWTName, v.ItemName, v.Addr);
                        formInfo.LogError(str);
                        bres = true;
                    }
                }

                lst_Temp.Clear();
                foreach (var v in obj.lst_Table_DYX_2.Values)
                {
                    if (lst_Temp.ContainsKey(v.Addr) == false)
                    {
                        lst_Temp.Add(v.Addr, "");
                    }
                    else
                    {
                        //重复key
                        str = string.Format("{0}, 双点遥信, 地址重复：{1}, 0x{2:X}", obj.FWTName, v.ItemName, v.Addr);
                        formInfo.LogError(str);
                        bres = true;
                    }
                }

                lst_Temp.Clear();
                foreach (var v in obj.lst_Table_YK_2.Values)
                {
                    if (lst_Temp.ContainsKey(v.Addr) == false)
                    {
                        lst_Temp.Add(v.Addr, "");
                    }
                    else
                    {
                        //重复key
                        str = string.Format("{0}, 遥控, 地址重复：{1}, 0x{2:X}", obj.FWTName, v.ItemName, v.Addr);
                        formInfo.LogError(str);
                        bres = true;
                    }
                }

                lst_Temp.Clear();
                foreach (var v in obj.lst_Table_Meter_2.Values)
                {
                    if (lst_Temp.ContainsKey(v.Addr) == false)
                    {
                        lst_Temp.Add(v.Addr, "");
                    }
                    else
                    {
                        //重复key
                        str = string.Format("{0}, 计量值, 地址重复：{1}, 0x{2:X}", obj.FWTName, v.ItemName, v.Addr);
                        formInfo.LogError(str);
                        bres = true;
                    }
                }
            }

            return bres;
        }
        #endregion

        #region "自动配置"
        bool Generate_Server()
        {
            string strTemp = "";
            int m = 0;
            //string strBaseFolder = "D:\\CfgFile\\";
            //string strBaseFolderServer = "D:\\CfgFile\\Server\\";
            //string strBaseFolderClient= "D:\\CfgFile\\Client\\";
            if (false == Directory.Exists(strBaseFolder)) { Directory.CreateDirectory(strBaseFolder); }
            if (false == Directory.Exists(strBaseFolderServer)) { Directory.CreateDirectory(strBaseFolderServer); }
            if (false == Directory.Exists(strBaseFolderClient)) { Directory.CreateDirectory(strBaseFolderClient); }
            //List<string> listFile = new List<string>();
            listFile.Clear();
            DirectoryInfo TheFolder = new DirectoryInfo(strBaseFolder);
            foreach (FileInfo NextFile in TheFolder.GetFiles("*.txt"))
            {
                //string s = NextFile.FullName;
                listFile.Add(NextFile.Name);
            }
            if(listFile.Count == 0)
            {
                strTemp = string.Format("{0} 下没有找到后缀为txt的模板文件！", strBaseFolder);
                MessageBox.Show(strTemp, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start("explorer.exe", strBaseFolder);
                return false;
            }
            else if (listFile.Count > 1)
            {
                strTemp = string.Format("{0} 下有{1}个后缀为txt的模板文件: \r\n", strBaseFolder, listFile.Count);
                for (m = 0; m < listFile.Count; m++)
                {
                    strTemp += listFile[m] + "\r\n";
                }
                MessageBox.Show(strTemp, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                System.Diagnostics.Process.Start("explorer.exe", strBaseFolder);
                return false;
            }
            strModelPath = string.Format("{0}{1}", strBaseFolder, listFile[0]);
            //因为文件比较大，所有使用StreamReader的效率要比使用File.ReadLines高
            Global.g_Model.init();
            Global.g_Model.ModelName = listFile[0].Substring(0, listFile[0].IndexOf(".", 0));//模板文件名称
            short iSet = 0;
            using (StreamReader sr = new StreamReader(strModelPath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    string readStr = sr.ReadLine();
                    readStr.Trim();
                    if (readStr == "") { iSet = 0; continue; }
                    if (readStr.Contains(Global.cst_Table_Para) == true)
                    {
                        iSet = CST_SET.Index_Para;
                    }
                    else if (readStr.Contains(Global.cst_Table_Setting) == true)
                    {
                        iSet = CST_SET.Index_Setting;
                    }
                    else if (readStr.Contains(Global.cst_Table_YC) == true)
                    {
                        iSet = CST_SET.Index_YC;
                    }
                    else if (readStr.Contains(Global.cst_Table_SYX) == true)
                    {
                        iSet = CST_SET.Index_SYX;
                    }
                    else if (readStr.Contains(Global.cst_Table_DYX) == true)
                    {
                        iSet = CST_SET.Index_DYX;
                    }
                    else if (readStr.Contains(Global.cst_Table_YK) == true)
                    {
                        iSet = CST_SET.Index_YK;
                    }
                    else if (readStr.Contains(Global.cst_Table_Meter) == true)
                    {
                        iSet = CST_SET.Index_Meter;
                    }
                    else if (readStr.Contains(Global.cst_Table_Port) == true)
                    {
                        iSet = CST_SET.Index_Port;
                    }
                    if (iSet == CST_SET.Index_Para) //参数表
                    {
                        if (readStr.Contains("//") == false && readStr.Contains(Global.cst_Table_Para) == false)
                        {
                            string[] sa = readStr.Split(Global.g_SplitChar, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                            CTablePara obj = get_Para(sa);
                            Global.g_Model.lst_Table_Para.Add(obj.Id, obj);
                        }
                    }
                    else if (iSet == CST_SET.Index_Setting) //定值表
                    {
                        if (readStr.Contains("//") == false && readStr.Contains(Global.cst_Table_Setting) == false)
                        {
                            string[] sa = readStr.Split(Global.g_SplitChar, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                            CTableSetting obj = get_Setting(sa);
                            Global.g_Model.lst_Table_Setting.Add(obj.Id, obj);
                        }
                    }
                    else if (iSet == CST_SET.Index_YC) //遥测表
                    {
                        if (readStr.Contains("//") == false && readStr.Contains(Global.cst_Table_YC) == false)
                        {
                            string[] sa = readStr.Split(Global.g_SplitChar, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                            CTableYC obj = get_YC(sa);
                            Global.g_Model.lst_Table_YC.Add(obj.Id, obj);
                        }
                    }
                    else if (iSet == CST_SET.Index_SYX) //单点遥信表
                    {
                        if (readStr.Contains("//") == false && readStr.Contains(Global.cst_Table_SYX) == false)
                        {
                            string[] sa = readStr.Split(Global.g_SplitChar, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                            CTableSYX obj = get_SYX(sa);
                            Global.g_Model.lst_Table_SYX.Add(obj.Id, obj);
                        }
                    }
                    else if (iSet == CST_SET.Index_DYX) //双点遥信表
                    {
                        if (readStr.Contains("//") == false && readStr.Contains(Global.cst_Table_DYX) == false)
                        {
                            string[] sa = readStr.Split(Global.g_SplitChar, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                            CTableDYX obj = get_DYX(sa);
                            Global.g_Model.lst_Table_DYX.Add(obj.Id, obj);
                        }
                    }
                    else if (iSet == CST_SET.Index_YK) //遥控表
                    {
                        if (readStr.Contains("//") == false && readStr.Contains(Global.cst_Table_YK) == false)
                        {
                            string[] sa = readStr.Split(Global.g_SplitChar, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                            CTableYK obj = get_YK(sa);
                            Global.g_Model.lst_Table_YK.Add(obj.Id, obj);
                        }
                    }
                    else if (iSet == CST_SET.Index_Meter) //计量值表
                    {
                        if (readStr.Contains("//") == false && readStr.Contains(Global.cst_Table_Meter) == false)
                        {
                            string[] sa = readStr.Split(Global.g_SplitChar, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                            CTableMeter obj = get_Meter(sa);
                            Global.g_Model.lst_Table_Meter.Add(obj.Id, obj);
                        }
                    }
                    else if (iSet == CST_SET.Index_Port) //端口表
                    {
                        if (readStr.Contains("//") == false && readStr.Contains(Global.cst_Table_Port) == false)
                        {
                            string[] sa = readStr.Split(Global.g_SplitChar, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                            CTablePort obj = get_Port(sa);
                            Global.g_Model.lst_Table_Port.Add(obj.Id, obj);
                        }
                    }
                }
            }

            //设备表集(不需要)

            //转发表集
            CFWT f = new CFWT();
            f.Id = CFWT.Accu;
            CFWT.Accu += 1;
            f.FWTName = "Fwt_1";
            f.sourceClone_AutoConfig();
            Global.g_list_FWT.Add(f.Id, f);
            Global.sorted_list_FWT();

            int iComCheckedCnt = 0;
            for (int iCom = 0; iCom < gbComChecked.Length; iCom++)
            {
                if (gbComChecked[iCom] == true)
                {
                    CFWT f2 = new CFWT();
                    f2.Id = CFWT.Accu;
                    CFWT.Accu += 1;
                    f2.FWTName = string.Format("Fwt_{0}", iComCheckedCnt + 2);
                    f2.sourceClone_AutoConfig();
                    Global.g_list_FWT.Add(f2.Id, f2);
                    Global.sorted_list_FWT();

                    iComCheckedCnt += 1;
                }
            }

            //端口集
            //if (giComId >= 2 && giComId  <= 5)
            //{
            //    m = giComId;
            //    Global.g_Model.lst_Table_Port[m].Enabled = true;
            //    Global.g_Model.lst_Table_Port[m].Addr = 1;
            //    Global.g_Model.lst_Table_Port[m].FWTName = "Fwt_1";
            //    Global.g_Model.lst_Table_Port[m].cfg_Port.bUsed = true;
            //    Global.g_Model.lst_Table_Port[m].cfg_Port.u32FwtIndex = 0;
            //}
            iComCheckedCnt = 0;
            for (int iCom = 0; iCom < gbComChecked.Length; iCom++)
            {//0,1,2,3 -> 2,3,4,5
                if (gbComChecked[iCom] == true)
                {
                    Global.g_Model.lst_Table_Port[iCom + 2].Enabled = true;
                    Global.g_Model.lst_Table_Port[iCom + 2].Addr = 1;
                    Global.g_Model.lst_Table_Port[iCom + 2].FWTName = string.Format("Fwt_{0}", iComCheckedCnt + 1);
                    Global.g_Model.lst_Table_Port[iCom + 2].cfg_Port.bUsed = true;
                    Global.g_Model.lst_Table_Port[iCom + 2].cfg_Port.u32FwtIndex = 0;

                    Global.g_Model.lst_Table_Port[iCom + 2].cfg_Port.u8ProtocolInstNo = (byte)iComCheckedCnt;
                    Global.g_Model.lst_Table_Port[iCom + 2].cfg_Port.tCommB101UpCfg.tLinkCfg.u8Mode = (byte)(giComType[iCom] + 1);

                    iComCheckedCnt += 1;
                }
            }

            m = 6;
            Global.g_Model.lst_Table_Port[m].cfg_Port.tPort.NPort.u8IP[0] = 192;
            Global.g_Model.lst_Table_Port[m].cfg_Port.tPort.NPort.u8IP[1] = 168;
            Global.g_Model.lst_Table_Port[m].cfg_Port.tPort.NPort.u8IP[2] = 253;
            Global.g_Model.lst_Table_Port[m].cfg_Port.tPort.NPort.u8IP[3] = 100;
            Global.g_Model.lst_Table_Port[m].cfg_Port.tCommB104UpCfg.tLinkCfg.u8ServerIP[0] = 192;
            Global.g_Model.lst_Table_Port[m].cfg_Port.tCommB104UpCfg.tLinkCfg.u8ServerIP[1] = 168;
            Global.g_Model.lst_Table_Port[m].cfg_Port.tCommB104UpCfg.tLinkCfg.u8ServerIP[2] = 253;
            Global.g_Model.lst_Table_Port[m].cfg_Port.tCommB104UpCfg.tLinkCfg.u8ServerIP[3] = 100;
            Global.g_Model.lst_Table_Port[m].cfg_Port.tCommB104UpCfg.tLinkCfg.u8ClientIP[0] = 255;
            Global.g_Model.lst_Table_Port[m].cfg_Port.tCommB104UpCfg.tLinkCfg.u8ClientIP[1] = 255;
            Global.g_Model.lst_Table_Port[m].cfg_Port.tCommB104UpCfg.tLinkCfg.u8ClientIP[2] = 255;
            Global.g_Model.lst_Table_Port[m].cfg_Port.tCommB104UpCfg.tLinkCfg.u8ClientIP[3] = 255;
            if (giNetId >= 7 && giNetId <= 9)
            {
                m = giNetId;
                Global.g_Model.lst_Table_Port[m].Enabled = true;
                Global.g_Model.lst_Table_Port[m].Addr = 1;
                Global.g_Model.lst_Table_Port[m].FWTName = string.Format("Fwt_{0}", iComCheckedCnt + 1);
                Global.g_Model.lst_Table_Port[m].cfg_Port.bUsed = true;
                Global.g_Model.lst_Table_Port[m].cfg_Port.u32FwtIndex = 1;
                Global.g_Model.lst_Table_Port[m].cfg_Port.tPort.NPort.u8IP[0] = gbytServerIP[0];
                Global.g_Model.lst_Table_Port[m].cfg_Port.tPort.NPort.u8IP[1] = gbytServerIP[1];
                Global.g_Model.lst_Table_Port[m].cfg_Port.tPort.NPort.u8IP[2] = gbytServerIP[2];
                Global.g_Model.lst_Table_Port[m].cfg_Port.tPort.NPort.u8IP[3] = gbytServerIP[3];
                Global.g_Model.lst_Table_Port[m].cfg_Port.tCommB104UpCfg.tLinkCfg.u8ServerIP[0] = Global.g_Model.lst_Table_Port[m].cfg_Port.tPort.NPort.u8IP[0];
                Global.g_Model.lst_Table_Port[m].cfg_Port.tCommB104UpCfg.tLinkCfg.u8ServerIP[1] = Global.g_Model.lst_Table_Port[m].cfg_Port.tPort.NPort.u8IP[1];
                Global.g_Model.lst_Table_Port[m].cfg_Port.tCommB104UpCfg.tLinkCfg.u8ServerIP[2] = Global.g_Model.lst_Table_Port[m].cfg_Port.tPort.NPort.u8IP[2];
                Global.g_Model.lst_Table_Port[m].cfg_Port.tCommB104UpCfg.tLinkCfg.u8ServerIP[3] = Global.g_Model.lst_Table_Port[m].cfg_Port.tPort.NPort.u8IP[3];
                Global.g_Model.lst_Table_Port[m].cfg_Port.tCommB104UpCfg.tLinkCfg.u8ClientIP[0] = gbytClientIP[0];
                Global.g_Model.lst_Table_Port[m].cfg_Port.tCommB104UpCfg.tLinkCfg.u8ClientIP[1] = gbytClientIP[1];
                Global.g_Model.lst_Table_Port[m].cfg_Port.tCommB104UpCfg.tLinkCfg.u8ClientIP[2] = gbytClientIP[2];
                Global.g_Model.lst_Table_Port[m].cfg_Port.tCommB104UpCfg.tLinkCfg.u8ClientIP[3] = gbytClientIP[3];
            }
            DateTime dt = DateTime.Now;
            string strFileName = string.Format("cfg_{0:D4}{1:D2}{2:D2}.prj", dt.Year, dt.Month, dt.Day);
            string strServerIP = "";
            string strClientIP = "";
            strServerIP = string.Format("Server {0}.{1}.{2}.{3}", gbytServerIP[0], gbytServerIP[1], gbytServerIP[2], gbytServerIP[3]);
            strClientIP = string.Format("Client {0}.{1}.{2}.{3}", gbytClientIP[0], gbytClientIP[1], gbytClientIP[2], gbytClientIP[3]);
            strTemp = string.Format("{0}{1:D4}{2:D2}{3:D2}_{4:D2}{5:D2}{6:D2}({7},{8})_ByCfgTool//", 
                strBaseFolderServer,dt.Year,dt.Month,dt.Day,dt.Hour,dt.Minute,dt.Second,
                strServerIP,strClientIP);

            if (false == Directory.Exists(strTemp)) { Directory.CreateDirectory(strTemp); }
            write_prj(strTemp + strFileName);
            SaveCfg(strTemp);

            return true;
            //--------
        }

        void Generate_Client()
        {
            string strTemp = "";
            int m = 0;

            CModel mdl = new CModel();
            mdl.init();
            strTemp = string.Format("{0}_fwt", listFile[0].Substring(0, listFile[0].IndexOf(".", 0)));
            mdl.ModelName = strTemp;//模板文件名称
            short iSet = 0;
            using (StreamReader sr = new StreamReader(strModelPath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    string readStr = sr.ReadLine();
                    readStr.Trim(); readStr.TrimStart(); readStr.TrimEnd();
                    if (readStr == "") { iSet = 0; continue; }
                    if (readStr.Contains(Global.cst_Table_Para) == true)
                    {
                        iSet = CST_SET.Index_Para;
                    }
                    else if (readStr.Contains(Global.cst_Table_Setting) == true)
                    {
                        iSet = CST_SET.Index_Setting;
                    }
                    else if (readStr.Contains(Global.cst_Table_YC) == true)
                    {
                        iSet = CST_SET.Index_YC;
                    }
                    else if (readStr.Contains(Global.cst_Table_SYX) == true)
                    {
                        iSet = CST_SET.Index_SYX;
                    }
                    else if (readStr.Contains(Global.cst_Table_DYX) == true)
                    {
                        iSet = CST_SET.Index_DYX;
                    }
                    else if (readStr.Contains(Global.cst_Table_YK) == true)
                    {
                        iSet = CST_SET.Index_YK;
                    }
                    else if (readStr.Contains(Global.cst_Table_Meter) == true)
                    {
                        iSet = CST_SET.Index_Meter;
                    }
                    else if (readStr.Contains(Global.cst_Table_Port) == true)
                    {
                        iSet = CST_SET.Index_Port;
                    }
                    if (iSet == CST_SET.Index_Para) //参数表
                    {
                        if (readStr.Contains("//") == false && readStr.Contains(Global.cst_Table_Para) == false)
                        {
                            string[] sa = readStr.Split(Global.g_SplitChar, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                            CTablePara obj = get_Para(sa);
                            mdl.lst_Table_Para.Add(obj.Id, obj);
                        }
                    }
                    else if (iSet == CST_SET.Index_Setting) //定值表
                    {
                        if (readStr.Contains("//") == false && readStr.Contains(Global.cst_Table_Setting) == false)
                        {
                            string[] sa = readStr.Split(Global.g_SplitChar, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                            CTableSetting obj = get_Setting(sa);
                            mdl.lst_Table_Setting.Add(obj.Id, obj);
                        }
                    }
                    else if (iSet == CST_SET.Index_YC) //遥测表
                    {
                        if (readStr.Contains("//") == false && readStr.Contains(Global.cst_Table_YC) == false)
                        {
                            string[] sa = readStr.Split(Global.g_SplitChar, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                            CTableYC obj = get_YC(sa);
                            mdl.lst_Table_YC.Add(obj.Id, obj);
                        }
                    }
                    else if (iSet == CST_SET.Index_SYX) //单点遥信表
                    {
                        if (readStr.Contains("//") == false && readStr.Contains(Global.cst_Table_SYX) == false)
                        {
                            string[] sa = readStr.Split(Global.g_SplitChar, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                            CTableSYX obj = get_SYX(sa);
                            mdl.lst_Table_SYX.Add(obj.Id, obj);
                        }
                    }
                    else if (iSet == CST_SET.Index_DYX) //双点遥信表
                    {
                        if (readStr.Contains("//") == false && readStr.Contains(Global.cst_Table_DYX) == false)
                        {
                            string[] sa = readStr.Split(Global.g_SplitChar, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                            CTableDYX obj = get_DYX(sa);
                            mdl.lst_Table_DYX.Add(obj.Id, obj);
                        }
                    }
                    else if (iSet == CST_SET.Index_YK) //遥控表
                    {
                        if (readStr.Contains("//") == false && readStr.Contains(Global.cst_Table_YK) == false)
                        {
                            string[] sa = readStr.Split(Global.g_SplitChar, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                            CTableYK obj = get_YK(sa);
                            mdl.lst_Table_YK.Add(obj.Id, obj);
                        }
                    }
                    else if (iSet == CST_SET.Index_Meter) //计量值表
                    {
                        if (readStr.Contains("//") == false && readStr.Contains(Global.cst_Table_Meter) == false)
                        {
                            string[] sa = readStr.Split(Global.g_SplitChar, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                            CTableMeter obj = get_Meter(sa);
                            mdl.lst_Table_Meter.Add(obj.Id, obj);
                        }
                    }
                    else if (iSet == CST_SET.Index_Port) //端口表
                    {
                        if (readStr.Contains("//") == false && readStr.Contains(Global.cst_Table_Port) == false)
                        {
                            string[] sa = readStr.Split(Global.g_SplitChar, StringSplitOptions.RemoveEmptyEntries);//将读取的字符串按"制表符/t“和””“分割成数组
                            CTablePort obj = get_Port(sa);
                            mdl.lst_Table_Port.Add(obj.Id, obj);
                        }
                    }
                }
            }
            mdl.Id = CModel.Accu;
            CModel.Accu += 1;
            Global.g_list_Model.Add(mdl.Id, mdl);
            Global.sorted_list_Model();

            //设备表集
            CDeviceTable devs = new CDeviceTable();
            devs.Id = CDeviceTable.Accu;
            CDeviceTable.Accu += 1;
            devs.DeviceTableName = "Devs_1";
            Global.g_list_DeviceTable.Add(devs.Id, devs);

            CDevice dev = new CDevice();
            dev.Id = CDevice.Accu;
            CDevice.Accu += 1;
            dev.DeviceName = "Dev_1";
            dev.ModelName = Global.g_list_Model[1].ModelName;
            dev.CommAddr = 1;
            Global.g_list_DeviceTable[1].lst_Device.Add(dev.Id, dev);
 
            CDeviceTable devs2 = new CDeviceTable();
            devs2.Id = CDeviceTable.Accu;
            CDeviceTable.Accu += 1;
            devs2.DeviceTableName = "Devs_2";
            Global.g_list_DeviceTable.Add(devs2.Id, devs2);

            CDevice dev2 = new CDevice();
            dev2.Id = CDevice.Accu;
            CDevice.Accu += 1;
            dev2.DeviceName = "Dev_1";
            dev2.ModelName = Global.g_list_Model[1].ModelName;
            dev2.CommAddr = 1;
            Global.g_list_DeviceTable[2].lst_Device.Add(dev2.Id, dev2);

            //转发表集(清空, Client不需要)
            Global.g_list_FWT.Clear();

            //端口集(修改)
            for (m = 1; m <= 9; m++)
            {
                Global.g_Model.lst_Table_Port[m].Enabled = false;
                Global.g_Model.lst_Table_Port[m].cfg_Port.bUsed = false;
            }
            if (giComId >= 2 && giComId  <= 5)
            {//对于Client，无所谓用哪一个串口，默认就用2（普通串口2）
                m = 2;
                Global.g_Model.lst_Table_Port[m].Enabled = true;
                Global.g_Model.lst_Table_Port[m].Addr = 1;
                Global.g_Model.lst_Table_Port[m].cfg_Port.tPort.u8LogicAttr = 4;
                Global.g_Model.lst_Table_Port[m].cfg_Port.eProtocol = UserProtocolType_e.EN_PROTOCOL_101_DOWN;
                Global.g_Model.lst_Table_Port[m].LogicAttribute = "对下";
                Global.g_Model.lst_Table_Port[m].DeviceTableName = "Devs_2";
                Global.g_Model.lst_Table_Port[m].FWTName = "";
                Global.g_Model.lst_Table_Port[m].cfg_Port.bUsed = true;
                Global.g_Model.lst_Table_Port[m].cfg_Port.u32DevsIndex = 1;
                Global.g_Model.lst_Table_Port[m].cfg_Port.u32FwtIndex = 0;
                Global.g_Model.lst_Table_Port[m].cfg_Port.tCommB101UpCfg.tLinkCfg.u8StationType = 1;
                Global.g_Model.lst_Table_Port[m].cfg_Port.tCommB101UpCfg.tLinkCfg.u8Dir = 0;
            }
            if (giNetId >= 7 && giNetId <= 9)
            {//对于Client，无所谓用哪一个网口，默认就用7（虚拟网口1）
                m = 7;
                Global.g_Model.lst_Table_Port[m].Enabled = true;
                Global.g_Model.lst_Table_Port[m].Addr = 1;
                Global.g_Model.lst_Table_Port[m].cfg_Port.tPort.u8LogicAttr = 4;
                Global.g_Model.lst_Table_Port[m].cfg_Port.eProtocol = UserProtocolType_e.EN_PROTOCOL_104_DOWN;
                Global.g_Model.lst_Table_Port[m].LogicAttribute = "对下";
                Global.g_Model.lst_Table_Port[m].DeviceTableName = "Devs_1";
                Global.g_Model.lst_Table_Port[m].FWTName = "";
                Global.g_Model.lst_Table_Port[m].cfg_Port.bUsed = true;
                Global.g_Model.lst_Table_Port[m].cfg_Port.u32DevsIndex = 0;
                Global.g_Model.lst_Table_Port[m].cfg_Port.u32FwtIndex = 0;
                Global.g_Model.lst_Table_Port[m].cfg_Port.tCommB104UpCfg.tLinkCfg.u8StationType = 1;
                Global.g_Model.lst_Table_Port[m].cfg_Port.tCommB104UpCfg.tLinkCfg.u8LinkType = 2;
                Global.g_Model.lst_Table_Port[m].cfg_Port.tPort.NPort.u8IP[0] = gbytClientIP[0];
                Global.g_Model.lst_Table_Port[m].cfg_Port.tPort.NPort.u8IP[1] = gbytClientIP[1];
                Global.g_Model.lst_Table_Port[m].cfg_Port.tPort.NPort.u8IP[2] = gbytClientIP[2];
                Global.g_Model.lst_Table_Port[m].cfg_Port.tPort.NPort.u8IP[3] = gbytClientIP[3];
                Global.g_Model.lst_Table_Port[m].cfg_Port.tCommB104UpCfg.tLinkCfg.u8ServerIP[0] = gbytServerIP[0];
                Global.g_Model.lst_Table_Port[m].cfg_Port.tCommB104UpCfg.tLinkCfg.u8ServerIP[1] = gbytServerIP[1];
                Global.g_Model.lst_Table_Port[m].cfg_Port.tCommB104UpCfg.tLinkCfg.u8ServerIP[2] = gbytServerIP[2];
                Global.g_Model.lst_Table_Port[m].cfg_Port.tCommB104UpCfg.tLinkCfg.u8ServerIP[3] = gbytServerIP[3];
                Global.g_Model.lst_Table_Port[m].cfg_Port.tCommB104UpCfg.tLinkCfg.u8ClientIP[0] = Global.g_Model.lst_Table_Port[m].cfg_Port.tPort.NPort.u8IP[0];
                Global.g_Model.lst_Table_Port[m].cfg_Port.tCommB104UpCfg.tLinkCfg.u8ClientIP[1] = Global.g_Model.lst_Table_Port[m].cfg_Port.tPort.NPort.u8IP[1];
                Global.g_Model.lst_Table_Port[m].cfg_Port.tCommB104UpCfg.tLinkCfg.u8ClientIP[2] = Global.g_Model.lst_Table_Port[m].cfg_Port.tPort.NPort.u8IP[2];
                Global.g_Model.lst_Table_Port[m].cfg_Port.tCommB104UpCfg.tLinkCfg.u8ClientIP[3] = Global.g_Model.lst_Table_Port[m].cfg_Port.tPort.NPort.u8IP[3];
            }
            DateTime dt = DateTime.Now;
            string strFileName = string.Format("cfg_{0:D4}{1:D2}{2:D2}.prj", dt.Year, dt.Month, dt.Day);
            string strServerIP = "";
            string strClientIP = "";
            strServerIP = string.Format("Server {0}.{1}.{2}.{3}", gbytServerIP[0], gbytServerIP[1], gbytServerIP[2], gbytServerIP[3]);
            strClientIP = string.Format("Client {0}.{1}.{2}.{3}", gbytClientIP[0], gbytClientIP[1], gbytClientIP[2], gbytClientIP[3]);
            strTemp = string.Format("{0}{1:D4}{2:D2}{3:D2}_{4:D2}{5:D2}{6:D2}({7},{8})_ByCfgTool//",
                strBaseFolderClient, dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second,
                strServerIP, strClientIP);

            if (false == Directory.Exists(strTemp)) { Directory.CreateDirectory(strTemp); }
            write_prj(strTemp + strFileName);
            SaveCfg(strTemp);

            //清理
            CModel.Accu = 1;
            CFWT.Accu = 1;
            CDeviceTable.Accu = 1;
            CDevice.Accu = 1;
            Global.g_list_Model.Clear();
            Global.g_Model.lst_Table_Para.Clear();
            Global.g_Model.lst_Table_Setting.Clear();
            Global.g_Model.lst_Table_YC.Clear();
            Global.g_Model.lst_Table_SYX.Clear();
            Global.g_Model.lst_Table_DYX.Clear();
            Global.g_Model.lst_Table_YK.Clear();
            Global.g_Model.lst_Table_Meter.Clear();
            foreach (var t in Global.g_list_DeviceTable)
            {
                t.Value.lst_Device.Clear();
            }
            Global.g_list_DeviceTable.Clear();

            if(gbCopyFileFlag == true)
            {
                string s1 = "";
                string s2 = "";
                string s3 = "";
                string s4 = "";
                s1 = "D:\\COPY\\";
                s2 = string.Format("{0}{1:D4}{2:D2}{3:D2}_{4:D2}{5:D2}{6:D2}\\", s1, dt.Year, dt.Month,dt.Day, dt.Hour,dt.Minute,dt.Second);
                if (false == Directory.Exists(s1)) { Directory.CreateDirectory(s1); }
                if (false == Directory.Exists(s2)) { Directory.CreateDirectory(s2); }

                DirectoryInfo TheFolder = new DirectoryInfo("D:\\");
                foreach (FileInfo NextFile in TheFolder.GetFiles())
                {
                    s3 = NextFile.FullName;
                    s4 = NextFile.Name;
                    if (s3.Contains(".prj") == true || s3.Contains(".cfg") == true)
                    {
                        File.Copy(s3, s2 + s4, true);
                        File.Delete(s3);
                    }
                }

                DirectoryInfo TheFolder2 = new DirectoryInfo(strTemp);
                foreach (FileInfo NextFile in TheFolder2.GetFiles())
                {
                    s3 = NextFile.FullName;
                    s4 = NextFile.Name;
                    File.Copy(s3, "D:\\" + s4, true);
                }
            }
        }
        #endregion

        //END
    }
}
