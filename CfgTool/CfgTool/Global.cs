using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CfgTool
{
    public class Global
    {
        public static string g_strSelfName = "本体";
        public static ushort g_Set_ItemNum = 4;

        public static bool g_bSoftYk_1 = false;
        public static bool g_bSoftYk_2 = false;

        public static bool bPrintPara = false;
        public static bool bPrintFwt = false;
        public static bool bPrintPort = false;

        #region "Telnet"
        public static TelnetConnection g_telnetConn = null;
        public static Queue<string> g_telnetQueue = new Queue<string>();
        #endregion

        #region "数据库连接"
        public static string Name = "";
        public static string Source = "";
        public static string User = "";
        public static string Pwd = "";
        //public static CMySQL g_mysql = new CMySQL();
        #endregion

        public static int iniPara_NAME_LEN = 32;
        public static char[] g_SplitChar = new char[] { '\t', '"', ' ', ',' };
        #region "INI配置文件参数"
        //----[Para]
        public static int iniPara_CN_PARASPACE_SIZE = 2048;
        //----[RTDB]
        public static int iniRTDB_CN_RTDB_YC_VOL = 128;
        public static int iniRTDB_CN_RTDB_SYX_VOL = 256;
        public static int iniRTDB_CN_RTDB_DYX_VOL = 32;
        public static int iniRTDB_CN_RTDB_DO_VOL = 64;
        public static int iniRTDB_CN_RTDB_POW_VOL = 64;
        //----[Comm]
        public static int iniComm_CN_COMM_PROTOCOLCFG_SIZE = 128;

        public static int iniComm_CN_COMM_MODEL_MAX = 16;
        public static int iniComm_CN_COMM_MODEL_INFOPIECE_MAX = 256;
        public static int iniComm_CN_COMM_MODEL_PARAGROUP_MAX = 512;//满配后，256不够用。jifeng，2017-12-21 18:14

        public static int iniComm_CN_COMM_DEVS_MAX = 1;
        public static int iniComm_CN_COMM_DEV_MAX = 8;

        public static int iniComm_CN_COMM_FWT_MAX = 1;
        public static int iniComm_CN_COMM_FWT_YC_MAX = 128;
        public static int iniComm_CN_COMM_FWT_SYX_MAX = 256;
        public static int iniComm_CN_COMM_FWT_DYX_MAX = 32;
        public static int iniComm_CN_COMM_FWT_YK_MAX = 16;
        public static int iniComm_CN_COMM_FWT_POW_MAX = 64;

        public static int iniComm_CN_COMM_SPORT_MAX = 3;
        public static int iniComm_CN_COMM_NPORT_MAX = 0;
        public static int iniComm_CN_COMM_VPORT_MAX = 0;
        public static int iniComm_CN_COMM_PORT_MAX = 3;
        //----[FWT]
        public static int iniFWT_CN_FWT_YCGROUP_NUM = 3;
        //----------------------------------------------
        public static int g_General_FwtSimpleColumn = 1;
        public static int g_General_MultiYc = 0;
        public static int g_General_MultiYx = 0;
        public static int g_General_DuplexChannel = 0;//通信双通道，jifeng，2018-8-27 15:37
        public static int g_General_YcAddrOffset = 16385;
        public static int g_General_SyxAddrOffset = 1;
        //----------------------------------------------
        //----[Ftp]
        public static int g_FtpEnable = 0;
        public static string g_FtpIp = "";
        public static int g_FtpDspCore = 0;
        public static int g_FtpArmCore = 0;
        public static int g_FtpArmCore1 = 0;
        //END
        #endregion

        #region "读INI配置文件"
        public static void readINI()
        {
            String strTemp = System.AppDomain.CurrentDomain.BaseDirectory + "Config.ini";
            iniRW readIni = new iniRW(strTemp);
            //----[Para]
            Global.iniPara_CN_PARASPACE_SIZE = readIni.ReadInteger("Para", "CN_PARASPACE_SIZE", 2048);
            //----[RTDB]
            Global.iniRTDB_CN_RTDB_YC_VOL = readIni.ReadInteger("RTDB", "CN_RTDB_YC_VOL", 128);
            Global.iniRTDB_CN_RTDB_SYX_VOL = readIni.ReadInteger("RTDB", "CN_RTDB_SYX_VOL", 256);
            Global.iniRTDB_CN_RTDB_DYX_VOL = readIni.ReadInteger("RTDB", "CN_RTDB_DYX_VOL", 32);
            Global.iniRTDB_CN_RTDB_DO_VOL = readIni.ReadInteger("RTDB", "CN_RTDB_DO_VOL", 64);
            Global.iniRTDB_CN_RTDB_POW_VOL = readIni.ReadInteger("RTDB", "CN_RTDB_POW_VOL", 64);
            //----[Comm]
            Global.iniComm_CN_COMM_PROTOCOLCFG_SIZE = readIni.ReadInteger("Comm", "CN_COMM_PROTOCOLCFG_SIZE", 128);

            Global.iniComm_CN_COMM_MODEL_MAX = readIni.ReadInteger("Comm", "CN_COMM_MODEL_MAX", 16);
            Global.iniComm_CN_COMM_MODEL_INFOPIECE_MAX = readIni.ReadInteger("Comm", "CN_COMM_MODEL_INFOPIECE_MAX", 256);
            Global.iniComm_CN_COMM_MODEL_PARAGROUP_MAX = readIni.ReadInteger("Comm", "CN_COMM_MODEL_PARAGROUP_MAX", 256);

            Global.iniComm_CN_COMM_DEVS_MAX = readIni.ReadInteger("Comm", "CN_COMM_DEVS_MAX", 1);
            Global.iniComm_CN_COMM_DEV_MAX = readIni.ReadInteger("Comm", "CN_COMM_DEV_MAX", 8);

            Global.iniComm_CN_COMM_FWT_MAX = readIni.ReadInteger("Comm", "CN_COMM_FWT_MAX", 1);
            Global.iniComm_CN_COMM_FWT_YC_MAX = readIni.ReadInteger("Comm", "CN_COMM_FWT_YC_MAX", 128);
            Global.iniComm_CN_COMM_FWT_SYX_MAX = readIni.ReadInteger("Comm", "CN_COMM_FWT_SYX_MAX", 256);
            Global.iniComm_CN_COMM_FWT_DYX_MAX = readIni.ReadInteger("Comm", "CN_COMM_FWT_DYX_MAX", 32);
            Global.iniComm_CN_COMM_FWT_YK_MAX = readIni.ReadInteger("Comm", "CN_COMM_FWT_YK_MAX", 16);
            Global.iniComm_CN_COMM_FWT_POW_MAX = readIni.ReadInteger("Comm", "CN_COMM_FWT_POW_MAX", 64);

            Global.iniComm_CN_COMM_SPORT_MAX = readIni.ReadInteger("Comm", "CN_COMM_SPORT_MAX", 3);
            Global.iniComm_CN_COMM_NPORT_MAX = readIni.ReadInteger("Comm", "CN_COMM_NPORT_MAX", 0);
            Global.iniComm_CN_COMM_VPORT_MAX = readIni.ReadInteger("Comm", "CN_COMM_VPORT_MAX", 0);
            Global.iniComm_CN_COMM_PORT_MAX = readIni.ReadInteger("Comm", "CN_COMM_PORT_MAX", 3);
            //----[FWT]
            Global.iniFWT_CN_FWT_YCGROUP_NUM = readIni.ReadInteger("FWT", "CN_FWT_YCGROUP_NUM", 6);

            //----Config2.ini
            strTemp = System.AppDomain.CurrentDomain.BaseDirectory + "Config2.ini";
            iniRW readIni2 = new iniRW(strTemp);
            Global.g_General_FwtSimpleColumn = readIni2.ReadInteger("General", "FwtSimpleColumn ", 1);
            Global.g_General_MultiYc = readIni2.ReadInteger("General", "MultiYc ", 0);
            Global.g_General_MultiYx = readIni2.ReadInteger("General", "MultiYx ", 0);
            Global.g_General_DuplexChannel = readIni2.ReadInteger("General", "DuplexChannel ", 0);
            Global.g_General_YcAddrOffset = readIni2.ReadInteger("General", "YcAddrOffset ", 16385);
            Global.g_General_SyxAddrOffset = readIni2.ReadInteger("General", "SyxAddrOffset ", 1);
            //----
            Global.g_FtpEnable = readIni2.ReadInteger("Ftp", "Enable", 0);
            Global.g_FtpIp = readIni2.ReadString("Ftp", "Ip", ""); Global.g_FtpIp = Global.g_FtpIp.Trim();
            Global.g_FtpDspCore = readIni2.ReadInteger("Ftp", "DspCore", 0);
            Global.g_FtpArmCore = readIni2.ReadInteger("Ftp", "ArmCore", 0);
            Global.g_FtpArmCore1 = readIni2.ReadInteger("Ftp", "ArmCore1", 0);
            //----
            Global.g_bSoftYk_1 = (readIni2.ReadInteger("YkCfg", "YF", 0) == 0) ? false : true;
            Global.g_bSoftYk_2 = (readIni2.ReadInteger("YkCfg", "YB", 0) == 0) ? false : true;
            //END
        }
        #endregion

        #region "常量字符串"
        public const string cst_Table_Para = "参数表";
        public const string cst_Table_Setting = "定值表";
        public const string cst_Table_YC = "遥测表";
        public const string cst_Table_SYX = "单点遥信表";
        public const string cst_Table_DYX = "双点遥信表";
        public const string cst_Table_YK = "遥控表";
        public const string cst_Table_Meter = "计量值表";
        public const string cst_Table_Port = "端口表";
        public const string cst_Table_GYX = "组合遥信表"; //新增，jifeng，2018-11-1 11:01

        public const string cst_Set_Para = "参数集";//无二级菜单
        public const string cst_Set_Setting = "定值集";//无二级菜单
        public const string cst_Set_Model = "模板集";
        public const string cst_Set_DeviceTable = "设备表集";
        public const string cst_Set_FWT = "转发表集";
        public const string cst_Set_RTDB = "实时数据集";//无二级菜单
        public const string cst_Set_Port = "端口集";

        public const string cst_Port_Serial = "串口";
        public const string cst_Port_Network = "网口";
        public const string cst_Port_Virtual = "虚拟口";
        #endregion

        #region "串口参数"
        public static string[] Serial_BaudRate = {"300",
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
                                                  "115200" };
        public static string[] Serial_DataBits = {"8",
                                                  "7",
                                                  "6",
                                                  "5" };
        public static string[] Serial_StopBits = {"1",
                                                  "2" };
        public static string[] Serial_Parity = {"None-无校验",
                                                "Odd-奇校验",
                                                "Even-偶校验" };
        #endregion

        #region "规约"
        public static string[] Protocol_Support = {"对上101规约",
                                                   "对上104规约",
                                                   "对上MODBUS规约", 
                                                   "对上CDT规约",
                                                   "对下101规约",
                                                   "对下104规约", 
                                                   "对下MODBUS规约",
                                                   "对下CDT规约",
                                                   "对上101规约V1",//老版信息安全，2017-11-16
                                                   "对上104规约V1"};
        #endregion

        public static CModel g_Model = new CModel();//新建工程时，选择的模板
        public static SortedList<int, CModel> g_list_Model = new SortedList<int, CModel>();//模板集，右键菜单，导入的模板
        public static SortedList<int, CDeviceTable> g_list_DeviceTable = new SortedList<int, CDeviceTable>();//设备表集，右键菜单，新增设备表
        public static SortedList<int, CFWT> g_list_FWT = new SortedList<int, CFWT>();//转发表集，右键菜单，新增转发表
        public static SortedList<int, CFWT> g_list_FWT_Import = new SortedList<int, CFWT>();//转发表集(导入)

        public static void sorted_list_Model()
        {
            int iCount = 0;
            foreach (var t in g_list_Model)
            {
                t.Value.SN = iCount;
                iCount += 1;
            }
        }
        public static void sorted_list_DeviceTable()
        {
            int iCount_1 = 0;
            foreach (var t in g_list_DeviceTable)
            {
                t.Value.SN = iCount_1;
                int iCount_2 = 0;
                foreach (var dev in t.Value.lst_Device)
                {
                    dev.Value.SN = iCount_2;
                    iCount_2 += 1;
                }
                iCount_1 += 1;
            }
        }
        public static void sorted_list_FWT()
        {
            int iCount = 0;
            foreach (var t in g_list_FWT)
            {
                t.Value.SN = iCount;
                iCount += 1;
            }
        }

        public static void refresh_list_FWT()
        {
            //建立4张Fwt，删除第1和第2个，只剩下key=3和key=4的Fwt；如果再新增1张Fwt，则Id=3，报Key重复。问题先遗留。2017-12-13 18:15
        }

        #region "公共方法"
        public static void cancel_dgv_select(DataGridView dgv)
        {
            if (dgv.RowCount == 0) { return; }
            if (dgv.SelectionMode == DataGridViewSelectionMode.FullRowSelect)
            {
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    if (dgv.Rows[i].Selected == true)
                    {
                        dgv.Rows[i].Selected = false;
                    }
                }
            }
            else if (dgv.SelectionMode == DataGridViewSelectionMode.CellSelect)
            {
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    for (int j = 0; j < dgv.RowCount; j++)
                    {
                        if (dgv.Rows[i].Cells[j].Selected == true)
                        {
                            dgv.Rows[i].Cells[j].Selected = false;
                        }
                    }
                }
            }
        }
        #endregion
    }

    #region "eDataType"
    public enum eDataType
    {
        S8 = 0,
        U8,
        S16,
        U16,
        S32,
        U32,
        S64,
        U64,
        F32,
        F64
    }
    #endregion

    #region "类定义"
    public class CTableBase //基类
    {
        public int Id;//参数序号，按1累加
        public string GroupName;//组名，字符串，最大32bytes，遥信防抖时间
        public string ItemName;//条目名，字符串，最大32bytes，YX01
        public string DataType;//数据类型
        public int Addr;//信息对象地址，16进制，0x8241
        //FWT左侧列表中是否移除或隐藏的标志位
        public bool FlagDelete = false;//true-被删除
        //新增2017.5.10，转发表View中使用
        public int SN = 0;
        public string DeviceName = "";
        public string ModelName = "";
        //新增2017.5.12 19:20，信息分组
        public int Group = 0;
        //新增 本体字符串，2017-11-21 17:27
        //public string SelfName = "";
    }
    //--------
    public class CTablePara : CTableBase //参数表
    {
        //public eDataType DataType;//数据类型，U16
        public string Unit;//单位，字符串，最大8bytes，ms
        public float ValueMax;//最大值，10进制，60000
        public float ValueMin;//最小值，10进制，7
        public float ValueDefault;//默认值，10进制，20
        public float Ratio;//系数，10进制，1
        
        public float ValueCurrent;//当前值
        public string strValueCurrent;

        public int BytePos;//新增：字节数组中的位置，jifeng，2017-6-15
    }
    //--------
    public class CTableSetting : CTableBase //定值表
    {
        //public eDataType DataType;//数据类型，U16
        public string Unit;//单位，字符串，最大8bytes，ms
        public float ValueMax;//最大值，10进制，60000
        public float ValueMin;//最小值，10进制，7
        public float ValueDefault;//默认值，10进制，20
        public float Ratio;//系数，10进制，1

        public float ValueCurrent;//当前值
        public string strValueCurrent;

        public int BytePos;//新增：字节数组中的位置，jifeng，2017-6-15
    }
    //--------
    public class CTableYC : CTableBase //遥测
    {
        //public eDataType DataType;//数据类型，U16
        public string Unit;//单位，字符串，最大8bytes，ms
        public int Ratio;//系数，10进制，1
        public int iGroup;//Zone不用了，用Group(遥测分组)，jifeng，2017-6-18
        //南网需求，2017-10-27 10:49
        public float fYcZone;//遥测死区
        public float fYcCoe;//遥测调整系数

        public void setYcZone(int iGrp)
        {
            if(iGrp == 0)
            {
                fYcZone = 0.005f;
            }
            else if (iGrp == 1)
            {
                fYcZone = 0.5f;
            }
            else if (iGrp == 2)
            {
                fYcZone = 0.2f;
            }
            else if (iGrp == 3)
            {
                fYcZone = 3.0f;
            }
            else if (iGrp == 4)
            {
                fYcZone = 0.01f;
            }
            else if (iGrp == 5)
            {
                fYcZone = 0.005f;
            }
        }

        public void setYcCoe(int iGrp, int iType)
        {
            //归一化上送默认值0，标度化上送默认值1，浮点数上送默认值2
            if (iGrp == 0)
            {
                if(iType == 0)
                {
                    fYcCoe = 100.0f;
                }
                else if (iType == 1)
                {
                    fYcCoe = 100.0f;
                }
                else if (iType == 2)
                {
                    fYcCoe = 1.0f;
                }
            }
            else if (iGrp == 1)
            {
                if (iType == 0)
                {
                    fYcCoe = 450.0f;
                }
                else if (iType == 1)
                {
                    fYcCoe = 100.0f;
                }
                else if (iType == 2)
                {
                    fYcCoe = 1.0f;
                }
            }
            else if (iGrp == 2)
            {
                if (iType == 0)
                {
                    fYcCoe = 60.0f;
                }
                else if (iType == 1)
                {
                    fYcCoe = 1000.0f;
                }
                else if (iType == 2)
                {
                    fYcCoe = 1.0f;
                }
            }
            else if (iGrp == 3)
            {
                if (iType == 0)
                {
                    fYcCoe = 6600.0f;
                }
                else if (iType == 1)
                {
                    fYcCoe = 10.0f;
                }
                else if (iType == 2)
                {
                    fYcCoe = 1.0f;
                }
            }
            else if (iGrp == 4)
            {
                if (iType == 0)
                {
                    fYcCoe = 60.0f;
                }
                else if (iType == 1)
                {
                    fYcCoe = 100.0f;
                }
                else if (iType == 2)
                {
                    fYcCoe = 1.0f;
                }
            }
            else if (iGrp == 5)
            {
                if (iType == 0)
                {
                    fYcCoe = 1.0f;
                }
                else if (iType == 1)
                {
                    fYcCoe = 1000.0f;
                }
                else if (iType == 2)
                {
                    fYcCoe = 1.0f;
                }
            }
        }

        public CTableYC MyClone()
        {
            CTableYC c = new CTableYC();
            c.Id = this.Id;
            c.GroupName = this.GroupName;
            c.ItemName = this.ItemName;
            c.DataType = this.DataType;
            c.Unit = this.Unit;
            c.Ratio = this.Ratio;
            c.iGroup = this.iGroup;
            c.Addr = this.Addr;

            c.SN = this.SN;
            c.DeviceName = this.DeviceName;
            c.ModelName = this.ModelName;

            c.fYcZone = this.fYcZone;
            c.fYcCoe = this.fYcCoe;

            c.FlagDelete = this.FlagDelete;
            return c;
        }
    }
    public class CTableSYX : CTableBase //单点遥信
    {
        //public eDataType DataType;//数据类型，U8
        public CTableSYX MyClone()
        {
            CTableSYX c = new CTableSYX();
            c.Id = this.Id;
            c.GroupName = this.GroupName;
            c.ItemName = this.ItemName;
            c.DataType = this.DataType;
            c.Addr = this.Addr;

            c.SN = this.SN;
            c.DeviceName = this.DeviceName;
            c.ModelName = this.ModelName;

            c.FlagDelete = this.FlagDelete;
            return c;
        }
    }
    public class CTableGYX : CTableBase //组合遥信
    {
        //public eDataType DataType;//数据类型，U8
        public LogicCfg_Logic_t logic = new LogicCfg_Logic_t();
        public CTableGYX MyClone()
        {
            CTableGYX c = new CTableGYX();
            c.Id = this.Id;
            c.GroupName = this.GroupName;
            c.ItemName = this.ItemName;
            c.DataType = this.DataType;
            c.Addr = this.Addr;

            c.SN = this.SN;
            c.DeviceName = this.DeviceName;
            c.ModelName = this.ModelName;

            c.FlagDelete = this.FlagDelete;
            return c;
        }
    }
    public class CTableDYX : CTableBase //双点遥信
    {
        //public eDataType DataType;//数据类型，U8
        public CTableDYX MyClone()
        {
            CTableDYX c = new CTableDYX();
            c.Id = this.Id;
            c.GroupName = this.GroupName;
            c.ItemName = this.ItemName;
            c.DataType = this.DataType;
            c.Addr = this.Addr;

            c.SN = this.SN;
            c.DeviceName = this.DeviceName;
            c.ModelName = this.ModelName;

            c.FlagDelete = this.FlagDelete;
            return c;
        }
    }
    public class CTableYK : CTableBase //遥控
    {
        public CTableYK MyClone()
        {
            CTableYK c = new CTableYK();
            c.Id = this.Id;
            c.GroupName = this.GroupName;
            c.ItemName = this.ItemName;
            c.DataType = this.DataType;
            c.Addr = this.Addr;

            c.SN = this.SN;
            c.DeviceName = this.DeviceName;
            c.ModelName = this.ModelName;

            c.FlagDelete = this.FlagDelete;
            return c;
        }
    }
    public class CTableMeter : CTableBase //计量
    {
        //public eDataType DataType;//数据类型，S64
        public string Unit;//单位，字符串，最大8bytes，W
        //public int Ratio;//系数，10进制，1
        public CTableMeter MyClone()
        {
            CTableMeter c = new CTableMeter();
            c.Id = this.Id;
            c.GroupName = this.GroupName;
            c.ItemName = this.ItemName;
            c.DataType = this.DataType;
            c.Unit = this.Unit;
            //c.Ratio = this.Ratio;
            c.Addr = this.Addr;

            c.SN = this.SN;
            c.DeviceName = this.DeviceName;
            c.ModelName = this.ModelName;

            c.FlagDelete = this.FlagDelete;
            return c;
        }
    }
    //--------
    public class CTablePort //端口
    {
        public CTablePort()
        {
            cfg_Port = new PortCfg();
            PhysicalAttribute = "串口";
            LogicAttribute = "对上";
            ProtocolName = "对上101规约";
            FWTName = "";
            DeviceTableName = "";
            ProtocolInstanceNum = 0;
            Addr = 1;
            Enabled = false;
            cfg_Port.bUsed = Enabled;
            cfg_Port.eProtocol = UserProtocolType_e.EN_PROTOCOL_101_UP;
        }

        public void setAttribute()
        {
            if (PhysicalAttribute == "串口")
            {
                cfg_Port.tPort.u8PhyAttr = Convert.ToByte(1);
                ProtocolName = "对上101规约";
                cfg_Port.eProtocol = UserProtocolType_e.EN_PROTOCOL_101_UP;
            }
            else if (PhysicalAttribute == "网口" || PhysicalAttribute == "虚拟口")
            {
                cfg_Port.tPort.u8PhyAttr = Convert.ToByte(2);
                ProtocolName = "对上104规约";
                cfg_Port.eProtocol = UserProtocolType_e.EN_PROTOCOL_104_UP;
                cfg_Port.tCommB104UpCfg.tAppCfg.u8ObjAddrLen = 3;
            }
            cfg_Port.tPort.u8LogicAttr = Convert.ToByte(3);
        }

        public int Id;//端口的序号，按1累加
        public string PortName;//端口的名称，字符串，最大32bytes
        public string PhysicalAttribute;//端口的物理属性，串口/网口/虚拟口
        public string LogicAttribute;//逻辑属性，对上/对下
        public string ProtocolName;//规约名称
        public string FWTName = "";//转发表名称
        public string DeviceTableName = "";//设备表名称
        public int ProtocolInstanceNum;//规约实例号
        public int Addr;//地址
        public bool Enabled;//使能
        //------------------------------------------------------------------
        public PortCfg cfg_Port;//对上 or 对下101规约
    }
    //--------
    public class CModel //模板
    {
        public int SN = 0;//顺序编号
        public int Id = 0;
        public static int Accu = 1;//累加
        public string ModelName = "";
        //-------------------------------------------
        public SortedList<int, CTablePara> lst_Table_Para = new SortedList<int, CTablePara>();//参数表
        public SortedList<int, CTableSetting> lst_Table_Setting = new SortedList<int, CTableSetting>();//定值表
        public SortedList<int, CTableYC> lst_Table_YC = new SortedList<int, CTableYC>();//遥测表
        public SortedList<int, CTableSYX> lst_Table_SYX = new SortedList<int, CTableSYX>();//单点遥信表
        public SortedList<int, CTableDYX> lst_Table_DYX = new SortedList<int, CTableDYX>();//双点遥信表
        public SortedList<int, CTableYK> lst_Table_YK = new SortedList<int, CTableYK>();//遥控表
        public SortedList<int, CTableMeter> lst_Table_Meter = new SortedList<int, CTableMeter>();//计量值表
        public SortedList<int, CTablePort> lst_Table_Port = new SortedList<int, CTablePort>();//端口表
        public SortedList<int, CTableGYX> lst_Table_GYX = new SortedList<int, CTableGYX>();//组合遥信表
        //-------------------------------------------
        public void init()
        {
            ModelName = "";
            lst_Table_Para.Clear();
            lst_Table_Setting.Clear();
            lst_Table_YC.Clear();
            lst_Table_SYX.Clear();
            lst_Table_DYX.Clear();
            lst_Table_YK.Clear();
            lst_Table_Meter.Clear();
            lst_Table_Port.Clear();
        }
        //-------------------------------------------
        public void write_bin_Para_Setting(BinaryWriter bw)
        {
            //-------------------------------------jifeng，2017-6-16
            byte[] btWrite = new byte[Global.iniPara_CN_PARASPACE_SIZE];
            for (int m = 0; m < btWrite.Length; m++) { btWrite[m] = 0x00; }
            //-------------------------------------
            foreach (var t in lst_Table_Para.Values)
            {
                if (t.BytePos == 0xffff) 
                { 
                    continue; 
                }
                if (t.DataType == "U8" || t.DataType == "BOOL")
                {
                    byte b = Convert.ToByte(t.strValueCurrent);
                    btWrite[t.BytePos] = b;

                    if(Global.bPrintPara == true)
                    {
                        string ssss;
                        ssss = string.Format("{0}, {1}, U8: {2:x}", t.Id, t.ItemName, btWrite[t.BytePos]);
                        Form_CfgTool.pMainForm.formInfo.LogMessage(ssss);
                    }
                }
                else if (t.DataType == "U16")
                {
                    UInt16 b = Convert.ToUInt16(t.strValueCurrent);
                    byte[] bt = BitConverter.GetBytes(b);
                    btWrite[t.BytePos] = bt[0];
                    if (bt.Length >= 2) { btWrite[t.BytePos + 1] = bt[1]; }
                    if (Global.bPrintPara == true)
                    {
                        string ssss;
                        ssss = string.Format("{0}, {1}, U16: {2:x} {3:x}", t.Id, t.ItemName, btWrite[t.BytePos], btWrite[t.BytePos + 1]);
                        Form_CfgTool.pMainForm.formInfo.LogMessage(ssss);
                    }
                }
                else if (t.DataType == "U32")
                {
                    UInt32 b = Convert.ToUInt32(t.strValueCurrent);
                    byte[] bt = BitConverter.GetBytes(b);
                    btWrite[t.BytePos] = bt[0];
                    if (bt.Length >= 2) { btWrite[t.BytePos + 1] = bt[1]; }
                    if (bt.Length >= 3) { btWrite[t.BytePos + 2] = bt[2]; }
                    if (bt.Length >= 4) { btWrite[t.BytePos + 3] = bt[3]; }
                    if (Global.bPrintPara == true)
                    {
                        string ssss;
                        ssss = string.Format("{0}, {1}, U32: {2:x} {3:x} {4:x} {5:x}", t.Id, t.ItemName, btWrite[t.BytePos], btWrite[t.BytePos + 1]
                            , btWrite[t.BytePos + 2], btWrite[t.BytePos + 3]);
                        Form_CfgTool.pMainForm.formInfo.LogMessage(ssss);
                    }
                }
                else if (t.DataType == "F32")
                {
                    float f = Convert.ToSingle(t.strValueCurrent);
                    byte[] bt = BitConverter.GetBytes(f);
                    btWrite[t.BytePos] = bt[0];
                    btWrite[t.BytePos + 1] = bt[1];
                    btWrite[t.BytePos + 2] = bt[2];
                    btWrite[t.BytePos + 3] = bt[3];
                    if (Global.bPrintPara == true)
                    {
                        string ssss;
                        ssss = string.Format("{0}, {1}, F32: {2:x} {3:x} {4:x} {5:x}", t.Id, t.ItemName, btWrite[t.BytePos], btWrite[t.BytePos + 1]
                            , btWrite[t.BytePos + 2], btWrite[t.BytePos + 3]);
                        Form_CfgTool.pMainForm.formInfo.LogMessage(ssss);
                    }
                }
                else if (t.DataType == "STRING")
                {
                    //btWrite[t.BytePos] = 0x00;//STRING类型，不写
                }
            }
            //-------------------------------------
            foreach (var t in lst_Table_Setting.Values)
            {
                if (t.BytePos == 0xffff)
                {
                    continue;
                }
                if (t.DataType == "U8" || t.DataType == "BOOL")
                {
                    byte b = Convert.ToByte(t.strValueCurrent);
                    btWrite[t.BytePos] = b;
                    if (Global.bPrintPara == true)
                    {
                        string ssss;
                        ssss = string.Format("{0}, {1}, U8: {2:x}", t.Id, t.ItemName, btWrite[t.BytePos]);
                        Form_CfgTool.pMainForm.formInfo.LogMessage(ssss);
                    }
                }
                else if (t.DataType == "U16")
                {
                    UInt16 b = Convert.ToUInt16(t.strValueCurrent);
                    byte[] bt = BitConverter.GetBytes(b);
                    btWrite[t.BytePos] = bt[0];
                    if (bt.Length >= 2) { btWrite[t.BytePos + 1] = bt[1]; }
                    if (Global.bPrintPara == true)
                    {
                        string ssss;
                        ssss = string.Format("{0}, {1}, U16: {2:x} {3:x}", t.Id, t.ItemName, btWrite[t.BytePos], btWrite[t.BytePos + 1]);
                        Form_CfgTool.pMainForm.formInfo.LogMessage(ssss);
                    }
                }
                else if (t.DataType == "U32")
                {
                    UInt32 b = Convert.ToUInt32(t.strValueCurrent);
                    byte[] bt = BitConverter.GetBytes(b);

                    btWrite[t.BytePos] = bt[0];
                    if (bt.Length >= 2) { btWrite[t.BytePos + 1] = bt[1]; }
                    if (bt.Length >= 3) { btWrite[t.BytePos + 2] = bt[2]; }
                    if (bt.Length >= 4) { btWrite[t.BytePos + 3] = bt[3]; }
                    if (Global.bPrintPara == true)
                    {
                        string ssss;
                        ssss = string.Format("{0}, {1}, U32: {2:x} {3:x} {4:x} {5:x}", t.Id, t.ItemName, btWrite[t.BytePos], btWrite[t.BytePos + 1]
                            , btWrite[t.BytePos + 2], btWrite[t.BytePos + 3]);
                        Form_CfgTool.pMainForm.formInfo.LogMessage(ssss);
                    }
                }
                else if (t.DataType == "F32")
                {
                    float f = Convert.ToSingle(t.strValueCurrent);
                    byte[] bt = BitConverter.GetBytes(f);
                    btWrite[t.BytePos] = bt[0];
                    btWrite[t.BytePos + 1] = bt[1];
                    btWrite[t.BytePos + 2] = bt[2];
                    btWrite[t.BytePos + 3] = bt[3];
                    if (Global.bPrintPara == true)
                    {
                        string ssss;
                        ssss = string.Format("{0}, {1}, F32: {2:x} {3:x} {4:x} {5:x}", t.Id, t.ItemName, btWrite[t.BytePos], btWrite[t.BytePos + 1]
                            , btWrite[t.BytePos + 2], btWrite[t.BytePos + 3]);
                        Form_CfgTool.pMainForm.formInfo.LogMessage(ssss);
                    }
                }
                else if (t.DataType == "STRING")
                {
                    //btWrite[t.BytePos] = 0x00;//STRING类型，不写
                }
            }
            for (int m = 0; m < btWrite.Length; m++) { bw.Write(btWrite[m]); }
        }
        public void read_bin_Para_Setting(BinaryReader br)
        {
            //-------------------------------------jifeng，2017-6-16
            byte[] btRead = new byte[Global.iniPara_CN_PARASPACE_SIZE];
            for (int m = 0; m < btRead.Length; m++) { btRead[m] = 0x00; }
            //----
            try
            {
                int iPos = 0;
                while (true)
                {
                    btRead[iPos++] = br.ReadByte();
                }
            }
            catch
            {
            }
            //----
            foreach (var t in lst_Table_Para.Values)
            {
                if (t.DataType == "U8" || t.DataType == "BOOL")
                {
                    t.strValueCurrent = btRead[t.BytePos].ToString();
                }
                else if (t.DataType == "U16")
                {
                    byte[] tmp = { btRead[t.BytePos], btRead[t.BytePos + 1] };
                    UInt16 ui = BitConverter.ToUInt16(tmp, 0);
                    t.strValueCurrent = ui.ToString();
                }
                else if (t.DataType == "U32")
                {
                    byte[] tmp = { btRead[t.BytePos], btRead[t.BytePos + 1], btRead[t.BytePos + 2], btRead[t.BytePos + 3] };
                    UInt32 ui = BitConverter.ToUInt32(tmp, 0);
                    t.strValueCurrent = ui.ToString();
                }
            }
            //----
            foreach (var t in lst_Table_Setting.Values)
            {
                if (t.DataType == "U8" || t.DataType == "BOOL")
                {
                    t.strValueCurrent = btRead[t.BytePos].ToString();
                }
                else if (t.DataType == "U16")
                {
                    byte[] tmp = { btRead[t.BytePos], btRead[t.BytePos + 1] };
                    UInt16 ui = BitConverter.ToUInt16(tmp, 0);
                    t.strValueCurrent = ui.ToString();
                }
                else if (t.DataType == "U32")
                {
                    byte[] tmp = { btRead[t.BytePos], btRead[t.BytePos + 1], btRead[t.BytePos + 2], btRead[t.BytePos + 3] };
                    UInt32 ui = BitConverter.ToUInt32(tmp, 0);
                    t.strValueCurrent = ui.ToString();
                }
            }
        }
        //END
    }
    //--------
    public class CDeviceTable //设备表
    {
        public int SN = 0;//顺序编号
        public static int Accu = 1;//累加
        public int Id = 0;
        public string DeviceTableName = "";//设备表名称

        public SortedList<int, CDevice> lst_Device = new SortedList<int, CDevice>();
    }
    public class CDevice //设备
    {
        public int SN = 0;//顺序编号
        public static int Accu = 1;//累加
        public int Id = 0;
        public string DeviceName = "";//设备名称
        public string ModelName = "";//模板名称
        public int CommAddr = 0;//通信地址

        //public DevCfg_t cfg_Dev = new DevCfg_t();
    }
    //--------
    public class CFWT //转发表
    {
        public int SN = 0;//顺序编号
        public static int Accu = 1;//累加
        public int Id = 0;
        public string FWTName = "";//转发表名称
        //-------------------------------------------
        public int I_MAX = 60000;
        public int V_MAX = 60000;
        public int DC_MAX = 60000;
        public int P_MAX = 60000;
        public int FR_MAX = 60000;
        public int COS_MAX = 60000;
        //----
        public int I_COE = 1000;
        public int V_COE = 1000;
        public int DC_COE = 1000;
        public int P_COE = 1000;
        public int FR_COE = 1000;
        public int COS_COE = 1000;
        //-------------------------------------------
        public SortedList<int, CTableYC> lst_Table_YC_1 = new SortedList<int, CTableYC>();//遥测表
        public SortedList<int, CTableSYX> lst_Table_SYX_1 = new SortedList<int, CTableSYX>();//单点遥信表
        public SortedList<int, CTableDYX> lst_Table_DYX_1 = new SortedList<int, CTableDYX>();//双点遥信表
        public SortedList<int, CTableYK> lst_Table_YK_1 = new SortedList<int, CTableYK>();//遥控表
        public SortedList<int, CTableMeter> lst_Table_Meter_1 = new SortedList<int, CTableMeter>();//计量值表

        public SortedList<int, CTableGYX> lst_Table_GYX = new SortedList<int, CTableGYX>();//组合遥信表
        //public LogicCfg_Logic_t m_Logic = new LogicCfg_Logic_t();
        //-------------------------------------------
        public SortedList<int, CTableYC> lst_Table_YC_2 = new SortedList<int, CTableYC>();//遥测表
        public SortedList<int, CTableSYX> lst_Table_SYX_2 = new SortedList<int, CTableSYX>();//单点遥信表
        public SortedList<int, CTableDYX> lst_Table_DYX_2 = new SortedList<int, CTableDYX>();//双点遥信表
        public SortedList<int, CTableYK> lst_Table_YK_2 = new SortedList<int, CTableYK>();//遥控表
        public SortedList<int, CTableMeter> lst_Table_Meter_2 = new SortedList<int, CTableMeter>();//计量值表
        //-------------------------------------------
        public FwtCfg_t cfg_fwt = new FwtCfg_t();
        //-------------------------------------------
        public void sourceClone()
        {
            foreach (var t in Global.g_Model.lst_Table_YC.Values)
            {
                CTableYC tmp = t.MyClone();
                tmp.SN = lst_Table_YC_1.Count;
                tmp.DeviceName = Global.g_strSelfName;
                tmp.ModelName = Global.g_Model.ModelName;
                lst_Table_YC_1.Add(tmp.SN, tmp);
            }
            foreach (var t in Global.g_Model.lst_Table_SYX.Values)
            {
                CTableSYX tmp = t.MyClone();
                tmp.SN = lst_Table_SYX_1.Count;
                tmp.DeviceName = Global.g_strSelfName;
                tmp.ModelName = Global.g_Model.ModelName;
                lst_Table_SYX_1.Add(tmp.SN, tmp);
            }
            foreach (var t in Global.g_Model.lst_Table_GYX.Values)
            {
                CTableGYX tmp = t.MyClone();
                tmp.SN = lst_Table_GYX.Count;
                tmp.DeviceName = Global.g_strSelfName;
                tmp.ModelName = Global.g_Model.ModelName;
                lst_Table_GYX.Add(tmp.SN, tmp);
            }
            foreach (var t in Global.g_Model.lst_Table_DYX.Values)
            {
                CTableDYX tmp = t.MyClone();
                tmp.SN = lst_Table_DYX_1.Count;
                tmp.DeviceName = Global.g_strSelfName;
                tmp.ModelName = Global.g_Model.ModelName;
                lst_Table_DYX_1.Add(tmp.SN, tmp);
            }
            foreach (var t in Global.g_Model.lst_Table_YK.Values)
            {
                CTableYK tmp = t.MyClone();
                tmp.SN = lst_Table_YK_1.Count;
                tmp.DeviceName = Global.g_strSelfName;
                tmp.ModelName = Global.g_Model.ModelName;
                lst_Table_YK_1.Add(tmp.SN, tmp);
            }
            foreach (var t in Global.g_Model.lst_Table_Meter.Values)
            {
                CTableMeter tmp = t.MyClone();
                tmp.SN = lst_Table_Meter_1.Count;
                tmp.DeviceName = Global.g_strSelfName;
                tmp.ModelName = Global.g_Model.ModelName;
                lst_Table_Meter_1.Add(tmp.SN, tmp);
            }
            //----
            foreach (var k in Global.g_list_DeviceTable.Values)
            {
                foreach (var m in k.lst_Device.Values)
                {
                    foreach (var g in Global.g_list_Model.Values)
                    {
                        if (m.ModelName == g.ModelName)
                        {
                            foreach (var t in g.lst_Table_YC.Values)
                            {
                                CTableYC tmp = t.MyClone();
                                tmp.SN = lst_Table_YC_1.Count;
                                tmp.DeviceName = m.DeviceName;
                                tmp.ModelName = m.ModelName;
                                lst_Table_YC_1.Add(tmp.SN, tmp);
                            }
                            //----单点遥信
                            foreach (var t in g.lst_Table_SYX.Values)
                            {
                                CTableSYX tmp = t.MyClone();
                                tmp.SN = lst_Table_SYX_1.Count;
                                tmp.DeviceName = m.DeviceName;
                                tmp.ModelName = m.ModelName;
                                lst_Table_SYX_1.Add(tmp.SN, tmp);
                            }
                            foreach (var t in g.lst_Table_GYX.Values)
                            {
                                CTableGYX tmp = t.MyClone();
                                tmp.SN = lst_Table_GYX.Count;
                                tmp.DeviceName = m.DeviceName;
                                tmp.ModelName = m.ModelName;
                                lst_Table_GYX.Add(tmp.SN, tmp);
                            }
                            foreach (var t in g.lst_Table_DYX.Values)
                            {
                                CTableDYX tmp = t.MyClone();
                                tmp.SN = lst_Table_DYX_1.Count;
                                tmp.DeviceName = m.DeviceName;
                                tmp.ModelName = m.ModelName;
                                lst_Table_DYX_1.Add(tmp.SN, tmp);
                            }
                            foreach (var t in g.lst_Table_YK.Values)
                            {
                                CTableYK tmp = t.MyClone();
                                tmp.SN = lst_Table_YK_1.Count;
                                tmp.DeviceName = m.DeviceName;
                                tmp.ModelName = m.ModelName;
                                lst_Table_YK_1.Add(tmp.SN, tmp);
                            }
                            foreach (var t in g.lst_Table_Meter.Values)
                            {
                                CTableMeter tmp = t.MyClone();
                                tmp.SN = lst_Table_Meter_1.Count;
                                tmp.DeviceName = m.DeviceName;
                                tmp.ModelName = m.ModelName;
                                lst_Table_Meter_1.Add(tmp.SN, tmp);
                            }
                        }
                    }
                }
            }
        }

        public void sourceClone_AutoConfig()
        {
            foreach (var t in Global.g_Model.lst_Table_YC.Values)
            {
                CTableYC tmp = t.MyClone();
                tmp.SN = lst_Table_YC_1.Count;
                tmp.DeviceName = Global.g_strSelfName;
                tmp.ModelName = Global.g_Model.ModelName;
                tmp.FlagDelete = true;
                lst_Table_YC_1.Add(tmp.SN, tmp);

                CTableYC tmp2 = t.MyClone();
                tmp2.SN = lst_Table_YC_2.Count;
                tmp2.DeviceName = Global.g_strSelfName;
                tmp2.ModelName = Global.g_Model.ModelName;
                tmp2.FlagDelete = true;
                lst_Table_YC_2.Add(tmp2.SN, tmp2);
            }
            foreach (var t in Global.g_Model.lst_Table_SYX.Values)
            {
                CTableSYX tmp = t.MyClone();
                tmp.SN = lst_Table_SYX_1.Count;
                tmp.DeviceName = Global.g_strSelfName;
                tmp.ModelName = Global.g_Model.ModelName;
                tmp.FlagDelete = true;
                lst_Table_SYX_1.Add(tmp.SN, tmp);

                CTableSYX tmp2 = t.MyClone();
                tmp2.SN = lst_Table_SYX_2.Count;
                tmp2.DeviceName = Global.g_strSelfName;
                tmp2.ModelName = Global.g_Model.ModelName;
                tmp2.FlagDelete = true;
                lst_Table_SYX_2.Add(tmp2.SN, tmp2);
            }
            foreach (var t in Global.g_Model.lst_Table_DYX.Values)
            {
                CTableDYX tmp = t.MyClone();
                tmp.SN = lst_Table_DYX_1.Count;
                tmp.DeviceName = Global.g_strSelfName;
                tmp.ModelName = Global.g_Model.ModelName;
                tmp.FlagDelete = true;
                lst_Table_DYX_1.Add(tmp.SN, tmp);

                CTableDYX tmp2 = t.MyClone();
                tmp2.SN = lst_Table_DYX_2.Count;
                tmp2.DeviceName = Global.g_strSelfName;
                tmp2.ModelName = Global.g_Model.ModelName;
                tmp2.FlagDelete = true;
                lst_Table_DYX_2.Add(tmp2.SN, tmp2);
            }
            foreach (var t in Global.g_Model.lst_Table_YK.Values)
            {
                CTableYK tmp = t.MyClone();
                tmp.SN = lst_Table_YK_1.Count;
                tmp.DeviceName = Global.g_strSelfName;
                tmp.ModelName = Global.g_Model.ModelName;
                tmp.FlagDelete = true;
                lst_Table_YK_1.Add(tmp.SN, tmp);

                CTableYK tmp2 = t.MyClone();
                tmp2.SN = lst_Table_YK_2.Count;
                tmp2.DeviceName = Global.g_strSelfName;
                tmp2.ModelName = Global.g_Model.ModelName;
                tmp2.FlagDelete = true;
                lst_Table_YK_2.Add(tmp2.SN, tmp2);
            }
            foreach (var t in Global.g_Model.lst_Table_Meter.Values)
            {
                CTableMeter tmp = t.MyClone();
                tmp.SN = lst_Table_Meter_1.Count;
                tmp.DeviceName = Global.g_strSelfName;
                tmp.ModelName = Global.g_Model.ModelName;
                tmp.FlagDelete = true;
                lst_Table_Meter_1.Add(tmp.SN, tmp);

                CTableMeter tmp2 = t.MyClone();
                tmp2.SN = lst_Table_Meter_2.Count;
                tmp2.DeviceName = Global.g_strSelfName;
                tmp2.ModelName = Global.g_Model.ModelName;
                tmp2.FlagDelete = true;
                lst_Table_Meter_2.Add(tmp2.SN, tmp2);
            }
            //----
            foreach (var k in Global.g_list_DeviceTable.Values)
            {
                foreach (var m in k.lst_Device.Values)
                {
                    foreach (var g in Global.g_list_Model.Values)
                    {
                        if (m.ModelName == g.ModelName)
                        {
                            foreach (var t in g.lst_Table_YC.Values)
                            {
                                CTableYC tmp = t.MyClone();
                                tmp.SN = lst_Table_YC_1.Count;
                                tmp.DeviceName = m.DeviceName;
                                tmp.ModelName = m.ModelName;
                                tmp.FlagDelete = true;
                                lst_Table_YC_1.Add(tmp.SN, tmp);

                                CTableYC tmp2 = t.MyClone();
                                tmp2.SN = lst_Table_YC_2.Count;
                                tmp2.DeviceName = m.DeviceName;
                                tmp2.ModelName = m.ModelName;
                                tmp2.FlagDelete = true;
                                lst_Table_YC_2.Add(tmp2.SN, tmp2);
                            }
                            //----单点遥信
                            foreach (var t in g.lst_Table_SYX.Values)
                            {
                                CTableSYX tmp = t.MyClone();
                                tmp.SN = lst_Table_SYX_1.Count;
                                tmp.DeviceName = m.DeviceName;
                                tmp.ModelName = m.ModelName;
                                tmp.FlagDelete = true;
                                lst_Table_SYX_1.Add(tmp.SN, tmp);

                                CTableSYX tmp2 = t.MyClone();
                                tmp2.SN = lst_Table_SYX_2.Count;
                                tmp2.DeviceName = m.DeviceName;
                                tmp2.ModelName = m.ModelName;
                                tmp2.FlagDelete = true;
                                lst_Table_SYX_2.Add(tmp2.SN, tmp2);
                            }
                            foreach (var t in g.lst_Table_DYX.Values)
                            {
                                CTableDYX tmp = t.MyClone();
                                tmp.SN = lst_Table_DYX_1.Count;
                                tmp.DeviceName = m.DeviceName;
                                tmp.ModelName = m.ModelName;
                                tmp.FlagDelete = true;
                                lst_Table_DYX_1.Add(tmp.SN, tmp);

                                CTableDYX tmp2 = t.MyClone();
                                tmp2.SN = lst_Table_DYX_2.Count;
                                tmp2.DeviceName = m.DeviceName;
                                tmp2.ModelName = m.ModelName;
                                tmp2.FlagDelete = true;
                                lst_Table_DYX_2.Add(tmp2.SN, tmp2);
                            }
                            foreach (var t in g.lst_Table_YK.Values)
                            {
                                CTableYK tmp = t.MyClone();
                                tmp.SN = lst_Table_YK_1.Count;
                                tmp.DeviceName = m.DeviceName;
                                tmp.ModelName = m.ModelName;
                                tmp.FlagDelete = true;
                                lst_Table_YK_1.Add(tmp.SN, tmp);

                                CTableYK tmp2 = t.MyClone();
                                tmp2.SN = lst_Table_YK_2.Count;
                                tmp2.DeviceName = m.DeviceName;
                                tmp2.ModelName = m.ModelName;
                                tmp2.FlagDelete = true;
                                lst_Table_YK_2.Add(tmp2.SN, tmp2);
                            }
                            foreach (var t in g.lst_Table_Meter.Values)
                            {
                                CTableMeter tmp = t.MyClone();
                                tmp.SN = lst_Table_Meter_1.Count;
                                tmp.DeviceName = m.DeviceName;
                                tmp.ModelName = m.ModelName;
                                tmp.FlagDelete = true;
                                lst_Table_Meter_1.Add(tmp.SN, tmp);

                                CTableMeter tmp2 = t.MyClone();
                                tmp2.SN = lst_Table_Meter_2.Count;
                                tmp2.DeviceName = m.DeviceName;
                                tmp2.ModelName = m.ModelName;
                                tmp2.FlagDelete = true;
                                lst_Table_Meter_2.Add(tmp2.SN, tmp2);
                            }
                        }
                    }
                }
            }
        }
        //-------------------------------------------
        public void deleteDevice(string devicename)
        {
            Queue<int> qi = new Queue<int>();
            foreach (var t in lst_Table_YC_1)
            {
                if(t.Value.DeviceName == devicename)
                {
                    qi.Enqueue(t.Value.SN);
                }
            }
        }
        //-------------------------------------------
        public void convertData()
        {
            setName();
            setNum();
            setStartAddr();
            setIndexArray();
            //setYC();
        }
        public void setName()
        {
            cfg_fwt.sName = FWTName;
        }
        public void setNum()
        {
            cfg_fwt.u32YcNum = Convert.ToUInt32(lst_Table_YC_2.Count);
            cfg_fwt.u32SyxNum = Convert.ToUInt32(lst_Table_SYX_2.Count);
            cfg_fwt.u32DyxNum = Convert.ToUInt32(lst_Table_DYX_2.Count);
            cfg_fwt.u32YkNum = Convert.ToUInt32(lst_Table_YK_2.Count);
            cfg_fwt.u32PowNum = Convert.ToUInt32(lst_Table_Meter_2.Count);
        }
        public void setStartAddr()
        {
            int iCount = 0;
            foreach (var t in lst_Table_YC_2.Values)
            {
                if (iCount == 0)
                {
                    cfg_fwt.u32YcStart = Convert.ToUInt32(t.Addr);
                    break;
                }
            }
            foreach (var t in lst_Table_SYX_2.Values)
            {
                iCount = 0;
                if (iCount == 0)
                {
                    cfg_fwt.u32SyxStart = Convert.ToUInt32(t.Addr);
                    break;
                }
            }
            foreach (var t in lst_Table_DYX_2.Values)
            {
                iCount = 0;
                if (iCount == 0)
                {
                    cfg_fwt.u32DyxStart = Convert.ToUInt32(t.Addr);
                    break;
                }
            }
            foreach (var t in lst_Table_YK_2.Values)
            {
                iCount = 0;
                if (iCount == 0)
                {
                    cfg_fwt.u32YkStart = Convert.ToUInt32(t.Addr);
                    break;
                }
            }
            foreach (var t in lst_Table_Meter_2.Values)
            {
                iCount = 0;
                if (iCount == 0)
                {
                    cfg_fwt.u32PowStart = Convert.ToUInt32(t.Addr);
                    break;
                }
            }
        }
        public void setIndexArray()
        {
            if (lst_Table_YC_2.Count > Global.iniComm_CN_COMM_FWT_YC_MAX)
            {
                MessageBox.Show(string.Format("转发表[遥测]数目越界[实际数目：{0},上限：{1}]！",
                    lst_Table_YC_2.Count, Global.iniComm_CN_COMM_FWT_YC_MAX), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (lst_Table_SYX_2.Count > Global.iniComm_CN_COMM_FWT_SYX_MAX)
            {
                MessageBox.Show(string.Format("转发表[单点遥信]数目越界[实际数目：{0},上限：{1}]！",
                    lst_Table_SYX_2.Count, Global.iniComm_CN_COMM_FWT_SYX_MAX), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (lst_Table_DYX_2.Count > Global.iniComm_CN_COMM_FWT_DYX_MAX)
            {
                MessageBox.Show(string.Format("转发表[双点遥信]数目越界[实际数目：{0},上限：{1}]！",
                    lst_Table_DYX_2.Count, Global.iniComm_CN_COMM_FWT_DYX_MAX), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (lst_Table_YK_2.Count > Global.iniComm_CN_COMM_FWT_YK_MAX)
            {
                MessageBox.Show(string.Format("转发表[遥控]数目越界[实际数目：{0},上限：{1}]！",
                    lst_Table_YK_2.Count, Global.iniComm_CN_COMM_FWT_YK_MAX), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (lst_Table_Meter_2.Count > Global.iniComm_CN_COMM_FWT_POW_MAX)
            {
                MessageBox.Show(string.Format("转发表[计量值]数目越界[实际数目：{0},上限：{1}]！",
                    lst_Table_Meter_2.Count, Global.iniComm_CN_COMM_FWT_POW_MAX), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int iCount = 0;
            foreach (var t in lst_Table_YC_2)
            {
                if (iCount > (Global.iniComm_CN_COMM_FWT_YC_MAX - 1)) { break; }
                cfg_fwt.u32YcIndex[iCount] = Convert.ToUInt32(t.Value.SN);
                cfg_fwt.u32YcGroup[iCount] = Convert.ToUInt32(t.Value.iGroup);
                //----南网需求
                cfg_fwt.f32YcZone[iCount] = t.Value.fYcZone;
                cfg_fwt.f32YcCoe[iCount] = t.Value.fYcCoe;
                iCount += 1;
            }
            iCount = 0;
            foreach (var t in lst_Table_SYX_2)
            {
                if (iCount > (Global.iniComm_CN_COMM_FWT_SYX_MAX - 1)) { break; }
                cfg_fwt.u32SyxIndex[iCount] = Convert.ToUInt32(t.Value.SN);
                iCount += 1;
            }
            //----
            iCount = 0;
            foreach (var t in lst_Table_SYX_2)
            {
                if (iCount > (Global.iniComm_CN_COMM_FWT_SYX_MAX - 1)) { break; }
                cfg_fwt.u32SyxAddr[iCount] = Convert.ToUInt32(t.Value.Addr);
                iCount += 1;
            }
            iCount = 0;
            foreach (var t in lst_Table_DYX_2)
            {
                if (iCount > (Global.iniComm_CN_COMM_FWT_DYX_MAX - 1)) { break; }
                cfg_fwt.u32DyxAddr[iCount] = Convert.ToUInt32(t.Value.Addr);
                iCount += 1;
            }
            //----
            iCount = 0;
            foreach (var t in lst_Table_DYX_2)
            {
                if (iCount > (Global.iniComm_CN_COMM_FWT_DYX_MAX - 1)) { break; }
                cfg_fwt.u32DyxIndex[iCount] = Convert.ToUInt32(t.Value.SN);
                iCount += 1;
            }
            iCount = 0;
            foreach (var t in lst_Table_YK_2)
            {
                if (iCount > (Global.iniComm_CN_COMM_FWT_YK_MAX - 1)) { break; }
                cfg_fwt.u32YkIndex[iCount] = Convert.ToUInt32(t.Value.SN);
                iCount += 1;
            }
            iCount = 0;
            foreach (var t in lst_Table_Meter_2)
            {
                if (iCount > (Global.iniComm_CN_COMM_FWT_POW_MAX - 1)) { break; }
                cfg_fwt.u32PowIndex[iCount] = Convert.ToUInt32(t.Value.SN);
                iCount += 1;
            }

        }
        public void setYC()
        {
            //int iPos = 0;
            //cfg_fwt.u32YcMax[iPos++] = Convert.ToUInt32(I_MAX);
            //cfg_fwt.u32YcMax[iPos++] = Convert.ToUInt32(V_MAX);
            //cfg_fwt.u32YcMax[iPos++] = Convert.ToUInt32(DC_MAX);
            //cfg_fwt.u32YcMax[iPos++] = Convert.ToUInt32(P_MAX);
            //cfg_fwt.u32YcMax[iPos++] = Convert.ToUInt32(FR_MAX);
            //cfg_fwt.u32YcMax[iPos++] = Convert.ToUInt32(COS_MAX);
            //iPos = 0;
            //cfg_fwt.u32YcCoe[iPos++] = Convert.ToUInt32(I_COE);
            //cfg_fwt.u32YcCoe[iPos++] = Convert.ToUInt32(V_COE);
            //cfg_fwt.u32YcCoe[iPos++] = Convert.ToUInt32(DC_COE);
            //cfg_fwt.u32YcCoe[iPos++] = Convert.ToUInt32(P_COE);
            //cfg_fwt.u32YcCoe[iPos++] = Convert.ToUInt32(FR_COE);
            //cfg_fwt.u32YcCoe[iPos++] = Convert.ToUInt32(COS_COE);
        }
        public void getYC(ref FwtCfg_t fwtcfg)
        {
            //I_MAX = Convert.ToInt32(fwtcfg.u32YcMax[0]);
            //V_MAX = Convert.ToInt32(fwtcfg.u32YcMax[1]);
            //DC_MAX = Convert.ToInt32(fwtcfg.u32YcMax[2]);
            //P_MAX = Convert.ToInt32(fwtcfg.u32YcMax[3]);
            //FR_MAX = Convert.ToInt32(fwtcfg.u32YcMax[4]);
            //COS_MAX = Convert.ToInt32(fwtcfg.u32YcMax[5]);
            ////----
            //I_COE = Convert.ToInt32(fwtcfg.u32YcCoe[0]);
            //V_COE = Convert.ToInt32(fwtcfg.u32YcCoe[1]);
            //DC_COE = Convert.ToInt32(fwtcfg.u32YcCoe[2]);
            //P_COE = Convert.ToInt32(fwtcfg.u32YcCoe[3]);
            //FR_COE = Convert.ToInt32(fwtcfg.u32YcCoe[4]);
            //COS_COE = Convert.ToInt32(fwtcfg.u32YcCoe[5]);
        }
    }
    #endregion

    #region "eFwtType"
    public enum eFwtType
    {
        E_FWT_TYPE_YC = 0,
        E_FWT_TYPE_SYX,
        E_FWT_TYPE_DYX,
        E_FWT_TYPE_YK,
        E_FWT_TYPE_Meter
    }
    #endregion
}
