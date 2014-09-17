using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Tekla.Structures.Model;
using TeklaBeans;
using QImporting.Beans;
using QImporting.Utils;
using System.Xml;
using QCS.Utils;
using Tekla.Structures;

namespace TeklaExporting
{
    public class ExportingConverter
    {
        private static int getBigger(int i1, int i2)
        {
            if (i2 > i1)
                return i2;
            return i1;
        }
        public static PointInfo toPointInfo(TPoint tPoint)
        {
            if (null == tPoint)
                return null;
            PointInfo pointInfo = new PointInfo(tPoint.X, tPoint.Y, tPoint.Z);
            return pointInfo;
        }
        public static PointInfo toPointInfo(Point Point)
        {
            if (null == Point)
                return null;
            PointInfo pointInfo = new PointInfo(Point.X, Point.Y, Point.Z);
            return pointInfo;
        }
        public static ArrayList toPointInfos(ArrayList tPoints)
        {
            ArrayList pointInfoArr = new ArrayList();
            if (null != tPoints)
            {
                foreach (TPoint tPoint in tPoints)
                {
                    PointInfo pointInfo = toPointInfo(tPoint);
                    pointInfoArr.Add(pointInfo);
                }
            }
            return pointInfoArr;
        }
        public static double getPlateWidth(AnyBeam plate)
        {
            string profileName = plate.Profile.ProfileString;
            if (profileName.Contains("X"))
            {
                string postfixStr = TeklaUtil.getPostfixStrFromProfile(profileName);
                double width = TeklaUtil.parseLengthFromProfile(postfixStr);
                return width;
            }
            return 0.0;
        }
        public static double getPlateLength(AnyBeam plate)
        {
            string profileName = plate.Profile.ProfileString;
            if (profileName.Contains("X"))
            {
                string prefixStr = TeklaUtil.getPrefixStrFromProfile(profileName);
                double length = TeklaUtil.parseLengthFromProfile(prefixStr);
                return length;
            }
            return 0.0;
        }
        public static IPolyGirderPlateInfo toPlateInfo(AnyBeam plate)
        {
            IPolyGirderPlateInfo plateInfo = new IPolyGirderPlateInfo();
            plateInfo.setName(plate.Name);
            plateInfo.setSectionName(plate.Profile.ProfileString);
            PointInfo stPointInfo = toPointInfo(plate.getStPoint());
            plateInfo.setStPoint(stPointInfo);
            PointInfo enPointInfo = toPointInfo(plate.getEnPoint());
            plateInfo.setEnPoint(enPointInfo);
            ArrayList mdPoints = toPointInfos(plate.getMdPoints());
            plateInfo.setMdPoints(mdPoints);
            double depth = getPlateLength(plate);
            double width = getPlateWidth(plate);
            RSectionInfo rSecInfo = new RSectionInfo();
            rSecInfo.setDepth(depth);
            rSecInfo.setWidth(width);
            plateInfo.setSectionInfo(rSecInfo);
            plateInfo.setUnit(QCS.Utils.UnitEnum.INCH);
            
            return plateInfo;
        }
        public static ArrayList convertToPrecastBeamArr(ArrayList beamSpanArr)
        {
            ArrayList pcGirderArr = new ArrayList();
            int girderCount = beamSpanArr.Count;
            if (girderCount < 1)
            {
                return pcGirderArr;
            }
            for (int i = 1; i <= girderCount; i++)
            {
                ArrayList beamSpans = seperatePrecastBeamSpanArrForEachGirder(beamSpanArr, i);
                if (beamSpans.Count < 1)
                {
                    break;
                }
                PrecastBeam pcGirder = new PrecastBeam();
                pcGirder.setName("BEAM_G" + i);
                pcGirder.Name = "BEAM_G" + i;
                pcGirder.setBeamSpanList(beamSpans);
                pcGirderArr.Add(pcGirder);
            }
            return pcGirderArr;
        }
        public static ArrayList convertToTeklaISteelPolyGirderArr(ArrayList anyBeamArr)
        {
            ArrayList ispGirders = new ArrayList();
            int girderCount = anyBeamArr.Count;
            if (girderCount < 1)
            {
                return ispGirders;
            }
            for (int i = 1; i <= girderCount; i++)
            {
                ArrayList plates = seperateAnyBeamArrForEachGirder(anyBeamArr, i);
                if (plates.Count < 1)
                {
                    break;
                }
                TeklaISteelPolyGirder ispGirder = buildTeklaISteelPolyGirder(plates);
                ispGirder.Name = "BEAM_G" + i;
                ispGirders.Add(ispGirder);
            }
            return ispGirders;
        }
        public static ArrayList seperatePrecastBeamSpanArrForEachGirder(ArrayList beamSpanArr, int inum)
        {
            ArrayList beamSpans = new ArrayList();
            string filterStr = "BEAM_G" + inum;
            foreach (PrecastBeamSpan beamSpan in beamSpanArr)
            {
                if (beamSpan.Name.Contains(filterStr))
                {
                    beamSpans.Add(beamSpan);
                }
            }
            return beamSpans;
        }
        public static ArrayList seperateAnyBeamArrForEachGirder(ArrayList anyBeamArr, int inum)
        {
            ArrayList girderPlates = new ArrayList();
            string filterStr = "BEAM_G" + inum;
            foreach (AnyBeam plate in anyBeamArr)
            {
                if (plate.Name.Contains(filterStr))
                {
                    girderPlates.Add(plate);
                }
            }
            return girderPlates;
        }
        public static TeklaISteelPolyGirder buildTeklaISteelPolyGirder(ArrayList anyBeams)
        {
            ArrayList tfPlates = getTFPlates(anyBeams);
            ArrayList webPlates = getWebPlates(anyBeams);
            ArrayList bfPlates = getBFPlates(anyBeams);
            TeklaISteelPolyGirder ispGirder = new TeklaISteelPolyGirder();
            ispGirder.setTopPlates(tfPlates);
            ispGirder.setWebPlates(webPlates);
            ispGirder.setBotPlates(bfPlates);
            return ispGirder;
        }
        public static ArrayList sortPlatesByName(ArrayList plates)
        {
            ArrayList sortedPlates = new ArrayList();
            int platesCount = plates.Count;
            for (int i = 1; i <= platesCount; i++)
            {
                foreach (AnyBeam plate in plates)
                {
                    string filterStr = "_" + i;
                    if (plate.Name.Contains(filterStr))
                    {
                        sortedPlates.Add(plate);
                        break;
                    }
                }
            }
            return sortedPlates;
        }
        public static ArrayList getTFPlates(ArrayList anyBeams)
        {
            string filterStr = "_TF_";
            ArrayList tfPlates = getPlatesByNameFilter(anyBeams, filterStr);

            return sortPlatesByName(tfPlates);
        }
        public static ArrayList getWebPlates(ArrayList anyBeams)
        {
            string filterStr = "_WEB_";
            ArrayList webPlates = getPlatesByNameFilter(anyBeams, filterStr);
            return sortPlatesByName(webPlates);
        }
        public static ArrayList getBFPlates(ArrayList anyBeams)
        {
            string filterStr = "_BF_";
            ArrayList bfPlates = getPlatesByNameFilter(anyBeams, filterStr);
            return sortPlatesByName(bfPlates);
        }
        public static ArrayList getPlatesByNameFilter(ArrayList anyBeamArr, string filterStr)
        {
            ArrayList plates = new ArrayList();
            foreach (AnyBeam plate in anyBeamArr)
            {
                if (plate.Name.Contains(filterStr))
                {
                    plates.Add(plate);
                }
            }
            return plates;
        }
        
