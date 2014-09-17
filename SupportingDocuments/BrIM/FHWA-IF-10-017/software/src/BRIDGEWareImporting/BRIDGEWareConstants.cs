using System;
using System.Collections.Generic;
using System.Text;

namespace BRIDGEWareImporting
{
    public static class BRIDGEWareConstants
    {
        //XML file constants......
        public const short S_OK = 0;
        public const short ABW_SUCCESS = 0;
        public const string NEW_BR_TPL_FILENAME = @"D:\template_for_new_bridge_VirtiOpis_6_0.xml";
        public const string NEW_CONC_BR_TPL_FILENAME = @"template_for_new_conc_bridge_VirtiOpis_6_0.xml";
        public const string BR_NODE_NAME = "bridge";
        public const string BR_ID_ATTR_NAME = "bridge_id";
        public const string BR_NAME_ATTR_NAME = "bridge_name";

        // Type Category Keyword: STRDEF
        // Type Category Description: Structure Definition
        public const int TYP_STRDEF_SUBSTR = 25401;      // Substructure Definition
        public const int TYP_STRDEF_PIPEARCH = 25402;      // Pipe Arch Culvert Definition
        public const int TYP_STRDEF_BOXCUL = 25403;      // Box Culvert Definition
        public const int TYP_STRDEF_SLAB = 25404;      // Slab Superstructure
        public const int TYP_STRDEF_GIRDFLB = 25405;      // Girder Floorbeam Superstructure
        public const int TYP_STRDEF_GIRDER = 25406;      // Girder Superstructure
        public const int TYP_STRDEF_GIRDLINE = 25407;      // Girder Line Superstructure
        public const int TYP_STRDEF_TRUSS = 25408;      // Truss Superstructure
        public const int TYP_STRDEF_FRAMELINE = 25409;      // Frame-Line
        public const int TYP_STRDEF_FRAMESYS = 25410;      // Frame System
        public const int TYP_STRDEF_FLOORSYS = 25411;      // Floor System
        public const int TYP_STRDEF_FLOORLINE = 25412;      // Floor Line
        public const int TYP_STRDEF_FSYSGFS = 25413;      // Girder-Floor Beam-Stringer Floor System
        public const int TYP_STRDEF_FSYSGF = 25414;      // Girder-Floor Beam Floor System
        public const int TYP_STRDEF_FSYSFS = 25415;      // FloorBeam Stringer Floor System
        public const int TYP_STRDEF_FLINEGFS = 25416;      // Girder-Floor Beam-Stringer FloorLine
        public const int TYP_STRDEF_FLINEGF = 25417;      // Girder-Floor Beam Floor Line
        public const int TYP_STRDEF_FLINEFS = 25418;      // FloorBeam Stringer Floor Line
        public const int TYP_STRDEF_PARTIALSUP = 25419;      // Partial Super-structure Definition
        public const int TYP_STRDEF_PIERRCFRM = 25420;      // RC Pier Frame Sub-struct Def

        //Unit......
        public const int UC_ULEN = 2;
        public const int U_FT = 21;         // Feet
        public const int U_IN = 22;         // Inches
        public const int U_M = 23;         // Meter
        public const int U_MM = 24;         // Millimeters
        public const int U_MI = 25;         // miles
        public const int U_KM = 26;         // kilometer

        public const int UC_UNITLESS = 49;

        public const int U_UNITLESS = 491;        // Unitless
        public const int U_PERCENT = 492;        // Percent

        public const int UC_UFRC = 3;

        public const int U_KIPS = 31;         // Kips
        public const int U_KN = 32;         // KiloNewton
        public const int U_LBS = 33;        // Pounds
        public const int U_TONS = 34;        // Tons
        public const int U_N = 35;       // Newton
        public const int U_MTONS = 36;      // Metric Tons
        ////////////////////////////////////////////////////////////////////
        // Type Category Keyword: SYSUNITS
        // Type Category Description: System of Units

        public const int TYP_SYSUNITS_US = 10401;      // US Customary
        public const int TYP_SYSUNITS_SI = 10402;      // SI / Metric
        public const int TYP_SYSUNITS_NA = 10403;      // Not Applicable
        // Referencing Spans of a Girder
        public const int ABW_RIGHT_MOST_SPAN = -2;
        public const int ABW_LEFT_MOST_SPAN = -1;
////////////////////////////////////////////////////////////////////
// Type Category Keyword: ANALPTSOURCE
// Type Category Description: Analysis Point Source
        public const int TYP_ANALPTSOURCE_USERDEF = 45201;      // User-defined
        public const int TYP_ANALPTSOURCE_STDREINFDEV = 45202;      // Standard Reinf Development
        public const int TYP_ANALPTSOURCE_LRFDREINFDEV = 45203;      // LRFD Reinf Development
        public const int TYP_ANALPTSOURCE_LOCINTEREST = 45204;      // Location of interest
        public const int TYP_ANALPTSOURCE_XSECCHANGE = 45205;      // Cross section change point
////////////////////////////////////////////////////////////////////
// Type Category Keyword: MBRDEF
// Type Category Description: Member Definition
        public const int TYP_MBRDEF_STLTRUSS = 24101;      // Steel Truss Member
        public const int TYP_MBRDEF_CONCSLAB = 24102;      // Concrete Slab Member
        public const int TYP_MBRDEF_STLBEAM = 24103;      // Steel Beam
        public const int TYP_MBRDEF_CONCBEAM = 24104;      // Concrete Beam
        public const int TYP_MBRDEF_TIMBEAM = 24105;      // Timber Beam
        public const int TYP_MBRDEF_RLDBEAM = 24106;      // Rolled Steel Beam
        public const int TYP_MBRDEF_PLATEBEAM = 24107;      // Steel Plate I-Beam
        public const int TYP_MBRDEF_BUILTUPBEAM = 24108;      // Built-up Steel I-Beam
        public const int TYP_MBRDEF_STBOXBEAM = 24109;      // Closed Steel Box Beam
        public const int TYP_MBRDEF_PSPRECAST = 24110;      // Prestressed Precast Concrete Beam
        public const int TYP_MBRDEF_PSPRECASTIBEAM = 24111;      // Prestressed Precast Concrete I-Beam
        public const int TYP_MBRDEF_PSPRECASTBOXBEAM = 24112;      // Prestresseed Precast Concrete Box Beam
        public const int TYP_MBRDEF_PSPRECASTSLAB = 24113;      // Prestresseed Precast Slab
        public const int TYP_MBRDEF_RCBEAM = 24114;      // Reinforced Concrete Beam
        public const int TYP_MBRDEF_RCIBEAM = 24115;      // Reinforced Concrete I Beam
        public const int TYP_MBRDEF_RCSLABBEAM = 24116;      // Reinforced Concrete Slab Beam
        public const int TYP_MBRDEF_RCTEEBEAM = 24117;      // Reinforced Concrete Tee Beam
        public const int TYP_MBRDEF_TMBRRECTSAWN = 24118;      // Rectangular Sawn Timber  Beam
        public const int TYP_MBRDEF_NODETAIL = 24119;      // Steel Non-Detailed Beam
        public const int TYP_MBRDEF_PSPRECASTUBEAM = 24120;      // Prestressed Precast Concrete U-Beam
        public const int TYP_MBRDEF_PSPRECASTTEEBEAM = 24121;      // Prestressed Precast Concrete Tee-Beam
        public const int TYP_MBRDEF_TIMBERTRUSS = 24122;      // Timber Truss Member
    }
}
