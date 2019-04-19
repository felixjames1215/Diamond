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
    //对上 or 对下 101规约
    public partial class Form_ProtocolPara_Up101 : Form
    {
        CTablePort TablePort = null;
        CommSafetyLayerConfig_t safety = null;
        Comm101LinkConfig_t lnk = null;
        Comm101AppConfig_t app = null;
        public Form_ProtocolPara_Up101(CTablePort tableport)
        {
            InitializeComponent();
            TablePort = tableport;
            safety = TablePort.cfg_Port.tCommB101UpCfg.tSafetyLayerCfg;
            lnk = TablePort.cfg_Port.tCommB101UpCfg.tLinkCfg;
            app = TablePort.cfg_Port.tCommB101UpCfg.tAppCfg;
        }

        private void Form_ProtocolPara_Up101_Click(object sender, EventArgs e)
        {
            lbl_Title.Focus();
        }

        private void Form_ProtocolPara_Up101_Load(object sender, EventArgs e)
        {
            initPara();
        }

        void initPara()
        {
            /////////////////////////////////////////////////////////////////////////////////////
            //----安全层
            //1、是否使用加密。0-false，否；1-true，是。
            cmb_bHaveSafetyLayer.SelectedIndex = (safety.bHaveSafetyLayer == false ? 0 : 1);
            tb_u16TimeDiffLimit.Text = safety.u16TimeDiffLimit.ToString();
            //----链路层
            //2、链路模式：非平衡式or平衡式
            if (lnk.u8Mode == 0x01)
            {
                cmb_u8Mode.SelectedIndex = 0;
            }
            else if (lnk.u8Mode == 0x02)
            {
                cmb_u8Mode.SelectedIndex = 1;
            }
            else
            {
                cmb_u8Mode.SelectedIndex = -1;
            }
            //3、站类型：主控站or被控站
            if (lnk.u8StationType == 0x01)
            {
                cmb_u8StationType.SelectedIndex = 0;
            }
            else if (lnk.u8StationType == 0x02)
            {
                cmb_u8StationType.SelectedIndex = 1;
            }
            else
            {
                cmb_u8StationType.SelectedIndex = -1;
            }
            //4、链路地址：1~254
            tb_u16LinkAddr.Text = lnk.u16LinkAddr.ToString();
            //5、链路地址长度：1or2
            cmb_u8LinkAddrLen.SelectedIndex = lnk.u8LinkAddrLen - 1;
            //6、重发间隔（s）：1-30s；
            tb_u8ResendInterval.Text = lnk.u8ResendInterval.ToString();
            //7、重发次数：0-5次
            tb_u8ResendNum.Text = lnk.u8ResendNum.ToString();
            //8、DIR位：0or1
            cmb_u8Dir.SelectedIndex = lnk.u8Dir;
            //----应用层
            //9、应用层地址：1~254
            try
            {
                tb_u16AppAddr.Text = app.u16AppAddr.ToString();
                //10、应用层地址长度：1~2
                cmb_u8AppAddrLen.SelectedIndex = app.u8AppAddrLen - 1;
                //11、传送原因长度：1~2
                cmb_u8CotLen.SelectedIndex = app.u8CotLen - 1;
                //12、信息对象地址长度：2~3
                cmb_u8ObjAddrLen.SelectedIndex = app.u8ObjAddrLen - 2;
                //13、遥测上送类型：ASDU9归一化值”、“ASDU11标度化值”、“ASDU13短浮点数”三个，配置值对应0/1/2
                cmb_u8YcSendTYP.SelectedIndex = app.u8YcSendTYP;
                //14、遥控有效时间：10s~3600s
                tb_u16YkValidTime.Text = app.u16YkValidTime.ToString();
                //15、站总召唤间隔时间：10s~3600s
                tb_u16IC_Time.Text = app.u16IC_Time.ToString();
                //16、电度量总召唤间隔时间：10s~3600s
                tb_u16CI_Time.Text = app.u16CI_Time.ToString();
                //17、对时时间间隔：10s~3600s
                tb_u16CmdTime.Text = app.u16CmdTime.ToString();
                //18、新增：针对遥测变化上送，设置的标志位，默认是0，勾选是1
                cb_Res1.Checked = (app.u8Res1[0] == 1 ? true : false);
                tb_YcChgSendCycle.Text = app.u16YcChgSendCycle.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            /////////////////////////////////////////////////////////////////////////////////////
        }

        #region "参数修改自动保存"
        //一、安全层
        //1、是否使用加密：0or1
        private void cmb_bHaveSafetyLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            safety.bHaveSafetyLayer = (cmb_bHaveSafetyLayer.SelectedIndex == 0 ? false : true);
        }
        private void tb_u16TimeDiffLimit_TextChanged(object sender, EventArgs e)
        {
            safety.u16TimeDiffLimit = Convert.ToUInt16(tb_u16TimeDiffLimit.Text);
        }
        //二、链路层
        //2、链路模式：非平衡式or平衡式
        private void cmb_u8Mode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmb_u8Mode.SelectedIndex == 0)
            {
                lnk.u8Mode = 1;
            }
            else if (cmb_u8Mode.SelectedIndex == 1)
            {
                lnk.u8Mode = 2;
            }
            else
            {
                lnk.u8Mode = 0;
            }
        }
        //3、站类型：主控站or被控站
        private void cmb_u8StationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmb_u8StationType.SelectedIndex == 0)
            {
                lnk.u8StationType = 1;
            }
            else if (cmb_u8StationType.SelectedIndex == 1)
            {
                lnk.u8StationType = 2;
            }
            else
            {
                lnk.u8StationType = 0;
            }
            //----非平衡，主控，dir = 0
            if (lnk.u8StationType == 1 && lnk.u8Mode == 1)
            {
                cmb_u8Dir.SelectedIndex = 0;
            }
        }
        //4、链路地址：1~254
        private void tb_u16LinkAddr_TextChanged(object sender, EventArgs e)
        {
            lnk.u16LinkAddr = Convert.ToUInt16(tb_u16LinkAddr.Text);
        }
        //5、链路地址长度：1or2
        private void cmb_u8LinkAddrLen_SelectedIndexChanged(object sender, EventArgs e)
        {
            lnk.u8LinkAddrLen = Convert.ToByte(cmb_u8LinkAddrLen.SelectedIndex + 1);
        }
        //6、重发间隔（s）：1-30s；
        private void tb_u8ResendInterval_TextChanged(object sender, EventArgs e)
        {
            lnk.u8ResendInterval = Convert.ToByte(tb_u8ResendInterval.Text);
        }
        //7、重发次数：0-5次
        private void tb_u8ResendNum_TextChanged(object sender, EventArgs e)
        {
            lnk.u8ResendNum = Convert.ToByte(tb_u8ResendNum.Text);
        }
        //8、DIR位：0or1
        private void cmb_u8Dir_SelectedIndexChanged(object sender, EventArgs e)
        {
            lnk.u8Dir = Convert.ToByte(cmb_u8Dir.SelectedIndex);
        }

        //三、应用层
        //9、应用层地址：1~254
        private void tb_u16AppAddr_TextChanged(object sender, EventArgs e)
        {
            app.u16AppAddr = Convert.ToUInt16(tb_u16AppAddr.Text);
        }
        //10、应用层地址长度：1~2
        private void cmb_u8AppAddrLen_SelectedIndexChanged(object sender, EventArgs e)
        {
            app.u8AppAddrLen = Convert.ToByte(cmb_u8AppAddrLen.SelectedIndex + 1);
        }
        //11、传送原因长度：1~2
        private void cmb_u8CotLen_SelectedIndexChanged(object sender, EventArgs e)
        {
            app.u8CotLen = Convert.ToByte(cmb_u8CotLen.SelectedIndex + 1);
        }
        //12、信息对象地址长度：2~3
        private void cmb_u8ObjAddrLen_SelectedIndexChanged(object sender, EventArgs e)
        {
            app.u8ObjAddrLen = Convert.ToByte(cmb_u8ObjAddrLen.SelectedIndex + 2);
        }
        //13、新增[遥测上送类型:ASDU9归一化值”、“ASDU11标度化值”、“ASDU13短浮点数”三个，配置值对应0/1/2]，jifeng，2017-6-25
        private void cmb_u8YcSendTYP_SelectedIndexChanged(object sender, EventArgs e)
        {
            app.u8YcSendTYP = Convert.ToByte(cmb_u8YcSendTYP.SelectedIndex);
        }
        //14、遥控有效时间：10s~3600s
        private void tb_u8YkValidTime_TextChanged(object sender, EventArgs e)
        {
            app.u16YkValidTime = Convert.ToUInt16(tb_u16YkValidTime.Text);
        }
        //15、站总召唤间隔时间：10s~3600s
        private void tb_u8IC_Time_TextChanged(object sender, EventArgs e)
        {
            app.u16IC_Time = Convert.ToUInt16(tb_u16IC_Time.Text);
        }
        //16、电度量总召唤间隔时间：10s~3600s
        private void tb_u8CI_Time_TextChanged(object sender, EventArgs e)
        {
            app.u16CI_Time = Convert.ToUInt16(tb_u16CI_Time.Text);
        }
        //17、对时时间间隔：10s~3600s
        private void tb_u16CmdTime_TextChanged(object sender, EventArgs e)
        {
            app.u16CmdTime = Convert.ToUInt16(tb_u16CmdTime.Text);
        }
        //18、遥测变化
        private void cb_Res1_CheckedChanged(object sender, EventArgs e)
        {
            app.u8Res1[0] = (byte)(cb_Res1.Checked == true ? 1 : 0);
        }

        private void tb_YcChgSendCycle_TextChanged(object sender, EventArgs e)
        {
            app.u16YcChgSendCycle = Convert.ToUInt16(tb_YcChgSendCycle.Text);
        }
        #endregion

        #region "KeyPress"
        private void tb_u16TimeDiffLimit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8) { e.Handled = true; }
        }

        private void tb_u16LinkAddr_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8) { e.Handled = true; }
        }

        private void tb_u8ResendInterval_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsNumber(e.KeyChar)) && e.KeyChar != (char)8) { e.Handled = true; }
        }

        private void tb_u8ResendNum_KeyPress(object sender, KeyPressEventArgs e)
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

        private void pb_Dir_MouseEnter(object sender, EventArgs e)
        {
            string strTitle = "DIR位：";
            string strCaption = string.Format("平衡模式，装置的DIR = 1，主站的DIR = 0！");
            showTip(strTitle, strCaption, this.pb_Dir, 10000);
        }


        #region "showTip"
        private void showTip(string title, string caption, Control ctrl, int popdelay)
        {
            toolTip1.ToolTipIcon = ToolTipIcon.Info;
            toolTip1.AutoPopDelay = popdelay;
            toolTip1.ToolTipTitle = title;
            toolTip1.IsBalloon = true;
            toolTip1.SetToolTip(ctrl, caption);
        }
        #endregion

        private void pb_LinkMode_MouseEnter(object sender, EventArgs e)
        {
            string strTitle = "链路模式：";
            string s1,s2,s3,s4;
            s1 = string.Format("1、非平衡方式传输：\r\n");
            s2 = string.Format("只有主站启动各种链路传输服务，子站只有当主站请求时才传输。这种传输方式对于所有网络结构都可适用。 \r\n");
            s3 = string.Format("2、平衡方式传输：\r\n");
            s4 = string.Format("主站和子站可以同时启动链路传输服务，所以必须有一对全双工的通道。\r\n");
            string strCaption = s1 + s2 + s3 + s4;
            showTip(strTitle, strCaption, this.pb_LinkMode, 15000);
        }
        //----END
    }
}
