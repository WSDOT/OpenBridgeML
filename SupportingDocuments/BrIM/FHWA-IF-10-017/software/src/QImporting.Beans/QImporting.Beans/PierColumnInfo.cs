using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace QImporting.Beans
{
    public class PierColumnInfo
    {
        private string name;
        private SectionInfo sectionInfo;
        private double depth;
        private int unit;

        public int getUnit()
        {
            return unit;
        }
        public void setUnit(int unit)
        {
            this.unit = unit;
        }

        public void setName(string name)
        {
            this.name = name;
        }
        public void setSectionInfo(SectionInfo sectionInfo)
        {
            this.sectionInfo = sectionInfo;
        }
        public string getName()
        {
            return name;
        }
        public SectionInfo getSectionInfo()
        {
            return sectionInfo;
        }
        public void setDepth(double depth)
        {
            this.depth = depth;
        }
        public double getDepth()
        {
            return depth;
        }
    }
}
