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
    public partial class Form_Info : Form
    {
        public Form_Info()
        {
            InitializeComponent();
        }

        private void Form_Info_Load(object sender, EventArgs e)
        {
            this.KeyPreview = true;
        }

        private void Form_Info_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Visible = false;
            e.Cancel = true;
        }

        #region 日志记录、支持其他线程访问
        public delegate void LogAppendDelegate(Color color, string text);
        /// <summary> 
        /// 追加显示文本 
        /// </summary> 
        /// <param name="color">文本颜色</param> 
        /// <param name="text">显示文本</param> 
        public void LogAppend(Color color, string text)
        {
            rtb.AppendText("\n");
            rtb.SelectionColor = color;
            rtb.AppendText(text);
        }
        /// <summary> 
        /// 显示错误日志 
        /// </summary> 
        /// <param name="text"></param> 
        public void LogError(string text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
            rtb.Invoke(la, Color.Red, DateTime.Now.ToString("HH:mm:ss ") + text);
        }
        /// <summary> 
        /// 显示警告信息 
        /// </summary> 
        /// <param name="text"></param> 
        public void LogWarning(string text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
            rtb.Invoke(la, Color.Violet, DateTime.Now.ToString("HH:mm:ss ") + text);
        }
        /// <summary> 
        /// 显示信息 
        /// </summary> 
        /// <param name="text"></param> 
        public void LogMessage(string text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
            rtb.Invoke(la, Color.Black, DateTime.Now.ToString("HH:mm:ss ") + text);
            //rtb.Invoke(la, Color.Black, text);////
        }

        public void LogMessage2(string text)
        {
            LogAppendDelegate la = new LogAppendDelegate(LogAppend);
            //rtb.Invoke(la, Color.Black, DateTime.Now.ToString("HH:mm:ss ") + text);
            rtb.Invoke(la, Color.Black, text);////
        }
        #endregion

        private void tsmi_Clear_Click(object sender, EventArgs e)
        {
            rtb.Text = "";
        }

        private void Form_Info_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Control) && e.KeyCode == Keys.S)//显示/隐藏日志信息窗口
            {
                if (this.Visible == true)
                {
                    this.Visible = false;
                }
                else
                {
                    this.Visible = true;
                }
            }
        }

    }
}
