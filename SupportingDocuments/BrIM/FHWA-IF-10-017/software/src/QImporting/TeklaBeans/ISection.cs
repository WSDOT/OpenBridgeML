using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace TeklaBeans
{
    public class ISection : Section
    {
        public double topFlangeWidth;
        public double botFlangeWidth;
        public double webDepth;
        public double topFlangeThickness;
        public double botFlangeThickness;
        public double webThickness;
        public ISection(XmlElement elem)
        {

        }
    }
}
