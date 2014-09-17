using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Tekla.Structures.Model;
using Tekla.Structures;
using System.Xml;
using QCS.Utils;

namespace TeklaBeans
{
    public class TeklaPierColumn : Part
    {
        private string name;
        private Beam column;
        private Section section;
        public TeklaPierColumn()
        {
        }
        public TeklaPierColumn(XmlElement colElem)
        {
            XmlElement secElem = (XmlElement)colElem.SelectSingleNode(QCS.Utils.QImportingConstants.SECTION_NODE_NAME);
            if (secElem.GetAttribute(QCS.Utils.QExportingConstants.XML_TYPE_ATTR).Equals(QCS.Utils.QExportingConstants.XML_SECTION_TYPE_REC))
            {
                this.section = new RSection(secElem);
            }
            else
            {
                this.section = new CSection(secElem);
            }
            string colName = colElem.GetAttribute(QExportingConstants.XML_NAME_ATTR);
            this.column = new Beam();
            XmlNodeList pointList = colElem.SelectNodes(QExportingConstants.POINT_NODE_NAME);
            foreach (XmlElement pointElem in pointList)
            {
                if (pointElem.GetAttribute(QExportingConstants.XML_NAME_ATTR).Equals(QExportingConstants.STARTPOINT_ATTR_VALUE))
                {
                    this.column.StartPoint = new TPoint(pointElem);
                }
                else
                {
                    this.column.EndPoint = new TPoint(pointElem);
                }
            }
            this.column.Name = colName;
            this.column.Profile.ProfileString = this.section.profileString;
        }
        public TeklaPierColumn(string name, XmlElement colElem)
        {
            XmlElement secElem = (XmlElement)colElem.SelectSingleNode(QCS.Utils.QImportingConstants.SECTION_NODE_NAME);
            if(secElem.GetAttribute(QCS.Utils.QExportingConstants.XML_TYPE_ATTR).Equals(QCS.Utils.QExportingConstants.XML_SECTION_TYPE_REC))
            {
                this.section = new RSection(secElem);
            }
            else
            {
                this.section = new CSection(secElem);
            }
            string colName = colElem.GetAttribute(QExportingConstants.XML_NAME_ATTR);
            this.column = new Beam();
            XmlNodeList pointList = colElem.SelectNodes(QExportingConstants.POINT_NODE_NAME);
            foreach(XmlElement pointElem in pointList)
            {
                if(pointElem.GetAttribute(QExportingConstants.XML_NAME_ATTR).Equals(QExportingConstants.STARTPOINT_ATTR_VALUE))
                {
                    this.column.StartPoint = new TPoint(pointElem);
                }
                else
                {
                    this.column.EndPoint = new TPoint(pointElem);
                }
            }
            this.column.Name = colName;
            this.column.Profile.ProfileString = this.section.profileString;
        }

        private Beam getColumn()
        {
            return this.column;
        }
        private void setColumn(Beam beam)
        {
            this.column = beam;
        }
        public override bool Insert()
        {
            if (null == column)
            {
                throw new Exception("Try to insert a column with null value.");
            }
            return this.column.Insert();
        }
        public override bool Select()
        {
            if (null == column)
            {
                throw new Exception("Try to select a column with null value.");
            }
            return column.Select();
        }
        public override bool Modify()
        {
            if (null == column)
            {
                throw new Exception("Try to modify a column with null value.");
            }
            return column.Modify();
        }
        public override bool Delete()
        {
            if (null == column)
            {
                throw new Exception("Try to delete a column with null value.");
            }
            return column.Delete();
        }
        
    }
}
