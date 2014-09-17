using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace QImporting.Beans
{
    public class CSectionInfo : SectionInfo
    {
        private double diameter;
        public void setDiameter(double diameter)
        {
            this.diameter = diameter;
        }
        public double getDiameter()
        {
            return diameter;
        }
        public void rebuild()
        {
            if (String.IsNullOrEmpty(unit ))
            {
                return;
            }
            this.diameter = QCS.Utils.Utils.toDouble(diameter, unit);
        }
        public override XmlElement toXmlElement(XmlDocument xmlDoc)
        {
            XmlElement secElem = xmlDoc.CreateElement("section");
            if (!String.IsNullOrEmpty(this.name))
            {
                secElem.SetAttribute("name", this.name);
            }
            secElem.SetAttribute( "type", "C");
            XmlElement diaElem = xmlDoc.CreateElement("diameter");
            secElem.AppendChild(diaElem);
            diaElem.SetAttribute("unit", "mm");
            diaElem.InnerText = "" + this.diameter;
            return secElem;
        }
        public override double getArea()
        {
            return Math.PI * diameter * diameter / 4;
        }
        public override string toTeklaProfileStr()
        {
            string profileStr = "D" + diameter;
            return profileStr;
        }
    }
}
