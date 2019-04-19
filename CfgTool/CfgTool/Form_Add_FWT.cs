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
    public partial class Form_Add_FWT : Form
    {
        Form_CfgTool pParent = null;
        public Form_Add_FWT(Form_CfgTool p)
        {
            InitializeComponent();
            pParent = p;
        }

        private void Form_Add_FDB_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            tb_FDBName.Text = string.Format("转发表_{0}", GetSuffixId()/*CFWT.Accu*/);
        }

        private int GetSuffixId()
        {
            int sid = 0;
            try
            {
                foreach (var dt in Global.g_list_FWT)
                {
                    if (dt.Value.FWTName.Contains("转发表_"))
                    {
                        string[] sa = dt.Value.FWTName.Split('_');
                        int id = Convert.ToInt32(sa[1]);
                        if (id >= sid)
                        {
                            sid = id;
                        }
                    }
                }
            }
            catch
            {
                ;
            }
            return (sid + 1);
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            string s1 = tb_FDBName.Text.Trim();
            if (s1 == "")
            {
                MessageBox.Show("请输入转发表名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //----
            foreach (var dt in Global.g_list_FWT)
            {
                if (dt.Value.FWTName == s1)
                {
                    MessageBox.Show("重复转发表名称，请重新命名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tb_FDBName.Focus();
                    tb_FDBName.SelectionStart = 0;
                    tb_FDBName.SelectionLength = tb_FDBName.Text.Length;
                    return;
                }
            }
            //----
            CFWT obj = new CFWT();
            obj.Id = CFWT.Accu;
            CFWT.Accu += 1;
            obj.FWTName = s1;
            //----
            obj.sourceClone();
            //----

            pParent.formInfo.LogMessage(string.Format("新增转发表[编号：{0}，转发表名称：{1}]",
                                                                   obj.Id, obj.FWTName));
            Global.g_list_FWT.Add(obj.Id, obj);
            Global.sorted_list_FWT();
            pParent.addNode_FWT(obj);
            this.Close();
        }
    }
}
