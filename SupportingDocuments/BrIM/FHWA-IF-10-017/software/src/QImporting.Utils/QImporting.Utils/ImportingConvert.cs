using System;
using System.Collections.Generic;
using System.Text;
using QImporting.Beans;
using System.Xml;
using RevitImporting.Comps;
using System.Collections;
using TeklaBeans;
using TSM=Tekla.Structures.Model;
using QCS.Utils;
using Tekla.Structures.Model;

namespace QImporting.Utils
{
    public class ImportingConvert
    {
        public static ArrayList convertToGirderInfos(string xmlFile)
        {
            ArrayList girderInfos = new ArrayList();
            try
            {
                XmlNodeList xNodeList = QImportingUtil.loadXmlFile(xmlFile, "//" + QImportingConstants.GIRDER_NODE_NAME);
                foreach (XmlNode xmlNode in xNodeList)
                {
                    IGirderInfo girderInfo = toIGirderInfo(xmlNode);
                    girderInfos.Add(girderInfo);
                }
            }
            catch (Exception ec)
            {
                Console.WriteLine(ec.Message);
            }
            return girderInfos;
        }
        public static ArrayList convertSupportLineInfoList(XmlDocument xmlDoc)
        {
            XmlElement piersElem = (XmlElement)xmlDoc.SelectSingleNode( "//piers");
            ArrayList pierSupportLineList = getLayoutLineInfoList(piersElem);
            XmlElement abutsElem = (XmlElement)xmlDoc.SelectSingleNode("//abutments");
            ArrayList abutSupportLineList = getLayoutLineInfoList(abutsElem);
            ArrayList supportLineList = new ArrayList();
            int i = 0;
            foreach (LayoutLineInfo abutLineInfo in abutSupportLineList)
            {
                if (i == 0)
                {
                    supportLineList.Add(abutLineInfo);
                    supportLineList.AddRange(pierSupportLineList);
                }
                if (i == 1)
                {
                    supportLineList.Add(abutLineInfo);
                    break;
                }
                i++;
            }
            return supportLineList;
        }
        public static ArrayList getLayoutLineInfoList(XmlElement elem)
        {
            ArrayList supportLineInfoList = new ArrayList();
            XmlNodeList lineNodeList = elem.GetElementsByTagName(QImportingConstants.XML_LAYOUT_LINE_NODE_NAME);
            foreach (XmlElement lineElem in lineNodeList)
            {
                if (null != lineElem)
                {
                    LayoutLineInfo lineInfo = toLayoutlineInfo(lineElem);
                    supportLineInfoList.Add(lineInfo);
                }
            }
            return supportLineInfoList;
        }
        static StrandInfo toStrandInfo(XmlElement xmlElem)
        {
            StrandInfo strandInfo = new StrandInfo();
            XmlNodeList childList = xmlElem.ChildNodes;
            foreach (XmlElement childElem in childList)
            {
                if (childElem.Name.Equals(QExportingConstants.POINT_NODE_NAME))
                {
                    //point elem...
                    PointInfo pointInfo = toPointInfo(childElem);
                    if (childElem.GetAttribute(QExportingConstants.XML_TYPE_ATTR).Equals(QExportingConstants.STARTPOINT_ATTR_VALUE))
                    {
                        strandInfo.setStPointInfo(pointInfo);
                    }
                    else  //End Point...
                    {
                        strandInfo.setEnPointInfo(pointInfo);
                    }
                }
            }
            strandInfo.setSize(QCS.Utils.Utils.toDouble(xmlElem.GetAttribute(QExportingConstants.XML_SIZE_ATTR)));
            strandInfo.setGrade(xmlElem.GetAttribute(QExportingConstants.XML_GRADE_ATTR));
            strandInfo.setBendingRadius(QCS.Utils.Utils.toDouble(xmlElem.GetAttribute(QExportingConstants.XML_BENDINGRADIUS_ATTR)));
            strandInfo.setPullStress(QCS.Utils.Utils.toDouble(xmlElem.GetAttribute(QExportingConstants.XML_PULLSTRESS_ATTR)));
            strandInfo.setName(xmlElem.GetAttribute(QExportingConstants.XML_NAME_ATTR));
            return strandInfo;
        }
        static StrandRowInfo toStrandRowInfo(XmlElement xmlElem)
        {
            StrandRowInfo rowInfo = new StrandRowInfo();
            XmlNodeList childNodes = xmlElem.ChildNodes;
            foreach (XmlElement childElem in childNodes)
            {
                if (childElem.Name.Equals(QExportingConstants.XML_STRAND_NODE_NAME))
                {
                    StrandInfo strandInfo = toStrandInfo(childElem);
                    rowInfo.addStrand(strandInfo);
                }
            }
            rowInfo.setName(xmlElem.GetAttribute(QExportingConstants.XML_NAME_ATTR));
            rowInfo.setRowID(xmlElem.GetAttribute(QExportingConstants.XML_ID_ATTR));
            return rowInfo;
        }
        static PrestressedStrandsInfo toPrestressedStrandsInfo(XmlElement xmlElem)
        {
            PrestressedStrandsInfo strandsInfo = new PrestressedStrandsInfo();
            XmlNodeList childNodes = xmlElem.ChildNodes;
            foreach (XmlElement childElem in childNodes)
            {
                if (childElem.Name.Equals(QExportingConstants.XML_STRANDROW_NODE_NAME))
                {
                    StrandRowInfo rowInfo = toStrandRowInfo(childElem);
                    strandsInfo.addStrandRow(rowInfo);
                }
            }
            strandsInfo.setName(xmlElem.GetAttribute(QExportingConstants.XML_NAME_ATTR));
            return strandsInfo;
        }
        static PrecastGirderSpanInfo toPrecastGirderSpanInfo(XmlElement xmlElem)
        {
            PrecastGirderSpanInfo spanInfo = new PrecastGirderSpanInfo();
            XmlNodeList childNodes = xmlElem.ChildNodes;
            foreach (XmlElement childElem in childNodes)
            {
                if (childElem.Name.Equals(QExportingConstants.XML_PRESTRESSEDSTRANDS_NODE_NAME))
                {
                    if (childElem.ChildNodes.Count > 0)
                    {
                        PrestressedStrandsInfo strandsInfo = toPrestressedStrandsInfo(childElem);
                        if (childElem.GetAttribute(QExportingConstants.XML_TYPE_ATTR).Equals(QExportingConstants.UPSTRAND_ATTR_VALUE))
                        {
                            spanInfo.setUpStrands(strandsInfo);
                        }
                        else //down strands
                        {
                            spanInfo.setDownStrands(strandsInfo);
                        }
                    }
                }
                if (childElem.Name.Equals(QExportingConstants.POINT_NODE_NAME))
                {
                    //point elem...
                    PointInfo pointInfo = toPointInfo(childElem);
                    if (childElem.GetAttribute(QExportingConstants.XML_TYPE_ATTR).Equals(QExportingConstants.STARTPOINT_ATTR_VALUE))
                    {
                        spanInfo.setStPoint(pointInfo);
                    }
                    else  //End Point...
                    {
                        spanInfo.setEnPoint(pointInfo);
                    }
                }
            }
            spanInfo.setName(xmlElem.GetAttribute(QExportingConstants.XML_NAME_ATTR));
            spanInfo.setMaterial(xmlElem.GetAttribute(QExportingConstants.XML_MATERIAL_ATTR));
            return spanInfo;
        }
        public static PrecastGirderInfo toPrecastGirderInfo(XmlNode xmlNode)
        {
            PrecastGirderInfo girderInfo = new PrecastGirderInfo();
            XmlElement girderElem = (XmlElement)xmlNode;
            string girderName = getXmlElemNameAttrValue(girderElem);
            XmlNodeList spanList = (XmlNodeList)girderElem.SelectNodes(QExportingConstants.XML_PRECASTSPAN_NODE_NAME);
            foreach (XmlElement spanElem in spanList)
            {
                PrecastGirderSpanInfo spanInfo = toPrecastGirderSpanInfo(spanElem);
                girderInfo.addGirderSpan(spanInfo);
            }
            girderInfo.setName(girderElem.GetAttribute(QExportingConstants.XML_NAME_ATTR));
            return girderInfo;
        }
        public static ArrayList convertToPrecastGirderInfos(string xmlFile)
        {
            ArrayList girderInfos = new ArrayList();
            try
            {
                XmlNodeList xNodeList = QImportingUtil.loadXmlFile(xmlFile, "//" + QImportingConstants.GIRDER_NODE_NAME);
                foreach (XmlNode xmlNode in xNodeList)
                {
                    PrecastGirderInfo girderInfo = toPrecastGirderInfo(xmlNode);
                    girderInfos.Add(girderInfo);
                }
            }
            catch (Exception ec)
            {
                Console.WriteLine(ec.Message);
            }
            return girderInfos;
        }
        public static ArrayList convertToPolyGirderInfos(string xmlFile)
        {
            ArrayList girderInfos = new ArrayList();
            try
            {
                XmlNodeList xNodeList = QImportingUtil.loadXmlFile(xmlFile, "//" + QImportingConstants.GIRDER_NODE_NAME);
                foreach (XmlNode xmlNode in xNodeList)
                {
                    IPolyGirderInfo girderInfo = toIPolyGirderInfo(xmlNode);
                    girderInfos.Add(girderInfo);
                }
            }
            catch (Exception ec)
            {
                Console.WriteLine(ec.Message);
            }
            return girderInfos;
        }

