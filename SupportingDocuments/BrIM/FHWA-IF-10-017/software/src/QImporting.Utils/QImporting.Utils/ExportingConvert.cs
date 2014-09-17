using System;
using System.Collections.Generic;
using System.Text;
using QImporting.Beans;
using System.Xml;
using RevitImporting.Comps;
using System.Collections;
using TeklaBeans;
using TSM=Tekla.Structures.Model;
using QCS.Utils;

namespace QImporting.Utils
{
    public class ExportingConvert
    {
        public static void setBridgeNodeAttrs(XmlDocument xmlDoc, string name, string id, string desc)
        {
            XmlElement bridgeElem = xmlDoc.LastChild as XmlElement;
            if (null != bridgeElem)
            {
                bridgeElem.SetAttribute(QExportingConstants.XML_NAME_ATTR, name);
                bridgeElem.SetAttribute(QExportingConstants.XML_ID_ATTR, id);
                bridgeElem.SetAttribute(QExportingConstants.XML_DESC_ATTR, desc);
            }
        }
        public static XmlElement toXmlElement(XmlDocument xmlDoc, IPolyGirderInfo girderInfo)
        {
            //Girder Elem......
            XmlElement girderElem = xmlDoc.CreateElement(QExportingConstants.GIRDER_NODE_NAME);
            girderElem.SetAttribute(QExportingConstants.GIRDER_NAME_ATTR, girderInfo.getName());
            //Point Elem......
                //Top Flange Elem......
            ArrayList tfPlateInfos = girderInfo.getTfPlates();
            int i = 0;
            foreach (IPolyGirderPlateInfo plateInfo in tfPlateInfos)
            {
                PointInfo stPoint = plateInfo.getStPoint();
                RSectionInfo rSecInfo = plateInfo.getSectionInfo();
                double width = rSecInfo.getWidth();
                double depth = rSecInfo.getDepth();
                XmlNode stPointElem = toXmlElem(xmlDoc,stPoint,girderInfo.getName() + "_TF_" + i);
                insertWidthDepthElem(xmlDoc,stPointElem,"top_flange",width,depth);
                girderElem.AppendChild(stPointElem);
                foreach (PointInfo mdPoint in plateInfo.getMdPoints())
                {
                    girderElem.AppendChild(toXmlElem(xmlDoc, mdPoint, girderInfo.getName() + "_TF_" + i));
                }
                PointInfo enPoint = plateInfo.getEnPoint();
                girderElem.AppendChild(toXmlElem(xmlDoc, enPoint, girderInfo.getName() + "_TF_" + i));
                
                i++;
            }
            ArrayList webPlateInfos = girderInfo.getWebPlates();
            i = 0;
            foreach (IPolyGirderPlateInfo plateInfo in webPlateInfos)
            {
                PointInfo stPoint = plateInfo.getStPoint();
                RSectionInfo rSecInfo = plateInfo.getSectionInfo();
                double width = rSecInfo.getWidth();
                double depth = rSecInfo.getDepth();
                XmlNode stPointElem = toXmlElem(xmlDoc, stPoint, girderInfo.getName() + "_WEB_" + i);
                insertWidthDepthElem(xmlDoc, stPointElem, "web", width, depth);
                girderElem.AppendChild(stPointElem);
                foreach (PointInfo mdPoint in plateInfo.getMdPoints())
                {
                    girderElem.AppendChild(toXmlElem(xmlDoc, mdPoint, girderInfo.getName() + "_WEB_" + i));
                }
                PointInfo enPoint = plateInfo.getEnPoint();
                girderElem.AppendChild(toXmlElem(xmlDoc, enPoint, girderInfo.getName() + "_WEB_" + i));

                i++;
            }
            ArrayList bfPlateInfos = girderInfo.getBfPlates();
            i = 0;
            foreach (IPolyGirderPlateInfo plateInfo in bfPlateInfos)
            {
                PointInfo stPoint = plateInfo.getStPoint();
                RSectionInfo rSecInfo = plateInfo.getSectionInfo();
                double width = rSecInfo.getWidth();
                double depth = rSecInfo.getDepth();
                XmlNode stPointElem = toXmlElem(xmlDoc, stPoint, girderInfo.getName() + "_BF_" + i);
                insertWidthDepthElem(xmlDoc, stPointElem, "bot_flange", width, depth);
                girderElem.AppendChild(stPointElem);
                foreach (PointInfo mdPoint in plateInfo.getMdPoints())
                {
                    girderElem.AppendChild(toXmlElem(xmlDoc, mdPoint, girderInfo.getName() + "_BF_" + i));
                }
                PointInfo enPoint = plateInfo.getEnPoint();
                girderElem.AppendChild(toXmlElem(xmlDoc, enPoint, girderInfo.getName() + "_BF_" + i));

                i++;
            }
            return girderElem;
        }
        public static XmlElement toXmlElem(XmlDocument xmlDoc, PointInfo pointInfo, string name)
        {
            XmlElement pointElem = xmlDoc.CreateElement(QExportingConstants.POINT_NODE_NAME);
            pointElem.SetAttribute(QExportingConstants.XML_NAME_ATTR, name);
            XmlElement xElem = xmlDoc.CreateElement(QExportingConstants.X_NODE_NAME);
            xElem.InnerText = pointInfo.getX().ToString();
            pointElem.AppendChild(xElem);
            XmlElement yElem = xmlDoc.CreateElement(QExportingConstants.Y_NODE_NAME);
            yElem.InnerText = pointInfo.getY().ToString();
            pointElem.AppendChild(yElem);
            XmlElement zElem = xmlDoc.CreateElement(QExportingConstants.Z_NODE_NAME);
            zElem.InnerText = pointInfo.getZ().ToString();
            pointElem.AppendChild(zElem);
            return pointElem;
        }
        public static void insertWidthDepthElem(XmlDocument xmlDoc, XmlNode pointElem, string prefixStr, double width, double depth)
        {
            //Width
            string widthNodeName = prefixStr + QExportingConstants.WIDTH_POSTFIX;
            XmlElement widthElem = xmlDoc.CreateElement(widthNodeName);

            XmlElement widthBElem = xmlDoc.CreateElement(widthNodeName + QExportingConstants.BEGIN_POSTFIX);
            widthBElem.InnerText = width.ToString();
            XmlElement widthEElem = xmlDoc.CreateElement(widthNodeName + QExportingConstants.END_POSTFIX);
            widthEElem.InnerText = width.ToString();
            widthElem.AppendChild(widthBElem);
            widthElem.AppendChild(widthEElem);
            //Depth
            string depthNodeName = prefixStr + QExportingConstants.DEPTH_POSTFIX;
            XmlElement depthElem = xmlDoc.CreateElement(depthNodeName);
            XmlElement depthBElem = xmlDoc.CreateElement(depthNodeName +QExportingConstants.BEGIN_POSTFIX);
            depthBElem.InnerText = depth.ToString();
            XmlElement depthEElem = xmlDoc.CreateElement(depthNodeName + QExportingConstants.END_POSTFIX);
            depthEElem.InnerText = depth.ToString();
            depthElem.AppendChild(depthBElem);
            depthElem.AppendChild(depthEElem);

            pointElem.AppendChild(widthElem);
            pointElem.AppendChild(depthElem);
        }
        public static void insertGirdersXmlNode(XmlDocument xmlDoc, ArrayList girderInfoArr)
        {
            XmlNode beamNode = xmlDoc.SelectSingleNode("//beams");
            if (null != beamNode)
            {
                beamNode.InnerText = "";
                foreach (IPolyGirderInfo girderInfo in girderInfoArr)
                {
                    XmlNode girderNode = toXmlElement(xmlDoc, girderInfo);
                    beamNode.AppendChild(girderNode);
                }
            }
        }
        public static void insertDeckLayoutLineNodes(XmlDocument xmlDoc, ArrayList layoutLineInfoArr)
        {
            XmlNode deckNode = xmlDoc.SelectSingleNode("//deck");
            if (null != deckNode)
            {
                deckNode.InnerText = "";
                foreach (LayoutLineInfo lineInfo in layoutLineInfoArr)
                {
                    XmlElement lineElem = toXmlElement(xmlDoc, lineInfo);
                    deckNode.AppendChild(lineElem);
                }
            }
        }
        public static XmlElement toXmlElement(XmlDocument xmlDoc, LayoutLineInfo lineInfo)
        {
            //Point_String Elem......
            XmlElement lineElem = xmlDoc.CreateElement(QExportingConstants.LAYOUT_LINE_NODE_NAME);
            lineElem.SetAttribute(QExportingConstants.XML_NAME_ATTR, lineInfo.getName());
            //Point Elem......
            ArrayList pointInfoArr = lineInfo.getPoints();
            int i = 0;
            foreach (PointInfo pointInfo in pointInfoArr)
            {
                XmlElement pointElem = toXmlElem(xmlDoc, pointInfo, lineInfo.getName() + "_" + i);
                pointElem.SetAttribute(QExportingConstants.XML_ID_ATTR, "" + i);
                lineElem.AppendChild(pointElem);
                i++;
            }
            return lineElem;
        }

    }
}
