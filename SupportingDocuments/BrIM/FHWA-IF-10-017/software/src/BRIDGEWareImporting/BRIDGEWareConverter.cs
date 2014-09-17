using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using AbaMdModelDomain;
using abobrdg;
using abosys;
using System.Collections;
using QImporting.Utils;
using QImporting.Beans;
using abedata;
using QCS.Utils;

namespace BRIDGEWareImporting
{
    public class BRIDGEWareConverter
    {
        private static BRIDGEWare brware;
        private static DoSysBridgeManager doBrMgr = null;
        public static bool convertToBRIDGEWare(string tXmlFile, string templateFile)
        {

            //File.Copy(templateFile, tXmlFile, true);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(templateFile);
            XmlComment xmlComment = xmlDoc.CreateComment("Try to write to the xml file...");

            XmlElement xmlRoot = xmlDoc.DocumentElement;
            xmlDoc.InsertBefore(xmlComment, xmlRoot);

            XmlNode bridgeNode = xmlDoc.SelectSingleNode("bridge_");
            //XmlElement xmlTryElem = xmlDoc.CreateElement("TryingFlag");
            //xmlRoot.AppendChild(xmlTryElem);

            xmlDoc.Save(tXmlFile);
            return true;
        }
        public static void createNewBridge()
        {


        }
        public static bool connectDB()
        {
            //IBRIDGEWare m_IBRIDGEWare;

            string sOdbcDataSource = "OpisVirtis60_Samples";
            string sUsername = "virtis";
            string sPassword = "virtis";
            brware = new BRIDGEWare();
            short iRetCode = brware.StartSession(sUsername, sPassword, sOdbcDataSource);
            if (iRetCode != BRIDGEWareConstants.ABW_SUCCESS)
            {
                Console.WriteLine("iRetCode : " + iRetCode);
                Console.WriteLine("Unable to StartSession!");
                return false;
            }
            doBrMgr = brware.GetBridgeManager();
            return true;
        }
        public static void endSession()
        {
            brware.EndSession();
        }
        public static bool retrieveBridge(string brName)
        {
            try
            {
                if (connectDB())
                {
                    DoBridgeList brList = new DoBridgeList();
                    if (!brList.Create())
                    {
                        return false;
                    }
                    int brID = 0;

                    Console.WriteLine("Super Struct Count : " + brList.GetCount());
                    string bstrCriteria = "";
                    Console.WriteLine("doBrList Count: " + brList.Requery(0, ref bstrCriteria, false, false));
                    for (; brList.MoveNext(); )
                    {
                        if (brName.Equals(brList.GetBridgeName().GetValue()))
                        {
                            brID = brList.GetBridgeId().GetValue();
                            break;
                        }
                    }
                    DoBridge doBr = doBrMgr.RetrieveBridge(brID);
                    IDoSuperStructDefList superStructList = doBr.GetSuperStructDefList();
                    ;
                    Console.WriteLine("Super Struct Count : " + superStructList.GetCount());

                    Console.WriteLine("Super Struct First : " + superStructList.First());
                    int type = BRIDGEWareConstants.TYP_STRDEF_GIRDER;

                    DoGirderSystemStructDef structDef = superStructList.GetNext(ref type) as DoGirderSystemStructDef;
                    if (null != structDef)
                    {
                        Console.WriteLine("Get Item: " + structDef);
                        DoReferenceLine refLine = structDef.GetReferenceLine();
                        Console.WriteLine("Ref Line Type: " + refLine.GetLineType().GetCurrentTypeId());
                        Console.WriteLine("X:::Y:::Z: " + refLine.GetX().GetValue(BRIDGEWareConstants.U_MM) + ":::" + refLine.GetY().GetValue(BRIDGEWareConstants.U_MM) + ":::" + refLine.GetZ().GetValue(BRIDGEWareConstants.U_MM));
                        Console.WriteLine("can modify: " + refLine.CanModifyObject());
                        Console.WriteLine("Ref Line Type: " + refLine.GetLineType().ToString());
                        DoGirderMbrList girderMbrList = structDef.GetGirderMbrList();
                        Console.WriteLine("Girder Member list: " + girderMbrList.GetCount());
                        girderMbrList.First();
                        DoGirderMbr girder1 = girderMbrList.GetNext();
                        Console.WriteLine("Set Girder one Name: " + girder1.GetName().SetValue("G01"));
                        DoGirderMbr newGirder = girderMbrList.Add();
                        Console.WriteLine("Set New Girder name: " + newGirder.GetName().SetValue("G03"));
                        DoSupportLineSet supportLineSet = structDef.GetSupportLineSet();
                        Console.WriteLine("Unit: " + structDef.GetUnitsType().GetCurrentTypeId());
                        Console.WriteLine("Support Line Set Count: " + supportLineSet.GetCount());

                        //Console.WriteLine("Set Span Length: " + structDef.SetSpanLength(0,40.0,BRIDGEWareConstants.U_M));
                        Console.WriteLine("Set Span Length: " + structDef.SetSpanLength(1, 50.0, BRIDGEWareConstants.U_M));
                        //Console.WriteLine("Add New Span: " + structDef.AddSpan(60.0, BRIDGEWareConstants.U_M));
                        //Console.WriteLine("Delete Span: " + structDef.DeleteSpan(BRIDGEWareConstants.ABW_RIGHT_MOST_SPAN));
                        Console.WriteLine("SupportLine Set count: " + structDef.GetSupportLineSet().GetCount());
                        //int girderOID2 = structDef.AddGirder(10.0, 8.0, BRIDGEWareConstants.U_M);
                        //Console.WriteLine("Set Span Length: " + structDef.SetSpanLength(2, 50.0, BRIDGEWareConstants.U_M));
                        //Console.WriteLine("Add an Girder: " + girderOID2);
                        //End Setting......
                    }
                    DoGirderSystemStructDef structDef2 = superStructList.GetNext(ref type) as DoGirderSystemStructDef;
                    Console.WriteLine("Next Supper Structure: " + structDef2);
                    Console.WriteLine("Br Save after importing Xml File: " + doBr.Save());
                    brware.EndSession();
                    return true;
                }

            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
            return false;
        }
        public static bool importBridgeFromXml(string xmlFile)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                if (connectDB())
                {
                    xmlDoc.Load(xmlFile);
                    string br_fk_id = getBrFkIDFromXmlFile(xmlDoc);
                    DoBridge doBr = getBridgeFromDBByAgencyCode(br_fk_id);
                    if (null == doBr)
                    {
                        doBr = getTemplateBridge();
                        Console.WriteLine("Create a new bridge......");
                        string structID = br_fk_id.Length > 15 ? br_fk_id.Substring(0, 15) : br_fk_id;
                        Console.WriteLine("Set Bridge Struct Num: " + doBr.GetStructNum().SetValue(structID));
                        string agencyCode = br_fk_id.Length > 30 ? br_fk_id.Substring(0, 30) : br_fk_id;
                        Console.WriteLine("Set Bridge Agency Code: " + doBr.GetAgencyCode().SetValue(agencyCode));
                    }
                    string brName = getBrNameFromXmlFile(xmlDoc);
                    Console.WriteLine("Set Bridge Name: " + doBr.GetName().SetValue(brName));
                    //Set more properties......
                    //Superstructure......
                    DoSuperStructDefList superDefList = doBr.GetSuperStructDefList();
                    Console.WriteLine("Super Def Count: " + superDefList.GetCount());
                    //Girder System......
                    DoGirderSystemStructDef structDef = null;
                    if (superDefList.GetCount() > 0)
                    {
                        superDefList.First();
                        int plType = BRIDGEWareConstants.TYP_STRDEF_GIRDER;
                        structDef = superDefList.GetNext(ref plType) as DoGirderSystemStructDef;
                    }
                    else
                    {
                        structDef = superDefList.Add(BRIDGEWareConstants.TYP_STRDEF_GIRDER) as DoGirderSystemStructDef;
                    }
                    Console.WriteLine("Get the Girder System Struct Def: " + structDef);
                    structDef.GetName().SetValue("Steel");
                    //Girder Members......
                    DoGirderMbrList girderList = structDef.GetGirderMbrList();
                    int iGirderCount = girderList.GetCount();
                    Console.WriteLine("Curr Girder Number: " + iGirderCount);
                    girderList.First();
                    for (int i = 0 ; i < iGirderCount ; i++)
                    {
                        DoGirderMbr mbr = girderList.GetNext();
                        //Console.WriteLine("Delete Original Mbrs: " + mbr.Delete());
                    }
                    Console.WriteLine("Curr Girder Number: " + girderList.GetCount());
                    //ArrayList girderInfos = ImportingConvert.convertToPolyGirderInfos(xmlFile);
                    //foreach (IPolyGirderInfo girderInfo in girderInfos)
                    //{
                    //    //DoGirderMbr girder = girderList.Add();
                    //    //Console.WriteLine("Girder New(): " + girder);
                    //    //Console.WriteLine("Set Girder Name:" + girder.GetName().SetValue(girderInfo.getName()));
                    //    //Console.WriteLine("GirderName: " + girder.GetName().GetValue());
                    //    //Girder Member Alternative......
                    //}
                    //Span......
                    //DoSupportLineSet supportLineSet = structDef.GetSupportLineSet();
                    //Console.WriteLine("Support Line Set count: " + supportLineSet.GetCount());
                    //Console.WriteLine("Support Line Set delete all: " + supportLineSet.DeleteAll());
                    //Console.WriteLine("Support Line Set count: " + supportLineSet.GetCount());
                    //object vaUnit = BRIDGEWareConstants.U_MM;
                    //supportLineSet.MoveFirst();
                    //for (; supportLineSet.MoveNext(); )
                    //{
                    //    DoReferenceLine refLine = supportLineSet.GetReferenceLine();
                        
                    //    Console.WriteLine("Ref direction angle X:: Y :: : " + refLine.GetDirectionAngleX().GetValue(vaUnit) + "::" + refLine.GetDirectionAngleY().GetValue(vaUnit) + "::" + refLine.GetDirectionAngleZ().GetValue(vaUnit));
                    //    Console.WriteLine("Ref Line X:: Y :: : " + refLine.GetX().GetValue(vaUnit) + "::" + refLine.GetY().GetValue(vaUnit) + "::" + refLine.GetZ().GetValue(vaUnit));
                    //}
                    //supportLineSet.DeleteAll();
                    //ArrayList supportLineInfoList = ImportingConvert.convertSupportLineInfoList(xmlDoc);
                    //foreach(LayoutLineInfo lineInfo in supportLineInfoList)
                    //{
                    //    PointInfo pointInfo = lineInfo.getStPoint();
                        
                    //    Console.WriteLine("Ref direction angle X:: Y :: : " + supportLineSet.Add());
                    //    DoReferenceLine refline = supportLineSet.GetReferenceLine();
                    //    Console.WriteLine("Ref Line set X: " + refline.GetX().SetValue(pointInfo.getY(), vaUnit));
                    //    Console.WriteLine("Ref Line set Y: " + refline.GetY().SetValue(pointInfo.getX(), vaUnit));
                    //    Console.WriteLine("Ref Line set Z: " + refline.GetZ().SetValue(pointInfo.getZ(), vaUnit));
                        
                    //    //Console.WriteLine("Ref Line direction angle X: " + refline.GetDirectionAngleX().SetValue(1.57, vaUnit));
                    //    //Console.WriteLine("Ref Line direction angle Y: " + refline.GetDirectionAngleY().SetValue(1.57, vaUnit));
                    //    //Console.WriteLine("Ref Line direction angle Z: " + refline.GetDirectionAngleZ().SetValue(3.14, vaUnit));
                    //}
                    //supportLineSet.MoveFirst();
                    //for (; supportLineSet.MoveNext(); )
                    //{
                    //    DoReferenceLine refLine = supportLineSet.GetReferenceLine();
                    //    Console.WriteLine("Ref direction angle X:: Y :: : " + refLine.GetDirectionAngleX().GetValue(vaUnit) + "::" + refLine.GetDirectionAngleY().GetValue(vaUnit) + "::" + refLine.GetDirectionAngleZ().GetValue(vaUnit));
                    //    Console.WriteLine("Ref Line X:: Y :: : " + refLine.GetX().GetValue(vaUnit) + "::" + refLine.GetY().GetValue(vaUnit) + "::" + refLine.GetZ().GetValue(vaUnit));
                    //}
                    //short numSpans = 0;
                    //Console.WriteLine("Compute Number of Spans : " + structDef.ComputeNumSpans(ref numSpans) + ":::" + numSpans);
                    //Console.WriteLine("ReCompute the number of Spans: " + structDef.ComputeNumSpans(ref oriSpanNum) + ":::" + oriSpanNum);
                    //Material......
                    //DoMatlConcreteList matlConcreteList = doBr.GetMatlConcreteList();
                    //DoMatlConcrete matlconcrete = matlConcreteList.Add();

                    //Console.WriteLine("Set Material Concrete 5000" + matlconcrete.GetName().SetValue("5000"));
                    

                    //End Setting......
                     Console.WriteLine("Br Save after importing Xml File: " + doBr.Save());
                    brware.EndSession();
                    return true;
                }

            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
            return false;
        }
        public static string getBrFkIDFromXmlFile(XmlDocument xmlDoc)
        {
            XmlNode brNode = xmlDoc.SelectSingleNode(BRIDGEWareConstants.BR_NODE_NAME);
            string br_fk_id = getAttrValueFromNode(brNode, QExportingConstants.XML_ID_ATTR);
            return br_fk_id;
        }
        public static string getBrNameFromXmlFile(XmlDocument xmlDoc)
        {
            XmlNode brNode = xmlDoc.SelectSingleNode(BRIDGEWareConstants.BR_NODE_NAME);
            string brName = getAttrValueFromNode(brNode, QExportingConstants.XML_NAME_ATTR);
            return brName;
        }
        public static string getAttrValueFromNode(XmlNode xmlNode, string attrName)
        {
            XmlAttributeCollection attrCollection = xmlNode.Attributes;
            IEnumerator ie = attrCollection.GetEnumerator();
            foreach (XmlAttribute attr in attrCollection)
            {
                if (attr.Name.Equals(attrName))
                {
                    return attr.Value;
                }
            }
            return "";
        }
        public static DoBridge getBridgeFromDBByAgencyCode(string br_fk_id)
        {
            DoBridgeList doBrList = new DoBridgeList();
            try
            {
                Console.WriteLine("doBrList Creation: " + doBrList.Create());
                int iCount = doBrList.GetCount();
                Console.WriteLine("doBrList Count: " + iCount);
                string bstrCriteria = "";
                Console.WriteLine("doBrList Count: " + doBrList.Requery(0, ref bstrCriteria, false, false));
                for (; doBrList.MoveNext(); )
                {
                    string agencyCode = doBrList.GetAgencyCode().GetValue();
                    if (agencyCode.Equals(br_fk_id))
                    {
                        Console.WriteLine("Have found the bridge......");
                        int bridgeID = doBrList.GetBridgeId().GetValue();

                        Console.WriteLine("Curr bridge ID : " + bridgeID);
                        DoBridge currBr = doBrMgr.RetrieveBridge(bridgeID);
                        return currBr;
                    }
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
            return null;

        }
        public static DoBridge getTemplateBridge()
        {
            //DoBridgeList doBrList = new DoBridgeList();            
            try
            {
                DoBridge doBr = doBrMgr.CreateNewBridge();

                Console.WriteLine("Imporing Xml Data: " + doBr.ImportXMLData(BRIDGEWareConstants.NEW_BR_TPL_FILENAME));

                return doBr;
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
            return null;

        }
        public static DoBridge getConcTemplateBridge()
        {
            try
            {
                DoBridge doBr = doBrMgr.CreateNewBridge();

                Console.WriteLine("Imporing Xml Data: " + doBr.ImportXMLData(BRIDGEWareConstants.NEW_CONC_BR_TPL_FILENAME));

                return doBr;
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
            return null;
        }
    }
}
