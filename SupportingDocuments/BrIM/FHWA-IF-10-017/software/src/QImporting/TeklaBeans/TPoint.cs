using System;
using System.Collections.Generic;
using System.Text;
using Tekla.Structures;
using System.Xml;
using QCS.Utils;
using Tekla.Structures.Model;

namespace TeklaBeans
{
    public class TPoint : Point
    {
        public TPoint()
        {
        }
        public TPoint(Point p)
        {
            this.X = p.X;
            this.Y = p.Y;
            this.Z = p.Z;
        }
        public TPoint(ContourPoint p)
        {
            this.X = p.X;
            this.Y = p.Y;
            this.Z = p.Z;
        }
        public TPoint(XmlElement elem)
        {
            if (elem == null)
                return;
            XmlNodeList sonNodes = elem.ChildNodes;
            foreach (XmlElement sonElem in sonNodes)
            {
                if (sonElem.Name.Equals(QExportingConstants.X_NODE_NAME))
                {
                    this.X = Utils.toDouble(sonElem.InnerText);
                }
                else if (sonElem.Name.Equals(QExportingConstants.Y_NODE_NAME))
                {
                    this.Y = Utils.toDouble(sonElem.InnerText);
                }
                else if (sonElem.Name.Equals(QExportingConstants.Z_NODE_NAME))
                {
                    this.Z = Utils.toDouble(sonElem.InnerText);
                }

            }
        }
        public TPoint(double X, double Y, double Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;

        }

        public TPoint(double X, double Y, double Z, TPoint Origin)
        {
            this.X = X - Origin.X;
            this.Y = Y - Origin.Y;
            this.Z = Z - Origin.Z;

        }
        public void setX( double X)
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
        public void setTPoint(double X, double Y, double Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }
        public void Translate(TPoint tpoint)
        {
            Translate(tpoint.X, tpoint.Y, tpoint.Z);
        }

        public void Rotation( TPoint origin , double angle)
        {
            double xi = angle / 180 * Math.PI;
            X = X * Math.Cos(xi) - Y * Math.Sin(xi);
            Y = Y * Math.Cos(xi) + X * Math.Sin(xi);
        }
        //private void translateByUnit()
        //{
        //    if (String.IsNullOrEmpty(OriginUnit) || String.IsNullOrEmpty(CurrUnit))
        //        return;
        //    if (OriginUnit == "FT")
        //    {
        //        if (CurrUnit == "MM")
        //        {
        //            setTPoint(X * 12 * 25.4, Y * 12 * 25.4, Z * 12 * 25.4);
        //        }
        //    }
        //    if (OriginUnit == "IN")
        //    {
        //        if (CurrUnit == "MM")
        //        {
        //            setTPoint(X * 2.54, Y * 2.54, Z * 2.54);
        //        }
        //    }
        //    if (OriginUnit == "M")
        //    {
        //        if (CurrUnit == "MM")
        //        {
        //            setTPoint(X * 1000, Y * 1000, Z * 1000);
        //        }
        //    }
        //}
    }
}
