using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace CfgTool
{
    public enum eType
    {
        EN_TYPE_NONE = 0,
        EN_TYPE_YC,
        EN_TYPE_SYX,
        EN_TYPE_DYX,
        EN_TYPE_YK,
        EN_TYPE_METER
    }

    public partial class Form_Table_FWT_Name : Form
    {
        string strGroupBoxName_1 = "本转发表未包含的实时库中其他条目";
        string strGroupBoxName_2 = "本转发表包含的信息条目";
        CFWT Fwt = null;
        string FwtName = "";
        SortedList<int, CTableYC> lst_Copy = new SortedList<int, CTableYC>();
        SortedList<float, CTableYC> lst_Copy_Yc = new SortedList<float, CTableYC>();

        SortedList<int, CTableSYX> lst_Copy_Syx = new SortedList<int, CTableSYX>();
        SortedList<int, CTableDYX> lst_Copy_Dyx = new SortedList<int, CTableDYX>();
        SortedList<int, CTableYK> lst_Copy_Yk = new SortedList<int, CTableYK>();

        eType enumType;
        public Form_Table_FWT_Name(string fwtname)
        {
            InitializeComponent();
            FwtName = fwtname;
            foreach (var t in Global.g_list_FWT.Values)
            {
                if (t.FWTName == FwtName)
                {
                    Fwt = t;
                    break;
                }
            }
        }

        private void Form_Table_FDB_Name2_Load(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = splitContainer1.Width / 2;
            splitContainer2.SplitterDistance = splitContainer2.Width / 2;
            splitContainer3.SplitterDistance = splitContainer3.Width / 2;
            splitContainer4.SplitterDistance = splitContainer4.Width / 2;
            splitContainer5.SplitterDistance = splitContainer5.Width / 2;
            //----
            init_View_YC();
            init_View_SYX();
            init_View_DYX();
            init_View_YK();
            init_View_Meter();

            init_View_GYX();//添加组合遥信，jifeng，2018-11-1 11:12
        }
        /// <summary>
        /// 遥测
        /// </summary>
        #region "遥测视图"
        void init_View_YC()
        {
            init_View_YC_1();
            init_View_YC_2();
            cancel_dgv_select(dgv_YC_1);
            cancel_dgv_select(dgv_YC_2);
            gb_YC_1.Text = string.Format("{0}[{1}条]", strGroupBoxName_1, dgv_YC_1.RowCount);
            gb_YC_2.Text = string.Format("{0}[{1}条]", strGroupBoxName_2, dgv_YC_2.RowCount);

            if (Global.g_General_FwtSimpleColumn == 1)
            {
                dgv_YC_1.Columns[4].Visible = false;
                dgv_YC_1.Columns[6].Visible = false;
                dgv_YC_1.Columns[7].Visible = false;
                dgv_YC_1.Columns[8].Visible = false;

                dgv_YC_2.Columns[4].Visible = false;
                dgv_YC_2.Columns[6].Visible = false;
                dgv_YC_2.Columns[7].Visible = false;
                dgv_YC_2.Columns[8].Visible = false;
            }
        }
        void init_View_YC_1()
        {
            int iPos = 0;
            int iCount = 0;
            dgv_YC_1.Rows.Clear();
            foreach (var t in Fwt.lst_Table_YC_1)
            {
                if (t.Value.FlagDelete == true) { continue; }
                dgv_YC_1.RowCount += 1;
                iPos = 0;
                dgv_YC_1.Rows[iCount].Cells[iPos++].Value = iCount + 1;//t.Value.SN;//iCount + 1;
                dgv_YC_1.Rows[iCount].Cells[iPos++].Value = t.Value.Id;
                dgv_YC_1.Rows[iCount].Cells[iPos++].Value = t.Value.DeviceName;
                dgv_YC_1.Rows[iCount].Cells[iPos++].Value = t.Value.ModelName;
                dgv_YC_1.Rows[iCount].Cells[iPos++].Value = t.Value.GroupName;
                dgv_YC_1.Rows[iCount].Cells[iPos++].Value = t.Value.ItemName;
                dgv_YC_1.Rows[iCount].Cells[iPos++].Value = t.Value.DataType;
                dgv_YC_1.Rows[iCount].Cells[iPos++].Value = t.Value.Unit;
                dgv_YC_1.Rows[iCount].Cells[iPos++].Value = t.Value.Ratio;
                dgv_YC_1.Rows[iCount].Cells[iPos++].Value = t.Value.iGroup;
                //----南网需求,2017-10-27 12:22
                dgv_YC_1.Rows[iCount].Cells[iPos++].Value = t.Value.fYcZone;
                dgv_YC_1.Rows[iCount].Cells[iPos++].Value = t.Value.fYcCoe;
                //----
                dgv_YC_1.Rows[iCount].Cells[iPos++].Value = getHex(t.Value.Addr);
                dgv_YC_1.Rows[iCount].Tag = t.Value;
                iCount += 1;
            }
        }
        void init_View_YC_2()
        {
            int iPos = 0;
            int iCount = 0;
            dgv_YC_2.Rows.Clear();
            if (Fwt.lst_Table_YC_2.Count == 0) { return; }
            foreach (var t in Fwt.lst_Table_YC_2)
            {
                dgv_YC_2.RowCount += 1;
                iPos = 0;
                dgv_YC_2.Rows[iCount].Cells[iPos++].Value = t.Key + 1;//t.Value.SN;//iCount + 1;
                dgv_YC_2.Rows[iCount].Cells[iPos++].Value = t.Value.Id;
                dgv_YC_2.Rows[iCount].Cells[iPos++].Value = t.Value.DeviceName;
                dgv_YC_2.Rows[iCount].Cells[iPos++].Value = t.Value.ModelName;
                dgv_YC_2.Rows[iCount].Cells[iPos++].Value = t.Value.GroupName;
                dgv_YC_2.Rows[iCount].Cells[iPos++].Value = t.Value.ItemName;
                dgv_YC_2.Rows[iCount].Cells[iPos++].Value = t.Value.DataType;
                dgv_YC_2.Rows[iCount].Cells[iPos++].Value = t.Value.Unit;
                dgv_YC_2.Rows[iCount].Cells[iPos++].Value = t.Value.Ratio;
                dgv_YC_2.Rows[iCount].Cells[iPos++].Value = t.Value.iGroup;
                //----南网需求,2017-10-27 12:22
                dgv_YC_2.Rows[iCount].Cells[iPos++].Value = t.Value.fYcZone;
                dgv_YC_2.Rows[iCount].Cells[iPos++].Value = t.Value.fYcCoe;
                //----
                dgv_YC_2.Rows[iCount].Cells[dgv_YC_2.ColumnCount - 4].Style.BackColor = Color.White;
                dgv_YC_2.Rows[iCount].Cells[dgv_YC_2.ColumnCount - 3].Style.BackColor = Color.White;
                dgv_YC_2.Rows[iCount].Cells[dgv_YC_2.ColumnCount - 2].Style.BackColor = Color.White;
                dgv_YC_2.Rows[iCount].Cells[dgv_YC_2.ColumnCount - 1].Style.BackColor = Color.White;
                dgv_YC_2.Rows[iCount].Cells[iPos++].Value = getHex(t.Value.Addr);
                dgv_YC_2.Rows[iCount].Tag = t.Value;
                dgv_YC_2.Rows[iCount].Cells[0].Tag = t.Key;//change
                iCount += 1;
            }
            //----dgv属性设置
            for (int m = 0; m < dgv_YC_2.RowCount; m++)
            {
                for (int k = 0; k < dgv_YC_2.ColumnCount; k++)
                {
                    dgv_YC_2.Rows[m].Cells[k].ReadOnly = true;
                }
            }
            if (dgv_YC_2.RowCount > 0)
            {
                //dgv_YC_2.Rows[0].Cells[dgv_YC_2.ColumnCount - 1].ReadOnly = false;
                //dgv_YC_2.Rows[0].Cells[dgv_YC_2.ColumnCount - 1].Style.BackColor = Color.White;
                dgv_YC_2.Columns[dgv_YC_2.ColumnCount - 4].ReadOnly = false;
                dgv_YC_2.Columns[dgv_YC_2.ColumnCount - 3].ReadOnly = false;
                dgv_YC_2.Columns[dgv_YC_2.ColumnCount - 2].ReadOnly = false;
                dgv_YC_2.Columns[dgv_YC_2.ColumnCount - 1].ReadOnly = false;
            }
        }
        #endregion

        #region "遥测1→←遥测2"
        #region "遥测1→遥测2"
        private void dgv_YC_1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                List<string> a = new List<string>();
                for (int i = 0; i < dgv_YC_1.RowCount; i++)
                {
                    if (dgv_YC_1.Rows[i].Selected)
                    {
                        CTableYC t = (CTableYC)dgv_YC_1.Rows[i].Tag;
                        a.Add(t.SN.ToString());
                    }
                }
                this.dgv_YC_1.DoDragDrop(a, DragDropEffects.Move);
            }
        }
        private void dgv_YC_2_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        private void dgv_YC_2_DragDrop(object sender, DragEventArgs e)
        {
            Point p = this.dgv_YC_2.PointToClient(new Point(e.X, e.Y));
            DataGridView.HitTestInfo hit = this.dgv_YC_2.HitTest(p.X, p.Y);

            List<string> obj = (List<string>)e.Data.GetData(typeof(List<string>));
            if (obj.Count == 0) { return; }
            List<int> a = new List<int>();
            //----
            SortedList<int, CTableYC> lst_Temp = new SortedList<int, CTableYC>();//遥控表临时
            if (-1 == hit.RowIndex)
            {
                for (int k = 0; k < Fwt.lst_Table_YC_2.Count; k++)
                {
                    CTableYC t = Fwt.lst_Table_YC_2[k].MyClone();
                    lst_Temp.Add(lst_Temp.Count, t);
                }
                for (int i = 0; i < obj.Count; i++)
                {
                    int key = Convert.ToInt32(obj[i]);
                    CTableYC t = Fwt.lst_Table_YC_1[key].MyClone();
                    lst_Temp.Add(lst_Temp.Count, t);
                    if (Global.g_General_MultiYc == 0)
                    {
                        Fwt.lst_Table_YC_1[key].FlagDelete = true;
                    }
                    else
                    {
                        Fwt.lst_Table_YC_1[key].FlagDelete = false;
                    }
                    //----
                    a.Add(lst_Temp.Count - 1);
                }
            }
            else
            {
                for (int k = 0; k <= hit.RowIndex; k++)
                {
                    CTableYC t = Fwt.lst_Table_YC_2[k].MyClone();
                    lst_Temp.Add(lst_Temp.Count, t);
                }
                for (int i = 0; i < obj.Count; i++)
                {
                    int key = Convert.ToInt32(obj[i]);
                    CTableYC t = Fwt.lst_Table_YC_1[key].MyClone();
                    lst_Temp.Add(lst_Temp.Count, t);
                    if (Global.g_General_MultiYc == 0)
                    {
                        Fwt.lst_Table_YC_1[key].FlagDelete = true;
                    }
                    else
                    {
                        Fwt.lst_Table_YC_1[key].FlagDelete = false;
                    }
                    //----
                    a.Add(lst_Temp.Count - 1);
                }
                for (int k = (hit.RowIndex + 1); k < Fwt.lst_Table_YC_2.Count; k++)
                {
                    CTableYC t = Fwt.lst_Table_YC_2[k].MyClone();
                    lst_Temp.Add(lst_Temp.Count, t);
                }
            }

            Fwt.lst_Table_YC_2.Clear();
            for (int k = 0; k < lst_Temp.Count; k++)
            {
                CTableYC t = lst_Temp[k].MyClone();
                Fwt.lst_Table_YC_2.Add(Fwt.lst_Table_YC_2.Count, t);
            }
            //----
            //for (int i = 0; i < obj.Count; i++)
            //{
            //    int key = Convert.ToInt32(obj[i]);
            //    CTableYC t = Fwt.lst_Table_YC_1[key].MyClone();
            //    //Fwt.lst_Table_YC_2.Add(key, t);
            //    Fwt.lst_Table_YC_2.Add(Fwt.lst_Table_YC_2.Count, t);
            //    Fwt.lst_Table_YC_1[key].FlagDelete = true;
            //}
            triggerAddr_YC(true);
            //----
            for (int k = 0; k < a.Count; k++)
            {
                dgv_YC_2.Rows[a[k]].Selected = true;
            }
            enumType = eType.EN_TYPE_YC;
            timer2000.Start();
            //----
        }
        #endregion

        #region "遥测1←遥测2"
        private void tsmi_YC_Remove_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> a = new List<string>();
                for (int i = 0; i < dgv_YC_2.RowCount; i++)
                {
                    if (dgv_YC_2.Rows[i].Selected)
                    {
                        int key = Convert.ToInt32(dgv_YC_2.Rows[i].Cells[0].Tag);//change
                        //a.Add(i.ToString());//bug,jifeng,201709112016
                        a.Add(key.ToString());
                    }
                }
                if (a.Count == 0) { return; }
                for (int i = 0; i < a.Count; i++)
                {
                    int key = Convert.ToInt32(a[i]);
                    CTableYC t = Fwt.lst_Table_YC_2[key].MyClone();
                    Fwt.lst_Table_YC_1[t.SN].FlagDelete = false;//change
                    Fwt.lst_Table_YC_2.Remove(key);
                }
                //----list2排序，jifeng，2018-5-22 15:40
                SortedList<int, CTableYC> lst_Temp = new SortedList<int, CTableYC>();//临时表
                foreach (var obj in Fwt.lst_Table_YC_2.Values)
                {
                    CTableYC t = obj.MyClone();
                    lst_Temp.Add(lst_Temp.Count, t);
                }
                Fwt.lst_Table_YC_2.Clear();
                foreach (var obj in lst_Temp.Values)
                {
                    CTableYC t = obj.MyClone();
                    Fwt.lst_Table_YC_2.Add(Fwt.lst_Table_YC_2.Count, t);
                }
                //----
                triggerAddr_YC(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("YC2 remove err: " + ex.ToString());
            }
        }
        #endregion
        #endregion

        /// <summary>
        /// 单点遥信
        /// </summary>
        #region "单点遥信视图"
        void init_View_SYX()
        {
            init_View_SYX_1();
            init_View_SYX_2();
            cancel_dgv_select(dgv_SYX_1);
            cancel_dgv_select(dgv_SYX_2);
            gb_SYX_1.Text = string.Format("{0}[{1}条]", strGroupBoxName_1, dgv_SYX_1.RowCount);
            gb_SYX_2.Text = string.Format("{0}[{1}条]", strGroupBoxName_2, dgv_SYX_2.RowCount);

            if (Global.g_General_FwtSimpleColumn == 1)
            {
                dgv_SYX_1.Columns[4].Visible = false;
                dgv_SYX_1.Columns[6].Visible = false;

                dgv_SYX_2.Columns[4].Visible = false;
                dgv_SYX_2.Columns[6].Visible = false;
            }
        }
        void init_View_SYX_1()
        {
            int iPos = 0;
            int iCount = 0;
            dgv_SYX_1.Rows.Clear();
            foreach (var t in Fwt.lst_Table_SYX_1)
            {
                if (t.Value.FlagDelete == true) { continue; }
                dgv_SYX_1.RowCount += 1;
                iPos = 0;
                dgv_SYX_1.Rows[iCount].Cells[iPos++].Value = iCount + 1;//t.Key;// iCount + 1;
                dgv_SYX_1.Rows[iCount].Cells[iPos++].Value = t.Value.Id;
                dgv_SYX_1.Rows[iCount].Cells[iPos++].Value = t.Value.DeviceName;
                dgv_SYX_1.Rows[iCount].Cells[iPos++].Value = t.Value.ModelName;
                dgv_SYX_1.Rows[iCount].Cells[iPos++].Value = t.Value.GroupName;
                dgv_SYX_1.Rows[iCount].Cells[iPos++].Value = t.Value.ItemName;
                dgv_SYX_1.Rows[iCount].Cells[iPos++].Value = t.Value.DataType;
                dgv_SYX_1.Rows[iCount].Cells[iPos++].Value = getHex(t.Value.Addr);
                dgv_SYX_1.Rows[iCount].Tag = t.Value;
                iCount += 1;
            }
        }
        void init_View_SYX_2()
        {
            int iPos = 0;
            int iCount = 0;
            dgv_SYX_2.Rows.Clear();
            if (Fwt.lst_Table_SYX_2.Count == 0) { return; }
            foreach (var t in Fwt.lst_Table_SYX_2)
            {
                dgv_SYX_2.RowCount += 1;
                iPos = 0;
                dgv_SYX_2.Rows[iCount].Cells[iPos++].Value = t.Key + 1;// iCount + 1;
                dgv_SYX_2.Rows[iCount].Cells[iPos++].Value = t.Value.Id;
                dgv_SYX_2.Rows[iCount].Cells[iPos++].Value = t.Value.DeviceName;
                dgv_SYX_2.Rows[iCount].Cells[iPos++].Value = t.Value.ModelName;
                dgv_SYX_2.Rows[iCount].Cells[iPos++].Value = t.Value.GroupName;
                dgv_SYX_2.Rows[iCount].Cells[iPos++].Value = t.Value.ItemName;
                dgv_SYX_2.Rows[iCount].Cells[iPos++].Value = t.Value.DataType;
                dgv_SYX_2.Rows[iCount].Cells[iPos++].Value = getHex(t.Value.Addr);
                dgv_SYX_2.Rows[iCount].Tag = t.Value;
                dgv_SYX_2.Rows[iCount].Cells[0].Tag = t.Key;//change
                iCount += 1;
            }
            //----dgv属性设置
            for (int m = 0; m < dgv_SYX_2.RowCount; m++)
            {
                for (int k = 0; k < dgv_SYX_2.ColumnCount; k++)
                {
                    if (k == (dgv_SYX_2.ColumnCount - 1))
                    {
                        dgv_SYX_2.Rows[m].Cells[dgv_SYX_2.ColumnCount - 1].ReadOnly = false;
                        dgv_SYX_2.Rows[m].Cells[dgv_SYX_2.ColumnCount - 1].Style.BackColor = Color.White;
                    }
                    else
                    {
                        dgv_SYX_2.Rows[m].Cells[k].ReadOnly = true;
                    }
                }
            }
        }
        #endregion

        #region "单点遥信1→←单点遥信2"
        #region "单点遥信1→单点遥信2"
        private void dgv_SYX_1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                List<string> a = new List<string>();
                for (int i = 0; i < dgv_SYX_1.RowCount; i++)
                {
                    if (dgv_SYX_1.Rows[i].Selected)
                    {
                        CTableSYX t = (CTableSYX)dgv_SYX_1.Rows[i].Tag;
                        a.Add(t.SN.ToString());
                    }
                }
                this.dgv_SYX_1.DoDragDrop(a, DragDropEffects.Move);
            }
        }

        private void dgv_SYX_2_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void dgv_SYX_2_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                Point p = this.dgv_SYX_2.PointToClient(new Point(e.X, e.Y));
                DataGridView.HitTestInfo hit = this.dgv_SYX_2.HitTest(p.X, p.Y);

                List<string> obj = (List<string>)e.Data.GetData(typeof(List<string>));
                if (obj.Count == 0) { return; }
                List<int> a = new List<int>();
                //----
                SortedList<int, CTableSYX> lst_Temp = new SortedList<int, CTableSYX>();//遥控表临时
                if (-1 == hit.RowIndex)
                {
                    for (int k = 0; k < Fwt.lst_Table_SYX_2.Count; k++)
                    {
                        CTableSYX t = Fwt.lst_Table_SYX_2[k].MyClone();
                        lst_Temp.Add(lst_Temp.Count, t);
                    }
                    for (int i = 0; i < obj.Count; i++)
                    {
                        int key = Convert.ToInt32(obj[i]);
                        CTableSYX t = Fwt.lst_Table_SYX_1[key].MyClone();
                        lst_Temp.Add(lst_Temp.Count, t);
                        if (Global.g_General_MultiYx == 0)
                        {
                            Fwt.lst_Table_SYX_1[key].FlagDelete = true;
                        }
                        else
                        {
                            Fwt.lst_Table_SYX_1[key].FlagDelete = false;
                        }
                        a.Add(lst_Temp.Count - 1);
                    }
                }
                else
                {
                    for (int k = 0; k <= hit.RowIndex; k++)
                    {
                        CTableSYX t = Fwt.lst_Table_SYX_2[k].MyClone();
                        lst_Temp.Add(lst_Temp.Count, t);
                    }
                    for (int i = 0; i < obj.Count; i++)
                    {
                        int key = Convert.ToInt32(obj[i]);
                        CTableSYX t = Fwt.lst_Table_SYX_1[key].MyClone();
                        lst_Temp.Add(lst_Temp.Count, t);
                        if (Global.g_General_MultiYx == 0)
                        {
                            Fwt.lst_Table_SYX_1[key].FlagDelete = true;
                        }
                        else
                        {
                            Fwt.lst_Table_SYX_1[key].FlagDelete = false;
                        }
                        a.Add(lst_Temp.Count - 1);
                    }
                    for (int k = (hit.RowIndex + 1); k < Fwt.lst_Table_SYX_2.Count; k++)
                    {
                        CTableSYX t = Fwt.lst_Table_SYX_2[k].MyClone();
                        lst_Temp.Add(lst_Temp.Count, t);
                    }
                }

                Fwt.lst_Table_SYX_2.Clear();
                for (int k = 0; k < lst_Temp.Count; k++)
                {
                    CTableSYX t = lst_Temp[k].MyClone();
                    Fwt.lst_Table_SYX_2.Add(Fwt.lst_Table_SYX_2.Count, t);
                }
                //----
                //for (int i = 0; i < obj.Count; i++)
                //{
                //    int key = Convert.ToInt32(obj[i]);
                //    CTableSYX t = Fwt.lst_Table_SYX_1[key].MyClone();
                //    //Fwt.lst_Table_SYX_2.Add(key, t);
                //    Fwt.lst_Table_SYX_2.Add(Fwt.lst_Table_SYX_2.Count, t);
                //    Fwt.lst_Table_SYX_1[key].FlagDelete = true;
                //}
                triggerAddr_SYX(true);
                //----
                for (int k = 0; k < a.Count; k++)
                {
                    dgv_SYX_2.Rows[a[k]].Selected = true;
                }
                enumType = eType.EN_TYPE_SYX;
                timer2000.Start();
                //----
            }
            catch (Exception ex)
            {
                MessageBox.Show("SYX1 drag drop err: " + ex.ToString());
            }
        }
        #endregion

        #region "单点遥信1←单点遥信2"
        private void tsmi_SYX_Remove_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> a = new List<string>();
                for (int i = 0; i < dgv_SYX_2.RowCount; i++)
                {
                    if (dgv_SYX_2.Rows[i].Selected)
                    {
                        int key = Convert.ToInt32(dgv_SYX_2.Rows[i].Cells[0].Tag);//change
                        a.Add(key.ToString());
                    }
                }
                if (a.Count == 0) { return; }
                for (int i = 0; i < a.Count; i++)
                {
                    int key = Convert.ToInt32(a[i]);
                    CTableSYX t = Fwt.lst_Table_SYX_2[key].MyClone();
                    Fwt.lst_Table_SYX_1[t.SN].FlagDelete = false;//change
                    Fwt.lst_Table_SYX_2.Remove(key);
                }
                //----list2排序，jifeng，2018-5-22 15:40
                SortedList<int, CTableSYX> lst_Temp = new SortedList<int, CTableSYX>();//临时表
                foreach (var obj in Fwt.lst_Table_SYX_2.Values)
                {
                    CTableSYX t = obj.MyClone();
                    lst_Temp.Add(lst_Temp.Count, t);
                }
                Fwt.lst_Table_SYX_2.Clear();
                foreach (var obj in lst_Temp.Values)
                {
                    CTableSYX t = obj.MyClone();
                    Fwt.lst_Table_SYX_2.Add(Fwt.lst_Table_SYX_2.Count, t);
                }
                //----
                triggerAddr_SYX(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("SYX2 remove err: " + ex.ToString());
            }
        }
        #endregion
        #endregion

        /// <summary>
        /// 双点遥信
        /// </summary>
        #region "双点遥信视图"
        void init_View_DYX()
        {
            init_View_DYX_1();
            init_View_DYX_2();
            cancel_dgv_select(dgv_DYX_1);
            cancel_dgv_select(dgv_DYX_2);
            gb_DYX_1.Text = string.Format("{0}[{1}条]", strGroupBoxName_1, dgv_DYX_1.RowCount);
            gb_DYX_2.Text = string.Format("{0}[{1}条]", strGroupBoxName_2, dgv_DYX_2.RowCount);

            if (Global.g_General_FwtSimpleColumn == 1)
            {
                dgv_DYX_1.Columns[4].Visible = false;
                dgv_DYX_1.Columns[6].Visible = false;

                dgv_DYX_2.Columns[4].Visible = false;
                dgv_DYX_2.Columns[6].Visible = false;
            }
        }
        void init_View_DYX_1()
        {
            int iPos = 0;
            int iCount = 0;
            dgv_DYX_1.Rows.Clear();
            foreach (var t in Fwt.lst_Table_DYX_1)
            {
                if (t.Value.FlagDelete == true) { continue; }
                dgv_DYX_1.RowCount += 1;
                iPos = 0;
                dgv_DYX_1.Rows[iCount].Cells[iPos++].Value = iCount + 1;//t.Key;// iCount + 1;
                dgv_DYX_1.Rows[iCount].Cells[iPos++].Value = t.Value.Id;
                dgv_DYX_1.Rows[iCount].Cells[iPos++].Value = t.Value.DeviceName;
                dgv_DYX_1.Rows[iCount].Cells[iPos++].Value = t.Value.ModelName;
                dgv_DYX_1.Rows[iCount].Cells[iPos++].Value = t.Value.GroupName;
                dgv_DYX_1.Rows[iCount].Cells[iPos++].Value = t.Value.ItemName;
                dgv_DYX_1.Rows[iCount].Cells[iPos++].Value = t.Value.DataType;
                dgv_DYX_1.Rows[iCount].Cells[iPos++].Value = getHex(t.Value.Addr);
                dgv_DYX_1.Rows[iCount].Tag = t.Value;
                iCount += 1;
            }
        }
        void init_View_DYX_2()
        {
            int iPos = 0;
            int iCount = 0;
            dgv_DYX_2.Rows.Clear();
            if (Fwt.lst_Table_DYX_2.Count == 0) { return; }
            foreach (var t in Fwt.lst_Table_DYX_2)
            {
                dgv_DYX_2.RowCount += 1;
                iPos = 0;
                dgv_DYX_2.Rows[iCount].Cells[iPos++].Value = t.Key + 1;// iCount + 1;
                dgv_DYX_2.Rows[iCount].Cells[iPos++].Value = t.Value.Id;
                dgv_DYX_2.Rows[iCount].Cells[iPos++].Value = t.Value.DeviceName;
                dgv_DYX_2.Rows[iCount].Cells[iPos++].Value = t.Value.ModelName;
                dgv_DYX_2.Rows[iCount].Cells[iPos++].Value = t.Value.GroupName;
                dgv_DYX_2.Rows[iCount].Cells[iPos++].Value = t.Value.ItemName;
                dgv_DYX_2.Rows[iCount].Cells[iPos++].Value = t.Value.DataType;
                dgv_DYX_2.Rows[iCount].Cells[iPos++].Value = getHex(t.Value.Addr);
                dgv_DYX_2.Rows[iCount].Tag = t.Value;
                dgv_DYX_2.Rows[iCount].Cells[0].Tag = t.Key;//change
                iCount += 1;
            }
            //----dgv属性设置
            for (int m = 0; m < dgv_DYX_2.RowCount; m++)
            {
                for (int k = 0; k < dgv_DYX_2.ColumnCount; k++)
                {
                    if (k == (dgv_DYX_2.ColumnCount - 1))
                    {
                        dgv_DYX_2.Rows[m].Cells[dgv_DYX_2.ColumnCount - 1].ReadOnly = false;
                        dgv_DYX_2.Rows[m].Cells[dgv_DYX_2.ColumnCount - 1].Style.BackColor = Color.White;
                    }
                    else
                    {
                        dgv_DYX_2.Rows[m].Cells[k].ReadOnly = true;
                    }
                }
            }
        }
        #endregion

        #region "双点遥信1→←双点遥信2"
        #region "双点遥信1→双点遥信2"
        private void dgv_DYX_1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                List<string> a = new List<string>();
                for (int i = 0; i < dgv_DYX_1.RowCount; i++)
                {
                    if (dgv_DYX_1.Rows[i].Selected)
                    {
                        CTableDYX t = (CTableDYX)dgv_DYX_1.Rows[i].Tag;
                        a.Add(t.SN.ToString());
                    }
                }
                this.dgv_DYX_1.DoDragDrop(a, DragDropEffects.Move);
            }
        }

        private void dgv_DYX_2_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void dgv_DYX_2_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                Point p = this.dgv_DYX_2.PointToClient(new Point(e.X, e.Y));
                DataGridView.HitTestInfo hit = this.dgv_DYX_2.HitTest(p.X, p.Y);

                List<string> obj = (List<string>)e.Data.GetData(typeof(List<string>));
                if (obj.Count == 0) { return; }
                List<int> a = new List<int>();
                //----
                SortedList<int, CTableDYX> lst_Temp = new SortedList<int, CTableDYX>();//遥控表临时
                if (-1 == hit.RowIndex)
                {
                    for (int k = 0; k < Fwt.lst_Table_DYX_2.Count; k++)
                    {
                        CTableDYX t = Fwt.lst_Table_DYX_2[k].MyClone();
                        lst_Temp.Add(lst_Temp.Count, t);
                    }
                    for (int i = 0; i < obj.Count; i++)
                    {
                        int key = Convert.ToInt32(obj[i]);
                        CTableDYX t = Fwt.lst_Table_DYX_1[key].MyClone();
                        lst_Temp.Add(lst_Temp.Count, t);
                        Fwt.lst_Table_DYX_1[key].FlagDelete = true;
                        //----
                        a.Add(lst_Temp.Count - 1);
                    }
                }
                else
                {
                    for (int k = 0; k <= hit.RowIndex; k++)
                    {
                        CTableDYX t = Fwt.lst_Table_DYX_2[k].MyClone();
                        lst_Temp.Add(lst_Temp.Count, t);
                    }
                    for (int i = 0; i < obj.Count; i++)
                    {
                        int key = Convert.ToInt32(obj[i]);
                        CTableDYX t = Fwt.lst_Table_DYX_1[key].MyClone();
                        lst_Temp.Add(lst_Temp.Count, t);
                        Fwt.lst_Table_DYX_1[key].FlagDelete = true;
                        //----
                        a.Add(lst_Temp.Count - 1);
                    }
                    for (int k = (hit.RowIndex + 1); k < Fwt.lst_Table_DYX_2.Count; k++)
                    {
                        CTableDYX t = Fwt.lst_Table_DYX_2[k].MyClone();
                        lst_Temp.Add(lst_Temp.Count, t);
                    }
                }

                Fwt.lst_Table_DYX_2.Clear();
                for (int k = 0; k < lst_Temp.Count; k++)
                {
                    CTableDYX t = lst_Temp[k].MyClone();
                    Fwt.lst_Table_DYX_2.Add(Fwt.lst_Table_DYX_2.Count, t);
                }
                //----
                //for (int i = 0; i < obj.Count; i++)
                //{
                //    int key = Convert.ToInt32(obj[i]);
                //    CTableDYX t = Fwt.lst_Table_DYX_1[key].MyClone();
                //    //Fwt.lst_Table_DYX_2.Add(key, t);
                //    Fwt.lst_Table_DYX_2.Add(Fwt.lst_Table_DYX_2.Count, t);
                //    Fwt.lst_Table_DYX_1[key].FlagDelete = true;
                //}
                triggerAddr_DYX(true);
                //----
                for (int k = 0; k < a.Count; k++)
                {
                    dgv_DYX_2.Rows[a[k]].Selected = true;
                }
                enumType = eType.EN_TYPE_DYX;
                timer2000.Start();
                //----
            }
            catch (Exception ex)
            {
                MessageBox.Show("DYX1 drag drop err: " + ex.ToString());
            }
        }
        #endregion

        #region "双点遥信1←双点遥信2"
        private void tsmi_DYX_Remove_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> a = new List<string>();
                for (int i = 0; i < dgv_DYX_2.RowCount; i++)
                {
                    if (dgv_DYX_2.Rows[i].Selected)
                    {
                        int key = Convert.ToInt32(dgv_DYX_2.Rows[i].Cells[0].Tag);//change
                        a.Add(key.ToString());
                    }
                }
                if (a.Count == 0) { return; }
                for (int i = 0; i < a.Count; i++)
                {
                    int key = Convert.ToInt32(a[i]);
                    CTableDYX t = Fwt.lst_Table_DYX_2[key].MyClone();
                    Fwt.lst_Table_DYX_1[t.SN].FlagDelete = false;//change
                    Fwt.lst_Table_DYX_2.Remove(key);
                }
                //----list2排序，jifeng，2018-5-22 15:40
                SortedList<int, CTableDYX> lst_Temp = new SortedList<int, CTableDYX>();//临时表
                foreach (var obj in Fwt.lst_Table_DYX_2.Values)
                {
                    CTableDYX t = obj.MyClone();
                    lst_Temp.Add(lst_Temp.Count, t);
                }
                Fwt.lst_Table_DYX_2.Clear();
                foreach (var obj in lst_Temp.Values)
                {
                    CTableDYX t = obj.MyClone();
                    Fwt.lst_Table_DYX_2.Add(Fwt.lst_Table_DYX_2.Count, t);
                }
                //----
                triggerAddr_DYX(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("DYX2 remove err: " + ex.ToString());
            }
        }
        #endregion
        #endregion
        /// <summary>
        /// 遥控
        /// </summary>
        #region "遥控视图"
        void init_View_YK()
        {
            init_View_YK_1();
            init_View_YK_2();
            cancel_dgv_select(dgv_YK_1);
            cancel_dgv_select(dgv_YK_2);
            gb_YK_1.Text = string.Format("{0}[{1}条]", strGroupBoxName_1, dgv_YK_1.RowCount);
            gb_YK_2.Text = string.Format("{0}[{1}条]", strGroupBoxName_2, dgv_YK_2.RowCount);

            if (Global.g_General_FwtSimpleColumn == 1)
            {
                dgv_YK_1.Columns[4].Visible = false;
                dgv_YK_1.Columns[6].Visible = false;

                dgv_YK_2.Columns[4].Visible = false;
                dgv_YK_2.Columns[6].Visible = false;
            }
        }
        void init_View_YK_1()
        {
            int iPos = 0;
            int iCount = 0;
            dgv_YK_1.Rows.Clear();
            foreach (var t in Fwt.lst_Table_YK_1)
            {
                if (t.Value.FlagDelete == true) { continue; }
                dgv_YK_1.RowCount += 1;
                iPos = 0;
                dgv_YK_1.Rows[iCount].Cells[iPos++].Value = iCount + 1;//t.Key;// iCount + 1;
                dgv_YK_1.Rows[iCount].Cells[iPos++].Value = t.Value.Id;
                dgv_YK_1.Rows[iCount].Cells[iPos++].Value = t.Value.DeviceName;
                dgv_YK_1.Rows[iCount].Cells[iPos++].Value = t.Value.ModelName;
                dgv_YK_1.Rows[iCount].Cells[iPos++].Value = t.Value.GroupName;
                dgv_YK_1.Rows[iCount].Cells[iPos++].Value = t.Value.ItemName;
                dgv_YK_1.Rows[iCount].Cells[iPos++].Value = t.Value.DataType;
                dgv_YK_1.Rows[iCount].Cells[iPos++].Value = getHex(t.Value.Addr);
                dgv_YK_1.Rows[iCount].Tag = t.Value;
                iCount += 1;
            }
        }
        void init_View_YK_2()
        {
            int iPos = 0;
            int iCount = 0;
            dgv_YK_2.Rows.Clear();
            if (Fwt.lst_Table_YK_2.Count == 0) { return; }
            foreach (var t in Fwt.lst_Table_YK_2)
            {
                dgv_YK_2.RowCount += 1;
                iPos = 0;
                dgv_YK_2.Rows[iCount].Cells[iPos++].Value = t.Key + 1;// iCount + 1;
                dgv_YK_2.Rows[iCount].Cells[iPos++].Value = t.Value.Id;
                dgv_YK_2.Rows[iCount].Cells[iPos++].Value = t.Value.DeviceName;
                dgv_YK_2.Rows[iCount].Cells[iPos++].Value = t.Value.ModelName;
                dgv_YK_2.Rows[iCount].Cells[iPos++].Value = t.Value.GroupName;
                dgv_YK_2.Rows[iCount].Cells[iPos++].Value = t.Value.ItemName;
                dgv_YK_2.Rows[iCount].Cells[iPos++].Value = t.Value.DataType;
                dgv_YK_2.Rows[iCount].Cells[iPos++].Value = getHex(t.Value.Addr);
                dgv_YK_2.Rows[iCount].Tag = t.Value;
                dgv_YK_2.Rows[iCount].Cells[0].Tag = t.Key;//change
                iCount += 1;
            }
            //----dgv属性设置
            for (int m = 0; m < dgv_YK_2.RowCount; m++)
            {
                for (int k = 0; k < dgv_YK_2.ColumnCount; k++)
                {
                    if (k == (dgv_YK_2.ColumnCount - 1))
                    {
                        dgv_YK_2.Rows[m].Cells[dgv_YK_2.ColumnCount - 1].ReadOnly = false;
                        dgv_YK_2.Rows[m].Cells[dgv_YK_2.ColumnCount - 1].Style.BackColor = Color.White;
                    }
                    else
                    {
                        dgv_YK_2.Rows[m].Cells[k].ReadOnly = true;
                    }

                }
            }
        }
        #endregion

        #region "遥控1→←遥控2"
        #region "遥控1→遥控2"
        private void dgv_YK_1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                List<string> a = new List<string>();
                for (int i = 0; i < dgv_YK_1.RowCount; i++)
                {
                    if (dgv_YK_1.Rows[i].Selected)
                    {
                        CTableYK t = (CTableYK)dgv_YK_1.Rows[i].Tag;
                        a.Add(t.SN.ToString());
                    }
                }
                this.dgv_YK_1.DoDragDrop(a, DragDropEffects.Move);
            }
        }

        private void dgv_YK_2_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;

            Point p = this.dgv_YK_2.PointToClient(new Point(e.X, e.Y));
            DataGridView.HitTestInfo hit = this.dgv_YK_2.HitTest(p.X, p.Y);
            if (hit.RowIndex > 0)
            {
                cancel_dgv_select(dgv_YK_2);
                this.dgv_YK_2.Rows[hit.RowIndex].Selected = true;
            }
        }

        private void dgv_YK_2_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                Point p = this.dgv_YK_2.PointToClient(new Point(e.X, e.Y));
                DataGridView.HitTestInfo hit = this.dgv_YK_2.HitTest(p.X, p.Y);

                List<string> obj = (List<string>)e.Data.GetData(typeof(List<string>));
                if (obj.Count == 0) { return; }
                List<int> a = new List<int>();
                //----
                SortedList<int, CTableYK> lst_Temp = new SortedList<int, CTableYK>();//遥控表临时
                if (-1 == hit.RowIndex)
                {
                    for (int k = 0; k < Fwt.lst_Table_YK_2.Count; k++)
                    {
                        CTableYK t = Fwt.lst_Table_YK_2[k].MyClone();
                        lst_Temp.Add(lst_Temp.Count, t);
                    }
                    for (int i = 0; i < obj.Count; i++)
                    {
                        int key = Convert.ToInt32(obj[i]);
                        CTableYK t = Fwt.lst_Table_YK_1[key].MyClone();
                        lst_Temp.Add(lst_Temp.Count, t);
                        Fwt.lst_Table_YK_1[key].FlagDelete = true;
                        //----
                        a.Add(lst_Temp.Count - 1);
                    }
                }
                else
                {
                    for (int k = 0; k <= hit.RowIndex; k++)
                    {
                        CTableYK t = Fwt.lst_Table_YK_2[k].MyClone();
                        lst_Temp.Add(lst_Temp.Count, t);
                    }
                    for (int i = 0; i < obj.Count; i++)
                    {
                        int key = Convert.ToInt32(obj[i]);
                        CTableYK t = Fwt.lst_Table_YK_1[key].MyClone();
                        lst_Temp.Add(lst_Temp.Count, t);
                        Fwt.lst_Table_YK_1[key].FlagDelete = true;
                        //----
                        a.Add(lst_Temp.Count - 1);
                    }
                    for (int k = (hit.RowIndex + 1); k < Fwt.lst_Table_YK_2.Count; k++)
                    {
                        CTableYK t = Fwt.lst_Table_YK_2[k].MyClone();
                        lst_Temp.Add(lst_Temp.Count, t);
                    }
                }

                Fwt.lst_Table_YK_2.Clear();
                for (int k = 0; k < lst_Temp.Count; k++)
                {
                    CTableYK t = lst_Temp[k].MyClone();
                    Fwt.lst_Table_YK_2.Add(Fwt.lst_Table_YK_2.Count, t);
                }
                //----
                //for (int i = 0; i < obj.Count; i++)
                //{
                //    int key = Convert.ToInt32(obj[i]);
                //    CTableYK t = Fwt.lst_Table_YK_1[key].MyClone();
                //    Fwt.lst_Table_YK_2.Add(Fwt.lst_Table_YK_2.Count, t);
                //    Fwt.lst_Table_YK_1[key].FlagDelete = true;
                //}
                triggerAddr_YK(true);
                //----
                for (int k = 0; k < a.Count; k++)
                {
                    dgv_YK_2.Rows[a[k]].Selected = true;
                }
                enumType = eType.EN_TYPE_YK;
                timer2000.Start();
                //----
            }
            catch (Exception ex)
            {
                MessageBox.Show("YK1 drag drop err: " + ex.ToString());
            }
        }
        #endregion

        #region "遥控1←遥控2"
        private void tsmi_YK_Remove_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> a = new List<string>();
                for (int i = 0; i < dgv_YK_2.RowCount; i++)
                {
                    if (dgv_YK_2.Rows[i].Selected)
                    {
                        int key = Convert.ToInt32(dgv_YK_2.Rows[i].Cells[0].Tag);//change
                        a.Add(key.ToString());
                    }
                }
                if (a.Count == 0) { return; }
                for (int i = 0; i < a.Count; i++)
                {
                    int key = Convert.ToInt32(a[i]);
                    CTableYK t = Fwt.lst_Table_YK_2[key].MyClone();
                    Fwt.lst_Table_YK_1[t.SN].FlagDelete = false;//change
                    Fwt.lst_Table_YK_2.Remove(key);
                }
                //----list2排序，jifeng，2018-5-22 15:40
                SortedList<int, CTableYK> lst_Temp = new SortedList<int, CTableYK>();//遥控表临时
                foreach (var obj in Fwt.lst_Table_YK_2.Values)
                {
                    CTableYK t = obj.MyClone();
                    lst_Temp.Add(lst_Temp.Count, t);
                }
                Fwt.lst_Table_YK_2.Clear();
                foreach (var obj in lst_Temp.Values)
                {
                    CTableYK t = obj.MyClone();
                    Fwt.lst_Table_YK_2.Add(Fwt.lst_Table_YK_2.Count, t);
                }
                //----
                triggerAddr_YK(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("YK2 remove Err: " + ex.ToString());
            }
        }
        #endregion
        #endregion
        /// <summary>
        /// 计量
        /// </summary>
        #region "计量视图"
        void init_View_Meter()
        {
            init_View_Meter_1();
            init_View_Meter_2();
            cancel_dgv_select(dgv_Meter_1);
            cancel_dgv_select(dgv_Meter_2);
            gb_Meter_1.Text = string.Format("{0}[{1}条]", strGroupBoxName_1, dgv_Meter_1.RowCount);
            gb_Meter_2.Text = string.Format("{0}[{1}条]", strGroupBoxName_2, dgv_Meter_2.RowCount);

            if (Global.g_General_FwtSimpleColumn == 1)
            {
                dgv_Meter_1.Columns[4].Visible = false;
                dgv_Meter_1.Columns[6].Visible = false;
                dgv_Meter_1.Columns[7].Visible = false;

                dgv_Meter_2.Columns[4].Visible = false;
                dgv_Meter_2.Columns[6].Visible = false;
                dgv_Meter_2.Columns[7].Visible = false;
            }
        }
        void init_View_Meter_1()
        {
            int iPos = 0;
            int iCount = 0;
            dgv_Meter_1.Rows.Clear();
            foreach (var t in Fwt.lst_Table_Meter_1)
            {
                if (t.Value.FlagDelete == true) { continue; }
                dgv_Meter_1.RowCount += 1;
                iPos = 0;
                dgv_Meter_1.Rows[iCount].Cells[iPos++].Value = iCount + 1;//t.Key;// iCount + 1;
                dgv_Meter_1.Rows[iCount].Cells[iPos++].Value = t.Value.Id;
                dgv_Meter_1.Rows[iCount].Cells[iPos++].Value = t.Value.DeviceName;
                dgv_Meter_1.Rows[iCount].Cells[iPos++].Value = t.Value.ModelName;
                dgv_Meter_1.Rows[iCount].Cells[iPos++].Value = t.Value.GroupName;
                dgv_Meter_1.Rows[iCount].Cells[iPos++].Value = t.Value.ItemName;
                dgv_Meter_1.Rows[iCount].Cells[iPos++].Value = t.Value.DataType;
                dgv_Meter_1.Rows[iCount].Cells[iPos++].Value = t.Value.Unit;
                //dgv_Meter_1.Rows[iCount].Cells[iPos++].Value = t.Value.Ratio;
                dgv_Meter_1.Rows[iCount].Cells[iPos++].Value = getHex(t.Value.Addr);
                dgv_Meter_1.Rows[iCount].Tag = t.Value;
                iCount += 1;
            }
        }
        void init_View_Meter_2()
        {
            int iPos = 0;
            int iCount = 0;
            dgv_Meter_2.Rows.Clear();
            if (Fwt.lst_Table_Meter_2.Count == 0) { return; }
            foreach (var t in Fwt.lst_Table_Meter_2)
            {
                dgv_Meter_2.RowCount += 1;
                iPos = 0;
                dgv_Meter_2.Rows[iCount].Cells[iPos++].Value = t.Key + 1;// iCount + 1;
                dgv_Meter_2.Rows[iCount].Cells[iPos++].Value = t.Value.Id;
                dgv_Meter_2.Rows[iCount].Cells[iPos++].Value = t.Value.DeviceName;
                dgv_Meter_2.Rows[iCount].Cells[iPos++].Value = t.Value.ModelName;
                dgv_Meter_2.Rows[iCount].Cells[iPos++].Value = t.Value.GroupName;
                dgv_Meter_2.Rows[iCount].Cells[iPos++].Value = t.Value.ItemName;
                dgv_Meter_2.Rows[iCount].Cells[iPos++].Value = t.Value.DataType;
                dgv_Meter_2.Rows[iCount].Cells[iPos++].Value = t.Value.Unit;
                //dgv_Meter_2.Rows[iCount].Cells[iPos++].Value = t.Value.Ratio;
                dgv_Meter_2.Rows[iCount].Cells[iPos++].Value = getHex(t.Value.Addr);
                dgv_Meter_2.Rows[iCount].Tag = t.Value;
                dgv_Meter_2.Rows[iCount].Cells[0].Tag = t.Key;//change
                iCount += 1;
            }
            //----dgv属性设置
            for (int m = 0; m < dgv_Meter_2.RowCount; m++)
            {
                for (int k = 0; k < dgv_Meter_2.ColumnCount; k++)
                {
                    dgv_Meter_2.Rows[m].Cells[k].ReadOnly = true;
                }
            }
            if (dgv_Meter_2.RowCount > 0)
            {
                dgv_Meter_2.Rows[0].Cells[dgv_Meter_2.ColumnCount - 1].ReadOnly = false;
                dgv_Meter_2.Rows[0].Cells[dgv_Meter_2.ColumnCount - 1].Style.BackColor = Color.GreenYellow;
            }
        }
        #endregion

        #region "计量1→←计量2"
        #region "计量1→计量2"
        private void dgv_Meter_1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                List<string> a = new List<string>();
                for (int i = 0; i < dgv_Meter_1.RowCount; i++)
                {
                    if (dgv_Meter_1.Rows[i].Selected)
                    {
                        CTableMeter t = (CTableMeter)dgv_Meter_1.Rows[i].Tag;
                        a.Add(t.SN.ToString());
                    }
                }
                this.dgv_Meter_1.DoDragDrop(a, DragDropEffects.Move);
            }
        }

        private void dgv_Meter_2_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void dgv_Meter_2_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                Point p = this.dgv_Meter_2.PointToClient(new Point(e.X, e.Y));
                DataGridView.HitTestInfo hit = this.dgv_Meter_2.HitTest(p.X, p.Y);

                List<string> obj = (List<string>)e.Data.GetData(typeof(List<string>));
                if (obj.Count == 0) { return; }
                List<int> a = new List<int>();
                //----
                SortedList<int, CTableMeter> lst_Temp = new SortedList<int, CTableMeter>();//临时表
                if (-1 == hit.RowIndex)
                {
                    for (int k = 0; k < Fwt.lst_Table_Meter_2.Count; k++)
                    {
                        CTableMeter t = Fwt.lst_Table_Meter_2[k].MyClone();
                        lst_Temp.Add(lst_Temp.Count, t);
                    }
                    for (int i = 0; i < obj.Count; i++)
                    {
                        int key = Convert.ToInt32(obj[i]);
                        CTableMeter t = Fwt.lst_Table_Meter_1[key].MyClone();
                        lst_Temp.Add(lst_Temp.Count, t);
                        Fwt.lst_Table_Meter_1[key].FlagDelete = true;
                        //----
                        a.Add(lst_Temp.Count - 1);
                    }
                }
                else
                {
                    for (int k = 0; k <= hit.RowIndex; k++)
                    {
                        CTableMeter t = Fwt.lst_Table_Meter_2[k].MyClone();
                        lst_Temp.Add(lst_Temp.Count, t);
                    }
                    for (int i = 0; i < obj.Count; i++)
                    {
                        int key = Convert.ToInt32(obj[i]);
                        CTableMeter t = Fwt.lst_Table_Meter_1[key].MyClone();
                        lst_Temp.Add(lst_Temp.Count, t);
                        Fwt.lst_Table_Meter_1[key].FlagDelete = true;
                        //----
                        a.Add(lst_Temp.Count - 1);
                    }
                    for (int k = (hit.RowIndex + 1); k < Fwt.lst_Table_Meter_2.Count; k++)
                    {
                        CTableMeter t = Fwt.lst_Table_Meter_2[k].MyClone();
                        lst_Temp.Add(lst_Temp.Count, t);
                    }
                }

                Fwt.lst_Table_Meter_2.Clear();
                for (int k = 0; k < lst_Temp.Count; k++)
                {
                    CTableMeter t = lst_Temp[k].MyClone();
                    Fwt.lst_Table_Meter_2.Add(Fwt.lst_Table_Meter_2.Count, t);
                }
                //----

                //for (int i = 0; i < obj.Count; i++)
                //{
                //    int key = Convert.ToInt32(obj[i]);
                //    CTableMeter t = Fwt.lst_Table_Meter_1[key].MyClone();
                //    //Fwt.lst_Table_Meter_2.Add(key, t);
                //    Fwt.lst_Table_Meter_2.Add(Fwt.lst_Table_Meter_2.Count, t);
                //    Fwt.lst_Table_Meter_1[key].FlagDelete = true;
                //}
                triggerAddr_Meter(true);
                //----
                for (int k = 0; k < a.Count; k++)
                {
                    dgv_Meter_2.Rows[a[k]].Selected = true;
                }
                enumType = eType.EN_TYPE_METER;
                timer2000.Start();
                //----
            }
            catch (Exception ex)
            {
                MessageBox.Show("Meter1 drag drop err: " + ex.ToString());
            }
        }
        #endregion

        #region "计量1←计量2"
        private void tsmi_Meter_Remove_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> a = new List<string>();
                for (int i = 0; i < dgv_Meter_2.RowCount; i++)
                {
                    if (dgv_Meter_2.Rows[i].Selected)
                    {
                        int key = Convert.ToInt32(dgv_Meter_2.Rows[i].Cells[0].Tag);//change
                        a.Add(key.ToString());
                    }
                }
                if (a.Count == 0) { return; }
                for (int i = 0; i < a.Count; i++)
                {
                    int key = Convert.ToInt32(a[i]);
                    CTableMeter t = Fwt.lst_Table_Meter_2[key].MyClone();
                    Fwt.lst_Table_Meter_1[t.SN].FlagDelete = false;//change
                    Fwt.lst_Table_Meter_2.Remove(key);
                }
                //----list2排序，jifeng，2018-5-22 15:40
                SortedList<int, CTableMeter> lst_Temp = new SortedList<int, CTableMeter>();//临时表
                foreach (var obj in Fwt.lst_Table_Meter_2.Values)
                {
                    CTableMeter t = obj.MyClone();
                    lst_Temp.Add(lst_Temp.Count, t);
                }
                Fwt.lst_Table_Meter_2.Clear();
                foreach (var obj in lst_Temp.Values)
                {
                    CTableMeter t = obj.MyClone();
                    Fwt.lst_Table_Meter_2.Add(Fwt.lst_Table_Meter_2.Count, t);
                }
                //----
                triggerAddr_Meter(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Meter2 remove err: " + ex.ToString());
            }
        }
        #endregion
        #endregion
        /// //////////////////////////////////////////////////////////////////////////////////
        #region "cancel_dgv_select"
        public void cancel_dgv_select(DataGridView dgv)
        {
            if (dgv.RowCount == 0) { return; }
            if (dgv.SelectionMode == DataGridViewSelectionMode.FullRowSelect)
            {
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    if (dgv.Rows[i].Selected == true)
                    {
                        dgv.Rows[i].Selected = false;
                    }
                }
            }
            else if (dgv.SelectionMode == DataGridViewSelectionMode.CellSelect)
            {
                for (int i = 0; i < dgv.RowCount; i++)
                {
                    for (int j = 0; j < dgv.RowCount; j++)
                    {
                        if (dgv.Rows[i].Cells[j].Selected == true)
                        {
                            dgv.Rows[i].Cells[j].Selected = false;
                        }
                    }
                }
            }
        }
        #endregion

        #region "getHex"
        private string getHex(int addr)
        {
            return string.Format("0x{0:X}", addr);
        }
        #endregion

        #region "CellValueChanged"
        private void dgv_YC_2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgv_YC_2.ColumnCount - 1 && e.RowIndex >= 0)
            {
                CTableYC t = (CTableYC)dgv_YC_2.Rows[e.RowIndex].Tag;
                if (t == null) { return; }
                string strAddr = Convert.ToString(dgv_YC_2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                int iAddr = Convert.ToInt32(strAddr, 16);
                //----
                //foreach (var obj in Fwt.lst_Table_YC_2.Values)
                //{
                //    obj.Addr = iAddr;
                //    iAddr += 1;
                //}
                t.Addr = iAddr;
                //init_View_YC_2();
            }
            else if (e.ColumnIndex == dgv_YC_2.ColumnCount - 4 && e.RowIndex >= 0) //分组
            {
                CTableYC t = (CTableYC)dgv_YC_2.Rows[e.RowIndex].Tag;
                if (t == null) { return; }
                string strGroup = Convert.ToString(dgv_YC_2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                int iGroup = Convert.ToInt32(strGroup);
                //----
                t.iGroup = iGroup;
            }
            else if (e.ColumnIndex == dgv_YC_2.ColumnCount - 3 && e.RowIndex >= 0) //遥测死区
            {
                CTableYC t = (CTableYC)dgv_YC_2.Rows[e.RowIndex].Tag;
                if (t == null) { return; }
                string strYcZone = Convert.ToString(dgv_YC_2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                float fYcZone = Convert.ToSingle(strYcZone);
                //----
                t.fYcZone = fYcZone;
            }
            else if (e.ColumnIndex == dgv_YC_2.ColumnCount - 2 && e.RowIndex >= 0) //遥测调整系数
            {
                CTableYC t = (CTableYC)dgv_YC_2.Rows[e.RowIndex].Tag;
                if (t == null) { return; }
                string strYcCoe = Convert.ToString(dgv_YC_2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                float fYcCoe = Convert.ToSingle(strYcCoe);
                //----
                t.fYcCoe = fYcCoe;
            }
        }

        private void dgv_SYX_2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) { return; }
            if (e.ColumnIndex == dgv_SYX_2.ColumnCount - 1 /*&& e.RowIndex == 0*/)
            {
                CTableSYX t = (CTableSYX)dgv_SYX_2.Rows[e.RowIndex].Tag;
                if (t == null) { return; }
                int iKey = Convert.ToInt32(dgv_SYX_2.Rows[e.RowIndex].Cells[0].Value);
                string strAddr = Convert.ToString(dgv_SYX_2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                int iAddr = Convert.ToInt32(strAddr, 16);
                //----
                //foreach (var obj in Fwt.lst_Table_SYX_2)
                //{
                //    if (obj.Key == iKey)
                //    {
                //        obj.Value.Addr = iAddr;
                //    }
                //    //iAddr += 1;
                //}
                t.Addr = iAddr;
                //init_View_SYX_2();
            }
        }

        private void dgv_DYX_2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) { return; }
            if (e.ColumnIndex == dgv_DYX_2.ColumnCount - 1 /*&& e.RowIndex == 0*/)
            {
                CTableDYX t = (CTableDYX)dgv_DYX_2.Rows[e.RowIndex].Tag;
                if (t == null) { return; }
                int iKey = Convert.ToInt32(dgv_DYX_2.Rows[e.RowIndex].Cells[0].Value);
                string strAddr = Convert.ToString(dgv_DYX_2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                int iAddr = Convert.ToInt32(strAddr, 16);
                //----
                foreach (var obj in Fwt.lst_Table_DYX_2)
                {
                    if (obj.Key == iKey)
                    {
                        obj.Value.Addr = iAddr;
                    }
                    //iAddr += 1;
                }
                //init_View_DYX_2();
            }
        }

        private void dgv_YK_2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) { return; }
            if (e.ColumnIndex == dgv_YK_2.ColumnCount - 1)
            {
                CTableYK t = (CTableYK)dgv_YK_2.Rows[e.RowIndex].Tag;
                if (t == null) { return; }
                int iKey = Convert.ToInt32(dgv_YK_2.Rows[e.RowIndex].Cells[0].Value);
                string strAddr = Convert.ToString(dgv_YK_2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                int iAddr = Convert.ToInt32(strAddr, 16);
                //----
                foreach (var obj in Fwt.lst_Table_YK_2)
                {
                    if (obj.Key == iKey)
                    {
                        obj.Value.Addr = iAddr;
                    }

                    //iAddr += 1;
                }
                //init_View_YK_2();
            }
        }

        private void dgv_Meter_2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgv_Meter_2.ColumnCount - 1 && e.RowIndex == 0)
            {
                CTableMeter t = (CTableMeter)dgv_Meter_2.Rows[e.RowIndex].Tag;
                if (t == null) { return; }
                string strAddr = Convert.ToString(dgv_Meter_2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                int iAddr = Convert.ToInt32(strAddr, 16);
                //----
                foreach (var obj in Fwt.lst_Table_Meter_2.Values)
                {
                    obj.Addr = iAddr;
                    iAddr += 1;
                }
                init_View_Meter_2();
            }
        }
        #endregion

        #region "每次的拖拽/移除，都触发地址顺序排序"
        //YC:4001
        //YX:1
        //YK:6001
        //Meter:6401
        public void triggerAddr_YC(bool bRefView)
        {
            if (Fwt.lst_Table_YC_2.Count > 0)
            {
                int iAddr = (int)0x4001;
                foreach (var obj in Fwt.lst_Table_YC_2.Values)
                {
                    obj.Addr = iAddr;
                    iAddr += 1;
                }
            }
            if (true == bRefView) { init_View_YC(); };
        }
        //----DYX要特殊处理，地址要紧接着SYX编号
        void triggerAddr_SYX(bool bRefView)
        {
            //return;
            //if (Fwt.lst_Table_SYX_2.Count > 0)
            //{
            //    int iAddr = (int)0x0001;
            //    foreach (var obj in Fwt.lst_Table_SYX_2.Values)
            //    {
            //        obj.Addr = iAddr;
            //        iAddr += 1;
            //    }
            //}
            if (true == bRefView) { init_View_SYX(); };
        }
        void triggerAddr_DYX(bool bRefView)
        {
            if (Fwt.lst_Table_DYX_2.Count > 0)
            {
                int iAddr = getStartAddr();
                foreach (var obj in Fwt.lst_Table_DYX_2.Values)
                {
                    obj.Addr = iAddr;
                    iAddr += 1;
                }
            }
            if (true == bRefView) { init_View_DYX(); };
        }
        int getStartAddr()
        {
            int iAddr = 0;
            int iSYXCount = Fwt.lst_Table_SYX_2.Count;
            if (iSYXCount == 0)
            {
                iAddr = (int)0x0001;
            }
            else
            {
                int iAcc = 0;
                foreach (var obj in Fwt.lst_Table_SYX_2.Values)
                {
                    iAcc += 1;
                    if (iAcc == iSYXCount)
                    {
                        iAddr = obj.Addr + 1;
                    }
                }
            }
            return iAddr;
        }
        //----
        void triggerAddr_YK(bool bRefView)
        {
            if (Fwt.lst_Table_YK_2.Count > 0)
            {
                int iAddr = (int)0x6001;
                foreach (var obj in Fwt.lst_Table_YK_2.Values)
                {
                    obj.Addr = iAddr;
                    iAddr += 1;
                }
            }
            if (true == bRefView) { init_View_YK(); };
        }
        void triggerAddr_Meter(bool bRefView)
        {
            if (Fwt.lst_Table_Meter_2.Count > 0)
            {
                int iAddr = (int)0x6401;
                foreach (var obj in Fwt.lst_Table_Meter_2.Values)
                {
                    obj.Addr = iAddr;
                    iAddr += 1;
                }
            }
            if (true == bRefView) { init_View_Meter(); };
        }
        #endregion

        #region "单元格复制粘贴(遥测)"
        private int iColumnMouseDown = -1;
        private void dgv_YC_2_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (/*e.Button == System.Windows.Forms.MouseButtons.Left &&*/
                (e.ColumnIndex == dgv_YC_2.Columns.Count - 4 || e.ColumnIndex == dgv_YC_2.Columns.Count - 3 || e.ColumnIndex == dgv_YC_2.Columns.Count - 2))
            {
                dgv_YC_2.SelectionMode = DataGridViewSelectionMode.CellSelect;
            }
            else
            {
                dgv_YC_2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            }
            //----
            dgv_YC_2.ContextMenuStrip = null;
            if (e.RowIndex == -1)
            {
                if (e.ColumnIndex == dgv_YC_2.Columns.Count - 3)
                {
                    dgv_YC_2.ContextMenuStrip = CMS_YC_Zone;
                }
                else if (e.ColumnIndex == dgv_YC_2.Columns.Count - 2)
                {
                    dgv_YC_2.ContextMenuStrip = CMS_YC_Coe;
                }
            }
            else
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right &&
                (e.ColumnIndex == dgv_YC_2.Columns.Count - 4 || e.ColumnIndex == dgv_YC_2.Columns.Count - 3 || e.ColumnIndex == dgv_YC_2.Columns.Count - 2))
                {
                    dgv_YC_2.ContextMenuStrip = CMS_Copy;
                    iColumnMouseDown = e.ColumnIndex;
                }
                else
                {
                    dgv_YC_2.ContextMenuStrip = CMS_YC;
                }
            }
        }

        private void tsmi_Copy_Click(object sender, EventArgs e)
        {
            lst_Copy.Clear();
            for (int k = 0; k < dgv_YC_2.RowCount; k++)
            {
                if (dgv_YC_2.Rows[k].Cells[iColumnMouseDown/*dgv_YC_2.ColumnCount - 2*/].Selected == true)
                {
                    CTableYC obj = (CTableYC)dgv_YC_2.Rows[k].Tag;
                    lst_Copy.Add(lst_Copy.Count, obj);
                }
            }
            Form_CfgTool.pMainForm.formInfo.LogMessage(string.Format("已复制{0}行遥测记录[列名：{1}]",
                                                       lst_Copy.Count, dgv_YC_2.Columns[iColumnMouseDown].HeaderText));
        }

        private void tsmi_Paste_Click(object sender, EventArgs e)
        {
            if (lst_Copy.Count == 0) { return; }
            for (int k = 0; k < dgv_YC_2.RowCount; k++)
            {
                if (dgv_YC_2.Rows[k].Cells[iColumnMouseDown/*dgv_YC_2.ColumnCount - 2*/].Selected == true)
                {
                    CTableYC obj = (CTableYC)dgv_YC_2.Rows[k].Tag;
                    int key = k % lst_Copy.Count;
                    string strinfo = "";
                    if (iColumnMouseDown == dgv_YC_2.ColumnCount - 4)
                    {
                        strinfo = string.Format("将[顺序编号,实际编号]为[{0},{1}]的遥测记录的[列名：{2}]，从{3}修改成{4}",
                        dgv_YC_2.Rows[k].Cells[0].Value, obj.Id,
                        dgv_YC_2.Columns[iColumnMouseDown].HeaderText,
                        obj.iGroup, lst_Copy[key].iGroup);

                        obj.iGroup = lst_Copy[key].iGroup;
                        dgv_YC_2.Rows[k].Cells[iColumnMouseDown/*dgv_YC_2.ColumnCount - 2*/].Value = obj.iGroup;
                    }
                    else if (iColumnMouseDown == dgv_YC_2.ColumnCount - 3)
                    {
                        strinfo = string.Format("将[顺序编号,实际编号]为[{0},{1}]的遥测记录的[列名：{2}]，从{3}修改成{4}",
                        dgv_YC_2.Rows[k].Cells[0].Value, obj.Id,
                        dgv_YC_2.Columns[iColumnMouseDown].HeaderText,
                        obj.fYcZone, lst_Copy[key].fYcZone);

                        obj.fYcZone = lst_Copy[key].fYcZone;
                        dgv_YC_2.Rows[k].Cells[iColumnMouseDown/*dgv_YC_2.ColumnCount - 2*/].Value = obj.fYcZone;
                    }
                    else if (iColumnMouseDown == dgv_YC_2.ColumnCount - 2)
                    {
                        strinfo = string.Format("将[顺序编号,实际编号]为[{0},{1}]的遥测记录的[列名：{2}]，从{3}修改成{4}",
                        dgv_YC_2.Rows[k].Cells[0].Value, obj.Id,
                        dgv_YC_2.Columns[iColumnMouseDown].HeaderText,
                        obj.fYcCoe, lst_Copy[key].fYcCoe);

                        obj.fYcCoe = lst_Copy[key].fYcCoe;
                        dgv_YC_2.Rows[k].Cells[iColumnMouseDown/*dgv_YC_2.ColumnCount - 2*/].Value = obj.fYcCoe;
                    }
                    Form_CfgTool.pMainForm.formInfo.LogMessage(strinfo);
                }
            }
        }
        #endregion

        #region "单元格复制粘贴(单点遥信)"
        private void dgv_SYX_2_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && e.ColumnIndex == dgv_SYX_2.Columns.Count - 1)
            {
                dgv_SYX_2.ContextMenuStrip = CMS_SYX_AddrCopy;
            }
            else
            {
                dgv_SYX_2.ContextMenuStrip = CMS_SYX;
            }
        }

        private void tsmi_Syx_Addr_Copy_Click(object sender, EventArgs e)
        {
            lst_Copy_Syx.Clear();
            DataGridView dgv = dgv_SYX_2;
            for (int k = 0; k < dgv.RowCount; k++)
            {
                if (dgv.Rows[k].Cells[dgv.ColumnCount - 1].Selected == true)
                {
                    CTableSYX obj = (CTableSYX)dgv.Rows[k].Tag;
                    lst_Copy_Syx.Add(lst_Copy_Syx.Count, obj);
                }
            }
            Form_CfgTool.pMainForm.formInfo.LogMessage(string.Format("已复制{0}行单点遥信记录", lst_Copy.Count));
        }

        private void tsmi_Syx_Addr_Paste_Click(object sender, EventArgs e)
        {
            if (lst_Copy_Syx.Count == 0) { return; }
            DataGridView dgv = dgv_SYX_2;
            for (int k = 0; k < dgv.RowCount; k++)
            {
                if (dgv.Rows[k].Cells[dgv.ColumnCount - 1].Selected == true)
                {
                    CTableSYX obj = (CTableSYX)dgv.Rows[k].Tag;
                    int key = k % lst_Copy_Syx.Count;
                    string strinfo = string.Format("将[顺序编号,实际编号]为[{0},{1}]的单点遥信记录的地址，从{2}修改成{3}",
                        dgv.Rows[k].Cells[0].Value, obj.Id, obj.Addr, lst_Copy_Syx[key].Addr);
                    Form_CfgTool.pMainForm.formInfo.LogMessage(strinfo);
                    obj.Addr = lst_Copy_Syx[key].Addr;
                    dgv.Rows[k].Cells[dgv.ColumnCount - 1].Value = getHex(obj.Addr);
                }
            }
        }

        //所选顺序编号
        private void tsmi_Syx_Addr_SN_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dgv_SYX_2;
            string strAddr = "";
            int iAddr = 0;
            int iCount = 0;
            for (int k = 0; k < dgv.RowCount; k++)
            {
                if (dgv.Rows[k].Selected == true)
                {
                    iCount += 1;
                    CTableSYX t = (CTableSYX)dgv.Rows[k].Tag;
                    if (t == null) { return; }
                    if (iCount == 1)
                    {
                        strAddr = Convert.ToString(dgv.Rows[k].Cells[dgv.ColumnCount - 1].Value);
                        iAddr = Convert.ToInt32(strAddr, 16);
                    }
                    else if (iCount > 1)
                    {
                        iAddr += 1;
                        t.Addr = iAddr;
                        dgv.Rows[k].Cells[dgv.ColumnCount - 1].Value = getHex(t.Addr);
                    }
                }
            }
        }

        //全部顺序编号
        private void tsmi_Syx_Addr_SN_All_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dgv_SYX_2;
            string strAddr = "";
            int iAddr = 0;
            for (int k = 0; k < dgv.RowCount; k++)
            {
                CTableSYX t = (CTableSYX)dgv.Rows[k].Tag;
                if (t == null) { return; }
                if (k == 0)
                {
                    strAddr = Convert.ToString(dgv.Rows[k].Cells[dgv.ColumnCount - 1].Value);
                    iAddr = Convert.ToInt32(strAddr, 16);
                }
                else if (k >= 1)
                {
                    iAddr += 1;
                    t.Addr = iAddr;
                    dgv.Rows[k].Cells[dgv.ColumnCount - 1].Value = getHex(t.Addr);
                }
            }
        }
        #endregion

        #region "单元格复制粘贴(双点遥信)"
        private void dgv_DYX_2_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && e.ColumnIndex == dgv_DYX_2.Columns.Count - 1)
            {
                dgv_DYX_2.ContextMenuStrip = CMS_DYX_AddrCopy;
            }
            else
            {
                dgv_DYX_2.ContextMenuStrip = CMS_DYX;
            }
        }

        private void tsmi_Dyx_Addr_Copy_Click(object sender, EventArgs e)
        {
            lst_Copy_Dyx.Clear();
            DataGridView dgv = dgv_DYX_2;
            for (int k = 0; k < dgv.RowCount; k++)
            {
                if (dgv.Rows[k].Cells[dgv.ColumnCount - 1].Selected == true)
                {
                    CTableDYX obj = (CTableDYX)dgv.Rows[k].Tag;
                    lst_Copy_Dyx.Add(lst_Copy_Dyx.Count, obj);
                }
            }
            Form_CfgTool.pMainForm.formInfo.LogMessage(string.Format("已复制{0}行双点遥信记录", lst_Copy.Count));
        }

        private void tsmi_Dyx_Addr_Paste_Click(object sender, EventArgs e)
        {
            if (lst_Copy_Dyx.Count == 0) { return; }
            DataGridView dgv = dgv_DYX_2;
            for (int k = 0; k < dgv.RowCount; k++)
            {
                if (dgv.Rows[k].Cells[dgv.ColumnCount - 1].Selected == true)
                {
                    CTableDYX obj = (CTableDYX)dgv.Rows[k].Tag;
                    int key = k % lst_Copy_Dyx.Count;
                    string strinfo = string.Format("将[顺序编号,实际编号]为[{0},{1}]的双点遥信记录的地址，从{2}修改成{3}",
                        dgv.Rows[k].Cells[0].Value, obj.Id, obj.Addr, lst_Copy_Dyx[key].Addr);
                    Form_CfgTool.pMainForm.formInfo.LogMessage(strinfo);
                    obj.Addr = lst_Copy_Dyx[key].Addr;
                    dgv.Rows[k].Cells[dgv.ColumnCount - 1].Value = getHex(obj.Addr);
                }
            }
        }

        //所选顺序编号
        private void tsmi_Dyx_Addr_SN_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dgv_DYX_2;
            string strAddr = "";
            int iAddr = 0;
            int iCount = 0;
            for (int k = 0; k < dgv.RowCount; k++)
            {
                if (dgv.Rows[k].Selected == true)
                {
                    iCount += 1;
                    CTableDYX t = (CTableDYX)dgv.Rows[k].Tag;
                    if (t == null) { return; }
                    if (iCount == 1)
                    {
                        strAddr = Convert.ToString(dgv.Rows[k].Cells[dgv.ColumnCount - 1].Value);
                        iAddr = Convert.ToInt32(strAddr, 16);
                    }
                    else if (iCount > 1)
                    {
                        iAddr += 1;
                        t.Addr = iAddr;
                        dgv.Rows[k].Cells[dgv.ColumnCount - 1].Value = getHex(t.Addr);
                    }
                }
            }
        }

        //全部顺序编号
        private void tsmi_Dyx_Addr_SN_All_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dgv_DYX_2;
            string strAddr = "";
            int iAddr = 0;
            for (int k = 0; k < dgv.RowCount; k++)
            {
                CTableDYX t = (CTableDYX)dgv.Rows[k].Tag;
                if (t == null) { return; }
                if (k == 0)
                {
                    strAddr = Convert.ToString(dgv.Rows[k].Cells[dgv.ColumnCount - 1].Value);
                    iAddr = Convert.ToInt32(strAddr, 16);
                }
                else if (k >= 1)
                {
                    iAddr += 1;
                    t.Addr = iAddr;
                    dgv.Rows[k].Cells[dgv.ColumnCount - 1].Value = getHex(t.Addr);
                }
            }
        }
        #endregion

        #region "单元格复制粘贴(遥控)"
        private void dgv_YK_2_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right && e.ColumnIndex == dgv_YK_2.Columns.Count - 1)
            {
                dgv_YK_2.ContextMenuStrip = CMS_YK_AddrCopy;
            }
            else
            {
                dgv_YK_2.ContextMenuStrip = CMS_YK;
            }
        }

        private void tsmi_Yk_Addr_Copy_Click(object sender, EventArgs e)
        {
            lst_Copy_Yk.Clear();
            DataGridView dgv = dgv_YK_2;
            for (int k = 0; k < dgv.RowCount; k++)
            {
                if (dgv.Rows[k].Cells[dgv.ColumnCount - 1].Selected == true)
                {
                    CTableYK obj = (CTableYK)dgv.Rows[k].Tag;
                    lst_Copy_Yk.Add(lst_Copy_Yk.Count, obj);
                }
            }
            Form_CfgTool.pMainForm.formInfo.LogMessage(string.Format("已复制{0}行遥控记录", lst_Copy.Count));
        }

        private void tsmi_Yk_Addr_Paste_Click(object sender, EventArgs e)
        {
            if (lst_Copy_Yk.Count == 0) { return; }
            DataGridView dgv = dgv_YK_2;
            for (int k = 0; k < dgv.RowCount; k++)
            {
                if (dgv.Rows[k].Cells[dgv.ColumnCount - 1].Selected == true)
                {
                    CTableYK obj = (CTableYK)dgv.Rows[k].Tag;
                    int key = k % lst_Copy_Yk.Count;
                    string strinfo = string.Format("将[顺序编号,实际编号]为[{0},{1}]的遥控记录的地址，从{2}修改成{3}",
                        dgv.Rows[k].Cells[0].Value, obj.Id, obj.Addr, lst_Copy_Yk[key].Addr);
                    Form_CfgTool.pMainForm.formInfo.LogMessage(strinfo);
                    obj.Addr = lst_Copy_Yk[key].Addr;
                    dgv.Rows[k].Cells[dgv.ColumnCount - 1].Value = getHex(obj.Addr);
                }
            }
        }

        private void tsmi_Yk_Addr_SN_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dgv_YK_2;
            string strAddr = "";
            int iAddr = 0;
            int iCount = 0;
            for (int k = 0; k < dgv.RowCount; k++)
            {
                if (dgv.Rows[k].Selected == true)
                {
                    iCount += 1;
                    CTableYK t = (CTableYK)dgv.Rows[k].Tag;
                    if (t == null) { return; }
                    if (iCount == 1)
                    {
                        strAddr = Convert.ToString(dgv.Rows[k].Cells[dgv.ColumnCount - 1].Value);
                        iAddr = Convert.ToInt32(strAddr, 16);
                    }
                    else if (iCount > 1)
                    {
                        iAddr += 1;
                        t.Addr = iAddr;
                        dgv.Rows[k].Cells[dgv.ColumnCount - 1].Value = getHex(t.Addr);
                    }
                }
            }
        }

        private void tsmi_Yk_Addr_SN_All_Click(object sender, EventArgs e)
        {
            DataGridView dgv = dgv_YK_2;
            string strAddr = "";
            int iAddr = 0;
            for (int k = 0; k < dgv.RowCount; k++)
            {
                CTableYK t = (CTableYK)dgv.Rows[k].Tag;
                if (t == null) { return; }
                if (k == 0)
                {
                    strAddr = Convert.ToString(dgv.Rows[k].Cells[dgv.ColumnCount - 1].Value);
                    iAddr = Convert.ToInt32(strAddr, 16);
                }
                else if (k >= 1)
                {
                    iAddr += 1;
                    t.Addr = iAddr;
                    dgv.Rows[k].Cells[dgv.ColumnCount - 1].Value = getHex(t.Addr);
                }
            }
        }
        #endregion

        #region "恢复默认值"
        private void tsmi_RestoreDefault_Click(object sender, EventArgs e)
        {
            for (int k = 0; k < dgv_YC_2.RowCount; k++)
            {
                CTableYC t = (CTableYC)dgv_YC_2.Rows[k].Tag;
                if (t == null) { continue; }
                string strGroup = Convert.ToString(dgv_YC_2.Rows[k].Cells[dgv_YC_2.Columns.Count - 4].Value);
                int iGroup = Convert.ToInt32(strGroup);
                if (iGroup == 0) { t.fYcZone = 0.005f; }
                else if (iGroup == 1) { t.fYcZone = 0.5f; }
                else if (iGroup == 2) { t.fYcZone = 0.2f; }
                else if (iGroup == 3) { t.fYcZone = 3.0f; }
                else if (iGroup == 4) { t.fYcZone = 0.01f; }
                else if (iGroup == 5) { t.fYcZone = 0.005f; }
                //----
                dgv_YC_2.Rows[k].Cells[dgv_YC_2.Columns.Count - 3].Value = t.fYcZone;
            }
        }
        //----------------------------------------------------------------------
        private void tsmi_RestoreDefault_Type0_Click(object sender, EventArgs e)
        {
            for (int k = 0; k < dgv_YC_2.RowCount; k++)
            {
                CTableYC t = (CTableYC)dgv_YC_2.Rows[k].Tag;
                if (t == null) { continue; }
                string strGroup = Convert.ToString(dgv_YC_2.Rows[k].Cells[dgv_YC_2.Columns.Count - 4].Value);
                int iGroup = Convert.ToInt32(strGroup);
                if (iGroup == 0)
                {
                    t.fYcCoe = 100.0f;
                }
                else if (iGroup == 1)
                {
                    t.fYcCoe = 450.0f;
                }
                else if (iGroup == 2)
                {
                    t.fYcCoe = 60.0f;
                }
                else if (iGroup == 3)
                {
                    t.fYcCoe = 6600.0f;
                }
                else if (iGroup == 4)
                {
                    t.fYcCoe = 60.0f;
                }
                else if (iGroup == 5)
                {
                    t.fYcCoe = 1.0f;
                }
                //----
                dgv_YC_2.Rows[k].Cells[dgv_YC_2.Columns.Count - 2].Value = t.fYcCoe;
            }
        }

        private void tsmi_RestoreDefault_Type1_Click(object sender, EventArgs e)
        {
            for (int k = 0; k < dgv_YC_2.RowCount; k++)
            {
                CTableYC t = (CTableYC)dgv_YC_2.Rows[k].Tag;
                if (t == null) { continue; }
                string strGroup = Convert.ToString(dgv_YC_2.Rows[k].Cells[dgv_YC_2.Columns.Count - 4].Value);
                int iGroup = Convert.ToInt32(strGroup);
                if (iGroup == 0)
                {
                    t.fYcCoe = 100.0f;
                }
                else if (iGroup == 1)
                {
                    t.fYcCoe = 100.0f;
                }
                else if (iGroup == 2)
                {
                    t.fYcCoe = 1000.0f;
                }
                else if (iGroup == 3)
                {
                    t.fYcCoe = 10.0f;
                }
                else if (iGroup == 4)
                {
                    t.fYcCoe = 100.0f;
                }
                else if (iGroup == 5)
                {
                    t.fYcCoe = 1000.0f;
                }
                //----
                dgv_YC_2.Rows[k].Cells[dgv_YC_2.Columns.Count - 2].Value = t.fYcCoe;
            }
        }

        private void tsmi_RestoreDefault_Type2_Click(object sender, EventArgs e)
        {
            for (int k = 0; k < dgv_YC_2.RowCount; k++)
            {
                CTableYC t = (CTableYC)dgv_YC_2.Rows[k].Tag;
                if (t == null) { continue; }
                string strGroup = Convert.ToString(dgv_YC_2.Rows[k].Cells[dgv_YC_2.Columns.Count - 4].Value);
                int iGroup = Convert.ToInt32(strGroup);
                if (iGroup == 0)
                {
                    t.fYcCoe = 1.0f;
                }
                else if (iGroup == 1)
                {
                    t.fYcCoe = 1.0f;
                }
                else if (iGroup == 2)
                {
                    t.fYcCoe = 1.0f;
                }
                else if (iGroup == 3)
                {
                    t.fYcCoe = 1.0f;
                }
                else if (iGroup == 4)
                {
                    t.fYcCoe = 1.0f;
                }
                else if (iGroup == 5)
                {
                    t.fYcCoe = 1.0f;
                }
                //----
                dgv_YC_2.Rows[k].Cells[dgv_YC_2.Columns.Count - 2].Value = t.fYcCoe;
            }
        }
        #endregion

        #region "生成SNString"
        private void tsmi_Fwt_Left_Yc_Click(object sender, EventArgs e)
        {
            Form_Table_FWT_FastDrag f = new Form_Table_FWT_FastDrag(this, eFwtType.E_FWT_TYPE_YC);
            f.Show(this);
        }

        private void tsmi_Fwt_Left_Syx_Click(object sender, EventArgs e)
        {
            Form_Table_FWT_FastDrag f = new Form_Table_FWT_FastDrag(this, eFwtType.E_FWT_TYPE_SYX);
            f.Show(this);
        }

        private void tsmi_Fwt_Left_Dyx_Click(object sender, EventArgs e)
        {
            Form_Table_FWT_FastDrag f = new Form_Table_FWT_FastDrag(this, eFwtType.E_FWT_TYPE_DYX);
            f.Show(this);
        }

        private void tsmi_Fwt_Left_Yk_Click(object sender, EventArgs e)
        {
            Form_Table_FWT_FastDrag f = new Form_Table_FWT_FastDrag(this, eFwtType.E_FWT_TYPE_YK);
            f.Show(this);
        }

        private void tsmi_Fwt_Left_Meter_Click(object sender, EventArgs e)
        {
            Form_Table_FWT_FastDrag f = new Form_Table_FWT_FastDrag(this, eFwtType.E_FWT_TYPE_Meter);
            f.Show(this);
        }
        #endregion

        #region "获取SNString"
        private void tsmi_YC_SNString_Click(object sender, EventArgs e)
        {
            string strSN = "";
            if (Fwt.lst_Table_YC_2.Count > 0)
            {
                foreach (var obj in Fwt.lst_Table_YC_2.Values)
                {
                    strSN += obj.SN.ToString() + ",";
                }
                strSN = strSN.Substring(0, strSN.Length - 1);
            }
            if (strSN == "") { return; };
            Form_Table_FWT_SNString f = new Form_Table_FWT_SNString(this, eFwtType.E_FWT_TYPE_YC, strSN);
            f.Show(this);
        }

        private void tsmi_SYX_SNString_Click(object sender, EventArgs e)
        {
            string strSN = "";
            if (Fwt.lst_Table_SYX_2.Count > 0)
            {
                foreach (var obj in Fwt.lst_Table_SYX_2.Values)
                {
                    strSN += obj.SN.ToString() + ",";
                }
                strSN = strSN.Substring(0, strSN.Length - 1);
            }
            if (strSN == "") { return; };
            Form_Table_FWT_SNString f = new Form_Table_FWT_SNString(this, eFwtType.E_FWT_TYPE_SYX, strSN);
            f.Show(this);
        }

        private void tsmi_DYX_SNString_Click(object sender, EventArgs e)
        {
            string strSN = "";
            if (Fwt.lst_Table_DYX_2.Count > 0)
            {
                foreach (var obj in Fwt.lst_Table_DYX_2.Values)
                {
                    strSN += obj.SN.ToString() + ",";
                }
                strSN = strSN.Substring(0, strSN.Length - 1);
            }
            if (strSN == "") { return; };
            Form_Table_FWT_SNString f = new Form_Table_FWT_SNString(this, eFwtType.E_FWT_TYPE_DYX, strSN);
            f.Show(this);
        }

        private void tsmi_YK_SNString_Click(object sender, EventArgs e)
        {
            string strSN = "";
            if (Fwt.lst_Table_YK_2.Count > 0)
            {
                foreach (var obj in Fwt.lst_Table_YK_2.Values)
                {
                    strSN += obj.SN.ToString() + ",";
                }
                strSN = strSN.Substring(0, strSN.Length - 1);
            }
            if (strSN == "") { return; };
            Form_Table_FWT_SNString f = new Form_Table_FWT_SNString(this, eFwtType.E_FWT_TYPE_YK, strSN);
            f.Show(this);
        }

        private void tsmi_Meter_SNString_Click(object sender, EventArgs e)
        {
            string strSN = "";
            if (Fwt.lst_Table_Meter_2.Count > 0)
            {
                foreach (var obj in Fwt.lst_Table_Meter_2.Values)
                {
                    strSN += obj.SN.ToString() + ",";
                }
                strSN = strSN.Substring(0, strSN.Length - 1);
            }
            if (strSN == "") { return; };
            Form_Table_FWT_SNString f = new Form_Table_FWT_SNString(this, eFwtType.E_FWT_TYPE_Meter, strSN);
            f.Show(this);
        }
        #endregion

        #region "快速拖拽"
        public void FastDrag_YC(string strSN)
        {
            string[] sa = strSN.Split(',');
            for (int i = 0; i < sa.Length; i++)
            {
                int key = Convert.ToInt32(sa[i]);
                CTableYC t = Fwt.lst_Table_YC_1[key].MyClone();
                Fwt.lst_Table_YC_2.Add(Fwt.lst_Table_YC_2.Count, t);
                Fwt.lst_Table_YC_1[key].FlagDelete = true;
            }
            triggerAddr_YC(true);
        }

        public void FastDrag_SYX(string strSN)
        {
            string[] sa = strSN.Split(',');
            for (int i = 0; i < sa.Length; i++)
            {
                int key = Convert.ToInt32(sa[i]);
                CTableSYX t = Fwt.lst_Table_SYX_1[key].MyClone();
                Fwt.lst_Table_SYX_2.Add(Fwt.lst_Table_SYX_2.Count, t);
                Fwt.lst_Table_SYX_1[key].FlagDelete = true;
            }
            triggerAddr_SYX(true);
        }

        public void FastDrag_DYX(string strSN)
        {
            string[] sa = strSN.Split(',');
            for (int i = 0; i < sa.Length; i++)
            {
                int key = Convert.ToInt32(sa[i]);
                CTableDYX t = Fwt.lst_Table_DYX_1[key].MyClone();
                Fwt.lst_Table_DYX_2.Add(Fwt.lst_Table_DYX_2.Count, t);
                Fwt.lst_Table_DYX_1[key].FlagDelete = true;
            }
            triggerAddr_DYX(true);
        }

        public void FastDrag_YK(string strSN)
        {
            string[] sa = strSN.Split(',');
            for (int i = 0; i < sa.Length; i++)
            {
                int key = Convert.ToInt32(sa[i]);
                CTableYK t = Fwt.lst_Table_YK_1[key].MyClone();
                Fwt.lst_Table_YK_2.Add(Fwt.lst_Table_YK_2.Count, t);
                Fwt.lst_Table_YK_1[key].FlagDelete = true;
            }
            triggerAddr_YK(true);
        }

        public void FastDrag_Meter(string strSN)
        {
            string[] sa = strSN.Split(',');
            for (int i = 0; i < sa.Length; i++)
            {
                int key = Convert.ToInt32(sa[i]);
                CTableMeter t = Fwt.lst_Table_Meter_1[key].MyClone();
                Fwt.lst_Table_Meter_2.Add(Fwt.lst_Table_Meter_2.Count, t);
                Fwt.lst_Table_Meter_1[key].FlagDelete = true;
            }
            triggerAddr_Meter(true);
        }
        #endregion

        #region "拖动行高亮"
        private void dgv_YC_2_DragOver(object sender, DragEventArgs e)
        {
            Point p = this.dgv_YC_2.PointToClient(new Point(e.X, e.Y));
            DataGridView.HitTestInfo hit = this.dgv_YC_2.HitTest(p.X, p.Y);
            if (hit.RowIndex >= 0)
            {
                cancel_dgv_select(dgv_YC_2);
                this.dgv_YC_2.Rows[hit.RowIndex].Selected = true;
            }
        }

        private void dgv_SYX_2_DragOver(object sender, DragEventArgs e)
        {
            Point p = this.dgv_SYX_2.PointToClient(new Point(e.X, e.Y));
            DataGridView.HitTestInfo hit = this.dgv_SYX_2.HitTest(p.X, p.Y);
            if (hit.RowIndex >= 0)
            {
                cancel_dgv_select(dgv_SYX_2);
                this.dgv_SYX_2.Rows[hit.RowIndex].Selected = true;
            }
        }

        private void dgv_DYX_2_DragOver(object sender, DragEventArgs e)
        {
            Point p = this.dgv_DYX_2.PointToClient(new Point(e.X, e.Y));
            DataGridView.HitTestInfo hit = this.dgv_DYX_2.HitTest(p.X, p.Y);
            if (hit.RowIndex >= 0)
            {
                cancel_dgv_select(dgv_DYX_2);
                this.dgv_DYX_2.Rows[hit.RowIndex].Selected = true;
            }
        }

        private void dgv_YK_2_DragOver(object sender, DragEventArgs e)
        {
            Point p = this.dgv_YK_2.PointToClient(new Point(e.X, e.Y));
            DataGridView.HitTestInfo hit = this.dgv_YK_2.HitTest(p.X, p.Y);
            if (hit.RowIndex >= 0)
            {
                cancel_dgv_select(dgv_YK_2);
                this.dgv_YK_2.Rows[hit.RowIndex].Selected = true;
            }
        }

        private void dgv_Meter_2_DragOver(object sender, DragEventArgs e)
        {
            Point p = this.dgv_Meter_2.PointToClient(new Point(e.X, e.Y));
            DataGridView.HitTestInfo hit = this.dgv_Meter_2.HitTest(p.X, p.Y);
            if (hit.RowIndex >= 0)
            {
                cancel_dgv_select(dgv_Meter_2);
                this.dgv_Meter_2.Rows[hit.RowIndex].Selected = true;
            }
        }
        #endregion

        #region "左侧视图-右键菜单-添加"
        private void tsmi_yc1_lb_add_Click(object sender, EventArgs e)
        {
            List<string> a = new List<string>();
            for (int i = 0; i < dgv_YC_1.RowCount; i++)
            {
                if (dgv_YC_1.Rows[i].Selected)
                {
                    CTableYC t = (CTableYC)dgv_YC_1.Rows[i].Tag;
                    a.Add(t.SN.ToString());
                }
            }
            if (a.Count == 0) { return; }
            List<int> b = new List<int>();
            for (int i = 0; i < a.Count; i++)
            {
                int key = Convert.ToInt32(a[i]);
                CTableYC t = Fwt.lst_Table_YC_1[key].MyClone();
                Fwt.lst_Table_YC_2.Add(Fwt.lst_Table_YC_2.Count, t);
                if (Global.g_General_MultiYc == 0)
                {
                    Fwt.lst_Table_YC_1[key].FlagDelete = true;
                }
                else
                {
                    Fwt.lst_Table_YC_1[key].FlagDelete = false;
                }
                //----
                b.Add(Fwt.lst_Table_YC_2.Count - 1);
            }

            triggerAddr_YC(true);
            //----
            for (int k = 0; k < b.Count; k++)
            {
                dgv_YC_2.Rows[b[k]].Selected = true;
            }
            enumType = eType.EN_TYPE_YC;
            timer2000.Start();
            //----
        }

        private void tsmi_syx1_lb_add_Click(object sender, EventArgs e)
        {
            List<string> a = new List<string>();
            for (int i = 0; i < dgv_SYX_1.RowCount; i++)
            {
                if (dgv_SYX_1.Rows[i].Selected)
                {
                    CTableSYX t = (CTableSYX)dgv_SYX_1.Rows[i].Tag;
                    a.Add(t.SN.ToString());
                }
            }
            if (a.Count == 0) { return; }
            List<int> b = new List<int>();
            for (int i = 0; i < a.Count; i++)
            {
                int key = Convert.ToInt32(a[i]);
                CTableSYX t = Fwt.lst_Table_SYX_1[key].MyClone();
                Fwt.lst_Table_SYX_2.Add(Fwt.lst_Table_SYX_2.Count, t);
                if (Global.g_General_MultiYx == 0)
                {
                    Fwt.lst_Table_SYX_1[key].FlagDelete = true;
                }
                else
                {
                    Fwt.lst_Table_SYX_1[key].FlagDelete = false;
                }
                b.Add(Fwt.lst_Table_SYX_2.Count - 1);
            }

            triggerAddr_SYX(true);
            //----
            for (int k = 0; k < b.Count; k++)
            {
                dgv_SYX_2.Rows[b[k]].Selected = true;
            }
            enumType = eType.EN_TYPE_SYX;
            timer2000.Start();
            //----
        }

        private void tsmi_dyx1_lb_add_Click_Click(object sender, EventArgs e)
        {
            List<string> a = new List<string>();
            for (int i = 0; i < dgv_DYX_1.RowCount; i++)
            {
                if (dgv_DYX_1.Rows[i].Selected)
                {
                    CTableDYX t = (CTableDYX)dgv_DYX_1.Rows[i].Tag;
                    a.Add(t.SN.ToString());
                }
            }
            if (a.Count == 0) { return; }
            List<int> b = new List<int>();
            for (int i = 0; i < a.Count; i++)
            {
                int key = Convert.ToInt32(a[i]);
                CTableDYX t = Fwt.lst_Table_DYX_1[key].MyClone();
                Fwt.lst_Table_DYX_2.Add(Fwt.lst_Table_DYX_2.Count, t);
                Fwt.lst_Table_DYX_1[key].FlagDelete = true;
                //----
                b.Add(Fwt.lst_Table_DYX_2.Count - 1);
            }

            triggerAddr_DYX(true);
            //----
            for (int k = 0; k < b.Count; k++)
            {
                dgv_DYX_2.Rows[b[k]].Selected = true;
            }
            enumType = eType.EN_TYPE_DYX;
            timer2000.Start();
            //----
        }

        private void tsmi_yk1_lb_add_Click(object sender, EventArgs e)
        {
            List<string> a = new List<string>();
            for (int i = 0; i < dgv_YK_1.RowCount; i++)
            {
                if (dgv_YK_1.Rows[i].Selected)
                {
                    CTableYK t = (CTableYK)dgv_YK_1.Rows[i].Tag;
                    a.Add(t.SN.ToString());
                }
            }
            if (a.Count == 0) { return; }
            List<int> b = new List<int>();
            for (int i = 0; i < a.Count; i++)
            {
                int key = Convert.ToInt32(a[i]);
                CTableYK t = Fwt.lst_Table_YK_1[key].MyClone();
                Fwt.lst_Table_YK_2.Add(Fwt.lst_Table_YK_2.Count, t);
                Fwt.lst_Table_YK_1[key].FlagDelete = true;
                //----
                b.Add(Fwt.lst_Table_YK_2.Count - 1);
            }

            triggerAddr_YK(true);
            //----
            for (int k = 0; k < b.Count; k++)
            {
                dgv_YK_2.Rows[b[k]].Selected = true;
            }
            enumType = eType.EN_TYPE_YK;
            timer2000.Start();
            //----
        }

        private void tsmi_meter1_lb_add_Click(object sender, EventArgs e)
        {
            List<string> a = new List<string>();
            for (int i = 0; i < dgv_Meter_1.RowCount; i++)
            {
                if (dgv_Meter_1.Rows[i].Selected)
                {
                    CTableMeter t = (CTableMeter)dgv_Meter_1.Rows[i].Tag;
                    a.Add(t.SN.ToString());
                }
            }
            if (a.Count == 0) { return; }
            List<int> b = new List<int>();
            for (int i = 0; i < a.Count; i++)
            {
                int key = Convert.ToInt32(a[i]);
                CTableMeter t = Fwt.lst_Table_Meter_1[key].MyClone();
                Fwt.lst_Table_Meter_2.Add(Fwt.lst_Table_Meter_2.Count, t);
                Fwt.lst_Table_Meter_1[key].FlagDelete = true;
                //----
                b.Add(Fwt.lst_Table_Meter_2.Count - 1);
            }

            triggerAddr_Meter(true);
            //----
            for (int k = 0; k < b.Count; k++)
            {
                dgv_Meter_2.Rows[b[k]].Selected = true;
            }
            enumType = eType.EN_TYPE_METER;
            timer2000.Start();
            //----
        }
        #endregion

        #region "2000毫秒定时器"
        private void timer2000_Tick(object sender, EventArgs e)
        {
            timer2000.Stop();

            if (enumType == eType.EN_TYPE_YC)
            {
                cancel_dgv_select(dgv_YC_2);
            }
            else if (enumType == eType.EN_TYPE_SYX)
            {
                cancel_dgv_select(dgv_SYX_2);
            }
            else if (enumType == eType.EN_TYPE_DYX)
            {
                cancel_dgv_select(dgv_DYX_2);
            }
            else if (enumType == eType.EN_TYPE_YK)
            {
                cancel_dgv_select(dgv_YK_2);
            }
            else if (enumType == eType.EN_TYPE_METER)
            {
                cancel_dgv_select(dgv_Meter_2);
            }
        }
        #endregion

        private void tsmi_YC_AddrImport_Click(object sender, EventArgs e)
        {
            string path = System.Environment.CurrentDirectory + @"\Text";//@是取消转义字符的意思
            OpenFileDialog fd = new OpenFileDialog();
            fd.Title = "请选择地址文本";
            fd.Filter = "文本文件(*.txt)|*.txt";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                int icount = 0;
                string strAbsolutePath = System.IO.Path.GetFullPath(fd.FileName);//绝对路径
                string strFileName = System.IO.Path.GetFileNameWithoutExtension(fd.FileName);//文件名没有扩展名
                using (StreamReader sr = new StreamReader(strAbsolutePath, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        string readStr = sr.ReadLine();
                        readStr.Trim();
                        if (readStr == "") { continue; }
                        int iAddr = Convert.ToInt32(readStr);
                        iAddr += Global.g_General_YcAddrOffset;
                        dgv_YC_2.Rows[icount].Cells[12].Value = getHex(iAddr);
                        dgv_YC_2.Rows[icount].Cells[12].Tag = iAddr;
                        icount++;
                    }
                    //END
                }
                fd.InitialDirectory = fd.FileName.Substring(0, fd.FileName.LastIndexOf("\\") + 1);
            }

            int iItemCnt = 0;
            foreach (var t in Fwt.lst_Table_YC_2)
            {
                int ireal = Convert.ToInt32(dgv_YC_2.Rows[iItemCnt].Cells[12].Tag);
                t.Value.Addr = ireal;
                iItemCnt++;
            }
        }

        private void tsmi_SYX_AddrImport_Click(object sender, EventArgs e)
        {
            string path = System.Environment.CurrentDirectory + @"\Text";//@是取消转义字符的意思
            OpenFileDialog fd = new OpenFileDialog();
            fd.Title = "请选择地址文本";
            fd.Filter = "文本文件(*.txt)|*.txt";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                int icount = 0;
                string strAbsolutePath = System.IO.Path.GetFullPath(fd.FileName);//绝对路径
                string strFileName = System.IO.Path.GetFileNameWithoutExtension(fd.FileName);//文件名没有扩展名
                using (StreamReader sr = new StreamReader(strAbsolutePath, Encoding.Default))
                {
                    while (!sr.EndOfStream)
                    {
                        string readStr = sr.ReadLine();
                        readStr.Trim();
                        if (readStr == "") { continue; }
                        int iAddr = Convert.ToInt32(readStr);
                        iAddr += Global.g_General_SyxAddrOffset;
                        dgv_SYX_2.Rows[icount].Cells[7].Value = getHex(iAddr);
                        dgv_SYX_2.Rows[icount].Cells[7].Tag = iAddr;
                        icount++;
                    }
                    //END
                }
                fd.InitialDirectory = fd.FileName.Substring(0, fd.FileName.LastIndexOf("\\") + 1);
            }

            int iItemCnt = 0;
            foreach (var t in Fwt.lst_Table_SYX_2)
            {
                int ireal = Convert.ToInt32(dgv_SYX_2.Rows[iItemCnt].Cells[7].Tag);
                t.Value.Addr = ireal;
                iItemCnt++;
            }
        }

        #region "组合遥信"
        void init_View_GYX()
        {
            init_View_GYX_1();
            init_View_GYX_2();
            init_View_GYX_3();
            init_View_GYX_4();
        }

        void init_View_GYX_1()
        {
            int iPos = 0;
            int iCount = 0;
            dgv_GYX_1.ReadOnly = true;
            dgv_GYX_1.Rows.Clear();
            foreach (var t in Fwt.lst_Table_GYX)
            {
                dgv_GYX_1.RowCount += 1;
                iPos = 0;
                dgv_GYX_1.Rows[iCount].Cells[iPos++].Value = iCount + 1;//t.Key;// iCount + 1;
                //dgv_SYX_1.Rows[iCount].Cells[iPos++].Value = t.Value.Id;
                dgv_GYX_1.Rows[iCount].Cells[iPos++].Value = t.Value.ItemName;
                dgv_GYX_1.Rows[iCount].Tag = t.Value;
                iCount += 1;
            }
        }

        void init_View_GYX_2()
        {
            int index = GetIndex_GYX();
            if (-1 == index)
            {
                return;
            }

            CTableGYX gyx = Fwt.lst_Table_GYX[index];

            dgv_GYX_2.Rows.Clear();
            dgv_GYX_2.ReadOnly = true;
            dgv_GYX_2.RowCount = gyx.logic.lstOr.Count();
            for (int k = 0; k < dgv_GYX_2.RowCount; k++)
            {
                dgv_GYX_2.Rows[k].Cells[0].Value = k + 1;
                dgv_GYX_2.Rows[k].Cells[1].Value = string.Format("{0} 或表达式{1}", dgv_GYX_1.Rows[index].Cells[1].Value, k + 1);
            }
        }

        void init_View_GYX_3()
        {
            int index1 = GetIndex_GYX();
            int index2 = GetIndex_GYX_Or();
            if (-1 == index1 || -1 == index2)
            {
                return;
            }
            CTableGYX gyx = Fwt.lst_Table_GYX[index1];
            LogicCfg_Or_t t_or = gyx.logic.lstOr[index2];

            dgv_GYX_3.Rows.Clear();
            dgv_GYX_3.ReadOnly = true;
            dgv_GYX_3.RowCount = t_or.lstAnd.Count();
            for (int k = 0; k < dgv_GYX_3.RowCount; k++)
            {
                dgv_GYX_3.Rows[k].Cells[0].Value = k + 1;
                dgv_GYX_3.Rows[k].Cells[1].Value = string.Format("{0} 与表达式{1}",
                    dgv_GYX_2.Rows[index2].Cells[1].Value,
                    k + 1);
            }
        }

        void init_View_GYX_4()
        {
            int index1 = GetIndex_GYX();
            int index2 = GetIndex_GYX_Or();
            int index3 = GetIndex_GYX_And();
            if (-1 == index1 || -1 == index2 || -1 == index3)
            {
                return;
            }

            CTableGYX gyx = Fwt.lst_Table_GYX[index1];
            LogicCfg_Or_t tor = gyx.logic.lstOr[index2];
            LogicCfg_And_t tand = tor.lstAnd[index3];

            cmb_gyx_11.SelectedIndex = tand.u8Data1Attr;
            cmb_gyx_12.Items.Clear();
            if (0 == cmb_gyx_11.SelectedIndex)
            {//通道号
                foreach (var t in Fwt.lst_Table_SYX_1)
                {
                    cmb_gyx_12.Items.Add(t.Value.ItemName);
                }
            }
            else
            {//常数
                cmb_gyx_12.Items.Add("0");
                cmb_gyx_12.Items.Add("1");
            }
            if (tand.u16Data1Input < cmb_gyx_12.Items.Count)
            {
                cmb_gyx_12.SelectedIndex = tand.u16Data1Input;
            }
            else
            {
                tand.u16Data1Input = 0;
                cmb_gyx_12.SelectedIndex = tand.u16Data1Input;
            }

            cmb_gyx_21.SelectedIndex = tand.u8Data2Attr;
            cmb_gyx_22.Items.Clear();
            if (0 == cmb_gyx_21.SelectedIndex)
            {//通道号
                foreach (var t in Fwt.lst_Table_SYX_1)
                {
                    cmb_gyx_22.Items.Add(t.Value.ItemName);
                }
            }
            else
            {//常数
                cmb_gyx_22.Items.Add("0");
                cmb_gyx_22.Items.Add("1");
            }
            if (tand.u16Data2Input < cmb_gyx_22.Items.Count)
            {
                cmb_gyx_22.SelectedIndex = tand.u16Data2Input;
            }
            else
            {
                tand.u16Data2Input = 0;
                cmb_gyx_22.SelectedIndex = tand.u16Data2Input;
            }

            cmb_gyx_3.SelectedIndex = tand.u8ResultAttr;
            cmb_gyx_4.SelectedIndex = tand.u8Opt;
        }
        #endregion

        private void tsmi_GYX_NewOr_Click(object sender, EventArgs e)
        {
            int index = GetIndex_GYX();
            if (-1 == index)
            {
                return;
            }
            CTableGYX gyx = Fwt.lst_Table_GYX[index];
            LogicCfg_Or_t obj = new LogicCfg_Or_t();
            obj.u16OrNo = (UInt16)gyx.logic.lstOr.Count();

            gyx.logic.lstOr.Add(obj.u16OrNo, obj);
            init_View_GYX_2();
        }

        int GetIndex_GYX()
        {
            for (int m = 0; m < dgv_GYX_1.RowCount; m++)
            {
                if (dgv_GYX_1.Rows[m].Selected == true)
                {
                    return m;
                }
            }
            return -1;
        }

        int GetIndex_GYX_Or()
        {
            for (int m = 0; m < dgv_GYX_2.RowCount; m++)
            {
                if (dgv_GYX_2.Rows[m].Selected == true)
                {
                    return m;
                }
            }
            return -1;
        }

        int GetIndex_GYX_And()
        {
            for (int m = 0; m < dgv_GYX_3.RowCount; m++)
            {
                if (dgv_GYX_3.Rows[m].Selected == true)
                {
                    return m;
                }
            }
            return -1;
        }

        private void tsmi_GYX_NewAnd_Click(object sender, EventArgs e)
        {
            int index1 = GetIndex_GYX();
            int index2 = GetIndex_GYX_Or();
            if (-1 == index1 || -1 == index2)
            {
                return;
            }
            CTableGYX gyx = Fwt.lst_Table_GYX[index1];
            LogicCfg_Or_t t_or = gyx.logic.lstOr[index2];

            LogicCfg_And_t obj = new LogicCfg_And_t();
            obj.u16AndNo = (UInt16)t_or.lstAnd.Count();

            t_or.lstAnd.Add(obj.u16AndNo, obj);
            init_View_GYX_3();
        }

        private void dgv_GYX_1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            init_View_GYX_2();
            if(dgv_GYX_3.RowCount > 0)
            {
                dgv_GYX_3.Rows.Clear();
            }
        }

        private void dgv_GYX_2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            init_View_GYX_3();
        }

        private void dgv_GYX_3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
            {
                return;
            }
            init_View_GYX_4();
        }

        #region "下拉列表响应事件"
        private void cmb_gyx_11_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index1 = GetIndex_GYX();
            int index2 = GetIndex_GYX_Or();
            int index3 = GetIndex_GYX_And();
            if (-1 == index1 || -1 == index2 || -1 == index3)
            {
                return;
            }

            CTableGYX gyx = Fwt.lst_Table_GYX[index1];
            LogicCfg_Or_t tor = gyx.logic.lstOr[index2];
            LogicCfg_And_t tand = tor.lstAnd[index3];

            int index = cmb_gyx_11.SelectedIndex;
            cmb_gyx_12.Items.Clear();
            if (0 == index)
            {
                foreach (var t in Fwt.lst_Table_SYX_1)
                {
                    cmb_gyx_12.Items.Add(t.Value.ItemName);
                }
            }
            else if (1 == index)
            {
                cmb_gyx_12.Items.Add("0");
                cmb_gyx_12.Items.Add("1");
            }

            tand.u8Data1Attr = (byte)index;
        }

        private void cmb_gyx_12_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index1 = GetIndex_GYX();
            int index2 = GetIndex_GYX_Or();
            int index3 = GetIndex_GYX_And();
            if (-1 == index1 || -1 == index2 || -1 == index3)
            {
                return;
            }

            CTableGYX gyx = Fwt.lst_Table_GYX[index1];
            LogicCfg_Or_t tor = gyx.logic.lstOr[index2];
            LogicCfg_And_t tand = tor.lstAnd[index3];

            tand.u16Data1Input = (byte)cmb_gyx_12.SelectedIndex;
        }

        private void cmb_gyx_21_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index1 = GetIndex_GYX();
            int index2 = GetIndex_GYX_Or();
            int index3 = GetIndex_GYX_And();
            if (-1 == index1 || -1 == index2 || -1 == index3)
            {
                return;
            }

            CTableGYX gyx = Fwt.lst_Table_GYX[index1];
            LogicCfg_Or_t tor = gyx.logic.lstOr[index2];
            LogicCfg_And_t tand = tor.lstAnd[index3];

            int index = cmb_gyx_21.SelectedIndex;
            cmb_gyx_22.Items.Clear();
            if (0 == index)
            {
                foreach (var t in Fwt.lst_Table_SYX_1)
                {
                    cmb_gyx_22.Items.Add(t.Value.ItemName);
                }
            }
            else if (1 == index)
            {
                cmb_gyx_22.Items.Add("0");
                cmb_gyx_22.Items.Add("1");
            }

            tand.u8Data2Attr = (byte)index;
        }

        private void cmb_gyx_22_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index1 = GetIndex_GYX();
            int index2 = GetIndex_GYX_Or();
            int index3 = GetIndex_GYX_And();
            if (-1 == index1 || -1 == index2 || -1 == index3)
            {
                return;
            }

            CTableGYX gyx = Fwt.lst_Table_GYX[index1];
            LogicCfg_Or_t tor = gyx.logic.lstOr[index2];
            LogicCfg_And_t tand = tor.lstAnd[index3];

            tand.u16Data2Input = (byte)cmb_gyx_22.SelectedIndex;
        }

        private void cmb_gyx_3_SelectedIndexChanged(object sender, EventArgs e)
        {//输出不作处理, 输出取反
            int index1 = GetIndex_GYX();
            int index2 = GetIndex_GYX_Or();
            int index3 = GetIndex_GYX_And();
            if (-1 == index1 || -1 == index2 || -1 == index3)
            {
                return;
            }

            CTableGYX gyx = Fwt.lst_Table_GYX[index1];
            LogicCfg_Or_t tor = gyx.logic.lstOr[index2];
            LogicCfg_And_t tand = tor.lstAnd[index3];

            tand.u8ResultAttr = (byte)cmb_gyx_3.SelectedIndex;
        }

        private void cmb_gyx_4_SelectedIndexChanged(object sender, EventArgs e)
        {//等于，小于，大于
            int index1 = GetIndex_GYX();
            int index2 = GetIndex_GYX_Or();
            int index3 = GetIndex_GYX_And();
            if (-1 == index1 || -1 == index2 || -1 == index3)
            {
                return;
            }

            CTableGYX gyx = Fwt.lst_Table_GYX[index1];
            LogicCfg_Or_t tor = gyx.logic.lstOr[index2];
            LogicCfg_And_t tand = tor.lstAnd[index3];

            tand.u8Opt = (byte)cmb_gyx_4.SelectedIndex;
        }
        #endregion

        int gu16LogicCfgSize = 0;
        private void Generate_GYX()
        {
            int m = 0;
            int k = 0;

            gu16LogicCfgSize = 4;
            foreach (var g in Fwt.lst_Table_GYX)
            {
                int iCntBytes = 0;
                //U16 u16LogicNo;
                //U16 u16LogicSize;
                //U16 u16ZhyxChannel;
                //U16 u16OrNumber;
                g.Value.logic.u16LogicNo = (UInt16)g.Value.Id;
                g.Value.logic.u16LogicSize = 0;//最后再修正
                g.Value.logic.u16ZhyxChannel = (UInt16)g.Value.SN;
                g.Value.logic.u16OrNumber = (UInt16)g.Value.logic.lstOr.Count();
                m = 0;
                iCntBytes = 0;
                foreach (var lor in g.Value.logic.lstOr)
                {
                    //U16 u16OrNo;
                    //U16 u16OrSize;
                    //U16 u16Res1;
                    //U16 u16AndNumber;
                    lor.Value.u16OrNo = (UInt16)m;
                    lor.Value.u16OrSize = (UInt16)(8 + lor.Value.lstAnd.Count() * 16);//
                    lor.Value.u16Res1 = 0;
                    lor.Value.u16AndNumber = (UInt16)lor.Value.lstAnd.Count();
                    iCntBytes += lor.Value.u16OrSize; //计算大小
                    k = 0;
                    foreach (var land in lor.Value.lstAnd) //每个与 16个字节大小
                    {
                        //U16 u16AndNo;			//该And表达式的编号
                        //U16 u16AndSize;			//该And表达式的大小
                        //U16 u16Data1Input;
                        //U16 u16Data2Input;
                        //U8 u8Data1Attr;			//0:输入为通道号;1:输入为常数
                        //U8 u8Data2Attr;			//0:输入为通道号;1:输入为常数
                        //U8 u8ResultAttr;			//0:输出不作处理;1:输出取反
                        //U8 u8Opt;				//0:等于;1:小于;2:大于
                        //U8 u8Res[4];
                        land.Value.u16AndNo = (UInt16)k;
                        land.Value.u16AndSize = 16;//目前，就2个操作数，大小视为固定，每个12个字节
                        k++;
                    }
                    m++;
                }

                g.Value.logic.u16LogicSize = (UInt16)(8 + iCntBytes); //修正大小
                gu16LogicCfgSize += (g.Value.logic.u16LogicSize);
            }
        }

        private void btnGYXSave_Click(object sender, EventArgs e)
        {//generate LogicYx.cfg

            Generate_GYX();

            string path = System.Environment.CurrentDirectory + @"\Binary";
            SaveFileDialog fd = new SaveFileDialog();
            fd.Title = "保存为组合遥信配置文件";
            //fd.InitialDirectory = path;
            DateTime dt = DateTime.Now;
            fd.FileName = string.Format("LogicYx", dt.Year, dt.Month, dt.Day);
            fd.Filter = "配置文件(*.cfg)|*.cfg";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(fd.FileName, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);

                //----
                //u16 LogicCfgSize
                //U16 u16LogicCfgNumber;
                bw.Write((Int16)gu16LogicCfgSize);
                bw.Write((Int16)Fwt.lst_Table_GYX.Count());
                foreach (var g in Fwt.lst_Table_GYX)
                {
                    //U16 u16LogicNo;
                    //U16 u16LogicSize;
                    //U16 u16ZhyxChannel;
                    //U16 u16OrNumber;
                    bw.Write((UInt16)g.Value.logic.u16LogicNo);
                    bw.Write((UInt16)g.Value.logic.u16LogicSize);
                    bw.Write((UInt16)g.Value.logic.u16ZhyxChannel);
                    bw.Write((UInt16)g.Value.logic.u16OrNumber);
                    foreach (var lor in g.Value.logic.lstOr)
                    {
                        //U16 u16OrNo;
                        //U16 u16OrSize;
                        //U16 u16Res1;
                        //U16 u16AndNumber;
                        bw.Write((UInt16)lor.Value.u16OrNo);
                        bw.Write((UInt16)lor.Value.u16OrSize);
                        bw.Write((UInt16)lor.Value.u16Res1);
                        bw.Write((UInt16)lor.Value.u16AndNumber);
                        foreach (var land in lor.Value.lstAnd)
                        {
                            //U16 u16AndNo;			//该And表达式的编号
                            //U16 u16AndSize;			//该And表达式的大小
                            //U16 u16Data1Input;
                            //U16 u16Data2Input;
                            //U8 u8Data1Attr;			//0:输入为通道号;1:输入为常数
                            //U8 u8Data2Attr;			//0:输入为通道号;1:输入为常数
                            //U8 u8ResultAttr;			//0:输出不作处理;1:输出取反
                            //U8 u8Opt;				//0:等于;1:小于;2:大于
                            //U8 u8Res[4];
                            bw.Write((UInt16)land.Value.u16AndNo);
                            bw.Write((UInt16)land.Value.u16AndSize);
                            bw.Write((UInt16)land.Value.u16Data1Input);
                            bw.Write((UInt16)land.Value.u16Data2Input);
                            bw.Write((Byte)land.Value.u8Data1Attr);
                            bw.Write((Byte)land.Value.u8Data2Attr);
                            bw.Write((Byte)land.Value.u8ResultAttr);
                            bw.Write((Byte)land.Value.u8Opt);
                            bw.Write((Byte)land.Value.u8Res[0]);
                            bw.Write((Byte)land.Value.u8Res[1]);
                            bw.Write((Byte)land.Value.u8Res[2]);
                            bw.Write((Byte)land.Value.u8Res[3]);
                        }
                    }
                }
                //----

                bw.Close();
                fs.Close();
                MessageBox.Show(string.Format("保存[{0}]成功！", fd.FileName), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGYXImport_Click(object sender, EventArgs e)
        {
            string path = System.Environment.CurrentDirectory + @"\Binary";
            OpenFileDialog fd = new OpenFileDialog();
            fd.Title = "导入组合遥信配置";
            //fd.InitialDirectory = path;
            fd.Filter = "二进制文件(*.cfg)|*.cfg";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                FileStream fs = new FileStream(fd.FileName, FileMode.Open);
                BinaryReader br = new BinaryReader(fs);

                foreach(var t in Fwt.lst_Table_GYX)
                {
                    foreach (var tand in t.Value.logic.lstOr)
                    {
                        tand.Value.lstAnd.Clear();
                    }
                    t.Value.logic.lstOr.Clear();
                }
                //----
                UInt16 u16LogicCfgSize = br.ReadUInt16();
                UInt16 u16LogicCfgNumber = br.ReadUInt16();
                for (int m = 0; m < u16LogicCfgNumber;m++ )
                {
                    //LogicCfg_Logic_t tlogic = new LogicCfg_Logic_t();
                    Fwt.lst_Table_GYX[m].logic.u16LogicNo = br.ReadUInt16();
                    Fwt.lst_Table_GYX[m].logic.u16LogicSize = br.ReadUInt16();
                    Fwt.lst_Table_GYX[m].logic.u16ZhyxChannel = br.ReadUInt16();
                    Fwt.lst_Table_GYX[m].logic.u16OrNumber = br.ReadUInt16();
                    for (int k = 0; k < Fwt.lst_Table_GYX[m].logic.u16OrNumber; k++)
                    {
                        LogicCfg_Or_t tor = new LogicCfg_Or_t();
                        tor.u16OrNo = br.ReadUInt16();
                        tor.u16OrSize = br.ReadUInt16();
                        tor.u16Res1 = br.ReadUInt16();
                        tor.u16AndNumber = br.ReadUInt16();
                        for (int j = 0; j < tor.u16AndNumber; j++)
                        {//16个字节
                            LogicCfg_And_t tand = new LogicCfg_And_t();
                            tand.u16AndNo = br.ReadUInt16();			//该And表达式的编号
                            tand.u16AndSize = br.ReadUInt16();		//该And表达式的大小
                            tand.u16Data1Input = br.ReadUInt16();
                            tand.u16Data2Input = br.ReadUInt16();
                            tand.u8Data1Attr = br.ReadByte();			//0:输入为通道号;1:输入为常数
                            tand.u8Data2Attr = br.ReadByte();			//0:输入为通道号;1:输入为常数
                            tand.u8ResultAttr = br.ReadByte();			//0:输出不作处理;1:输出取反
                            tand.u8Opt = br.ReadByte();				    //0:等于;1:小于;2:大于
                            tand.u8Res[0] = br.ReadByte();
                            tand.u8Res[1] = br.ReadByte();
                            tand.u8Res[2] = br.ReadByte();
                            tand.u8Res[3] = br.ReadByte();
                            tor.lstAnd.Add(tor.lstAnd.Count, tand);
                        }
                        Fwt.lst_Table_GYX[m].logic.lstOr.Add(Fwt.lst_Table_GYX[m].logic.lstOr.Count, tor);
                    }
                }
                //----

                br.Close();
                fs.Close();
            }
        }
        //---------------------------
    }
}
