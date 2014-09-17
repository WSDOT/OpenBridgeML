Readme 

The linkage from MS Project Professional 2007 to Tekla 4D
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
1. Run MS Project and modify the schedule based on your requirements;
2. Export the schedule into csv file. Please find the instruction about how to export schedule data from MS Project from the software manual or tutorial with license;
3. Start Visual Studio and open solution FundedProjectSolution.sln and then open the Form1.cs in ProjectApps project;
4. Go to "Tekla Structure Page" and double click "Update Planned Date From MS Project" button. The click button event handling source code will show up. Find line: string csvFileName = @"D:\QC_Steel_Alt.db1_Schedule_INSTALL_PLAN.csv" to set the name and directory of the csv linkage file just exported from MS Project;
3. Run this project;
4. Start Tekla Structure and open the model you want to do the importing;
5. Go to "Tekla Structure Page" tab, and click "Update Planned Date From MS Project" button to update the planned date information for each component from CSV file;

