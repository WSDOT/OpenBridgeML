using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace QImporting.Beans
{
    public class CIPSlabInfo
    {
        private string name;
        private RSectionInfo sectionInfo;
        private ArrayList points;
        private double depth;
        private string unit;

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
        public void setSectionInfo(RSectionInfo sectionInfo)
        {
            this.sectionInfo = sectionInfo;
        }
        public string getName()
        {
            return name;
        }
        public RSectionInfo getSectionInfo()
        {
            return sectionInfo;
        }
        public void addPoint(PointInfo point)
        {
            if (null == points)
            {
                points = new ArrayList();
            }
            points.Add(point);
        }
        public void setPoints(ArrayList points)
        {
            this.points = points;
        }
        public void setDepth(double depth)
        {
            this.depth = depth;
        }
        public double getDepth()
        {
            return depth;
        }
        public double getSurfaceArea()
        {
            return sectionInfo.getArea();
        }
        public double getVolumn()
        {
            return sectionInfo.getArea() * depth;
        }
    }
}
