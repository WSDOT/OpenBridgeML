using System;
using System.Collections.Generic;
using System.Text;
using QCS.Utils;
using System.Xml;

namespace QImporting.Beans
{
    public class RSectionInfo : SectionInfo
    {
        private double width;
        private double depth;

        public void setWidth(double width)
        {
            this.width = width;
        }
        public void setDepth(double depth)
        {
            this.depth = depth;
        }
        public double getWidth()
        {
            return width;
        }
        public double getDepth()
        {
            return depth;
        }
        public void rebuild()
        {
            if (String.IsNullOrEmpty(unit))
            {
                return;
            }

            this.width = QCS.Utils.Utils.toDouble(width, unit);
            this.depth = QCS.Utils.Utils.toDouble(depth, unit);
        }
        public override XmlElement toXmlElement(XmlDocument xmlDoc)
        {
            XmlElement secElem = xmlDoc.CreateElement("section");
            secElem.SetAttribute("name", this.name);
            secElem.SetAttribute( "type", "R");
            XmlElement depthElem = xmlDoc.CreateElement("depth");
            secElem.AppendChild(depthElem);
            depthElem.SetAttribute("unit", "mm");
            depthElem.InnerText = "" + this.depth;
            XmlElement widthElem = xmlDoc.CreateElement("width");
            secElem.AppendChild(widthElem);
            widthElem.SetAttribute("unit", "mm");
            widthElem.InnerText = "" + this.width;
            return secElem;
        }
        public override double getArea()
        {
            return width * depth;
        }
        public override string toTeklaProfileStr()
        {
            string profileStr = "" + depth + "X" + width;
            return profileStr;
        }
    }
}
