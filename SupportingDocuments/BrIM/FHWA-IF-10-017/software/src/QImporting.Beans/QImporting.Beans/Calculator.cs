using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace QImporting.Beans
{
    public class Calculator
    {
        public static double toDouble(string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return 0;
            }
            return Convert.ToDouble(str);
        }
        public static bool IsValiateDoubleChars(string str)
        {
            Char[] charArray = str.ToCharArray();
            string numberString = "1234567890.+-";
            int iCountForPoint = 0;
            int iCountForSign = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (numberString.IndexOf(charArray[i]) < 0)
                {
                    return false;
                }
                if (numberString.IndexOf(charArray[i]) == 10)
                {
                    iCountForPoint++;
                }
                if (numberString.IndexOf(charArray[i]) == 11 || numberString.IndexOf(charArray[i]) == 12)
                {
                    iCountForSign++;
                }
            }
            if (iCountForPoint > 1 || iCountForSign > 1 || str.IndexOf("+") > 0 || str.IndexOf("-") > 0)
            {
                return false;
            }
            return true;
        }
        public static bool IsValiateXmlFileName(string fileName)
        {
            if (String.IsNullOrEmpty(fileName))
            {
                //MessageBox.Show("Please choose the import file! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (fileName.Length < 4 || fileName.Substring(fileName.Length - 4).ToLower() != ".xml")
            {
                //MessageBox.Show("The file is not in valid format, please choose again! ", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        public static double getLength(PointInfo stPointInfo, PointInfo enPointInfo)
        {
            if (null == stPointInfo || null == enPointInfo)
                return 0.0;
            double x1 = stPointInfo.getX();
            double y1 = stPointInfo.getY();
            double z1 = stPointInfo.getZ();
            double x2 = enPointInfo.getX();
            double y2 = enPointInfo.getY();
            double z2 = enPointInfo.getZ();

            return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2) + (z1 - z2));
        }
        public static double getLength(ArrayList pointInfoList)
        {

            if (null == pointInfoList || pointInfoList.Count < 2)
                return 0.0;
            Array pointArr = pointInfoList.ToArray();
            double sum = 0.0;
            for(int i = 0; i < pointInfoList.Count -1; i++) 
            {
                PointInfo pointInfo1 = (PointInfo)pointArr.GetValue(i);
                PointInfo pointInfo2 = (PointInfo)pointArr.GetValue(i + 1);
                sum = sum + getLength(pointInfo1, pointInfo2);
            }
            return sum;
        }
    }
}
