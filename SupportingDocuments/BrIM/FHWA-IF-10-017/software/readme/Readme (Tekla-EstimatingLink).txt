Readme 

The linkage from Tekla to EstimatingLink
==========================================================================================
Operating System: Windows XP
Prerequisite: Tekla Structures 13.0, EstimatingLink, Visual Studio 2005 have been correctly installed. For the software itself installation instructions, please find the materials with software license or contact the technical support of the corresponding vendor.

Build Linkage
----------------------------
1. Make sure that Tekla Structures 13.0 has been correctly installed and start it;
2. Start Visual Studio and open solution FundedProjectSolution.sln;

Linkage Test Run
----------------------------
1. Start Visual Studio and open solution FundedProjectSolution.sln and then open the Form1.cs in ProjectApps project;
2. Go to "Tekla Structure Page" tab in the Form Design;
3. Run this project;
4. Start Tekla Structure and open the model you want to do the exporting from;
5. Go to "Tekla Structure Page" tab, and click "Export BOM for Concrete Alternate" button to export date from Tekla model to CSV file;
6. The CSV linkage file will be created in D: disk, and the file name is the same with the Tekla model file name plus "BOM". For example, if the Tekla model name is Bridge29_Clear_Demo_20090109, the xml file will be "BOM_Bridge29_Clear_Demo_20090109.db1.csv";
7. Start EstimatingLink, import the csv file into it. For the instruction of importing csv file, please find the manual of EstimatingLink;

Note: If you want to change the disk to which the CSV file save, open QCS.Utils.QExportingConstants.cs source code.
	 Find the line: public const string EX_FILE_DIR = @"D:\" And change it to the disk you want;