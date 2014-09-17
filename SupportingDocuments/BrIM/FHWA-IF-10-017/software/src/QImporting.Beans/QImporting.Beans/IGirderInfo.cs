using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace QImporting.Beans
{
    public class IGirderInfo
    {
        private string name;
        private string material;
        private ArrayList tfPlates;
        private ArrayList bfPlates;
        private PointInfo stPoint;
        private PointInfo enPoint;
        private ArrayList webPlates;
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
        public void setStPoint(PointInfo stPoint)
        {
            this.stPoint = stPoint;
        }
        public void setEnPoint(PointInfo enPoint)
        {
            this.enPoint = enPoint;
        }
        public void addWebPlate(IGirderPlateInfo webPlate)
        {
            if (null == webPlates)
            {
                webPlates = new ArrayList();
            }
            webPlates.Add(webPlate);
        }
        public void addTfPlate(IGirderPlateInfo tfPlate)
        {
            if (null == tfPlates)
            {
                tfPlates = new ArrayList();
            }
            tfPlates.Add(tfPlate);
        }
        public void addBfPlate(IGirderPlateInfo bfPlate)
        {
            if (null == bfPlates)
            {
                bfPlates = new ArrayList();
            }
            bfPlates.Add(bfPlate);
        }
        public void setWebPlates(ArrayList webPlates)
        {
            this.webPlates = webPlates;
        }
        public void setTfPlates(ArrayList tfPlates)
        {
            this.tfPlates = tfPlates;
            int i = 0;
            foreach (IPolyGirderPlateInfo plateInfo in tfPlates)
            {
                if (i == 0)
                {
                    setStPoint(plateInfo.getStPoint());
                }
                i++;
                if (i == tfPlates.Count)
                {
                    setEnPoint(plateInfo.getEnPoint());
                }
            }
        }
        public void setBfPlates(ArrayList bfPlates)
        {
            this.bfPlates = bfPlates;
        }
        public ArrayList getWebPlates()
        {
            return webPlates;
        }
        public ArrayList getTfPlates()
        {
            return tfPlates;
        }
        public ArrayList getBfPlates()
        {
            return bfPlates;
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

    }
}
