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
    public partial class Form_Add_DeviceTable : Form
    {
        Form_CfgTool pParent = null;

        public Form_Add_DeviceTable(Form_CfgTool p)
        {
            InitializeComponent();
            pParent = p;
        }

        private void Form_Add_DeviceTable_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            tb_DeviceTableName.Text = string.Format("设备表_{0}", GetSuffixId()/*CDeviceTable.Accu*/);
        }

        private int GetSuffixId()
        {
            int sid = 0;
            try
            {
                foreach (var dt in Global.g_list_DeviceTable)
                {
                    if (dt.Value.DeviceTableName.Contains("设备表_"))
                    {
                        string[] sa = dt.Value.DeviceTableName.Split('_');
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
            string s1 = tb_DeviceTableName.Text.Trim();
            if (s1 == "")
            {
                MessageBox.Show("请输入设备表名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //----
            foreach(var dt in Global.g_list_DeviceTable)
            {
                if(dt.Value.DeviceTableName == s1)
                {
                    MessageBox.Show("重复设备表名称，请重新命名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    tb_DeviceTableName.Focus();
                    tb_DeviceTableName.SelectionStart = 0;
                    tb_DeviceTableName.SelectionLength = tb_DeviceTableName.Text.Length;
                    return;
                }
            }
            //----
            CDeviceTable obj = new CDeviceTable();
            obj.Id = CDeviceTable.Accu;
            CDeviceTable.Accu += 1;
            obj.DeviceTableName = s1;
            //----
            pParent.formInfo.LogMessage(string.Format("新增设备表[编号：{0}，设备表名称：{1}]",
                                                                   obj.Id, obj.DeviceTableName));
            Global.g_list_DeviceTable.Add(obj.Id, obj);
            pParent.addNode_DeviceTable(obj);
            this.Close();
        }

    }
}
