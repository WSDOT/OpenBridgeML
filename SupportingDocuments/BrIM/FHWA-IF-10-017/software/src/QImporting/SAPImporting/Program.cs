using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace QImporting.SAPImporting
{
    class Program
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

        //static void Main(string[] args)
        //{
        //    string dir = @"D:\S2KFiles";
        //    if (!Directory.Exists(dir))
        //    {
        //        Directory.CreateDirectory(dir);
        //    }
        //    string fileName = dir + @"\S2KFile_Test_.s2k";// +DateTime.Now.Date;
        //    StreamWriter writer = new StreamWriter(fileName, false);
        //    writer.WriteLine(getFileTitleStr(fileName));
        //    writer.Write("\n");
        //    writer.Write(SAPConstants.TXT_TABLE_STR);
        //    writer.Write("\n");
        //    writer.WriteLine(getFRAME_SECTION_PROPERTIESStr());
        //    writer.Write("\n");
        //    writer.Write("SectionName=BRD1    ");

        //    writer.Write("AutoBridge=Yes    ");
        //    //TODO...


        //    writer.Close();
        //}
    }
}
