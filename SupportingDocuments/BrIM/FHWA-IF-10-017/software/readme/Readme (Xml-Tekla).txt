Readme 

Import bridge model from xml file into Tekla
==========================================================================================
Operating System: Windows XP
Prerequisite: Tekla Structures 13.0, Visual Studio 2005 have been correctly installed. For the software itself installation instructions, please find the materials with software license or contact the technical support of the corresponding vendor.

Build Linkage
----------------------------
1. Make sure that Tekla Structures 13.0 has been correctly installed and start it;
2. Start Visual Studio and open solution FundedProjectSolution.sln;

Linkage Test Run
----------------------------
1. Start Visual Studio and open solution FundedProjectSolution.sln and then open the Form1.cs in ProjectApps project;
2. Go to "Tekla Structure Page" tab in the Form Design, double click the "Import Bridge From XML" button to view this button event handling source code. Find line: xmlDoc.Load(@"D:\Exporting_Linkage_QC_Steel_Alt.db1_FromTekla.xml") and set the xml file name;
3. Run this project;
4. Start Tekla Structure and create a new model;
5. Go to "Tekla Structure Page" tab, and click "Import Bridge From XML" button to import the bridge model into Tekla;
