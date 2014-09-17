using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using QCS.Utils;

namespace QImporting.Beans
{
    public class ISectionInfo : SectionInfo
    {
        private double webWidth;
        private double webDepth;
        private double tfDepth;
        private double tfWidth;
        private double bfDepth;
        private double bfWidth;

        public void setWebWidth(double webWidth)
        {
            this.webWidth = webWidth;
        }
        public void setWebDepth(double webDepth)
        {
            this.webDepth = webDepth;
        }
        public void setTFWidth(double tfWidth)
        {
            this.tfWidth = tfWidth;
        }
        public void setTFDepth(double tfDepth)
        {
            this.tfDepth = tfDepth;
        }
        public void setBFWidth(double bfWidth)
        {
            this.bfWidth = bfWidth;
        }
        public void setBFDepth(double bfDepth)
        {
            this.bfDepth = bfDepth;
        }
        public override XmlElement toXmlElement(XmlDocument xmlDoc)
        {
            XmlElement secElem = xmlDoc.CreateElement("section");
            secElem.SetAttribute("name", this.name);
            secElem.SetAttribute("type", "I");
            secElem.SetAttribute("unit", "mm");

            XmlElement tfElem = xmlDoc.CreateElement("top flange");
            tfElem.AppendChild(tfElem);
            XmlElement tfDepthElem = xmlDoc.CreateElement("depth");
            tfElem.AppendChild(tfDepthElem);
            tfDepthElem.SetAttribute("unit", "mm");
            tfDepthElem.Value = "" + this.tfDepth;
            XmlElement tfWidthElem = xmlDoc.CreateElement("width");
            tfElem.AppendChild(tfWidthElem);
            tfWidthElem.SetAttribute("unit", "mm");
            tfWidthElem.Value = "" + this.tfWidth;

            XmlElement bfElem = xmlDoc.CreateElement("bot flange");
            bfElem.AppendChild(bfElem);
            XmlElement bfDepthElem = xmlDoc.CreateElement("depth");
            bfElem.AppendChild(bfDepthElem);
            bfDepthElem.SetAttribute("unit", "mm");
            bfDepthElem.Value = "" + this.bfDepth;
            XmlElement bfWidthElem = xmlDoc.CreateElement("width");
            bfElem.AppendChild(bfWidthElem);
            bfWidthElem.SetAttribute("unit", "mm");
            bfWidthElem.Value = "" + this.bfWidth;

            XmlElement webElem = xmlDoc.CreateElement("web");
            webElem.AppendChild(webElem);
            XmlElement webDepthElem = xmlDoc.CreateElement("depth");
            webElem.AppendChild(webDepthElem);
            webDepthElem.SetAttribute("unit", "mm");
            webDepthElem.Value = "" + this.webDepth;
            XmlElement webWidthElem = xmlDoc.CreateElement("width");
            webElem.AppendChild(webWidthElem);
            webWidthElem.SetAttribute("unit", "mm");
            webWidthElem.Value = "" + this.webWidth;

            return secElem;
        }
        public override double getArea()
        {
            return tfWidth * tfDepth + bfWidth * bfDepth + webWidth * webDepth;
        }
        public override string toTeklaProfileStr()
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }
}
