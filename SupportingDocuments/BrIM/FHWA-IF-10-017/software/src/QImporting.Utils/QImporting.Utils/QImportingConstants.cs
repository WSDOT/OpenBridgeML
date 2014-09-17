using System;
using System.Collections.Generic;
using System.Text;

namespace QImporting.Utils
{
    public static class QImportingConstants
    {
        //XML file constants......
        public const string GIRDER_NODE_NAME = "girder";
        public const string DECK_NODE_NAME = "deck";
        public const string GIRDER_POINT_NODE_NAME = "point";
        public const string XML_POINT_NODE_NAME = "point";
        public const string XML_LAYOUT_LINE_NODE_NAME = "point_string";
        public const string X_NODE_NAME = "x_coord";
        public const string Y_NODE_NAME = "y_coord";
        public const string Z_NODE_NAME = "elev";
        public const string WEB_WIDTH_NODE_NAME = "web_width_begin";
        public const string WEB_DEPTH_NODE_NAME = "web_thick_begin";
        public const string WEB_SECTION_PREFIX = "Web";

        //Revit component constants......
        public const string TF_NODE_NAME = "top_flange_thick";
        public const string TF_WIDTH_NODE_NAME = "top_flange_width_begin";
        public const string TF_DEPTH_NODE_NAME = "top_flange_thick_begin";
        public const string TF_SECTION_PREFIX = "Top Flange";

        public const string BF_NODE_NAME = "bot_flange_thick";
        public const string BF_WIDTH_NODE_NAME = "bot_flange_width_begin";
        public const string BF_DEPTH_NODE_NAME = "bot_flange_thick_begin";
        public const string BF_SECTION_PREFIX = "Bot Flange";

        public const string WIDTH_PARAM_NAME = "width";
        public const string DEPTH_PARAM_NAME = "depth";

        //General unit constants.....
        public const string UNIT_MM = "MM";
        public const string UNIT_M = "M";
        public const string UNIT_FOOT = "FOOT";
        public const string UNIT_INCH = "INCH";


    }
}
