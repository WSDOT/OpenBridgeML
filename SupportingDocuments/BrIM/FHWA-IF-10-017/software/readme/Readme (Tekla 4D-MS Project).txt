Readme 

The linkage from Tekla 4D to MS Project Professional 2007
==========================================================================================
Operating System: Windows XP
Prerequisite: Tekla Structure 13.0, MS Project Professional 2007, Visual Studio 2005 have been correctly installed. For the software itself installation instruction, please find the materials with software license or contact the technical support of the corresponding vendor.

Build Linkage
----------------------------
1. Make sure that Tekla Structures 13.0 has been correctly installed and start it;
2. Make sure that MS Project has been correctly installed;
3. Start Visual Studio and open solution FundedProjectSolution.sln;

Linkage Test Run
----------------------------
1. Start Visual Studio and open solution FundedProjectSolution.sln and then open the Form1.cs in ProjectApps project;
2. Go to TeklaExporting.Program.export4DSchedule() function source code. Find line: string fileName = @"D:\" + modelName + "_Schedule_" + propertyName + ".csv" to set the name and directory of the csv linkage file;
3. Run this project;
4. Start Tekla Structure and open the model you want to do the exporting;
5. Go to "Tekla Structure Page" tab, and click "Export Tekla 4D Schedule" button to export date from Tekla 4D model to CSV file;
6. The CSV linkage file will be created;
7. Start MS Project, import the csv file into it. For the instruction of importing csv format of schedule file, please find the manual of MS Project;
