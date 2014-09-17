using System;
using System.Collections.Generic;
using System.Text;

namespace QCS.Utils
{
    public static class QExportingConstants
    {
        public const string EX_TPL_FILE_NAME = @"D:\linkage_template.xml";
        public const string EX_FILE_DIR = @"D:\";
        public const string EX_FILE_NAME_PREFIX = "Exporting_Linkage_";
        //XML file constants......
        public const string GIRDER_NODE_NAME = "girder";
        public const string XML_NAME_ATTR = "name";
        public const string XML_TYPE_ATTR = "type";
        public const string XML_UNIT_ATTR = "unit";
        public const string XML_ID_ATTR = "id";
        public const string XML_DESC_ATTR = "desc";
        public const string GIRDER_NAME_ATTR = "name";
        public const string POINT_NODE_NAME = "point";
        public const string GIRDER_POINT_NAME_ATTR = "name";
        public const string X_NODE_NAME = "x";
        public const string Y_NODE_NAME = "y";
        public const string Z_NODE_NAME = "z";
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
        public const string LAYOUT_LINE_NODE_NAME = "point_string";
        public const string PLATE_NODE_NAME = "plate";
        //Piers...
        public const string PIER_NODE_NAME = "pier";
        public const string PIER_CAP_NODE_NAME = "pier_cap";
        public const string ABUTMENT_NODE_NAME = "abutment";
        public const string ABUTMENT_CAP_NODE_NAME = "pier_cap";
        public const string PIER_COLUMN_NODE_NAME = "pier_column";
        public const string ABUT_COLUMN_NODE_NAME = "abutment_column";
        //Section...
        public const string XML_SECTION_TYPE_ATTR = "type";
        public const string XML_SECTION_TYPE_REC = "R";
        public const string XML_SECTION_TYPE_CIR = "C";
        public const string XML_SECTION_TYPE_I = "I";
        public const string XML_DIAMETER_NODE_NAME = "diameter";

        //Precast/Prestressed
        public const string XML_PRECASTSPAN_NODE_NAME = "precast_span";
        public const string XML_PRESTRESSEDSTRANDS_NODE_NAME = "prestressed_strands";
        public const string XML_STRANDROW_NODE_NAME = "strand_row";
        public const string XML_STRAND_NODE_NAME = "strand";
        public const string XML_GRADE_ATTR = "grade";
        public const string XML_SIZE_ATTR = "size";
        public const string XML_BENDINGRADIUS_ATTR = "bending_radius";
        public const string XML_PULLSTRESS_ATTR = "pull_sress";
        public const string XML_MATERIAL_ATTR = "material";
        public const string UPSTRAND_ATTR_VALUE = "up";
        public const string DOWNSTRAND_ATTR_VALUE = "down";
        public const string STARTPOINT_ATTR_VALUE = "Start";
        public const string ENDPOINT_ATTR_VALUE = "End";

    }
}