        public static IPolyGirderPlateInfo toIPolyGirderPlateInfo(string plateName, ArrayList pointInfoArr, RSectionInfo rSecInfo)
        {
            IPolyGirderPlateInfo plateInfo = new IPolyGirderPlateInfo();
            int iCount = pointInfoArr.Count;
            if(iCount > 1)
            {
                int i = 1;
                ArrayList mdPointArr = new ArrayList();
                foreach (PointInfo pointInfo in pointInfoArr)
                {
                    if (i == 1)
                    {
                        plateInfo.setStPoint(pointInfo);
                    }
                    if (i > 1 && i < iCount)
                    {
                        mdPointArr.Add(pointInfo);
                    }
                    if (i == iCount)
                    {
                        plateInfo.setEnPoint(pointInfo);
                    }
                }
                plateInfo.setMdPoints(mdPointArr);
                plateInfo.setSectionInfo(rSecInfo);
                plateInfo.setUnit(UnitEnum.MILLIMETER);
                plateInfo.setName(plateName);
            }
            return plateInfo;
        }
        public static ArrayList toPointInfoArrOfMbr(ArrayList nodeList)
        {
            ArrayList pointInfoArr = new ArrayList();
            foreach (XmlElement elem in nodeList)
            {
                PointInfo pointInfo = toPointInfo(elem);
                pointInfoArr.Add(pointInfo);
            }
            return pointInfoArr;
        }
        public static string getXmlElemNameAttrValue(XmlElement elem)
        {
            string name = "";
            if (null != elem)
            {
                name = elem.GetAttribute("name");                
            }
            return name;
        }
        public static ArrayList getSubXmlElemList(XmlNodeList nodeList, string filterStr)
        {
            ArrayList subArr = new ArrayList();
            foreach (XmlNode node in nodeList)
            {
                XmlElement elem = node as XmlElement;
                if (null != elem)
                {
                    string name = getXmlElemNameAttrValue(elem);
                    if (name.Contains(filterStr))
                    {
                        subArr.Add(elem);
                    }
                }
            }
            return subArr;
        }
        public static ArrayList getXmlElemsOfPlateByIndex(ArrayList elemArr, int index)
        {
            ArrayList subArr = new ArrayList();
            foreach(XmlElement elem in elemArr)
            {
                string name = getXmlElemNameAttrValue(elem);
                string indexStr = getIndexFromName(name);
                if (indexStr.Equals("" + index))
                {
                    subArr.Add(elem);
                }
            }
            return subArr;
        }
        public static string getIndexFromName(string name)
        {
            string index = name;
            while (name.Contains("_"))
            {
                int i = name.IndexOf("_");
                name = name.Substring(i + 1);
                index = name;
            }
            return index;
        }
        public static ArrayList toPointInfoArr(ArrayList elemArr)
        {
            ArrayList pointInfoArr = new ArrayList();
            foreach (XmlElement elem in elemArr)
            {
                PointInfo pointInfo = toPointInfo(elem);
                pointInfoArr.Add(pointInfo);
            }
            return pointInfoArr;
        }
        public static string getSingleElemValueFromElemArr(ArrayList elemArr, string elemName)
        {
            foreach (XmlElement elem in elemArr)
            {
                XmlNode node = elem.SelectSingleNode(elemName);
                if (node != null)
                {
                    if (node.Value == null)
                    {
                        return node.LastChild.Value;
                    }
                    return node.Value;
                }
            }
            return "";
        }
        //public static void removeSubElemArrByNameAttr(ref ArrayList arr, string name)
        //{
        //    foreach (XmlElement elem in arr)
        //    {
        //        string nameAttr = getXmlElemNameAttrValue(elem);
        //        if (name.Equals(nameAttr))
        //        {
        //            arr.Remove(elem);
        //        }
        //    }
        //}
        public static PointInfo getStartOrEndPointInfo(ArrayList plateInfoArr, string flag)
        {
            if (flag.Equals("start"))
            {
                foreach (IPolyGirderPlateInfo plateInfo in plateInfoArr)
                {
                    return plateInfo.getStPoint();
                }
            }
            if (flag.Equals("end"))
            {
                IPolyGirderPlateInfo lastPlateInfo = null;
                foreach (IPolyGirderPlateInfo plateInfo in plateInfoArr)
                {
                    lastPlateInfo = plateInfo;
                }
                return lastPlateInfo.getEnPoint();
            }
            return null;
        }
        public static int getPlateNumFromPointElems(ArrayList elems)
        {
            int iNum = 0;
            if (null == elems || elems.Count < 1)
            {
                return iNum;
            }
            int iCount = elems.Count;
            string name = getXmlElemNameAttrValue(elems.ToArray().GetValue(iCount - 1) as XmlElement);
            iNum = Convert.ToInt16(getIndexFromName(name));
            return iNum;
        }
        public static IPolyGirderInfo toIPolyGirderInfo(XmlNode xmlNode)
        {
            XmlElement girderElem = (XmlElement)xmlNode;
            string girderName = getXmlElemNameAttrValue(girderElem);
            XmlNodeList xnList = (XmlNodeList)girderElem.SelectNodes(QImportingConstants.GIRDER_POINT_NODE_NAME);

            ArrayList tfElems = getSubXmlElemList(xnList, "_TF_");
            int iCount = getPlateNumFromPointElems(tfElems);

            ArrayList tfPlateInfoArr = new ArrayList();
            for (int i = 0; i <= iCount; i++ )
            {
                ArrayList elems = getXmlElemsOfPlateByIndex(tfElems, i);
                ArrayList pointInfos = toPointInfoArr(elems);
                string plateName = girderName + "_TF_" + i;
                double width = QImportingUtil.toDouble(getSingleElemValueFromElemArr(elems, "//" + QImportingConstants.TF_WIDTH_NODE_NAME));
                double depth = QImportingUtil.toDouble(getSingleElemValueFromElemArr(elems, "//" + QImportingConstants.TF_DEPTH_NODE_NAME));
                RSectionInfo rSecInfo = new RSectionInfo();
                rSecInfo.setWidth(width);
                rSecInfo.setDepth(depth);
                rSecInfo.setUnit(UnitEnum.MILLIMETER);
                IPolyGirderPlateInfo plateInfo = toIPolyGirderPlateInfo(plateName, pointInfos, rSecInfo);
                tfPlateInfoArr.Add(plateInfo);
            }

            ArrayList webElems = getSubXmlElemList(xnList, "_WEB_");
            iCount = getPlateNumFromPointElems(webElems);
            ArrayList webPlateInfoArr = new ArrayList();
            for (int i = 0; i <= iCount; i++)
            {
                ArrayList elems = getXmlElemsOfPlateByIndex(webElems, i);
                ArrayList pointInfos = toPointInfoArr(elems);
                string plateName = girderName + "_WEB_" + i;
                double width = QImportingUtil.toDouble(getSingleElemValueFromElemArr(elems, "//" + QImportingConstants.WEB_WIDTH_NODE_NAME));
                double depth = QImportingUtil.toDouble(getSingleElemValueFromElemArr(elems, "//" + QImportingConstants.WEB_DEPTH_NODE_NAME));
                RSectionInfo rSecInfo = new RSectionInfo();
                rSecInfo.setWidth(width);
                rSecInfo.setDepth(depth);
                rSecInfo.setUnit(UnitEnum.MILLIMETER);
                IPolyGirderPlateInfo plateInfo = toIPolyGirderPlateInfo(plateName, pointInfos, rSecInfo);
                webPlateInfoArr.Add(plateInfo);
            }

            ArrayList bfElems = getSubXmlElemList(xnList, "_BF_");
            iCount = getPlateNumFromPointElems(bfElems);
            ArrayList bfPlateInfoArr = new ArrayList();
            for (int i = 0; i <= iCount; i++)
            {
                ArrayList elems = getXmlElemsOfPlateByIndex(bfElems, i);
                ArrayList pointInfos = toPointInfoArr(elems);
                string plateName = girderName + "_BF_" + i;
                double width = QImportingUtil.toDouble(getSingleElemValueFromElemArr(elems, "//" + QImportingConstants.BF_WIDTH_NODE_NAME));
                double depth = QImportingUtil.toDouble(getSingleElemValueFromElemArr(elems, "//" + QImportingConstants.BF_DEPTH_NODE_NAME));
                RSectionInfo rSecInfo = new RSectionInfo();
                rSecInfo.setWidth(width);
                rSecInfo.setDepth(depth);
                rSecInfo.setUnit(UnitEnum.MILLIMETER);
                IPolyGirderPlateInfo plateInfo = toIPolyGirderPlateInfo(plateName, pointInfos, rSecInfo);
                bfPlateInfoArr.Add(plateInfo);
            }

            IPolyGirderInfo girderInfo = new IPolyGirderInfo();
            girderInfo.setTfPlates(tfPlateInfoArr);
            girderInfo.setBfPlates(bfPlateInfoArr);
            girderInfo.setWebPlates(webPlateInfoArr);
            girderInfo.setStPoint(getStartOrEndPointInfo(tfPlateInfoArr,"start"));
            girderInfo.setEnPoint(getStartOrEndPointInfo(tfPlateInfoArr, "end"));
            girderInfo.setName(girderName);
            return girderInfo;
        }
        public static LayoutLineInfo toLayoutlineInfo(XmlNode xmlNode)
        {
            XmlElement lineElem = (XmlElement)xmlNode;
            string lineName = getXmlElemNameAttrValue(lineElem);
            XmlNodeList xnList = (XmlNodeList)lineElem.SelectNodes(QImportingConstants.XML_POINT_NODE_NAME);
            LayoutLineInfo lineInfo = new LayoutLineInfo();

            foreach (XmlNode xn in xnList)
            {
                XmlElement pointElem = (XmlElement)xn;
                PointInfo pointInfo = toPointInfo(pointElem);
                lineInfo.addPoint(pointInfo);
            }
            lineInfo.setName(lineName);
            return lineInfo;
        }

