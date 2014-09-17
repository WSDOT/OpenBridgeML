using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using QCS.Utils;
using System.Xml;

namespace QImporting.Beans
{
    public class StrandRowInfo
    {
        private string name;
        private ArrayList strands;
        private string rowID;
        private string unit;

        public string getRowID()
        {
            return this.rowID;
        }
        public void setRowID(string rowID)
        {
            this.rowID = rowID;
        }
        public ArrayList getStrands()
        {
            return strands;
        }
        public void setStrands(ArrayList strands)
        {
            this.strands = strands;
        }
        public void addStrand(StrandInfo strand)
        {
            if (null == this.strands)
            {
                this.strands = new ArrayList();
            }
            this.strands.Add(strand);
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
            XmlElement elem = xmlDoc.CreateElement(QExportingConstants.XML_STRANDROW_NODE_NAME);
            if (!String.IsNullOrEmpty(this.name))
            {
                elem.SetAttribute(QCS.Utils.QExportingConstants.XML_NAME_ATTR, this.name);
            }
            if (!String.IsNullOrEmpty(this.rowID))
            {
                elem.SetAttribute(QCS.Utils.QExportingConstants.XML_ID_ATTR, this.rowID);
            }
            foreach (StrandInfo strand in this.strands)
            {
                XmlElement strandElem = strand.toXmlElement(xmlDoc);
                elem.AppendChild(strandElem);
            }
            return elem;
        }
    }
}
