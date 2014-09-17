using System;
using System.Collections.Generic;
using System.Text;
using Tekla.Structures.Model;
using System.Collections;
using Tekla.Structures;
using Tekla.Structures.Model.Collaboration;
using System.Xml;

namespace TeklaBeans
{
    public class TeklaISteelPolyGirder : Part
    {
        private Assembly assembly;
        private ArrayList webPlates;
        private ArrayList topPlates;
        private ArrayList botPlates;
        private ArrayList splitPoints;
        private ISection typicalSection;

        public TeklaISteelPolyGirder()
        {
            //mainBeam = new Beam();
            assembly = new Assembly();
            //assembly.SetMainPart(mainBeam);
        }
        public TeklaISteelPolyGirder( XmlElement elem)
        {
            if (elem == null)
                return;
            string name = elem.GetAttribute("name");
            this.Name = name;

            XmlNodeList sonNodes = elem.ChildNodes;
            ArrayList pointArr = new ArrayList();
            foreach (XmlElement sonElem in sonNodes)
            {                
                if (sonElem.Name.Equals(QCS.Utils.QImportingConstants.FLANGE_NODE_NAME))
                {
                    string type = sonElem.GetAttribute(QCS.Utils.QImportingConstants.TYPE_ATTR_NAME);
                    if (QCS.Utils.QImportingConstants.TOP_FLANGE_ATTR.Equals(type))
                    {
                        XmlNodeList plateNodes = sonElem.ChildNodes;
                        this.topPlates = new ArrayList();
                        foreach (XmlElement plateElem in plateNodes)
                        {
                            AnyBeam beam = new AnyBeam(plateElem);
                            this.topPlates.Add(beam);
                        }
                    }
                    else if (QCS.Utils.QImportingConstants.BOTTOM_FLANGE_ATTR.Equals(type))
                    {
                        XmlNodeList plateNodes = sonElem.ChildNodes;
                        this.botPlates = new ArrayList();
                        foreach (XmlElement plateElem in plateNodes)
                        {
                            AnyBeam beam = new AnyBeam(plateElem);
                            this.botPlates.Add(beam);
                        }
                    }
                    
                }
                else if(sonElem.Name.Equals(QCS.Utils.QImportingConstants.WEB_NODE_NAME))
                {
                    XmlNodeList plateNodes = sonElem.ChildNodes;
                    this.webPlates = new ArrayList();
                    foreach (XmlElement plateElem in plateNodes)
                    {
                        AnyBeam beam = new AnyBeam(plateElem);
                        this.webPlates.Add(beam);
                    }
                }
            }

        }
        public void setTypicalSection(ISection typicalSection)
        {
            this.typicalSection = typicalSection;
        }
        public void setClass(string c)
        {
            foreach (Part web in webPlates)
            {
                web.Class = c;
            }
            foreach (Part tp in topPlates)
            {
                tp.Class = c;
            }
            foreach (Part bp in botPlates)
            {
                bp.Class = c;
            }
        }
        private bool insertWebPlates()
        {
            if (null == webPlates)
                return false;
            foreach (Part web in webPlates)
            {
                if (!web.Insert())
                    return false;
            }
            return true;
        }
        private bool insertTopPlates()
        {
            if (null == topPlates)
                return false;
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
                return false;
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
        public ArrayList getTopPlates()
        {
            return topPlates;
        }
        public void setBotPlates(ArrayList plates)
        {
            botPlates = plates;
        }
        public ArrayList getBotPlates()
        {
            return botPlates;
        }
        public void setWebPlates(ArrayList plates)
        {
            webPlates = plates;
        }
        public ArrayList getWebPlates()
        {
            return webPlates;
        }
        private string getProfileName(double thick)
        {
            return "BeamPlate_" + thick;
        }
        public void setSplits( ArrayList splitPoints)
        {
            if (null != splitPoints)
            {
                this.splitPoints = splitPoints;
            }
            throw new Exception("The method or operation is not implemented.");
        }
        public void addSplitPoint(Point splitPoint)
        {
            if (null == splitPoints)
            {
                splitPoints = new ArrayList();
            }
            splitPoints.Add(splitPoint);
            throw new Exception("The method or operation is not implemented.");
        }
        public override bool Insert()
        {
            if (null == assembly)
                this.assembly = new Assembly();
            assembly.Add(webPlates);
            assembly.Add(topPlates);
            assembly.Add(botPlates);
            if (!insertWebPlates())
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
