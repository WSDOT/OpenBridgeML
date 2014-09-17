using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using QCS.Utils;
using System.Xml;

namespace QImporting.Beans
{
    public class PrecastGirderSpanInfo
    {
        private string name;
        private string material;
        private string profile;

        private PointInfo stPoint;
        private PointInfo enPoint;

        private PrestressedStrandsInfo upStrands;
        private PrestressedStrandsInfo downStrands;

        private string unit;

        public PointInfo getStPoint()
        {
            return this.stPoint;
        }
        public void setStPoint(PointInfo pointInfo)
        {
            this.stPoint = pointInfo;
        }
        public PointInfo getEnPoint()
        {
            return this.enPoint;
        }
        public void setEnPoint(PointInfo pointInfo)
        {
            this.enPoint = pointInfo;
        }
        public string getMaterial()
        {
            return this.material;
        }
        public void setMaterial(string material)
        {
            this.material = material;
        }
        public string getProfile()
        {
            return this.profile;
        }
        public void setProfile(string profile)
        {
            this.profile = profile;
        }
        public PrestressedStrandsInfo getUpStrands()
        {
            return this.upStrands;
        }
        public void setUpStrands(PrestressedStrandsInfo strands)
        {
            this.upStrands = strands;
        }
        public PrestressedStrandsInfo getDownStrands()
        {
            return this.downStrands;
        }
        public void setDownStrands(PrestressedStrandsInfo strands)
        {
            this.downStrands = strands;
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
 
        public XmlElement toXmlElement(XmlDocument xmlDoc)
        {
            XmlElement elem = xmlDoc.CreateElement(QExportingConstants.XML_PRECASTSPAN_NODE_NAME);
            if (!String.IsNullOrEmpty(this.name))
            {
                elem.SetAttribute(QCS.Utils.QExportingConstants.XML_NAME_ATTR, this.name);
            }
            XmlElement stPointElem = this.stPoint.toXmlElement(xmlDoc);
            stPointElem.SetAttribute(QExportingConstants.XML_TYPE_ATTR, QExportingConstants.STARTPOINT_ATTR_VALUE);
            elem.AppendChild(stPointElem);
            XmlElement enPointElem = this.enPoint.toXmlElement(xmlDoc);
            enPointElem.SetAttribute(QExportingConstants.XML_TYPE_ATTR, QExportingConstants.ENDPOINT_ATTR_VALUE);
            elem.AppendChild(enPointElem);
            if (null != this.upStrands)
            {
                XmlElement upStrandsElem = this.upStrands.toXmlElement(xmlDoc);
                upStrandsElem.SetAttribute(QCS.Utils.QExportingConstants.XML_TYPE_ATTR, "up");
                elem.AppendChild(upStrandsElem);
            }
            if (null != this.downStrands)
            {
                XmlElement downStrandsElem = this.downStrands.toXmlElement(xmlDoc);
                downStrandsElem.SetAttribute(QCS.Utils.QExportingConstants.XML_TYPE_ATTR, "down");
                elem.AppendChild(downStrandsElem);
            }

            return elem;
        }
    }
}
