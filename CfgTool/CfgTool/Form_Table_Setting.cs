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
    public partial class Form_Table_Setting : Form
    {
        string GroupName = "";
        SortedList<int, CTableSetting> lst_Copy = new SortedList<int, CTableSetting>();
        const int CST_COLUMN_CURRENTVALUE = 5;
        public Form_Table_Setting(string name)
        {
            InitializeComponent();
            GroupName = name;
        }

        private void Form_Table_Setting_Load(object sender, EventArgs e)
        {
            set_dgv_Prperty();
            init_View();
        }

        void set_dgv_Prperty()
        {
            for (int k = 0; k < dgv_Setting.ColumnCount; k++)
            {
                if (k != CST_COLUMN_CURRENTVALUE)
                {
                    dgv_Setting.Columns[k].DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
                    dgv_Setting.Columns[k].ReadOnly = true;
                }
            }
        }

        void init_View()
        {
            int iPos = 0;
            int iCount = 0;
            dgv_Setting.Rows.Clear();
            foreach (var t in Global.g_Model.lst_Table_Setting)
            {
                if (GroupName == t.Value.GroupName)
                {
                    dgv_Setting.RowCount += 1;
                    iPos = 0;
                    dgv_Setting.Rows[iCount].Cells[iPos++].Value = iCount + 1;
                    dgv_Setting.Rows[iCount].Cells[iPos++].Value = t.Value.Id;
                    dgv_Setting.Rows[iCount].Cells[iPos++].Value = t.Value.GroupName;
                    dgv_Setting.Rows[iCount].Cells[iPos++].Value = t.Value.ItemName;
                    dgv_Setting.Rows[iCount].Cells[iPos++].Value = t.Value.DataType;
                    //----
                    dgv_Setting.Rows[iCount].Cells[iPos++].Value = (float)t.Value.ValueCurrent / (float)t.Value.Ratio;
                    //----
                    dgv_Setting.Rows[iCount].Cells[iPos++].Value = t.Value.Unit;
                    dgv_Setting.Rows[iCount].Cells[iPos++].Value = t.Value.ValueMax;
                    dgv_Setting.Rows[iCount].Cells[iPos++].Value = t.Value.ValueMin;
                    dgv_Setting.Rows[iCount].Cells[iPos++].Value = t.Value.ValueDefault;
                    dgv_Setting.Rows[iCount].Cells[iPos++].Value = t.Value.Ratio;
                    dgv_Setting.Rows[iCount].Cells[iPos++].Value = getHex(t.Value.Addr);
                    dgv_Setting.Rows[iCount].Cells[iPos++].Value = getHex(t.Value.BytePos);
                    //----
                    dgv_Setting.Rows[iCount].Tag = t.Value;
                    //----
                    iCount += 1;
                }
            }
        }

        private void dgv_Setting_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //当前值
            if (e.ColumnIndex == CST_COLUMN_CURRENTVALUE && e.RowIndex >= 0)
            {
                CTableSetting tp = (CTableSetting)dgv_Setting.Rows[e.RowIndex].Tag;
                if (tp == null) { return; }
                float fValue = Convert.ToSingle(dgv_Setting.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                tp.ValueCurrent = Convert.ToInt32(fValue * tp.Ratio);
                tp.strValueCurrent = Convert.ToString(dgv_Setting.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            }
        }

        private string getHex(int addr)
        {
            return string.Format("0x{0:X}", addr);
        }

        private void tsmi_Copy_Click(object sender, EventArgs e)
        {
            lst_Copy.Clear();
            for (int k = 0; k < dgv_Setting.RowCount; k++)
            {
                if (dgv_Setting.Rows[k].Cells[dgv_Setting.ColumnCount - 1].Selected == true)
                {
                    CTableSetting obj = (CTableSetting)dgv_Setting.Rows[k].Tag;
                    lst_Copy.Add(lst_Copy.Count, obj);
                }
            }
            Form_CfgTool.pMainForm.formInfo.LogMessage(string.Format("已复制{0}行定值记录", lst_Copy.Count));
        }

        private void tsmi_Paste_Click(object sender, EventArgs e)
        {
            if (lst_Copy.Count == 0) { return; }
            for (int k = 0; k < dgv_Setting.RowCount; k++)
            {
                if (dgv_Setting.Rows[k].Cells[dgv_Setting.ColumnCount - 1].Selected == true)
                {
                    CTableSetting obj = (CTableSetting)dgv_Setting.Rows[k].Tag;
                    int key = k % lst_Copy.Count;
                    string strinfo = string.Format("将[顺序编号,实际编号]为[{0},{1}]的定值记录的当前值，从{2}修改成{3}",
                        dgv_Setting.Rows[k].Cells[0].Value, obj.Id, obj.strValueCurrent, lst_Copy[key].strValueCurrent);
                    Form_CfgTool.pMainForm.formInfo.LogMessage(strinfo);
                    obj.strValueCurrent = lst_Copy[key].strValueCurrent;
                    dgv_Setting.Rows[k].Cells[dgv_Setting.ColumnCount - 1].Value = obj.strValueCurrent;
                }
            }
        }

        private void dgv_Setting_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && e.ColumnIndex == dgv_Setting.Columns.Count - 1)
            {
                dgv_Setting.ContextMenuStrip = CMS_Copy;
            }
            else
            {
                dgv_Setting.ContextMenuStrip = null;
            }
        }
    }
}
