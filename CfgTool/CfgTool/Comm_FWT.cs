using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CfgTool
{
    public enum FwtYcGroup_e
    {
        EN_FWT_YCGROUP_START = 0,
        EN_FWT_YCGROUP_I = EN_FWT_YCGROUP_START, //电流
        EN_FWT_YCGROUP_V,                        //交流电压
        EN_FWT_YCGROUP_DC,                       //直流电压
        EN_FWT_YCGROUP_P,                        //功率
        EN_FWT_YCGROUP_FR,                       //频率
        EN_FWT_YCGROUP_COS,                      //功率因数
        EN_FWT_YCGROUP_END
    }

    public class FwtCfg_t
    {
        //转发表名称
        public byte[] bName = new byte[Global.iniPara_NAME_LEN];
        public string sName = "";
        //----数量
        public UInt32 u32YcNum;
        public UInt32 u32SyxNum;
        public UInt32 u32DyxNum;
        public UInt32 u32YkNum;
        public UInt32 u32PowNum;
        //----信息对象地址
        public UInt32 u32YcStart;
        public UInt32 u32SyxStart;
        public UInt32 u32DyxStart;
        public UInt32 u32YkStart;
        public UInt32 u32PowStart;
        //----存的在实时库中的序号
        public UInt32[] u32YcIndex = new UInt32[Global.iniComm_CN_COMM_FWT_YC_MAX];
        public UInt32[] u32SyxIndex = new UInt32[Global.iniComm_CN_COMM_FWT_SYX_MAX];
        public UInt32[] u32DyxIndex = new UInt32[Global.iniComm_CN_COMM_FWT_DYX_MAX];
        public UInt32[] u32YkIndex = new UInt32[Global.iniComm_CN_COMM_FWT_YK_MAX];
        public UInt32[] u32PowIndex = new UInt32[Global.iniComm_CN_COMM_FWT_POW_MAX];
        //----
        public UInt32[] u32YcGroup = new UInt32[Global.iniComm_CN_COMM_FWT_YC_MAX];
        //新增单点遥信和双点遥信地址，jifeng，20170911
        public UInt32[] u32SyxAddr = new UInt32[Global.iniComm_CN_COMM_FWT_SYX_MAX];
        public UInt32[] u32DyxAddr = new UInt32[Global.iniComm_CN_COMM_FWT_DYX_MAX];

        //CN_FWT_YCGROUP_NUM = EN_FWT_YCGROUP_END - EN_FWT_YCGROUP_START = 6 (见FwtYcGroup_e)
        //public UInt32[] u32YcMax = new UInt32[Global.iniFWT_CN_FWT_YCGROUP_NUM];//遥测最大值，jifeng，2017-6-26
        //public UInt32[] u32YcCoe = new UInt32[Global.iniFWT_CN_FWT_YCGROUP_NUM];//遥测系数，jifeng，2017-6-26
        //南网需求，2017-10-27 10:09
        //F32 f32YcZone[CN_COMM_FWT_YC_MAX];
		//F32 f32YcCoe[CN_COMM_FWT_YC_MAX];
        public float[] f32YcZone = new float[Global.iniComm_CN_COMM_FWT_YC_MAX];
        public float[] f32YcCoe = new float[Global.iniComm_CN_COMM_FWT_YC_MAX];
        //----
        public void write_bin(BinaryWriter bw)
        {
            //使用GB2312编码，一个汉字，占2个字节.jifeng,2017-6-15
            writeChineseString(sName, bw);
            if (Global.bPrintFwt == true)
            {
                Byte[] encodedBytes = Encoding.GetEncoding("gb2312").GetBytes(sName);
                byte bt = 0x00;
                string strTmp1 = "";
                string strTmp2 = "";
                for (int k = 0; k < Global.iniPara_NAME_LEN; k++)
                {
                    if ((k + 1) <= encodedBytes.Length)
                    {
                        strTmp1 = string.Format("{0:X2} ", encodedBytes[k]);
                    }
                    else
                    {
                        bw.Write(bt);
                        strTmp1 = string.Format("{0:X2} ", 0);
                    }
                    strTmp2 += strTmp1;
                }
                //string ssss = string.Format("转发表名称: {0}", strTmp2);
                //Form_CfgTool.pMainForm.formInfo.LogMessage(ssss);
            }
            //----
            bw.Write(u32YcNum);
            bw.Write(u32SyxNum);
            bw.Write(u32DyxNum);
            bw.Write(u32YkNum);
            bw.Write(u32PowNum);

            bw.Write(u32YcStart);
            bw.Write(u32SyxStart);
            bw.Write(u32DyxStart);
            bw.Write(u32YkStart);
            bw.Write(u32PowStart);

            //----print
            if (Global.bPrintFwt == true)
            {
                byte[] bt = BitConverter.GetBytes(u32YcNum);
                string ssss = string.Format("ycnum: {0:X2} {1:X2} {2:X2} {3:X2}", bt[0], bt[1], bt[2], bt[3]);
                Form_CfgTool.pMainForm.formInfo.LogMessage(ssss);
                byte[] bt2 = BitConverter.GetBytes(u32SyxNum);
                ssss = string.Format("syxnum: {0:X2} {1:X2} {2:X2} {3:X2}", bt2[0], bt2[1], bt2[2], bt2[3]);
                Form_CfgTool.pMainForm.formInfo.LogMessage(ssss);
                byte[] bt3 = BitConverter.GetBytes(u32DyxNum);
                ssss = string.Format("dyxnum: {0:X2} {1:X2} {2:X2} {3:X2}", bt3[0], bt3[1], bt3[2], bt3[3]);
                Form_CfgTool.pMainForm.formInfo.LogMessage(ssss);
                byte[] bt4 = BitConverter.GetBytes(u32YkNum);
                ssss = string.Format("yknum: {0:X2} {1:X2} {2:X2} {3:X2}", bt4[0], bt4[1], bt4[2], bt4[3]);
                Form_CfgTool.pMainForm.formInfo.LogMessage(ssss);
                byte[] bt5 = BitConverter.GetBytes(u32PowNum);
                ssss = string.Format("meternum: {0:X2} {1:X2} {2:X2} {3:X2}", bt5[0], bt5[1], bt5[2], bt5[3]);
                Form_CfgTool.pMainForm.formInfo.LogMessage(ssss);


                byte[] bt6 = BitConverter.GetBytes(u32YcStart);
                ssss = string.Format("ycstart: {0:X2} {1:X2} {2:X2} {3:X2}", bt6[0], bt6[1], bt6[2], bt6[3]);
                Form_CfgTool.pMainForm.formInfo.LogMessage(ssss);
                byte[] bt7 = BitConverter.GetBytes(u32SyxStart);
                ssss = string.Format("syxstart: {0:X2} {1:X2} {2:X2} {3:X2}", bt7[0], bt7[1], bt7[2], bt7[3]);
                Form_CfgTool.pMainForm.formInfo.LogMessage(ssss);
                byte[] bt8 = BitConverter.GetBytes(u32DyxStart);
                ssss = string.Format("dyxstart: {0:X2} {1:X2} {2:X2} {3:X2}", bt8[0], bt8[1], bt8[2], bt8[3]);
                Form_CfgTool.pMainForm.formInfo.LogMessage(ssss);
                byte[] bt9 = BitConverter.GetBytes(u32YkStart);
                ssss = string.Format("ykstart: {0:X2} {1:X2} {2:X2} {3:X2}", bt9[0], bt9[1], bt9[2], bt9[3]);
                Form_CfgTool.pMainForm.formInfo.LogMessage(ssss);
                byte[] bt10 = BitConverter.GetBytes(u32PowStart);
                ssss = string.Format("meterstart: {0:X2} {1:X2} {2:X2} {3:X2}", bt10[0], bt10[1], bt10[2], bt10[3]);
                Form_CfgTool.pMainForm.formInfo.LogMessage(ssss);
            }
            //----
            for (int k = 0; k < u32YcIndex.Length;k++ )
            {
                bw.Write(u32YcIndex[k]);
                //----print
                if (Global.bPrintFwt == true)
                {
                    byte[] bt = BitConverter.GetBytes(u32YcIndex[k]);
                    string ssss = string.Format("[{0:D4}]ycindex: {1:X2} {2:X2} {3:X2} {4:X2}", k, bt[0], bt[1], bt[2], bt[3]);
                    Form_CfgTool.pMainForm.formInfo.LogMessage(ssss);
                }
            }
            for (int k = 0; k < u32SyxIndex.Length; k++)
            {
                bw.Write(u32SyxIndex[k]);
                //----print
                if (Global.bPrintFwt == true)
                {
                    byte[] bt = BitConverter.GetBytes(u32SyxIndex[k]);
                    string ssss = string.Format("[{0:D4}]syxindex: {1:X2} {2:X2} {3:X2} {4:X2}", k, bt[0], bt[1], bt[2], bt[3]);
                    Form_CfgTool.pMainForm.formInfo.LogMessage(ssss);
                }
            }
            for (int k = 0; k < u32DyxIndex.Length; k++)
            {
                bw.Write(u32DyxIndex[k]);
                //----print
                if (Global.bPrintFwt == true)
                {
                    byte[] bt = BitConverter.GetBytes(u32DyxIndex[k]);
                    string ssss = string.Format("[{0:D4}]dyxindex: {1:X2} {2:X2} {3:X2} {4:X2}", k, bt[0], bt[1], bt[2], bt[3]);
                    Form_CfgTool.pMainForm.formInfo.LogMessage(ssss);
                }
            }
            for (int k = 0; k < u32YkIndex.Length; k++)
            {
                bw.Write(u32YkIndex[k]);
                //----print
                if (Global.bPrintFwt == true)
                {
                    byte[] bt = BitConverter.GetBytes(u32YkIndex[k]);
                    string ssss = string.Format("[{0:D4}]ykindex: {1:X2} {2:X2} {3:X2} {4:X2}", k, bt[0], bt[1], bt[2], bt[3]);
                    Form_CfgTool.pMainForm.formInfo.LogMessage(ssss);
                }
            }
            for (int k = 0; k < u32PowIndex.Length; k++)
            {
                bw.Write(u32PowIndex[k]);
                //----print
                if (Global.bPrintFwt == true)
                {
                    byte[] bt = BitConverter.GetBytes(u32PowIndex[k]);
                    string ssss = string.Format("[{0:D4}]meterindex: {1:X2} {2:X2} {3:X2} {4:X2}", k, bt[0], bt[1], bt[2], bt[3]);
                    Form_CfgTool.pMainForm.formInfo.LogMessage(ssss);
                }
            }
            //----
            for (int k = 0; k < u32YcGroup.Length; k++)
            {
                bw.Write(u32YcGroup[k]);
                //----print
                if (Global.bPrintFwt == true)
                {
                    byte[] bt = BitConverter.GetBytes(u32YcGroup[k]);
                    string ssss = string.Format("[{0:D4}]ycgroup: {1:X2} {2:X2} {3:X2} {4:X2}", k, bt[0], bt[1], bt[2], bt[3]);
                    Form_CfgTool.pMainForm.formInfo.LogMessage(ssss);
                }
            }
            //新增单点遥信和双点遥信地址，jifeng，20170911
            for (int k = 0; k < u32SyxAddr.Length; k++)
            {
                bw.Write(u32SyxAddr[k]);
                //----print
                if (Global.bPrintFwt == true)
                {
                    byte[] bt = BitConverter.GetBytes(u32SyxAddr[k]);
                    string ssss = string.Format("[{0:D4}]syxaddr: {1:X2} {2:X2} {3:X2} {4:X2}", k, bt[0], bt[1], bt[2], bt[3]);
                    Form_CfgTool.pMainForm.formInfo.LogMessage(ssss);
                }
            }
            for (int k = 0; k < u32DyxAddr.Length; k++)
            {
                bw.Write(u32DyxAddr[k]);
                //----print
                if (Global.bPrintFwt == true)
                {
                    byte[] bt = BitConverter.GetBytes(u32DyxAddr[k]);
                    string ssss = string.Format("[{0:D4}]dyxaddr: {1:X2} {2:X2} {3:X2} {4:X2}", k, bt[0], bt[1], bt[2], bt[3]);
                    Form_CfgTool.pMainForm.formInfo.LogMessage(ssss);
                }
            }
            //----
            //for (int k = 0; k < u32YcMax.Length; k++)
            //{
            //    bw.Write(u32YcMax[k]);
            //}
            //for (int k = 0; k < u32YcCoe.Length; k++)
            //{
            //    bw.Write(u32YcCoe[k]);
            //}
            //----南网需求
            for (int k = 0; k < f32YcZone.Length; k++)
            {
                bw.Write(f32YcZone[k]);
                //----print
                if (Global.bPrintFwt == true)
                {
                    byte[] bt = BitConverter.GetBytes(f32YcZone[k]);
                    string ssss = string.Format("[{0:D4}]yczone: {1:X2} {2:X2} {3:X2} {4:X2}", k, bt[0], bt[1], bt[2], bt[3]);
                    Form_CfgTool.pMainForm.formInfo.LogMessage(ssss);
                }
            }
            for (int k = 0; k < f32YcCoe.Length; k++)
            {
                bw.Write(f32YcCoe[k]);
                //----print
                if (Global.bPrintFwt == true)
                {
                    byte[] bt = BitConverter.GetBytes(f32YcCoe[k]);
                    string ssss = string.Format("[{0:D4}]yccoe: {1:X2} {2:X2} {3:X2} {4:X2}", k, bt[0], bt[1], bt[2], bt[3]);
                    Form_CfgTool.pMainForm.formInfo.LogMessage(ssss);
                }
            }
        }
        public void read_bin(BinaryReader br)
        {
            sName = readChineseString(br);
            //----
            u32YcNum = br.ReadUInt32();
            u32SyxNum = br.ReadUInt32();
            u32DyxNum = br.ReadUInt32();
            u32YkNum = br.ReadUInt32();
            u32PowNum = br.ReadUInt32();
            //----
            u32YcStart = br.ReadUInt32();
            u32SyxStart = br.ReadUInt32();
            u32DyxStart = br.ReadUInt32();
            u32YkStart = br.ReadUInt32();
            u32PowStart = br.ReadUInt32();
            //----
            for (int k = 0; k < Global.iniComm_CN_COMM_FWT_YC_MAX; k++)
            {
                u32YcIndex[k] = br.ReadUInt32();
            }
            for (int k = 0; k < Global.iniComm_CN_COMM_FWT_SYX_MAX; k++)
            {
                u32SyxIndex[k] = br.ReadUInt32();
            }
            for (int k = 0; k < Global.iniComm_CN_COMM_FWT_DYX_MAX; k++)
            {
                u32DyxIndex[k] = br.ReadUInt32();
            }
            for (int k = 0; k < Global.iniComm_CN_COMM_FWT_YK_MAX; k++)
            {
                u32YkIndex[k] = br.ReadUInt32();
            }
            for (int k = 0; k < Global.iniComm_CN_COMM_FWT_POW_MAX; k++)
            {
                u32PowIndex[k] = br.ReadUInt32();
            }
            //----
            for (int k = 0; k < Global.iniComm_CN_COMM_FWT_YC_MAX; k++)
            {
                u32YcGroup[k] = br.ReadUInt32();
            }
            //新增单点遥信和双点遥信地址，jifeng，20170911
            for (int k = 0; k < Global.iniComm_CN_COMM_FWT_SYX_MAX; k++)
            {
                u32SyxAddr[k] = br.ReadUInt32();
            }
            for (int k = 0; k < Global.iniComm_CN_COMM_FWT_DYX_MAX; k++)
            {
                u32DyxAddr[k] = br.ReadUInt32();
            }
            //----
            //for (int k = 0; k < Global.iniFWT_CN_FWT_YCGROUP_NUM; k++)
            //{
            //    u32YcMax[k] = br.ReadUInt32();
            //}
            //for (int k = 0; k < Global.iniFWT_CN_FWT_YCGROUP_NUM; k++)
            //{
            //    u32YcCoe[k] = br.ReadUInt32();
            //}
            //----
            //public float[] f32YcZone = new float[Global.iniComm_CN_COMM_FWT_YC_MAX];
            //public float[] f32YcCoe = new float[Global.iniComm_CN_COMM_FWT_YC_MAX];
            for (int k = 0; k < Global.iniComm_CN_COMM_FWT_YC_MAX; k++)
            {
                f32YcZone[k] = br.ReadSingle();
            }
            for (int k = 0; k < Global.iniComm_CN_COMM_FWT_YC_MAX; k++)
            {
                f32YcCoe[k] = br.ReadSingle();
            }
        }
        //----
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
        //----END
    }
}
