using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace QCS.Utils
{
    public class Utils
    {
        public static DateTime toDateTime(string str, string splitStr)
        {
            string[] strValues = Regex.Split(str, splitStr);
            int month = Convert.ToInt32(strValues[0]);
            int day = Convert.ToInt32(strValues[1]);
            int year = Convert.ToInt32(strValues[2]);
            DateTime date = new DateTime(year, month,day);
            return date;
        }
        public static int toInt(string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return 0;
            }
            return Convert.ToInt16(str);
        }
        public static double toDouble(string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return 0;
            }
            return Convert.ToDouble(str);
        }
        public static double toDouble(double d, string currUnit, string unit)
        {
            if (currUnit.Equals(UnitEnum.METER))
            {
                if (unit.Equals(UnitEnum.MILLIMETER))
                {
                    return d * 1000;
                }
                if (unit.Equals( UnitEnum.FOOT))
                {
                    return d * 1000 / 25.4 / 12;
                }
                if (unit.Equals(UnitEnum.INCH))
                {
                    return d * 1000 / 25.4;
                }
            }
            if (currUnit.Equals(UnitEnum.MILLIMETER))
            {
                if (unit.Equals(UnitEnum.METER))
                {
                    return d / 1000;
                }
                if (unit.Equals(UnitEnum.FOOT))
                {
                    return d / 25.4 / 12;
                }
                if (unit.Equals(UnitEnum.INCH))
                {
                    return d / 25.4;
                }
            }
            if (currUnit.Equals(UnitEnum.FOOT))
            {
                if (unit.Equals( UnitEnum.MILLIMETER))
                {
                    return d * 25.4 * 12;
                }
                if (unit.Equals( UnitEnum.METER))
                {
                    return d * 25.4 * 12 / 1000;
                }
                if (unit.Equals( UnitEnum.INCH))
                {
                    return d * 12;
                }
            }
            if (currUnit.Equals(UnitEnum.INCH))
            {
                if (unit.Equals(UnitEnum.MILLIMETER))
                {
                    return d * 25.4;
                }
                if (unit.Equals(UnitEnum.FOOT))
                {
                    return d / 12;
                }
                if (unit.Equals(UnitEnum.METER))
                {
                    return d / 1000 * 25.4;
                }
            }
            return d;
        }

        public static double toDouble(double d, string unit)
        {
            return toDouble(d, UnitEnum.METER, unit);
        }
        public static double toDouble(string str, string unit)
        {
            return toDouble(toDouble(str), unit);
        }
        public static double toDouble(string str, string currUnit, string unit)
        {
            return toDouble(toDouble(str), currUnit, unit);
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
        public static double getLength(double x1, double y1, double z1, double x2, double y2, double z2)
        {
            return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2) + (z1 - z2));
        }
    }
}
