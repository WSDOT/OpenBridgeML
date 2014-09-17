using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace BRIDGEWareImporting
{
    class Program
    {
        static void Main(string[] args)
        {
            //string templateFile = "D:\\xml_template_VirtiOpis.xml";

            string sXmlFile = @"D:\Exporting_Linkage_qc_steel_alt.db1.xml";
            //XmlDocument xmlDoc = new XmlDocument();
            //xmlDoc.Load(sXmlFile);
            //XmlNode bridgeNode = xmlDoc.SelectSingleNode("bridge");

            //string bridgeName = bridgeNode.Attributes.GetNamedItem("bridge_name").InnerText;
            //string bridgeID = bridgeNode.Attributes.GetNamedItem("bridge_id").InnerText;
            //string tFile = "D:\\" + bridgeName + "--" + bridgeID +".xml";
            //BRIDGEWareConverter.convertToBRIDGEWare(tFile, templateFile);
            BRIDGEWareConverter.importBridgeFromXml(sXmlFile);
            string brName = "template_bridge";//"Q-Quincy Avenue bridge over I-25 & LRT";
            //BRIDGEWareConverter.retrieveBridge(brName);
            Console.ReadLine();
        }
    }
}
