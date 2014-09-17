using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace TeklaBeans
{
    public class RSection : Section
    {
        private double width;
        private double depth;

        public RSection(XmlElement elem)
        {
            XmlNodeList sonNodes = elem.ChildNodes;
            foreach (XmlElement sonElem in sonNodes)
            {
                if (sonElem.Name.Equals("depth"))
                {
                    this.depth = QCS.Utils.Utils.toDouble(sonElem.InnerText);
                }
                if (sonElem.Name.Equals("width"))
                {
                    this.width = QCS.Utils.Utils.toDouble(sonElem.InnerText);
                }
            }
            this.profileString = this.depth + "X" + this.width;
        }
        public double getWidth()
        {
            return this.width;
        }
        public void setWidth(double width)
        {
            this.width = width;
        }
        public double getDepth()
        {
            return this.depth;
        }
        public void setDepth(double depth)
        {
            this.depth = depth;
        }
        public string getProfileString()
        {
            if (this.profileString == null)
            {
                this.profileString = this.depth + "X" + this.width;
            }
            return this.profileString;
        }
        public void setProfileString(string profileString)
        {
            this.profileString = profileString;
        }
    }
}
