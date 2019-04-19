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
using System.Threading;

namespace CfgTool
{
    public partial class Form_AutoCfgSet : Form
    {
        Form_CfgTool pParent;
        public Form_AutoCfgSet(Form_CfgTool p)
        {
            InitializeComponent();
            pParent = p;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form_AutoCfgSet_Load(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
            radioButton7.Checked = true;
            tbServerIP.Text = "192.168.1.150";
            tbClientIP.Text = "192.168.1.151";
            CenterToScreen();

            checkBox1.Checked = true;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string str = "";
            pParent.gAutoCfgFlag = true;
            if (radioButton1.Checked == true)
            {
                pParent.giComId = -1;
            }
            else if (radioButton2.Checked == true)
            {
                pParent.giComId = 2;
            }
            else if (radioButton3.Checked == true)
            {
                pParent.giComId = 3;
            }
            else if (radioButton4.Checked == true)
            {
                pParent.giComId = 4;
            }
            else if (radioButton5.Checked == true)
            {
                pParent.giComId = 5;
            }
            else
            {
                pParent.giComId = -1;
            }

            if (radioButton6.Checked == true)
            {
                pParent.giNetId = -1;
            }
            else if (radioButton7.Checked == true)
            {
                pParent.giNetId = 7;
            }
            else if (radioButton8.Checked == true)
            {
                pParent.giNetId = 8;
            }
            else if (radioButton9.Checked == true)
            {
                pParent.giNetId = 9;
            }
            else
            {
                pParent.giNetId = -1;
            }
            
            //----
            pParent.gbComChecked[0] = checkBox1.Checked;
            pParent.gbComChecked[1] = checkBox2.Checked;
            pParent.gbComChecked[2] = checkBox3.Checked;
            pParent.gbComChecked[3] = checkBox4.Checked;
            pParent.giComType[0] = comboBox1.SelectedIndex;
            pParent.giComType[1] = comboBox2.SelectedIndex;
            pParent.giComType[2] = comboBox3.SelectedIndex;
            pParent.giComType[3] = comboBox4.SelectedIndex;
            //----

            pParent.gbCopyFileFlag = cbCopyFile.Checked;

            try
            {
                str = tbServerIP.Text;
                string[] sa = str.Split('.');
                for (int m = 0; m < 4;m++ )
                {
                    pParent.gbytServerIP[m] = byte.Parse(sa[m]);
                }

                str = tbClientIP.Text;
                string[] sa2 = str.Split('.');
                for (int m = 0; m < 4; m++)
                {
                    pParent.gbytClientIP[m] = byte.Parse(sa2[m]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            pParent.gAutoCfgFlag = false;
            this.Close();
        }

        private void lblPath_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string strBaseFolder = "D:\\CfgFile";
            string strBaseFolderServer = string.Format("{0}\\Server", strBaseFolder);
            string strBaseFolderClient = string.Format("{0}\\Client", strBaseFolder);

            if (false == Directory.Exists(strBaseFolder)) { Directory.CreateDirectory(strBaseFolder); }
            if (false == Directory.Exists(strBaseFolderServer)) { Directory.CreateDirectory(strBaseFolderServer); }
            if (false == Directory.Exists(strBaseFolderClient)) { Directory.CreateDirectory(strBaseFolderClient); }

            System.Diagnostics.Process.Start("explorer.exe", strBaseFolder);
        }
    }
}
