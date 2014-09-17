using System;
using System.Collections.Generic;
using System.Text;
using QCS.Utils;
using System.Xml;

namespace QImporting.Beans
{
    public class PointInfo
    {
        private double X;
        private double Y;
        private double Z;
        private string name;
        private string id;
        private string unit;

        public PointInfo()
        {
        }

        public PointInfo(double X, double Y, double Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
        public void setName(string name)
        {
            this.name = name;
        }
        public void setId(string id)
        {
            this.id = id;
        }
        public string getName()
        {
            return name;
        }
        public string getId()
        {
            return id;
        }
        public string getUnit()
        {
            return unit;
        }
        public void setUnit(string unit)
        {
            this.unit = unit;
        }
        public void setX( double X )
        {
            this.X = X;
        }
        public void setY(double Y)
        {
            this.Y = Y;
        }
        public void setZ(double Z)
        {
            this.Z = Z;
        }
        public double getX()
        {
            return X;
        }
        public double getY()
        {
            return Y;
        }
        public double getZ()
        {
            return Z;
        }
        public void rebuild()
        {
            if (String.IsNullOrEmpty(unit))
            {
                return ;
            }

            this.X = QCS.Utils.Utils.toDouble(X, unit);
            this.Y = QCS.Utils.Utils.toDouble(Y, unit);
            this.Z = QCS.Utils.Utils.toDouble(Z, unit);
        }
        public XmlElement toXmlElement(XmlDocument xmlDoc)
        {
            XmlElement elem = xmlDoc.CreateElement("point");
            if (!String.IsNullOrEmpty(this.name))
            {
                elem.SetAttribute(QCS.Utils.QExportingConstants.XML_NAME_ATTR, this.name);
                
            }
            if(!String.IsNullOrEmpty(this.id))
            {
                elem.SetAttribute(QCS.Utils.QExportingConstants.XML_ID_ATTR, this.id);
            }
            XmlElement xElem = xmlDoc.CreateElement(QExportingConstants.X_NODE_NAME);
            xElem.InnerText = "" + this.X;
            elem.AppendChild(xElem);
            XmlElement yElem = xmlDoc.CreateElement(QExportingConstants.Y_NODE_NAME);
            yElem.InnerText = "" + this.Y;
            elem.AppendChild(yElem);
            XmlElement zElem = xmlDoc.CreateElement(QExportingConstants.Z_NODE_NAME);
            zElem.InnerText = "" + this.Z;
            elem.AppendChild(zElem);

            return elem;
        }
    }
}
