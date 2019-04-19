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
    public partial class Form_Table_Device : Form
    {
        CDeviceTable DeviceTable = null;
        const int CST_DGV_COLUMN_MODEL = 3;

        public Form_Table_Device(CDeviceTable cdt)
        {
            InitializeComponent();
            DeviceTable = cdt;
        }

        private void Form_Table_Device_Load(object sender, EventArgs e)
        {
            set_dgv_Property();
            init_View();
        }

        void set_dgv_Property()
        {
            dgv_Device.Columns[0].ReadOnly = true;
            dgv_Device.Columns[0].DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            //----
            ((DataGridViewComboBoxColumn)dgv_Device.Columns[CST_DGV_COLUMN_MODEL]).Items.Clear();
            foreach (var t in Global.g_list_Model)
            {
                ((DataGridViewComboBoxColumn)dgv_Device.Columns[CST_DGV_COLUMN_MODEL]).Items.Add(t.Value.ModelName);
            }
        }

        private void init_View()
        {
            int iPos = 0;
            int iCount = 0;
            foreach (var t in DeviceTable.lst_Device.Values)
            {
                dgv_Device.RowCount += 1;
                iPos = 0;
                dgv_Device.Rows[iCount].Cells[iPos++].Value = iCount + 1;
                dgv_Device.Rows[iCount].Cells[iPos++].Value = t.DeviceName;
                dgv_Device.Rows[iCount].Cells[iPos++].Value = t.CommAddr;
                dgv_Device.Rows[iCount].Cells[iPos++].Value = t.ModelName;
                dgv_Device.Rows[iCount].Tag = t;
                iCount += 1;
            }
        }

        private void dgv_Device_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) { return; };
            if (e.ColumnIndex >= 1 && e.ColumnIndex <= 3)//设备名称
            {
                CDevice d = (CDevice)dgv_Device.Rows[e.RowIndex].Tag;
                if (d == null) { return; };
                if (e.ColumnIndex == 1)
                {
                    d.DeviceName = Convert.ToString(dgv_Device.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    Form_CfgTool.pMainForm.refresh_TreeNode_DeviceName(DeviceTable, d);
                }
                else if (e.ColumnIndex == 2)//设备地址
                {
                    d.CommAddr = Convert.ToInt32(dgv_Device.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                }
                else if (e.ColumnIndex == 3)//模板名称
                {
                    string modelname = Convert.ToString(dgv_Device.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                    CDevice p = (CDevice)dgv_Device.Rows[e.RowIndex].Tag;
                    if (p != null) { p.ModelName = modelname; }
                }
            }
        }
    }
}
