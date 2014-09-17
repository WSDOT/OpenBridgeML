using System;
using System.Collections.Generic;
using System.Text;

namespace TeklaBeans
{
    class BeamPlateInfo
    {
        public string name;
        public double thick = 0.0;
        public string sectionName;
        public TPoint startPoint;
        public TPoint endPoint;

        public BeamPlateInfo()
        {

        }
        public BeamPlateInfo(string name)
        {
            this.name = name;
        }
        public BeamPlateInfo(string name, string sectionName )
        {
            this.name = name;
            this.sectionName = sectionName;
        }
        public BeamPlateInfo(TPoint startPoint, TPoint endPoint)
        {
            this.startPoint = startPoint;
            this.endPoint = endPoint;
        }
        public BeamPlateInfo(string sectionName, TPoint startPoint, TPoint endPoint)
        {
            this.sectionName = sectionName;
            this.startPoint = startPoint;
            this.endPoint = endPoint;
        }
        public BeamPlateInfo(string name, string sectionName, TPoint startPoint, TPoint endPoint)
        {
            this.name = name;
            this.sectionName = sectionName;
            this.startPoint = startPoint;
            this.endPoint = endPoint;
        }
    }
}
