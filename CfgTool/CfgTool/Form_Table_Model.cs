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
    public partial class Form_Table_Model : Form
    {
        public Form_Table_Model()
        {
            InitializeComponent();
        }

        private void Form_Table_TemplateList_Load(object sender, EventArgs e)
        {
            set_dgv_Property();
            init_View();
        }

        void set_dgv_Property()
        {
            dgv_Model.ReadOnly = true;
            dgv_Model.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
        }

        void init_View()
        {
            dgv_Model.RowCount = Global.g_list_Model.Count;
            int iCount = 0;
            int iPos = 0;
            foreach(var t in Global.g_list_Model)
            {
                iPos = 0;
                dgv_Model.Rows[iCount].Cells[iPos++].Value = iCount + 1;//序号
                dgv_Model.Rows[iCount].Cells[iPos++].Value = t.Value.ModelName;//模板名称
                dgv_Model.Rows[iCount].Cells[iPos++].Value = t.Value.lst_Table_Para.Count;//参数个数
                dgv_Model.Rows[iCount].Cells[iPos++].Value = t.Value.lst_Table_Setting.Count;//定值个数
                dgv_Model.Rows[iCount].Cells[iPos++].Value = t.Value.lst_Table_YC.Count;//遥测个数
                dgv_Model.Rows[iCount].Cells[iPos++].Value = t.Value.lst_Table_SYX.Count;//单点遥信个数
                dgv_Model.Rows[iCount].Cells[iPos++].Value = t.Value.lst_Table_DYX.Count;//双点遥信个数
                dgv_Model.Rows[iCount].Cells[iPos++].Value = t.Value.lst_Table_YK.Count;//遥控个数
                dgv_Model.Rows[iCount].Cells[iPos++].Value = t.Value.lst_Table_Meter.Count;//计量值个数
                dgv_Model.Rows[iCount].Cells[iPos++].Value = t.Value.lst_Table_Port.Count;//端口个数
                iCount += 1;
            }
        }
    }
}
