using System;
using System.Collections.Generic;
using System.Text;
using Tekla.Structures.Model;
using Tekla.Structures;
using System.Collections;
using System.Xml;
using QCS.Utils;

namespace TeklaBeans
{
    public class AnyBeam : Part
    {
        private Beam beam;
        private PolyBeam polyBeam;
        private Section section;
        public AnyBeam(XmlElement elem)
        {
            if (elem == null)
                return;
            string name = elem.GetAttribute(QExportingConstants.XML_NAME_ATTR);
            XmlNodeList sonNodes = elem.ChildNodes;
            ArrayList pointArr = new ArrayList();
            foreach (XmlElement sonElem in sonNodes)
            {
                if (sonElem.Name.Equals(QImportingConstants.SECTION_NODE_NAME))
                {
                    string type = sonElem.GetAttribute(QExportingConstants.XML_TYPE_ATTR);
                    if (QExportingConstants.XML_SECTION_TYPE_REC.Equals(type))
                    {
                        this.section = new RSection(sonElem);
                    }
                    else if (QExportingConstants.XML_SECTION_TYPE_CIR.Equals(type))
                    {
                        this.section = new CSection(sonElem);
                    }
                }
            }
            XmlNodeList pointList = elem.GetElementsByTagName(QExportingConstants.POINT_NODE_NAME);
            if (pointList.Count > 2)
            {
                this.polyBeam = new PolyBeam();
                foreach (XmlElement pointElem in pointList)
                {
                    TContourPoint p = new TContourPoint(pointElem);
                    
                    this.polyBeam.AddContourPoint(p);
                    this.polyBeam.Name = name;
                    this.polyBeam.Profile.ProfileString = this.section.getProfileString();
                }
            }
            else if (pointList.Count == 2)
            {
                XmlElement stPointElem = (XmlElement)pointList.Item(0);
                XmlElement enPointElem = (XmlElement)pointList.Item(1);
                TPoint stPoint = new TPoint(stPointElem);
                TPoint enPoint = new TPoint(enPointElem);
                this.beam = new Beam();
                this.beam.StartPoint = stPoint;
                this.beam.EndPoint = enPoint;
                this.beam.Name = name;
                this.beam.Profile.ProfileString = this.section.getProfileString();
            }

        }
        public AnyBeam(Part beam) 
        {
            this.beam = beam as Beam;
            this.polyBeam = beam as PolyBeam;
            this.Name = beam.Name;
            this.Profile = beam.Profile;
            this.DeformingData = beam.DeformingData;
            this.CastUnitType = beam.CastUnitType;
            this.Identifier = beam.Identifier;
            this.Finish = beam.Finish;
            this.Material = beam.Material;
            this.PartNumber = beam.PartNumber;
            this.Position = beam.Position;
            this.Class = beam.Class;
        }
        public override bool Insert()
        {
            if (polyBeam != null)
                return polyBeam.Insert();
            if (beam != null)
                return beam.Insert();
            return false;
        }
        public override bool Delete()
        {
            if (polyBeam != null)
                return polyBeam.Delete();
            if (beam != null)
                return beam.Delete();
            return false;
        }
        public override bool Modify()
        {
            if (polyBeam != null)
                return polyBeam.Modify();
            if (beam != null)
                return beam.Modify();
            return false;
        }
        public override bool Select()
        {
            if (polyBeam != null)
                return polyBeam.Select();
            if (beam != null)
                return beam.Select();
            return false;
        }
        public Beam getBeam()
        {
            return this.beam;
        }
        public PolyBeam getPolyBeam()
        {
            return this.polyBeam;
        }
        public TPoint getStPoint()
        {
            if (beam != null)
            {
                return new TPoint(beam.StartPoint.X,beam.StartPoint.Y,beam.StartPoint.Z);
            }
            if (polyBeam != null)
            {
                TPoint tPoint = new TPoint();
                Contour c = polyBeam.Contour;
                ArrayList cpArr = c.ContourPoints;
                IEnumerator ie = cpArr.GetEnumerator();
                if( ie.MoveNext())
                {
                    ContourPoint cp = ie.Current as ContourPoint;
                    if (cp != null)
                    {
                        tPoint.setTPoint(cp.X, cp.Y, cp.Z);
                    }
                }
                return tPoint;
            }
            return null;
        }
        public ArrayList getMdPoints()
        {
            if (null != polyBeam)
            {
                Contour c = polyBeam.Contour;
                ArrayList cpArr = c.ContourPoints;
                int iCount = cpArr.Count;
                if (iCount > 2)
                {
                    ArrayList tPoints = new ArrayList();
                    for (int i = 1; i < iCount - 1; i++)
                    {
                        ContourPoint cp = cpArr.ToArray().GetValue(i) as ContourPoint;
                        if (cp != null)
                        {
                            TPoint tPoint = new TPoint(cp.X, cp.Y, cp.Z);
                            tPoints.Add(tPoint);
                        }
                    }
                    return tPoints;
                }
                
            }
            return null;
        }
        public TPoint getEnPoint()
        {
            if (beam != null)
            {
                return new TPoint(beam.EndPoint.X, beam.EndPoint.Y, beam.EndPoint.Z);
            }
            if (polyBeam != null)
            {
                TPoint tPoint = new TPoint();
                Contour c = polyBeam.Contour;
                ArrayList cpArr = c.ContourPoints;
                int iCount = cpArr.Count;
                if (iCount > 0)
                {
                    ContourPoint cp = cpArr.ToArray().GetValue(iCount - 1) as ContourPoint;
                    if (cp != null)
                    {
                        tPoint.setTPoint(cp.X, cp.Y, cp.Z);
                    }
                }
                return tPoint;
            }
            return null;
        }
    }
}
