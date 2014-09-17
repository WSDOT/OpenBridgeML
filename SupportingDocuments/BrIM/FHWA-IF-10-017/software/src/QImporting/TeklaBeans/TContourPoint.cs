using System;
using System.Collections.Generic;
using System.Text;
using Tekla.Structures.Model;
using Tekla.Structures;
using System.Xml;
using QCS.Utils;

namespace TeklaBeans
{
    public class TContourPoint : ContourPoint
    {
        public TContourPoint(XmlElement elem)
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
        public TContourPoint(double X, double Y, double Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.Chamfer = new Chamfer() ;
        }
        public TContourPoint(Point p)
        {
            this.X = p.X;
            this.Y = p.Y;
            this.Z = p.Z;
            this.Chamfer = new Chamfer();
        }
        public TContourPoint(Point p, Point Origin)
        {
            this.X = p.X - Origin.X;
            this.Y = p.Y - Origin.Y;
            this.Z = p.Z - Origin.Z;
            this.Chamfer = new Chamfer();
        }
        public TContourPoint(double X, double Y, double Z, Point Origin)
        {
            this.X = X - Origin.X;
            this.Y = Y - Origin.Y;
            this.Z = Z - Origin.Z;
            this.Chamfer = new Chamfer();
            
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
        public void setTPoint(TPoint p)
        {
            this.X = p.X;
            this.Y = p.Y;
            this.Z = p.Z;
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
    }
}
