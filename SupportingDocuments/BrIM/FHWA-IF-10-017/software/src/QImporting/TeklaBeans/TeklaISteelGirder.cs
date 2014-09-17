using System;
using System.Collections.Generic;
using System.Text;
using Tekla.Structures.Model;
using System.Collections;
using Tekla.Structures;
using Tekla.Structures.Model.Collaboration;

namespace TeklaBeans
{
    public class TeklaISteelGirder : Part
    {
        private Assembly assembly;
        private Beam mainBeam;
        private ArrayList topPlates;
        private ArrayList botPlates;
        private ArrayList splitPoints;
        private ISection typicalSection;

        public TeklaISteelGirder()
        {
            //mainBeam = new Beam();
            assembly = new Assembly();
            //assembly.SetMainPart(mainBeam);
        }
        public TeklaISteelGirder( Beam iBeam)
        {
            mainBeam = iBeam;
            assembly = new Assembly();
            assembly.Add(iBeam);
            assembly.SetMainPart(mainBeam);
        }
        public void setTypicalSection(ISection typicalSection)
        {
            this.typicalSection = typicalSection;
        }
        public void setMainPartName(string name)
        {
            mainBeam.Name = name;
        }
        public Beam getMainBeam()
        {
            return mainBeam;
        }
        public void setMainPartProfile(string profile)
        {
            mainBeam.Profile.ProfileString = profile;
        }
        public void setClass(string c)
        {
            mainBeam.Class = c;
            foreach (Part tp in topPlates)
            {
                tp.Class = c;
            }
            foreach (Part bp in botPlates)
            {
                bp.Class = c;
            }
        }
        private bool insertTopPlates()
        {
            if (null == topPlates)
                return true;
            foreach (Part tp in topPlates)
            {
                if (!tp.Insert())
                    return false;
            }
            return true;
        }
        private bool insertBotPlates()
        {
            if (null == botPlates)
                return true;
            foreach (Part bp in botPlates)
            {
                if (!bp.Insert())
                    return false;
            }
            return true;
        }
        public void setTopPlates(ArrayList plates)
        {
            topPlates = plates;
        }
        public void setBotPlates(ArrayList plates)
        {
            botPlates = plates;
        }

        public void setMainBeam(Beam mainBeam)
        {
            this.mainBeam = mainBeam;
            mainBeam.Position.Depth = Position.DepthEnum.BEHIND;
            if (null != typicalSection)
            {
                mainBeam.Position.DepthOffset = typicalSection.topFlangeThickness;
            }
        }
        private string getProfileName(double thick)
        {
            return "" + thick;
        }
        public void setSplits( ArrayList splitPoints)
        {
            if (null != splitPoints)
            {
                this.splitPoints = splitPoints;
            }
        }
        public void addSplitPoint(Point splitPoint)
        {
            if (null == splitPoints)
            {
                splitPoints = new ArrayList();
            }
            splitPoints.Add(splitPoint);
        }
        public override bool Insert()
        {
            assembly.Add(topPlates);
            assembly.Add(botPlates);
            if (!mainBeam.Insert())
            {
                return false;
            }
            if (!insertTopPlates())
            {
                return false;
            }
            if (!insertBotPlates())
            {
                return false;
            }
            return assembly.Insert();
        }
        public override bool Delete()
        {
            return assembly.Delete();
        }
        public override bool Modify()
        {
            return assembly.Modify();
        }
        public override bool Select()
        {
            return assembly.Select();
        }
    }
}
