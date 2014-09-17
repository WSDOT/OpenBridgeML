Readme 

Export data from Bridgeware (Opis/Virtis 6.0) to xml linkage
==========================================================================================
Operating System: Windows XP
Prerequisite: Opis 6.0, Virtis 6.0, Opis-Virtis 6.0 (Base System Developer license), Visual Studio 2005 have been correctly installed. For the software itself installation instruction, please find the materials with software license or contact the technical support of the corresponding vendor.

Build Linkage
----------------------------
1. Make sure the Opis-Virtis 6.0 (Base System Developer license) has been correctly installed and start Opis/Virstis server;
2. Start Visual Studio and open solution FundedProjectSolution.sln;

Linkage Test Run
----------------------------
1. Start Visual Studio and open solution FundedProjectSolution.sln and then open the Program.cs file in BRIDGEWareExporting project;
2. Find the line: string brName = "Quincy Avenue bridge over I-25 & LRT" from the source code of Program.cs then modify the value of varable brName as the bridge name of yours. Note: to guarantee the success of this exporting, make sure the bridge model exists in the BRIDGEWare DB.
3. Find the line: string xmlFileName = @"D:\Exporting_Linkage_qc_steel_alt.db1.xml" from the source code of Program.cs then modify the value of varable xmlFileName to be the xml file name you want it to be;
4. Run the project of BRIDGEWareExporting in Visual Studio, and then you will find that the xml file you indicated has been created;
