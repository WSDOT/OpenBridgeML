using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;
using QCS.Utils;

namespace QImporting.Beans
{
    public class ColumnInfo
    {
        private string name;
        private SectionInfo sectionInfo;
        private PointInfo stPoint;
        private PointInfo enPoint;
        private double depth;
        private string unit;

        public PointInfo getStPoint()
        {
            return this.stPoint;
        }
        public void setStPoint(PointInfo p)
        {
            this.stPoint = p;
        }
        public PointInfo getEnPoint()
        {
            return this.enPoint;
        }
        public void setEnPoint(PointInfo p)
        {
            this.enPoint = p;
        }
        public string getUnit()
        {
            return unit;
        }
        public void setUnit(string unit)
        {
            this.unit = unit;
        }

        public void setName(string name)
        {
            this.name = name;
        }
        public void setSectionInfo(SectionInfo sectionInfo)
        {
            this.sectionInfo = sectionInfo;
        }
        public string getName()
        {
            return name;
        }
        public SectionInfo getSectionInfo()
        {
            return sectionInfo;
        }
        public void setDepth(double depth)
        {
            this.depth = depth;
        }
        public double getDepth()
        {
            return depth;
        }
        public XmlElement toXmlElement(XmlDocument xmlDoc)
        {
            XmlElement elem = xmlDoc.CreateElement("pier_column");
            if (!String.IsNullOrEmpty(this.name))
            {
                elem.SetAttribute("name", this.name);
            }
            XmlElement secElem = this.sectionInfo.toXmlElement(xmlDoc);
            elem.AppendChild(secElem);
            XmlElement depthElem = xmlDoc.CreateElement("depth");
            elem.AppendChild(depthElem);
            depthElem.SetAttribute("unit", "mm");
            depthElem.InnerText = "" + this.depth;
            XmlElement stPointElem = this.stPoint.toXmlElement(xmlDoc);
            stPointElem.SetAttribute(QExportingConstants.XML_TYPE_ATTR, QExportingConstants.STARTPOINT_ATTR_VALUE);
            elem.AppendChild(stPointElem);
            XmlElement enPointElem = this.enPoint.toXmlElement(xmlDoc);
            enPointElem.SetAttribute(QExportingConstants.XML_TYPE_ATTR, QExportingConstants.ENDPOINT_ATTR_VALUE);
            elem.AppendChild(enPointElem);
            return elem;
        }
        public double getVolumn()
        {
            double secArea = sectionInfo.getArea();
            return secArea * depth;
        }
    }
}
