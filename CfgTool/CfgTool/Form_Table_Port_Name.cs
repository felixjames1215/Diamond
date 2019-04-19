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
    public partial class Form_Table_Port_Name : Form
    {
        //////////////////////////////////////////////////////
        string PortName = "";
        CTablePort TablePort = null;
        PortCfg cfgport = null;
        PORT_T port = null;
        SPORT_T sport = null;
        NPORT_T nport = null;
        private String[] strArray_Property;
        private String[] strArray_Protocol;
        private String[] strArray_Protocol_Up;
        private String[] strArray_Protocol_Down;

        //private String[] strArray_Protocol_Up_Com;
        //private String[] strArray_Protocol_Up_Net;
        //private String[] strArray_Protocol_Down_Com;
        //private String[] strArray_Protocol_Down_Net;
        private Font myFont;
        private bool bPropertyRefresh = false;
        private bool bProtocolRefresh = false;
        //////////////////////////////////////////////////////
        #region "构造函数"
        public Form_Table_Port_Name(string name)
        {
            InitializeComponent();
            PortName = name;
            foreach (var t in Global.g_Model.lst_Table_Port.Values)
            {
                if (t.PortName == PortName)
                {
                    TablePort = t;
                    cfgport = t.cfg_Port;
                    port = cfgport.tPort;
                    sport = port.SPort;
                    nport = port.NPort;
                    break;
                }
            }
            lbl_PortName_1.Text = PortName;
            lbl_PortName_2.Text = PortName;
            lbl_PortName_3.Text = PortName;

            strArray_Property = new string[2] { "对上", "对下" };
            strArray_Protocol = new string[10]{"对上101规约", "对上104规约", "对上MODBUS规约", "对上CDT规约", 
                                               "对下101规约", "对下104规约", "对下MODBUS规约", "对下CDT规约", 
                                               "对上101规约V1", "对上104规约V1"};

            strArray_Protocol_Up = new string[6]{"对上101规约", "对上104规约", "对上MODBUS规约", "对上CDT规约", 
                                               "对上101规约V1", "对上104规约V1"};
            strArray_Protocol_Down = new string[4] { "对下101规约", "对下104规约", "对下MODBUS规约", "对下CDT规约" };

            //strArray_Protocol_Up_Com = new string[4]{"对上101规约", "对上MODBUS规约", "对上CDT规约", "对上104规约V1"};
            //strArray_Protocol_Up_Net = new string[2]{"对上104规约", "对上104规约V1"};

            //strArray_Protocol_Down_Com = new string[3] { "对下101规约", "对下MODBUS规约", "对下CDT规约" };
            //strArray_Protocol_Down_Net = new string[1] { "对下104规约"};
        }
        #endregion

        #region "窗体Load"
        private void Form_Table_Port_Name_Load(object sender, EventArgs e)
        {
            myFont = new System.Drawing.Font("Comic Sans", 11);

            cmb_LogicPara_Property.Items.Clear();
            cmb_LogicPara_Property.DataSource = strArray_Property;
            //cmb_LogicPara_Property.SelectedIndex = 0;
            cmb_LogicPara_Property.Text = TablePort.LogicAttribute;
            bPropertyRefresh = true;
            init_Protocol(cmb_LogicPara_Property.Text);//屏蔽对上或对下的过滤,列出全部规约
            //cmb_LogicPara_Protocol.Items.Clear();
            //cmb_LogicPara_Protocol.DataSource = strArray_Protocol;
            //cmb_LogicPara_Protocol.SelectedIndex = 0;

            init_Serial();//初始化 串口 下拉列表

            init_Para();//初始化 串口/网口/虚拟口 参数

            init_ProtocolPara();
        }
        #endregion

        #region "初始化串口和规约下拉列表"
        void init_Serial()
        {
            //----
            cmb_SerialPort_BaudRate.Items.Clear();
            cmb_SerialPort_DataBit.Items.Clear();
            cmb_SerialPort_StopBit.Items.Clear();
            cmb_SerialPort_CheckBit.Items.Clear();
            //----
            foreach (var t in Global.Serial_BaudRate)
            {
                cmb_SerialPort_BaudRate.Items.Add(t);
            }
            //----
            foreach (var t in Global.Serial_DataBits)
            {
                cmb_SerialPort_DataBit.Items.Add(t);
            }
            //----
            foreach (var t in Global.Serial_StopBits)
            {
                cmb_SerialPort_StopBit.Items.Add(t);
            }
            //----
            foreach (var t in Global.Serial_Parity)
            {
                cmb_SerialPort_CheckBit.Items.Add(t);
            }
            //----
        }

        void init_Protocol(string logicattr)
        {
            //过滤出：对上 or 对下
            try
            {
                //bProtocolRefresh = true;
                cmb_LogicPara_Protocol.DataSource = null;
                cmb_LogicPara_Protocol.Items.Clear();
                if (logicattr == strArray_Property[0])
                {//对上
                    cmb_LogicPara_Protocol.DataSource = strArray_Protocol_Up;
                    //if(PortName.Contains("串口"))
                    //{
                    //    cmb_LogicPara_Protocol.Text = "对上101规约";
                    //}
                    //else if (PortName.Contains("网口"))
                    //{
                    //    cmb_LogicPara_Protocol.Text = "对上104规约";
                    //}
                    //cmb_LogicPara_Protocol.SelectedIndex = 0;
                }
                else if (logicattr == strArray_Property[1])
                {//对下
                    cmb_LogicPara_Protocol.DataSource = strArray_Protocol_Down;
                    //cmb_LogicPara_Protocol.SelectedIndex = 0;
                    //if (PortName.Contains("串口"))
                    //{
                    //    cmb_LogicPara_Protocol.Text = "对下101规约";
                    //}
                    //else if (PortName.Contains("网口"))
                    //{
                    //    cmb_LogicPara_Protocol.Text = "对下104规约";
                    //}
                }
                cmb_LogicPara_Protocol.Text = TablePort.ProtocolName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        #region "初始化参数"
        void init_Para()
        {
            //------------------------------------------------------------------
            cb_Enabled.Checked = TablePort.Enabled;//是否使用
            tb_u16PortAddr.Text = TablePort.Addr.ToString();//地址。取值范围：1-65535
            //------------------------------------------------------------------
            if (TablePort.PhysicalAttribute == Global.cst_Port_Serial) //串口
            {
                gb_PhysicalPara_Network.Visible = false;
                gb_PhysicalPara_Serial.Left = 50;
                gb_PhysicalPara_Serial.Top = 50;
                //----------------------------------------------------------串口参数
                if (TablePort.ProtocolName.Contains("101") == true || TablePort.ProtocolName.Contains("MODBUS") == true)
                {
                    cmb_SerialPort_BaudRate.Text = sport.u32BaudRate.ToString();//波特率
                    cmb_SerialPort_DataBit.SelectedIndex = getIndex_DataBit(sport.u8DataBit);//数据位:8,7,6,5
                    cmb_SerialPort_StopBit.SelectedIndex = (int)sport.u8StopBit - 1;//停止位:1,2
                    cmb_SerialPort_CheckBit.SelectedIndex = (int)sport.u8CheckBit;//校验位:0,1,2
                }
                //----------------------------------------------------------
            }
            else if (TablePort.PhysicalAttribute == Global.cst_Port_Network || TablePort.PhysicalAttribute == Global.cst_Port_Virtual) //网口
            {
                gb_PhysicalPara_Serial.Visible = false;
                gb_PhysicalPara_Network.Left = 50;
                gb_PhysicalPara_Network.Top = 50;
                //----------------------------------------------------------网口参数
                tb_Network_IP.Text = getIP(nport.u8IP);//IP地址
                tb_Network_Mask.Text = getIP(nport.u8Mask);//子网掩码
                //新增MAC地址。2017-5-17
                tb_u8MAC_1.Text = nport.u8MAC[0].ToString("X2");
                tb_u8MAC_2.Text = nport.u8MAC[1].ToString("X2");
                tb_u8MAC_3.Text = nport.u8MAC[2].ToString("X2");
                tb_u8MAC_4.Text = nport.u8MAC[3].ToString("X2");
                tb_u8MAC_5.Text = nport.u8MAC[4].ToString("X2");
                tb_u8MAC_6.Text = nport.u8MAC[5].ToString("X2");
                //----------------------------------------------------------
                if (TablePort.PortName.Contains("虚拟"))
                {
                    try
                    {
                        foreach (Control c in gb_PhysicalPara_Network.Controls)
                        {
                            int itag = Convert.ToInt32(c.Tag);
                            if (itag == 100) { c.Visible = false; }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
            //------------------------------------------------------------------
            gb_LogicPara.Left = 50;
            gb_LogicPara.Top = 50;
            //----------------------------------------------------------逻辑参数
            cmb_LogicPara_Property.Text = TablePort.LogicAttribute;//逻辑属性
            //cmb_LogicPara_Protocol.SelectedIndex = (int)cfgport.eProtocol;//规约名称
            cmb_LogicPara_Protocol.Text = TablePort.ProtocolName;//规约名称
            //----------------------------------------------------------规约参数
            gb_ProtocolPara.Left = 50;
            gb_ProtocolPara.Top = 50;
            //------------------------------------------------------------------
        }

        void init_ProtocolPara()
        {
            gb_ProtocolPara.Controls.Clear();
            if (PortName.Contains("串口") == true)
            {
                Form_ProtocolPara_Up101 dlg = new Form_ProtocolPara_Up101(TablePort);
                dlg.TopLevel = false;
                dlg.Dock = DockStyle.Fill;
                dlg.Show();
                gb_ProtocolPara.Controls.Add(dlg);
            }
            else if (PortName.Contains("网口") == true)
            {
                Form_ProtocolPara_Up104 dlg = new Form_ProtocolPara_Up104(TablePort);
                dlg.TopLevel = false;
                dlg.Dock = DockStyle.Fill;
                dlg.Show();
                gb_ProtocolPara.Controls.Add(dlg);
            }
        }

        int getIndex_DataBit(byte v)
        {
            int index = 0;
            if (v == 8)
            {
                index = 0;
            }
            else if (v == 7)
            {
                index = 1;
            }
            else if (v == 6)
            {
                index = 2;
            }
            else if (v == 5)
            {
                index = 3;
            }
            return index;
        }
        #endregion

        #region "物理参数"
        #region "串口"
        private void cmb_SerialPort_BaudRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            sport.u32BaudRate = Convert.ToUInt32(cmb_SerialPort_BaudRate.Text);
        }

        private void cmb_SerialPort_DataBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            sport.u8DataBit = getDataBit(cmb_SerialPort_DataBit.SelectedIndex);
        }

        private void cmb_SerialPort_StopBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            sport.u8StopBit = Convert.ToByte(cmb_SerialPort_StopBit.SelectedIndex + 1);
        }

        private void cmb_SerialPort_Parity_SelectedIndexChanged(object sender, EventArgs e)
        {
            sport.u8CheckBit = Convert.ToByte(cmb_SerialPort_CheckBit.SelectedIndex);
        }

        byte getDataBit(int v)
        {
            byte b = 0x00;
            if (v == 0)
            {
                b = 0x08;
            }
            else if (v == 1)
            {
                b = 0x07;
            }
            else if (v == 2)
            {
                b = 0x06;
            }
            else if (v == 3)
            {
                b = 0x05;
            }
            return b;
        }
        #endregion
        /// ////////////////////////////////////////////////////////////////////////
        #region "网口"
        private void tb_Network_IP_TextChanged(object sender, EventArgs e)
        {
            ;
        }

        private void tb_Network_Mask_TextChanged(object sender, EventArgs e)
        {
            ;
        }

        private void tb_Network_Port_TextChanged(object sender, EventArgs e)
        {
            ;
        }
        #endregion
        #endregion

        #region "逻辑参数"
        private void cmb_LogicPara_Property_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bPropertyRefresh == false)
            {
                bPropertyRefresh = true;
                return;
            }
            //对上 or 对下
            bProtocolRefresh = false;
            port.u8LogicAttr = (cmb_LogicPara_Property.Text == "对上" ? (byte)3 : (byte)4);
            TablePort.LogicAttribute = cmb_LogicPara_Property.Text;
            init_Protocol(cmb_LogicPara_Property.Text);//屏蔽对上或对下的过滤,列出全部规约
        }

        private void cmb_LogicPara_Protocol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bProtocolRefresh == false)
            {
                bProtocolRefresh = true;
                return;
            }
            int iProtocolId = -1;

            TablePort.ProtocolName = cmb_LogicPara_Protocol.Text;//规约名称
            iProtocolId = GetProtocolId(TablePort.ProtocolName);
            //lbl_PortName_2.Text = iProtocolId.ToString();
            if (-1 == iProtocolId)
            {
                //MessageBox.Show("规约ID错误，请检查！");
                return;
            }
            TablePort.cfg_Port.eProtocol = (UserProtocolType_e)iProtocolId;
            refreshInstanceNum();//刷新规约实例号（重新编号）
        }

        void refreshInstanceNum()
        {
            for (int k = 0; k < Global.Protocol_Support.Length; k++)
            {
                int iIns = 0;
                foreach (var t in Global.g_Model.lst_Table_Port)
                {
                    if (t.Value.Enabled == false)
                    {
                        t.Value.ProtocolInstanceNum = 0;
                        t.Value.cfg_Port.u8ProtocolInstNo = Convert.ToByte(t.Value.ProtocolInstanceNum);
                        continue;
                    }
                    if (Global.Protocol_Support[k] == t.Value.ProtocolName)
                    {
                        t.Value.ProtocolInstanceNum = iIns;
                        t.Value.cfg_Port.u8ProtocolInstNo = Convert.ToByte(t.Value.ProtocolInstanceNum);
                        iIns += 1;
                    }
                }
            }
        }
        #endregion

        #region "窗体Click"
        private void tp_PhysicalPara_Click(object sender, EventArgs e)
        {
            lbl_PortName_1.Focus();
        }
        #endregion

        #region "参数变化事件（使能和地址）"
        private void cb_Enabled_CheckedChanged(object sender, EventArgs e)
        {
            //端口是否使用标志位
            TablePort.Enabled = cb_Enabled.Checked;
            TablePort.cfg_Port.bUsed = TablePort.Enabled;
            refreshInstanceNum();
        }

        private void tb_u16PortAddr_TextChanged(object sender, EventArgs e)
        {
            TablePort.Addr = Convert.ToInt32(tb_u16PortAddr.Text);
            //----
            TablePort.cfg_Port.tPort.u16PortAddr = Convert.ToUInt16(TablePort.Addr);
        }
        #endregion

        private void btn_Network_OK_Click(object sender, EventArgs e)
        {
            //----IP地址
            string s = tb_Network_IP.Text;
            string[] sa = s.Split('.');
            if (sa.Length != 4)
            {
                MessageBox.Show("IP地址不符合规范，请检查！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxHightlight(tb_Network_IP);
                return;
            }
            for (int k = 0; k < 4; k++)
            {
                nport.u8IP[k] = Convert.ToByte(sa[k]);
                TablePort.cfg_Port.tCommB104UpCfg.tLinkCfg.u8ServerIP[k] = nport.u8IP[k];
                TablePort.cfg_Port.tCommB104UpCfg.tLinkCfg.u8ClientIP[k] = nport.u8IP[k];
                TablePort.cfg_Port.tCommB104UpCfg.tLinkCfg.u8ClientIP2[k] = nport.u8IP[k];
            }
            //if (TablePort.cfg_Port.tCommB104UpCfg.tLinkCfg.u8ClientIP[3] <= 254)
            //{
            //    TablePort.cfg_Port.tCommB104UpCfg.tLinkCfg.u8ClientIP[3] += 1;
            //}
            //else
            //{
            //    TablePort.cfg_Port.tCommB104UpCfg.tLinkCfg.u8ClientIP[3] = 101;
            //}
            
            init_ProtocolPara();
            //----子网掩码
            string s2 = tb_Network_Mask.Text;
            string[] sa2 = s2.Split('.');
            if (sa2.Length != 4)
            {
                MessageBox.Show("子网掩码不符合规范，请检查！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBoxHightlight(tb_Network_Mask);
                return;
            }
            for (int k = 0; k < 4; k++)
            {
                nport.u8Mask[k] = Convert.ToByte(sa2[k]);
            }
            //----MAC地址
            if (tb_u8MAC_1.Text.Trim() == "" || tb_u8MAC_1.Text.Length != 2 ||
                tb_u8MAC_2.Text.Trim() == "" || tb_u8MAC_2.Text.Length != 2 ||
                tb_u8MAC_3.Text.Trim() == "" || tb_u8MAC_3.Text.Length != 2 ||
                tb_u8MAC_4.Text.Trim() == "" || tb_u8MAC_4.Text.Length != 2 ||
                tb_u8MAC_5.Text.Trim() == "" || tb_u8MAC_5.Text.Length != 2 ||
                tb_u8MAC_6.Text.Trim() == "" || tb_u8MAC_6.Text.Length != 2)
            {
                MessageBox.Show("请输入完整的MAC地址！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            nport.u8MAC[0] = Convert.ToByte(tb_u8MAC_1.Text, 16);
            nport.u8MAC[1] = Convert.ToByte(tb_u8MAC_2.Text, 16);
            nport.u8MAC[2] = Convert.ToByte(tb_u8MAC_3.Text, 16);
            nport.u8MAC[3] = Convert.ToByte(tb_u8MAC_4.Text, 16);
            nport.u8MAC[4] = Convert.ToByte(tb_u8MAC_5.Text, 16);
            nport.u8MAC[5] = Convert.ToByte(tb_u8MAC_6.Text, 16);
        }

        void textBoxHightlight(TextBox tb)
        {
            tb.SelectionStart = 0;
            tb.SelectionLength = tb.Text.Length;
            tb.Focus();
        }

        string getIP(byte[] bt)
        {
            string res = "";
            res = string.Format("{0}.{1}.{2}.{3}",
                                bt[0], bt[1], bt[2], bt[3]);
            return res;
        }

        #region "MAC地址的KeyPress事件"
        private void tb_u8MAC_1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 'a' && e.KeyChar <= 'f') || (e.KeyChar >= 'A' && e.KeyChar <= 'F')
                || (e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == 8))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }

        private void tb_u8MAC_2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 'a' && e.KeyChar <= 'f') || (e.KeyChar >= 'A' && e.KeyChar <= 'F')
                || (e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == 8))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }

        private void tb_u8MAC_3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 'a' && e.KeyChar <= 'f') || (e.KeyChar >= 'A' && e.KeyChar <= 'F')
                || (e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == 8))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }

        private void tb_u8MAC_4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 'a' && e.KeyChar <= 'f') || (e.KeyChar >= 'A' && e.KeyChar <= 'F')
                || (e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == 8))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }

        private void tb_u8MAC_5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 'a' && e.KeyChar <= 'f') || (e.KeyChar >= 'A' && e.KeyChar <= 'F')
                || (e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == 8))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }

        }

        private void tb_u8MAC_6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 'a' && e.KeyChar <= 'f') || (e.KeyChar >= 'A' && e.KeyChar <= 'F')
                || (e.KeyChar >= '0' && e.KeyChar <= '9') || (e.KeyChar == 8))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        #endregion

        private void cmb_LogicPara_Property_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == -1) { return; }
            e.Graphics.FillRectangle(Brushes.GreenYellow, e.Bounds);
            e.Graphics.DrawString(strArray_Property[e.Index], myFont, Brushes.Blue, new Point(e.Bounds.X + 2, e.Bounds.Y + 8));
            if ((e.State & DrawItemState.Focus) == 0)
            {
                e.Graphics.FillRectangle(Brushes.White, e.Bounds);
                e.Graphics.DrawString(strArray_Property[e.Index], myFont, Brushes.Blue, new Point(e.Bounds.X + 2, e.Bounds.Y + 8));
            }
        }

        private void cmb_LogicPara_Protocol_DrawItem(object sender, DrawItemEventArgs e)
        {
            //if (e.Index == -1) { return; }
            //e.Graphics.FillRectangle(Brushes.GreenYellow, e.Bounds);
            //e.Graphics.DrawString(strArray_Protocol[e.Index], myFont, Brushes.Blue, new Point(e.Bounds.X + 2, e.Bounds.Y + 8));
            //if ((e.State & DrawItemState.Focus) == 0)
            //{
            //    e.Graphics.FillRectangle(Brushes.White, e.Bounds);
            //    e.Graphics.DrawString(strArray_Protocol[e.Index], myFont, Brushes.Blue, new Point(e.Bounds.X + 2, e.Bounds.Y + 8));
            //}  
            string strProperty = cmb_LogicPara_Property.Text;
            if (strProperty == strArray_Property[0])
            {//对上
                if (e.Index == -1) { return; }
                e.Graphics.FillRectangle(Brushes.GreenYellow, e.Bounds);
                e.Graphics.DrawString(strArray_Protocol_Up[e.Index], myFont, Brushes.Blue, new Point(e.Bounds.X + 2, e.Bounds.Y + 8));
                if ((e.State & DrawItemState.Focus) == 0)
                {
                    e.Graphics.FillRectangle(Brushes.White, e.Bounds);
                    e.Graphics.DrawString(strArray_Protocol_Up[e.Index], myFont, Brushes.Blue, new Point(e.Bounds.X + 2, e.Bounds.Y + 8));
                }
            }
            else if (strProperty == strArray_Property[1])
            {//对下
                if (e.Index == -1) { return; }
                e.Graphics.FillRectangle(Brushes.GreenYellow, e.Bounds);
                e.Graphics.DrawString(strArray_Protocol_Down[e.Index], myFont, Brushes.Blue, new Point(e.Bounds.X + 2, e.Bounds.Y + 8));
                if ((e.State & DrawItemState.Focus) == 0)
                {
                    e.Graphics.FillRectangle(Brushes.White, e.Bounds);
                    e.Graphics.DrawString(strArray_Protocol_Down[e.Index], myFont, Brushes.Blue, new Point(e.Bounds.X + 2, e.Bounds.Y + 8));
                }
            }
            else
            {
                MessageBox.Show(strProperty);
            }
        }

        private void tp_LogicPara_Click(object sender, EventArgs e)
        {
            gb_LogicPara.Focus();
        }

        private int GetProtocolId(string strProtocol)
        {
            for (int m = 0; m < strArray_Protocol.Length; m++)
            {
                if (strArray_Protocol[m] == strProtocol)
                {
                    return m;
                }
            }
            return -1;
        }
        //////////////////////////////////////////////////////
    }
}
