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
    public partial class Form_Table_Model_Name : Form
    {
        public string ModelName = "";

        public Form_Table_Model_Name(string name)
        {
            InitializeComponent();
            ModelName = name;
        }

        private void Form_Table_Template_Name_Load(object sender, EventArgs e)
        {
            set_dgv_property();
            foreach (var k in Global.g_list_Model)
            {
                if (ModelName == k.Value.ModelName)
                {
                    init_View_Para(k.Value);
                    init_View_Setting(k.Value);
                    init_View_YC(k.Value);
                    init_View_SYX(k.Value);
                    init_View_DYX(k.Value);
                    init_View_YK(k.Value);
                    init_View_Meter(k.Value);
                    break;
                }
            }
        }

        private void set_dgv_property()
        {
            dgv_Para.ReadOnly = true;
            dgv_Para.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            dgv_Setting.ReadOnly = true;
            dgv_Setting.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            dgv_YC.ReadOnly = true;
            dgv_YC.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            dgv_SYX.ReadOnly = true;
            dgv_SYX.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            dgv_DYX.ReadOnly = true;
            dgv_DYX.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            dgv_YK.ReadOnly = true;
            dgv_YK.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            dgv_Meter.ReadOnly = true;
            dgv_Meter.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
        }

        private void init_View_Para(CModel tpl)
        {
            int iPos = 0;
            int iCount = 0;
            foreach (var t in tpl.lst_Table_Para.Values)
            {
                dgv_Para.RowCount += 1;
                iPos = 0;
                dgv_Para.Rows[iCount].Cells[iPos++].Value = t.Id;
                dgv_Para.Rows[iCount].Cells[iPos++].Value = t.GroupName;
                dgv_Para.Rows[iCount].Cells[iPos++].Value = t.ItemName;
                dgv_Para.Rows[iCount].Cells[iPos++].Value = t.DataType;
                dgv_Para.Rows[iCount].Cells[iPos++].Value = t.Unit;
                dgv_Para.Rows[iCount].Cells[iPos++].Value = t.ValueMax;
                dgv_Para.Rows[iCount].Cells[iPos++].Value = t.ValueMin;
                dgv_Para.Rows[iCount].Cells[iPos++].Value = t.ValueDefault;
                dgv_Para.Rows[iCount].Cells[iPos++].Value = t.Ratio;
                dgv_Para.Rows[iCount].Cells[iPos++].Value = getHex(t.Addr);
                iCount += 1;
            }
        }

        private void init_View_Setting(CModel tpl)
        {
            int iPos = 0;
            int iCount = 0;
            foreach (var t in tpl.lst_Table_Setting.Values)
            {
                dgv_Setting.RowCount += 1;
                iPos = 0;
                dgv_Setting.Rows[iCount].Cells[iPos++].Value = t.Id;
                dgv_Setting.Rows[iCount].Cells[iPos++].Value = t.GroupName;
                dgv_Setting.Rows[iCount].Cells[iPos++].Value = t.ItemName;
                dgv_Setting.Rows[iCount].Cells[iPos++].Value = t.DataType;
                dgv_Setting.Rows[iCount].Cells[iPos++].Value = t.Unit;
                dgv_Setting.Rows[iCount].Cells[iPos++].Value = t.ValueMax;
                dgv_Setting.Rows[iCount].Cells[iPos++].Value = t.ValueMin;
                dgv_Setting.Rows[iCount].Cells[iPos++].Value = t.ValueDefault;
                dgv_Setting.Rows[iCount].Cells[iPos++].Value = t.Ratio;
                dgv_Setting.Rows[iCount].Cells[iPos++].Value = getHex(t.Addr);
                iCount += 1;
            }
        }

        private void init_View_YC(CModel tpl)
        {
            int iPos = 0;
            int iCount = 0;
            foreach (var t in tpl.lst_Table_YC.Values)
            {
                dgv_YC.RowCount += 1;
                iPos = 0;
                dgv_YC.Rows[iCount].Cells[iPos++].Value = t.Id;
                dgv_YC.Rows[iCount].Cells[iPos++].Value = t.GroupName;
                dgv_YC.Rows[iCount].Cells[iPos++].Value = t.ItemName;
                dgv_YC.Rows[iCount].Cells[iPos++].Value = t.DataType;
                dgv_YC.Rows[iCount].Cells[iPos++].Value = t.Unit;
                dgv_YC.Rows[iCount].Cells[iPos++].Value = t.Ratio;
                dgv_YC.Rows[iCount].Cells[iPos++].Value = t.iGroup;
                dgv_YC.Rows[iCount].Cells[iPos++].Value = getHex(t.Addr);
                dgv_YC.Rows[iCount].Cells[iPos++].Value = t.fYcCoe;//新增，jifeng，2018-9-11 15:50
                dgv_YC.Rows[iCount].Cells[iPos++].Value = t.fYcZone;
                iCount += 1;
            }
        }

        private void init_View_SYX(CModel tpl)
        {
            int iPos = 0;
            int iCount = 0;
            foreach (var t in tpl.lst_Table_SYX.Values)
            {
                dgv_SYX.RowCount += 1;
                iPos = 0;
                dgv_SYX.Rows[iCount].Cells[iPos++].Value = t.Id;
                dgv_SYX.Rows[iCount].Cells[iPos++].Value = t.GroupName;
                dgv_SYX.Rows[iCount].Cells[iPos++].Value = t.ItemName;
                dgv_SYX.Rows[iCount].Cells[iPos++].Value = t.DataType;
                dgv_SYX.Rows[iCount].Cells[iPos++].Value = getHex(t.Addr);
                iCount += 1;
            }
        }

        private void init_View_DYX(CModel tpl)
        {
            int iPos = 0;
            int iCount = 0;
            foreach (var t in tpl.lst_Table_DYX.Values)
            {
                dgv_DYX.RowCount += 1;
                iPos = 0;
                dgv_DYX.Rows[iCount].Cells[iPos++].Value = t.Id;
                dgv_DYX.Rows[iCount].Cells[iPos++].Value = t.GroupName;
                dgv_DYX.Rows[iCount].Cells[iPos++].Value = t.ItemName;
                dgv_DYX.Rows[iCount].Cells[iPos++].Value = t.DataType;
                dgv_DYX.Rows[iCount].Cells[iPos++].Value = getHex(t.Addr);
                iCount += 1;
            }
        }

        private void init_View_YK(CModel tpl)
        {
            int iPos = 0;
            int iCount = 0;
            foreach (var t in tpl.lst_Table_YK.Values)
            {
                dgv_YK.RowCount += 1;
                iPos = 0;
                dgv_YK.Rows[iCount].Cells[iPos++].Value = t.Id;
                dgv_YK.Rows[iCount].Cells[iPos++].Value = t.GroupName;
                dgv_YK.Rows[iCount].Cells[iPos++].Value = t.ItemName;
                dgv_YK.Rows[iCount].Cells[iPos++].Value = t.DataType;
                dgv_YK.Rows[iCount].Cells[iPos++].Value = getHex(t.Addr);
                iCount += 1;
            }
        }

        private void init_View_Meter(CModel tpl)
        {
            int iPos = 0;
            int iCount = 0;
            foreach (var t in tpl.lst_Table_Meter.Values)
            {
                dgv_Meter.RowCount += 1;
                iPos = 0;
                dgv_Meter.Rows[iCount].Cells[iPos++].Value = t.Id;
                dgv_Meter.Rows[iCount].Cells[iPos++].Value = t.GroupName;
                dgv_Meter.Rows[iCount].Cells[iPos++].Value = t.ItemName;
                dgv_Meter.Rows[iCount].Cells[iPos++].Value = t.DataType;
                dgv_Meter.Rows[iCount].Cells[iPos++].Value = t.Unit;
                dgv_Meter.Rows[iCount].Cells[iPos++].Value = getHex(t.Addr);
                iCount += 1;
            }
        }

        private string getHex(int addr)
        {
            return string.Format("0x{0:X}", addr);
        }
    }
}
