using System;
using System.Collections.Generic;
using System.Text;
using Tekla.Structures.Model;
using TeklaBeans;
using System.Collections;
using System.IO;

namespace ExportEstimatingLink
{
    class Program
    {
        public static ArrayList getPartMaterialTypeArr1(ModelObjectSelector moSelector, ModelObject.ModelObjectEnum partEnum, ArrayList matTypeArr)
        {
            ModelObjectEnumerator moEnumerator = moSelector.GetAllObjectsWithType(partEnum);
            return getPartMaterialTypeArrFromEnumerator(moEnumerator);
        }
        //public static ArrayList getPartMaterialTypeArr(ModelObjectSelector moSelector)
        //{
        //    ModelObjectEnumerator moEnumerator = moSelector.GetAllObjects();
        //    return getPartMaterialTypeArrFromEnumerator(moEnumerator);
        //}
        public static ArrayList getBearingList(ModelObjectEnumerator moEnumerator)
        {
            ArrayList bearingArr = new ArrayList();
            for (; moEnumerator.MoveNext(); )
            {
                if (moEnumerator.Current.Identifier.ID.Equals("122313"))
                    Console.WriteLine("" + moEnumerator.Current);
                Part part = moEnumerator.Current as Part;
                if (part != null)
                {
                    string partName = part.Name;
                    if (partName.Contains("BEARING_"))
                    {
                        bearingArr.Add(part);
                    }
                }
            }
            return bearingArr;
        }
        public static ArrayList getGirderPlateList(ModelObjectEnumerator moEnumerator)
        {
            ArrayList girderPlateArr = new ArrayList();
            for (; moEnumerator.MoveNext(); )
            {
               
                Part part = moEnumerator.Current as Part;
                if (part != null)
                {
                    string partName = part.Name;
                    if (partName.Contains("BEAM_G"))
                    {
                        girderPlateArr.Add(part);
                    }
                }
            }
            return girderPlateArr;
        }
        public static ArrayList getPartMaterialTypeArrFromEnumerator(ModelObjectEnumerator moEnumerator)
        {
            ArrayList  matTypeArr = new ArrayList();
            int iCount = moEnumerator.GetSize();
            Console.WriteLine("Model Object Number: " + iCount);
            for (; moEnumerator.MoveNext(); )
            {
                if(moEnumerator.Current.Identifier.ID.Equals("122313"))
                    Console.WriteLine("" + moEnumerator.Current);
                Part part = moEnumerator.Current as Part;
                if (part != null)
                {
                    string partName = part.Name;
                    string material = part.Material.MaterialString;
                    if (!matTypeArr.Contains(material))
                    {
                        matTypeArr.Add(material);
                        Console.WriteLine("Material Type: " + material);
                    }
                }
            }
            return matTypeArr;
        }
        public static ArrayList getCIPConcretePartList(ModelObjectEnumerator moEnumerator)
        {
            ArrayList cipPartArr = new ArrayList();
            for (; moEnumerator.MoveNext(); )
            {
                if (moEnumerator.Current.Identifier.ID.Equals("122313"))
                    Console.WriteLine("" + moEnumerator.Current);
                Part part = moEnumerator.Current as Part;
                if (part != null)
                {
                    if (part.CastUnitType.Equals(Tekla.Structures.Model.Part.CastUnitTypeEnum.CAST_IN_PLACE))
                    {
                        cipPartArr.Add(part);
                    }
                }
            }
            return cipPartArr;
        }
        public static ArrayList getPartList(ModelObjectEnumerator moEnumerator)
        {
            ArrayList partArr = new ArrayList();
            for (; moEnumerator.MoveNext(); )
            {
                if (moEnumerator.Current.Identifier.ID.Equals("122313"))
                    Console.WriteLine("" + moEnumerator.Current);
                Part part = moEnumerator.Current as Part;
                if (part != null)
                {
                    if (part.CastUnitType.Equals(Tekla.Structures.Model.Part.CastUnitTypeEnum.PRECAST))
                    {
                        partArr.Add(part);
                    }
                }
            }
            return partArr;
        }
        public static ArrayList getPrecastPartList(ModelObjectEnumerator moEnumerator)
        {
            ArrayList pcPartArr = new ArrayList();
            for (; moEnumerator.MoveNext(); )
            {
                if (moEnumerator.Current.Identifier.ID.Equals("122313"))
                    Console.WriteLine("" + moEnumerator.Current);
                Part part = moEnumerator.Current as Part;
                if (part != null)
                {
                    if (part.CastUnitType.Equals(Tekla.Structures.Model.Part.CastUnitTypeEnum.PRECAST))
                    {
                        pcPartArr.Add(part);
                    }
                }
            }
            return pcPartArr;
        }
        public static int getDistinctNumByProfile(ArrayList partArr)
        {
            ArrayList tempArr = new ArrayList();
            foreach (object part in partArr)
            {
                if (!tempArr.Contains(part))
                {
                    tempArr.Add(part);
                }
            }
            return tempArr.Count;
        }
        public static ArrayList getDistinctProfileList(ArrayList partArr)
        {
            ArrayList tempArr = new ArrayList();
            foreach (Part part in partArr)
            {
                if (!tempArr.Contains(part))
                {
                    tempArr.Add(part.Profile.ProfileString);
                }
            }
            return tempArr;
        }
        public static ArrayList getDistinctMaterialList(ArrayList partArr)
        {
            ArrayList tempArr = new ArrayList();
            foreach (Part part in partArr)
            {
                if (!tempArr.Contains(part))
                {
                    tempArr.Add(part.Material.MaterialString);
                }
            }
            return tempArr;
        }
        static void Main(string[] args)
        {
            Model model = new Model();
            if (model.GetConnectionStatus())
            {
                string modelName = model.GetInfo().ModelName;
                Console.WriteLine("Model name: " + modelName);
                ModelObjectSelector moSelector = model.GetModelObjectSelector();
                //////
                //ModelObjectEnumerator moSlabEnumerator = moSelector.GetAllObjectsWithType(ModelObject.ModelObjectEnum.CONTOURPLATE);
                //ArrayList slabArr = getPartList(moSlabEnumerator);
                //foreach (ContourPlate slab in slabArr)
                //{
                //    if (slab.Name.Contains("DECK_SLAB"))
                //    {
                //        slab.CastUnitType = Part.CastUnitTypeEnum.CAST_IN_PLACE;
                //        slab.Modify();
                //    }
                //}
                //Console.WriteLine("Modify the Cast Unit Type for deck slabs: " + model.CommitChanges());
                //////
                //ModelObjectEnumerator moBeamEnumerator = moSelector.GetAllObjectsWithType(ModelObject.ModelObjectEnum.BEAM);
                //ArrayList matTypeArr = getPartMaterialTypeArrFromEnumerator(moBeamEnumerator);
                Console.WriteLine("Building Bearing List...... ");
                ModelObjectEnumerator moBrgPlateEnumerator = moSelector.GetAllObjectsWithType(ModelObject.ModelObjectEnum.CONTOURPLATE);
                ArrayList bearingArr = getBearingList(moBrgPlateEnumerator);
                Console.WriteLine("Building Girder Plate List...... ");
                ModelObjectEnumerator moGirderPlateEnumerator = moSelector.GetAllObjectsWithType(ModelObject.ModelObjectEnum.BEAM);
                ArrayList girderPlateArr = getGirderPlateList(moGirderPlateEnumerator);
                Console.WriteLine("Building CIP Concrete Part List...... ");
                ModelObjectEnumerator moCIPConcreteEnumerator = moSelector.GetAllObjectsWithType(ModelObject.ModelObjectEnum.CONTOURPLATE);
                ArrayList cipPartArr = getCIPConcretePartList(moCIPConcreteEnumerator);
                Console.WriteLine("Building PC Beam List...... ");
                ModelObjectEnumerator moPCBeamEnumerator = moSelector.GetAllObjectsWithType(ModelObject.ModelObjectEnum.BEAM);
                ArrayList pcBeamArr = getPrecastPartList(moPCBeamEnumerator);
                Console.WriteLine("Building PC PolyBeam List...... ");
                ModelObjectEnumerator moPCPolyBeamEnumerator = moSelector.GetAllObjectsWithType(ModelObject.ModelObjectEnum.POLYBEAM);
                ArrayList pcPolyBeamArr = getPrecastPartList(moPCPolyBeamEnumerator);
                string dir = @"D:\EstimateLinkImporting\";
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                string fileName = dir + modelName + @".csv";
                StreamWriter writer = new StreamWriter(fileName, false);
                writer.Write("Code, Description, Unit, Qty");
                //foreach (string material in matTypeArr)
                //{
                //    writer.Write(material);
                //    writer.Write(",");
                //    writer.Write(material);
                //    writer.WriteLine();
                //}
                double weight = 0;
                double totalWeight = 0;
                string propertyName = "WEIGHT";
                //Bearing Plates......
                Console.WriteLine("Creating Bearing Material...... ");
                if (null != bearingArr && bearingArr.Count > 0)
                {
                    Part aBrg = (Part)bearingArr.ToArray().GetValue(1);
                    string brgProfileStr = aBrg.Profile.ProfileString;
                    writer.WriteLine();
                    writer.Write("BRNG0001, Bearing Plate--" + brgProfileStr + ", each," + bearingArr.Count);
                }

                Console.WriteLine("Creating Girder Plate Material...... ");
                if (null != girderPlateArr && girderPlateArr.Count > 0)
                {
                    
                    foreach (Part girderPlate in girderPlateArr)
                    {
                        girderPlate.GetReportProperty(propertyName, ref weight);
                        totalWeight = totalWeight + weight;
                    }
                    ArrayList profileArr = getDistinctProfileList(girderPlateArr);
                    int i = 0;
                    
                    //foreach (string profileStr in profileArr)
                    //{
                        
                    //    //foreach (Part part in girderPlateArr)
                    //    //{
                    //    //    if (part.Profile.ProfileString.Equals(profileStr))
                    //    //    {
                    //    //        i++;
                    //    //    }
                    //    //}
                    //    //writer.WriteLine();
                        
                    //    //writer.Write("G_STL_PLT_" + i + ", Girder Plate--" + profileStr + ",each," + i);
                    //    i++;
                    //}
                    writer.WriteLine();
                    writer.Write("G_STL_PLTS, Girder Plates--" + ",ton," + totalWeight/1000);
                    totalWeight = 0;
                }
                Console.WriteLine("Creating CIP Material...... ");
                if (null != cipPartArr && cipPartArr.Count > 0)
                {
                    foreach (Part part in cipPartArr)
                    {
                        part.GetReportProperty(propertyName, ref weight);
                        totalWeight = totalWeight + weight;
                    }
                    //ArrayList materialArr = getDistinctMaterialList(cipPartArr);
                    //int i = 0;
                    //foreach (string matStr in materialArr)
                    //{
                    //    writer.WriteLine();
                    //    writer.Write("CIP_Con_" + i + ", Cast In Place Concrete--Material: " + matStr + ", ton, " + 0.0);
                    //    i++;
                    //}
                    string matStr = "5000";
                    writer.WriteLine();
                    writer.Write("CIP_Conc" + ", Cast In Place Concrete--Material: " + matStr + ", ton, " + totalWeight/1000);
                }
                Console.WriteLine("Creating Precast Parts...... ");
                pcBeamArr.AddRange(pcPolyBeamArr);
                int j = 0;
                foreach (Part part in pcBeamArr)
                {
                    
                    writer.WriteLine();
                    writer.Write("PC_BM_" + j + ", Precast Beam--Name: " + part.Name + " --Profile: " + part.Profile.ProfileString + " --Material: " + part.Material.MaterialString + ", each, " + 1);
                    j++;
                }
                writer.Close();
            }
            Console.ReadLine();
        }
    }
}
