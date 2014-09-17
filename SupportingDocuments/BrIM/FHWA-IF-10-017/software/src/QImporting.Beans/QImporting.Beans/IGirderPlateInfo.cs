using System;
using System.Collections.Generic;
using System.Text;

namespace QImporting.Beans
{
    public class IGirderPlateInfo
    {
        private string name;
        private RSectionInfo sectionInfo;
        private string sectionName;
        private PointInfo stPoint;
        private PointInfo enPoint;
        private string unit;

        public string getUnit()
        {
            return unit;
        }
        public void setUnit(string unit)
        {
            this.unit = unit;
        }

        public IGirderPlateInfo()
        {

        }
        public IGirderPlateInfo(PointInfo stPoint, PointInfo enPoint)
        {
            this.stPoint = stPoint;
            this.enPoint = enPoint;
        }
        public IGirderPlateInfo(string sectionName, PointInfo stPoint, PointInfo enPoint)
        {
            this.sectionName = sectionName;
            this.stPoint = stPoint;
            this.enPoint = enPoint;
        }
        public IGirderPlateInfo(string name, string sectionName, PointInfo stPoint, PointInfo enPoint)
        {
            this.name = name;
            this.sectionName = sectionName;
            this.stPoint = stPoint;
            this.enPoint = enPoint;
        }
        public IGirderPlateInfo(string name, RSectionInfo sectionInfo, PointInfo stPoint, PointInfo enPoint)
        {
            this.name = name;
            this.sectionInfo = sectionInfo;
            this.stPoint = stPoint;
            this.enPoint = enPoint;
        }
        public IGirderPlateInfo(string name, string sectionName, RSectionInfo sectionInfo, PointInfo stPoint, PointInfo enPoint)
        {
            this.name = name;
            this.sectionName = sectionName;
            this.sectionInfo = sectionInfo;
            this.stPoint = stPoint;
            this.enPoint = enPoint;
        }
        public string getName()
        {
            return name;
        }
        public string getSectionName()
        {
            return sectionName;
        }

        public PointInfo getStPoint()
        {
            return stPoint;
        }
        public PointInfo getEnPoint()
        {
            return enPoint;
        }
        public void setName( string name )
        {
            this.name = name;
        }
        public void setSectionName(string sectionName)
        {
            this.sectionName = sectionName;
        }

        public RSectionInfo getSectionInfo()
        {
            return sectionInfo;
        }

        public void setSectionInfo( RSectionInfo sectionInfo )
        {
            this.sectionInfo = sectionInfo;
        }
        public double getVolumn()
        {
            double secArea = sectionInfo.getArea();
            double length = Calculator.getLength(stPoint, enPoint);
            return secArea * length;
        }
    }
}
