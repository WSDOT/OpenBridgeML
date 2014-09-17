using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace TeklaBeans
{
    public class CSection : Section
    {
        private double diameter;
        private string profileString;

        public CSection(XmlElement elem)
        {
            XmlNodeList sonNodes = elem.ChildNodes;
            foreach (XmlElement sonElem in sonNodes)
            {
                if (sonElem.Name.Equals("diameter"))
                {
                    this.diameter = QCS.Utils.Utils.toDouble(sonElem.InnerText);
                }
            }
        }
        public double getDiameter()
        {
            return this.diameter;
        }
        public void setDiameter(double diameter)
        {
            this.diameter = diameter;
        }
        public string getProfileString()
        {
            if (this.profileString == null)
            {
                this.profileString = "D" + this.diameter;
            }
            return this.profileString;
        }
        public void setProfileString(string profileString)
        {
            this.profileString = profileString;
        }
    }
}
