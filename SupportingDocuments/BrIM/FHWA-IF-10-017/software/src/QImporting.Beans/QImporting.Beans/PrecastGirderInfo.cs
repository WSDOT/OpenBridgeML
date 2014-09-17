using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;
using QCS.Utils;

namespace QImporting.Beans
{
    public class PrecastGirderInfo
    {
        private string name;
        private ArrayList girderSpans;
        private string unit;

        public ArrayList getGirderSpans()
        {
            return this.girderSpans;
        }
        public void setGirderSpans(ArrayList girderSpans)
        {
            this.girderSpans = girderSpans;
        }
        public void addGirderSpan(PrecastGirderSpanInfo girderSpan)
        {
            if (null == this.girderSpans)
            {
                this.girderSpans = new ArrayList();
            }
            this.girderSpans.Add(girderSpan);
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
            XmlElement elem = xmlDoc.CreateElement(QImportingConstants.GIRDER_NODE_NAME);
            if (!String.IsNullOrEmpty(this.name))
            {
                elem.SetAttribute(QExportingConstants.XML_NAME_ATTR, this.name);
            }

            foreach (PrecastGirderSpanInfo span in this.girderSpans)
            {
                XmlElement spanElem = span.toXmlElement(xmlDoc);
                elem.AppendChild(spanElem);
            }

            return elem;
        }
    }
}
