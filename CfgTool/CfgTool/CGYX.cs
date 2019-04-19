using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfgTool
{
    public class CGYX
    {
        public string strName = "组合遥信";
    }

    public class LogicCfg_And_t
    {
        public LogicCfg_And_t()
        {
            u16AndNo = 0;
            u16AndSize = 0;
            u16Data1Input = 0;
            u16Data2Input = 0;
            u8Data1Attr = 0;
            u8Data2Attr = 0;
            u8ResultAttr = 0;
            u8Opt = 0;
            for (int k = 0; k < u8Res.Length; k++) { u8Res[k] = 0x00; }
        }

        public UInt16 u16AndNo;			//该And表达式的编号
        public UInt16 u16AndSize;			//该And表达式的大小
        public UInt16 u16Data1Input;
        public UInt16 u16Data2Input;
        public Byte u8Data1Attr;			//0:输入为通道号;1:输入为常数
        public Byte u8Data2Attr;			//0:输入为通道号;1:输入为常数
        public Byte u8ResultAttr;			//0:输出不作处理;1:输出取反
        public Byte u8Opt;				//0:等于;1:小于;2:大于
        public Byte[] u8Res = new Byte[4];
    }

    public class LogicCfg_Or_t
    {
        public LogicCfg_Or_t()
        {
            u16OrNo = 0;
            u16OrSize = 0;
            u16Res1 = 0;
            u16AndNumber = 0;
            lstAnd.Clear();
        }

        public UInt16 u16OrNo;
        public UInt16 u16OrSize;
        public UInt16 u16Res1;
        public UInt16 u16AndNumber;
        public SortedList<int, LogicCfg_And_t> lstAnd = new SortedList<int, LogicCfg_And_t>();
    //    LogicCfg_And_t tAnd1;	
    //    LogicCfg_And_t tAnd2;	
    ////...
    //    LogicCfg_And_t tAndN;	
    }

    public class LogicCfg_Logic_t
    {
        public LogicCfg_Logic_t()
        {
            u16LogicNo = 0;
            u16LogicSize = 0;
            u16ZhyxChannel = 0;
            u16OrNumber = 0;
            lstOr.Clear();
        }

	    public UInt16 u16LogicNo;
	    public UInt16 u16LogicSize;
        public UInt16 u16ZhyxChannel;
	    public UInt16 u16OrNumber;
        public SortedList<int, LogicCfg_Or_t> lstOr = new SortedList<int, LogicCfg_Or_t>();
        //LogicCfg_Or_t tOr1;	
        //LogicCfg_Or_t tOr2;	
        //...
        //LogicCfg_Or_t tOrN;
    }


}