        static ArrayList getRowPoints(ArrayList points)
        {
            ArrayList rowPoints = new ArrayList();
            //find minimum one...
            Point minPoint = points.ToArray().GetValue(0) as Point;
            foreach (Point point in points)
            {
                if (minPoint.Z > point.Z)
                {
                    minPoint = point;
                }
            }
            foreach (Point point in points)
            {
                if (Math.Round(minPoint.Z) == Math.Round(point.Z))
                {
                    rowPoints.Add(point);
                }
            }
            return rowPoints;
        }
        public static PrestressedStrandsInfo getPrestressedStrandsInfo(Beam beam, string filterStr)
        {
            PrestressedStrandsInfo strandsInfo = new PrestressedStrandsInfo();
            ModelObjectEnumerator moEnum = beam.GetReinforcements();
            while (moEnum.MoveNext())
            {
                RebarStrand strand = moEnum.Current as RebarStrand;
                if (null != strand && strand.Name.Contains(filterStr))
                {
                    strandsInfo.setName(strand.Name);
                    ArrayList patterns = strand.Patterns;
                    int i = 0;
                    foreach (Polygon polygon in patterns)
                    {
                        ArrayList points = polygon.Points;
                        int iRow = 1;
                        while (null != points && points.Count > 0)
                        {
                            ArrayList rowPoints = getRowPoints(points);
                            StrandRowInfo rowInfo = new StrandRowInfo();
                            foreach (Point point in rowPoints)
                            {
                                PointInfo pointInfo = toPointInfo(point);
                                StrandInfo strandInfo = new StrandInfo();
                                strandInfo.setStPointInfo(pointInfo);
                                strandInfo.setEnPointInfo(pointInfo);
                                rowInfo.addStrand(strandInfo);
                                rowInfo.setRowID(Convert.ToString(iRow));
                                points.Remove(point);
                            }
                            strandsInfo.addStrandRow(rowInfo);
                            i = i + rowPoints.Count;
                            iRow++;
                        }
                        //Assume only has one pattern...
                        break;
                    }
                }
            }
            return strandsInfo;
        }
        public static PrecastGirderInfo convertToPrecastGirderInfo(PrecastBeam girder)
        {
            PrecastGirderInfo girderInfo = new PrecastGirderInfo();
            if (null == girder)
                return girderInfo;

            try
            {
                ArrayList girderSpanArr = new ArrayList();
                ArrayList beamSpanList = girder.getBeamSpanList();
                foreach (PrecastBeamSpan beamSpan in beamSpanList)
                {
                    PrecastGirderSpanInfo girderSpanInfo = new PrecastGirderSpanInfo();
                    Beam beam = beamSpan.getBeam();
                    PointInfo stPointInfo = toPointInfo(beam.StartPoint);
                    PointInfo enPointInfo = toPointInfo(beam.EndPoint);
                    girderSpanInfo.setStPoint(stPointInfo);
                    girderSpanInfo.setEnPoint(enPointInfo);
                    girderSpanInfo.setMaterial(beam.Material.MaterialString);
                    girderSpanInfo.setProfile(beam.Profile.ProfileString);
                    girderSpanInfo.setName(beamSpan.getName());
                    PrestressedStrandsInfo upStrands = getPrestressedStrandsInfo(beam, "Up");
                    PrestressedStrandsInfo downStrands = getPrestressedStrandsInfo(beam, "Down");
                    girderSpanInfo.setUpStrands(upStrands);
                    girderSpanInfo.setDownStrands(downStrands);

                    girderSpanArr.Add(girderSpanInfo);
                }

                girderInfo.setGirderSpans(girderSpanArr);
                girderInfo.setName(girder.Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return girderInfo;
        }
        public static IPolyGirderInfo convertToIGirderInfo(TeklaISteelPolyGirder girder)
        {
            IPolyGirderInfo girderInfo = new IPolyGirderInfo();
            if (null == girder)
                return girderInfo;

            try
            {
                ArrayList tfPlateInfos = new ArrayList();
                ArrayList bfPlateInfos = new ArrayList();
                ArrayList webPlateInfos = new ArrayList();
                ArrayList tfPlates = girder.getTopPlates();
                ArrayList webPlates = girder.getWebPlates();
                ArrayList bfPlates = girder.getBotPlates();
                foreach (AnyBeam plate in webPlates)
                {
                    IPolyGirderPlateInfo plateInfo = toPlateInfo(plate);
                    webPlateInfos.Add(plateInfo);
                }
                int i = 0;
                foreach (AnyBeam plate in tfPlates)
                {
                    IPolyGirderPlateInfo plateInfo = toPlateInfo(plate);
                    if (i == 0)
                    {
                        girderInfo.setStPoint(plateInfo.getStPoint());
                    }
                    girderInfo.setEnPoint(plateInfo.getEnPoint());
                    tfPlateInfos.Add(plateInfo);
                    i++;
                }

                foreach (AnyBeam plate in bfPlates)
                {
                    IPolyGirderPlateInfo plateInfo = toPlateInfo(plate);
                    bfPlateInfos.Add(plateInfo);
                }
                girderInfo.setWebPlates(webPlateInfos);
                girderInfo.setTfPlates(tfPlateInfos);
                girderInfo.setBfPlates(bfPlateInfos);
                girderInfo.setName(girder.Name);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return girderInfo;
        }

        public static int getGirderNumber(ArrayList girderArr)
        {
            string numStr = "";
            int iCount = 0;
            IEnumerator ie = girderArr.GetEnumerator();
            for (; ie.MoveNext(); )
            {
                Beam beam = ie.Current as Beam;
                if (beam != null)
                {
                    string name = beam.Name;
                    numStr = name.Substring(6, 1);
                    int iNum = Convert.ToInt16(numStr);
                    iCount = getBigger(iCount, iNum);
                    continue;
                }
                PolyBeam polyBeam = ie.Current as PolyBeam;
                if (polyBeam != null)
                {
                    string name = polyBeam.Name;
                    numStr = name.Substring(6, 1);
                    int iNum = Convert.ToInt16(numStr);
                    iCount = getBigger(iCount, iNum);
                }
            }

            return iCount;
        }
        public static ArrayList getPrecastBeamSpanArr(Model model)
        {
            if (model == null)
            {
                model = new Model();
            }
            if (model.GetConnectionStatus())
            {
                try
                {
                    ModelObjectSelector moSelector = model.GetModelObjectSelector();
                    ModelObjectEnumerator moEnumerator = moSelector.GetAllObjectsWithType(ModelObject.ModelObjectEnum.BEAM);
                    ArrayList beamSpanArr = new ArrayList();
                    for (; moEnumerator.MoveNext(); )
                    {
                        Beam beam = moEnumerator.Current as Beam;
                        if (beam != null)
                        {
                            string beamName = beam.Name;

                            if (beamName.Contains("BEAM_G"))
                            {
                                string spanID = TeklaUtil.getSpanIDByName(beamName);
                                PrecastBeamSpan beamSpan = new PrecastBeamSpan(spanID, beamName, beam);
                                beamSpanArr.Add(beamSpan);
                            }
                        }
                    }
                    Console.WriteLine("The count of plateArr: " + beamSpanArr.Count);
                    return beamSpanArr;
                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee.Message);
                }
            }
            return null;
        }
        public static ArrayList getGirderPlateArr(Model model)
        {
            if (model == null)
            {
                model = new Model();
            }
            if (model.GetConnectionStatus())
            {
                try
                {
                    ModelObjectSelector moSelector = model.GetModelObjectSelector();
                    ModelObjectEnumerator moEnumerator = moSelector.GetAllObjectsWithType(ModelObject.ModelObjectEnum.BEAM);
                    ArrayList girderArr = new ArrayList();
                    for (; moEnumerator.MoveNext(); )
                    {
                        AnyBeam beam = new AnyBeam(moEnumerator.Current as Part);
                        if (beam != null)
                        {
                            string beamName = beam.Name;

                            if (beamName.Contains("BEAM_G"))
                            {
                                girderArr.Add(beam);
                            }
                        }
                    }
                    moEnumerator = moSelector.GetAllObjectsWithType(ModelObject.ModelObjectEnum.POLYBEAM);
                    for (; moEnumerator.MoveNext(); )
                    {
                        AnyBeam beam = new AnyBeam(moEnumerator.Current as Part);
                        if (beam != null)
                        {
                            string polyBeamName = beam.Name;

                            if (polyBeamName.Contains("BEAM_G"))
                            {
                                girderArr.Add(beam);
                            }
                        }
                    }
                    Console.WriteLine("The count of plateArr: " + girderArr.Count);
                    return girderArr;
                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee.Message);
                }
            }
            return null;
        }
        public static ArrayList getDeckContourPlateArr(Model model)
        {
            if (model == null)
            {
                model = new Model();
            }
            if (model.GetConnectionStatus())
            {
                try
                {
                    ModelObjectSelector moSelector = model.GetModelObjectSelector();
                    ModelObjectEnumerator moEnumerator = moSelector.GetAllObjectsWithType(ModelObject.ModelObjectEnum.CONTOURPLATE);
                    ArrayList deckPlateArr = new ArrayList();
                    for (; moEnumerator.MoveNext(); )
                    {
                        ContourPlate plate = moEnumerator.Current as ContourPlate;

                        if (plate != null)
                        {
                            string plateName = plate.Name;
                            if (plateName.Contains("DECK_SLAB"))
                            {
                                deckPlateArr.Add(plate);
                            }
                        }
                    }
                    Console.WriteLine("The count of Deck Plate Arr: " + deckPlateArr.Count);
                    return deckPlateArr;
                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee.Message);
                }
            }
            return null;
        }
        public static ArrayList getAbutmentColumnArr(Model model)
        {
            ArrayList abutColArr = new ArrayList();
            //TODO......
            return abutColArr;
        }
        public static ArrayList getPierColumnArr(Model model)
        {
            if (model == null)
            {
                model = new Model();
            }
            if (model.GetConnectionStatus())
            {
                try
                {
                    ModelObjectSelector moSelector = model.GetModelObjectSelector();
                    ModelObjectEnumerator moEnumerator = moSelector.GetAllObjectsWithType(ModelObject.ModelObjectEnum.BEAM);
                    ArrayList pierColArr = new ArrayList();
                    for (; moEnumerator.MoveNext(); )
                    {
                        Beam col = moEnumerator.Current as Beam;

                        if (col != null)
                        {
                            string colName = col.Name;
                            if (colName.Contains("COLUMN_Pier"))
                            {
                                pierColArr.Add(col);
                            }
                        }
                    }
                    Console.WriteLine("The total count of Pier Columns Arr: " + pierColArr.Count);
                    return pierColArr;
                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee.Message);
                }
            }
            return null;
        }
        public static ArrayList getPierCapArr(Model model)
        {
            if (model == null)
            {
                model = new Model();
            }
            if (model.GetConnectionStatus())
            {
                try
                {
                    ModelObjectSelector moSelector = model.GetModelObjectSelector();
                    ModelObjectEnumerator moEnumerator = moSelector.GetAllObjectsWithType(ModelObject.ModelObjectEnum.POLYBEAM);
                    ArrayList pierCapArr = new ArrayList();
                    for (; moEnumerator.MoveNext(); )
                    {
                        PolyBeam cap = moEnumerator.Current as PolyBeam;

                        if (cap != null)
                        {
                            string capName = cap.Name;
                            if (capName.Contains("BEAM_Pier") && capName.Contains("_Cap"))
                            {
                                pierCapArr.Add(cap);
                            }
                        }
                    }
                    Console.WriteLine("The total count of Pier Cap Arr: " + pierCapArr.Count);
                    return pierCapArr;
                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee.Message);
                }
            }
            return null;
        }
        public static ArrayList getAbutmentCapArr(Model model)
        {
            if (model == null)
            {
                model = new Model();
            }
            if (model.GetConnectionStatus())
            {
                try
                {
                    ModelObjectSelector moSelector = model.GetModelObjectSelector();
                    ModelObjectEnumerator moEnumerator = moSelector.GetAllObjectsWithType(ModelObject.ModelObjectEnum.POLYBEAM);
                    ArrayList capArr = new ArrayList();
                    for (; moEnumerator.MoveNext(); )
                    {
                        PolyBeam cap = moEnumerator.Current as PolyBeam;

                        if (cap != null)
                        {
                            string capName = cap.Name;
                            if (capName.Contains("BEAM_Abut") && capName.Contains("_Cap"))
                            {
                                capArr.Add(cap);
                            }
                        }
                    }
                    Console.WriteLine("The total count of Abutment Cap Arr: " + capArr.Count);
                    return capArr;
                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee.Message);
                }
            }
            return null;
        }
        public static int getDeckPlateRowNumber(ArrayList deckPlateArr)
        {
            string numStr = "";
            int iCount = 0;
            foreach (ContourPlate deckPlate in deckPlateArr)
            {
                string name = deckPlate.Name;
                numStr = name.Substring(9, 1);
                int iNum = Convert.ToInt16(numStr);
                iCount = getBigger(iCount, iNum);
                continue;
            }
            return iCount;
        }
        public static int getPierNumber(ArrayList pierColArr)
        {
            string numStr = "";
            int iCount = 0;
            foreach (Beam col in pierColArr)
            {
                string name = col.Name;
                numStr = name.Substring(11, 1);
                int iNum = Convert.ToInt16(numStr);
                iCount = getBigger(iCount, iNum);
                continue;
            }
            return iCount;
        }
        public static LayoutLineInfo convertToLayoutLineInfoFromRSlabs(string name, ArrayList rSlabs, int direction)
        {
            LayoutLineInfo line = new LayoutLineInfo();
            line.setName(name);
            int i = 1;
            foreach (CIPDeckRSlab rSlab in rSlabs)
            {
                if (i == 1)
                {
                    TPoint point1 = getPointFromRSlab(rSlab, direction, 0);
                    PointInfo pointInfo1 = toPointInfo(point1);
                    line.addPoint(pointInfo1);
                }
                TPoint point2 = getPointFromRSlab(rSlab, direction, 1);
                PointInfo pointInfo2 = toPointInfo(point2);
                line.addPoint(pointInfo2);
                i++;
            }
            return line;
        }
        public static ColumnInfo convertToPierColumnInfo(Beam column)
        {
            ColumnInfo pierColumnInfo = new ColumnInfo();
            pierColumnInfo.setName(column.Name);
            pierColumnInfo.setStPoint(toPointInfo(column.StartPoint));
            pierColumnInfo.setEnPoint(toPointInfo(column.EndPoint));
            pierColumnInfo.setDepth(0.0);
            PointInfo centerPointInfo = toPointInfo(new TPoint(column.StartPoint.X, column.StartPoint.Y, column.StartPoint.Z));
            SectionInfo sectionInfo = toSectionInfo(column.Profile.ProfileString);
            pierColumnInfo.setSectionInfo(sectionInfo);
            sectionInfo.setCenterPoint(centerPointInfo);
            return pierColumnInfo;
        }
        public static PierCapInfo convertToPierCapInfo(PolyBeam cap)
        {
            PierCapInfo pierCapInfo = new PierCapInfo();
            pierCapInfo.setName(cap.Name);
            SectionInfo sectionInfo = toSectionInfo(cap.Profile.ProfileString);
            pierCapInfo.setSectionInfo(sectionInfo);
            ArrayList contourPointArr = cap.Contour.ContourPoints;
            foreach (ContourPoint contourPoint in contourPointArr)
            {
                TPoint point = new TPoint(contourPoint.X, contourPoint.Y, contourPoint.Z);
                PointInfo pointInfo = toPointInfo(point);
                pierCapInfo.addPointInfo(pointInfo);
            }
            
            return pierCapInfo;
        }
        
        public static SectionInfo toSectionInfo(string profileString)
        {
            if (profileString.Substring(0, 1).Equals("D"))
            {
                CSectionInfo sectionInfo = new CSectionInfo();
                double diameter = Calculator.toDouble(profileString.Substring(1, profileString.Length - 1));
                sectionInfo.setDiameter(diameter);
                sectionInfo.setName(profileString);
                return sectionInfo;
            }
            if (profileString.Contains("*"))
            {
                RSectionInfo sectionInfo = new RSectionInfo();
                int index = profileString.IndexOf("*");
                double depth = Calculator.toDouble(profileString.Substring(0, index));
                double width = Calculator.toDouble(profileString.Substring(index + 1));
                sectionInfo.setDepth(depth);
                sectionInfo.setWidth(width);
                sectionInfo.setName(profileString);
                return sectionInfo;
            }
            return null;
        }
        public static TPoint getPointFromRSlab(CIPDeckRSlab rSlab, int direction, int index)
        {
            int pointIndex = 1;
            if (QExportingConstants.RSLAB_LEFT == direction)
            {
                if (index == 0)
                {
                    pointIndex = 1;
                }
                else
                {
                    pointIndex = 4;
                }
            }
            if (QExportingConstants.RSLAB_RIGHT == direction)
            {
                if (index == 0)
                {
                    pointIndex = 2;
                }
                else
                {
                    pointIndex = 3;
                }
            }
            if (QExportingConstants.RSLAB_FRONT == direction)
            {
                if (index == 0)
                {
                    pointIndex = 1;
                }
                else
                {
                    pointIndex = 2;
                }
            }
            if (QExportingConstants.RSLAB_REAR == direction)
            {
                if (index == 0)
                {
                    pointIndex = 4;
                }
                else
                {
                    pointIndex = 3;
                }
            }
            return rSlab.getPoint(pointIndex);
        }
        public static CIPDeckInfo convertToCIPDeckInfo(Model model)
        {
                        ArrayList layoutLineArr = new ArrayList();
            ArrayList deckContourPlateArr = getDeckContourPlateArr(model);
            int deckPlateRowNum = getDeckPlateRowNumber(deckContourPlateArr);
            double deckDepth = 0.0;
            for (int i = 1; i <= deckPlateRowNum; i++)
            {
                ArrayList rSlabArr = new ArrayList();
                int j = 0;
                foreach (ContourPlate contourPlate in deckContourPlateArr)
                {
                    string contourPlateName = contourPlate.Name;

                    if (contourPlateName.Contains("DECK_SLAB" + i))
                    {
                        CIPDeckRSlab rSlab = new CIPDeckRSlab(contourPlate);
                        rSlabArr.Add(rSlab);
                        deckDepth = QCS.Utils.Utils.toDouble(contourPlate.Profile.ProfileString);
                    }

                }
                IComparer partSortClass = new TeklaBeans.TeklaPartSortByName();
                rSlabArr.Sort(partSortClass);
                if (i == 1)
                {
                    //LayoutLineInfo line0 = convertToLayoutLineInfoFromRSlabs("Deck_Layout_Line_0", rSlabArr, QExportingConstants.RSLAB_RIGHT);
                    LayoutLineInfo line0 = convertToLayoutLineInfoFromRSlabs("Deck_Layout_Line_0", rSlabArr, QExportingConstants.RSLAB_RIGHT);
                    layoutLineArr.Add(line0);
                }
                LayoutLineInfo line = convertToLayoutLineInfoFromRSlabs("Deck_Layout_Line_" + i, rSlabArr, QExportingConstants.RSLAB_LEFT);
                layoutLineArr.Add(line);
            }
            //ExportingConvert.insertDeckLayoutLineNodes(xmlDoc, layoutLineArr);
            CIPDeckInfo deckInfo = new CIPDeckInfo();
            deckInfo.setLayoutLineInfos(layoutLineArr);
            deckInfo.setDepth(deckDepth);
            return deckInfo;
        }
        public static ArrayList convertToPierInfos(Model model)
        {
            ArrayList pierColArr = getPierColumnArr(model);
            ArrayList pierCapArr = getPierCapArr(model);
            int pierNum = getPierNumber(pierColArr);
            ArrayList pierArr = new ArrayList();
            for (int i = 1; i <= pierNum; i++)
            {
                PierInfo pierInfo = new PierInfo();
                string pierName = "Pier" + i;
                pierInfo.setName(pierName);
                foreach (Beam col in pierColArr)
                {
                    string colName = col.Name;
                    if (colName.Contains(pierName))
                    {
                        ColumnInfo pierCol = convertToPierColumnInfo(col);
                        pierInfo.addPierColumnInfo(pierCol);
                    }
                }

                foreach (PolyBeam cap in pierCapArr)
                {
                    string capName = cap.Name;
                    if (capName.Contains(pierName))
                    {
                        PierCapInfo capInfo = convertToPierCapInfo(cap);
                        capInfo.getSupportLineInfo().setName(pierName + "_support_line");
                        pierInfo.setPierCapInfo(capInfo);
                    }
                }
                pierArr.Add(pierInfo);
            }
            
            return pierArr;
        }
        public static ArrayList convertToAbutmentInfos(Model model)
        {
            ArrayList abutColArr = getAbutmentColumnArr(model);
            ArrayList abutCapArr = getAbutmentCapArr(model);
            int num = abutCapArr.Count;
            ArrayList abutArr = new ArrayList();
            for (int i = 1; i <= num; i++)
            {
                AbutmentInfo abutInfo = new AbutmentInfo();
                string abutName = "Abut" + i;
                abutInfo.setName(abutName);
                foreach (Beam col in abutColArr)
                {
                    //string colName = col.Name;
                    //if (colName.Contains(abutName))
                    //{
                    //    ColumnInfo pierCol = convertToPierColumnInfo(col);
                    //    abutInfo.addPierColumnInfo(pierCol);
                    //}
                }

                foreach (PolyBeam cap in abutCapArr)
                {
                    string capName = cap.Name;
                    if (capName.Contains(abutName))
                    {
                        PierCapInfo capInfo = convertToPierCapInfo(cap);
                        capInfo.getSupportLineInfo().setName(abutName + "_support_line");
                        abutInfo.setAbutmentCapInfo(capInfo);
                    }
                }
                abutArr.Add(abutInfo);
            }

            return abutArr;
        }
    }
}
