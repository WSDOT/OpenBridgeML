using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using abobrdg;
using abosys;
using System.Collections;
using QImporting.Utils;
using QImporting.Beans;
using abedata;
using QCS.Utils;
using BRIDGEWareImporting;
using abostld;

namespace BRIDGEWareExporting
{
    public class BRIDGEWareConverter
    {
        public static BRIDGEWare connectDB()
        {
            //IBRIDGEWare m_IBRIDGEWare;

            string sOdbcDataSource = "OpisVirtis60_Samples";
            string sUsername = "virtis";
            string sPassword = "virtis";
            BRIDGEWare brware = new BRIDGEWare();
            short iRetCode = brware.StartSession(sUsername, sPassword, sOdbcDataSource);
            if (iRetCode != BRIDGEWareConstants.ABW_SUCCESS)
            {
                Console.WriteLine("iRetCode : " + iRetCode);
                Console.WriteLine("Unable to StartSession!");
            }
            return brware;
        }
        public static DoBridge getBridgeObjByName(DoSysBridgeManager doBrMgr, string brName)
        {
            try
            {
                //Get the list of All the bridges......
                DoBridgeList brList = new DoBridgeList();
                if (!brList.Create())
                {
                    Console.WriteLine("Cannot create Bridge Object list......");
                    return null;
                }
                int brID = -1;
                Console.WriteLine("Bridge List Total Count : " + brList.GetCount());
                //Get a smaller list by criteria......
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
                //if it cannot be found, then return......
                if (brID < 0)
                {
                    Console.WriteLine("Cannot find the indicated bridge!");
                    return null;
                }
                //get DoBridge object by bridgeID......
                DoBridge doBr = doBrMgr.RetrieveBridge(brID);
                return doBr;
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
            return null;
        }
        public static DoGirderSystemStructDef getGiderSystemStructDefFromBridgeObj(DoBridge doBr)
        {
            //get super structure definition list......
            IDoSuperStructDefList superStructList = doBr.GetSuperStructDefList();
            Console.WriteLine("Super Struct Count : " + superStructList.GetCount());
            Console.WriteLine("Super Struct First : " + superStructList.First());
            int type = BRIDGEWareConstants.TYP_STRDEF_GIRDER;
            //get Girder System Structure Definition object......
            DoGirderSystemStructDef structDef = superStructList.GetNext(ref type) as DoGirderSystemStructDef;
            return structDef;
        }
        public static DoBridge getBridgeFromDBByAgencyCode(DoSysBridgeManager doBrMgr, string br_fk_id)
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
        public static IPolyGirderInfo toIPolyGirderInfo(DoSteelWebPlateRangeSet webRangeSet, DoSteelFlangePlateRangeSet tfRangeSet, DoSteelFlangePlateRangeSet bfRangeSet, object vaUnit)
        {
            IPolyGirderInfo girderInfo = new IPolyGirderInfo();
            ArrayList tfPlateInfoArr = toIPolyGirderPlateInfoList(tfRangeSet, vaUnit);
            ArrayList bfPlateInfoArr = toIPolyGirderPlateInfoList(bfRangeSet, vaUnit);
            ArrayList webPlateInfoArr = toIPolyGirderPlateInfoList(webRangeSet, vaUnit);
            girderInfo.setTfPlates(tfPlateInfoArr);
            girderInfo.setBfPlates(bfPlateInfoArr);
            girderInfo.setWebPlates(webPlateInfoArr);
            return girderInfo;
        }
        public static ArrayList toIPolyGirderPlateInfoList(DoSteelWebPlateRangeSet rangeSet, object vaUnit)
        {
            ArrayList plateInfoArr = new ArrayList();
            bool hasNext = rangeSet.MoveFirst();
            while (hasNext)
            {
                double width = rangeSet.GetThick().GetValue(vaUnit);
                double depth = rangeSet.GetDepthStart().GetValue(vaUnit);
                IPolyGirderPlateInfo plateInfo = new IPolyGirderPlateInfo();
                RSectionInfo sectionInfo = new RSectionInfo();
                sectionInfo.setDepth(depth);
                sectionInfo.setWidth(width);
                plateInfo.setSectionInfo(sectionInfo);
                plateInfoArr.Add(plateInfo);
                hasNext = rangeSet.MoveNext();
            }
            return plateInfoArr;
        }
        public static ArrayList toIPolyGirderPlateInfoList(DoSteelFlangePlateRangeSet rangeSet, object vaUnit)
        {
            ArrayList plateInfoArr = new ArrayList();
            bool hasNext = rangeSet.MoveFirst();
            while (hasNext)
            {
                double depth = rangeSet.GetThick().GetValue(vaUnit);
                double width = rangeSet.GetWidthStart().GetValue(vaUnit);
                IPolyGirderPlateInfo plateInfo = new IPolyGirderPlateInfo();
                RSectionInfo sectionInfo = new RSectionInfo();
                sectionInfo.setDepth(depth);
                sectionInfo.setWidth(width);
                plateInfo.setSectionInfo(sectionInfo);
                plateInfoArr.Add(plateInfo);
                hasNext = rangeSet.MoveNext();
            }
            return plateInfoArr;
        }
        public static ArrayList updatePolyGirderInfoList(ArrayList oldGirderInfoList, ArrayList newGirderInfoList)
        {
            ArrayList girderInfoList = new ArrayList();
            Array newGirderInfoArr = newGirderInfoList.ToArray();
            Array oldGirderInfoArr = oldGirderInfoList.ToArray();
            for (int i = 0; i < newGirderInfoList.Count; i++)
            {
                IPolyGirderInfo oldGirderInfo = (IPolyGirderInfo)oldGirderInfoArr.GetValue(i);
                IPolyGirderInfo newGirderInfo = (IPolyGirderInfo)newGirderInfoArr.GetValue(i);
                girderInfoList.Add(updatePolyGirderInfo(oldGirderInfo, newGirderInfo));
            }
            return girderInfoList;
        }
        public static IPolyGirderInfo updatePolyGirderInfo(IPolyGirderInfo oldGirderInfo, IPolyGirderInfo newGirderInfo)
        {
            IPolyGirderInfo girderInfo = new IPolyGirderInfo();
            //web......            
            ArrayList oldWebPlateList = oldGirderInfo.getWebPlates();
            ArrayList newWebPlateList = newGirderInfo.getWebPlates();
            ArrayList webPlateList = updatePolyGirderPlateInfoList(oldWebPlateList, newWebPlateList);
            //TF...
            ArrayList oldTFPlateList = oldGirderInfo.getTfPlates();
            ArrayList newTFPlateList = newGirderInfo.getTfPlates();
            ArrayList tfPlateList = updatePolyGirderPlateInfoList(oldTFPlateList, newTFPlateList);
            //BF...
            ArrayList oldBFPlateList = oldGirderInfo.getBfPlates();
            ArrayList newBFPlateList = newGirderInfo.getBfPlates();
            ArrayList bfPlateList = updatePolyGirderPlateInfoList(oldBFPlateList, newBFPlateList);
            //Set to GirderInfo......
            girderInfo.setWebPlates(webPlateList);
            girderInfo.setTfPlates(tfPlateList);
            girderInfo.setBfPlates(bfPlateList);

            return girderInfo;
        }
        public static ArrayList updatePolyGirderPlateInfoList(ArrayList oldPlateInfoList, ArrayList newPlateInfoList)
        {
            ArrayList plateInfoList = new ArrayList();
            Array newPlateArr = newPlateInfoList.ToArray();
            Array oldPlateArr = oldPlateInfoList.ToArray();
            for (int i = 0; i < newPlateInfoList.Count; i++)
            {
                IPolyGirderPlateInfo oldPlateInfo = (IPolyGirderPlateInfo)oldPlateArr.GetValue(i);
                IPolyGirderPlateInfo newPlateInfo = (IPolyGirderPlateInfo)newPlateArr.GetValue(i);
                oldPlateInfo.setSectionInfo(newPlateInfo.getSectionInfo());
                plateInfoList.Add(oldPlateInfo);
            }
            return plateInfoList;
        }

    }
}
