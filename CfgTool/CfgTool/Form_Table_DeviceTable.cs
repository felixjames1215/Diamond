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
    public partial class Form_Table_DeviceTable : Form
    {
        public Form_Table_DeviceTable()
        {
            InitializeComponent();
        }

        private void Form_Table_DeviceTable_Load(object sender, EventArgs e)
        {
            set_dgv_Property();
            init_View();
        }

        void set_dgv_Property()
        {
            dgv_DeviceTable.Columns[0].ReadOnly = true;
            dgv_DeviceTable.Columns[0].DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
        }

        private void init_View()
        {
            int iPos = 0;
            int iCount = 0;
            foreach (var t in Global.g_list_DeviceTable.Values)
            {
                dgv_DeviceTable.RowCount += 1;
                iPos = 0;
                dgv_DeviceTable.Rows[iCount].Cells[iPos++].Value = iCount + 1;
                dgv_DeviceTable.Rows[iCount].Cells[iPos++].Value = t.DeviceTableName;
                dgv_DeviceTable.Rows[iCount].Tag = t;
                iCount += 1;
            }
        }

        private void dgv_DeviceTable_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) { return; };
            if (e.ColumnIndex == 1)//设备表名称
            {
                CDeviceTable d = (CDeviceTable)dgv_DeviceTable.Rows[e.RowIndex].Tag;
                if (d == null) { return; };
                string dtname_Origin = d.DeviceTableName;
                string dtname_New = Convert.ToString(dgv_DeviceTable.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                d.DeviceTableName = dtname_New;
                Form_CfgTool.pMainForm.refresh_TreeNode_DeviceTableName(d);
                //----
                foreach (var t in Global.g_Model.lst_Table_Port.Values)
                {
                    if (t.DeviceTableName == dtname_Origin)
                    {
                        t.DeviceTableName = dtname_New;
                        break;
                    }
                }
            }
        }

    }
}
