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
    public partial class Form_Table_RTDB : Form
    {
        Color selfClr = Color.AliceBlue;//本体背景色，以示区分
        //重新生成实时库：遍历被配置对象和设备集，将遥测、单点遥信、双点遥信、遥控、计量值重新汇总，形成实时库。
        public Form_Table_RTDB()
        {
            InitializeComponent();
        }

        private void Form_Table_RealData_Load(object sender, EventArgs e)
        {
            set_dgv_Property();
            //----
            init_View_YC();
            init_View_SYX();
            init_View_DYX();
            init_View_YK();
            init_View_Meter();
        }

        void set_dgv_Property()
        {
            dgv_YC.ReadOnly = true;
            dgv_SYX.ReadOnly = true;
            dgv_DYX.ReadOnly = true;
            dgv_YK.ReadOnly = true;
            dgv_Meter.ReadOnly = true;
        }

        private void init_View_YC()
        {
            int iPos = 0;
            int iCount = 0;
            //1/2.被配置对象选择的模板
            foreach (var t in Global.g_Model.lst_Table_YC.Values)
            {
                dgv_YC.RowCount += 1;
                iPos = 0;
                dgv_YC.Rows[iCount].Cells[iPos++].Value = iCount + 1;
                dgv_YC.Rows[iCount].Cells[iPos++].Value = t.Id;
                dgv_YC.Rows[iCount].Cells[iPos++].Value = Global.g_strSelfName;
                dgv_YC.Rows[iCount].Cells[iPos++].Value = Global.g_Model.ModelName;
                dgv_YC.Rows[iCount].Cells[iPos++].Value = t.GroupName;
                dgv_YC.Rows[iCount].Cells[iPos++].Value = t.ItemName;
                dgv_YC.Rows[iCount].Cells[iPos++].Value = t.DataType;
                dgv_YC.Rows[iCount].Cells[iPos++].Value = t.Unit;
                dgv_YC.Rows[iCount].Cells[iPos++].Value = t.Ratio;
                dgv_YC.Rows[iCount].Cells[iPos++].Value = t.iGroup;
                dgv_YC.Rows[iCount].Cells[iPos++].Value = getHex(t.Addr);
                dgv_YC.Rows[iCount].DefaultCellStyle.BackColor = selfClr;
                iCount += 1;
            }
            //2/2.设备集中的选择的模板
            foreach (var k in Global.g_list_DeviceTable.Values)
            {
                foreach (var dev in k.lst_Device.Values)
                {
                    foreach (var t in Global.g_list_Model.Values)
                    {
                        if (dev.ModelName == t.ModelName)
                        {
                            foreach (var obj in t.lst_Table_YC.Values)
                            {
                                dgv_YC.RowCount += 1;
                                iPos = 0;
                                dgv_YC.Rows[iCount].Cells[iPos++].Value = iCount + 1;
                                dgv_YC.Rows[iCount].Cells[iPos++].Value = obj.Id;
                                dgv_YC.Rows[iCount].Cells[iPos++].Value = dev.DeviceName;
                                dgv_YC.Rows[iCount].Cells[iPos++].Value = dev.ModelName;
                                dgv_YC.Rows[iCount].Cells[iPos++].Value = obj.GroupName;
                                dgv_YC.Rows[iCount].Cells[iPos++].Value = obj.ItemName;
                                dgv_YC.Rows[iCount].Cells[iPos++].Value = obj.DataType;
                                dgv_YC.Rows[iCount].Cells[iPos++].Value = obj.Unit;
                                dgv_YC.Rows[iCount].Cells[iPos++].Value = obj.Ratio;
                                dgv_YC.Rows[iCount].Cells[iPos++].Value = obj.iGroup;
                                dgv_YC.Rows[iCount].Cells[iPos++].Value = getHex(obj.Addr);
                                iCount += 1;
                            }
                            break;
                        }
                    }
                }
            }
        }

        private void init_View_SYX()
        {
            int iPos = 0;
            int iCount = 0;
            //1/2.被配置对象选择的模板
            foreach (var t in Global.g_Model.lst_Table_SYX.Values)
            {
                dgv_SYX.RowCount += 1;
                iPos = 0;
                dgv_SYX.Rows[iCount].Cells[iPos++].Value = iCount + 1;
                dgv_SYX.Rows[iCount].Cells[iPos++].Value = t.Id;
                dgv_SYX.Rows[iCount].Cells[iPos++].Value = Global.g_strSelfName;
                dgv_SYX.Rows[iCount].Cells[iPos++].Value = Global.g_Model.ModelName;
                dgv_SYX.Rows[iCount].Cells[iPos++].Value = t.GroupName;
                dgv_SYX.Rows[iCount].Cells[iPos++].Value = t.ItemName;
                dgv_SYX.Rows[iCount].Cells[iPos++].Value = t.DataType;
                dgv_SYX.Rows[iCount].Cells[iPos++].Value = getHex(t.Addr);
                dgv_SYX.Rows[iCount].DefaultCellStyle.BackColor = selfClr;
                iCount += 1;
            }
            //2/2.设备表集中的模板
            foreach (var k in Global.g_list_DeviceTable.Values)
            {
                foreach(var dev in k.lst_Device.Values)
                {
                    foreach (var t in Global.g_list_Model.Values)
                    {
                        if (dev.ModelName == t.ModelName)
                        {
                            foreach (var obj in t.lst_Table_SYX.Values)
                            {
                                dgv_SYX.RowCount += 1;
                                iPos = 0;
                                dgv_SYX.Rows[iCount].Cells[iPos++].Value = iCount + 1;
                                dgv_SYX.Rows[iCount].Cells[iPos++].Value = obj.Id;
                                dgv_SYX.Rows[iCount].Cells[iPos++].Value = dev.DeviceName;
                                dgv_SYX.Rows[iCount].Cells[iPos++].Value = dev.ModelName;
                                dgv_SYX.Rows[iCount].Cells[iPos++].Value = obj.GroupName;
                                dgv_SYX.Rows[iCount].Cells[iPos++].Value = obj.ItemName;
                                dgv_SYX.Rows[iCount].Cells[iPos++].Value = obj.DataType;
                                dgv_SYX.Rows[iCount].Cells[iPos++].Value = getHex(obj.Addr);
                                iCount += 1;
                            }
                            break;
                        }
                    }
                }
            }
        }

        private void init_View_DYX()
        {
            int iPos = 0;
            int iCount = 0;
            //1/2.被配置对象选择的模板
            foreach (var t in Global.g_Model.lst_Table_DYX.Values)
            {
                dgv_DYX.RowCount += 1;
                iPos = 0;
                dgv_DYX.Rows[iCount].Cells[iPos++].Value = iCount + 1;
                dgv_DYX.Rows[iCount].Cells[iPos++].Value = t.Id;
                dgv_DYX.Rows[iCount].Cells[iPos++].Value = Global.g_strSelfName;
                dgv_DYX.Rows[iCount].Cells[iPos++].Value = Global.g_Model.ModelName;
                dgv_DYX.Rows[iCount].Cells[iPos++].Value = t.GroupName;
                dgv_DYX.Rows[iCount].Cells[iPos++].Value = t.ItemName;
                dgv_DYX.Rows[iCount].Cells[iPos++].Value = t.DataType;
                dgv_DYX.Rows[iCount].Cells[iPos++].Value = getHex(t.Addr);
                dgv_DYX.Rows[iCount].DefaultCellStyle.BackColor = selfClr;
                iCount += 1;
            }
            //2/2.设备集中的模板
            foreach (var k in Global.g_list_DeviceTable.Values)
            {
                foreach(var dev in k.lst_Device.Values)
                {
                    foreach (var t in Global.g_list_Model.Values)
                    {
                        if (dev.ModelName == t.ModelName)
                        {
                            foreach (var obj in t.lst_Table_DYX.Values)
                            {
                                dgv_DYX.RowCount += 1;
                                iPos = 0;
                                dgv_DYX.Rows[iCount].Cells[iPos++].Value = iCount + 1;
                                dgv_DYX.Rows[iCount].Cells[iPos++].Value = obj.Id;
                                dgv_DYX.Rows[iCount].Cells[iPos++].Value = dev.DeviceName;
                                dgv_DYX.Rows[iCount].Cells[iPos++].Value = dev.ModelName;
                                dgv_DYX.Rows[iCount].Cells[iPos++].Value = obj.GroupName;
                                dgv_DYX.Rows[iCount].Cells[iPos++].Value = obj.ItemName;
                                dgv_DYX.Rows[iCount].Cells[iPos++].Value = obj.DataType;
                                dgv_DYX.Rows[iCount].Cells[iPos++].Value = getHex(obj.Addr);
                                iCount += 1;
                            }
                            break;
                        }
                    }
                }
            }
        }

        private void init_View_YK()
        {
            int iPos = 0;
            int iCount = 0;
            //1/2.被配置对象选择的模板
            foreach (var t in Global.g_Model.lst_Table_YK.Values)
            {
                dgv_YK.RowCount += 1;
                iPos = 0;
                dgv_YK.Rows[iCount].Cells[iPos++].Value = iCount + 1;
                dgv_YK.Rows[iCount].Cells[iPos++].Value = t.Id;
                dgv_YK.Rows[iCount].Cells[iPos++].Value = Global.g_strSelfName;
                dgv_YK.Rows[iCount].Cells[iPos++].Value = Global.g_Model.ModelName;
                dgv_YK.Rows[iCount].Cells[iPos++].Value = t.GroupName;
                dgv_YK.Rows[iCount].Cells[iPos++].Value = t.ItemName;
                dgv_YK.Rows[iCount].Cells[iPos++].Value = t.DataType;
                dgv_YK.Rows[iCount].Cells[iPos++].Value = getHex(t.Addr);
                dgv_YK.Rows[iCount].DefaultCellStyle.BackColor = selfClr;
                iCount += 1;
            }
            //2/2.设备集中的模板
            foreach (var k in Global.g_list_DeviceTable.Values)
            {
                foreach(var dev in k.lst_Device.Values)
                {
                    foreach (var t in Global.g_list_Model.Values)
                    {
                        if (dev.ModelName == t.ModelName)
                        {
                            foreach (var obj in t.lst_Table_YK.Values)
                            {
                                dgv_YK.RowCount += 1;
                                iPos = 0;
                                dgv_YK.Rows[iCount].Cells[iPos++].Value = iCount + 1;
                                dgv_YK.Rows[iCount].Cells[iPos++].Value = obj.Id;
                                dgv_YK.Rows[iCount].Cells[iPos++].Value = dev.DeviceName;
                                dgv_YK.Rows[iCount].Cells[iPos++].Value = dev.ModelName;
                                dgv_YK.Rows[iCount].Cells[iPos++].Value = obj.GroupName;
                                dgv_YK.Rows[iCount].Cells[iPos++].Value = obj.ItemName;
                                dgv_YK.Rows[iCount].Cells[iPos++].Value = obj.DataType;
                                dgv_YK.Rows[iCount].Cells[iPos++].Value = getHex(obj.Addr);
                                iCount += 1;
                            }
                            break;
                        }
                    }
                }
            }
        }

        private void init_View_Meter()
        {
            int iPos = 0;
            int iCount = 0;
            //1/2.被配置对象选择的模板
            foreach (var t in Global.g_Model.lst_Table_Meter.Values)
            {
                dgv_Meter.RowCount += 1;
                iPos = 0;
                dgv_Meter.Rows[iCount].Cells[iPos++].Value = iCount + 1;
                dgv_Meter.Rows[iCount].Cells[iPos++].Value = t.Id;
                dgv_Meter.Rows[iCount].Cells[iPos++].Value = Global.g_strSelfName;
                dgv_Meter.Rows[iCount].Cells[iPos++].Value = Global.g_Model.ModelName;
                dgv_Meter.Rows[iCount].Cells[iPos++].Value = t.GroupName;
                dgv_Meter.Rows[iCount].Cells[iPos++].Value = t.ItemName;
                dgv_Meter.Rows[iCount].Cells[iPos++].Value = t.DataType;
                dgv_Meter.Rows[iCount].Cells[iPos++].Value = t.Unit;
                dgv_Meter.Rows[iCount].Cells[iPos++].Value = getHex(t.Addr);
                dgv_Meter.Rows[iCount].DefaultCellStyle.BackColor = selfClr;
                iCount += 1;
            }
            //2/2.设备集中的模板
            foreach (var k in Global.g_list_DeviceTable.Values)
            {
                foreach(var dev in k.lst_Device.Values)
                {
                    foreach (var t in Global.g_list_Model.Values)
                    {
                        if (dev.ModelName == t.ModelName)
                        {
                            foreach (var obj in t.lst_Table_Meter.Values)
                            {
                                dgv_Meter.RowCount += 1;
                                iPos = 0;
                                dgv_Meter.Rows[iCount].Cells[iPos++].Value = iCount + 1;
                                dgv_Meter.Rows[iCount].Cells[iPos++].Value = obj.Id;
                                dgv_Meter.Rows[iCount].Cells[iPos++].Value = dev.DeviceName;
                                dgv_Meter.Rows[iCount].Cells[iPos++].Value = dev.ModelName;
                                dgv_Meter.Rows[iCount].Cells[iPos++].Value = obj.GroupName;
                                dgv_Meter.Rows[iCount].Cells[iPos++].Value = obj.ItemName;
                                dgv_Meter.Rows[iCount].Cells[iPos++].Value = obj.DataType;
                                dgv_Meter.Rows[iCount].Cells[iPos++].Value = obj.Unit;
                                dgv_Meter.Rows[iCount].Cells[iPos++].Value = getHex(obj.Addr);
                                iCount += 1;
                            }
                            break;
                        }
                    }
                }
            }
        }

        private string getHex(int addr)
        {
            return string.Format("0x{0:X}", addr);
        }
    }
}
