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
    public partial class Form_Table_FWT_FastDrag : Form
    {
        Form_Table_FWT_Name pParent = null;
        eFwtType FwtType;
        public Form_Table_FWT_FastDrag(Form_Table_FWT_Name p, eFwtType eft)
        {
            InitializeComponent();
            pParent = p;
            FwtType = eft;
        }

        private void Form_Table_FWT_FastDrag_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("转发表快捷配置[{0}]", GetFwtType());
            this.CenterToParent();
        }

        private void btnFastDrag_Click(object sender, EventArgs e)
        {
            try
            {
                string str = tbIDString.Text.Trim();
                if (str == "") { return; };

                if (FwtType == eFwtType.E_FWT_TYPE_YC)
                {
                    pParent.FastDrag_YC(str);
                }
                else if (FwtType == eFwtType.E_FWT_TYPE_SYX)
                {
                    pParent.FastDrag_SYX(str);
                }
                else if (FwtType == eFwtType.E_FWT_TYPE_DYX)
                {
                    pParent.FastDrag_DYX(str);
                }
                else if (FwtType == eFwtType.E_FWT_TYPE_YK)
                {
                    pParent.FastDrag_YK(str);
                }
                else if (FwtType == eFwtType.E_FWT_TYPE_Meter)
                {
                    pParent.FastDrag_Meter(str);
                }
            }
            catch
            {
            }
        }

        private string GetFwtType()
        {
            string res = "Unkown";
            if (FwtType == eFwtType.E_FWT_TYPE_YC)
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
