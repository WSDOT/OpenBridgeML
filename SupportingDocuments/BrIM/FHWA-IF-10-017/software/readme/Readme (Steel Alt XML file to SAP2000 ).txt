Readme 

The linkage from XML file to SAP2000 v11.0.8
========================================================================================== 
Operating System: Windows XP 
Prerequisite: SAP2000 v11.0.8, visual studio 2005 and Notepad have been correctly installed. 
For the software installation instructions of each, please find the HELP files embedded in the software or contact technical support of corresponding vendor.

File needed for this linkage
------------------------------------------------------------------------------------------
1. The XML file "Exporting_Linkage_qc_steel_alt.db1.xml"
2. C# code "QImporting.Beans" and "S2KFile_Test_Quincy_final.cs"

Linkage Test Run
------------------------------------------------------------------------------------------
1. Open visual studio 2005 software, load c# code "QImporting.Beans" and "S2KFile_Test_Quincy_final.cs";
2. Put XML file "Exporting_Linkage_qc_steel_alt.db1.xml" in the folder F:\C# NEW, and keep it open;
3. Run this C# program, a text file named "S2KFile_Test_Quincy_final.s2k" will be generated in the same folder;
4. Open SAP2000 v11.0.8 software, open "file" menu, select "import", and choose "SAP2000 v8 to v11 .s2k Text File";
5. Browse the text input file "S2KFile_Test_Quincy_final.s2k" and load it. Then the bridge model has been imported in SAP2000 software;
6. Choose "BrIM" menu in SAP2000, and select "Update Linked Bridge Model", choose "update as Area Object Model". Now you can see the FEM bridge model shown in SAP2000. 