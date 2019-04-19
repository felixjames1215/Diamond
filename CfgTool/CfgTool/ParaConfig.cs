using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CfgTool
{
    public class ParaConfig
    {
        public static int STRING_LENGTH_0 = 10;
        public static int STRING_LENGTH_1 = 15;
        public static int STRING_LENGTH_2 = 20;
        public static int STRING_LENGTH_3 = 25;
        public static int STRING_LENGTH_4 = 30;
        public static int STRING_LENGTH_5 = 40;
        public static int STRING_LENGTH_6 = 60;

        public static string SpaceStrFunc(int length)
        {
            string strReturn = string.Empty;
            if (length > 0)
            {
                for (int i = 0; i < length; i++)
                {
                    strReturn += " ";
                }
            }
            return strReturn;
        }

        public static string alignmentStrFunc(string strTemp, int iDistance)
        {
            int iLen = STRING_LENGTH_1;
            if(0==iDistance)
            {
                iLen = STRING_LENGTH_0;
            }
            else if (1 == iDistance)
            {
                iLen = STRING_LENGTH_1;
            }
            else if (2 == iDistance)
            {
                iLen = STRING_LENGTH_2;
            }
            else if (3 == iDistance)
            {
                iLen = STRING_LENGTH_3;
            }
            else if (4 == iDistance)
            {
                iLen = STRING_LENGTH_4;
            }
            else if (5 == iDistance)
            {
                iLen = STRING_LENGTH_5;
            }
            else if (6 == iDistance)
            {
                iLen = STRING_LENGTH_6;
            }
            byte[] byteStr = System.Text.Encoding.Default.GetBytes(strTemp.Trim());
            int iLength = byteStr.Length;
            int iNeed = iLen - iLength;

            byte[] spaceLen = Encoding.Default.GetBytes(" "); //一个空格的长度
            iNeed = iNeed / spaceLen.Length;

            string spaceString = SpaceStrFunc(iNeed);
            return strTemp + spaceString;

        }
    }
    //----END
}
