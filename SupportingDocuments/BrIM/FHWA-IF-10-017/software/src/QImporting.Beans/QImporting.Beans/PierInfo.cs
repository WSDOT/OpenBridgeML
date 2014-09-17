using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using QCS.Utils;
using System.Xml;

namespace QImporting.Beans
{
    public class PierInfo
    {
        private string name;
        private ArrayList pierColumnInfos;
        private PierCapInfo pierCapInfo;
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
        public void setPierColumnInfos(ArrayList pierColumnInfos)
        {
            this.pierColumnInfos = pierColumnInfos;
        }
        public string getName()
        {
            return name;
        }
        public ArrayList getPierColumnInfos()
        {
            return pierColumnInfos;
        }

        public void addPierColumnInfo(ColumnInfo pierColumnInfo)
        {
            if (null == pierColumnInfos)
                pierColumnInfos = new ArrayList();
            pierColumnInfos.Add(pierColumnInfo);
        }
        public void setPierCapInfo(PierCapInfo pierCapInfo)
        {
            this.pierCapInfo = pierCapInfo;
        }
        public PierCapInfo getPierCapInfo()
        {
            return pierCapInfo;
        }
        public XmlElement toXmlElement(XmlDocument xmlDoc)
        {
            XmlElement elem = xmlDoc.CreateElement(QExportingConstants.PIER_NODE_NAME);
            if (!String.IsNullOrEmpty(this.name))
            {
                elem.SetAttribute(QCS.Utils.QExportingConstants.XML_NAME_ATTR, this.name);
            }
            int i = 0;
            foreach (ColumnInfo pierCol in this.pierColumnInfos)
            {
                XmlElement pierColElem = pierCol.toXmlElement(xmlDoc);
                elem.AppendChild(pierColElem);
                i++;
            }
            if (null != this.pierCapInfo)
            {
                elem.AppendChild(pierCapInfo.toXmlElement(xmlDoc));
            }
            return elem;
        }
    }
}
