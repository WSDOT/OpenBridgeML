using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using QCS.Utils;
using System.Xml;

namespace QImporting.Beans
{
    public class StrandInfo
    {
        private string name;
        private PointInfo stPointInfo;
        private PointInfo enPointInfo;
        private ArrayList harps;
        private string grade;
        private double size;
        private double bendingRadius;
        private double pullStress;
        private string unit;

        public PointInfo getStPointInfo()
        {
            return this.stPointInfo;
        }
        public void setStPointInfo(PointInfo pointInfo)
        {
            this.stPointInfo = pointInfo;
        }
        public PointInfo getEnPointInfo()
        {
            return this.enPointInfo;
        }
        public void setEnPointInfo(PointInfo pointInfo)
        {
            this.enPointInfo = pointInfo;
        }
        public ArrayList getHarps()
        {
            return this.harps;
        }
        public void setHarps(ArrayList harps)
        {
            this.harps = harps;
        }
        public void addHarp(PointInfo harp)
        {
            this.harps.Add(harp);
        }
        public string getGrade()
        {
            return this.grade;
        }
        public void setGrade(string grade)
        {
            this.grade = grade;
        }
        public double getSize()
        {
            return this.size;
        }
        public void setSize(double size)
        {
            this.size = size;
        }
        public double getBendingRadius()
        {
            return this.bendingRadius;
        }
        public void setBendingRadius(double radius)
        {
            this.bendingRadius = radius;
        }
        public double getPullStress()
        {
            return this.pullStress;
        }
        public void setPullStress(double stress)
        {
            this.pullStress = stress;
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
        public string getName()
        {
            return name;
        }
        public XmlElement toXmlElement(XmlDocument xmlDoc)
        {
            XmlElement elem = xmlDoc.CreateElement(QExportingConstants.XML_STRAND_NODE_NAME);
            if (!String.IsNullOrEmpty(this.name))
            {
                elem.SetAttribute(QCS.Utils.QExportingConstants.XML_NAME_ATTR, this.name);
            }
            if (!String.IsNullOrEmpty(this.grade))
            {
                elem.SetAttribute(QCS.Utils.QExportingConstants.XML_GRADE_ATTR, this.grade);
            }
            elem.SetAttribute(QCS.Utils.QExportingConstants.XML_SIZE_ATTR, Convert.ToString(this.size));
            elem.SetAttribute(QCS.Utils.QExportingConstants.XML_BENDINGRADIUS_ATTR, Convert.ToString(this.bendingRadius));
            elem.SetAttribute(QCS.Utils.QExportingConstants.XML_PULLSTRESS_ATTR, Convert.ToString(this.pullStress));

            //Start Point...
            XmlElement stPointElem = this.stPointInfo.toXmlElement(xmlDoc);
            stPointElem.SetAttribute(QExportingConstants.XML_TYPE_ATTR, QExportingConstants.STARTPOINT_ATTR_VALUE);
            elem.AppendChild(stPointElem);
            //Harp Points...
            if (null != this.harps)
            {
                foreach (PointInfo pointInfo in this.harps)
                {
                    XmlElement pointElem = pointInfo.toXmlElement(xmlDoc);
                    elem.AppendChild(pointElem);
                }
            }
            //End Point...
            XmlElement enPointElem = this.enPointInfo.toXmlElement(xmlDoc);
            enPointElem.SetAttribute(QExportingConstants.XML_TYPE_ATTR, QExportingConstants.ENDPOINT_ATTR_VALUE);
            elem.AppendChild(enPointElem);

            return elem;
        }
    }
}
