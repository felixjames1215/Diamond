using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace CfgTool
{
    public class ModelCfgFile_t
    {
        public ModelCfgFile_t()
        {
            u32ModelNum = 0;
            tModel = new Model_t[Global.iniComm_CN_COMM_MODEL_MAX];
            for (int k = 0; k < tModel.Length;k++ ) { tModel[k] = new Model_t(); }
        }
        //----
        public UInt32 u32ModelNum;
        public Model_t[] tModel;
        //----
        public void form_Piece()
        {
            form_InfoPiece();
            form_ParaPiece();
            form_SetPiece();
        }
        //----1、信息片
        public void form_InfoPiece()
        {
            u32ModelNum = Convert.ToUInt32(Global.g_list_Model.Count);
            foreach (var t in Global.g_list_Model)
            {
                int iCount = 0;
                int iGroup = 1;
                int iPieceNum = 0;
                string strCompare_1 = "";
                string strCompare_2 = "";
                int Index = t.Key - 1;
                //----
                tModel[Index].sName = t.Value.ModelName;//模板名称
                //---------------------------------------------------------1、遥测
                foreach (var obj in t.Value.lst_Table_YC)
                {
                    if (iCount == 0)
                    {
                        obj.Value.Group = iGroup;
                        iPieceNum += 1;
                        //-----------------------------------------
                        tModel[Index].tInfoPiece[iGroup - 1].u32StartObjAddr = Convert.ToUInt32(obj.Value.Addr);
                        tModel[Index].tInfoPiece[iGroup - 1].u8YcGroup = Convert.ToByte(obj.Value.iGroup);
                        tModel[Index].tInfoPiece[iGroup - 1].u16InfoNum = Convert.ToUInt16(iPieceNum);
                        tModel[Index].tInfoPiece[iGroup - 1].u8RtdbType = 0x02;
                        tModel[Index].tInfoPiece[iGroup - 1].u8InfoType = getInfoType(obj.Value.DataType);
                        //-----------------------------------------
                        iGroup += 1;
                        strCompare_1 = string.Format("{0}_{1}", obj.Value.iGroup, obj.Value.Addr);
                    }
                    else
                    {
                        strCompare_2 = string.Format("{0}_{1}", obj.Value.iGroup, obj.Value.Addr - 1);
                        if (strCompare_1 != strCompare_2)
                        {
                            obj.Value.Group = iGroup;
                            strCompare_1 = string.Format("{0}_{1}", obj.Value.iGroup, obj.Value.Addr);
                            iPieceNum = 1;
                            //-----------------------------------------
                            tModel[Index].tInfoPiece[iGroup - 1].u32StartObjAddr = Convert.ToUInt32(obj.Value.Addr);
                            tModel[Index].tInfoPiece[iGroup - 1].u8YcGroup = Convert.ToByte(obj.Value.iGroup);
                            tModel[Index].tInfoPiece[iGroup - 1].u16InfoNum = Convert.ToUInt16(iPieceNum);
                            tModel[Index].tInfoPiece[iGroup - 1].u8RtdbType = 0x02;
                            tModel[Index].tInfoPiece[iGroup - 1].u8InfoType = getInfoType(obj.Value.DataType);
                            //-----------------------------------------
                            iGroup += 1;
                        }
                        else
                        {
                            strCompare_1 = string.Format("{0}_{1}", obj.Value.iGroup, obj.Value.Addr);
                            obj.Value.Group = iGroup - 1;
                            //-----------------------------------------
                            iPieceNum += 1;
                            tModel[Index].tInfoPiece[iGroup - 2].u16InfoNum = Convert.ToUInt16(iPieceNum);
                            //-----------------------------------------
                        }
                    }
                    iCount += 1;
                }
                //----
                //tModel[Index].u32InfoPieceNum += Convert.ToUInt32(iGroup - 1);
                //---------------------------------------------------------2、单点遥信
                iCount = 0;
                //iGroup = 1;
                iPieceNum = 0;
                strCompare_1 = "";
                strCompare_2 = "";
                foreach (var obj in t.Value.lst_Table_SYX)
                {
                    if (iCount == 0)
                    {
                        obj.Value.Group = iGroup;
                        iPieceNum += 1;
                        //-----------------------------------------
                        tModel[Index].tInfoPiece[iGroup - 1].u32StartObjAddr = Convert.ToUInt32(obj.Value.Addr);
                        tModel[Index].tInfoPiece[iGroup - 1].u8YcGroup = 0;
                        tModel[Index].tInfoPiece[iGroup - 1].u16InfoNum = Convert.ToUInt16(iPieceNum);
                        tModel[Index].tInfoPiece[iGroup - 1].u8RtdbType = 0x04;
                        tModel[Index].tInfoPiece[iGroup - 1].u8InfoType = getInfoType(obj.Value.DataType);
                        //-----------------------------------------
                        iGroup += 1;
                        strCompare_1 = string.Format("{0}_{1}", obj.Value.DataType, obj.Value.Addr);
                    }
                    else
                    {
                        strCompare_2 = string.Format("{0}_{1}", obj.Value.DataType, obj.Value.Addr - 1);
                        if (strCompare_1 != strCompare_2)
                        {
                            obj.Value.Group = iGroup;
                            strCompare_1 = string.Format("{0}_{1}", obj.Value.DataType, obj.Value.Addr);
                            iPieceNum = 1;
                            //-----------------------------------------
                            tModel[Index].tInfoPiece[iGroup - 1].u32StartObjAddr = Convert.ToUInt32(obj.Value.Addr);
                            tModel[Index].tInfoPiece[iGroup - 1].u8YcGroup = 0;
                            tModel[Index].tInfoPiece[iGroup - 1].u16InfoNum = Convert.ToUInt16(iPieceNum);
                            tModel[Index].tInfoPiece[iGroup - 1].u8RtdbType = 0x04;
                            tModel[Index].tInfoPiece[iGroup - 1].u8InfoType = getInfoType(obj.Value.DataType);
                            //-----------------------------------------
                            iGroup += 1;
                        }
                        else
                        {
                            strCompare_1 = string.Format("{0}_{1}", obj.Value.DataType, obj.Value.Addr);
                            obj.Value.Group = iGroup - 1;
                            //-----------------------------------------
                            iPieceNum += 1;
                            tModel[Index].tInfoPiece[iGroup - 2].u16InfoNum = Convert.ToUInt16(iPieceNum);
                            //-----------------------------------------
                        }
                    }
                    iCount += 1;
                }
                //----
                //tModel[Index].u32InfoPieceNum += Convert.ToUInt32(iGroup - 1);
                //---------------------------------------------------------3、双点遥信
                iCount = 0;
                //iGroup = 1;
                iPieceNum = 0;
                strCompare_1 = "";
                strCompare_2 = "";
                foreach (var obj in t.Value.lst_Table_DYX)
                {
                    if (iCount == 0)
                    {
                        obj.Value.Group = iGroup;
                        iPieceNum += 1;
                        //-----------------------------------------
                        tModel[Index].tInfoPiece[iGroup - 1].u32StartObjAddr = Convert.ToUInt32(obj.Value.Addr);
                        tModel[Index].tInfoPiece[iGroup - 1].u8YcGroup = 0;
                        tModel[Index].tInfoPiece[iGroup - 1].u16InfoNum = Convert.ToUInt16(iPieceNum);
                        tModel[Index].tInfoPiece[iGroup - 1].u8RtdbType = 0x05;
                        tModel[Index].tInfoPiece[iGroup - 1].u8InfoType = getInfoType(obj.Value.DataType);
                        //-----------------------------------------
                        iGroup += 1;
                        strCompare_1 = string.Format("{0}_{1}", obj.Value.DataType, obj.Value.Addr);
                    }
                    else
                    {
                        strCompare_2 = string.Format("{0}_{1}", obj.Value.DataType, obj.Value.Addr - 1);
                        if (strCompare_1 != strCompare_2)
                        {
                            obj.Value.Group = iGroup;
                            strCompare_1 = string.Format("{0}_{1}", obj.Value.DataType, obj.Value.Addr);
                            iPieceNum = 1;
                            //-----------------------------------------
                            tModel[Index].tInfoPiece[iGroup - 1].u32StartObjAddr = Convert.ToUInt32(obj.Value.Addr);
                            tModel[Index].tInfoPiece[iGroup - 1].u8YcGroup = 0;
                            tModel[Index].tInfoPiece[iGroup - 1].u16InfoNum = Convert.ToUInt16(iPieceNum);
                            tModel[Index].tInfoPiece[iGroup - 1].u8RtdbType = 0x05;
                            tModel[Index].tInfoPiece[iGroup - 1].u8InfoType = getInfoType(obj.Value.DataType);
                            //-----------------------------------------
                            iGroup += 1;
                        }
                        else
                        {
                            strCompare_1 = string.Format("{0}_{1}", obj.Value.DataType, obj.Value.Addr);
                            obj.Value.Group = iGroup - 1;
                            //-----------------------------------------
                            iPieceNum += 1;
                            tModel[Index].tInfoPiece[iGroup - 2].u16InfoNum = Convert.ToUInt16(iPieceNum);
                            //-----------------------------------------
                        }
                    }
                    iCount += 1;
                }
                //----
                //tModel[Index].u32InfoPieceNum += Convert.ToUInt32(iGroup - 1);
                //---------------------------------------------------------4、遥控
                iCount = 0;
                //iGroup = 1;
                iPieceNum = 0;
                strCompare_1 = "";
                strCompare_2 = "";
                foreach (var obj in t.Value.lst_Table_YK)
                {
                    if (iCount == 0)
                    {
                        obj.Value.Group = iGroup;
                        iPieceNum += 1;
                        //-----------------------------------------
                        tModel[Index].tInfoPiece[iGroup - 1].u32StartObjAddr = Convert.ToUInt32(obj.Value.Addr);
                        tModel[Index].tInfoPiece[iGroup - 1].u8YcGroup = 0;
                        tModel[Index].tInfoPiece[iGroup - 1].u16InfoNum = Convert.ToUInt16(iPieceNum);
                        tModel[Index].tInfoPiece[iGroup - 1].u8RtdbType = 0x06;
                        tModel[Index].tInfoPiece[iGroup - 1].u8InfoType = getInfoType(obj.Value.DataType);
                        //-----------------------------------------
                        iGroup += 1;
                        strCompare_1 = string.Format("{0}_{1}", obj.Value.DataType, obj.Value.Addr);
                    }
                    else
                    {
                        strCompare_2 = string.Format("{0}_{1}", obj.Value.DataType, obj.Value.Addr - 1);
                        if (strCompare_1 != strCompare_2)
                        {
                            obj.Value.Group = iGroup;
                            strCompare_1 = string.Format("{0}_{1}", obj.Value.DataType, obj.Value.Addr);
                            iPieceNum = 1;
                            //-----------------------------------------
                            tModel[Index].tInfoPiece[iGroup - 1].u32StartObjAddr = Convert.ToUInt32(obj.Value.Addr);
                            tModel[Index].tInfoPiece[iGroup - 1].u8YcGroup = 0;
                            tModel[Index].tInfoPiece[iGroup - 1].u16InfoNum = Convert.ToUInt16(iPieceNum);
                            tModel[Index].tInfoPiece[iGroup - 1].u8RtdbType = 0x06;
                            tModel[Index].tInfoPiece[iGroup - 1].u8InfoType = getInfoType(obj.Value.DataType);
                            //-----------------------------------------
                            iGroup += 1;
                        }
                        else
                        {
                            strCompare_1 = string.Format("{0}_{1}", obj.Value.DataType, obj.Value.Addr);
                            obj.Value.Group = iGroup - 1;
                            //-----------------------------------------
                            iPieceNum += 1;
                            tModel[Index].tInfoPiece[iGroup - 2].u16InfoNum = Convert.ToUInt16(iPieceNum);
                            //-----------------------------------------
                        }
                    }
                    iCount += 1;
                }
                //----
                //tModel[Index].u32InfoPieceNum += Convert.ToUInt32(iGroup - 1);
                //---------------------------------------------------------5、计量
                iCount = 0;
                //iGroup = 1;
                iPieceNum = 0;
                strCompare_1 = "";
                strCompare_2 = "";
                foreach (var obj in t.Value.lst_Table_Meter)
                {
                    if (iCount == 0)
                    {
                        obj.Value.Group = iGroup;
                        iPieceNum += 1;
                        //-----------------------------------------
                        tModel[Index].tInfoPiece[iGroup - 1].u32StartObjAddr = Convert.ToUInt32(obj.Value.Addr);
                        tModel[Index].tInfoPiece[iGroup - 1].u8YcGroup = 0;
                        tModel[Index].tInfoPiece[iGroup - 1].u16InfoNum = Convert.ToUInt16(iPieceNum);
                        tModel[Index].tInfoPiece[iGroup - 1].u8RtdbType = 0x07;
                        tModel[Index].tInfoPiece[iGroup - 1].u8InfoType = getInfoType(obj.Value.DataType);
                        //-----------------------------------------
                        iGroup += 1;
                        strCompare_1 = string.Format("{0}_{1}", obj.Value.DataType, obj.Value.Addr);
                    }
                    else
                    {
                        strCompare_2 = string.Format("{0}_{1}", obj.Value.DataType, obj.Value.Addr - 1);
                        if (strCompare_1 != strCompare_2)
                        {
                            obj.Value.Group = iGroup;
                            strCompare_1 = string.Format("{0}_{1}", obj.Value.DataType, obj.Value.Addr);
                            iPieceNum = 1;
                            //-----------------------------------------
                            tModel[Index].tInfoPiece[iGroup - 1].u32StartObjAddr = Convert.ToUInt32(obj.Value.Addr);
                            tModel[Index].tInfoPiece[iGroup - 1].u8YcGroup = 0;
                            tModel[Index].tInfoPiece[iGroup - 1].u16InfoNum = Convert.ToUInt16(iPieceNum);
                            tModel[Index].tInfoPiece[iGroup - 1].u8RtdbType = 0x07;
                            tModel[Index].tInfoPiece[iGroup - 1].u8InfoType = getInfoType(obj.Value.DataType);
                            //-----------------------------------------
                            iGroup += 1;
                        }
                        else
                        {
                            strCompare_1 = string.Format("{0}_{1}", obj.Value.DataType, obj.Value.Addr);
                            obj.Value.Group = iGroup - 1;
                            //-----------------------------------------
                            iPieceNum += 1;
                            tModel[Index].tInfoPiece[iGroup - 2].u16InfoNum = Convert.ToUInt16(iPieceNum);
                            //-----------------------------------------
                        }
                    }
                    iCount += 1;
                }
                //----
                tModel[Index].u32InfoPieceNum += Convert.ToUInt32(iGroup - 1);
                //END
            }
        }
        //----2、参数片
        public void form_ParaPiece()
        {
            try
            {
                foreach (var t in Global.g_list_Model)
                {
                    int iCount = 0;
                    int iGroup = 1;
                    int iPieceNum = 0;
                    string strCompare_1 = "";
                    string strCompare_2 = "";
                    int Index = t.Key - 1;
                    //---------------------------------------------------------参数
                    foreach (var obj in t.Value.lst_Table_Para)
                    {
                        if (iGroup >= Global.iniComm_CN_COMM_MODEL_PARAGROUP_MAX)
                        {
                            MessageBox.Show(string.Format("参数片分组越界，最大值：{0}", Global.iniComm_CN_COMM_MODEL_PARAGROUP_MAX),
                                "告警", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //return;
                        }
                        if (iCount == 0)
                        {
                            obj.Value.Group = iGroup;
                            iPieceNum += 1;
                            //-----------------------------------------
                            tModel[Index].tParaPiece[iGroup - 1].u8ItemType = getInfoType(obj.Value.DataType);
                            tModel[Index].tParaPiece[iGroup - 1].u8ParaType = getParaType();
                            tModel[Index].tParaPiece[iGroup - 1].u16ItemNum = Convert.ToByte(iPieceNum);
                            tModel[Index].tParaPiece[iGroup - 1].u32ObjAddrStart = Convert.ToUInt32(obj.Value.Addr);
                            //-----------------------------------------
                            iGroup += 1;
                            strCompare_1 = string.Format("{0}_{1}", obj.Value.GroupName, obj.Value.Addr);
                        }
                        else
                        {
                            strCompare_2 = string.Format("{0}_{1}", obj.Value.GroupName, obj.Value.Addr - 1);
                            if (strCompare_1 != strCompare_2)
                            {
                                obj.Value.Group = iGroup;
                                strCompare_1 = string.Format("{0}_{1}", obj.Value.GroupName, obj.Value.Addr);
                                iPieceNum = 1;
                                //-----------------------------------------
                                tModel[Index].tParaPiece[iGroup - 1].u8ItemType = getInfoType(obj.Value.DataType);
                                tModel[Index].tParaPiece[iGroup - 1].u8ParaType = getParaType();
                                tModel[Index].tParaPiece[iGroup - 1].u16ItemNum = (ushort)iPieceNum;// Convert.ToByte(iPieceNum);
                                tModel[Index].tParaPiece[iGroup - 1].u32ObjAddrStart = Convert.ToUInt32(obj.Value.Addr);
                                //-----------------------------------------
                                iGroup += 1;
                            }
                            else
                            {
                                strCompare_1 = string.Format("{0}_{1}", obj.Value.GroupName, obj.Value.Addr);
                                obj.Value.Group = iGroup - 1;
                                //-----------------------------------------
                                iPieceNum += 1;
                                tModel[Index].tParaPiece[iGroup - 2].u16ItemNum = (ushort)iPieceNum;// Convert.ToByte(iPieceNum);
                                //-----------------------------------------
                            }
                        }
                        iCount += 1;
                    }
                    //----
                    tModel[Index].u32ParaPieceNum += Convert.ToUInt32(iGroup - 1);
                    //END
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
        //----3、定值片
        public void form_SetPiece()
        {
            foreach (var t in Global.g_list_Model)
            {
                int iCount = 0;
                int iGroup = 1;
                int iPieceNum = 0;
                string strCompare_1 = "";
                string strCompare_2 = "";
                int Index = t.Key - 1;
                //---------------------------------------------------------定值
                foreach (var obj in t.Value.lst_Table_Setting)
                {
                    if (iGroup >= Global.iniComm_CN_COMM_MODEL_PARAGROUP_MAX)
                    {
                        MessageBox.Show(string.Format("定值片分组越界，最大值：{0}", Global.iniComm_CN_COMM_MODEL_PARAGROUP_MAX), 
                            "告警", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //return;
                    }
                    if (iCount == 0)
                    {
                        obj.Value.Group = iGroup;
                        iPieceNum += 1;
                        //-----------------------------------------
                        tModel[Index].tSetPiece[iGroup - 1].u8ItemType = getInfoType(obj.Value.DataType);
                        tModel[Index].tSetPiece[iGroup - 1].u8ParaType = getSetType();
                        tModel[Index].tSetPiece[iGroup - 1].u16ItemNum = Global.g_Set_ItemNum;//Convert.ToByte(iPieceNum);
                        tModel[Index].tSetPiece[iGroup - 1].u32ObjAddrStart = Convert.ToUInt32(obj.Value.Addr);
                        //-----------------------------------------
                        iGroup += 1;
                        strCompare_1 = string.Format("{0}", obj.Value.GroupName);
                    }
                    else
                    {
                        strCompare_2 = string.Format("{0}", obj.Value.GroupName);
                        if (strCompare_1 != strCompare_2)
                        {
                            obj.Value.Group = iGroup;
                            strCompare_1 = string.Format("{0}", obj.Value.GroupName);
                            iPieceNum = 1;
                            //-----------------------------------------
                            tModel[Index].tSetPiece[iGroup - 1].u8ItemType = getInfoType(obj.Value.DataType);
                            tModel[Index].tSetPiece[iGroup - 1].u8ParaType = getSetType();
                            tModel[Index].tSetPiece[iGroup - 1].u16ItemNum = Global.g_Set_ItemNum;//Convert.ToByte(iPieceNum);
                            tModel[Index].tSetPiece[iGroup - 1].u32ObjAddrStart = Convert.ToUInt32(obj.Value.Addr);
                            //-----------------------------------------
                            iGroup += 1;
                        }
                        else
                        {
                            strCompare_1 = string.Format("{0}", obj.Value.GroupName);
                            obj.Value.Group = iGroup - 1;
                            //-----------------------------------------
                            iPieceNum += 1;
                            tModel[Index].tSetPiece[iGroup - 2].u16ItemNum = Global.g_Set_ItemNum;//Convert.ToByte(iPieceNum);
                            //-----------------------------------------
                        }
                    }
                    iCount += 1;
                }
                //----
                tModel[Index].u32SetPieceNum += Convert.ToUInt32(iGroup - 1);
                //END
            }
        }
        //----
        public void write_bin(BinaryWriter bw)
        {
            //1、模板数量
            bw.Write(Convert.ToUInt32(Global.g_list_Model.Count));
            //2、模板
            for (int m = 0; m < u32ModelNum; m++)
            {
                //模板名称
                //使用GB2312编码，一个汉字，占2个字节.jifeng,2017-6-15
                Byte[] encodedBytes = Encoding.GetEncoding("gb2312").GetBytes(tModel[m].sName);
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
                //1、信息片
                bw.Write(tModel[m].u32InfoPieceNum);
                for (int k = 0; k < Global.iniComm_CN_COMM_MODEL_INFOPIECE_MAX; k++)
                {
                    bw.Write(tModel[m].tInfoPiece[k].u32StartObjAddr);
                    bw.Write(tModel[m].tInfoPiece[k].u8YcGroup);
                    bw.Write(tModel[m].tInfoPiece[k].u8Res1[0]);
                    bw.Write(tModel[m].tInfoPiece[k].u8Res1[1]);
                    bw.Write(tModel[m].tInfoPiece[k].u8Res1[2]);
                    bw.Write(tModel[m].tInfoPiece[k].u16InfoNum);
                    bw.Write(tModel[m].tInfoPiece[k].u8InfoType);
                    bw.Write(tModel[m].tInfoPiece[k].u8RtdbType);
                }
                //2、参数片
                bw.Write(tModel[m].u32ParaPieceNum);
                for (int k = 0; k < Global.iniComm_CN_COMM_MODEL_PARAGROUP_MAX; k++)
                {
                    bw.Write(tModel[m].tParaPiece[k].u8ItemType);
                    bw.Write(tModel[m].tParaPiece[k].u8ParaType);
                    bw.Write(tModel[m].tParaPiece[k].u16ItemNum);
                    bw.Write(tModel[m].tParaPiece[k].u32ObjAddrStart);
                }
                //3、定值片
                bw.Write(tModel[m].u32SetPieceNum);
                for (int k = 0; k < Global.iniComm_CN_COMM_MODEL_PARAGROUP_MAX; k++)
                {
                    bw.Write(tModel[m].tSetPiece[k].u8ItemType);
                    bw.Write(tModel[m].tSetPiece[k].u8ParaType);
                    bw.Write(tModel[m].tSetPiece[k].u16ItemNum);
                    bw.Write(tModel[m].tSetPiece[k].u32ObjAddrStart);
                }
            }
            //END
        }
        public void read_bin(BinaryReader br)
        {
            //1、模板数量
            //2、模板
        }
        public byte getInfoType(string datatype)
        {
            byte res = 0x00;
            int ires = 0;
            if(datatype == "BOOL")
            {
                ires = 1;
            }
            else if (datatype == "S8")
            {
                ires = 43;
            }
            else if (datatype == "U8")
            {
                ires = 32;
            }
            else if (datatype == "S16")
            {
                ires = 33;
            }
            else if (datatype == "U16")
            {
                ires = 45;
            }
            else if (datatype == "S32")
            {
                ires = 2;
            }
            else if (datatype == "U32")
            {
                ires = 35;
            }
            else if (datatype == "S64")
            {
                ires = 36;
            }
            else if (datatype == "U64")
            {
                ires = 37;
            }
            else if (datatype == "F32")
            {
                ires = 38;
            }
            else if (datatype == "F64")
            {
                ires = 39;
            }
            else if (datatype == "STR" || datatype == "STRING")
            {
                ires = 4;
            }
            res = Convert.ToByte(ires);
            return res;
        }
        public byte getParaType()
        {
            //见ParaType_e枚举
            return 0x02;
        }
        public byte getSetType()
        {
            return 0x03;
        }
        //----END
    }

    public class Model_t
    {
        public Model_t()
        {
            sName = "";
            u32InfoPieceNum = 0;
            tInfoPiece = new InfoPiece_t[Global.iniComm_CN_COMM_MODEL_INFOPIECE_MAX];
            for (int k = 0; k < tInfoPiece.Length; k++) { tInfoPiece[k] = new InfoPiece_t(); }
            //----
            u32ParaPieceNum = 0;
            tParaPiece = new ParaPiece_t[Global.iniComm_CN_COMM_MODEL_PARAGROUP_MAX];
            for (int k = 0; k < tParaPiece.Length; k++) { tParaPiece[k] = new ParaPiece_t(); }
            //----
            u32SetPieceNum = 0;
            tSetPiece = new ParaPiece_t[Global.iniComm_CN_COMM_MODEL_PARAGROUP_MAX];
            for (int k = 0; k < tSetPiece.Length; k++) { tSetPiece[k] = new ParaPiece_t(); }
        }
        //----
        public string sName;
        //----
        //1、信息片
        public UInt32 u32InfoPieceNum;
        public InfoPiece_t[] tInfoPiece;
        //2、参数片
        public UInt32 u32ParaPieceNum;
        public ParaPiece_t[] tParaPiece;
        //3、定值片
        public UInt32 u32SetPieceNum;
        public ParaPiece_t[] tSetPiece;
    }
    //----
    public class InfoPiece_t
    {
        public InfoPiece_t()
        {
            u32StartObjAddr = 0;
            u8YcGroup = 0x00;
            u8Res1[0] = 0x00;
            u8Res1[1] = 0x00;
            u8Res1[2] = 0x00;

            u16InfoNum = 0x00;
            u8InfoType = 0x00;
            u8RtdbType = 0x00;
        }
        //----
        public UInt32 u32StartObjAddr;//信息起始对象地址
        public byte u8YcGroup;//遥测组别，jifeng，2017-6-25
        public byte[] u8Res1 = new byte[3];//占位

        public UInt16 u16InfoNum;//信息数目
        public byte u8InfoType;//数据类型
        public byte u8RtdbType;//遥测、单点遥信、双点遥信、遥控、计量
    }
    public class ParaPiece_t
    {
        public ParaPiece_t()
        {
            u8ItemType = 0x00;
            u8ParaType = 0x00;
            u16ItemNum = 0;
            u32ObjAddrStart = 0;
        }
        //----
        public byte  u8ItemType;
	    public byte  u8ParaType;
	    public UInt16 u16ItemNum;
	    public UInt32 u32ObjAddrStart;
    }
    //----
    public enum InfoType_e
    {
        EN_VTYPE_START = 0,
        EN_VYTPE_BOOL = 1,
        EN_VTYPE_S8 = 43,
        EN_VTYPE_U8 = 32,
        EN_VTYPE_S16 = 33,
        EN_VTYPE_U16 = 45,
        EN_VTYPE_S32 = 2,
        EN_VTYPE_U32 = 35,
        EN_VTYPE_S64 = 36,
        EN_VTYPE_U64 = 37,
        EN_VTYPE_F32 = 38,
        EN_VTYPE_F64 = 39,
        EN_VTYPE_STR = 4,
        EN_VTYPE_END
    }
    public enum ParaType_e
    {
        EN_PARATYPE_NULL = 0,
        EN_PARATYPE_TYPE1,

        EN_PARATYPE_PARA,
        EN_PARATYPE_SET,
        EN_PARATYPE_SOFTYB,
        EN_PARATYPE_SETZONE,

        EN_PARATYPE_TYPE2,

        EN_PARATYPE_POINT,
        EN_PARATYPE_FACTORY,

        EN_PARATYPE_END
    }
    //----END
}
