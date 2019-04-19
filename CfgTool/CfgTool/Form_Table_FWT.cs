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
    public partial class Form_Table_FWT : Form
    {
        const int cst_Column_FWTName = 1;
        const int cst_Column_I_MAX = 2;
        const int cst_Column_V_MAX = 3;
        const int cst_Column_DC_MAX = 4;
        const int cst_Column_P_MAX = 5;
        const int cst_Column_FR_MAX = 6;
        const int cst_Column_COS_MAX = 7;
        //----
        const int cst_Column_I_COE = 8;
        const int cst_Column_V_COE = 9;
        const int cst_Column_DC_COE = 10;
        const int cst_Column_P_COE = 11;
        const int cst_Column_FR_COE = 12;
        const int cst_Column_COS_COE = 13;
        public Form_Table_FWT()
        {
            InitializeComponent();
        }

        private void Form_Table_FDB_Load(object sender, EventArgs e)
        {
            set_dgv_Property(dgv_FWT);
            init_View();
        }

        void set_dgv_Property(DataGridView dgv)
        {
            dgv.Columns[0].ReadOnly = true;
            dgv.Columns[0].DefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
        }

        private void init_View()
        {
            int iPos = 0;
            int iCount = 0;
            foreach (var t in Global.g_list_FWT.Values)
            {
                dgv_FWT.RowCount += 1;
                iPos = 0;
                dgv_FWT.Rows[iCount].Cells[iPos++].Value = iCount + 1;
                dgv_FWT.Rows[iCount].Cells[iPos++].Value = t.FWTName;
                //----
                dgv_FWT.Rows[iCount].Cells[iPos++].Value = t.I_MAX.ToString();
                dgv_FWT.Rows[iCount].Cells[iPos++].Value = t.V_MAX.ToString();
                dgv_FWT.Rows[iCount].Cells[iPos++].Value = t.DC_MAX.ToString();
                dgv_FWT.Rows[iCount].Cells[iPos++].Value = t.P_MAX.ToString();
                dgv_FWT.Rows[iCount].Cells[iPos++].Value = t.FR_MAX.ToString();
                dgv_FWT.Rows[iCount].Cells[iPos++].Value = t.COS_MAX.ToString();
                //----
                dgv_FWT.Rows[iCount].Cells[iPos++].Value = t.I_COE.ToString();
                dgv_FWT.Rows[iCount].Cells[iPos++].Value = t.V_COE.ToString();
                dgv_FWT.Rows[iCount].Cells[iPos++].Value = t.DC_COE.ToString();
                dgv_FWT.Rows[iCount].Cells[iPos++].Value = t.P_COE.ToString();
                dgv_FWT.Rows[iCount].Cells[iPos++].Value = t.FR_COE.ToString();
                dgv_FWT.Rows[iCount].Cells[iPos++].Value = t.COS_COE.ToString();
                //----
                dgv_FWT.Rows[iCount].Tag = t;
                iCount += 1;
            }
        }

        private void dgv_FDB_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) { return; };
            if (e.ColumnIndex == cst_Column_FWTName)//转发表名称
            {
                CFWT d = (CFWT)dgv_FWT.Rows[e.RowIndex].Tag;
                if (d == null) { return; };
                string FwtName_Origin = d.FWTName;
                string FwtName_New = Convert.ToString(dgv_FWT.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                d.FWTName = FwtName_New;
                Form_CfgTool.pMainForm.refresh_TreeNode_FWTName(d);
                //----
                foreach(var t in Global.g_Model.lst_Table_Port.Values)
                {
                    if(t.FWTName == FwtName_Origin)
                    {
                        t.FWTName = FwtName_New;
                        break;
                    }
                }
            }
            else if (e.ColumnIndex == cst_Column_I_MAX)
            {
                CFWT d = (CFWT)dgv_FWT.Rows[e.RowIndex].Tag;
                if (d == null) { return; };
                int value_origin = d.I_MAX;
                int value_new = Convert.ToInt32(dgv_FWT.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                if (value_origin != value_new) { d.I_MAX = value_new; }
            }
            else if (e.ColumnIndex == cst_Column_V_MAX)
            {
                CFWT d = (CFWT)dgv_FWT.Rows[e.RowIndex].Tag;
                if (d == null) { return; };
                int value_origin = d.V_MAX;
                int value_new = Convert.ToInt32(dgv_FWT.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                if (value_origin != value_new) { d.V_MAX = value_new; }
            }
            else if (e.ColumnIndex == cst_Column_DC_MAX)
            {
                CFWT d = (CFWT)dgv_FWT.Rows[e.RowIndex].Tag;
                if (d == null) { return; };
                int value_origin = d.DC_MAX;
                int value_new = Convert.ToInt32(dgv_FWT.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                if (value_origin != value_new) { d.DC_MAX = value_new; }
            }
            else if (e.ColumnIndex == cst_Column_P_MAX)
            {
                CFWT d = (CFWT)dgv_FWT.Rows[e.RowIndex].Tag;
                if (d == null) { return; };
                int value_origin = d.P_MAX;
                int value_new = Convert.ToInt32(dgv_FWT.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                if (value_origin != value_new) { d.P_MAX = value_new; }
            }
            else if (e.ColumnIndex == cst_Column_FR_MAX)
            {
                CFWT d = (CFWT)dgv_FWT.Rows[e.RowIndex].Tag;
                if (d == null) { return; };
                int value_origin = d.FR_MAX;
                int value_new = Convert.ToInt32(dgv_FWT.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                if (value_origin != value_new) { d.FR_MAX = value_new; }
            }
            else if (e.ColumnIndex == cst_Column_COS_MAX)
            {
                CFWT d = (CFWT)dgv_FWT.Rows[e.RowIndex].Tag;
                if (d == null) { return; };
                int value_origin = d.COS_MAX;
                int value_new = Convert.ToInt32(dgv_FWT.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                if (value_origin != value_new) { d.COS_MAX = value_new; }
            }
            //--
            else if (e.ColumnIndex == cst_Column_I_COE)
            {
                CFWT d = (CFWT)dgv_FWT.Rows[e.RowIndex].Tag;
                if (d == null) { return; };
                int value_origin = d.I_COE;
                int value_new = Convert.ToInt32(dgv_FWT.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                if (value_origin != value_new) { d.I_COE = value_new; }
            }
            else if (e.ColumnIndex == cst_Column_V_COE)
            {
                CFWT d = (CFWT)dgv_FWT.Rows[e.RowIndex].Tag;
                if (d == null) { return; };
                int value_origin = d.V_COE;
                int value_new = Convert.ToInt32(dgv_FWT.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                if (value_origin != value_new) { d.V_COE = value_new; }
            }
            else if (e.ColumnIndex == cst_Column_DC_COE)
            {
                CFWT d = (CFWT)dgv_FWT.Rows[e.RowIndex].Tag;
                if (d == null) { return; };
                int value_origin = d.DC_COE;
                int value_new = Convert.ToInt32(dgv_FWT.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                if (value_origin != value_new) { d.DC_COE = value_new; }
            }
            else if (e.ColumnIndex == cst_Column_P_COE)
            {
                CFWT d = (CFWT)dgv_FWT.Rows[e.RowIndex].Tag;
                if (d == null) { return; };
                int value_origin = d.P_COE;
                int value_new = Convert.ToInt32(dgv_FWT.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                if (value_origin != value_new) { d.P_COE = value_new; }
            }
            else if (e.ColumnIndex == cst_Column_FR_COE)
            {
                CFWT d = (CFWT)dgv_FWT.Rows[e.RowIndex].Tag;
                if (d == null) { return; };
                int value_origin = d.FR_COE;
                int value_new = Convert.ToInt32(dgv_FWT.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                if (value_origin != value_new) { d.FR_COE = value_new; }
            }
            else if (e.ColumnIndex == cst_Column_COS_COE)
            {
                CFWT d = (CFWT)dgv_FWT.Rows[e.RowIndex].Tag;
                if (d == null) { return; };
                int value_origin = d.COS_COE;
                int value_new = Convert.ToInt32(dgv_FWT.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                if (value_origin != value_new) { d.COS_COE = value_new; }
            }
            //----END
        }

        public string strCurrentFwtName = "";
        private void dgv_FWT_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex == -1) { return; }
            if (e.Button == System.Windows.Forms.MouseButtons.Right && e.ColumnIndex == 1)
            {
                strCurrentFwtName = string.Format("{0}", dgv_FWT.Rows[e.RowIndex].Cells[1].Value);
                CMS_FwtCopy.Items.Clear();
                int iCount = 0;
                foreach (var t in Global.g_list_FWT.Values)
                {
                    if (strCurrentFwtName != t.FWTName)
                    {
                        CMS_FwtCopy.Items.Add("复制 " + t.FWTName);
                        this.CMS_FwtCopy.Items[iCount].Click += new EventHandler(CMS_FwtCopy_click);
                        iCount += 1;
                    }

                }
                dgv_FWT.ContextMenuStrip = CMS_FwtCopy;
            }
        }

        CFWT Fwt_1_Source = null;
        CFWT Fwt_2_Destination = null;
        private void CMS_FwtCopy_click(object sender, EventArgs e)
        {
            ToolStripMenuItem Tsm = (sender as ToolStripMenuItem);
            string name = Tsm.Text;
            name = name.Replace("复制", "");
            name = name.Trim();
            //----
            foreach (var t in Global.g_list_FWT)
            {
                if (name == t.Value.FWTName)
                {
                    Fwt_1_Source = t.Value;
                }
            }
            foreach (var t in Global.g_list_FWT)
            {
                if (strCurrentFwtName == t.Value.FWTName)
                {
                    Fwt_2_Destination = t.Value;

                }
            }
            //----
            Form_Table_FWT_CopyFlagSet dlg = new Form_Table_FWT_CopyFlagSet(this);
            dlg.ShowDialog();
        }

        bool bCopy_YC, bCopy_SYX, bCopy_DYX, bCopy_YK, bCopy_Meter;
        public void setCopyFlag(bool byc, bool bsyx, bool bdyx, bool byk, bool bmeter)
        {
            bCopy_YC = byc;
            bCopy_SYX = bsyx;
            bCopy_DYX = bdyx;
            bCopy_YK = byk;
            bCopy_Meter = bmeter;
            //----
            if (Fwt_1_Source != null && Fwt_2_Destination != null)
            {
                fwtCopy(Fwt_1_Source, Fwt_2_Destination);
                Fwt_1_Source = null;
                Fwt_2_Destination = null;
            }
        }

        private void fwtCopy(CFWT t1, CFWT t2)
        {
            if (true == bCopy_YC)
            {
                t2.lst_Table_YC_1.Clear();
                t2.lst_Table_YC_2.Clear();
                foreach (var t in t1.lst_Table_YC_1)
                {
                    CTableYC obj = new CTableYC();
                    obj = t.Value.MyClone();//克隆
                    t2.lst_Table_YC_1.Add(obj.SN, obj);
                }
                foreach (var t in t1.lst_Table_YC_2)
                {
                    CTableYC obj = new CTableYC();
                    obj = t.Value.MyClone();//克隆
                    t2.lst_Table_YC_2.Add(t2.lst_Table_YC_2.Count, obj);
                }
            }
            //-------------------------------------
            if (true == bCopy_SYX)
            {
                t2.lst_Table_SYX_1.Clear();
                t2.lst_Table_SYX_2.Clear();
                foreach (var t in t1.lst_Table_SYX_1)
                {
                    CTableSYX obj = new CTableSYX();
                    obj = t.Value.MyClone();//克隆
                    t2.lst_Table_SYX_1.Add(obj.SN, obj);
                }
                foreach (var t in t1.lst_Table_SYX_2)
                {
                    CTableSYX obj = new CTableSYX();
                    obj = t.Value.MyClone();//克隆
                    t2.lst_Table_SYX_2.Add(t2.lst_Table_SYX_2.Count, obj);
                }
            }
            //-------------------------------------
            if (true == bCopy_DYX)
            {
                t2.lst_Table_DYX_1.Clear();
                t2.lst_Table_DYX_2.Clear();
                foreach (var t in t1.lst_Table_DYX_1)
                {
                    CTableDYX obj = new CTableDYX();
                    obj = t.Value.MyClone();//克隆
                    t2.lst_Table_DYX_1.Add(obj.SN, obj);
                }
                foreach (var t in t1.lst_Table_DYX_2)
                {
                    CTableDYX obj = new CTableDYX();
                    obj = t.Value.MyClone();//克隆
                    t2.lst_Table_DYX_2.Add(t2.lst_Table_DYX_2.Count, obj);
                }
            }
            //-------------------------------------
            if (true == bCopy_YK)
            {
                t2.lst_Table_YK_1.Clear();
                t2.lst_Table_YK_2.Clear();
                foreach (var t in t1.lst_Table_YK_1)
                {
                    CTableYK obj = new CTableYK();
                    obj = t.Value.MyClone();//克隆
                    t2.lst_Table_YK_1.Add(obj.SN, obj);
                }
                foreach (var t in t1.lst_Table_YK_2)
                {
                    CTableYK obj = new CTableYK();
                    obj = t.Value.MyClone();//克隆
                    t2.lst_Table_YK_2.Add(t2.lst_Table_YK_2.Count, obj);
                }
            }
            //-------------------------------------
            if (true == bCopy_Meter)
            {
                t2.lst_Table_Meter_1.Clear();
                t2.lst_Table_Meter_2.Clear();
                foreach (var t in t1.lst_Table_Meter_1)
                {
                    CTableMeter obj = new CTableMeter();
                    obj = t.Value.MyClone();//克隆
                    t2.lst_Table_Meter_1.Add(obj.SN, obj);
                }
                foreach (var t in t1.lst_Table_Meter_2)
                {
                    CTableMeter obj = new CTableMeter();
                    obj = t.Value.MyClone();//克隆
                    t2.lst_Table_Meter_2.Add(t2.lst_Table_Meter_2.Count, obj);
                }
            }
        }
        //----END
    }
}
