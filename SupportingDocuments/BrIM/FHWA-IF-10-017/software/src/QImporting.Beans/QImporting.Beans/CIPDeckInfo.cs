using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;
using QCS.Utils;

namespace QImporting.Beans
{
    public class CIPDeckInfo
    {
        private string name;
        private double depth;
        private ArrayList layoutLineInfos;
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
        public string getName()
        {
            return name;
        }
        public void addLayoutLineInfo(LayoutLineInfo layoutLineInfo)
        {
            if (null == this.layoutLineInfos)
                this.layoutLineInfos = new ArrayList();
            this.layoutLineInfos.Add(layoutLineInfo);
        }
        public void setLayoutLineInfos(ArrayList layoutLineInfos)
        {
            this.layoutLineInfos = layoutLineInfos;
        }
        public ArrayList getLayoutLineInfos()
        {
            return layoutLineInfos;
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
            XmlElement elem = xmlDoc.CreateElement(QImportingConstants.DECK_NODE_NAME);
            if (String.IsNullOrEmpty(this.name))
            {
                elem.SetAttribute(QExportingConstants.XML_NAME_ATTR, this.name);
            }
            XmlElement depthElem = xmlDoc.CreateElement("depth");
            elem.AppendChild(depthElem);
            depthElem.InnerText = "" + depth;
            depthElem.SetAttribute("unit", "mm");

            foreach (LayoutLineInfo layoutInfo in layoutLineInfos)
            {
                XmlElement layoutElem = layoutInfo.toXmlElement(xmlDoc);
                elem.AppendChild(layoutElem);
            }

            return elem;
        }
    }
}
