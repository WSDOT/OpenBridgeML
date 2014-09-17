using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;
using QCS.Utils;

namespace QImporting.Beans
{
    public class PierCapInfo
    {
        private string name;
        private ArrayList topPointInfos;
        private LayoutLineInfo supportLineInfo;
        private PointInfo stPointInfo;
        private PointInfo enPointInfo;
        private SectionInfo sectionInfo;
        private string unit;

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
        public void setStPointInfo(PointInfo stPointInfo)
        {
            this.stPointInfo = stPointInfo;
        }
        public string getName()
        {
            return name;
        }
        public PointInfo getStPointInfo()
        {
            return stPointInfo;
        }
        public void setSupportLineInfo(LayoutLineInfo supportLineInfo)
        {
            this.supportLineInfo = supportLineInfo;
            if (null != supportLineInfo)
            {
                this.stPointInfo = supportLineInfo.getStPoint();
                this.enPointInfo = supportLineInfo.getEnPoint();
            }
        }
        public void addPointInfo(PointInfo pointInfo)
        {
            if (null == topPointInfos)
                topPointInfos = new ArrayList();
            topPointInfos.Add(pointInfo);
            if (null == supportLineInfo)
                supportLineInfo = new LayoutLineInfo();
            supportLineInfo.addPoint(pointInfo);
            if (null == this.stPointInfo)
                this.stPointInfo = pointInfo;
            this.enPointInfo = pointInfo;
        }
        public LayoutLineInfo getSupportLineInfo()
        {
            return this.supportLineInfo;
        }
        public void setEnPointInfo(PointInfo enPointInfo)
        {
            this.enPointInfo = enPointInfo;
        }
        public PointInfo getEnPointInfo()
        {
            return enPointInfo;
        }
        public void setSectionInfo(SectionInfo sectionInfo)
        {
            this.sectionInfo = sectionInfo;
        }
        public SectionInfo getSectionInfo()
        {
            return sectionInfo;
        }
        public XmlElement toXmlElement(XmlDocument xmlDoc)
        {
            XmlElement elem = xmlDoc.CreateElement(QCS.Utils.QExportingConstants.PIER_CAP_NODE_NAME);
            elem.SetAttribute(QExportingConstants.XML_NAME_ATTR, this.name);
            elem.AppendChild(this.sectionInfo.toXmlElement(xmlDoc));
            //foreach (PointInfo pointInfo in topPointInfos)
            //{
            //    XmlElement pointElem = pointInfo.toXmlElement(xmlDoc);
            //    elem.AppendChild(pointElem);
            //}
            elem.AppendChild(this.supportLineInfo.toXmlElement(xmlDoc));
            return elem;
        }
        public double getVolumn()
        {
            double secArea = sectionInfo.getArea();
            double length = this.supportLineInfo.getLength();
            return secArea * length;
        }
    }
}
