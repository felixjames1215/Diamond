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
    public partial class Form_Table_FWT_SNString : Form
    {
        Form_Table_FWT_Name pParent = null;
        eFwtType FwtType;
        string strSN = "";
        public Form_Table_FWT_SNString(Form_Table_FWT_Name p, eFwtType eft, string s)
        {
            InitializeComponent();
            pParent = p;
            FwtType = eft;
            strSN = s;
        }

        private void Form_Table_FWT_SNString_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("总序号字符串[{0}]", GetFwtType());
            this.tbIDString.Text = strSN;
        }

        private string GetFwtType()
        {
            string res = "Unkown";
            if(FwtType == eFwtType.E_FWT_TYPE_YC)
            {
                res = "YC";
            }
            else if (FwtType == eFwtType.E_FWT_TYPE_SYX)
            {
                res = "SYX";
            }
            else if (FwtType == eFwtType.E_FWT_TYPE_DYX)
            {
                res = "DYX";
            }
            else if (FwtType == eFwtType.E_FWT_TYPE_YK)
            {
                res = "YK";
            }
            else if (FwtType == eFwtType.E_FWT_TYPE_Meter)
            {
                res = "Meter";
            }
            return res;
        }
    }
}
