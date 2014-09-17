using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;
using QCS.Utils;

namespace QImporting.Beans
{
    public class IPolyGirderInfo
    {
        private string name;
        private string material;
        private ArrayList tfPlates;
        private ArrayList bfPlates;
        private PointInfo stPoint;
        private PointInfo enPoint;
        private ArrayList webPlates;
        private ISectionInfo sectionInfo;
        private string unit;

        public string getUnit()
        {
            return unit;
        }
        public void setUnit(string unit)
        {
            this.unit = unit;
        }
        public PointInfo getStPoint()
        {
            return stPoint;
        }
        public PointInfo getEnPoint()
        {
            return enPoint;
        }
        public void setStPoint(PointInfo stPoint)
        {
            this.stPoint = stPoint;
        }
        public void setEnPoint(PointInfo enPoint)
        {
            this.enPoint = enPoint;
        }
        public void addTfPlate(IGirderPlateInfo tfPlate)
        {
            if (null == tfPlates)
            {
                tfPlates = new ArrayList();
            }
            tfPlates.Add(tfPlate);
        }
        public void addBfPlate(IGirderPlateInfo bfPlate)
        {
            if (null == bfPlates)
            {
                bfPlates = new ArrayList();
            }
            bfPlates.Add(bfPlate);
        }
        public void setTfPlates(ArrayList tfPlates)
        {
            this.tfPlates = tfPlates;
        }
        public void setBfPlates(ArrayList bfPlates)
        {
            this.bfPlates = bfPlates;
        }
        public ArrayList getTfPlates()
        {
            return tfPlates;
        }
        public ArrayList getBfPlates()
        {
            return bfPlates;
        }
        public void setName(string name)
        {
            this.name = name;
        }
        public void setMaterial(string material)
        {
            this.material = material;
        }

        public string getName()
        {
            return name;
        }
        public string getMaterial()
        {
            return material;
        }

        public void setSectionInfo(ISectionInfo sectionInfo)
        {
            this.sectionInfo = sectionInfo;
        }
        public ISectionInfo getSectionInfo()
        {
            return sectionInfo;
        }
        public ArrayList getWebPlates()
        {
            return webPlates;
        }

        public void setWebPlates(ArrayList webPlates)
        {
            this.webPlates = webPlates;
        }
        public XmlElement toXmlElement(XmlDocument xmlDoc)
        {
            XmlElement elem = xmlDoc.CreateElement(QImportingConstants.GIRDER_NODE_NAME);
            if (!String.IsNullOrEmpty(this.name))
            {
                elem.SetAttribute(QExportingConstants.XML_NAME_ATTR, this.name);
            }

            //XmlElement stPointElem = this.stPoint.toXmlElement(xmlDoc);
            //elem.AppendChild(stPointElem);
            //XmlElement enPointElem = this.enPoint.toXmlElement(xmlDoc);
            //elem.AppendChild(enPointElem);

            XmlElement tfElem = xmlDoc.CreateElement(QImportingConstants.FLANGE_NODE_NAME);
            tfElem.SetAttribute(QImportingConstants.TYPE_ATTR_NAME, QImportingConstants.TOP_FLANGE_ATTR);
            elem.AppendChild(tfElem);
            foreach (IPolyGirderPlateInfo plateInfo in this.tfPlates)
            {
                XmlElement plateElem = plateInfo.toXmlElement(xmlDoc);
                tfElem.AppendChild(plateElem);
            }
            XmlElement webElem = xmlDoc.CreateElement(QImportingConstants.WEB_NODE_NAME);
            elem.AppendChild(webElem);
            foreach (IPolyGirderPlateInfo plateInfo in this.webPlates)
            {
                XmlElement plateElem = plateInfo.toXmlElement(xmlDoc);
                webElem.AppendChild(plateElem);
            }
            XmlElement bfElem = xmlDoc.CreateElement(QImportingConstants.FLANGE_NODE_NAME);
            bfElem.SetAttribute(QImportingConstants.TYPE_ATTR_NAME, QImportingConstants.BOTTOM_FLANGE_ATTR);
            elem.AppendChild(bfElem);
            foreach (IPolyGirderPlateInfo plateInfo in this.bfPlates)
            {
                XmlElement plateElem = plateInfo.toXmlElement(xmlDoc);
                bfElem.AppendChild(plateElem);
            }

            return elem;
        }
    }
}
