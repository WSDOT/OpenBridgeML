using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using TeklaBeans;
using Tekla.Structures;
using Tekla.Structures.Model;

namespace TeklaImporting
{
    public class TeklaUtil
    {
        public static CoordinateSystem getProperCoordinateByGirder(TeklaISteelGirder girder)
        {
            CoordinateSystem coSys = new CoordinateSystem();
            if (null == girder)
            {
                return coSys;
            }
            Beam mainBeam = girder.getMainBeam();
            Point centerPoint = mainBeam.StartPoint;
            coSys.Origin = centerPoint;
            Vector xAxis = new Vector();

            return coSys;
        }
        public static double get2DDistance(Point p1, Point p2)
        {
            return Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y));
        }
        public static double parseLengthFromProfile(string lenStr)
        {
            int indexOfIN = lenStr.IndexOf("\"");
            string prefixStr = lenStr.Substring(0, indexOfIN);
            string tailStr = lenStr.Substring(indexOfIN + 1);
            double d1 = prefixStr.Equals("") ? 0.0 : Convert.ToDouble(prefixStr);
            double d2 = tailStr.Equals("") ? 0.0 : Convert.ToDouble(tailStr);
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
                return profileName.Substring(indexOfX);
            }
            return "";
        }

    }
}
