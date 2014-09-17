using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;
using QCS.Utils;

namespace QImporting.Beans
{
    public class IPolyGirderPlateInfo
    {
        private string name;
        private RSectionInfo sectionInfo;
        private string sectionName;
        private PointInfo stPoint;
        private ArrayList mdPoints;
        private PointInfo enPoint;
        private string unit;

        public string getUnit()
        {
            return unit;
        }
        public void setUnit(string unit)
        {
            this.unit = unit;
        }

        public IPolyGirderPlateInfo()
        {

        }
        public IPolyGirderPlateInfo(PointInfo stPoint, PointInfo enPoint)
        {
            this.stPoint = stPoint;
            this.enPoint = enPoint;
        }
        public IPolyGirderPlateInfo(PointInfo stPoint, ArrayList mdPoints, PointInfo enPoint)
        {
            this.stPoint = stPoint;
            this.mdPoints = mdPoints;
            this.enPoint = enPoint;
        }
        public IPolyGirderPlateInfo(string sectionName, PointInfo stPoint, PointInfo enPoint)
        {
            this.sectionName = sectionName;
            this.stPoint = stPoint;
            this.enPoint = enPoint;
        }
        public IPolyGirderPlateInfo(string name, string sectionName, PointInfo stPoint, PointInfo enPoint)
        {
            this.name = name;
            this.sectionName = sectionName;
            this.stPoint = stPoint;
            this.enPoint = enPoint;
        }
        public IPolyGirderPlateInfo(string name, RSectionInfo sectionInfo, PointInfo stPoint, PointInfo enPoint)
        {
            this.name = name;
            this.sectionInfo = sectionInfo;
            this.stPoint = stPoint;
            this.enPoint = enPoint;
        }
        public IPolyGirderPlateInfo(string name, RSectionInfo sectionInfo, PointInfo stPoint, ArrayList mdPoints, PointInfo enPoint)
        {
            this.name = name;
            this.sectionInfo = sectionInfo;
            this.stPoint = stPoint;
            this.mdPoints = mdPoints;
            this.enPoint = enPoint;
        }
        public IPolyGirderPlateInfo(string name, string sectionName, RSectionInfo sectionInfo, PointInfo stPoint, PointInfo enPoint)
        {
            this.name = name;
            this.sectionName = sectionName;
            this.sectionInfo = sectionInfo;
            this.stPoint = stPoint;
            this.enPoint = enPoint;
        }
        public string getName()
        {
            return name;
        }
        public string getSectionName()
        {
            return sectionName;
        }

        public PointInfo getStPoint()
        {
            return stPoint;
        }
        public ArrayList getMdPoints()
        {
            return mdPoints;
        }
        public PointInfo getEnPoint()
        {
            return enPoint;
        }
        public void setName( string name )
        {
            this.name = name;
        }
        public void setSectionName(string sectionName)
        {
            this.sectionName = sectionName;
        }

        public RSectionInfo getSectionInfo()
        {
            return sectionInfo;
        }

        public void setSectionInfo( RSectionInfo sectionInfo )
        {
            this.sectionInfo = sectionInfo;
        }
        public void setStPoint(PointInfo point)
        {
            this.stPoint = point;
        }
        public void setMdPoints(ArrayList points)
        {
            this.mdPoints = points;
        }
        public void setEnPoint(PointInfo point)
        {
            this.enPoint = point;
        }
        public double getVolumn()
        {
            double secArea = sectionInfo.getArea();
            ArrayList pointList = new ArrayList();
            pointList.Add(stPoint);
            pointList.AddRange(this.mdPoints);
            pointList.Add(enPoint);
            double length = Calculator.getLength(pointList);
            return secArea * length;
        }
        public XmlElement toXmlElement(XmlDocument xmlDoc)
        {
            XmlElement elem = xmlDoc.CreateElement(QImportingConstants.PLATE_NODE_NAME);
            if (!String.IsNullOrEmpty(this.name))
            {
                elem.SetAttribute(QExportingConstants.XML_NAME_ATTR, this.name);
            }

            XmlElement secElem = this.sectionInfo.toXmlElement(xmlDoc);
            elem.AppendChild(secElem);

            XmlElement stPointElem = this.stPoint.toXmlElement(xmlDoc);
            elem.AppendChild(stPointElem);
            foreach (PointInfo mdPoint in this.mdPoints)
            {
                XmlElement pointElem = mdPoint.toXmlElement(xmlDoc);
                elem.AppendChild(pointElem);
            }
            XmlElement enPointElem = this.enPoint.toXmlElement(xmlDoc);
            elem.AppendChild(enPointElem);

            return elem;
        }
    }
}
