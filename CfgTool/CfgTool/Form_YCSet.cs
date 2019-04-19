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
    public partial class Form_YCSet : Form
    {
        CFWT fwt;
        public Form_YCSet(ref CFWT f)
        {
            InitializeComponent();
            fwt = f;
        }

        private void Form_YCSet_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("[{0}]遥测最大值和系数", fwt.FWTName);
            init_Para();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            set_Para();
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void init_Para()
        {
            tb_I_MAX.Text = fwt.I_MAX.ToString();
            tb_V_MAX.Text = fwt.V_MAX.ToString();
            tb_DC_MAX.Text = fwt.DC_MAX.ToString();
            tb_P_MAX.Text = fwt.P_MAX.ToString();
            tb_FR_MAX.Text = fwt.FR_MAX.ToString();
            tb_COS_MAX.Text = fwt.COS_MAX.ToString();
            //----
            tb_I_COE.Text = fwt.I_COE.ToString();
            tb_V_COE.Text = fwt.V_COE.ToString();
            tb_DC_COE.Text = fwt.DC_COE.ToString();
            tb_P_COE.Text = fwt.P_COE.ToString();
            tb_FR_COE.Text = fwt.FR_COE.ToString();
            tb_COS_COE.Text = fwt.COS_COE.ToString();
        }

        void set_Para()
        {
            int I_MAX = Int32.Parse(tb_I_MAX.Text);
            int V_MAX = Int32.Parse(tb_V_MAX.Text);
            int DC_MAX = Int32.Parse(tb_DC_MAX.Text);
            int P_MAX = Int32.Parse(tb_P_MAX.Text);
            int FR_MAX = Int32.Parse(tb_FR_MAX.Text);
            int COS_MAX = Int32.Parse(tb_COS_MAX.Text);
            //----
            if (fwt.I_MAX != I_MAX) { fwt.I_MAX = I_MAX; }
            if (fwt.V_MAX != V_MAX) { fwt.V_MAX = V_MAX; }
            if (fwt.DC_MAX != DC_MAX) { fwt.DC_MAX = DC_MAX; }
            if (fwt.P_MAX != P_MAX) { fwt.P_MAX = P_MAX; }
            if (fwt.FR_MAX != FR_MAX) { fwt.FR_MAX = FR_MAX; }
            if (fwt.COS_MAX != COS_MAX) { fwt.COS_MAX = COS_MAX; }
            //---
            int I_COE = Int32.Parse(tb_I_COE.Text);
            int V_COE = Int32.Parse(tb_V_COE.Text);
            int DC_COE = Int32.Parse(tb_DC_COE.Text);
            int P_COE = Int32.Parse(tb_P_COE.Text);
            int FR_COE = Int32.Parse(tb_FR_COE.Text);
            int COS_COE = Int32.Parse(tb_COS_COE.Text);
            //----
            if (fwt.I_COE != I_COE) { fwt.I_COE = I_COE; }
            if (fwt.V_COE != V_COE) { fwt.V_COE = V_COE; }
            if (fwt.DC_COE != DC_COE) { fwt.DC_COE = DC_COE; }
            if (fwt.P_COE != P_COE) { fwt.P_COE = P_COE; }
            if (fwt.FR_COE != FR_COE) { fwt.FR_COE = FR_COE; }
            if (fwt.COS_COE != COS_COE) { fwt.COS_COE = COS_COE; }
        }
        //----END
    }
}
