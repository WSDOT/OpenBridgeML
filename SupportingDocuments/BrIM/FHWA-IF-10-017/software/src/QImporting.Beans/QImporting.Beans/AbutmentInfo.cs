using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using QCS.Utils;
using System.Xml;

namespace QImporting.Beans
{
    public class AbutmentInfo
    {
        private string name;
        private ArrayList abutColumnInfos;
        private PierCapInfo abutCapInfo;
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
        public void setAbutmentColumnInfos(ArrayList columnInfos)
        {
            this.abutColumnInfos = columnInfos;
        }
        public string getName()
        {
            return name;
        }
        public ArrayList getAbutmentColumnInfos()
        {
            return abutColumnInfos;
        }

        public void addAbutmentColumnInfo(ColumnInfo abutColumnInfo)
        {
            if (null == abutColumnInfos)
                abutColumnInfos = new ArrayList();
            abutColumnInfos.Add(abutColumnInfo);
        }
        public void setAbutmentCapInfo(PierCapInfo abutCapInfo)
        {
            this.abutCapInfo = abutCapInfo;
        }
        public PierCapInfo getAbutmentCapInfo()
        {
            return abutCapInfo;
        }
        public XmlElement toXmlElement(XmlDocument xmlDoc)
        {
            XmlElement elem = xmlDoc.CreateElement(QExportingConstants.ABUTMENT_NODE_NAME);
            if (!String.IsNullOrEmpty(this.name))
            {
                elem.SetAttribute(QCS.Utils.QExportingConstants.XML_NAME_ATTR, this.name);
            }
            if (null != this.abutColumnInfos)
            {
                int i = 0;
                foreach (ColumnInfo col in this.abutColumnInfos)
                {
                    XmlElement colElem = col.toXmlElement(xmlDoc);
                    elem.AppendChild(colElem);
                    i++;
                }
            }
            if (null != this.abutCapInfo)
            {
                elem.AppendChild(abutCapInfo.toXmlElement(xmlDoc));
            }
            return elem;
        }
    }
}
