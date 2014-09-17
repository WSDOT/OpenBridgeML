using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Tekla.Structures.Model;
using Tekla.Structures;
using System.Xml;

namespace TeklaBeans
{
    public class CIPDeck : Part
    {
        public string name;
        private TPoint originPoint;
        public ArrayList slabs;
        public ArrayList depths;
        public string material;

        public CIPDeck()
        {
            originPoint = new TPoint(0, 0, 0);
        }
        public CIPDeck(XmlElement elem)
        {
            if (elem == null)
                return;
            string name = "DECK_SLAB";
            this.slabs = new ArrayList();
            XmlElement depthElem = null;
            XmlNodeList sonNodes = elem.ChildNodes;
            ArrayList psElemArr = new ArrayList();
            foreach (XmlElement sonElem in sonNodes)
            {
                if (sonElem.Name.Equals("depth"))
                {
                    depthElem = sonElem;
                }
                else if (sonElem.Name.Equals("point_string"))
                {
                    psElemArr.Add(sonElem);
                }
            }
            int psCount = psElemArr.Count;
            Array psElems = psElemArr.ToArray();
            for (int i = 1; i < psCount; i++)
            {
                XmlElement ps1 = (XmlElement)psElems.GetValue(i - 1);
                XmlElement ps2 = (XmlElement)psElems.GetValue(i);
                XmlNodeList psNodeList1 = ps1.ChildNodes;
                XmlNodeList psNodeList2 = ps2.ChildNodes;
                Console.WriteLine("** The slab line: " + i);
                for (int j = 1; j < psNodeList1.Count; j++)
                {
                    XmlElement pointElem1 = (XmlElement)psNodeList1.Item(j - 1);
                    XmlElement pointElem2 = (XmlElement)psNodeList1.Item(j);
                    XmlElement pointElem3 = (XmlElement)psNodeList2.Item(j);
                    XmlElement pointElem4 = (XmlElement)psNodeList2.Item(j - 1);
                    string rSlabName = name + i + "_" + (j-1);
                    Console.WriteLine("--------The slab: " + rSlabName);
                    CIPDeckRSlab rSlab = new CIPDeckRSlab(rSlabName, pointElem1, pointElem2, pointElem3, pointElem4, depthElem);
                    this.slabs.Add(rSlab);
                }
            }
        }
        public CIPDeck(ArrayList slabs)
        {
            this.slabs = slabs;
            originPoint = new TPoint(0, 0, 0);
        }
        public CIPDeck(string name, ArrayList slabs)
        {
            this.name = name;
            this.slabs = slabs;
            originPoint = new TPoint(0, 0, 0);
        }
        public CIPDeck(string name, ArrayList slabs, ArrayList depths)
        {
            this.name = name;
            this.slabs = slabs;
            this.depths = depths;
            originPoint = new TPoint(0, 0, 0);
        }
        public void setOriginPoint(TPoint originPoint)
        {
            this.originPoint = originPoint;
        }
        public void setSlabPoints(ArrayList pointStrings)
        {
            if (null == pointStrings || pointStrings.Count < 2)
                return;
            slabs = new ArrayList();
            int iGroupCount = pointStrings.Count;
            Array pointGroup = pointStrings.ToArray();
            for (int i = 0; i < iGroupCount-1; i++)
            {
                Array leftChain = ((ArrayList)pointGroup.GetValue(i)).ToArray();
                Array rightChain = ((ArrayList)pointGroup.GetValue(i+1)).ToArray();
                if (leftChain.Length != rightChain.Length)
                {
                    slabs.Clear();
                    throw new Exception("There are some problems in PointStrings Array, such as the length is not identical.");
                }

                for (int j = 0; j < leftChain.Length - 1; j++)
                {
                    ContourPlate plate = new ContourPlate();
                    plate.Name = "Deck_Slab";
                    
                    TContourPoint lcp1 = new TContourPoint((Point)leftChain.GetValue(j));
                    TContourPoint rcp1 = new TContourPoint((Point)rightChain.GetValue(j));
                    TContourPoint rcp2 = new TContourPoint((Point)rightChain.GetValue(j + 1));
                    TContourPoint lcp2 = new TContourPoint((Point)leftChain.GetValue(j + 1));
                    plate.AddContourPoint(lcp1);
                    plate.AddContourPoint(rcp1);
                    plate.AddContourPoint(rcp2);
                    plate.AddContourPoint(lcp2);
                    slabs.Add(plate);
                }
            }

        }
        public override bool Insert()
        {
            if (null == slabs)
            {
                throw new Exception("Try to insert a null slab group before set any slabs in it.");
            }
            //int iCount = slabs.Count;
            //Array profiles = null;
            //if( null != depths && depths.Count > 0 )
            //    profiles = depths.ToArray();
            foreach (CIPDeckRSlab slab in slabs)
            {
                slab.Insert();
                //if (!slab.Insert())
                //    throw new Exception("ContourPlate inserting problems occur.");
            }
            //IEnumerator ie = slabs.GetEnumerator();
            //for ( int i = 0; i < iCount && ie.MoveNext(); i++ )
            //{
                
            //    ContourPlate slab = (ContourPlate)ie.Current;
            //    if (null != profiles && i < profiles.Length)
            //    {
            //        slab.Profile.ProfileString = profiles.GetValue(i).ToString();
            //    }
                
            //    if(!slab.Insert())
            //        return false;
                
            //}
            return true;
        }
        public override bool Select()
        {
            throw new Exception("The method or operation is not implemented.");
        }
        public override bool Modify()
        {
            throw new Exception("The method or operation is not implemented.");
        }
        public override bool Delete()
        {
            throw new Exception("The method or operation is not implemented.");
        }
        
    }
}
