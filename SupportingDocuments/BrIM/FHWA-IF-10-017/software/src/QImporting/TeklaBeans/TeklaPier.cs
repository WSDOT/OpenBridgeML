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
    public class TeklaPier : Part
    {
        private string name;
        private ArrayList columns;
        private ArrayList caps;
        public TeklaPier()
        {
        }
        public TeklaPier(string name, XmlElement elem)
        {
            XmlNodeList colElemList = elem.SelectNodes(QCS.Utils.QExportingConstants.PIER_COLUMN_NODE_NAME);
            foreach (XmlElement colElem in colElemList)
            {
                TeklaPierColumn col = new TeklaPierColumn(colElem);
                this.columns.Add(col);
            }
            XmlNodeList capElemList = elem.SelectNodes(QExportingConstants.PIER_CAP_NODE_NAME);
            foreach (XmlElement capElem in capElemList)
            {
                AnyBeam cap = new AnyBeam(capElem);
                this.caps.Add(cap);
            }
            string pierName = elem.GetAttribute(QExportingConstants.XML_NAME_ATTR);
        }

        public override bool Insert()
        {
            if (null == columns)
            {
                throw new Exception("Try to insert a column with null value.");
            }
            foreach (Beam col in this.columns)
            {
                if (!col.Insert())
                    return false;
            }
            return true;
        }
        public override bool Select()
        {
            if (null == columns)
            {
                throw new Exception("Try to select a column with null value.");
            }
            foreach (Beam col in this.columns)
            {
                if (!col.Select())
                    return false;
            }
            return true;
        }
        public override bool Modify()
        {
            if (null == this.columns)
            {
                throw new Exception("Try to modify a column with null value.");
            }
            foreach (Beam col in this.columns)
            {
                if (!col.Modify())
                    return false;
            }
            return true;
        }
        public override bool Delete()
        {
            if (null == columns)
            {
                throw new Exception("Try to delete a column with null value.");
            }
            foreach (Beam col in this.columns)
            {
                if (!col.Delete())
                    return false;
            }
            return true;
        }
        
    }
}