        public static CIPDeckInfo toCIPDeckInfo(XmlNode xmlNode)
        {
            XmlElement deckElem = (XmlElement)xmlNode;
            XmlNodeList xnList = (XmlNodeList)deckElem.SelectNodes(QImportingConstants.XML_LAYOUT_LINE_NODE_NAME);
            CIPDeckInfo deckInfo = new CIPDeckInfo();
            foreach (XmlNode xn in xnList)
            {
                XmlElement lineElem = (XmlElement)xn;
                LayoutLineInfo lineInfo = toLayoutlineInfo(lineElem);
                deckInfo.addLayoutLineInfo(lineInfo);
            }
            //deckInfo.setDepth(0.0);
            return deckInfo;
        }

        public static IGirderInfo toIGirderInfo(XmlNode xmlNode)
        {
            XmlElement girderElem = (XmlElement)xmlNode;
            XmlNodeList xnList = (XmlNodeList)girderElem.SelectNodes(QImportingConstants.GIRDER_POINT_NODE_NAME);

            //Form Web SectionInfo......
            XmlElement stElem = (XmlElement)xnList.Item(0);
            XmlElement enElem = (XmlElement)xnList.Item(xnList.Count - 1);

            PointInfo stPointInfo = toPointInfo(stElem);
            PointInfo enPointInfo = toPointInfo(enElem);
            IGirderPlateInfo webPlateInfo = new IGirderPlateInfo(stPointInfo, enPointInfo);

            double webWidth = QImportingUtil.toDoubleValue(stElem.SelectSingleNode(QImportingConstants.WEB_WIDTH_NODE_NAME));
            double webDepth = QImportingUtil.toDoubleValue(stElem.SelectSingleNode(QImportingConstants.WEB_DEPTH_NODE_NAME));
            RSectionInfo webSectionInfo = toRSectionInfo(webWidth, webDepth, QImportingConstants.WEB_SECTION_PREFIX);
            webPlateInfo.setSectionInfo(webSectionInfo);

            ArrayList tfNodes = new ArrayList();
            ArrayList bfNodes = new ArrayList();
            foreach (XmlNode xn in xnList )
            {
                XmlNode tfNode = xn.SelectSingleNode(QImportingConstants.TF_NODE_NAME);
                if( null != tfNode )
                {
                    tfNodes.Add(xn);
                }
                XmlNode bfNode = xn.SelectSingleNode(QImportingConstants.BF_NODE_NAME);
                if (null != bfNode)
                {
                    bfNodes.Add(xn);
                }
            }
            if (xnList.Count > 0)
            {
                XmlNode lastNode = xnList.Item(xnList.Count - 1);
                tfNodes.Add(lastNode);
                bfNodes.Add(lastNode);
            }
            //Form Top Flange SectionInfo List......
            ArrayList tfPlates = new ArrayList();

            int iTf = tfNodes.Count;
            object[] tnArr = tfNodes.ToArray();
            for (int i = 0; i < iTf - 1; i++)
            {
                XmlElement stNode = (XmlElement)tnArr[i];
                XmlElement enNode = (XmlElement)tnArr[i + 1];
                //Form Top Flange SectionInfo List......
                double tfWidth = QImportingUtil.toDoubleValue(stNode.SelectSingleNode(QImportingConstants.TF_WIDTH_NODE_NAME));
                double tfDepth = QImportingUtil.toDoubleValue(stNode.SelectSingleNode(QImportingConstants.TF_DEPTH_NODE_NAME));
                RSectionInfo tfSectionInfo = toRSectionInfo(tfWidth, tfDepth, QImportingConstants.TF_SECTION_PREFIX);
                PointInfo stTfPointInfo = toPointInfo(stNode);
                PointInfo enTfPointInfo = toPointInfo(enNode);
                IGirderPlateInfo plateInfo = new IGirderPlateInfo(stTfPointInfo, enTfPointInfo);
                plateInfo.setSectionInfo(tfSectionInfo);
                tfPlates.Add(plateInfo);
            }
            
            //Form Bot Flange SectionInfo List......
            ArrayList bfPlates = new ArrayList();

            int iBf = bfNodes.Count;
            object[] bnArr = bfNodes.ToArray();
            for (int i = 0; i < iBf - 1 ; i++ )
            {
                XmlElement stNode = (XmlElement)bnArr[i];
                XmlElement enNode = (XmlElement)bnArr[i + 1];
                //Form Top Flange SectionInfo List......
                double bfWidth = QImportingUtil.toDoubleValue(stNode.SelectSingleNode(QImportingConstants.BF_WIDTH_NODE_NAME));
                double bfDepth = QImportingUtil.toDoubleValue(stNode.SelectSingleNode(QImportingConstants.BF_DEPTH_NODE_NAME));
                RSectionInfo tfSectionInfo = toRSectionInfo(bfWidth, bfDepth, QImportingConstants.BF_SECTION_PREFIX);
                PointInfo stBfPointInfo = toPointInfo(stNode);
                PointInfo enBfPointInfo = toPointInfo(enNode);
                IGirderPlateInfo plateInfo = new IGirderPlateInfo(stBfPointInfo, enBfPointInfo);
                plateInfo.setSectionInfo(tfSectionInfo);
                bfPlates.Add(plateInfo);
            }
            IGirderInfo girderInfo = new IGirderInfo();
            girderInfo.setTfPlates(tfPlates);
            girderInfo.setBfPlates(bfPlates);
            ArrayList webPlateInfoArr = new ArrayList();
            webPlateInfoArr.Add(webPlateInfo);
            girderInfo.setWebPlates(webPlateInfoArr);
            girderInfo.setStPoint(stPointInfo);
            girderInfo.setEnPoint(enPointInfo);
            return girderInfo;
        }
        
