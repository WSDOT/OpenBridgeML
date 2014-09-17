using System;
using System.Collections.Generic;
using System.Text;
using QImporting.Beans;
using System.Xml;
using System.Collections;
using TeklaBeans;
using Tekla.Structures.Model;
using Tekla.Structures;

namespace TeklaConverter
{
    public class Converter
    {
        public static Beam toBeam(IPolyGirderPlateInfo plateInfo)
        {
            Beam beam = new Beam();
            beam.Name = plateInfo.getName();
            TPoint stPoint = toTPoint(plateInfo.getStPoint());
            TPoint enPoint = toTPoint(plateInfo.getEnPoint());
            beam.StartPoint = stPoint;
            beam.EndPoint = enPoint;
            SectionInfo secInfo = plateInfo.getSectionInfo();
            beam.Profile.ProfileString = secInfo.toTeklaProfileStr();
            return beam;
        }
        public static PolyBeam toPolyBeam(IPolyGirderPlateInfo plateInfo)
        {
            PolyBeam beam = new PolyBeam();
            beam.Name = plateInfo.getName();

            SectionInfo secInfo = plateInfo.getSectionInfo();
            beam.Profile.ProfileString = secInfo.toTeklaProfileStr();
            return beam;
        }
        public static TPoint toTPoint(PointInfo pointInfo)
        {
            if (null == pointInfo)
            {
                throw new Exception("The New TPoint is null....");
            }
            double x = pointInfo.getX();
            double y = pointInfo.getY();
            double z = pointInfo.getZ();
            TPoint point = new TPoint(x, y, z);
            return point;
        }
        public static TContourPoint toTContourPoint(PointInfo pointInfo)
        {
            
            double x = pointInfo.getX();
            double y = pointInfo.getY();
            double z = pointInfo.getZ();
            TContourPoint point = new TContourPoint(x, y, z);
            return point;
        }
    }
}
