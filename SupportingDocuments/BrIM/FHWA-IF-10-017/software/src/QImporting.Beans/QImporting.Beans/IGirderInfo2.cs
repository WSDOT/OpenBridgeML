using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace QImporting.Beans
{
    public class IGirderInfo2
    {
        private string name;
        private string material;
        private ArrayList tfPoints;
        private ArrayList bfPoints;
        private PointInfo stPoint;
        private PointInfo enPoint;
        private double[] tfDepths;
        private double[] bfDepths;
        private ISectionInfo sectionInfo;
        private string unit;

        public string getUnit()
        {
            return unit;
        }
        public void setUnit(string unit)
        {
            this.unit = unit;
        }
        public PointInfo getStPoint()
        {
            return stPoint;
        }
        public PointInfo getEnPoint()
        {
            return enPoint;
        }
        public void setSTPoint(PointInfo stPoint)
        {
            this.stPoint = stPoint;
        }
        public void setENPoint(PointInfo enPoint)
        {
            this.enPoint = enPoint;
        }
        public void addTFPoint(PointInfo tfPoint)
        {
            if (null == tfPoints)
            {
                tfPoints = new ArrayList();
            }
            tfPoints.Add(tfPoint);
        }
        public void addBFPoint(PointInfo bfPoint)
        {
            if (null == bfPoints)
            {
                bfPoints = new ArrayList();
            }
            bfPoints.Add(bfPoint);
        }
        public void setTFPoints(ArrayList tfPoints)
        {
            this.tfPoints = tfPoints;
        }
        public void setBFPoints(ArrayList bfPoints)
        {
            this.bfPoints = bfPoints;
        }
        public ArrayList getTFPoints()
        {
            return tfPoints;
        }
        public ArrayList getBFPoints()
        {
            return bfPoints;
        }
        public void setName(string name)
        {
            this.name = name;
        }
        public void setMaterial(string material)
        {
            this.material = material;
        }

        public string getName()
        {
            return name;
        }
        public string getMaterial()
        {
            return material;
        }

        public void setSectionInfo(ISectionInfo sectionInfo)
        {
            this.sectionInfo = sectionInfo;
        }
        public ISectionInfo getSectionInfo()
        {
            return sectionInfo;
        }
        public void addTFDepth(double tfDepth)
        {
            if (null == tfDepths)
            {
                tfDepths = new double[1];
                tfDepths[0] = tfDepth;
                return;
            }
            double[] tempArr = new double[tfDepths.Length + 1];
            for (int i = 0; i < tfDepths.Length; i++)
            {
                tempArr[i] = tfDepths[i];
            }
            tempArr[tempArr.Length - 1] = tfDepth;
            tfDepths = tempArr;
        }
        public void addBFDepth(double bfDepth)
        {
            if (null == bfDepths)
            {
                bfDepths = new double[1];
                bfDepths[0] = bfDepth;
                return;
            }
            double[] tempArr = new double[bfDepths.Length + 1];
            for (int i = 0; i < bfDepths.Length; i++)
            {
                tempArr[i] = bfDepths[i];
            }
            tempArr[tempArr.Length - 1] = bfDepth;
            bfDepths = tempArr;
        }
        public void setTFDepths(double[] tfDepths)
        {
            this.tfDepths = tfDepths;
        }
        public void setBFDepths(double[] bfDepths)
        {
            this.bfDepths = bfDepths;
        }
        public double[] getTFDepths()
        {
            return tfDepths;
        }
        public double[] getBFDepths()
        {
            return bfDepths;
        }

    }
}