        private static RSectionInfo toRSectionInfo(double width, double depth, string prefix)
        {
            RSectionInfo sectionInfo = new RSectionInfo();
            sectionInfo.setName(prefix + width + "X" + depth);
            sectionInfo.setWidth(width);
            sectionInfo.setDepth(depth);
            return sectionInfo;
        }
        private static PointInfo toPointInfo(XmlElement elem)
        {
            if (null == elem)
            {
                Console.WriteLine("The node of Point is null or empty!");
                return null;
            }
            if (!elem.Name.Equals(QImportingConstants.GIRDER_POINT_NODE_NAME))
            {
                Console.WriteLine("The node inputed is not correct!");
                return null;
            }
            double x = QImportingUtil.toDouble(elem.SelectSingleNode(QExportingConstants.X_NODE_NAME).InnerText);
            double y = QImportingUtil.toDouble(elem.SelectSingleNode(QExportingConstants.Y_NODE_NAME).InnerText);
            double z = QImportingUtil.toDouble(elem.SelectSingleNode(QExportingConstants.Z_NODE_NAME).InnerText);
            string name = getXmlElemNameAttrValue(elem );
            PointInfo pointInfo = new PointInfo(x, y, z);
            pointInfo.setName(name);
            string id = elem.GetAttribute(QExportingConstants.XML_ID_ATTR);
            pointInfo.setId(id);

            return pointInfo;
        }
        //private static bool convertToRevitGirder(Application app, XmlNode xmlNode)
        //{
        //    throw new Exception("TODO......");
        //}
        //public static bool convertToRevitGirder(Application app, IGirderInfo girderInfo )
        //{
        //    RevitISteelGirder risGirder = new RevitISteelGirder(app, girderInfo);
        //    return risGirder.create();
        //}
        public static bool convertToTeklaGirder(XmlNode xmlNode, RevitISteelGirder girder)
        {
            throw new Exception("TODO......");
        }
        public static TeklaISteelGirder convertToTeklaGirder(IGirderInfo girderInfo)
        {
            TeklaISteelGirder girder = new TeklaISteelGirder();
            if (null == girderInfo)
                return girder;
            
            try
            {
                TSM.Beam mainBeam = toTeklaBeam((IGirderPlateInfo)girderInfo.getWebPlates().ToArray().GetValue(0));
                ArrayList tfBeams = new ArrayList();
                ArrayList bfBeams = new ArrayList();
                ArrayList tfPlateInfos = girderInfo.getTfPlates();
                foreach (IGirderPlateInfo tfPlateInfo in tfPlateInfos)
                {
                    TSM.Beam beam = toTeklaBeam(tfPlateInfo);
                    tfBeams.Add(beam);
                }
                ArrayList bfPlateInfos = girderInfo.getBfPlates();
                foreach (IGirderPlateInfo bfPlateInfo in bfPlateInfos)
                {
                    TSM.Beam beam = toTeklaBeam(bfPlateInfo);
                    bfBeams.Add(beam);
                }
                girder.setMainBeam(mainBeam);
                girder.setTopPlates(tfBeams);
                girder.setBotPlates(bfBeams);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return girder;
        }

        public static AnyBeam toTeklaAnyBeam(IPolyGirderPlateInfo plateInfo)
        {
            PointInfo stPointInfo = plateInfo.getStPoint();
            PointInfo enPointInfo = plateInfo.getEnPoint();
            TPoint stPoint = toTPoint(stPointInfo);
            TPoint enPoint = toTPoint(enPointInfo);
            AnyBeam anyBeam = null;
            ArrayList mdPointInfoList = plateInfo.getMdPoints();
            if (null == mdPointInfoList || mdPointInfoList.Count < 1)
            {
                Beam beam = new Beam();
                beam.Name = plateInfo.getName();
                beam.StartPoint = stPoint;
                beam.EndPoint = enPoint;
                beam.Profile.ProfileString = plateInfo.getSectionInfo().toTeklaProfileStr();
                anyBeam = new AnyBeam(beam);
            }
            else
            {
                PolyBeam beam = new PolyBeam();
                beam.Name = plateInfo.getName();
                beam.AddContourPoint(new TContourPoint(stPoint));
                foreach (PointInfo pointInfo in mdPointInfoList)
                {
                    TPoint point = toTPoint(pointInfo);
                    TContourPoint contourPoint = new TContourPoint(point); 
                    beam.AddContourPoint(contourPoint);
                }
                beam.AddContourPoint(new TContourPoint(enPoint));
                beam.Profile.ProfileString = plateInfo.getSectionInfo().toTeklaProfileStr();
                anyBeam = new AnyBeam(beam);
            } 
            return anyBeam;                
        }
        
        public static TSM.Beam toTeklaBeam(IGirderPlateInfo plateInfo)
        {
            if (plateInfo == null)
            {
                return new TSM.Beam();
            }
            TPoint stPoint = toTPoint(plateInfo.getStPoint());
            TPoint enPoint = toTPoint(plateInfo.getEnPoint());
            TSM.Beam beam = new TSM.Beam(stPoint, enPoint);
            return beam;
        }
        public static TPoint toTPoint(PointInfo pointInfo)
        {
            TPoint tPoint = new TPoint(0.0, 0.0, 0.0);
            if (pointInfo != null)
            {
                double x = QCS.Utils.Utils.toDouble(pointInfo.getX(), QCS.Utils.UnitEnum.METER, QCS.Utils.UnitEnum.MILLIMETER);
                double y = QCS.Utils.Utils.toDouble(pointInfo.getY(), QCS.Utils.UnitEnum.METER, QCS.Utils.UnitEnum.MILLIMETER);
                double z = QCS.Utils.Utils.toDouble(pointInfo.getZ(), QCS.Utils.UnitEnum.METER, QCS.Utils.UnitEnum.MILLIMETER);

                tPoint = new TPoint( x, y, z);
            }
            return tPoint;
        }
        public static TPoint toTPoint(PointInfo pointInfo, UnitEnum oriUnit)
        {
            TPoint tPoint = new TPoint(0.0, 0.0, 0.0);
            if (pointInfo != null)
            {
                double x = QCS.Utils.Utils.toDouble(pointInfo.getX(), QCS.Utils.UnitEnum.METER, QCS.Utils.UnitEnum.MILLIMETER);
                double y = QCS.Utils.Utils.toDouble(pointInfo.getY(), QCS.Utils.UnitEnum.METER, QCS.Utils.UnitEnum.MILLIMETER);
                double z = QCS.Utils.Utils.toDouble(pointInfo.getZ(), QCS.Utils.UnitEnum.METER, QCS.Utils.UnitEnum.MILLIMETER);

                tPoint = new TPoint(x, y, z);
            }
            return tPoint;
        }
        public static CIPDeckInfo convertToCIPDeckInfo(XmlElement deckElem)
        {
            CIPDeckInfo deckInfo = new CIPDeckInfo();
            try
            {
                XmlNodeList lineElemList = deckElem.GetElementsByTagName(QExportingConstants.LAYOUT_LINE_NODE_NAME);
                foreach (XmlElement lineElem in lineElemList)
                {
                    LayoutLineInfo layoutLineInfo = toLayoutLineInfo(lineElem);
                    deckInfo.addLayoutLineInfo(layoutLineInfo);
                }
                XmlElement depthElem = (XmlElement)deckElem.SelectSingleNode(QExportingConstants.DEPTH_PARAM_NAME);
                double depth = QCS.Utils.Utils.toDouble(depthElem.InnerText);
                deckInfo.setDepth(depth);
            }
            catch (Exception ec)
            {
                Console.WriteLine(ec.Message);
            }
            return deckInfo;
        }
        public static LayoutLineInfo toLayoutLineInfo(XmlElement lineElem)
        {
            LayoutLineInfo lineInfo = new LayoutLineInfo();
            try
            {
                XmlNodeList pointElemList = lineElem.GetElementsByTagName(QImportingConstants.XML_POINT_NODE_NAME);
                foreach (XmlElement pointElem in pointElemList)
                {
                    PointInfo pointInfo = toPointInfo(pointElem);
                    lineInfo.addPoint(pointInfo);
                } 
                lineInfo.setName(lineElem.GetAttribute(QExportingConstants.XML_NAME_ATTR));
            }
            catch (Exception ec)
            {
                Console.WriteLine(ec.Message);
            }
            return lineInfo;
        }
        public static SectionInfo toSectionInfo(XmlElement secElem)
        {
            string typeStr = secElem.GetAttribute(QExportingConstants.XML_SECTION_TYPE_ATTR);
            string name = secElem.GetAttribute(QExportingConstants.XML_NAME_ATTR);
            if (String.IsNullOrEmpty(typeStr))
                return null;
            if (typeStr.Equals(QExportingConstants.XML_SECTION_TYPE_REC))
            {
                RSectionInfo secInfo = new RSectionInfo();
                secInfo.setName(name);
                double depth = QCS.Utils.Utils.toDouble(secElem.SelectSingleNode(QImportingConstants.DEPTH_PARAM_NAME).InnerText);
                double width = QCS.Utils.Utils.toDouble(secElem.SelectSingleNode(QImportingConstants.WIDTH_PARAM_NAME).InnerText);
                secInfo.setDepth(depth);
                secInfo.setWidth(width);
                string unit = secElem.GetAttribute(QExportingConstants.XML_UNIT_ATTR);
                secInfo.setUnit(unit);
                return secInfo;
            }
            if (typeStr.Equals(QExportingConstants.XML_SECTION_TYPE_CIR))
            {
                CSectionInfo secInfo = new CSectionInfo();
                secInfo.setName(name);
                double diameter = QCS.Utils.Utils.toDouble(secElem.SelectSingleNode(QExportingConstants.XML_DIAMETER_NODE_NAME).InnerText);
                
                secInfo.setDiameter(diameter);
                string unit = secElem.GetAttribute(QExportingConstants.XML_UNIT_ATTR);
                secInfo.setUnit(unit);
                return secInfo;
            }
            //TODO......
            return null;
        }
        public static PierCapInfo toPierCapInfo(XmlElement capElem)
        {
            PierCapInfo capInfo = new PierCapInfo();
            string name = capElem.GetAttribute(QExportingConstants.XML_NAME_ATTR);
            capInfo.setName(name);
            string unit = capElem.GetAttribute(QExportingConstants.XML_UNIT_ATTR);
            capInfo.setUnit(unit);
            try
            {
                XmlElement secElem = (XmlElement)capElem.SelectSingleNode(QImportingConstants.SECTION_NODE_NAME);
                SectionInfo secInfo = toSectionInfo(secElem);
                capInfo.setSectionInfo(secInfo);
                LayoutLineInfo supportLineInfo = toLayoutlineInfo(capElem.SelectSingleNode("//" + QExportingConstants.LAYOUT_LINE_NODE_NAME));
                capInfo.setSupportLineInfo(supportLineInfo);
                
            }
            catch (Exception ec)
            {
                Console.WriteLine(ec.Message);
            }
            return capInfo;
        }
        public static ColumnInfo toColumnInfo(XmlElement colElem)
        {
            ColumnInfo colInfo = new ColumnInfo();
            string name = colElem.GetAttribute(QExportingConstants.XML_NAME_ATTR);
            colInfo.setName(name);
            string unit = colElem.GetAttribute(QExportingConstants.XML_UNIT_ATTR);
            colInfo.setUnit(unit);
            double depth = QCS.Utils.Utils.toDouble(colElem.SelectSingleNode(QExportingConstants.DEPTH_PARAM_NAME).InnerText);
            colInfo.setDepth(depth);
            XmlElement secElem = (XmlElement)colElem.SelectSingleNode(QImportingConstants.SECTION_NODE_NAME);
            SectionInfo secInfo = toSectionInfo(secElem);
            colInfo.setSectionInfo(secInfo);
            
            return colInfo;
        }
        public static PierInfo toPierInfo(XmlElement pierElem)
        {
            PierInfo pierInfo = new PierInfo();
            string name = pierElem.GetAttribute(QExportingConstants.XML_NAME_ATTR);
            pierInfo.setName(name);
            string unit = pierElem.GetAttribute(QExportingConstants.XML_UNIT_ATTR);
            pierInfo.setUnit(unit);
            XmlElement capElem = (XmlElement)pierElem.SelectSingleNode(QExportingConstants.PIER_CAP_NODE_NAME);
            PierCapInfo capInfo = toPierCapInfo(capElem);
            pierInfo.setPierCapInfo(capInfo);
            XmlNodeList pierColList = pierElem.SelectNodes(QExportingConstants.PIER_COLUMN_NODE_NAME);
            foreach (XmlElement colElem in pierColList)
            {
                ColumnInfo colInfo = toColumnInfo(colElem);
                pierInfo.addPierColumnInfo(colInfo);
            }
            return pierInfo;
        }

        public static AbutmentInfo toAbutmentInfo(XmlElement abutElem)
        {
            AbutmentInfo abutInfo = new AbutmentInfo();
            string name = abutElem.GetAttribute(QExportingConstants.XML_NAME_ATTR);
            abutInfo.setName(name);
            string unit = abutElem.GetAttribute(QExportingConstants.XML_UNIT_ATTR);
            abutInfo.setUnit(unit);
            XmlElement capElem = (XmlElement)abutElem.SelectSingleNode(QExportingConstants.ABUTMENT_CAP_NODE_NAME);
            PierCapInfo capInfo = toPierCapInfo(capElem);
            abutInfo.setAbutmentCapInfo(capInfo);
            XmlNodeList colList = abutElem.SelectNodes(QExportingConstants.ABUT_COLUMN_NODE_NAME);
            foreach (XmlElement colElem in colList)
            {
                ColumnInfo colInfo = toColumnInfo(colElem);
                abutInfo.addAbutmentColumnInfo(colInfo);
            }
            return abutInfo;
        }
    }
}
