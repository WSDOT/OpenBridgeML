using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using abobrdg;
using System.Collections;
using QImporting.Utils;
using QImporting.Beans;
using abocncb;
using abosys;
using abostld;
using LumenWorks.Framework.IO.Csv;
using System.IO;

namespace BRIDGEWareImporting
{
    public class ProgramRunner
    {
        private static bool isValidateSecLossFile(string[] recArr, string type)
        {
            if (type.Contains("web"))
            {
                if (recArr.Length != 6)
                {
                    Console.WriteLine("The CSV file has format error!");
                    return false;
                }
            }
            else
            {
                if (recArr.Length != 7)
                {
                    Console.WriteLine("The CSV file has format error!");
                    return false;
                }
            }
            return true;
        }
        private static int getIndexFromCSVHeaderByKeyword(CsvReader csv, string keyword)
        {
            string[] header = csv.GetFieldHeaders();
            foreach (string fieldName in header)
            {
                if(fieldName.Contains(keyword))
                {
                    return csv.GetFieldIndex(fieldName);
                }
            }
            return -1;
        }
        private static DoGirderMbr getGirderMbr(DoGirderMbrList girderList, string girderType, string girderNo)
        {
            DoGirderMbr mbr = null;
            girderList.First();
            for (int i = 0; i < girderList.GetCount(); i++)
            {
                mbr = girderList.GetNext();
                string name = mbr.GetName().GetValue();
                if (name.ToLower().Contains(girderType) && name.ToLower().Contains(girderNo))
                {
                    return mbr;
                }
            }
            return mbr;
        }
        public static void updateDeteriorationProfileOfSteelAlt(string br_fk_id, string csvFile)
        {
            try
            {
                if (BRIDGEWareConverter.connectDB())
                {
                    DoBridge doBr = BRIDGEWareConverter.getBridgeFromDBByAgencyCode(br_fk_id);
                    if (null == doBr)
                    {
                        Console.WriteLine("Find no bridge model under the bridge ID: " + br_fk_id);
                        return;
                    }
                    //Set more properties......
                    //Superstructure......
                    DoSuperStructDefList superDefList = doBr.GetSuperStructDefList();
                    Console.WriteLine("Super Def Count: " + superDefList.GetCount());
                    //Girder System......
                    DoGirderSystemStructDef structDef = null;
                    int plType = 0;
                    if (superDefList.GetCount() > 0)
                    {
                        superDefList.First();
                        structDef = superDefList.GetNext(ref plType) as DoGirderSystemStructDef;
                    }
                    else
                    {
                        Console.WriteLine("The superstructure defination is empty!");
                        return;
                    }
                    object vaUnit = BRIDGEWareConstants.U_FT;
                    object percentUnit = BRIDGEWareConstants.U_PERCENT;
                    Console.WriteLine("Get the Girder System Struct Def: " + structDef);

                    //Girder Members......
                    DoGirderMbrList girderList = structDef.GetGirderMbrList();
                    int iGirderCount = girderList.GetCount();
                    if (iGirderCount < 1)
                    {
                        Console.WriteLine("The girder definations are empty!");
                        return;
                    }
                    CsvReader csv = new CsvReader(new StreamReader(csvFile), true);
                    CsvReader.RecordEnumerator csvEnum = csv.GetEnumerator();
                    while (csvEnum.MoveNext())
                    {
                        string[] recArr = csvEnum.Current;
                        if (!isValidateSecLossFile(recArr, csvFile))
                        {
                            continue;
                        }
                        string girderType = recArr[0].Contains("1") ? "interior" : "external";
                        string girderNo = recArr[1];
                        DoGirderMbr mbr = getGirderMbr(girderList, girderType, girderNo);
                        if (null == mbr)
                        {
                            Console.WriteLine("Cannot locate the girder member!");
                            continue;
                        }
                        DoGirderMbrAltList mbrAltList = mbr.GetGirderMbrAltList();
                        if (mbrAltList.GetCount() > 0)
                        {
                            mbrAltList.First();
                            DoGirderMbrAlt mbrAlt = mbrAltList.GetNext();
                            int mbrDefType = 0;
                            DoSteelPlateBeamDef stlPlateBeamDef = (DoSteelPlateBeamDef)mbrAlt.GetSpngMbrDef(ref mbrDefType);
                            int thickIndex = getIndexFromCSVHeaderByKeyword(csv, "Thickness");
                            string thickPercent = recArr[thickIndex];
                            int lengthIndex = getIndexFromCSVHeaderByKeyword(csv, "Length");
                            string length = recArr[lengthIndex];
                            int distanceIndex = getIndexFromCSVHeaderByKeyword(csv, "Distance");
                            string distance = recArr[distanceIndex];
                            int spanIndex = getIndexFromCSVHeaderByKeyword(csv, "Span");
                            string spanNo = recArr[spanIndex];
                            if (csvFile.Contains("web"))
                            {
                                DoSteelWebPlateLossRangeSet webLossRangeSet = stlPlateBeamDef.GetWebPlateLossRangeSet();
                                int rangeID = webLossRangeSet.Add();
                                webLossRangeSet.GetLength().SetValue(QCS.Utils.Utils.toDouble(length), vaUnit);
                                webLossRangeSet.GetDistance().SetValue(QCS.Utils.Utils.toDouble(distance), vaUnit);
                                webLossRangeSet.GetPercentThickLoss().SetValue(QCS.Utils.Utils.toDouble(thickPercent), percentUnit);
                            }
                            else
                            {
                                DoSteelFlangePlateLossRangeSet flangeLossRangeSet = stlPlateBeamDef.GetFlangePlateLossRangeSet();
                                int rangeID = flangeLossRangeSet.Add();
                                int widthIndex = getIndexFromCSVHeaderByKeyword(csv, "Widthness");
                                string widthPercent = recArr[widthIndex];
                                if (csvFile.ToLower().Contains("tf"))
                                {
                                    flangeLossRangeSet.GetTopLocationIndicator().SetValue(true);
                                }
                                flangeLossRangeSet.GetLength().SetValue(QCS.Utils.Utils.toDouble(length), vaUnit);
                                flangeLossRangeSet.GetDistance().SetValue(QCS.Utils.Utils.toDouble(distance), vaUnit);
                                flangeLossRangeSet.GetPercentThickLoss().SetValue(QCS.Utils.Utils.toDouble(thickPercent), percentUnit);
                                flangeLossRangeSet.GetPercentWidthLoss().SetValue(QCS.Utils.Utils.toDouble(widthPercent), percentUnit);
                            }
                        }
                    }
                    //End Setting......
                    bool bSave = doBr.Save();
                    //brware.EndSession();
                    BRIDGEWareConverter.endSession();
                }

            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
        }
        public static void updateStrandLossOfConAlt(string br_fk_id, string csvFile)
        {
            try
            {
                if (BRIDGEWareConverter.connectDB())
                {
                    DoBridge doBr = BRIDGEWareConverter.getBridgeFromDBByAgencyCode(br_fk_id);
                    if (null == doBr)
                    {
                        Console.WriteLine("Find no bridge model under the bridge ID: " + br_fk_id);
                        return;
                    }
                    //Set more properties......
                    //Superstructure......
                    DoSuperStructDefList superDefList = doBr.GetSuperStructDefList();
                    Console.WriteLine("Super Def Count: " + superDefList.GetCount());
                    //Girder System......
                    DoGirderSystemStructDef structDef = null;
                    int plType = 0;
                    if (superDefList.GetCount() > 0)
                    {
                        superDefList.First();
                        structDef = superDefList.GetNext(ref plType) as DoGirderSystemStructDef;
                    }
                    else
                    {
                        Console.WriteLine("The superstructure defination is empty!");
                        return;
                    }
                    object vaUnit = BRIDGEWareConstants.U_FT;
                    object kipUnit = BRIDGEWareConstants.U_KIPS;
                    Console.WriteLine("Get the Girder System Struct Def: " + structDef);
                    
                    //Girder Members......
                    DoGirderMbrList girderList = structDef.GetGirderMbrList();
                    int iGirderCount = girderList.GetCount();
                    if (iGirderCount < 1)
                    {
                        Console.WriteLine("The girder definations are empty!");
                        return;
                    }
                    CsvReader csv = new CsvReader(new StreamReader(csvFile), true);
                    CsvReader.RecordEnumerator csvEnum = csv.GetEnumerator();
                    while (csvEnum.MoveNext())
                    {
                        string[] recArr = csvEnum.Current;
                        string girderType = recArr[0].Contains("1") ? "interior" : "external";
                        string girderNo = recArr[1];
                        DoGirderMbr mbr = getGirderMbr(girderList, girderType, girderNo);
                        if (null == mbr)
                        {
                            Console.WriteLine("Cannot locate the girder member!");
                            continue;
                        }
                        DoGirderMbrAltList mbrAltList = mbr.GetGirderMbrAltList();
                        if (mbrAltList.GetCount() > 0)
                        {
                            mbrAltList.First();
                            DoGirderMbrAlt mbrAlt = mbrAltList.GetNext();
                            int mbrDefType = 0;
                            DoPsPrecastIBeamDef beamDef = (DoPsPrecastIBeamDef)mbrAlt.GetSpngMbrDef(ref mbrDefType);
                            int spanIndex = getIndexFromCSVHeaderByKeyword(csv, "Span");
                            string spanNo = recArr[spanIndex];
                            short spanNum = Convert.ToInt16(QCS.Utils.Utils.toInt(spanNo));
                            DoPsPrecastBeamSpan span = beamDef.GetSpan(spanNum);
                            if (null == span)
                            {
                                Console.WriteLine("Cannot locate the indicated span!");
                                continue;
                            }
                            DoPsPrecastStrandLayoutSet strandLayoutSet = span.GetStrandLayoutSet();
                            int forceIndex = getIndexFromCSVHeaderByKeyword(csv, "Force");
                            string force = recArr[forceIndex];
                            span.GetForce().SetValue(QCS.Utils.Utils.toInt(force), kipUnit);

                   
                        }
                    }
                    //End Setting......
                    bool bSave = doBr.Save();
                    //brware.EndSession();
                    BRIDGEWareConverter.endSession();
                }

            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
        }
        public static bool importConcreteBridgeFromXml(string xmlFile)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                if (BRIDGEWareConverter.connectDB())
                {
                    xmlDoc.Load(xmlFile);
                    string br_fk_id = BRIDGEWareConverter.getBrFkIDFromXmlFile(xmlDoc);
                    DoBridge doBr = BRIDGEWareConverter.getBridgeFromDBByAgencyCode(br_fk_id);
                    if (null == doBr)
                    {
                        doBr = BRIDGEWareConverter.getConcTemplateBridge();
                        Console.WriteLine("Create a new bridge......");
                        string structID = br_fk_id.Length > 15 ? br_fk_id.Substring(0, 15) : br_fk_id;
                        Console.WriteLine("Set Bridge Struct Num: " + doBr.GetStructNum().SetValue(structID));
                        string agencyCode = br_fk_id.Length > 30 ? br_fk_id.Substring(0, 30) : br_fk_id;
                        Console.WriteLine("Set Bridge Agency Code: " + doBr.GetAgencyCode().SetValue(agencyCode));
                    }
                    string brName = BRIDGEWareConverter.getBrNameFromXmlFile(xmlDoc);
                    Console.WriteLine("Set Bridge Name: " + doBr.GetName().SetValue(brName));
                    //Set more properties......
                    //Superstructure......
                    DoSuperStructDefList superDefList = doBr.GetSuperStructDefList();
                    Console.WriteLine("Super Def Count: " + superDefList.GetCount());
                    //Girder System......
                    DoGirderSystemStructDef structDef = null;
                    int plType = 0;
                    if (superDefList.GetCount() > 0)
                    {
                        superDefList.First();                        
                        structDef = superDefList.GetNext(ref plType) as DoGirderSystemStructDef;
                    }
                    else
                    {
                        structDef = superDefList.Add(BRIDGEWareConstants.TYP_STRDEF_GIRDER) as DoGirderSystemStructDef;
                    }
                    Console.WriteLine("Get the Girder System Struct Def: " + structDef);
                    structDef.GetName().SetValue("Concrete");
                    //Girder Members......
                    DoGirderMbrList girderList = structDef.GetGirderMbrList();
                    int iGirderCount = girderList.GetCount();
                    Console.WriteLine("Curr Girder Number: " + iGirderCount);
                    girderList.First();
                    for (int i = 0; i < iGirderCount; i++)
                    {
                        DoGirderMbr mbr = girderList.GetNext();
                        Console.WriteLine("Delete Original Mbrs: " + mbr.Delete());
                    }
                    Console.WriteLine("Curr Girder Number: " + girderList.GetCount());
                    ArrayList girderInfos = ImportingConvert.convertToPrecastGirderInfos(xmlFile);
                    DoSpngMbrDefList spngMbrDefList = structDef.GetSpngMbrDefList();
                    
                    foreach (PrecastGirderInfo girderInfo in girderInfos)
                    {
                        DoGirderMbr girder = girderList.Add();

                        //DoGirderMbrAltList girderMbrAltList = girder.GetGirderMbrAltList();
                        
                        //DoGirderMbrAlt girderMbrAlt = girderMbrAltList.Add();
                        Console.WriteLine("Girder New(): " + girder);
                        Console.WriteLine("Set Girder Name:" + girder.GetName().SetValue(girderInfo.getName()));
                        Console.WriteLine("GirderName: " + girder.GetName().GetValue());
                        int lpMbrDefType = BRIDGEWareImporting.BRIDGEWareConstants.TYP_MBRDEF_PSPRECASTIBEAM;
                        //bool b = girderMbrAlt.SetSpngMbrDefId(lpMbrDefType);
                        //DoPsPrecastIBeamDef beamDef = (DoPsPrecastIBeamDef)spngMbrDefList.Add(lpMbrDefType);
                        //beamDef.GetName().SetValue(girderInfo.getName() + "_MbrAlt");

                        //DoPsPrecastBeamSpan beamSpan = beamDef.AddSpan();
                        //DoPsPrecastStrandLayoutSet strandLayoutSet = beamSpan.GetStrandLayoutSet();
                        //strandLayoutSet.Add(1, 2);
                        
                        //strandLayoutSet.MoveFirst();
                        //Girder Member Alternative......
                    }

                    //End Setting......
                    bool bSave = doBr.Save();
                    //brware.EndSession();
                    BRIDGEWareConverter.endSession();
                    return true;
                }

            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
            return false;
        }
        public static void importSteelBridgeModel()
        {
            //string templateFile = "D:\\xml_template_VirtiOpis.xml";

            string sXmlFile = @"D:\Exporting_Linkage_Bridge29_Clear_Demo.db1_FromTekla.xml";
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
