using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using QImporting.Beans;

namespace QImporting.Utils
{
    class Tester
    {
        static void Main(string[] args)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"D:\Exporting_Linkage_qc_steel_alt.db1.xml");
            XmlNodeList pierNodeList = xmlDoc.SelectNodes("//pier");
            foreach (XmlElement pierElem in pierNodeList)
            {
                PierInfo pierInfo = ImportingConvert.toPierInfo(pierElem);
                Console.WriteLine("Pier Name: " + pierInfo.getName());
            }
            XmlNodeList abutNodeList = xmlDoc.SelectNodes("//abutment");
            foreach (XmlElement abutElem in abutNodeList)
            {
                AbutmentInfo abutInfo = ImportingConvert.toAbutmentInfo(abutElem);
                Console.WriteLine("Abutment Name: " + abutInfo.getName());
            }
            Console.ReadLine();
        }
    }
}
