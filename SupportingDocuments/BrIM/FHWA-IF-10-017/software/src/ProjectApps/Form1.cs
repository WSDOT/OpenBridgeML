using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace ProjectApps
{
    public partial class AppMainForm : Form
    {
        public AppMainForm()
        {
            InitializeComponent();
        }

        private void btnTEPExport_Click(object sender, EventArgs e)
        {
            
            //string print_text = TeklaExporting.ProgramRunner.printReinforcementsOfPart("B-3");
            //rtxtTEPShowResult.Text = print_text;
            TeklaExporting.ProgramRunner.exportTeklaModelToXML();
        }

        private void btnBRPExport_Click(object sender, EventArgs e)
        {
            string bridgeID = "Quincy Ave. Bridge1";
            string print_text = BRIDGEWareExporting.ProgramRunner.ExportReinforcements(bridgeID);
            rtxtBRPShowResult.Text = print_text;
        }

        private void btnTEPExportConc_Click(object sender, EventArgs e)
        {
            TeklaExporting.ProgramRunner.exportTeklaConcreteModelToXML();
        }

        private void btnBatchUpdateName_Click(object sender, EventArgs e)
        {
            TeklaExporting.ProgramRunner.batchUpdatePartName();
        }

        private void btnImportBridge_Click(object sender, EventArgs e)
        {
            BRIDGEWareImporting.ProgramRunner.importSteelBridgeModel();
        }

        private void btnImportConcBridge_Click(object sender, EventArgs e)
        {
            string fileName = @"D:\Exporting_Linkage_Bridge29_Clear_Demo_20090109.db1_FromTekla.xml";
            BRIDGEWareImporting.ProgramRunner.importConcreteBridgeFromXml(fileName);
        }

        private void btnImportFromXML_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(@"D:\Exporting_Linkage_QC_Steel_Alt.db1_FromTekla.xml");
            XmlNode beamsNode = xmlDoc.SelectSingleNode("//beams");
            TeklaImporting.ProgramRunner.importGirders((XmlElement)beamsNode);
            XmlNode deckNode = xmlDoc.SelectSingleNode("//deck");
            TeklaImporting.ProgramRunner.importDeck((XmlElement)deckNode);
            XmlNode piersNode = xmlDoc.SelectSingleNode("//piers");

        }

        private void btnExportConBOM_Click(object sender, EventArgs e)
        {
            string filePath = @"D:\";
            TeklaExporting.ProgramRunner.exportBOMFileFromTeklaModel(filePath);
        }

        private void btnUpdateDeteriorationProfileOfStAlt_Click(object sender, EventArgs e)
        {
            string brID = "Quincy_Ave_Steel_Economy";
            string fileName = @"D:\web_Section_Loss.csv";
            BRIDGEWareImporting.ProgramRunner.updateDeteriorationProfileOfSteelAlt(brID, fileName);
        }

        private void btnUpdateStrandLossOfConAlt_Click(object sender, EventArgs e)
        {
            string brID = "Quincy_Ave_LRFD_concrete";
            string fileName = @"D:\Strand_Loss.csv";
            BRIDGEWareImporting.ProgramRunner.updateStrandLossOfConAlt(brID, fileName);
        }

        private void btnExport4DSchedule_Click(object sender, EventArgs e)
        {
            TeklaExporting.Program.export4DSchedule();
        }


        private void btnImportPlannedDateFromMSProject_Click(object sender, EventArgs e)
        {
            string csvFileName = @"D:\QC_Steel_Alt.db1_Schedule_INSTALL_PLAN.csv";
            TeklaExporting.Program.importPlannedDateFromMSProject(csvFileName);
        }
    }
}