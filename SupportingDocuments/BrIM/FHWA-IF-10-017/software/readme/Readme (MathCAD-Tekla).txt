Readme 

The linkage from MathCAD 14 Calculation Sheet (developped by Troy) to Tekla Structure 13.0
==========================================================================================
Operating System: Windows XP
Prerequisite: MathCAD 14, Tekla Structure, Visual Studio 2005 have been correctly installed. For the software itself installation instruction of each, please find the materials with software license or contact technical support of corresponding vendor.

Build Linkage
----------------------------
1. Copy the MathCAD files 000-Units.xmcd, 001-Highway.xmcd,002-Layout.xmcd in a particular folder;
2. Copy the DTM and layout data files DTM_OR_FG.txt, DTM_OR_SG.txt, DTM_UR_FG.txt, DTM_UR_SG.txt, Overroad_HA.txt, Overroad_VP.txt, Underroad_HA.txt, Underroad_VP.txt in the same folder;
3. Open 002-Layout.xmcd in MathCAD 14, and make sure the bridge preliminary design, such as the girder number, bridge width, spacing of girders and so on, has been correctly inputted;
4. Open Tekla Structure 13.0, and create a new blank project;
5. Start Visual Studio and open solution FundedProjectSolution.sln;

Linkage Test Run
----------------------------
1. Make sure Tekla Structure 13.0 has been started and a project has been opened or created;
2. Open 002-Layout.xmcd in MathCAD 14, and go to the end of the file; after running for a while, MathCAD will export data to D:beam_model.xml which is the linkage file;
3. Start Visual Studio and open project UB.Tekla.App and then run it;
4. Choose the xml linkage file just created from MathCAD, then click create beams botton;
5. In the Tekla Structures model editor, you will see the girders and deck have been created automatically.
