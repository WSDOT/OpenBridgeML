using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;
using QCS.Utils;

namespace QImporting.Beans
{
    public class LayoutLineInfo
    {
        private string name;
        private PointInfo stPoint;
        private PointInfo enPoint;
        private ArrayList points;

        public void setName(string name)
        {
            this.name = name;
        }
        public string getName()
        {
            return name;
        }
        public void addPoint(PointInfo point)
        {
            if (null == points)
            {
                points = new ArrayList();
            }
            points.Add(point);
            if (null == stPoint)
            {
                stPoint = point;
            }
            enPoint = point;
        }
        public void setPoints(ArrayList points)
        {
            this.points = points;
            if (null != points && points.Count != 0)
            {
                stPoint = points.ToArray().GetValue(0) as PointInfo;
                enPoint = points.ToArray().GetValue(points.Count) as PointInfo;
            }

        }
        public PointInfo getStPoint()
        {
            return stPoint;
        }
        public PointInfo getEnPoint()
        {
            return enPoint;
        }
        public ArrayList getPoints()
        {
            return points;
        }
        public XmlElement toXmlElement(XmlDocument xmlDoc)
        {
            XmlElement elem = xmlDoc.CreateElement(QExportingConstants.LAYOUT_LINE_NODE_NAME);
            if (!String.IsNullOrEmpty(this.name))
            {
                elem.SetAttribute(QCS.Utils.QExportingConstants.XML_NAME_ATTR, this.name);
            }
            int i = 0;
            foreach (PointInfo pointInfo in points)
            {
                pointInfo.setId("" + i);
                pointInfo.setName(this.name + "_" + i);
                XmlElement pointElem = pointInfo.toXmlElement(xmlDoc);
                elem.AppendChild(pointElem);
                i++;
            }

            return elem;
        }
        public double getLength()
        {
            return Calculator.getLength(points);
        }
    }
}
