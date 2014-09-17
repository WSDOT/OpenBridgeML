Readme 

Import Inspection Data back into BRIDGEWare (Opis/Virtis) to do Re-rating
==========================================================================================
Operating System: Windows XP
Prerequisite: Opis 6.0, Virtis 6.0, Opis-Virtis 6.0 (Base System Developer license), Visual Studio 2005 have been correctly installed. For the software itself installation instruction, please find the materials with software license or contact the technical support of the corresponding vendor.

Build Linkage
----------------------------
1. Make sure the Opis-Virtis 6.0 (Base System Developer license) has been correctly installed and start it;
2. Start Visual Studio and open solution FundedProjectSolution.sln;
3. Copy the right template csv (tf_Section_Loss.csv, web_Section_Loss.csv for steel alternate, or Strand_Loss.csv for concrete alternate) file as input file;

Linkage Test Run
----------------------------
1. Start Visual Studio and open solution FundedProjectSolution.sln and then open the Form1.cs in ProjectApps project;
2. Go to "BRIDGEWare Page" tab in the Form Design:
	For Steel Alternate: double click the "Update Deterioration Profile Of Steel Alt" button to view button click event handling source code. Find line: string fileName = @"D:\web_Section_Loss.csv" and set the csv file name. Find line: string brID = "Quincy_Ave_Steel_Economy" and set the model name in BRIDGEWare;
 	For Concrete Alternate: double click the "Update Strand Loss of Concrete Alt" button to view the button click event handling source code. Find line: string fileName = @"D:\Strand_Loss.csv" and set the csv file name. Find line: string brID = "Quincy_Ave_LRFD_concrete" and set the model name in BRIDGEWare;
3. Run this project;
4. Start Opis or Virtis;
5. Go to "BRIDGEWare Page" tab, and click "Update Deterioration Profile Of Steel Alt" button or "Update Strand Loss of Concrete Alt" button to import the bridge model into BRIDGEWare;
6. Do re-rating in BRIDGEWare (Opis/Virtis);
