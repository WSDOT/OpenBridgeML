Readme 

Import bridge model from xml file into BRIDGEWare (Opis/Virtis)
==========================================================================================
Operating System: Windows XP
Prerequisite: Opis 6.0, Virtis 6.0, Opis-Virtis 6.0 (Base System Developer license), Visual Studio 2005 have been correctly installed. For the software itself installation instructions, please find the materials with software license or contact the technical support of the corresponding vendor.

Build Linkage
----------------------------
1. Make sure that Opis-Virtis 6.0 (Base System Developer license) has been correctly installed and start it;
2. Start Visual Studio and open solution FundedProjectSolution.sln;

Linkage Test Run
----------------------------
1. Start Visual Studio and open solution FundedProjectSolution.sln and then open the Form1.cs in ProjectApps project;
2. Go to "BRIDGEWare Page" tab in the Form Design:
	For Steel Alternate: double click the "Import Steel Bridge From XML" button or "Import Concrete Bridge From XML" button to view this button
 event handling source code. Go to BRIDGEWareImporting.ProgramRunner.importSteelBridgeModel() function source code and then find line: string sXmlFile = @"D:\Exporting_Linkage_Bridge29_Clear_Demo.db1_FromTekla.xml" and set the xml file name;
 	For Concrete Alternate: double click the "Import Concrete Bridge From XML" button to view the button click
 event handling source code. Find line: string fileName = @"D:\Exporting_Linkage_Bridge29_Clear_Demo_20090109.db1_FromTekla.xml" and set the xml file name;
3. Run this project;
4. Start Opis or Virtis;
5. Go to "Tekla Structure Page" tab, and click "Import Steel Bridge From XML" button or "Import Concrete Bridge From XML" button to import the bridge model into BRIDGEWare;
