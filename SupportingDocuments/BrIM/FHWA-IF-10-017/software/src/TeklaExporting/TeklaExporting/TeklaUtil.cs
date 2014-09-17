using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using TeklaBeans;
using Tekla.Structures.Model;

namespace TeklaExporting
{
    public class TeklaUtil
    {
        public static double parseLengthFromProfile(string lenStr)
        {
            int indexOfIN = lenStr.IndexOf("\"");
            string prefixStr = "0";
            string tailStr = lenStr;
            if (indexOfIN >= 0)
            {
                prefixStr = lenStr.Substring(0, indexOfIN);
                if (indexOfIN < lenStr.Length)
                {
                    tailStr = lenStr.Substring(indexOfIN + 1);
                }
                else
                {
                    tailStr = "0";
                }
            }
            double d1 = prefixStr.Equals("") ? 0.0 : toPointNum(prefixStr);
            double d2 = tailStr.Equals("") ? 0.0 : toPointNum(tailStr);
            double d = d1 + d2;
            return d;
        }
        public static string getPrefixStrFromProfile(string profileName)
        {
            int indexOfX = profileName.IndexOf("X");
            if (indexOfX >= 0)
            {
                return profileName.Substring(0, indexOfX);
            }
            return "";
        }
        public static string getPostfixStrFromProfile(string profileName)
        {
            int indexOfX = profileName.IndexOf("X");
            if (indexOfX >= 0)
            {
                return profileName.Substring(indexOfX + 1);
            }
            return "";
        }
        public static double toPointNum(string fracStr)
        {
            if (fracStr.Length <= 0)
                return 0.0;
            int indexOf = fracStr.IndexOf("/");
            if (indexOf > 0 && indexOf < fracStr.Length - 1)
            {
                string prefix = fracStr.Substring(0, indexOf);
                string postfix = fracStr.Substring(indexOf + 1);
                double d1 = Convert.ToDouble(prefix);
                double d2 = Convert.ToDouble(postfix);
                if (d2 != 0.0)
                {
                    return d1 / d2;
                }
                else
                {
                    return 0.0;
                }
            }
            else if (indexOf == fracStr.Length - 1)
            {
                return Convert.ToDouble(fracStr.Substring(0, fracStr.Length - 1));
            }
            else if (indexOf < 0)
            {
                return Convert.ToDouble(fracStr);
            }
            return 0.0;
        }
        public static string getSpanIDByName(string name)
        {
            string spanID = "";
            int index = name.IndexOf("_SPAN_");
            if (index > 0)
            {
                spanID = name.Substring(index + 6, 1);
            }
            return spanID;
        }
    }
}
