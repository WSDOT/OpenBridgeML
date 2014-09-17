using System;
using System.Collections.Generic;
using System.Text;
using Tekla.Structures.Model;
using System.Collections;
using System.Xml;
using TeklaBeans;

namespace TeklaImporting
{
    public class ProgramRunner
    {
        public static void importDeck(XmlElement deckElem)
        {
            Model model = new Model();
            CIPDeck deck = new CIPDeck(deckElem);
            deck.Insert();
            model.CommitChanges();
        }
        public static void importGirders(XmlElement beamsElem)
        {
            Model model = new Model();
            XmlNodeList beamList = beamsElem.ChildNodes;
            foreach (XmlElement girderElem in beamList)
            {
                //string girderName = girderElem.GetAttribute(QCS.Utils.QExportingConstants.XML_NAME_ATTR);
                TeklaISteelPolyGirder girder = new TeklaISteelPolyGirder(girderElem);
                girder.setClass(QCS.Utils.QImportingConstants.TEKLA_GIRDER_DEFAULT_CLASS);
                girder.Insert();
            }
            model.CommitChanges();
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
        static void modifyDeckNames()
        {
            Model model = new Model();
            string modelName = model.GetInfo().ModelName;
            ModelObjectSelector moSelector = model.GetModelObjectSelector();
            ModelObjectEnumerator moEnum = moSelector.GetAllObjectsWithType(ModelObject.ModelObjectEnum.CONTOURPLATE);
            moEnum = moSelector.GetSelectedObjects();
            ArrayList deckSlabArr = new ArrayList();
            int i = 34;
            while (moEnum.MoveNext())
            {
                ContourPlate part = (ContourPlate)moEnum.Current;
                if (null != part && part.Name.Contains("SLAB"))
                {
                    part.Name = "DECK_SLAB7_" + i;
                    part.Modify();
                    deckSlabArr.Add(part);
                }
                i++;
            }
            Console.WriteLine("The contourplates number is: " + moEnum.GetSize());
            Console.WriteLine("The deck slabs number is: " + deckSlabArr.Count);
        }
        static ContourPlate[] sortContourPlatesByY(ArrayList orgArr)
        {
            ContourPlate[] parts = (ContourPlate[])orgArr.ToArray();
            for (int i = 0; i < parts.Length; i++)
            {
                Contour c = parts[i].Contour;
                //c.ContourPoints.
            }
            return parts;
        }

        public static void importPiers(XmlElement piersElem)
        {
            Model model = new Model();
            XmlNodeList pierList = piersElem.ChildNodes;
            foreach (XmlElement pierElem in pierList)
            {
                string pierName = pierElem.GetAttribute(QCS.Utils.QExportingConstants.XML_NAME_ATTR);
                //TeklaPi girder = new TeklaISteelPolyGirder(girderElem);
                //girder.setClass(QCS.Utils.QImportingConstants.TEKLA_GIRDER_DEFAULT_CLASS);
                //girder.Insert();
            }
            model.CommitChanges();
        }
    }
}
