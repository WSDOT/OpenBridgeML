using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using QImporting.Utils;
using QImporting.Beans;
using System.Collections;
using QCS.Utils;

namespace QImporting.SAPImporting
{
    class Test
    {
        public static string getFileTitleStr( string fileName )
        {
            string titleStr = "File ";
            titleStr += fileName + " was saved by BrIM programs on " + DateTime.Now;
            return titleStr;
        }
        public static string getFRAME_SECTION_PROPERTIESStr()
        {
            string titleStr = SAPConstants.TXT_TABLE_STR;
            titleStr = titleStr + " \"FRAME SECTION PROPERTIES 08 - PCC I GIRDER\"";
            return titleStr;
        }

        static void Main(string[] args)
        {
            string sXmlFile = "D:\\Exporting_Linkage_qc_steel_alt.db1.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(sXmlFile);
            XmlNode deckNode = xmlDoc.SelectSingleNode("//" + QImportingConstants.DECK_NODE_NAME);
            CIPDeckInfo deckInfo = ImportingConvert.toCIPDeckInfo(deckNode);
            if (null != deckInfo)
            {
                ArrayList lineInfoArr = deckInfo.getLayoutLineInfos();
                foreach (LayoutLineInfo lineInfo in lineInfoArr)
                {
                    Console.WriteLine("Layout Line: " + lineInfo.getName());
                }
            }
            Console.ReadLine();
        }
    }
}
