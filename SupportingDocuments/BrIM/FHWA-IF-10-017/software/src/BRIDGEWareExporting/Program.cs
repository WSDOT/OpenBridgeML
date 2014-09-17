using System;
using System.Collections.Generic;
using System.Text;
using abosys;
using abobrdg;
using BRIDGEWareImporting;
using abostld;
using QImporting.Beans;
using System.Collections;
using QImporting.Utils;

namespace BRIDGEWareExporting
{
    class Program
    {
        static void Main(string[] args)
        {
            BRIDGEWare brware = BRIDGEWareConverter.connectDB();
            if (null == brware)
            {
                Console.WriteLine("Connnecting DB failed......");
                return;
            }
            DoSysBridgeManager doBrMgr = brware.GetBridgeManager();
            string brName = "Quincy Avenue bridge over I-25 & LRT";
            DoBridge doBr = BRIDGEWareConverter.getBridgeObjByName(doBrMgr, brName);
            if (null == doBr)
            {
                Console.WriteLine("Failed to find the bridge with name\"Quincy Avenue bridge over I-25 & LRT\"......");
                return;
            }
            DoGirderSystemStructDef superDef = BRIDGEWareConverter.getGiderSystemStructDefFromBridgeObj(doBr);
            Console.WriteLine("Girder Spacing Display Type ID: " + superDef.GetGirderSpacingDisplayType().GetCategoryDescription());
            if (null == superDef)
            {
                Console.WriteLine("Failed to find the superstructure definition......");
                return;
            }
            DoGirderMbrList girderMbrList = superDef.GetGirderMbrList();
            girderMbrList.First();
            DoGirderMbr girderMbr = girderMbrList.GetNext();
            ArrayList newGirderInfoList = new ArrayList();
            while (null != girderMbr)
            {
                DoGirderMbrAlt mbrAlt = girderMbr.GetCurrentMbrAltDef();
                if (null == mbrAlt)
                {
                    break;
                }
                int defId = mbrAlt.GetSpngMbrDefId();
                Console.WriteLine("Spng Mbr Def: " + mbrAlt.GetSpngMbrDef(ref defId));
                DoSteelPlateBeamDef beamDef = (DoSteelPlateBeamDef)mbrAlt.GetSpngMbrDef(ref defId);
                DoSteelWebPlateRangeSet webRangeSet = beamDef.GetWebPlateRangeSet();
                DoSteelFlangePlateRangeSet tfRangeSet = beamDef.GetTopFlangePlateRangeSet();
                DoSteelFlangePlateRangeSet bfRangeSet = beamDef.GetBottomFlangePlateRangeSet();
                Console.WriteLine("Web Plate Range Set Count:" + webRangeSet.GetCount());
                Console.WriteLine("TF Plate Range Set Count:" + tfRangeSet.GetCount());
                Console.WriteLine("BF Plate Range Set Count:" + bfRangeSet.GetCount());
                object vaUnit = BRIDGEWareConstants.U_MM;
                IPolyGirderInfo girderInfo = BRIDGEWareConverter.toIPolyGirderInfo(webRangeSet, tfRangeSet, bfRangeSet, vaUnit);
                newGirderInfoList.Add(girderInfo);

                girderMbr = girderMbrList.GetNext();
            }
            //Get Girder Infos. from BrIM model (xml)...
            string xmlFileName = @"D:\Exporting_Linkage_qc_steel_alt.db1.xml";
            ArrayList oldGirderInfoList = ImportingConvert.convertToPolyGirderInfos(xmlFileName);
            ArrayList girderInfoList = BRIDGEWareConverter.updatePolyGirderInfoList(oldGirderInfoList, newGirderInfoList);
            Console.ReadLine();
        }
    }
}
