using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CfgTool
{
    //对上 or 对下 104规约
    public partial class Form_ProtocolPara_Up104 : Form
    {
        CTablePort TablePort = null;
        CommSafetyLayerConfig_t safety = null;
        Comm104LinkConfig_t lnk = null;
        Comm101AppConfig_t app = null;
        class CST_RANGE_LIMIT
        {
            //范围：1-65535，其中从1-1023叫知名端口号，也叫源端口号。
            //这些端口是被tcp和udp定义了的端口，从1024-49151叫做已注册端口号，被一些公司用于自己的某种协议。
            //49152-65535叫动态分配端口号，是我们随便可以用的。
            public const int u16ServerPort_Min = 1; public const int u16ServerPort_Max = 65535;
            //----
            public const int u16TimeDiffLimit_Min = 10; public const int u16TimeDiffLimit_Max = 60;
            //----
            public const int u8K_Min = 1; public const int u8K_Max = 255;
            public const int u8W_Min = 1; public const int u8W_Max = 255;
            public const int u8T_Min = 1; public const int u8T_Max = 255;
            public const int u16AppAddr_Min = 1; public const int u16AppAddr_Max = 254;
            public const int u16YkValidTime_Min = 10; public const int u16YkValidTime_Max = 3600;
            public const int u16IC_Time_Min = 10; public const int u16IC_Time_Max = 3600;
            public const int u16CI_Time_Min = 10; public const int u16CI_Time_Max = 3600;
            public const int u16CmdTime_Min = 10; public const int u16CmdTime_Max = 3600;
            public const int u16YcChgSendCycle_Min = 0; public const int u16YcChgSendCycle_Max = 1000;
        }

        public Form_ProtocolPara_Up104(CTablePort tableport)
        {
            InitializeComponent();
            TablePort = tableport;
            safety = TablePort.cfg_Port.tCommB104UpCfg.tSafetyLayerCfg;
            lnk = TablePort.cfg_Port.tCommB104UpCfg.tLinkCfg;
            app = TablePort.cfg_Port.tCommB104UpCfg.tAppCfg;
        }

        private void Form_ProtocolPara_Up104_Load(object sender, EventArgs e)
        {
            if (Global.g_General_DuplexChannel == 0)
            {
                //cb_DuplexChannel.Visible = false;
                //tb_u8ClientIP2.Visible = false;
                //lblClientIP2.Visible = false;

                //foreach(Control c in this.Controls)
                //{
                //    int itag = Convert.ToInt32(c.Tag);
                //    if (itag == 1233)
                //    {
                //        c.Top -= 20;
                //    }
                //    else if (itag == 1234)
                //    {
                //        c.Top -= 50;
                //    }
                //}

                cb_DuplexChannel.Enabled = false;
                tb_u8ClientIP2.Enabled = false;
                lblClientIP2.Enabled = false;
                lblGateIP2.Enabled = false;
                tb_u8GateIP2.Enabled = false;
                tb_u16ServerPort2.Enabled = false;
                lnk.u8DuplexChannel = 0;
            }
            else
            {
                cb_DuplexChannel.Checked = true;
                cb_DuplexChannel.Enabled = false;
                lnk.u8DuplexChannel = 1;
            }
            initPara();
        }

        private void Form_ProtocolPara_Up104_Click(object sender, EventArgs e)
        {
            tb_Hide.Focus();
        }

        void initPara()
        {
            //----安全层
            //1、是否使用加密。0-false，否；1-true，是。
            cmb_bHaveSafetyLayer.SelectedIndex = (safety.bHaveSafetyLayer == false ? 0 : 1);
            tb_u16TimeDiffLimit.Text = safety.u16TimeDiffLimit.ToString();
            //----链路层
            //1、服务器IP、服务器端口号、客户端IP、网关IP
            tb_u8ServerIP.Text = getIP(lnk.u8ServerIP);
            tb_u16ServerPort.Text = lnk.u16ServerPort.ToString();
            tb_u16ServerPort2.Text = lnk.u16ServerPort2.ToString();
            tb_u8ClientIP.Text = getIP(lnk.u8ClientIP);
            tb_u8ClientIP2.Text = getIP(lnk.u8ClientIP2);
            tb_u8GateIP.Text = getIP(lnk.u8GateIP);
            tb_u8GateIP2.Text = getIP(lnk.u8GateIP2);
            //2、传输层规约、链接类型、站类型
            //传输层规约：TCP or UDP
            if (lnk.u8Protocol == (byte)Comm104LinkProtocolType_e.EN_104LINK_PROTOL_TCP)
            {
                cmb_eProtocol.SelectedIndex = 0;
            }
            else if (lnk.u8Protocol == (byte)Comm104LinkProtocolType_e.EN_104LINK_PROTOL_UDP)
            {
                cmb_eProtocol.SelectedIndex = 1;
            }
            else
            {
                cmb_eProtocol.SelectedIndex = -1;
            }
            //链接类型：服务器 or 客户端
            if (lnk.u8LinkType == (byte)Comm104LinkLinkType_e.EN_104LINK_LINKTYPE_SERVER)
            {
                cmb_u8LinkType.SelectedIndex = 0;
            }
            else if (lnk.u8LinkType == (byte)Comm104LinkLinkType_e.EN_104LINK_LINKTYPE_CLIENT)
            {
                cmb_u8LinkType.SelectedIndex = 1;
            }
            else
            {
                cmb_u8LinkType.SelectedIndex = -1;
            }
            //站类型：主控站or被控站
            if (lnk.u8StationType == (byte)Comm104LinkStationType_e.EN_104LINK_STATION_SERVER)
            {
                cmb_eStationType.SelectedIndex = 0;
            }
            else if (lnk.u8StationType == (byte)Comm104LinkStationType_e.EN_104LINK_STATION_CLIENT)
            {
                cmb_eStationType.SelectedIndex = 1;
            }
            else
            {
                cmb_eStationType.SelectedIndex = -1;
            }
            //3、K、W、T0、T1、T2、T3
            tb_u8K.Text = lnk.u8K.ToString();
            tb_u8W.Text = lnk.u8W.ToString();
            tb_u8T0.Text = lnk.u8T0.ToString();
            tb_u8T1.Text = lnk.u8T1.ToString();
            tb_u8T2.Text = lnk.u8T2.ToString();
            tb_u8T3.Text = lnk.u8T3.ToString();
            //----使能KW和T1
            cb_EnableKW.Checked = (lnk.u8EnableKW == 0x00 ? false : true);
            cb_EnableT1.Checked = (lnk.u8EnableT1== 0x00 ? false : true);
            //----应用层
            try
            {
                //1、应用层地址：1~254
                tb_u16AppAddr.Text = app.u16AppAddr.ToString();
                //2、应用层地址长度：1~2
                cmb_u8AppAddrLen.SelectedIndex = app.u8AppAddrLen - 1;
                //3、传送原因长度：1~2
                cmb_u8CotLen.SelectedIndex = app.u8CotLen - 1;
                //4、信息对象地址长度：2~3
                cmb_u8ObjAddrLen.SelectedIndex = app.u8ObjAddrLen - 2;
                cmb_u8YcSendTYP.SelectedIndex = app.u8YcSendTYP;//新增：遥测上送类型：0,1,2，jifeng，2017-6-27
                //5、遥控有效时间：10s~3600s
                tb_u16YkValidTime.Text = app.u16YkValidTime.ToString();
                //6、站总召唤间隔时间：10s~3600s
                tb_u16IC_Time.Text = app.u16IC_Time.ToString();
                //7、电度量总召唤间隔时间：10s~3600s
                tb_u16CI_Time.Text = app.u16CI_Time.ToString();
                //8、对时时间间隔：10s~3600s
                tb_u16CmdTime.Text = app.u16CmdTime.ToString();
                //9、变化遥测
                tb_YcChgSendCycle.Text = app.u16YcChgSendCycle.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            ///END
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            //一、安全层
            bool bSL = (cmb_bHaveSafetyLayer.SelectedIndex == 0 ? false : true);
            if(safety.bHaveSafetyLayer != bSL) { safety.bHaveSafetyLayer = bSL; }
            UInt16 i_u16TimeDiffLimit = Convert.ToUInt16(tb_u16TimeDiffLimit.Text);

            if (safety.u16TimeDiffLimit != i_u16TimeDiffLimit) { safety.u16TimeDiffLimit = i_u16TimeDiffLimit; }
            if (i_u16TimeDiffLimit < CST_RANGE_LIMIT.u16TimeDiffLimit_Min || i_u16TimeDiffLimit > CST_RANGE_LIMIT.u16TimeDiffLimit_Max)
            {
                MessageBox.Show("重放超时限值越界，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxHightlight(tb_u16TimeDiffLimit);
                return;
            }
            //二、链路层
            UInt16 i_u16ServerPort = Convert.ToUInt16(tb_u16ServerPort.Text);
            UInt16 i_u16ServerPort2 = Convert.ToUInt16(tb_u16ServerPort2.Text);
            if(getIP(lnk.u8ServerIP) != tb_u8ServerIP.Text)
            {
                string[] sa = tb_u8ServerIP.Text.Split('.');
                if (sa.Length != 4)
                {
                    MessageBox.Show("IP地址不符合规范，请检查！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBoxHightlight(tb_u8ServerIP);
                    return;
                }
                for(int k = 0;k < 4; k++)
                {
                    lnk.u8ServerIP[k] = Convert.ToByte(sa[k]);
                }
            }
            //----
            if (i_u16ServerPort < CST_RANGE_LIMIT.u16ServerPort_Min || i_u16ServerPort > CST_RANGE_LIMIT.u16ServerPort_Max)
            {
                MessageBox.Show("服务器端口号越界，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxHightlight(tb_u16ServerPort);
                return;
            }
            if (lnk.u16ServerPort != i_u16ServerPort) { lnk.u16ServerPort = i_u16ServerPort; }
            if (lnk.u16ServerPort2 != i_u16ServerPort2) { lnk.u16ServerPort2 = i_u16ServerPort2; }
            //----
            if (getIP(lnk.u8ClientIP) != tb_u8ClientIP.Text)
            {
                string[] sa = tb_u8ClientIP.Text.Split('.');
                if (sa.Length != 4)
                {
                    MessageBox.Show("IP地址不符合规范，请检查！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBoxHightlight(tb_u8ClientIP);
                    return;
                }
                for (int k = 0; k < 4; k++)
                {
                    lnk.u8ClientIP[k] = Convert.ToByte(sa[k]);
                }
            }
            if (getIP(lnk.u8ClientIP2) != tb_u8ClientIP2.Text)
            {
                string[] sa = tb_u8ClientIP2.Text.Split('.');
                if (sa.Length != 4)
                {
                    MessageBox.Show("IP地址不符合规范，请检查！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBoxHightlight(tb_u8ClientIP2);
                    return;
                }
                for (int k = 0; k < 4; k++)
                {
                    lnk.u8ClientIP2[k] = Convert.ToByte(sa[k]);
                }
            }
            //----
            if (getIP(lnk.u8GateIP) != tb_u8GateIP.Text)
            {
                string[] sa = tb_u8GateIP.Text.Split('.');
                if (sa.Length != 4)
                {
                    MessageBox.Show("IP地址不符合规范，请检查！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBoxHightlight(tb_u8GateIP);
                    return;
                }
                for (int k = 0; k < 4; k++)
                {
                    lnk.u8GateIP[k] = Convert.ToByte(sa[k]);
                }
            }
            if (getIP(lnk.u8GateIP2) != tb_u8GateIP2.Text)
            {
                string[] sa = tb_u8GateIP2.Text.Split('.');
                if (sa.Length != 4)
                {
                    MessageBox.Show("IP地址不符合规范，请检查！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBoxHightlight(tb_u8GateIP2);
                    return;
                }
                for (int k = 0; k < 4; k++)
                {
                    lnk.u8GateIP2[k] = Convert.ToByte(sa[k]);
                }
            }
            //----传输层规约、链接类型、站类型
            byte u8Protocol = Convert.ToByte(cmb_eProtocol.SelectedIndex + 1);
            byte u8LinkType = Convert.ToByte(cmb_u8LinkType.SelectedIndex + 1);
            byte u8StationType = Convert.ToByte(cmb_eStationType.SelectedIndex + 1);
            if (lnk.u8Protocol != u8Protocol) { lnk.u8Protocol = u8Protocol; }
            if (lnk.u8LinkType != u8LinkType) { lnk.u8LinkType = u8LinkType; }
            if (lnk.u8StationType != u8StationType) { lnk.u8StationType = u8StationType; }
            //----K、W、T0、T1、T2、T3、T4
            byte bt_u8K = Convert.ToByte(tb_u8K.Text);
            byte bt_u8W = Convert.ToByte(tb_u8W.Text);
            byte bt_u8T0 = Convert.ToByte(tb_u8T0.Text);
            byte bt_u8T1 = Convert.ToByte(tb_u8T1.Text);
            byte bt_u8T2 = Convert.ToByte(tb_u8T2.Text);
            byte bt_u8T3 = Convert.ToByte(tb_u8T3.Text);
            if (bt_u8K < CST_RANGE_LIMIT.u8K_Min || bt_u8K > CST_RANGE_LIMIT.u8K_Max)
            {
                MessageBox.Show("K值越界，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxHightlight(tb_u8K);
                return;
            }
            if (bt_u8W < CST_RANGE_LIMIT.u8W_Min || bt_u8W > CST_RANGE_LIMIT.u8W_Max)
            {
                MessageBox.Show("W值越界，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxHightlight(tb_u8W);
                return;
            }
            if (bt_u8T0 < CST_RANGE_LIMIT.u8T_Min || bt_u8T0 > CST_RANGE_LIMIT.u8T_Max)
            {
                MessageBox.Show("T0值越界，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxHightlight(tb_u8T0);
                return;
            }
            if (bt_u8T1 < CST_RANGE_LIMIT.u8T_Min || bt_u8T1 > CST_RANGE_LIMIT.u8T_Max)
            {
                MessageBox.Show("T1值越界，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxHightlight(tb_u8T0);
                return;
            }
            if (bt_u8T2 < CST_RANGE_LIMIT.u8T_Min || bt_u8T2 > CST_RANGE_LIMIT.u8T_Max)
            {
                MessageBox.Show("T2值越界，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxHightlight(tb_u8T2);
                return;
            }
            if (bt_u8T3 < CST_RANGE_LIMIT.u8T_Min || bt_u8T3 > CST_RANGE_LIMIT.u8T_Max)
            {
                MessageBox.Show("T3值越界，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxHightlight(tb_u8T3);
                return;
            }
            if (lnk.u8K != bt_u8K) { lnk.u8K = bt_u8K; }
            if (lnk.u8W != bt_u8W) { lnk.u8W = bt_u8W; }
            if (lnk.u8T0 != bt_u8T0) { lnk.u8T0 = bt_u8T0; }
            if (lnk.u8T1 != bt_u8T1) { lnk.u8T1 = bt_u8T1; }
            if (lnk.u8T2 != bt_u8T2) { lnk.u8T2 = bt_u8T2; }
            if (lnk.u8T3 != bt_u8T3) { lnk.u8T3 = bt_u8T3; }
            //----
            lnk.u8EnableKW = (byte)(cb_EnableKW.Checked == false ? 0x00 : 0x01);
            lnk.u8EnableT1 = (byte)(cb_EnableT1.Checked == false ? 0x00 : 0x01);
            //三、应用层
            UInt16 i_u16AppAddr = Convert.ToUInt16(tb_u16AppAddr.Text);
            UInt16 i_u16YkValidTime = Convert.ToUInt16(tb_u16YkValidTime.Text);
            UInt16 i_u16IC_Time = Convert.ToUInt16(tb_u16IC_Time.Text);
            UInt16 i_u16CI_Time = Convert.ToUInt16(tb_u16CI_Time.Text);
            UInt16 i_u16CmdTime = Convert.ToUInt16(tb_u16CmdTime.Text);
            UInt16 i_u16YcChgSendCycle = Convert.ToUInt16(tb_YcChgSendCycle.Text);
            byte bt_u8AppAddrLen = Convert.ToByte(cmb_u8AppAddrLen.SelectedIndex + 1);
            byte bt_u8CotLen = Convert.ToByte(cmb_u8CotLen.SelectedIndex + 1);
            byte bt_u8YcSendTYP = Convert.ToByte(cmb_u8YcSendTYP.SelectedIndex);
            byte bt_u8ObjAddrLen = Convert.ToByte(cmb_u8ObjAddrLen.SelectedIndex + 2);
            //----
            if (i_u16AppAddr < CST_RANGE_LIMIT.u16AppAddr_Min || i_u16AppAddr > CST_RANGE_LIMIT.u16AppAddr_Max)
            {
                MessageBox.Show("应用地址越界，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxHightlight(tb_u16AppAddr);
                return;
            }
            if (app.u16AppAddr != i_u16AppAddr) { app.u16AppAddr = i_u16AppAddr; }
            if (app.u8AppAddrLen != bt_u8AppAddrLen) { app.u8AppAddrLen = bt_u8AppAddrLen; }//1 or 2
            if (app.u8CotLen != bt_u8CotLen) { app.u8CotLen = bt_u8CotLen; }//1 or 2
            if (app.u8ObjAddrLen != bt_u8ObjAddrLen) { app.u8ObjAddrLen = bt_u8ObjAddrLen; }//2 or 3
            if (app.u8YcSendTYP != bt_u8YcSendTYP) { app.u8YcSendTYP = bt_u8YcSendTYP; }//0 or 1 or 2
            if (i_u16YkValidTime < CST_RANGE_LIMIT.u16YkValidTime_Min || i_u16YkValidTime > CST_RANGE_LIMIT.u16YkValidTime_Max)
            {
                MessageBox.Show("遥控有效时间越界，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxHightlight(tb_u16YkValidTime);
                return;
            }
            if (app.u16YkValidTime != i_u16YkValidTime) { app.u16YkValidTime = i_u16YkValidTime; }//10s~3600s
            //----
            if (i_u16IC_Time < CST_RANGE_LIMIT.u16IC_Time_Min || i_u16IC_Time > CST_RANGE_LIMIT.u16IC_Time_Max)
            {
                MessageBox.Show("站总召唤间隔时间，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxHightlight(tb_u16IC_Time);
                return;
            }
            if (app.u16IC_Time != i_u16IC_Time) { app.u16IC_Time = i_u16IC_Time; }//10s~3600s
            //----
            if (i_u16CI_Time < CST_RANGE_LIMIT.u16CI_Time_Min || i_u16CI_Time > CST_RANGE_LIMIT.u16CI_Time_Max)
            {
                MessageBox.Show("电度量总召唤间隔时间，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxHightlight(tb_u16CI_Time);
                return;
            }
            if (app.u16CI_Time != i_u16CI_Time) { app.u16CI_Time = i_u16CI_Time; }//10s~3600s
            //----
            if (i_u16CmdTime < CST_RANGE_LIMIT.u16CmdTime_Min || i_u16CmdTime > CST_RANGE_LIMIT.u16CmdTime_Max)
            {
                MessageBox.Show("对时时间间隔，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxHightlight(tb_u16CmdTime);
                return;
            }
            if (app.u16CmdTime != i_u16CmdTime) { app.u16CmdTime = i_u16CmdTime; }//10s~3600s
            //----
            //byte res1 = (byte)(cb_Res1.Checked == true ? 1 : 0);
            //if (res1 != app.u8Res1[0])
            //{
            //    app.u8Res1[0] = res1;
            //}
            //----
            if (i_u16YcChgSendCycle < CST_RANGE_LIMIT.u16YcChgSendCycle_Min || i_u16YcChgSendCycle > CST_RANGE_LIMIT.u16YcChgSendCycle_Max)
            {
                MessageBox.Show("遥测变化上送周期，请重新输入！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxHightlight(tb_YcChgSendCycle);
                return;
            }
            if (app.u16YcChgSendCycle != i_u16YcChgSendCycle) { app.u16YcChgSendCycle = i_u16YcChgSendCycle; }//0ms~1000ms
            //END
        }

        #region "KeyPress"
        private void tb_u16ServerPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8){ e.Handled = true; }
        }

        private void tb_u8K_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8) { e.Handled = true; }
        }

        private void tb_u8W_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8) { e.Handled = true; }
        }

        private void tb_u8T0_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8) { e.Handled = true; }
        }

        private void tb_u8T1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8) { e.Handled = true; }
        }

        private void tb_u8T2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8) { e.Handled = true; }
        }

        private void tb_u8T3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8) { e.Handled = true; }
        }

        private void tb_u16AppAddr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8) { e.Handled = true; }
        }

        private void tb_u16YkValidTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8) { e.Handled = true; }
        }

        private void tb_u16IC_Time_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8) { e.Handled = true; }
        }

        private void tb_u16CI_Time_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8) { e.Handled = true; }
        }

        private void tb_u16CmdTime_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8) { e.Handled = true; }
        }
        #endregion

        string getIP(byte[] bt)
        {
            return string.Format("{0}.{1}.{2}.{3}", bt[0], bt[1], bt[2], bt[3]);
        }

        void textBoxHightlight(TextBox tb)
        {
            tb.SelectionStart = 0;
            tb.SelectionLength = tb.Text.Length;
            tb.Focus(); 
        }

        private void cmb_u8LinkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            int iLinkType = cmb_u8LinkType.SelectedIndex;
            int iStationType = cmb_eStationType.SelectedIndex;
            if(iLinkType == 0 && iStationType != 1)
            {
                cmb_eStationType.SelectedIndex = 1;
            }
            else if (iLinkType == 1 && iStationType != 0)
            {
                cmb_eStationType.SelectedIndex = 0;
            }
        }
        //END
    }
}
