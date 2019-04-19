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
    public partial class Form_Add_Device : Form
    {
        Form_CfgTool pParent = null;
        CDeviceTable DeviceTable = null;

        public Form_Add_Device(Form_CfgTool p, CDeviceTable dt)
        {
            InitializeComponent();
            pParent = p;
            DeviceTable = dt;
        }

        private void Form_Device_Add_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            tb_DeviceName.Text = string.Format("设备_{0}", CDevice.Accu);
            initModel();
            tb_CommAddr.Text = getDefaultCommAddr();
        }

        void initModel()
        {
            foreach(var t in Global.g_list_Model)
            {
                cmb_Model.Items.Add(t.Value.ModelName);
            }
            if(cmb_Model.Items.Count > 0)
            {
                cmb_Model.SelectedIndex = 0;
            }
        }

        string getDefaultCommAddr()
        {
            int res = 0;
            foreach(var t in Global.g_list_DeviceTable)
            {
                foreach(var dev in t.Value.lst_Device)
                {
                    if (dev.Value.CommAddr >= res)
                    {
                        res = dev.Value.CommAddr;
                    }
                }
            }
            return (res + 1).ToString();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            string s1 = tb_DeviceName.Text.Trim();
            string s2 = cmb_Model.Text.Trim();
            string s3 = tb_CommAddr.Text.Trim();
            if (s1 == "")
            {
                MessageBox.Show("请输入设备名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (s2 == "")
            {
                MessageBox.Show("请选择模板！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (s3 == "")
            {
                MessageBox.Show("请输入设备通信地址！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            CDevice obj = new CDevice();
            obj.Id = CDevice.Accu;
            CDevice.Accu += 1;
            obj.DeviceName = s1;
            obj.ModelName = s2;
            obj.CommAddr = Convert.ToInt32(s3);
            pParent.formInfo.LogMessage(string.Format("新增设备[编号：{0}，设备名称：{1}，模板名称：{2}，通信地址：{3}]",
                                                       obj.Id, obj.DeviceName, obj.ModelName, obj.CommAddr));
            DeviceTable.lst_Device.Add(obj.Id, obj);
            pParent.addNode_Device(DeviceTable, obj);
            this.Close();
        }
    }
}
