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
using abocncb;

namespace BRIDGEWareExporting
{
    public static class ProgramRunner
    {
        public static string ExportReinforcements( string brName )
        {
            StringBuilder sb = new StringBuilder();
            BRIDGEWare brware = BRIDGEWareConverter.connectDB();
            if (null == brware)
            {
                sb.Append("Connnecting DB failed......\n");
                return sb.ToString() ;
            }
            DoSysBridgeManager doBrMgr = brware.GetBridgeManager();
            DoBridge doBr = BRIDGEWareConverter.getBridgeObjByName(doBrMgr, brName);
            if (null == doBr)
            {
                sb.Append("Failed to find the bridge with name: ").Append(brName);
                return sb.ToString();
            }
            DoGirderSystemStructDef superDef = BRIDGEWareConverter.getGiderSystemStructDefFromBridgeObj(doBr);
            sb.Append("Girder Spacing Display Type ID: " + superDef.GetGirderSpacingDisplayType().GetCategoryDescription());
            if (null == superDef)
            {
                sb.Append("Failed to find the superstructure definition......\n");
                return sb.ToString();
            }
            DoGirderMbrList girderMbrList = superDef.GetGirderMbrList();
            girderMbrList.First();
            DoGirderMbr girderMbr = girderMbrList.GetNext();
            DoGirderMbrAlt girderMbrAlt = girderMbr.GetCurrentMbrAltDef();
            sb.Append("Girder Member List: ").Append(girderMbrList.GetCount()).Append("\n");
            while (null != girderMbr)
            {
                sb.Append("Girder Member Name: ").Append(girderMbr.GetName().GetValue()).Append("\n");
                DoGirderMbrAlt mbrAlt = girderMbr.GetCurrentMbrAltDef();
                if (null == mbrAlt)
                {
                    break;
                }
                int defId = mbrAlt.GetSpngMbrDefId();
                int lpMbrDefType = 0;
                mbrAlt.GetSpngMbrDef(ref lpMbrDefType);
                if (lpMbrDefType == BRIDGEWareImporting.BRIDGEWareConstants.TYP_MBRDEF_PSPRECASTIBEAM)
                {
                    DoPsPrecastIBeamDef beamDef = (DoPsPrecastIBeamDef)mbrAlt.GetSpngMbrDef(ref lpMbrDefType);
                    short spanNum = beamDef.GetNumSpan();
                    sb.Append("This Bridge Girder Member has the number of Spans: ").Append(spanNum).Append("\n");
                    short firstSpanNum = 1;
                    DoPsPrecastBeamSpan beamSpan = beamDef.GetSpan(firstSpanNum);
                    DoPsPrecastStrandLayoutSet strandLayoutSet = beamSpan.GetStrandLayoutSet();
                    short strandLayoutCount = strandLayoutSet.GetCount();
                    sb.Append("The count of strand layout set of span ").Append(firstSpanNum).Append(" : ").Append(strandLayoutCount).Append("\n");
                    strandLayoutSet.MoveFirst();
                    for (int i = 0; strandLayoutCount > i; i++)
                    {
                        short iRow = 0;
                        short iCol = 0;
                        strandLayoutSet.GetPosition(ref iRow, ref iCol);
                        //Row Number starts from bottom...
                        //Column Number starts from left...
                        sb.Append("The Row Number of strand layout  : ").Append(iRow).Append("\n");
                        sb.Append("The Column Number of strand layout : ").Append(iCol).Append("\n");
                        strandLayoutSet.MoveNext();
                    }
                }
                object vaUnit = BRIDGEWareConstants.U_MM;

                girderMbr = girderMbrList.GetNext();
            }

            return sb.ToString();
        }
        public static void ExportSomething()
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
