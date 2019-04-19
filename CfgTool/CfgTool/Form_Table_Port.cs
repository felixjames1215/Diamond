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
    public partial class Form_Table_Port : Form
    {
        const int CST_DGV_COLUMN_FWT = 5;
        const int CST_DGV_COLUMN_DEVICETABLE = 6;
        public string strCurrentPortName = "";
        public Form_Table_Port()
        {
            InitializeComponent();
        }

        private void Form_Table_Port_Load(object sender, EventArgs e)
        {
            set_dgv_Property();
            //init_Menu();
            init_View();
        }

        void set_dgv_Property()
        {
            for (int k = 0; k < dgv_Port.ColumnCount; k++)
            {
                if (k != CST_DGV_COLUMN_FWT && k != CST_DGV_COLUMN_DEVICETABLE)
                {
                    dgv_Port.Columns[k].ReadOnly = true;
                }
            }
            dgv_Port.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            //----
            ((DataGridViewComboBoxColumn)dgv_Port.Columns[CST_DGV_COLUMN_FWT]).Items.Clear();
            foreach (var t in Global.g_list_FWT)
            {
                ((DataGridViewComboBoxColumn)dgv_Port.Columns[CST_DGV_COLUMN_FWT]).Items.Add(t.Value.FWTName);
            }
            //----
            ((DataGridViewComboBoxColumn)dgv_Port.Columns[CST_DGV_COLUMN_DEVICETABLE]).Items.Clear();
            foreach (var t in Global.g_list_DeviceTable)
            {
                ((DataGridViewComboBoxColumn)dgv_Port.Columns[CST_DGV_COLUMN_DEVICETABLE]).Items.Add(t.Value.DeviceTableName);
            }
        }

        void init_Menu()
        {
            cms_PortCopy.Items.Clear();
            foreach (var t in Global.g_Model.lst_Table_Port)
            {
                cms_PortCopy.Items.Add(t.Value.PortName);
            }
        }

        void init_View()
        {
            int iCount = 0;
            dgv_Port.Rows.Clear();
            foreach(var t in Global.g_Model.lst_Table_Port)
            {
                dgv_Port.RowCount += 1;
                dgv_Port.Rows[iCount].Cells[0].Value = t.Value.Id;
                dgv_Port.Rows[iCount].Cells[1].Value = t.Value.PortName;
                dgv_Port.Rows[iCount].Cells[2].Value = t.Value.PhysicalAttribute;//物理属性。串口/网口/虚拟口
                dgv_Port.Rows[iCount].Cells[3].Value = t.Value.LogicAttribute;//逻辑属性。对上/对下
                dgv_Port.Rows[iCount].Cells[4].Value = (t.Value.Enabled == false ? "否" : "是");//是否使用
                //----
                if (t.Value.Enabled == false)
                {
                    dgv_Port.Rows[iCount].Cells[5].Value = "";//转发表名称
                    dgv_Port.Rows[iCount].Cells[5].ReadOnly = true;
                    dgv_Port.Rows[iCount].Cells[6].Value = "";//设备表名称
                    dgv_Port.Rows[iCount].Cells[6].ReadOnly = true;
                    dgv_Port.Rows[iCount].DefaultCellStyle.BackColor = Color.LightGray;
                }
                else
                {
                    if (t.Value.LogicAttribute == "对下")
                    {
                        dgv_Port.Rows[iCount].Cells[5].Value = "";//转发表名称
                        dgv_Port.Rows[iCount].Cells[5].ReadOnly = true;
                        dgv_Port.Rows[iCount].Cells[6].Value = t.Value.DeviceTableName;//设备表名称
                        dgv_Port.Rows[iCount].Cells[6].ReadOnly = false;
                        dgv_Port.Rows[iCount].DefaultCellStyle.BackColor = Color.LightYellow;
                    }
                    else if (t.Value.LogicAttribute == "对上")
                    {
                        dgv_Port.Rows[iCount].Cells[5].Value = t.Value.FWTName;//转发表名称
                        dgv_Port.Rows[iCount].Cells[5].ReadOnly = false;
                        dgv_Port.Rows[iCount].Cells[6].Value = "";//设备表名称
                        dgv_Port.Rows[iCount].Cells[6].ReadOnly = true;
                        dgv_Port.Rows[iCount].DefaultCellStyle.BackColor = Color.YellowGreen;
                    }
                }
                //----
                dgv_Port.Rows[iCount].Cells[7].Value = t.Value.ProtocolName;//规约类型
                dgv_Port.Rows[iCount].Cells[8].Value = t.Value.ProtocolInstanceNum;//规约实例号
                dgv_Port.Rows[iCount].Tag = t.Value;
                iCount += 1;
            }
        }

        private void dgv_Port_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == CST_DGV_COLUMN_FWT && e.RowIndex >= 0)
            {
                string fwtname = Convert.ToString(dgv_Port.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                CTablePort p = (CTablePort)dgv_Port.Rows[e.RowIndex].Tag;
                if(p != null) { p.FWTName = fwtname; }
            }
            if (e.ColumnIndex == CST_DGV_COLUMN_DEVICETABLE && e.RowIndex >= 0)
            {
                string devicetablename = Convert.ToString(dgv_Port.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                CTablePort p = (CTablePort)dgv_Port.Rows[e.RowIndex].Tag;
                if (p != null) { p.DeviceTableName = devicetablename; }
            }
        }

        private void dgv_Port_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1) { return; }
            if (e.Button == System.Windows.Forms.MouseButtons.Right && e.ColumnIndex == 1)
            {
                string strPortName = string.Format("{0}", dgv_Port.Rows[e.RowIndex].Cells[1].Value);
                string strPortType = string.Format("{0}", dgv_Port.Rows[e.RowIndex].Cells[2].Value);
                strCurrentPortName = strPortName;
                cms_PortCopy.Items.Clear();
                int iCount = 0;
                foreach (var t in Global.g_Model.lst_Table_Port)
                {
                    if (strPortType == t.Value.PhysicalAttribute && strPortName != t.Value.PortName)
                    {
                        cms_PortCopy.Items.Add("复制 " + t.Value.PortName);
                        this.cms_PortCopy.Items[iCount].Click += new EventHandler(cms_PortCopy_click);
                        iCount += 1;
                    }
                    
                }
                dgv_Port.ContextMenuStrip = cms_PortCopy;
            }
        }

        private void cms_PortCopy_click(object sender, EventArgs e)
        {
            ToolStripMenuItem Tsm = (sender as ToolStripMenuItem);
            string name = Tsm.Text;
            name = name.Replace("复制", "");
            name = name.Trim();
            CTablePort portCopy = new CTablePort();
            foreach (var t in Global.g_Model.lst_Table_Port)
            {
                if(name == t.Value.PortName)
                {
                    //strCurrentPortName
                    //name
                    t.Value.cfg_Port.convertProtocolCfg2ByteArray(t.Value.cfg_Port.eProtocol);
                    for (int k = 0; k < t.Value.cfg_Port.u8ProtocolCfg.Length; k++)
                    {
                        portCopy.cfg_Port.u8ProtocolCfg[k] = t.Value.cfg_Port.u8ProtocolCfg[k];
                    }
                    break;
                }
            }
            foreach (var t in Global.g_Model.lst_Table_Port)
            {
                if (strCurrentPortName == t.Value.PortName)
                {
                    for (int k = 0; k < t.Value.cfg_Port.u8ProtocolCfg.Length; k++)
                    {
                        t.Value.cfg_Port.u8ProtocolCfg[k] = portCopy.cfg_Port.u8ProtocolCfg[k];
                    }
                    t.Value.cfg_Port.parseProtocolCfg(t.Value.cfg_Port.eProtocol);
                    break;
                }
            }
        }
        //END
    }
}
