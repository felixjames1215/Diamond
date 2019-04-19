using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace CfgTool
{
    public class PortCfg
    {
        public PortCfg() //构造函数，置默认值
        {
            tPort = new PORT_T();
            eProtocol = UserProtocolType_e.EN_PROTOCOL_101_UP;
            u8ProtocolInstNo = 0;
            u8Res1[0] = 0x00; 
            u8Res1[1] = 0x00;
            for (int k = 0; k < u8ProtocolCfg.Length; k++) { u8ProtocolCfg[k] = 0x00; }
            u32FwtIndex = 0;
            u32DevsIndex = 0;
        }

        public PORT_T tPort;
        public UserProtocolType_e eProtocol;//规约

        public bool bUsed;//是否使用。启用；禁用
        public byte u8ProtocolInstNo;//规约实例号
        public byte[] u8Res1 = new byte[2];//占位
        //------------------------------------------------------------
        public CommB101UpCfg_t tCommB101UpCfg = new CommB101UpCfg_t();
        public CommB104UpCfg_t tCommB104UpCfg = new CommB104UpCfg_t();
        //------------------------------------------------------------
        public byte[] u8ProtocolCfg = new byte[Global.iniComm_CN_COMM_PROTOCOLCFG_SIZE];
        public UInt32 u32FwtIndex;//转发表序号(从0开始，顺序编号)
        public UInt32 u32DevsIndex;//设备表序号(从0开始，顺序编号)
        //----END
        public void assignValue_FWT(string fwtname)
        {
            getFwtIndex(fwtname);
        }
        public void assignValue_Device(string devicetablename)
        {
            getDevsIndex(devicetablename);
        }
        public void getFwtIndex(string fwtname)
        {
            u32FwtIndex = 0;//初始化
            foreach(var t in Global.g_list_FWT)
            {
                if (fwtname == t.Value.FWTName)
                {
                    u32FwtIndex = Convert.ToUInt32(t.Value.SN);
                    break;
                }
            }
        }
        public void getDevsIndex(string devicetablename)
        {
            u32DevsIndex = 0;
            foreach (var t in Global.g_list_DeviceTable)//遍历设备表
            {
                if (devicetablename == t.Value.DeviceTableName)
                {
                    u32DevsIndex = Convert.ToUInt32(t.Value.SN);
                    break;
                }
            }
        }
        //----
        public string getFWTName()
        {
            string FwtName = "";
            foreach (var t in Global.g_list_FWT)
            {
                if (u32FwtIndex == t.Value.SN)
                {
                    FwtName = t.Value.FWTName;
                    break;
                }
            }
            return FwtName;
        }
        public string getDeviceTableName()
        {
            string DeviceTableName = "";
            foreach (var t in Global.g_list_DeviceTable)
            {
                if (u32DevsIndex == t.Value.SN)
                {
                    DeviceTableName = t.Value.DeviceTableName;
                }
            }
            return DeviceTableName;
        }
        //----
        public void convertProtocolCfg2ByteArray(UserProtocolType_e pt)
        {
            int iPos = 0;
            if(pt == UserProtocolType_e.EN_PROTOCOL_101_UP ||
               pt == UserProtocolType_e.EN_PROTOCOL_101_UP_V1 || //老版信息安全，2017-11-16 16:55
               pt == UserProtocolType_e.EN_PROTOCOL_101_DOWN ||
               pt == UserProtocolType_e.EN_PROTOCOL_MODBUS_DOWN ||
               pt == UserProtocolType_e.EN_PROTOCOL_MODBUS_UP)
            {
                if (Global.g_General_DuplexChannel == 0)
                {
                    //----安全层
                    u8ProtocolCfg[iPos++] = ((tCommB101UpCfg.tSafetyLayerCfg.bHaveSafetyLayer == false) ? (byte)0 : (byte)1);
                    u8ProtocolCfg[iPos++] = tCommB101UpCfg.tSafetyLayerCfg.u8AppProtocol;
                    byte[] b0 = BitConverter.GetBytes(tCommB101UpCfg.tSafetyLayerCfg.u16TimeDiffLimit);
                    u8ProtocolCfg[iPos++] = b0[0];
                    u8ProtocolCfg[iPos++] = b0[1];
                    //----链路层
                    u8ProtocolCfg[iPos++] = tCommB101UpCfg.tLinkCfg.u8Mode;
                    u8ProtocolCfg[iPos++] = tCommB101UpCfg.tLinkCfg.u8StationType;
                    byte[] b = BitConverter.GetBytes(tCommB101UpCfg.tLinkCfg.u16LinkAddr);
                    u8ProtocolCfg[iPos++] = b[0];
                    u8ProtocolCfg[iPos++] = b[1];
                    u8ProtocolCfg[iPos++] = tCommB101UpCfg.tLinkCfg.u8LinkAddrLen;
                    u8ProtocolCfg[iPos++] = tCommB101UpCfg.tLinkCfg.u8ResendInterval;
                    u8ProtocolCfg[iPos++] = tCommB101UpCfg.tLinkCfg.u8ResendNum;
                    u8ProtocolCfg[iPos++] = tCommB101UpCfg.tLinkCfg.u8Dir;
                    //----应用层
                    byte[] b2 = BitConverter.GetBytes(tCommB101UpCfg.tAppCfg.u16AppAddr);
                    u8ProtocolCfg[iPos++] = b2[0];
                    u8ProtocolCfg[iPos++] = b2[1];
                    u8ProtocolCfg[iPos++] = tCommB101UpCfg.tAppCfg.u8AppAddrLen;
                    u8ProtocolCfg[iPos++] = tCommB101UpCfg.tAppCfg.u8CotLen;
                    //----
                    u8ProtocolCfg[iPos++] = tCommB101UpCfg.tAppCfg.u8ObjAddrLen;
                    u8ProtocolCfg[iPos++] = tCommB101UpCfg.tAppCfg.u8YcSendTYP;
                    byte[] b2_1 = BitConverter.GetBytes(tCommB101UpCfg.tAppCfg.u16YcChgSendCycle);
                    u8ProtocolCfg[iPos++] = b2_1[0];
                    u8ProtocolCfg[iPos++] = b2_1[1];
                    //----
                    byte[] b3 = BitConverter.GetBytes(tCommB101UpCfg.tAppCfg.u16YkValidTime);
                    u8ProtocolCfg[iPos++] = b3[0];
                    u8ProtocolCfg[iPos++] = b3[1];
                    byte[] b4 = BitConverter.GetBytes(tCommB101UpCfg.tAppCfg.u16IC_Time);
                    u8ProtocolCfg[iPos++] = b4[0];
                    u8ProtocolCfg[iPos++] = b4[1];
                    //----
                    byte[] b5 = BitConverter.GetBytes(tCommB101UpCfg.tAppCfg.u16CI_Time);
                    u8ProtocolCfg[iPos++] = b5[0];
                    u8ProtocolCfg[iPos++] = b5[1];
                    //----
                    byte[] b6 = BitConverter.GetBytes(tCommB101UpCfg.tAppCfg.u16CmdTime);
                    u8ProtocolCfg[iPos++] = b6[0];
                    u8ProtocolCfg[iPos++] = b6[1];
                }
                else
                {
                    //----安全层
                    u8ProtocolCfg[iPos++] = ((tCommB101UpCfg.tSafetyLayerCfg.bHaveSafetyLayer == false) ? (byte)0 : (byte)1);
                    u8ProtocolCfg[iPos++] = tCommB101UpCfg.tSafetyLayerCfg.u8AppProtocol;
                    byte[] b0 = BitConverter.GetBytes(tCommB101UpCfg.tSafetyLayerCfg.u16TimeDiffLimit);
                    u8ProtocolCfg[iPos++] = b0[0];
                    u8ProtocolCfg[iPos++] = b0[1];
                    //已经有4字节，再增加12字节占位，jifeng，2018-8-24 9:17
                    for (int m = 0; m < 12; m++) { u8ProtocolCfg[iPos++] = 0x00; }
                    //----链路层
                    u8ProtocolCfg[iPos++] = tCommB101UpCfg.tLinkCfg.u8Mode;
                    u8ProtocolCfg[iPos++] = tCommB101UpCfg.tLinkCfg.u8StationType;
                    byte[] b = BitConverter.GetBytes(tCommB101UpCfg.tLinkCfg.u16LinkAddr);
                    u8ProtocolCfg[iPos++] = b[0];
                    u8ProtocolCfg[iPos++] = b[1];
                    u8ProtocolCfg[iPos++] = tCommB101UpCfg.tLinkCfg.u8LinkAddrLen;
                    u8ProtocolCfg[iPos++] = tCommB101UpCfg.tLinkCfg.u8ResendInterval;
                    u8ProtocolCfg[iPos++] = tCommB101UpCfg.tLinkCfg.u8ResendNum;
                    u8ProtocolCfg[iPos++] = tCommB101UpCfg.tLinkCfg.u8Dir;
                    //已经有8字节，再增加56字节占位，jifeng，2018-8-24 9:22
                    for (int m = 0; m < 56; m++) { u8ProtocolCfg[iPos++] = 0x00; }
                    //----应用层
                    byte[] b2 = BitConverter.GetBytes(tCommB101UpCfg.tAppCfg.u16AppAddr);
                    u8ProtocolCfg[iPos++] = b2[0];
                    u8ProtocolCfg[iPos++] = b2[1];
                    u8ProtocolCfg[iPos++] = tCommB101UpCfg.tAppCfg.u8AppAddrLen;
                    u8ProtocolCfg[iPos++] = tCommB101UpCfg.tAppCfg.u8CotLen;
                    //----
                    u8ProtocolCfg[iPos++] = tCommB101UpCfg.tAppCfg.u8ObjAddrLen;
                    u8ProtocolCfg[iPos++] = tCommB101UpCfg.tAppCfg.u8YcSendTYP;
                    byte[] b2_1 = BitConverter.GetBytes(tCommB101UpCfg.tAppCfg.u16YcChgSendCycle);
                    u8ProtocolCfg[iPos++] = b2_1[0];
                    u8ProtocolCfg[iPos++] = b2_1[1];
                    //----
                    byte[] b3 = BitConverter.GetBytes(tCommB101UpCfg.tAppCfg.u16YkValidTime);
                    u8ProtocolCfg[iPos++] = b3[0];
                    u8ProtocolCfg[iPos++] = b3[1];
                    byte[] b4 = BitConverter.GetBytes(tCommB101UpCfg.tAppCfg.u16IC_Time);
                    u8ProtocolCfg[iPos++] = b4[0];
                    u8ProtocolCfg[iPos++] = b4[1];
                    //----
                    byte[] b5 = BitConverter.GetBytes(tCommB101UpCfg.tAppCfg.u16CI_Time);
                    u8ProtocolCfg[iPos++] = b5[0];
                    u8ProtocolCfg[iPos++] = b5[1];
                    //----
                    byte[] b6 = BitConverter.GetBytes(tCommB101UpCfg.tAppCfg.u16CmdTime);
                    u8ProtocolCfg[iPos++] = b6[0];
                    u8ProtocolCfg[iPos++] = b6[1];
                    //已经有16字节，再增加32字节占位，jifeng，2018-8-24 9:34
                    for (int m = 0; m < 32; m++) { u8ProtocolCfg[iPos++] = 0x00; }
                }
            }
            else if (pt == UserProtocolType_e.EN_PROTOCOL_104_UP ||
                pt == UserProtocolType_e.EN_PROTOCOL_104_UP_V1 || //老版信息安全，2017-11-16 16:55
                pt == UserProtocolType_e.EN_PROTOCOL_104_DOWN)
            {
                if (Global.g_General_DuplexChannel == 0)
                {
                    u8ProtocolCfg[iPos++] = ((tCommB104UpCfg.tSafetyLayerCfg.bHaveSafetyLayer == false) ? (byte)0 : (byte)1);
                    u8ProtocolCfg[iPos++] = tCommB104UpCfg.tSafetyLayerCfg.u8AppProtocol;
                    byte[] b0 = BitConverter.GetBytes(tCommB104UpCfg.tSafetyLayerCfg.u16TimeDiffLimit);
                    u8ProtocolCfg[iPos++] = b0[0];
                    u8ProtocolCfg[iPos++] = b0[1];
                    //----链路层
                    for (int k = 0; k < 4; k++) { u8ProtocolCfg[iPos++] = tCommB104UpCfg.tLinkCfg.u8ServerIP[k]; }
                    for (int k = 0; k < 4; k++) { u8ProtocolCfg[iPos++] = tCommB104UpCfg.tLinkCfg.u8ClientIP[k]; }
                    for (int k = 0; k < 4; k++) { u8ProtocolCfg[iPos++] = tCommB104UpCfg.tLinkCfg.u8GateIP[k]; }
                    //----
                    byte[] b1 = BitConverter.GetBytes(tCommB104UpCfg.tLinkCfg.u16ServerPort);
                    u8ProtocolCfg[iPos++] = b1[0];
                    u8ProtocolCfg[iPos++] = b1[1];
                    u8ProtocolCfg[iPos++] = Convert.ToByte(tCommB104UpCfg.tLinkCfg.u8Protocol);
                    u8ProtocolCfg[iPos++] = Convert.ToByte(tCommB104UpCfg.tLinkCfg.u8LinkType);
                    u8ProtocolCfg[iPos++] = Convert.ToByte(tCommB104UpCfg.tLinkCfg.u8StationType);
                    u8ProtocolCfg[iPos++] = tCommB104UpCfg.tLinkCfg.u8Res1[0];
                    u8ProtocolCfg[iPos++] = tCommB104UpCfg.tLinkCfg.u8Res1[1];
                    u8ProtocolCfg[iPos++] = tCommB104UpCfg.tLinkCfg.u8Res1[2];
                    //----
                    u8ProtocolCfg[iPos++] = tCommB104UpCfg.tLinkCfg.u8K;
                    u8ProtocolCfg[iPos++] = tCommB104UpCfg.tLinkCfg.u8W;
                    u8ProtocolCfg[iPos++] = tCommB104UpCfg.tLinkCfg.u8EnableKW;
                    u8ProtocolCfg[iPos++] = tCommB104UpCfg.tLinkCfg.u8EnableT1;
                    //----
                    u8ProtocolCfg[iPos++] = tCommB104UpCfg.tLinkCfg.u8T0;
                    u8ProtocolCfg[iPos++] = tCommB104UpCfg.tLinkCfg.u8T1;
                    u8ProtocolCfg[iPos++] = tCommB104UpCfg.tLinkCfg.u8T2;
                    u8ProtocolCfg[iPos++] = tCommB104UpCfg.tLinkCfg.u8T3;
                    //----应用层(与101的一样)
                    byte[] b2 = BitConverter.GetBytes(tCommB104UpCfg.tAppCfg.u16AppAddr);
                    u8ProtocolCfg[iPos++] = b2[0];
                    u8ProtocolCfg[iPos++] = b2[1];
                    u8ProtocolCfg[iPos++] = tCommB104UpCfg.tAppCfg.u8AppAddrLen;
                    u8ProtocolCfg[iPos++] = tCommB104UpCfg.tAppCfg.u8CotLen;
                    //----
                    u8ProtocolCfg[iPos++] = tCommB104UpCfg.tAppCfg.u8ObjAddrLen;
                    u8ProtocolCfg[iPos++] = tCommB104UpCfg.tAppCfg.u8YcSendTYP;
                    byte[] b2_1 = BitConverter.GetBytes(tCommB104UpCfg.tAppCfg.u16YcChgSendCycle);
                    u8ProtocolCfg[iPos++] = b2_1[0];
                    u8ProtocolCfg[iPos++] = b2_1[1];
                    //----
                    byte[] b3 = BitConverter.GetBytes(tCommB104UpCfg.tAppCfg.u16YkValidTime);
                    u8ProtocolCfg[iPos++] = b3[0];
                    u8ProtocolCfg[iPos++] = b3[1];
                    byte[] b4 = BitConverter.GetBytes(tCommB104UpCfg.tAppCfg.u16IC_Time);
                    u8ProtocolCfg[iPos++] = b4[0];
                    u8ProtocolCfg[iPos++] = b4[1];
                    //----
                    byte[] b5 = BitConverter.GetBytes(tCommB104UpCfg.tAppCfg.u16CI_Time);
                    u8ProtocolCfg[iPos++] = b5[0];
                    u8ProtocolCfg[iPos++] = b5[1];
                    //----
                    byte[] b6 = BitConverter.GetBytes(tCommB104UpCfg.tAppCfg.u16CmdTime);
                    u8ProtocolCfg[iPos++] = b6[0];
                    u8ProtocolCfg[iPos++] = b6[1];
                }
                else
                {
                    u8ProtocolCfg[iPos++] = ((tCommB104UpCfg.tSafetyLayerCfg.bHaveSafetyLayer == false) ? (byte)0 : (byte)1);
                    u8ProtocolCfg[iPos++] = tCommB104UpCfg.tSafetyLayerCfg.u8AppProtocol;
                    byte[] b0 = BitConverter.GetBytes(tCommB104UpCfg.tSafetyLayerCfg.u16TimeDiffLimit);
                    u8ProtocolCfg[iPos++] = b0[0];
                    u8ProtocolCfg[iPos++] = b0[1];
                    //已经有4字节，再增加12字节占位，jifeng，2018-8-24 9:17
                    for (int k = 0; k < 12; k++) { u8ProtocolCfg[iPos++] = 0x00; }
                    //----链路层
                    for (int k = 0; k < 4; k++) { u8ProtocolCfg[iPos++] = tCommB104UpCfg.tLinkCfg.u8ServerIP[k]; }
                    u8ProtocolCfg[iPos++] = tCommB104UpCfg.tLinkCfg.u8DuplexChannel;
                    for (int k = 0; k < 3; k++) { u8ProtocolCfg[iPos++] = 0x00; }

                    for (int k = 0; k < 4; k++) { u8ProtocolCfg[iPos++] = tCommB104UpCfg.tLinkCfg.u8ClientIP[k]; }
                    for (int k = 0; k < 4; k++) { u8ProtocolCfg[iPos++] = tCommB104UpCfg.tLinkCfg.u8ClientIP2[k]; }

                    for (int k = 0; k < 4; k++) { u8ProtocolCfg[iPos++] = tCommB104UpCfg.tLinkCfg.u8GateIP[k]; }
                    for (int k = 0; k < 4; k++) { u8ProtocolCfg[iPos++] = tCommB104UpCfg.tLinkCfg.u8GateIP2[k]; }

                    byte[] b1 = BitConverter.GetBytes(tCommB104UpCfg.tLinkCfg.u16ServerPort);
                    u8ProtocolCfg[iPos++] = b1[0];
                    u8ProtocolCfg[iPos++] = b1[1];
                    byte[] b12 = BitConverter.GetBytes(tCommB104UpCfg.tLinkCfg.u16ServerPort2);
                    u8ProtocolCfg[iPos++] = b12[0];
                    u8ProtocolCfg[iPos++] = b12[1];

                    u8ProtocolCfg[iPos++] = Convert.ToByte(tCommB104UpCfg.tLinkCfg.u8Protocol);
                    u8ProtocolCfg[iPos++] = Convert.ToByte(tCommB104UpCfg.tLinkCfg.u8LinkType);
                    u8ProtocolCfg[iPos++] = Convert.ToByte(tCommB104UpCfg.tLinkCfg.u8StationType);
                    for (int m = 0; m < 1; m++) { u8ProtocolCfg[iPos++] = 0x00; }

                    u8ProtocolCfg[iPos++] = tCommB104UpCfg.tLinkCfg.u8K;
                    u8ProtocolCfg[iPos++] = tCommB104UpCfg.tLinkCfg.u8W;
                    u8ProtocolCfg[iPos++] = tCommB104UpCfg.tLinkCfg.u8EnableKW;
                    u8ProtocolCfg[iPos++] = tCommB104UpCfg.tLinkCfg.u8EnableT1;

                    u8ProtocolCfg[iPos++] = tCommB104UpCfg.tLinkCfg.u8T0;
                    u8ProtocolCfg[iPos++] = tCommB104UpCfg.tLinkCfg.u8T1;
                    u8ProtocolCfg[iPos++] = tCommB104UpCfg.tLinkCfg.u8T2;
                    u8ProtocolCfg[iPos++] = tCommB104UpCfg.tLinkCfg.u8T3;
                    //已经有40字节，再增加24字节占位，jifeng，2018-8-24 9:59
                    for (int m = 0; m < 24; m++) { u8ProtocolCfg[iPos++] = 0x00; }
                    //----应用层(与101的一样)
                    byte[] b2 = BitConverter.GetBytes(tCommB104UpCfg.tAppCfg.u16AppAddr);
                    u8ProtocolCfg[iPos++] = b2[0];
                    u8ProtocolCfg[iPos++] = b2[1];
                    u8ProtocolCfg[iPos++] = tCommB104UpCfg.tAppCfg.u8AppAddrLen;
                    u8ProtocolCfg[iPos++] = tCommB104UpCfg.tAppCfg.u8CotLen;
                    //----
                    u8ProtocolCfg[iPos++] = tCommB104UpCfg.tAppCfg.u8ObjAddrLen;
                    u8ProtocolCfg[iPos++] = tCommB104UpCfg.tAppCfg.u8YcSendTYP;
                    byte[] b2_1 = BitConverter.GetBytes(tCommB104UpCfg.tAppCfg.u16YcChgSendCycle);
                    u8ProtocolCfg[iPos++] = b2_1[0];
                    u8ProtocolCfg[iPos++] = b2_1[1];
                    //----
                    byte[] b3 = BitConverter.GetBytes(tCommB104UpCfg.tAppCfg.u16YkValidTime);
                    u8ProtocolCfg[iPos++] = b3[0];
                    u8ProtocolCfg[iPos++] = b3[1];
                    byte[] b4 = BitConverter.GetBytes(tCommB104UpCfg.tAppCfg.u16IC_Time);
                    u8ProtocolCfg[iPos++] = b4[0];
                    u8ProtocolCfg[iPos++] = b4[1];
                    //----
                    byte[] b5 = BitConverter.GetBytes(tCommB104UpCfg.tAppCfg.u16CI_Time);
                    u8ProtocolCfg[iPos++] = b5[0];
                    u8ProtocolCfg[iPos++] = b5[1];
                    //----
                    byte[] b6 = BitConverter.GetBytes(tCommB104UpCfg.tAppCfg.u16CmdTime);
                    u8ProtocolCfg[iPos++] = b6[0];
                    u8ProtocolCfg[iPos++] = b6[1];
                    //已经有16字节，再增加32字节占位，jifeng，2018-8-24 9:47
                    for (int m = 0; m < 32; m++) { u8ProtocolCfg[iPos++] = 0x00; }
                }
            }
            //----END
        }

        public void write_bin(BinaryWriter bw)
        {
            //----每个总计：160字节
            bw.Write(tPort.u16PortAddr);//端口地址
            bw.Write(tPort.u8PhyAttr);//物理属性
            bw.Write(tPort.u8LogicAttr);//逻辑属性
            if (Global.bPrintPort == true)
            {
                string ssss = string.Format("part 1: {0}, {1}, {2}", tPort.u16PortAddr, tPort.u8PhyAttr, tPort.u8LogicAttr);
                Form_CfgTool.pMainForm.formInfo.LogMessage2(ssss);
            }
            if (tPort.u8PhyAttr == 0x01)//串口
            {
                bw.Write(tPort.SPort.u32BaudRate);//波特率
                bw.Write(tPort.SPort.u8DataBit);//数据位
                bw.Write(tPort.SPort.u8StopBit);//停止位
                bw.Write(tPort.SPort.u8CheckBit);//校验位
                bw.Write(tPort.SPort.u8Res1[0]);//占位：1字节
                //----
                bw.Write(tPort.SPort.u8Res2[0]);//占位：4字节
                bw.Write(tPort.SPort.u8Res2[1]);
                bw.Write(tPort.SPort.u8Res2[2]);
                bw.Write(tPort.SPort.u8Res2[3]);
                //----
                bw.Write(tPort.SPort.u8Res3[0]);//占位：4字节
                bw.Write(tPort.SPort.u8Res3[1]);
                bw.Write(tPort.SPort.u8Res3[2]);
                bw.Write(tPort.SPort.u8Res3[3]);

                if (Global.bPrintPort == true)
                {
                    string ssss = string.Format("part 2 serial: {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}",
                       tPort.SPort.u32BaudRate, tPort.SPort.u8DataBit, tPort.SPort.u8StopBit, tPort.SPort.u8CheckBit, tPort.SPort.u8Res1[0],
                       tPort.SPort.u8Res2[0], tPort.SPort.u8Res2[1], tPort.SPort.u8Res2[2], tPort.SPort.u8Res2[3],
                       tPort.SPort.u8Res3[0], tPort.SPort.u8Res3[1], tPort.SPort.u8Res3[2], tPort.SPort.u8Res3[3]);
                    Form_CfgTool.pMainForm.formInfo.LogMessage2(ssss);
                }
            }
            else if (tPort.u8PhyAttr == 0x02)//网口
            {
                bw.Write(tPort.NPort.u8IP[0]);//IP
                bw.Write(tPort.NPort.u8IP[1]);
                bw.Write(tPort.NPort.u8IP[2]);
                bw.Write(tPort.NPort.u8IP[3]);
                bw.Write(tPort.NPort.u8Mask[0]);//子网掩码
                bw.Write(tPort.NPort.u8Mask[1]);
                bw.Write(tPort.NPort.u8Mask[2]);
                bw.Write(tPort.NPort.u8Mask[3]);
                bw.Write(tPort.NPort.u8MAC[0]);//MAC
                bw.Write(tPort.NPort.u8MAC[1]);
                bw.Write(tPort.NPort.u8MAC[2]);
                bw.Write(tPort.NPort.u8MAC[3]);
                bw.Write(tPort.NPort.u8MAC[4]);
                bw.Write(tPort.NPort.u8MAC[5]);
                bw.Write(tPort.NPort.u8Res1[0]);//占位：2字节
                bw.Write(tPort.NPort.u8Res1[1]);

                if (Global.bPrintPort == true)
                {
                    string ssss = string.Format("part 2 net: {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}",
                       tPort.NPort.u8IP[0], tPort.NPort.u8IP[1], tPort.NPort.u8IP[2], tPort.NPort.u8IP[3],
                       tPort.NPort.u8Mask[0], tPort.NPort.u8Mask[1], tPort.NPort.u8Mask[2], tPort.NPort.u8Mask[3],
                       tPort.NPort.u8MAC[0], tPort.NPort.u8MAC[1], tPort.NPort.u8MAC[2], tPort.NPort.u8MAC[3],tPort.NPort.u8MAC[4],tPort.NPort.u8MAC[5],
                       tPort.NPort.u8Res1[0], tPort.NPort.u8Res1[1]);
                    Form_CfgTool.pMainForm.formInfo.LogMessage2(ssss);
                }
            }
            else
            {
                byte tmp = 0x00;
                bw.Write(tmp);
                bw.Write(tmp);
                bw.Write(tmp);
                bw.Write(tmp);
                bw.Write(tmp);
                bw.Write(tmp);
                bw.Write(tmp);
                bw.Write(tmp);
                bw.Write(tmp);
                bw.Write(tmp);
                bw.Write(tmp);
                bw.Write(tmp);
                bw.Write(tmp);
                bw.Write(tmp);
                bw.Write(tmp);
                bw.Write(tmp);
            }
            //----
            UInt32 u32tmp = Convert.ToUInt32(eProtocol);
            bw.Write(u32tmp);//规约(枚举，占4字节)
            //----
            byte bt = (bUsed == false ? (byte)0 : (byte)1);
            bw.Write(bt);//是否使用
            bw.Write(u8ProtocolInstNo);//规约实例号
            bw.Write(u8Res1[0]);//占位
            bw.Write(u8Res1[1]);
            if (Global.bPrintPort == true)
            {
                string ssss = string.Format("part 3: {0}, {1}, {2}, {3}, {4}",
                   u32tmp, bt, u8ProtocolInstNo, u8Res1[0], u8Res1[1]);
                Form_CfgTool.pMainForm.formInfo.LogMessage2(ssss);
            }
            //----
            convertProtocolCfg2ByteArray(eProtocol);
            for (int k = 0; k < 128; k++)
            {
                bw.Write(u8ProtocolCfg[k]);//对上101规约配置参数
            }
            if (Global.bPrintPort == true)
            {
                string s1 = "";
                string s2 = "";
                for (int k = 0; k < 128; k++)
                {
                    s1 = string.Format("{0:X2} ", u8ProtocolCfg[k]);
                    s2 += s1;
                }
                string ssss = string.Format("part 4: {0}", s2);
                Form_CfgTool.pMainForm.formInfo.LogMessage2(ssss);
            }
            //----以上，合计156字节
            bw.Write(u32FwtIndex);//转发表序号
            bw.Write(u32DevsIndex);//设备表序号
            if (Global.bPrintPort == true)
            {
                string ssss = string.Format("part 5: {0}, {1}",
                   u32FwtIndex, u32DevsIndex);
                Form_CfgTool.pMainForm.formInfo.LogMessage2(ssss);
            }
            //END
        }

        public void read_bin(BinaryReader br)
        {
            tPort.u16PortAddr = br.ReadUInt16();
            tPort.u8PhyAttr = br.ReadByte();
            tPort.u8LogicAttr = br.ReadByte();
            if (tPort.u8PhyAttr == 0x01)//串口
            {
                tPort.SPort.u32BaudRate = br.ReadUInt32();//波特率
                tPort.SPort.u8DataBit = br.ReadByte();//数据位
                tPort.SPort.u8StopBit = br.ReadByte();//停止位
                tPort.SPort.u8CheckBit = br.ReadByte();//校验位
                tPort.SPort.u8Res1[0] = br.ReadByte();//占位1
                tPort.SPort.u8Res2[0] = br.ReadByte();//占位2
                tPort.SPort.u8Res2[1] = br.ReadByte();
                tPort.SPort.u8Res2[2] = br.ReadByte();
                tPort.SPort.u8Res2[3] = br.ReadByte();
                tPort.SPort.u8Res3[0] = br.ReadByte();//占位3
                tPort.SPort.u8Res3[1] = br.ReadByte();
                tPort.SPort.u8Res3[2] = br.ReadByte();
                tPort.SPort.u8Res3[3] = br.ReadByte();
            }
            else if (tPort.u8PhyAttr == 0x02)//网口
            {
                tPort.NPort.u8IP[0] = br.ReadByte();//IP
                tPort.NPort.u8IP[1] = br.ReadByte();
                tPort.NPort.u8IP[2] = br.ReadByte();
                tPort.NPort.u8IP[3] = br.ReadByte();
                tPort.NPort.u8Mask[0] = br.ReadByte();//子网掩码
                tPort.NPort.u8Mask[1] = br.ReadByte();
                tPort.NPort.u8Mask[2] = br.ReadByte();
                tPort.NPort.u8Mask[3] = br.ReadByte();
                tPort.NPort.u8MAC[0] = br.ReadByte();//MAC
                tPort.NPort.u8MAC[1] = br.ReadByte();
                tPort.NPort.u8MAC[2] = br.ReadByte();
                tPort.NPort.u8MAC[3] = br.ReadByte();
                tPort.NPort.u8MAC[4] = br.ReadByte();
                tPort.NPort.u8MAC[5] = br.ReadByte();
                tPort.NPort.u8Res1[0] = br.ReadByte();//占位
                tPort.NPort.u8Res1[1] = br.ReadByte();
            }
            else
            {
                byte bttmp = 0x00;
                bttmp = br.ReadByte();
                bttmp = br.ReadByte();
                bttmp = br.ReadByte();
                bttmp = br.ReadByte();
                bttmp = br.ReadByte();
                bttmp = br.ReadByte();
                bttmp = br.ReadByte();
                bttmp = br.ReadByte();
                bttmp = br.ReadByte();
                bttmp = br.ReadByte();
                bttmp = br.ReadByte();
                bttmp = br.ReadByte();
                bttmp = br.ReadByte();
                bttmp = br.ReadByte();
                bttmp = br.ReadByte();
                bttmp = br.ReadByte();
            }
            //----
            eProtocol = (UserProtocolType_e)br.ReadUInt32();//规约(枚举，占4字节)
            //----
            bUsed = (br.ReadByte() == 0x00 ? false : true);//是否使用
            u8ProtocolInstNo = br.ReadByte();//规约实例号
            u8Res1[0] = br.ReadByte();//占位
            u8Res1[1] = br.ReadByte();
            //----
            for (int k = 0; k < 128; k++)
            {
                u8ProtocolCfg[k] = br.ReadByte();//对上101规约配置参数
            }
            parseProtocolCfg(eProtocol);//解析规约条目(101共有15个条目)
            //----以上总计：156字节
            u32FwtIndex = br.ReadUInt32();//转发表序号，从0开始，顺序编号
            u32DevsIndex = br.ReadUInt32();//设备表序号，从0开始，顺序编号
            //----以上总计：164字节
            //END
        }

        public void parseProtocolCfg(UserProtocolType_e pt)
        {
            int iPos = 0;
            byte b1 = 0x00;
            byte b2 = 0x00;
            byte btemp = 0x00;

            if (pt == UserProtocolType_e.EN_PROTOCOL_101_UP ||
                pt == UserProtocolType_e.EN_PROTOCOL_101_UP_V1 || //老版信息安全，2017-11-16 17:08
                pt == UserProtocolType_e.EN_PROTOCOL_101_DOWN ||
                pt == UserProtocolType_e.EN_PROTOCOL_MODBUS_DOWN ||
                pt == UserProtocolType_e.EN_PROTOCOL_MODBUS_UP)
            {
                if (Global.g_General_DuplexChannel == 0)
                {
                    //一、安全层
                    tCommB101UpCfg.tSafetyLayerCfg.bHaveSafetyLayer = (u8ProtocolCfg[iPos++] == 0x00 ? false : true);
                    tCommB101UpCfg.tSafetyLayerCfg.u8AppProtocol = u8ProtocolCfg[iPos++];
                    b1 = u8ProtocolCfg[iPos++];
                    b2 = u8ProtocolCfg[iPos++];
                    byte[] ba_0 = { b1, b2 };
                    tCommB101UpCfg.tSafetyLayerCfg.u16TimeDiffLimit = BitConverter.ToUInt16(ba_0, 0);
                    //二、链路层
                    tCommB101UpCfg.tLinkCfg.u8Mode = u8ProtocolCfg[iPos++];
                    tCommB101UpCfg.tLinkCfg.u8StationType = u8ProtocolCfg[iPos++];
                    b1 = u8ProtocolCfg[iPos++];
                    b2 = u8ProtocolCfg[iPos++];
                    byte[] ba_1 = { b1, b2 };
                    tCommB101UpCfg.tLinkCfg.u16LinkAddr = BitConverter.ToUInt16(ba_1, 0);
                    tCommB101UpCfg.tLinkCfg.u8LinkAddrLen = u8ProtocolCfg[iPos++];
                    tCommB101UpCfg.tLinkCfg.u8ResendInterval = u8ProtocolCfg[iPos++];
                    tCommB101UpCfg.tLinkCfg.u8ResendNum = u8ProtocolCfg[iPos++];
                    tCommB101UpCfg.tLinkCfg.u8Dir = u8ProtocolCfg[iPos++];
                    //三、应用层
                    b1 = u8ProtocolCfg[iPos++];
                    b2 = u8ProtocolCfg[iPos++];
                    byte[] ba_2 = { b1, b2 };
                    tCommB101UpCfg.tAppCfg.u16AppAddr = BitConverter.ToUInt16(ba_2, 0);
                    tCommB101UpCfg.tAppCfg.u8AppAddrLen = u8ProtocolCfg[iPos++];
                    tCommB101UpCfg.tAppCfg.u8CotLen = u8ProtocolCfg[iPos++];
                    //----
                    tCommB101UpCfg.tAppCfg.u8ObjAddrLen = u8ProtocolCfg[iPos++];
                    tCommB101UpCfg.tAppCfg.u8YcSendTYP = u8ProtocolCfg[iPos++];
                    b1 = u8ProtocolCfg[iPos++];
                    b2 = u8ProtocolCfg[iPos++];
                    byte[] ba_2_1 = { b1, b2 };
                    tCommB101UpCfg.tAppCfg.u16YcChgSendCycle = BitConverter.ToUInt16(ba_2_1, 0);
                    //----
                    b1 = u8ProtocolCfg[iPos++];
                    b2 = u8ProtocolCfg[iPos++];
                    byte[] ba_3 = { b1, b2 };
                    tCommB101UpCfg.tAppCfg.u16YkValidTime = BitConverter.ToUInt16(ba_3, 0);
                    b1 = u8ProtocolCfg[iPos++];
                    b2 = u8ProtocolCfg[iPos++];
                    byte[] ba_4 = { b1, b2 };
                    tCommB101UpCfg.tAppCfg.u16IC_Time = BitConverter.ToUInt16(ba_4, 0);
                    //----
                    b1 = u8ProtocolCfg[iPos++];
                    b2 = u8ProtocolCfg[iPos++];
                    byte[] ba_5 = { b1, b2 };
                    tCommB101UpCfg.tAppCfg.u16CI_Time = BitConverter.ToUInt16(ba_5, 0);
                    //----
                    b1 = u8ProtocolCfg[iPos++];
                    b2 = u8ProtocolCfg[iPos++];
                    byte[] ba_6 = { b1, b2 };
                    tCommB101UpCfg.tAppCfg.u16CmdTime = BitConverter.ToUInt16(ba_6, 0);
                }
                else
                {
                    //一、安全层(16 = 4 + 12)
                    tCommB101UpCfg.tSafetyLayerCfg.bHaveSafetyLayer = (u8ProtocolCfg[iPos++] == 0x00 ? false : true);
                    tCommB101UpCfg.tSafetyLayerCfg.u8AppProtocol = u8ProtocolCfg[iPos++];
                    b1 = u8ProtocolCfg[iPos++];
                    b2 = u8ProtocolCfg[iPos++];
                    byte[] ba_0 = { b1, b2 };
                    tCommB101UpCfg.tSafetyLayerCfg.u16TimeDiffLimit = BitConverter.ToUInt16(ba_0, 0);

                    for (int m = 0; m < 12; m++) { btemp = u8ProtocolCfg[iPos++]; }
                    //二、链路层(64 = 8 + 56)
                    tCommB101UpCfg.tLinkCfg.u8Mode = u8ProtocolCfg[iPos++];
                    tCommB101UpCfg.tLinkCfg.u8StationType = u8ProtocolCfg[iPos++];
                    b1 = u8ProtocolCfg[iPos++];
                    b2 = u8ProtocolCfg[iPos++];
                    byte[] ba_1 = { b1, b2 };
                    tCommB101UpCfg.tLinkCfg.u16LinkAddr = BitConverter.ToUInt16(ba_1, 0);
                    tCommB101UpCfg.tLinkCfg.u8LinkAddrLen = u8ProtocolCfg[iPos++];
                    tCommB101UpCfg.tLinkCfg.u8ResendInterval = u8ProtocolCfg[iPos++];
                    tCommB101UpCfg.tLinkCfg.u8ResendNum = u8ProtocolCfg[iPos++];
                    tCommB101UpCfg.tLinkCfg.u8Dir = u8ProtocolCfg[iPos++];

                    for (int m = 0; m < 56; m++) { btemp = u8ProtocolCfg[iPos++]; }
                    //三、应用层(48 = 16 + 32)
                    b1 = u8ProtocolCfg[iPos++];
                    b2 = u8ProtocolCfg[iPos++];
                    byte[] ba_2 = { b1, b2 };
                    tCommB101UpCfg.tAppCfg.u16AppAddr = BitConverter.ToUInt16(ba_2, 0);
                    tCommB101UpCfg.tAppCfg.u8AppAddrLen = u8ProtocolCfg[iPos++];
                    tCommB101UpCfg.tAppCfg.u8CotLen = u8ProtocolCfg[iPos++];
                    //----
                    tCommB101UpCfg.tAppCfg.u8ObjAddrLen = u8ProtocolCfg[iPos++];
                    tCommB101UpCfg.tAppCfg.u8YcSendTYP = u8ProtocolCfg[iPos++];
                    b1 = u8ProtocolCfg[iPos++];
                    b2 = u8ProtocolCfg[iPos++];
                    byte[] ba_2_1 = { b1, b2 };
                    tCommB101UpCfg.tAppCfg.u16YcChgSendCycle = BitConverter.ToUInt16(ba_2_1, 0);
                    //----
                    b1 = u8ProtocolCfg[iPos++];
                    b2 = u8ProtocolCfg[iPos++];
                    byte[] ba_3 = { b1, b2 };
                    tCommB101UpCfg.tAppCfg.u16YkValidTime = BitConverter.ToUInt16(ba_3, 0);
                    b1 = u8ProtocolCfg[iPos++];
                    b2 = u8ProtocolCfg[iPos++];
                    byte[] ba_4 = { b1, b2 };
                    tCommB101UpCfg.tAppCfg.u16IC_Time = BitConverter.ToUInt16(ba_4, 0);
                    //----
                    b1 = u8ProtocolCfg[iPos++];
                    b2 = u8ProtocolCfg[iPos++];
                    byte[] ba_5 = { b1, b2 };
                    tCommB101UpCfg.tAppCfg.u16CI_Time = BitConverter.ToUInt16(ba_5, 0);
                    //----
                    b1 = u8ProtocolCfg[iPos++];
                    b2 = u8ProtocolCfg[iPos++];
                    byte[] ba_6 = { b1, b2 };
                    tCommB101UpCfg.tAppCfg.u16CmdTime = BitConverter.ToUInt16(ba_6, 0);

                    for (int m = 0; m < 32; m++) { btemp = u8ProtocolCfg[iPos++]; }
                }
            }
            else if (pt == UserProtocolType_e.EN_PROTOCOL_104_UP ||
                pt == UserProtocolType_e.EN_PROTOCOL_104_UP_V1 || //老版信息安全，2017-11-16 17:08
                pt == UserProtocolType_e.EN_PROTOCOL_104_DOWN)
            {
                if (Global.g_General_DuplexChannel == 0)
                {
                    //一、安全层
                    tCommB104UpCfg.tSafetyLayerCfg.bHaveSafetyLayer = (u8ProtocolCfg[iPos++] == 0x00 ? false : true);
                    tCommB101UpCfg.tSafetyLayerCfg.u8AppProtocol = u8ProtocolCfg[iPos++];
                    b1 = u8ProtocolCfg[iPos++];
                    b2 = u8ProtocolCfg[iPos++];
                    byte[] ba_0 = { b1, b2 };
                    tCommB101UpCfg.tSafetyLayerCfg.u16TimeDiffLimit = BitConverter.ToUInt16(ba_0, 0);
                    //二、链路层
                    tCommB104UpCfg.tLinkCfg.u8ServerIP[0] = u8ProtocolCfg[iPos++];
                    tCommB104UpCfg.tLinkCfg.u8ServerIP[1] = u8ProtocolCfg[iPos++];
                    tCommB104UpCfg.tLinkCfg.u8ServerIP[2] = u8ProtocolCfg[iPos++];
                    tCommB104UpCfg.tLinkCfg.u8ServerIP[3] = u8ProtocolCfg[iPos++];
                    //----
                    tCommB104UpCfg.tLinkCfg.u8ClientIP[0] = u8ProtocolCfg[iPos++];
                    tCommB104UpCfg.tLinkCfg.u8ClientIP[1] = u8ProtocolCfg[iPos++];
                    tCommB104UpCfg.tLinkCfg.u8ClientIP[2] = u8ProtocolCfg[iPos++];
                    tCommB104UpCfg.tLinkCfg.u8ClientIP[3] = u8ProtocolCfg[iPos++];
                    //----
                    tCommB104UpCfg.tLinkCfg.u8GateIP[0] = u8ProtocolCfg[iPos++];
                    tCommB104UpCfg.tLinkCfg.u8GateIP[1] = u8ProtocolCfg[iPos++];
                    tCommB104UpCfg.tLinkCfg.u8GateIP[2] = u8ProtocolCfg[iPos++];
                    tCommB104UpCfg.tLinkCfg.u8GateIP[3] = u8ProtocolCfg[iPos++];
                    //----
                    b1 = u8ProtocolCfg[iPos++];
                    b2 = u8ProtocolCfg[iPos++];
                    byte[] ba_1 = { b1, b2 };
                    tCommB104UpCfg.tLinkCfg.u16ServerPort = BitConverter.ToUInt16(ba_1, 0);
                    //----
                    tCommB104UpCfg.tLinkCfg.u8Protocol = u8ProtocolCfg[iPos++];
                    tCommB104UpCfg.tLinkCfg.u8LinkType = u8ProtocolCfg[iPos++];
                    tCommB104UpCfg.tLinkCfg.u8StationType = u8ProtocolCfg[iPos++];
                    tCommB104UpCfg.tLinkCfg.u8Res1[0] = u8ProtocolCfg[iPos++];
                    tCommB104UpCfg.tLinkCfg.u8Res1[1] = u8ProtocolCfg[iPos++];
                    tCommB104UpCfg.tLinkCfg.u8Res1[2] = u8ProtocolCfg[iPos++];
                    //----
                    tCommB104UpCfg.tLinkCfg.u8K = u8ProtocolCfg[iPos++];
                    tCommB104UpCfg.tLinkCfg.u8W = u8ProtocolCfg[iPos++];
                    tCommB104UpCfg.tLinkCfg.u8EnableKW = u8ProtocolCfg[iPos++];
                    tCommB104UpCfg.tLinkCfg.u8EnableT1 = u8ProtocolCfg[iPos++];
                    //----
                    tCommB104UpCfg.tLinkCfg.u8T0 = u8ProtocolCfg[iPos++];
                    tCommB104UpCfg.tLinkCfg.u8T1 = u8ProtocolCfg[iPos++];
                    tCommB104UpCfg.tLinkCfg.u8T2 = u8ProtocolCfg[iPos++];
                    tCommB104UpCfg.tLinkCfg.u8T3 = u8ProtocolCfg[iPos++];
                    //应用层
                    b1 = u8ProtocolCfg[iPos++];
                    b2 = u8ProtocolCfg[iPos++];
                    byte[] ba_2 = { b1, b2 };
                    tCommB104UpCfg.tAppCfg.u16AppAddr = BitConverter.ToUInt16(ba_2, 0);
                    tCommB104UpCfg.tAppCfg.u8AppAddrLen = u8ProtocolCfg[iPos++];
                    tCommB104UpCfg.tAppCfg.u8CotLen = u8ProtocolCfg[iPos++];
                    //----
                    tCommB104UpCfg.tAppCfg.u8ObjAddrLen = u8ProtocolCfg[iPos++];
                    tCommB104UpCfg.tAppCfg.u8YcSendTYP = u8ProtocolCfg[iPos++];
                    b1 = u8ProtocolCfg[iPos++];
                    b2 = u8ProtocolCfg[iPos++];
                    byte[] ba_2_1 = { b1, b2 };
                    tCommB104UpCfg.tAppCfg.u16YcChgSendCycle = BitConverter.ToUInt16(ba_2_1, 0);
                    //----
                    b1 = u8ProtocolCfg[iPos++];
                    b2 = u8ProtocolCfg[iPos++];
                    byte[] ba_3 = { b1, b2 };
                    tCommB104UpCfg.tAppCfg.u16YkValidTime = BitConverter.ToUInt16(ba_3, 0);
                    b1 = u8ProtocolCfg[iPos++];
                    b2 = u8ProtocolCfg[iPos++];
                    byte[] ba_4 = { b1, b2 };
                    tCommB104UpCfg.tAppCfg.u16IC_Time = BitConverter.ToUInt16(ba_4, 0);
                    //----
                    b1 = u8ProtocolCfg[iPos++];
                    b2 = u8ProtocolCfg[iPos++];
                    byte[] ba_5 = { b1, b2 };
                    tCommB104UpCfg.tAppCfg.u16CI_Time = BitConverter.ToUInt16(ba_5, 0);
                    //----
                    b1 = u8ProtocolCfg[iPos++];
                    b2 = u8ProtocolCfg[iPos++];
                    byte[] ba_6 = { b1, b2 };
                    tCommB104UpCfg.tAppCfg.u16CmdTime = BitConverter.ToUInt16(ba_6, 0);
                }
                else
                {
                    //一、安全层
                    tCommB104UpCfg.tSafetyLayerCfg.bHaveSafetyLayer = (u8ProtocolCfg[iPos++] == 0x00 ? false : true);
                    tCommB101UpCfg.tSafetyLayerCfg.u8AppProtocol = u8ProtocolCfg[iPos++];
                    b1 = u8ProtocolCfg[iPos++];
                    b2 = u8ProtocolCfg[iPos++];
                    byte[] ba_0 = { b1, b2 };
                    tCommB101UpCfg.tSafetyLayerCfg.u16TimeDiffLimit = BitConverter.ToUInt16(ba_0, 0);

                    for (int m = 0; m < 12; m++) { btemp = u8ProtocolCfg[iPos++]; }
                    //二、链路层(64 = 40 + 24)
                    for (int m = 0; m < 4; m++) { tCommB104UpCfg.tLinkCfg.u8ServerIP[m] = u8ProtocolCfg[iPos++]; }
                    tCommB104UpCfg.tLinkCfg.u8DuplexChannel = u8ProtocolCfg[iPos++];
                    for (int m = 0; m < 3; m++) { btemp = u8ProtocolCfg[iPos++]; }

                    for (int m = 0; m < 4; m++) { tCommB104UpCfg.tLinkCfg.u8ClientIP[m] = u8ProtocolCfg[iPos++]; }
                    for (int m = 0; m < 4; m++) { tCommB104UpCfg.tLinkCfg.u8ClientIP2[m] = u8ProtocolCfg[iPos++]; }

                    for (int m = 0; m < 4; m++) { tCommB104UpCfg.tLinkCfg.u8GateIP[m] = u8ProtocolCfg[iPos++]; }
                    for (int m = 0; m < 4; m++) { tCommB104UpCfg.tLinkCfg.u8GateIP2[m] = u8ProtocolCfg[iPos++]; }

                    b1 = u8ProtocolCfg[iPos++];
                    b2 = u8ProtocolCfg[iPos++];
                    byte[] ba_1 = { b1, b2 };
                    tCommB104UpCfg.tLinkCfg.u16ServerPort = BitConverter.ToUInt16(ba_1, 0);
                    b1 = u8ProtocolCfg[iPos++];
                    b2 = u8ProtocolCfg[iPos++];
                    byte[] ba_12 = { b1, b2 };
                    tCommB104UpCfg.tLinkCfg.u16ServerPort2 = BitConverter.ToUInt16(ba_12, 0);

                    tCommB104UpCfg.tLinkCfg.u8Protocol = u8ProtocolCfg[iPos++];
                    tCommB104UpCfg.tLinkCfg.u8LinkType = u8ProtocolCfg[iPos++];
                    tCommB104UpCfg.tLinkCfg.u8StationType = u8ProtocolCfg[iPos++];
                    for (int m = 0; m < 1; m++) { btemp = u8ProtocolCfg[iPos++]; }

                    tCommB104UpCfg.tLinkCfg.u8K = u8ProtocolCfg[iPos++];
                    tCommB104UpCfg.tLinkCfg.u8W = u8ProtocolCfg[iPos++];
                    tCommB104UpCfg.tLinkCfg.u8EnableKW = u8ProtocolCfg[iPos++];
                    tCommB104UpCfg.tLinkCfg.u8EnableT1 = u8ProtocolCfg[iPos++];

                    tCommB104UpCfg.tLinkCfg.u8T0 = u8ProtocolCfg[iPos++];
                    tCommB104UpCfg.tLinkCfg.u8T1 = u8ProtocolCfg[iPos++];
                    tCommB104UpCfg.tLinkCfg.u8T2 = u8ProtocolCfg[iPos++];
                    tCommB104UpCfg.tLinkCfg.u8T3 = u8ProtocolCfg[iPos++];

                    for (int m = 0; m < 24; m++) { btemp = u8ProtocolCfg[iPos++]; }
                    //应用层(48 = 16 + 32)
                    b1 = u8ProtocolCfg[iPos++];
                    b2 = u8ProtocolCfg[iPos++];
                    byte[] ba_2 = { b1, b2 };
                    tCommB104UpCfg.tAppCfg.u16AppAddr = BitConverter.ToUInt16(ba_2, 0);
                    tCommB104UpCfg.tAppCfg.u8AppAddrLen = u8ProtocolCfg[iPos++];
                    tCommB104UpCfg.tAppCfg.u8CotLen = u8ProtocolCfg[iPos++];
                    //----
                    tCommB104UpCfg.tAppCfg.u8ObjAddrLen = u8ProtocolCfg[iPos++];
                    tCommB104UpCfg.tAppCfg.u8YcSendTYP = u8ProtocolCfg[iPos++];
                    b1 = u8ProtocolCfg[iPos++];
                    b2 = u8ProtocolCfg[iPos++];
                    byte[] ba_2_1 = { b1, b2 };
                    tCommB104UpCfg.tAppCfg.u16YcChgSendCycle = BitConverter.ToUInt16(ba_2_1, 0);
                    //----
                    b1 = u8ProtocolCfg[iPos++];
                    b2 = u8ProtocolCfg[iPos++];
                    byte[] ba_3 = { b1, b2 };
                    tCommB104UpCfg.tAppCfg.u16YkValidTime = BitConverter.ToUInt16(ba_3, 0);
                    b1 = u8ProtocolCfg[iPos++];
                    b2 = u8ProtocolCfg[iPos++];
                    byte[] ba_4 = { b1, b2 };
                    tCommB104UpCfg.tAppCfg.u16IC_Time = BitConverter.ToUInt16(ba_4, 0);
                    //----
                    b1 = u8ProtocolCfg[iPos++];
                    b2 = u8ProtocolCfg[iPos++];
                    byte[] ba_5 = { b1, b2 };
                    tCommB104UpCfg.tAppCfg.u16CI_Time = BitConverter.ToUInt16(ba_5, 0);
                    //----
                    b1 = u8ProtocolCfg[iPos++];
                    b2 = u8ProtocolCfg[iPos++];
                    byte[] ba_6 = { b1, b2 };
                    tCommB104UpCfg.tAppCfg.u16CmdTime = BitConverter.ToUInt16(ba_6, 0);

                    for (int m = 0; m < 32; m++) { btemp = u8ProtocolCfg[iPos++]; }
                }
            }
            //END
        }
    }

    public class PORT_T
    {
        public PORT_T()
        {
//#define PORT_SERIAL		(1)
//#define PORT_NET		(2)
//#define PORT_UP			(3)
//#define PORT_DOWN		(4)
            u16PortAddr = 1;//端口地址
            u8PhyAttr = 0x01;//物理属性：串口-1，网口\虚拟口-2
            u8LogicAttr = 0x03;//逻辑属性：对上-3，对下-4
        }
        //----
        public UInt16 u16PortAddr;//地址(1-65535)
        public byte u8PhyAttr;//物理属性:串口or网口(非0)
        public byte u8LogicAttr;//逻辑属性:对上or对下(非0)
        //----以下以NPORT_T为准，占12字节，所以SPORT_T中多定义了一个4字节占位数组
        public SPORT_T SPort = new SPORT_T();//8字节 + 4字节占位
        public NPORT_T NPort = new NPORT_T();//12字节
        //------------------------------------------------------------------------
        public PORT_T MyClone()
        {
            PORT_T p = new PORT_T();
            p.u16PortAddr = this.u16PortAddr;
            p.u8PhyAttr = this.u8PhyAttr;
            p.u8LogicAttr = this.u8LogicAttr;
            //----
            p.SPort.u32BaudRate = this.SPort.u32BaudRate;
            p.SPort.u8DataBit = this.SPort.u8DataBit;
            p.SPort.u8StopBit = this.SPort.u8StopBit;
            p.SPort.u8CheckBit = this.SPort.u8CheckBit;
            //----
            for (int m = 0; m < 4; m++ )
            {
                p.NPort.u8IP[m] = this.NPort.u8IP[m];
                p.NPort.u8Mask[m] = this.NPort.u8Mask[m];
            }
            for (int m = 0; m < 6; m++) { p.NPort.u8MAC[m] = this.NPort.u8MAC[m]; }
            return p;
        }
    }

    public class SPORT_T //8字节
    {
        public SPORT_T()
        {
            u32BaudRate = 9600;
            u8DataBit = 0x08;//8,7,6,5
            u8StopBit = 0x01;//1,2
            u8CheckBit = 0x00;//0,1,2
            //占位
            for (int k = 0; k < u8Res1.Length; k++) { u8Res1[k] = 0x00; }
            for (int k = 0; k < u8Res2.Length; k++) { u8Res2[k] = 0x00; }
            for (int k = 0; k < u8Res3.Length; k++) { u8Res3[k] = 0x00; }
        }

        public UInt32 u32BaudRate;//波特率

        public byte u8DataBit;//数据位
        public byte u8StopBit;//停止位
        public byte u8CheckBit;//校验位
        public byte[] u8Res1 = new byte[1];//占位

        public byte[] u8Res2 = new byte[4];//占位

        public byte[] u8Res3 = new byte[4];//占位
    }

    public class NPORT_T //12字节
    {
        public NPORT_T()
        {
            u8IP = new byte[4];
            u8Mask = new byte[4];
            u8IP[0] = 192; 
            u8IP[1] = 168; 
            u8IP[2] = 253; 
            u8IP[3] = 100;//127.0.0.1
            u8Mask[0] = 0xFF; 
            u8Mask[1] = 0xFF;
            u8Mask[2] = 0xFF; 
            u8Mask[3] = 0x00;//255.255.255.0

            u8MAC = new byte[6];
            for (int k = 0; k < u8MAC.Length; k++) { u8MAC[k] = 0x00; }
            u8MAC[0] = 0x00; u8MAC[1] = 0xc0; u8MAC[2] = 0xd0; u8MAC[3] = 0x00; u8MAC[4] = 0x07; u8MAC[5] = 0x01; 
            u8Res1 = new byte[2];
            for (int k = 0; k < u8Res1.Length; k++) { u8Res1[k] = 0x00; }
        }

        public byte[] u8IP;//IP地址
        public byte[] u8Mask;//子网掩码

        public byte[] u8MAC;//新增MAC地址。2017-5-17
        public byte[] u8Res1;//占位
    }

    #region "枚举"
    //public enum UserProtocolType_e
    //{
    //    EN_PROTOCOL_START,

    //    EN_PROTOCOL_B101_UP = EN_PROTOCOL_START,
    //    EN_PROTOCOL_UB101_UP,//对上101规约
    //    EN_PROTOCOL_104_UP,//对上104规约
    //    EN_PROTOCOL_MODBUS_UP,//对上modbus规约
    //    EN_PROTOCOL_CDT_UP,

    //    EN_PROTOCOL_B101_DOWN,
    //    EN_PROTOCOL_UB101_DOWN,
    //    EN_PROTOCOL_104_DOWN,
    //    EN_PROTOCOL_MODBUS_DOWN,
    //    EN_PROTOCOL_CDT_DOWN,

    //    EN_PROTOCOL_END
    //}

    public enum UserProtocolType_e //修改
    {
        EN_PROTOCOL_START,

        EN_PROTOCOL_101_UP = EN_PROTOCOL_START,//对上101规约
        EN_PROTOCOL_104_UP,//对上104规约
        EN_PROTOCOL_MODBUS_UP,//对上ModBus规约
        EN_PROTOCOL_CDT_UP,//对上CDT规约

        EN_PROTOCOL_101_DOWN,//对下101规约
        EN_PROTOCOL_104_DOWN,//对下104规约
        EN_PROTOCOL_MODBUS_DOWN,//对下ModBus规约
        EN_PROTOCOL_CDT_DOWN,//对下CDT规约

        EN_PROTOCOL_101_UP_V1,//老版信息安全，2017-11-16 16:21
        EN_PROTOCOL_104_UP_V1,
        EN_PROTOCOL_END
    }

    public enum DataBit_e
    {
        NUM_5 = 0,
        NUM_6,
        NUM_7,
        NUM_8
    }

    public enum StopBit_e
    {
        NUM_1 = 0,
        NUM_15,
        NUM_2
    }

    public enum Parity_e
    {
        P_None = 0,
        P_Odd,
        P_Even,
        P_Space,
        P_Mark
    }
    #endregion

    #region "附2：对上101规约配置参数"
    public class CommB101UpCfg_t
    {
        public CommSafetyLayerConfig_t tSafetyLayerCfg = new CommSafetyLayerConfig_t();//是否加密
        public Comm101LinkConfig_t tLinkCfg = new Comm101LinkConfig_t();//链路
        public Comm101AppConfig_t tAppCfg = new Comm101AppConfig_t();//应用
    }

    //1/3.安全层
    public class CommSafetyLayerConfig_t
    {
        public CommSafetyLayerConfig_t()
        {
            bHaveSafetyLayer = false;
            u8AppProtocol = 0;
            u16TimeDiffLimit = 30;
            for (int k = 0; k < u8Res1.Length; k++) { u8Res1[k] = 0x00; }
        }
        //----
        public bool bHaveSafetyLayer;//1、是否使用加密：0or1
        public byte u8AppProtocol;
        public UInt16 u16TimeDiffLimit;
        //已经有4个字节了，再增加12个字节占位，总计16个字节，jifeng，2018-8-23 18:03
        public byte[] u8Res1 = new byte[12];
    }
    //2/3.链路层
    public class Comm101LinkConfig_t
    {
        public Comm101LinkConfig_t()
        {
            u8Mode = 2;
            u8StationType = 2;
            u16LinkAddr = 1;

            u8LinkAddrLen = 2;
            u8ResendInterval = 5;
            u8ResendNum = 3;
            u8Dir = 1;

            for (int k = 0; k < u8Res1.Length; k++) { u8Res1[k] = 0x00; }
        }

        public byte u8Mode;//2、链路模式：平衡式or非平衡式(非0)
        public byte u8StationType;//3、站类型：主控站or被控站(非0)
        public UInt16 u16LinkAddr;//4、链路地址：1~254
        //平衡式101需要配置
        public byte u8LinkAddrLen;//5、链路地址长度：1or2
        public byte u8ResendInterval;//6、重发间隔（s）：1-30s
        public byte u8ResendNum;//7、重发次数：0-5次
        public byte u8Dir;//8、DIR位：0or1
        //已经有4个字节了，再增加12个字节占位，总计16个字节，jifeng，2018-8-23 18:03
        public byte[] u8Res1 = new byte[12];
    }
    //3/3.应用层
    public class Comm101AppConfig_t
    {
        //----START
        public Comm101AppConfig_t()
        {
            u16AppAddr = 1;
            u8AppAddrLen = 2;
            u8CotLen = 2;

            u8ObjAddrLen = 2;
            u8YcSendTYP = 2;
            u16YcChgSendCycle = 200;

            u16YkValidTime = 20;
            u16IC_Time = 300;

            u16CI_Time = 400;
            u16CmdTime = 500;

            for (int k = 0; k < u8Res1.Length; k++) { u8Res1[k] = 0x00; }
        }
        //----
        public UInt16 u16AppAddr;//9、应用层地址：1~254
        public byte u8AppAddrLen;//10、应用层地址长度：1~2
        public byte u8CotLen;//11、传送原因长度：1~2

        public byte u8ObjAddrLen;//12、信息对象地址长度：2~3
        public byte u8YcSendTYP;//13、【新增20170625】遥测上送类型：下拉框实现，选项为“ASDU9归一化值”、“ASDU11标度化值”、“ASDU13短浮点数”三个，配置值对应0/1/2。
        public UInt16 u16YcChgSendCycle;//遥测变化上送周期

        public UInt16 u16YkValidTime;//13、遥控有效时间：10s~3600s
        public UInt16 u16IC_Time;//14、站总召唤间隔时间：10s~3600s

        public UInt16 u16CI_Time;//15、电度量总召唤间隔时间：10s~3600s
        public UInt16 u16CmdTime;//16、对时时间间隔：10s~3600s

        //已经有16个字节了，再增加32个字节占位，总计48个字节，jifeng，2018-8-23 18:08
        public byte[] u8Res1 = new byte[32];
    }
    #endregion

    #region "附3：对上104规约配置参数"
    public class CommB104UpCfg_t
    {
        public CommSafetyLayerConfig_t tSafetyLayerCfg = new CommSafetyLayerConfig_t();//是否加密
        public Comm104LinkConfig_t tLinkCfg = new Comm104LinkConfig_t();//链路
        public Comm101AppConfig_t tAppCfg = new Comm101AppConfig_t();//应用
    }

    public class Comm104LinkConfig_t
    {
        public Comm104LinkConfig_t()
        {
            u8ServerIP[0] = (byte)192; u8ServerIP[1] = (byte)168; u8ServerIP[2] = (byte)253; u8ServerIP[3] = (byte)100;
            u8DuplexChannel = 0x00;
            for (int k = 0; k < u8Res1.Length; k++) { u8Res1[k] = 0x00; }

            u8ClientIP[0] = (byte)192; u8ClientIP[1] = (byte)168; u8ClientIP[2] = (byte)253; u8ClientIP[3] = (byte)200;
            u8ClientIP2[0] = (byte)192; u8ClientIP2[1] = (byte)168; u8ClientIP2[2] = (byte)253; u8ClientIP2[3] = (byte)201;

            u8GateIP[0] = (byte)192; u8GateIP[1] = (byte)168; u8GateIP[2] = (byte)0; u8GateIP[3] = (byte)1;
            u8GateIP2[0] = (byte)192; u8GateIP2[1] = (byte)168; u8GateIP2[2] = (byte)0; u8GateIP2[3] = (byte)1;

            u16ServerPort = 2404;
            u16ServerPort2 = 2404;

            u8Protocol = 1;
            u8LinkType = 1;
            u8StationType = 2;
            for (int k = 0; k < u8Res2.Length; k++) { u8Res2[k] = 0x00; }

            u8K = 12;
            u8W = 8;
            u8EnableKW = 0x00;
            u8EnableT1 = 0x00;

            u8T0 = 30;
            u8T1 = 15;
            u8T2 = 10;
            u8T3 = 20;

            for (int k = 0; k < u8Res3.Length; k++) { u8Res3[k] = 0x00; }
        }

        public byte[] u8ServerIP = new byte[4];
        public byte u8DuplexChannel;
        public byte[] u8Res1 = new byte[3];

	    public byte[] u8ClientIP = new byte[4];
        public byte[] u8ClientIP2 = new byte[4];

	    public byte[] u8GateIP = new byte[4];
        public byte[] u8GateIP2 = new byte[4];

        public UInt16 u16ServerPort;
        public UInt16 u16ServerPort2;

        public byte u8Protocol;//传输层规约：1-TCP;2-UDP
	    public byte u8LinkType;//链接类型：1-服务器；2-客户端
	    public byte u8StationType;//站类型：1-主控站；2-被控站
        public byte[] u8Res2 = new byte[1];

        public byte u8K;
        public byte u8W;
        public byte u8EnableKW;//使用KT机制，2017-12-13 10:16，jifeng
        public byte u8EnableT1;

        public byte u8T0;
        public byte u8T1;
        public byte u8T2;
        public byte u8T3;

        //已经40字节，再增加24字节占位，jifeng，2018-8-24 9:37
        public byte[] u8Res3 = new byte[24];
    }

    public enum Comm104LinkProtocolType_e
    {
        EN_104LINK_PROTOL_NULL = 0,
        EN_104LINK_PROTOL_TCP,
        EN_104LINK_PROTOL_UDP,
        EN_104LINK_PROTOL_END
    }
    public enum Comm104LinkLinkType_e
    {
        EN_104LINK_LINKTYPE_NULL = 0,
        EN_104LINK_LINKTYPE_SERVER,
        EN_104LINK_LINKTYPE_CLIENT,
        EN_104LINK_LINKTYPE_END
    }
    public enum Comm104LinkStationType_e
    {
        EN_104LINK_STATION_NULL = 0,
        EN_104LINK_STATION_SERVER,
        EN_104LINK_STATION_CLIENT,
        EN_104LINK_STATION_END
    }
    #endregion
}
