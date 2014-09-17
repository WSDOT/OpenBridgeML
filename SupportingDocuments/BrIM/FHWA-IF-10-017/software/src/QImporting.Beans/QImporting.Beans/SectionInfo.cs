using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;
using QCS.Utils;

namespace QImporting.Beans
{
    public abstract class SectionInfo
    {
        protected string name;
        protected PointInfo centerPoint;
        protected string unit;

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
        public void setCenterPoint(PointInfo point)
        {
            this.centerPoint = point;
        }
        public PointInfo getCenterPoint()
        {
            return centerPoint;
        }
        public abstract XmlElement toXmlElement(XmlDocument xmlDoc);
        public abstract double getArea();
        public abstract string toTeklaProfileStr();

    }
}
