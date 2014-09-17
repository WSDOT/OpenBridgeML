using System;
using System.Collections.Generic;
using System.Text;
using Tekla.Structures.Model;
using Tekla.Structures;
using System.Collections;
using TeklaBeans;
using QImporting.Beans;
using System.Xml;
using QImporting.Utils;
using QCS.Utils;
using TSMU = Tekla.Structures.Model.UI;
using System.IO;
using System.Text.RegularExpressions;

namespace TeklaExporting
{
    public class Program
    {
        static void exportTeklaModelToXML()
        {
            Model model = new Model();
            string modelName = model.GetInfo().ModelName;
            Console.WriteLine("Model name: " + modelName);
            //Girders...
            ArrayList anyBeamArr = ExportingConverter.getGirderPlateArr(model);
            ArrayList girderArr = ExportingConverter.convertToTeklaISteelPolyGirderArr(anyBeamArr);
            int girderNumber = ExportingConverter.getGirderNumber(girderArr);
            Console.WriteLine("the number of girder plates is:: " + girderNumber);
            Console.WriteLine("the number of girders is:: " + girderArr.Count);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(QExportingConstants.EX_TPL_FILE_NAME);
            XmlNode beamsNode = xmlDoc.SelectSingleNode("//beams");
            ArrayList girderInfoArr = new ArrayList();
            foreach (TeklaISteelPolyGirder girder in girderArr)
            {
                IPolyGirderInfo girderInfo = ExportingConverter.convertToIGirderInfo(girder);
                
                XmlElement girderElem = girderInfo.toXmlElement(xmlDoc);
                beamsNode.AppendChild(girderElem);
                //girderInfoArr.Add(girderInfo);
            }
            ExportingConvert.setBridgeNodeAttrs(xmlDoc, modelName, modelName, "");
            //Deck...

            CIPDeckInfo deckInfo = ExportingConverter.convertToCIPDeckInfo(model);

            XmlNode deckNode = xmlDoc.SelectSingleNode("//deck");
            deckNode.InnerXml = deckInfo.toXmlElement(xmlDoc).InnerXml;
            //Piers......
            XmlNode piersNode = xmlDoc.SelectSingleNode("//piers");
            ArrayList pierInfoArr = ExportingConverter.convertToPierInfos(model);

            foreach (PierInfo pierInfo in pierInfoArr)
            {
                XmlElement pierElem = pierInfo.toXmlElement(xmlDoc);
                piersNode.AppendChild(pierElem);
            }

            //Abutment......
            XmlNode abutsNode = xmlDoc.SelectSingleNode("//abutments");
            ArrayList abutInfoArr = ExportingConverter.convertToAbutmentInfos(model);

            foreach (AbutmentInfo abutInfo in abutInfoArr)
            {
                XmlElement abutElem = abutInfo.toXmlElement(xmlDoc);
                abutsNode.AppendChild(abutElem);
            }
            xmlDoc.Save(QExportingConstants.EX_FILE_DIR + QExportingConstants.EX_FILE_NAME_PREFIX + model.GetInfo().ModelName + "_FromTekla.xml");
        }
        static void importGirders(XmlElement beamsElem)
        {
            XmlNodeList beamList = beamsElem.ChildNodes;
            foreach (XmlElement girderElem in beamList)
            {
                string girderName = girderElem.GetAttribute(QCS.Utils.QExportingConstants.XML_NAME_ATTR);

            }
        }
        static AnyBeam buildGirderPlate(XmlNodeList pointList)
        {
            Part beam = null;
            if (pointList.Count <= 2)
            {
                beam = new Beam();
            }
            else
            {
                beam = new PolyBeam();
            }
            foreach (XmlElement pointElem in pointList)
            {

            }
            AnyBeam anyBeam = new AnyBeam(beam);
            return anyBeam;
        }
        static void exportToXML()
        {
            Model model = new Model();
            string modelName = model.GetInfo().ModelName;
            Console.WriteLine("Model name: " + modelName);
            //Girders...
            ArrayList anyBeamArr = ExportingConverter.getGirderPlateArr(model);
            ArrayList girderArr = ExportingConverter.convertToTeklaISteelPolyGirderArr(anyBeamArr);
            int girderNumber = ExportingConverter.getGirderNumber(girderArr);
            Console.WriteLine("the number of girder plates is:: " + girderNumber);
            Console.WriteLine("the number of girders is:: " + girderArr.Count);
            ArrayList girderInfoArr = new ArrayList();
            foreach (TeklaISteelPolyGirder girder in girderArr)
            {
                IPolyGirderInfo girderInfo = ExportingConverter.convertToIGirderInfo(girder);

                girderInfoArr.Add(girderInfo);
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(QExportingConstants.EX_TPL_FILE_NAME);
            ExportingConvert.insertGirdersXmlNode(xmlDoc, girderInfoArr);
            ExportingConvert.setBridgeNodeAttrs(xmlDoc, modelName, modelName, "");
            //Deck...

            CIPDeckInfo deckInfo = ExportingConverter.convertToCIPDeckInfo(model);

            XmlNode deckNode = xmlDoc.SelectSingleNode("//deck");
            deckNode.InnerXml = deckInfo.toXmlElement(xmlDoc).InnerXml;
            //Piers......
            XmlNode piersNode = xmlDoc.SelectSingleNode("//piers");
            ArrayList pierInfoArr = ExportingConverter.convertToPierInfos(model);

            foreach (PierInfo pierInfo in pierInfoArr)
            {
                XmlElement pierElem = pierInfo.toXmlElement(xmlDoc);
                piersNode.AppendChild(pierElem);
            }

            //Abutment......
            XmlNode abutsNode = xmlDoc.SelectSingleNode("//abutments");
            ArrayList abutInfoArr = ExportingConverter.convertToAbutmentInfos(model);

            foreach (AbutmentInfo abutInfo in abutInfoArr)
            {
                XmlElement abutElem = abutInfo.toXmlElement(xmlDoc);
                abutsNode.AppendChild(abutElem);
            }
            xmlDoc.Save(QExportingConstants.EX_FILE_DIR + QExportingConstants.EX_FILE_NAME_PREFIX + model.GetInfo().ModelName + ".xml");
        }
        public static void export4DSchedule()
        {
            Model model = new Model();
            string modelName = model.GetInfo().ModelName;
            ModelObjectSelector moSelector = model.GetModelObjectSelector();
            //TSMU.ModelObjectSelector uiSelector = new Tekla.Structures.Model.UI.ModelObjectSelector();
            //TSMU.Picker picker = new TSMU.Picker();
            //Console.WriteLine("To pick something...");
            //Part part = (Part)picker.PickObject(TSMU.Picker.PickObjectEnum.PICK_ONE_PART);
            //Console.WriteLine("Picking is done. Let's see...");
            /**
             * For Girders...
             */
            //Get the girder number first...
            ArrayList moArr = getObjectsFromModel(model, ModelObject.ModelObjectEnum.BEAM);
            string partStr = "BEAM_G";
            ArrayList partArr = filterPartsByName(moArr, partStr);
            int i = 1;
            StringBuilder sb = new StringBuilder();
            //Adding title into SB...
            sb.Append("Parts/Tasks, Start Date, End Date \n");
            while (i <= partArr.Count)
            {
                string filterStr = partStr + i;
                ArrayList girderArr = filterPartsByName(partArr, filterStr);
                if (girderArr.Count < 1)
                {
                    break;
                }
                string content = getScheduleStr(girderArr, filterStr);
                sb.Append(content);
                i++;
            }
            /**
             * For the SubStructure...
             * Piers...
             */
            //ArrayList moArr = getObjectsFromModel(model, ModelObject.ModelObjectEnum.BEAM);
            string pierStr = "COLUMN_Pier";
            partArr = filterPartsByName(moArr, pierStr);
            i = 1;
            while (i <= partArr.Count)
            {
                string filterStr = pierStr + i;
                ArrayList pierArr = filterPartsByName(partArr, filterStr);
                if (pierArr.Count < 1)
                {
                    break;
                }
                string content = getScheduleStr(pierArr, filterStr);
                sb.Append(content);
                i++;
            }
            /**
             * Abutments...
             */
            string abutStr = "COLUMN_Abut";
            partArr = filterPartsByName(moArr, abutStr);
            i = 1;
            while (i <= partArr.Count)
            {
                string filterStr = abutStr + i;
                ArrayList abutArr = filterPartsByName(partArr, filterStr);
                if (abutArr.Count < 1)
                {
                    break;
                }
                string content = getScheduleStr(abutArr, filterStr);
                sb.Append(content);
                i++;
            }
            /**
             * Pier CAPs...
             */
            ArrayList polyBeamArr = getObjectsFromModel(model, ModelObject.ModelObjectEnum.POLYBEAM);
            string capStr = "_Cap";
            partArr = filterPartsByName(polyBeamArr, capStr);
            i = 1;
            while (i <= partArr.Count)
            {
                string filterStr = capStr + i;
                ArrayList capArr = filterPartsByName(partArr, filterStr);
                if (capArr.Count < 1)
                {
                    break;
                }
                string content = getScheduleStr(capArr, filterStr);
                sb.Append(content);
                i++;
            }
            //StringBuilder sb = new StringBuilder();
            string propertyName = "INSTALL_PLAN";
            //foreach (Part part in partArr)
            //{
            //    int dateInSecs = getUserDefinedIntProperty(part, propertyName);
            //    string moName = part.Name;
            //    if (dateInSecs > 0 && !String.IsNullOrEmpty(moName))
            //    {
            //        sb.Append(moName).Append(",");
            //        DateTime date = getDateTimeByTeklaSec(dateInSecs);
            //        sb.Append(date.ToShortDateString()).Append("\n");
            //    }
            //}
            string fileName = @"D:\" + modelName + "_Schedule_" + propertyName + ".csv";
            writeToFile(fileName, sb.ToString());
        }
        static string getScheduleStr(ArrayList partArr, string partName)
        {
            StringBuilder sb = new StringBuilder(partName).Append(",");
            double stDateSecs = getDateSecondsByDateTime(new DateTime(3000,1,1));
            double enDateSecs = 0;
            string propertyName = "INSTALL_PLAN";
            foreach (Part part in partArr)
            {
                double dateInSecs = getUserDefinedIntProperty(part, propertyName);
                if (dateInSecs > 0 && !String.IsNullOrEmpty(partName))
                {
                    stDateSecs = stDateSecs < dateInSecs ? stDateSecs : dateInSecs;
                    enDateSecs = enDateSecs > dateInSecs ? enDateSecs : dateInSecs;
                }
            }
            if (enDateSecs == 0)
            {
                sb.Remove(0, sb.Length);
            }
            else
            {
                //sb.Append(partName).Append(",");
                DateTime stDate = getDateTimeByTeklaSec(stDateSecs);
                DateTime enDate = getDateTimeByTeklaSec(enDateSecs);
                sb.Append(stDate.ToShortDateString()).Append(",");
                sb.Append(enDate.ToShortDateString()).Append(",");
                sb.Append("\n");
            }
            return sb.ToString();
        }
        static ArrayList filterPartsByName(ArrayList partArr, string filterStr)
        {
            ArrayList arr = new ArrayList();
            foreach (Part part in partArr)
            {
                if (part.Name.Contains(filterStr))
                {
                    arr.Add(part);
                }
            }
            return arr;
        }
        static ArrayList getObjectsFromModel( Model model,  ModelObject.ModelObjectEnum objectEnum)
        {
            ArrayList objArr = new ArrayList();
            ModelObjectEnumerator enumerator = model.GetModelObjectSelector().GetAllObjectsWithType(objectEnum);
            for (; enumerator.MoveNext(); )
            {
                ModelObject mo = enumerator.Current;                
                objArr.Add(mo);
            }
            return objArr;
        }

        static string getUserDefinedStringProperty(Part part, string propertyName)
        {
            string value = "";
            if (!part.GetUserProperty(propertyName, ref value))
            {
                Console.WriteLine("It is failed to get the '" + propertyName + "' property, please try it again!");
            }
            else
            {
                Console.WriteLine("Get the value of '" + propertyName + "' property: " + value);
            }
            return value;
        }
        static int getUserDefinedIntProperty(ModelObject part, string propertyName)
        {
            int value = 0;
            if (!part.GetUserProperty(propertyName, ref value))
            {
                Console.WriteLine("It is failed to get the '" + propertyName + "' property, please try it again!");
            }
            else
            {
                Console.WriteLine("Get the value of '" + propertyName + "' property: " + value);
            }
            return value;
        }
        static DateTime getDateTimeByTeklaSec(double seconds)
        {
            DateTime baseDate = new DateTime(1970, 1, 1);
            long baseAdder = (long)seconds;
            long adder = baseAdder * Convert.ToInt32(1e+7);
            long ticks = baseDate.Ticks + adder;
            DateTime date = new DateTime(ticks);
            return date;
        }

        static void writeToFile(string fileName, string content)
        {
            TextWriter tw = new StreamWriter(fileName);
            tw.Write(content);
            tw.Close();            
        }
        static void setUserDefinedProperties()
        {
            Model model = new Model();
            string modelName = model.GetInfo().ModelName;
            ModelObjectSelector moSelector = model.GetModelObjectSelector();
            ModelObjectEnumerator enumerator = moSelector.GetSelectedObjects();
            DateTime date = new DateTime(2008, 11, 7);
            for (; enumerator.MoveNext(); )
            {
                ModelObject mo = enumerator.Current;
                double dateSecs = getDateSecondsByDateTime(date);
                mo.SetUserProperty("INSTALL_PLAN", dateSecs);
                mo.Modify();
            }
            model.CommitChanges();
        }
        static void setUserDefinedDateProperties( string propertyName, string partName, DateTime date, ModelObject.ModelObjectEnum moEnum )
        {
            Model model = new Model();
            ModelObjectSelector moSelector = model.GetModelObjectSelector();
            ModelObjectEnumerator enumerator = moSelector.GetAllObjectsWithType(moEnum);
            //DateTime date = new DateTime(2008, 11, 7);
            for (; enumerator.MoveNext(); )
            {
                Part mo = (Part)enumerator.Current;
                if (mo.Name.Contains(partName))
                {
                    double dateSecs = getDateSecondsByDateTime(date);
                    mo.SetUserProperty(propertyName, dateSecs);
                    mo.Modify();
                }
            }
            model.CommitChanges();
        }
        static double getDateSecondsByDateTime(DateTime date)
        {
            DateTime baseDate = new DateTime(1970, 1, 1);
            long baseAdder = (long)baseDate.Ticks;
            long dateAdder = (long)date.Ticks;
            double adder = (dateAdder - baseAdder) / Convert.ToInt32(1e+7);
            double value = Math.Floor(adder);
            return value;
        }
        public static void importPlannedDateFromMSProject(string srcFileName)
        {
            TextReader tr = new StreamReader(srcFileName);
            try
            {
                string propertyName = "INSTALL_PLAN";
                
                tr.ReadLine();// Skip the first line which should be the titles......
                string strLine = null;
                while ((strLine = tr.ReadLine()) != null)
                {
                    string[] strValues = strLine.Split(new char[1] { ',' });
                    string partName = strValues[0];
                    string startDateStr = strValues[1];
                    string endDateStr = strValues[2];
                    Console.WriteLine("The end Date is: " + endDateStr);
                    DateTime endDate = QCS.Utils.Utils.toDateTime(endDateStr, "/");
                    setUserDefinedDateProperties(propertyName, partName, endDate, ModelObject.ModelObjectEnum.BEAM);
                    setUserDefinedDateProperties(propertyName, partName, endDate, ModelObject.ModelObjectEnum.POLYBEAM);
                }

            }catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }finally
            {
                tr.Close();
            }
        }
        static void printReinforcementsOfPart(string partName)
        {
            Model model = new Model();
            ModelObjectSelector moSelector = model.GetModelObjectSelector();
            ModelObjectEnumerator moEnum = moSelector.GetObjectsByFilterName(partName);
            moEnum = moSelector.GetSelectedObjects();
            Console.WriteLine("The objects number found: " + moEnum.GetSize());
            for (; moEnum.MoveNext(); )
            {
                Part part = moEnum.Current as Part;
                if (part != null)
                {
                    Console.WriteLine("Find One Part with the name: " + partName);
                    ModelObjectEnumerator reinforcementsEnum = part.GetReinforcements();
                    for (; reinforcementsEnum.MoveNext(); )
                    {
                        Reinforcement rift = reinforcementsEnum.Current as Reinforcement;
                        if( rift != null)
                        {
                            Console.WriteLine("Find refinforcement inside with the name: " + rift.Name );
                            Console.WriteLine("--Numbering Series: " + rift.NumberingSeries.ToString());
                            Console.WriteLine("--Size: " + rift.Size);
                            Console.WriteLine("Start Point Offset Type: " + rift.StartPointOffsetType);
                            Console.WriteLine("Start Point Offset Value: " + rift.StartPointOffsetValue);
                        }
                    }
                    break;
                }
            }
        }
        static void updatePropertiesOfPartsByName()
        {
            Model model = new Model();
            ModelObjectSelector moSelector = model.GetModelObjectSelector();
            ModelObjectEnumerator moEnum = moSelector.GetSelectedObjects();
            //ArrayList partArr = getObjectsFromModel(model, moEnum);
            String profileString = "";
            for (; moEnum.MoveNext(); )
            {
                Part part = moEnum.Current as Part;
                if (part.Name.Equals("BEAM_G1_BF_1"))
                {
                    profileString = part.Profile.ProfileString;
                    Console.WriteLine("Get ProfileString: " + profileString);
                    break;
                }
            }
            for(; moEnum.MoveNext(); )
            {
                Part part = moEnum.Current as Part;
                if (part.Name.Contains("BEAM_G") && part.Name.Contains("_BF_1"))
                {
                    double depth = 50;
                    double width = 9/16;
                    part.Profile.ProfileString = profileString;
                    part.Modify();
                    Console.WriteLine("Updating Part: " + part.Name);
                }
            }
            model.CommitChanges();
        }
        static void Main(string[] args)
        {
            //exportTeklaModelToXML();
            //exportToXML();
            //export4DSchedule();
            //setUserDefinedProperties();

            //Set the modified dates from csv file...
            string csvFileName = @"D:\QC_Steel_Alt.db1_Schedule_INSTALL_PLAN.csv";
            importPlannedDateFromMSProject(csvFileName);
            //printReinforcementsOfPart("B-3");
            updatePropertiesOfPartsByName();
            Console.ReadLine();
        }
    }
}
