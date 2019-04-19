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
    public partial class Form_Table_Para : Form
    {
        string GroupName = "";
        SortedList<int, CTablePara> lst_Copy = new SortedList<int, CTablePara>();
        const int CST_COLUMN_CURRENTVALUE = 5;
        public Form_Table_Para(string name)
        {
            InitializeComponent();
            GroupName = name;
        }

        private void Form_Table_Para_Load(object sender, EventArgs e)
        {
            set_dgv_Prperty();
            init_View();
        }

        void set_dgv_Prperty()
        {
            for (int k = 0; k < dgv_Para.ColumnCount;k++ )
            {
                if (k != CST_COLUMN_CURRENTVALUE)
                {
                    dgv_Para.Columns[k].DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
                    dgv_Para.Columns[k].ReadOnly = true;
                }
            }
        }

        void init_View()
        {
            int iPos = 0;
            int iCount = 0;
            dgv_Para.Rows.Clear();
            foreach (var t in Global.g_Model.lst_Table_Para)
            {
                if (GroupName == t.Value.GroupName)
                {
                    dgv_Para.RowCount += 1;
                    iPos = 0;
                    dgv_Para.Rows[iCount].Cells[iPos++].Value = iCount + 1;
                    dgv_Para.Rows[iCount].Cells[iPos++].Value = t.Value.Id;
                    dgv_Para.Rows[iCount].Cells[iPos++].Value = t.Value.GroupName;
                    dgv_Para.Rows[iCount].Cells[iPos++].Value = t.Value.ItemName;
                    dgv_Para.Rows[iCount].Cells[iPos++].Value = t.Value.DataType;
                    //----
                    dgv_Para.Rows[iCount].Cells[iPos++].Value = (float)t.Value.ValueCurrent / (float)t.Value.Ratio;
                    //----
                    dgv_Para.Rows[iCount].Cells[iPos++].Value = t.Value.Unit;
                    dgv_Para.Rows[iCount].Cells[iPos++].Value = t.Value.ValueMax;
                    dgv_Para.Rows[iCount].Cells[iPos++].Value = t.Value.ValueMin;
                    dgv_Para.Rows[iCount].Cells[iPos++].Value = t.Value.ValueDefault;
                    dgv_Para.Rows[iCount].Cells[iPos++].Value = t.Value.Ratio;
                    dgv_Para.Rows[iCount].Cells[iPos++].Value = getHex(t.Value.Addr);
                    dgv_Para.Rows[iCount].Cells[iPos++].Value = getHex(t.Value.BytePos);
                    //----
                    dgv_Para.Rows[iCount].Tag = t.Value;
                    //----
                    iCount += 1;
                }
            }
        }

        private void dgv_Para_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //当前值
            if(e.ColumnIndex == CST_COLUMN_CURRENTVALUE && e.RowIndex >= 0)
            {
                CTablePara tp = (CTablePara)dgv_Para.Rows[e.RowIndex].Tag;
                if (tp == null) { return; }
                float fValue = Convert.ToSingle(dgv_Para.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                tp.ValueCurrent = Convert.ToInt32(fValue * tp.Ratio);
                tp.strValueCurrent = Convert.ToString(dgv_Para.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            }
        }

        private string getHex(int addr)
        {
            return string.Format("0x{0:X}", addr);
        }

        private void tsmi_Copy_Click(object sender, EventArgs e)
        {
            lst_Copy.Clear();
            for (int k = 0; k < dgv_Para.RowCount; k++)
            {
                if(dgv_Para.Rows[k].Cells[CST_COLUMN_CURRENTVALUE].Selected == true)
                {
                    CTablePara obj = (CTablePara)dgv_Para.Rows[k].Tag;
                    lst_Copy.Add(lst_Copy.Count, obj);
                }
            }
            Form_CfgTool.pMainForm.formInfo.LogMessage(string.Format("已复制{0}行参数记录", lst_Copy.Count));
        }

        private void tsmi_Paste_Click(object sender, EventArgs e)
        {
            if (lst_Copy.Count == 0) { return; }
            for (int k = 0; k < dgv_Para.RowCount; k++)
            {
                if (dgv_Para.Rows[k].Cells[CST_COLUMN_CURRENTVALUE].Selected == true)
                {
                    CTablePara obj = (CTablePara)dgv_Para.Rows[k].Tag;
                    int key = k % lst_Copy.Count;
                    string strinfo = string.Format("将[顺序编号,实际编号]为[{0},{1}]的参数记录的当前值，从{2}修改成{3}",
                        dgv_Para.Rows[k].Cells[0].Value, obj.Id, obj.strValueCurrent, lst_Copy[key].strValueCurrent);
                    Form_CfgTool.pMainForm.formInfo.LogMessage(strinfo);
                    obj.strValueCurrent = lst_Copy[key].strValueCurrent;
                    dgv_Para.Rows[k].Cells[CST_COLUMN_CURRENTVALUE].Value = obj.strValueCurrent;
                }
            }
        }

        private void dgv_Para_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && e.ColumnIndex == CST_COLUMN_CURRENTVALUE)
            {
                dgv_Para.ContextMenuStrip = CMS_Copy;
                if(lst_Copy.Count == 1)
                {
                    tsmi_Paste_Accu.Visible = true;
                    tsmi_Paste_Accu.Text = string.Format("累加粘贴(起始值:{0})", lst_Copy[0].strValueCurrent);
                }
                else
                {
                    tsmi_Paste_Accu.Visible = false;
                }
            }
            else
            {
                dgv_Para.ContextMenuStrip = null;
            }
        }

        private void Form_Table_Para_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Alt) && e.KeyCode == Keys.A)
            {
                if(dgv_Para.RowCount == 0) { return; }
                int iRow_Start = 0;
                for (int m = 0; m < dgv_Para.RowCount; m++)
                {
                    if(dgv_Para.Rows[m].Cells[CST_COLUMN_CURRENTVALUE].Selected == true)
                    {
                        dgv_Para.Rows[m].Cells[CST_COLUMN_CURRENTVALUE].Selected = false;
                        iRow_Start = m;
                        break;
                    }
                }
                for (int m = iRow_Start + 1; m < dgv_Para.RowCount; m++)
                {
                    dgv_Para.Rows[m].Cells[CST_COLUMN_CURRENTVALUE].Selected = true;
                }
            }
            else if ((e.Control) && e.KeyCode == Keys.C)//复制
            {
                tsmi_Copy_Click(null, null);
            }
            else if ((e.Control) && e.KeyCode == Keys.V)//粘贴
            {
                tsmi_Paste_Click(null, null);
            }
        }

        private void tsmi_Paste_Accu_Click(object sender, EventArgs e)
        {
            //累加粘贴
            if (lst_Copy.ContainsKey(0) == false) { return; }
            int iStartValue = Convert.ToInt32(lst_Copy[0].strValueCurrent);
            iStartValue += 1;
            for (int m = 0; m < dgv_Para.RowCount; m++)
            {
                if (dgv_Para.Rows[m].Cells[CST_COLUMN_CURRENTVALUE].Selected == true)
                {
                    CTablePara obj = (CTablePara)dgv_Para.Rows[m].Tag;
                    string strinfo = string.Format("将[顺序编号,实际编号]为[{0},{1}]的参数记录的当前值，从{2}修改成{3}",
                        dgv_Para.Rows[m].Cells[0].Value, obj.Id, obj.strValueCurrent, iStartValue);
                    Form_CfgTool.pMainForm.formInfo.LogMessage(strinfo);
                    obj.strValueCurrent = iStartValue.ToString();
                    dgv_Para.Rows[m].Cells[CST_COLUMN_CURRENTVALUE].Value = obj.strValueCurrent;
                    iStartValue += 1;
                }
            }
        }
        //END
    }
}
