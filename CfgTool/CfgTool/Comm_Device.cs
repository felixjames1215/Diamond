using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CfgTool
{
    public class DevCfg_t
    {
        public DevCfg_t()
        {
            u8Used = 0x00;
            for (int k = 0; k < u8Res1.Length; k++) {u8Res1[k] = 0x00; }
            tDevPort = new PORT_T();
            if (tDevPort.NPort.u8IP[3] <= 0xfe) { tDevPort.NPort.u8IP[3] += 1; }
            u32ModelIndex = 0;
            bName = new byte[Global.iniPara_NAME_LEN];
            for (int k = 0; k < bName.Length; k++) { bName[k] = 0x00; }
            sName = "";
        }
        //----
        public byte u8Used;
	    public byte[] u8Res1 = new byte[3];
        public PORT_T tDevPort;//从属端口(地址赋值进去)
        public UInt32 u32ModelIndex;
        public byte[] bName;
        public string sName;
        //----
        public void write_bin(BinaryWriter bw)
        {
            bw.Write(u8Used);
            for (int m = 0; m < u8Res1.Length; m++) { bw.Write(u8Res1[m]); }
            //1、从属端口(合计16字节)
            bw.Write(tDevPort.u16PortAddr);//端口地址
            bw.Write(tDevPort.u8PhyAttr);//物理属性
            bw.Write(tDevPort.u8LogicAttr);//逻辑属性
            if (tDevPort.u8PhyAttr == 0x01)//串口
            {
                bw.Write(tDevPort.SPort.u32BaudRate);//波特率
                bw.Write(tDevPort.SPort.u8DataBit);//数据位
                bw.Write(tDevPort.SPort.u8StopBit);//停止位
                bw.Write(tDevPort.SPort.u8CheckBit);//校验位
                bw.Write(tDevPort.SPort.u8Res1[0]);//占位：1字节
                bw.Write(tDevPort.SPort.u8Res2[0]);//占位：4字节
                bw.Write(tDevPort.SPort.u8Res2[1]);
                bw.Write(tDevPort.SPort.u8Res2[2]);
                bw.Write(tDevPort.SPort.u8Res2[3]);
                bw.Write(tDevPort.SPort.u8Res3[0]);//占位：4字节
                bw.Write(tDevPort.SPort.u8Res3[1]);
                bw.Write(tDevPort.SPort.u8Res3[2]);
                bw.Write(tDevPort.SPort.u8Res3[3]);
            }
            else if (tDevPort.u8PhyAttr == 0x02)//网口
            {
                bw.Write(tDevPort.NPort.u8IP[0]);//IP
                bw.Write(tDevPort.NPort.u8IP[1]);
                bw.Write(tDevPort.NPort.u8IP[2]);
                bw.Write(tDevPort.NPort.u8IP[3]);
                bw.Write(tDevPort.NPort.u8Mask[0]);//子网掩码
                bw.Write(tDevPort.NPort.u8Mask[1]);
                bw.Write(tDevPort.NPort.u8Mask[2]);
                bw.Write(tDevPort.NPort.u8Mask[3]);
                bw.Write(tDevPort.NPort.u8MAC[0]);//占位
                bw.Write(tDevPort.NPort.u8MAC[1]);
                bw.Write(tDevPort.NPort.u8MAC[2]);
                bw.Write(tDevPort.NPort.u8MAC[3]);
                bw.Write(tDevPort.NPort.u8MAC[4]);
                bw.Write(tDevPort.NPort.u8MAC[5]);
                bw.Write(tDevPort.NPort.u8Res1[0]);//占位：2字节
                bw.Write(tDevPort.NPort.u8Res1[1]);
            }
            else
            {
                byte tmp = 0x00;
                bw.Write(tmp);
                bw.Write(tmp);
                bw.Write(tmp);
                bw.Write(tmp);
                bw.Write(tmp);
                bw.Write(tmp);
                bw.Write(tmp);
                bw.Write(tmp);
                bw.Write(tmp);
                bw.Write(tmp);
                bw.Write(tmp);
                bw.Write(tmp);
                bw.Write(tmp);
                bw.Write(tmp);
                bw.Write(tmp);
                bw.Write(tmp);
            }
            bw.Write(u32ModelIndex);
            //使用GB2312编码，一个汉字，占2个字节.jifeng,2017-6-15
            writeChineseString(sName, bw);
        }

        public void read_bin(BinaryReader br)
        {
            u8Used = br.ReadByte();
            for (int m = 0; m < u8Res1.Length; m++)
            {
                u8Res1[m] = br.ReadByte();
            }
            //1、从属端口
            tDevPort.u16PortAddr = br.ReadUInt16();
            tDevPort.u8PhyAttr = br.ReadByte();
            tDevPort.u8LogicAttr = br.ReadByte();
            if (tDevPort.u8PhyAttr == 0x01)//串口
            {
                tDevPort.SPort.u32BaudRate = br.ReadUInt32();//波特率
                tDevPort.SPort.u8DataBit = br.ReadByte();//数据位
                tDevPort.SPort.u8StopBit = br.ReadByte();//停止位
                tDevPort.SPort.u8CheckBit = br.ReadByte();//校验位
                tDevPort.SPort.u8Res1[0] = br.ReadByte();//占位1字节
                tDevPort.SPort.u8Res2[0] = br.ReadByte();//占位4字节
                tDevPort.SPort.u8Res2[1] = br.ReadByte();
                tDevPort.SPort.u8Res2[2] = br.ReadByte();
                tDevPort.SPort.u8Res2[3] = br.ReadByte();
                tDevPort.SPort.u8Res3[0] = br.ReadByte();//占位4字节
                tDevPort.SPort.u8Res3[1] = br.ReadByte();
                tDevPort.SPort.u8Res3[2] = br.ReadByte();
                tDevPort.SPort.u8Res3[3] = br.ReadByte();
            }
            else if (tDevPort.u8PhyAttr == 0x02)//网口
            {
                tDevPort.NPort.u8IP[0] = br.ReadByte();//IP
                tDevPort.NPort.u8IP[1] = br.ReadByte();
                tDevPort.NPort.u8IP[2] = br.ReadByte();
                tDevPort.NPort.u8IP[3] = br.ReadByte();
                tDevPort.NPort.u8Mask[0] = br.ReadByte();//子网掩码
                tDevPort.NPort.u8Mask[1] = br.ReadByte();
                tDevPort.NPort.u8Mask[2] = br.ReadByte();
                tDevPort.NPort.u8Mask[3] = br.ReadByte();
                tDevPort.NPort.u8MAC[0] = br.ReadByte();//MAC
                tDevPort.NPort.u8MAC[1] = br.ReadByte();
                tDevPort.NPort.u8MAC[2] = br.ReadByte();
                tDevPort.NPort.u8MAC[3] = br.ReadByte();
                tDevPort.NPort.u8MAC[4] = br.ReadByte();
                tDevPort.NPort.u8MAC[5] = br.ReadByte();
                tDevPort.NPort.u8Res1[0] = br.ReadByte();//占位
                tDevPort.NPort.u8Res1[1] = br.ReadByte();
            }
            else
            {
                byte bttmp = 0x00;
                bttmp = br.ReadByte();
                bttmp = br.ReadByte();
                bttmp = br.ReadByte();
                bttmp = br.ReadByte();
                bttmp = br.ReadByte();
                bttmp = br.ReadByte();
                bttmp = br.ReadByte();
                bttmp = br.ReadByte();
                bttmp = br.ReadByte();
                bttmp = br.ReadByte();
                bttmp = br.ReadByte();
                bttmp = br.ReadByte();
                bttmp = br.ReadByte();
                bttmp = br.ReadByte();
                bttmp = br.ReadByte();
                bttmp = br.ReadByte();
            }
            u32ModelIndex = br.ReadUInt32();
            string s = Form_CfgTool.pMainForm.readChineseString(br);
            sName = s;
        }

        #region "读GB2312中文字符串(32字节)"
        public string readChineseString(BinaryReader br)
        {
            for (int k = 0; k < bName.Length; k++) { bName[k] = br.ReadByte(); }
            return Encoding.GetEncoding("gb2312").GetString(bName).Replace("\0", "");
        }
        #endregion
        #region "写GB2312中文字符串(32字节)"
        public void writeChineseString(string chinese, BinaryWriter bw)
        {
            Byte[] encodedBytes = Encoding.GetEncoding("gb2312").GetBytes(chinese);
            byte bt = 0x00;
            for (int k = 0; k < Global.iniPara_NAME_LEN; k++)
            {
                if ((k + 1) <= encodedBytes.Length)
                {
                    bw.Write(encodedBytes[k]);
                }
                else
                {
                    bw.Write(bt);
                }
            }
        }
        #endregion
    }

    public class DevsCfg_t
    {
        public DevsCfg_t()
        {
            u32DevNum = 0;
            tDevCfg = new DevCfg_t[Global.iniComm_CN_COMM_DEV_MAX];
            for (int k = 0; k < tDevCfg.Length; k++) { tDevCfg[k] = new DevCfg_t(); }
            bName = new byte[Global.iniPara_NAME_LEN];
            for (int k = 0; k < bName.Length; k++) { bName[k] = 0x00; }
            sName = "";
        }
        //----
        public UInt32 u32DevNum;//设备数目
        public DevCfg_t[] tDevCfg;//设备数组
        //设备表名称
        public byte[] bName;
        public string sName;
        //----
        public void write_bin(BinaryWriter bw)
        {
            bw.Write(u32DevNum);
            for (int m = 0; m < Global.iniComm_CN_COMM_DEV_MAX/*u32DevNum*/; m++)
            {
                tDevCfg[m].write_bin(bw);
            }
            //使用GB2312编码，一个汉字，占2个字节.jifeng,2017-6-15
            writeChineseString(sName, bw);
        }

        public void read_bin(BinaryReader br)
        {
            u32DevNum = br.ReadUInt32();
            for (int m = 0; m < Global.iniComm_CN_COMM_DEV_MAX/*u32DevNum*/; m++)
            {
                tDevCfg[m].read_bin(br);
            }
            sName = readChineseString(br).Replace("\0", "");
        }

        #region "读GB2312中文字符串(32字节)"
        public string readChineseString(BinaryReader br)
        {
            for (int k = 0; k < bName.Length; k++) { bName[k] = br.ReadByte(); }
            return Encoding.GetEncoding("gb2312").GetString(bName).Replace("\0", "");
        }
        #endregion
        #region "写GB2312中文字符串(32字节)"
        public void writeChineseString(string chinese, BinaryWriter bw)
        {
            Byte[] encodedBytes = Encoding.GetEncoding("gb2312").GetBytes(chinese);
            byte bt = 0x00;
            for (int k = 0; k < Global.iniPara_NAME_LEN; k++)
            {
                if ((k + 1) <= encodedBytes.Length)
                {
                    bw.Write(encodedBytes[k]);
                }
                else
                {
                    bw.Write(bt);
                }
            }
        }
        #endregion
    }

    public class DevsCfgFile_t
    {
        public DevsCfgFile_t()
        {
            u32DevsNum = 0;
            tDevsCfg = new DevsCfg_t[Global.iniComm_CN_COMM_DEVS_MAX];
            for (int k = 0; k < tDevsCfg.Length; k++)
            {
                tDevsCfg[k] = new DevsCfg_t();
            }
        }
        //----
        public UInt32 u32DevsNum;//设备表数目
	    public DevsCfg_t[] tDevsCfg;//设备表数组
        //----
        public void formDevCfg()
        {
            //----设备表数目
            u32DevsNum = Convert.ToUInt32(Global.g_list_DeviceTable.Count);
            int iCount = 0;
            foreach(var dt in Global.g_list_DeviceTable.Values)
            {
                //----设备数目
                tDevsCfg[iCount].u32DevNum = Convert.ToUInt32(dt.lst_Device.Count);
                //----设备数组
                int iCount2 = 0;
                foreach(var dev in dt.lst_Device.Values)
                {
                    tDevsCfg[iCount].tDevCfg[iCount2].u8Used = getDeviceUsed(dt.DeviceTableName);//是否使用
                    tDevsCfg[iCount].tDevCfg[iCount2].tDevPort = getPort(dt.DeviceTableName);//端口
                    tDevsCfg[iCount].tDevCfg[iCount2].tDevPort.u16PortAddr = Convert.ToUInt16(dev.CommAddr);//地址
                    tDevsCfg[iCount].tDevCfg[iCount2].u32ModelIndex = getModelIndex(dev.ModelName);//模板Id
                    tDevsCfg[iCount].tDevCfg[iCount2].sName = dev.DeviceName;////设备名称，jifeng，2017-6-15
                    //----
                    iCount2 += 1;
                }
                //----设备表名称
                tDevsCfg[iCount].sName = dt.DeviceTableName;//新增，jifeng，2017-6-15
                //----
                iCount += 1;
            }
        }

        public void restoreFromCfg()
        {
            Global.g_list_DeviceTable.Clear();
            CDevice.Accu = 1;
            CDeviceTable.Accu = 1;
            //----
            for (int m = 0; m < u32DevsNum; m++)
            {
                CDeviceTable dt = new CDeviceTable();
                dt.Id = CDeviceTable.Accu;
                CDeviceTable.Accu += 1;
                dt.DeviceTableName = tDevsCfg[m].sName;
                for (int k = 0; k < tDevsCfg[m].u32DevNum; k++)
                {
                    CDevice dev = new CDevice();
                    dev.Id = CDevice.Accu;
                    CDevice.Accu += 1;
                    dev.DeviceName = tDevsCfg[m].tDevCfg[k].sName;
                    dev.ModelName = getModelName(tDevsCfg[m].tDevCfg[k].u32ModelIndex);
                    dt.lst_Device.Add(dev.Id, dev);
                }
                Global.g_list_DeviceTable.Add(dt.Id, dt);
            }
        }

        byte getDeviceUsed(string devicetablename)
        {
            byte res = 0x00;
            //----
            foreach(var p in Global.g_Model.lst_Table_Port.Values)
            {
                if (p.DeviceTableName == devicetablename)
                {
                    res = 0x01;
                    break;
                }
            }
            //----
            return res;
        }
        PORT_T getPort(string devicetablename)
        {
            PORT_T port = new PORT_T();
            //----
            foreach (var p in Global.g_Model.lst_Table_Port.Values)
            {
                if (p.DeviceTableName == devicetablename && p.Enabled == true) //add “p.Enabled == true”， jifeng， 2018-6-2 14:49
                {
                    port = p.cfg_Port.tPort.MyClone();
                    port.NPort.u8IP[3] += 1;//IP地址第四个字节，加1
                    break;
                }
            }
            //----
            return port;
        }
        UInt32 getModelIndex(string modelname)
        {
            UInt32 id = 0;
            //----
            foreach(var t in Global.g_list_Model)
            {
                if(t.Value.ModelName == modelname)
                {
                    id = Convert.ToUInt32(t.Value.SN);
                    break;
                }
            }
            //----
            return id;
        }
        string getModelName(UInt32 modelindex)
        {
            string modelname = "";
            foreach(var t in Global.g_list_Model.Values)
            {
                if(t.SN == modelindex)
                {
                    modelname = t.ModelName;
                    break;
                }
            }
            return modelname;
        }

        public void write_bin(BinaryWriter bw)
        {
            bw.Write(u32DevsNum);
            for (int m = 0; m < u32DevsNum; m++)
            {
                tDevsCfg[m].write_bin(bw);
            }
        }

        public void read_bin(BinaryReader br)
        {
            u32DevsNum = br.ReadUInt32();
            for (int m = 0; m < u32DevsNum; m++)
            {
                tDevsCfg[m].read_bin(br);
            }
        }
    }
}
