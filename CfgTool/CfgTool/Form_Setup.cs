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
using System.Runtime.InteropServices;

namespace CfgTool
{
    public partial class Form_Setup : Form
    {
        public Form_Setup()
        {
            InitializeComponent();
        }

        private void Form_Setup_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            tb_SelfName.Text = Global.g_strSelfName;

            cb_SoftYK_1.Checked = Global.g_bSoftYk_1;
            cb_SoftYK_2.Checked = Global.g_bSoftYk_2;
        }

        private bool checkDeviceName(string selfname)
        {
            bool res = false;
            foreach(var t in Global.g_list_DeviceTable.Values)
            {
                foreach(var t2 in t.lst_Device.Values)
                {
                    if (t2.DeviceName == selfname)
                    {
                        return true;
                    }
                }
            }
            return res;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            Global.g_bSoftYk_1 = cb_SoftYK_1.Checked;
            Global.g_bSoftYk_2 = cb_SoftYK_2.Checked;

            string strSelfName = tb_SelfName.Text;
            if (true == checkDeviceName(strSelfName))
            {
                MessageBox.Show("与某个设备名称同名，请重新设置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //tb_SelfName.Text = Global.g_strSelfName;
                tb_SelfName.Focus();
                tb_SelfName.HideSelection = true;
                return;
            }
            //----
            foreach(var t in Global.g_list_FWT.Values)
            {
                foreach(var obj in t.lst_Table_YC_1.Values)
                {
                    if (obj.DeviceName == Global.g_strSelfName)
                    {
                        obj.DeviceName = strSelfName;
                    }
                }
                foreach (var obj in t.lst_Table_YC_2.Values)
                {
                    if (obj.DeviceName == Global.g_strSelfName)
                    {
                        obj.DeviceName = strSelfName;
                    }
                }
                //----------------------------------------------------------------
                foreach (var obj in t.lst_Table_SYX_1.Values)
                {
                    if (obj.DeviceName == Global.g_strSelfName)
                    {
                        obj.DeviceName = strSelfName;
                    }
                }
                foreach (var obj in t.lst_Table_SYX_2.Values)
                {
                    if (obj.DeviceName == Global.g_strSelfName)
                    {
                        obj.DeviceName = strSelfName;
                    }
                }
                //----------------------------------------------------------------
                foreach (var obj in t.lst_Table_DYX_1.Values)
                {
                    if (obj.DeviceName == Global.g_strSelfName)
                    {
                        obj.DeviceName = strSelfName;
                    }
                }
                foreach (var obj in t.lst_Table_DYX_2.Values)
                {
                    if (obj.DeviceName == Global.g_strSelfName)
                    {
                        obj.DeviceName = strSelfName;
                    }
                }
                //----------------------------------------------------------------
                foreach (var obj in t.lst_Table_YK_1.Values)
                {
                    if (obj.DeviceName == Global.g_strSelfName)
                    {
                        obj.DeviceName = strSelfName;
                    }
                }
                foreach (var obj in t.lst_Table_YK_2.Values)
                {
                    if (obj.DeviceName == Global.g_strSelfName)
                    {
                        obj.DeviceName = strSelfName;
                    }
                }
            }
            //----
            Global.g_strSelfName = strSelfName;
            //----
            this.Close();
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbButton_GYkCfg_Click(object sender, EventArgs e)
        {
            string path = System.Environment.CurrentDirectory + @"\Binary";
            SaveFileDialog fd = new SaveFileDialog();
            fd.Title = "生成YK配置文件";
            fd.FileName = "YK";
            //fd.InitialDirectory = path;
            fd.Filter = "二进制文件(*.cfg)|*.cfg";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string strres = "";
                write_Cfg_YK(fd.FileName, ref strres);
                fd.InitialDirectory = fd.FileName.Substring(0, fd.FileName.LastIndexOf("\\") + 1);
                MessageBox.Show(strres);
            }
        }

        bool write_Cfg_YK(string filename, ref string strres)
        {
            try
            {
                FileStream fs = new FileStream(filename, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                //---------
                bw.Write(Convert.ToByte(Global.g_bSoftYk_1));
                bw.Write(Convert.ToByte(Global.g_bSoftYk_2));

                Form_CfgTool.pMainForm.formInfo.LogMessage(string.Format("保存的YK文件[{0}]大小：{1}字节", filename, fs.Length));
                //---------
                bw.Close();
                fs.Close();
                //MessageBox.Show(string.Format("保存[{0}]成功！", filename), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                strres = string.Format("保存[{0}]成功！", filename);

                //----
                String strTemp = System.AppDomain.CurrentDomain.BaseDirectory + "Config.ini";
                iniRW writeIni = new iniRW(strTemp);
                writeIni.WriteInteger("YkCfg", "YF", (Global.g_bSoftYk_1 == false) ? 0 : 1);
                writeIni.WriteInteger("YkCfg", "YB", (Global.g_bSoftYk_2 == false) ? 0 : 1);
                //----
                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message.ToString());
                strres = string.Format("保存[{0}]失败...失败原因：{1}", filename, ex.Message.ToString());
                return false;
            }
        }

        private void cb_SoftYK_1_CheckedChanged(object sender, EventArgs e)
        {
            Global.g_bSoftYk_1 = cb_SoftYK_1.Checked;
        }

        private void cb_SoftYK_2_CheckedChanged(object sender, EventArgs e)
        {
            Global.g_bSoftYk_2 = cb_SoftYK_2.Checked;
        }
        //END
    }
}
