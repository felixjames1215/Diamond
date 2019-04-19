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
    public partial class Form_Table_FWT_CopyFlagSet : Form
    {
        Form_Table_FWT pParent = null;
        public Form_Table_FWT_CopyFlagSet(Form_Table_FWT p)
        {
            InitializeComponent();
            pParent = p;
        }

        private void Form_Table_FWT_CopyFlagSet_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            cb_YC.Checked = true;
            cb_SYX.Checked = true;
            cb_DYX.Checked = true;
            cb_YK.Checked = true;
            cb_Meter.Checked = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            pParent.setCopyFlag(cb_YC.Checked,
                                cb_SYX.Checked,
                                cb_DYX.Checked,
                                cb_YK.Checked,
                                cb_Meter.Checked);
            this.Close();
        }
    }
}
