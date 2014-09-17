using System;
using System.Collections.Generic;
using System.Text;

namespace QImporting.Utils
{
    public static class QExportingConstants
    {
        public const string EX_TPL_FILE_NAME = @"D:\linkage_template.xml";
        public const string EX_FILE_DIR = @"D:\";
        public const string EX_FILE_NAME_PREFIX = "Exporting_Linkage_";
        //XML file constants......
        public const string GIRDER_NODE_NAME = "girder";
        public const string XML_NAME_ATTR = "name";
        public const string XML_ID_ATTR = "id";
        public const string XML_DESC_ATTR = "desc";
        public const string GIRDER_NAME_ATTR = "name";
        public const string GIRDER_POINT_NODE_NAME = "point";
        public const string GIRDER_POINT_NAME_ATTR = "name";
        public const string X_NODE_NAME = "x_coord";
        public const string Y_NODE_NAME = "y_coord";
        public const string Z_NODE_NAME = "elev";
        public const string WEB_WIDTH_NODE_NAME = "web_thick_begin";
        public const string WEB_DEPTH_NODE_NAME = "web_depth_begin";
        public const string WEB_SECTION_PREFIX = "Web";

        //Revit component constants......
        public const string DEPTH_POSTFIX = "_thick";
        public const string WIDTH_POSTFIX = "_width";
        public const string BEGIN_POSTFIX = "_begin";
        public const string END_POSTFIX = "_end";

        public const string TF_NODE_DEPTH = "top_flange_thick";
        public const string TF_NODE_WIDTH = "top_flange_width";
        public const string TF_WIDTH_NODE_END = "top_flange_width_end";
        public const string TF_WIDTH_NODE_BEGIN = "top_flange_width_begin";
        public const string TF_DEPTH_NODE_END = "top_flange_thick_end";
        public const string TF_DEPTH_NODE_BEGIN = "top_flange_thick_begin";
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

        //
        public const int RSLAB_LEFT = 1;
        public const int RSLAB_RIGHT = 2;
        public const int RSLAB_FRONT = 3;
        public const int RSLAB_REAR = 4;
        //Deck...
        public const string DECK_LAYOUT_LINE_NODE_NAME = "point_string";
        //Section...
        public const string XML_SECTION_TYPE_ATTR = "type";
        public const string XML_SECTION_TYPE_REC = "R";
        public const string XML_SECTION_TYPE_CIR = "C";
        public const string XML_SECTION_TYPE_I = "I";

    }
}
