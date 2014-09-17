using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using QCS.Utils;
using System.Xml;

namespace QImporting.Beans
{
    public class PrestressedStrandsInfo
    {
        private string name;
        private ArrayList strandRows;
        private string unit;

        public ArrayList getStrandRows()
        {
            return this.strandRows;
        }
        public void setStrandRows(ArrayList strandRows)
        {
            this.strandRows = strandRows;
        }
        public void addStrandRow(StrandRowInfo rowInfo)
        {
            if (null == this.strandRows)
            {
                this.strandRows = new ArrayList();
            }
            this.strandRows.Add(rowInfo);
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
            XmlElement elem = xmlDoc.CreateElement(QExportingConstants.XML_PRESTRESSEDSTRANDS_NODE_NAME);
            if (!String.IsNullOrEmpty(this.name))
            {
                elem.SetAttribute(QCS.Utils.QExportingConstants.XML_NAME_ATTR, this.name);
            }
            
            if (null != this.strandRows)
            {
                foreach (StrandRowInfo strandRow in this.strandRows)
                {
                    XmlElement rowElem = strandRow.toXmlElement(xmlDoc);
                    elem.AppendChild(rowElem);
                }
            }
            return elem;
        }
    }
}
