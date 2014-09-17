using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Tekla.Structures.Model;
using Tekla.Structures;
using System.Xml;

namespace TeklaBeans
{
    public class CIPDeckRSlab : Part
    {
        public string name;
        private TPoint originPoint;
        public ContourPlate slab;
        public TPoint point1;
        public TPoint point2;
        public TPoint point3;
        public TPoint point4;
        public string material;

        public CIPDeckRSlab()
        {
            originPoint = new TPoint(0, 0, 0);
        }
        public CIPDeckRSlab(string name, XmlElement pElem1, XmlElement pElem2, XmlElement pElem3, XmlElement pElem4, XmlElement depthElem)
        {
            this.point1 = new TPoint(pElem1);
            this.point2 = new TPoint(pElem2);
            this.point3 = new TPoint(pElem3);
            this.point4 = new TPoint(pElem4);
            TContourPoint cp1 = new TContourPoint(pElem1);
            TContourPoint cp2 = new TContourPoint(pElem2);
            TContourPoint cp3 = new TContourPoint(pElem3);
            TContourPoint cp4 = new TContourPoint(pElem4);
            this.slab = new ContourPlate();
            this.slab.AddContourPoint(cp1);
            this.slab.AddContourPoint(cp2);
            this.slab.AddContourPoint(cp3);
            this.slab.AddContourPoint(cp4);
            this.name = name;
            this.slab.Name = name;
            this.slab.Profile.ProfileString = "PL"+depthElem.InnerText;
            this.slab.CastUnitType = CastUnitTypeEnum.CAST_IN_PLACE;
            //slab.Identifier = new Identifier();
            this.slab.Material.MaterialString = "6000";
            this.slab.Position.Depth = Position.DepthEnum.BEHIND;
        }
        public CIPDeckRSlab(ContourPlate slab)
        {
            this.slab = slab;
            if (null == name)
            {
                this.name = slab.Name;
                this.Name = slab.Name;
            }
            setPoints(slab.Contour.ContourPoints);
        }
        public CIPDeckRSlab(string name, ContourPlate slab)
        {
            this.name = name;
            this.Name = name;
            this.slab = slab;
            setPoints(slab.Contour.ContourPoints);
        }
        public void setPoints(TPoint point1, TPoint point2, TPoint point3, TPoint point4)
        {
            this.point4 = point4;
            this.point3 = point3;
            this.point2 = point2;
            this.point1 = point1;
            if (null == originPoint)
            {
                this.originPoint = point1;
            }
        }
        public void setPoints(ArrayList contourPointArr)
        {
            if(null == contourPointArr || contourPointArr.Count <4)
                return;
            IComparer xSortClass = new TeklaBeans.TeklaContourPointSortByX();
            IComparer ySortClass = new TeklaBeans.TeklaContourPointSortByY();
            contourPointArr.Sort(xSortClass);
            Array contourPoints = contourPointArr.ToArray();
            ContourPoint contourPoint1 = contourPoints.GetValue(0) as ContourPoint;
            ContourPoint contourPoint2 = contourPoints.GetValue(1) as ContourPoint;
            ContourPoint contourPoint3 = contourPoints.GetValue(2) as ContourPoint;
            ContourPoint contourPoint4 = contourPoints.GetValue(3) as ContourPoint;
            if (Convert.ToInt32(contourPoint1.Y) > Convert.ToInt32(contourPoint2.Y))
            {
                this.point1 = new TPoint(contourPoint2);
                this.point4 = new TPoint(contourPoint1);
            }
            else
            {
                this.point1 = new TPoint(contourPoint1);
                this.point4 = new TPoint(contourPoint2);
            }
            if (Convert.ToInt32(contourPoint3.Y) > Convert.ToInt32(contourPoint4.Y))
            {
                this.point2 = new TPoint(contourPoint4);
                this.point3 = new TPoint(contourPoint3);
            }
            else
            {
                this.point2 = new TPoint(contourPoint3);
                this.point3 = new TPoint(contourPoint4);
            }
            this.originPoint = point1;
        }
        public void setPoints(TPoint originPoint, TPoint point1, TPoint point2, TPoint point3, TPoint point4)
        {
            setPoints(point1, point2, point3, point4);
            this.originPoint = originPoint;
        }
        public void setPoint(TPoint point, int index)
        {
            switch (index)
            {
                case 1:
                    point1 = point;
                    if (null == originPoint)
                        originPoint = point;
                    break;
                case 2:
                    point2 = point;
                    break;
                case 3:
                    point3 = point;
                    break;
                case 4:
                    point4 = point;
                    break;
                default:
                    break;
            }
        }
        public void setOriginPoint(TPoint originPoint)
        {
            this.originPoint = originPoint;
        }
        public TPoint getPoint(int index)
        {
            switch (index)
            {
                case 1:
                    return point1;
                case 2:
                    return point2;
                case 3:
                    return point3;
                case 4:
                    return point4;
                default:
                    break;
            }
            return null;
        }
        private void setSlabPoints()
        {
            if (null == point1 || null == point2 || null == point3 || null == point4)
            {
                return;
            }
            slab.AddContourPoint(new TContourPoint(point1));
            slab.AddContourPoint(new TContourPoint(point2));
            slab.AddContourPoint(new TContourPoint(point3));
            slab.AddContourPoint(new TContourPoint(point4));
        }
        private void setSlab()
        {
            slab.Name = name;
            slab.Material.MaterialString = material;
            slab.Class = QCS.Utils.QImportingConstants.TEKLA_DECK_DEFAULT_CLASS;
            setSlabPoints();

        }

        public override bool Insert()
        {
            if (null == slab)
            {
                throw new Exception("Try to insert a slab with null value.");
            }
            return slab.Insert();
        }
        public override bool Select()
        {
            if (null == slab)
            {
                throw new Exception("Try to select a slab with null value.");
            }
            return slab.Select();
        }
        public override bool Modify()
        {
            if (null == slab)
            {
                throw new Exception("Try to modify a slab with null value.");
            }
            return slab.Modify();
        }
        public override bool Delete()
        {
            if (null == slab)
            {
                throw new Exception("Try to delete a slab with null value.");
            }
            return slab.Delete();
        }
        
    }
}
