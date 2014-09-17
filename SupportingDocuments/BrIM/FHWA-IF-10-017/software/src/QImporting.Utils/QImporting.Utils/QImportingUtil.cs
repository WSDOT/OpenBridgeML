using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using QImporting.Beans;

namespace QImporting.Utils
{
    public class QImportingUtil
    {
        public static double ConsiderUnit(string originUnit, string currUnit, double d)
        {
            if (String.IsNullOrEmpty(originUnit) || String.IsNullOrEmpty(currUnit))
                return d;
            if (originUnit == "FT")
            {
                if (currUnit == "MM")
                {
                    return d * 12 * 25.4;
                }
            }
            if (originUnit == "M")
            {
                if (currUnit == "MM")
                {
                    return d * 1000;
                }
            }
            return d;
        }
        public static double ConsiderUnit(string originUnit, double d)
        {
            return ConsiderUnit(originUnit, "MM", d);

        }
        public static double ConsiderUnit(double d)
        {
            return ConsiderUnit("FT", "MM", d);
        }
        public static double toDouble(string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return 0;
            }
            return Convert.ToDouble(str);
        }
        public static double toDoubleValue(XmlNode node)
        {
            if (null == node)
            {
                Console.WriteLine("The node of Point is null or empty!");
                return 0;
            }
            return toDouble(node.InnerText);
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
        public static XmlNodeList loadXmlFile(string xmlFile, string nodeName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFile);
            XmlNodeList xNodeList = xmlDoc.SelectNodes(nodeName);
            return xNodeList;
        }
        //public static Parameter getInstanceParameter(FamilyInstance fi, string name)
        //{
        //    if (null == fi || null == name)
        //    {
        //        return null;
        //    }
        //    ParameterSet paramSet = fi.Parameters;
        //    foreach (Parameter param in paramSet)
        //    {
        //        if (name.Equals(param.Definition.Name))
        //        {
        //            return param;
        //        }
        //    }
        //    return null;
        //}
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

    }
}
