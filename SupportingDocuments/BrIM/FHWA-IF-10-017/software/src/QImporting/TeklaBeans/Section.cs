using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace TeklaBeans
{
    public class Section
    {
        public string name;
        public string profileString;
        public Section()
        {

        }
        public Section(XmlElement elem)
        {
            if (null == elem)
                return;
            if (null != elem.GetAttribute("name"))
            {
                this.name = elem.GetAttribute("name");
            }
        }
        public string getProfileString()
        {
            return this.profileString;
        }
        public void setProfileString(string profileString)
        {
            this.profileString = profileString;
        }
    }
}
