namespace ProjectApps
{
    partial class AppMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MainPanal = new System.Windows.Forms.Panel();
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.BRIDGEWareTabPage = new System.Windows.Forms.TabPage();
            this.btnUpdateStrandLossOfConAlt = new System.Windows.Forms.Button();
            this.btnUpdateDeteriorationProfileOfStAlt = new System.Windows.Forms.Button();
            this.btnImportConcBridge = new System.Windows.Forms.Button();
            this.btnImportBridge = new System.Windows.Forms.Button();
            this.rtxtBRPShowResult = new System.Windows.Forms.RichTextBox();
            this.btnBRPExport = new System.Windows.Forms.Button();
            this.TeklaTabPage = new System.Windows.Forms.TabPage();
            this.btnImportPlannedDateFromMSProject = new System.Windows.Forms.Button();
            this.btnExport4DSchedule = new System.Windows.Forms.Button();
            this.btnExportConBOM = new System.Windows.Forms.Button();
            this.btnImportFromXML = new System.Windows.Forms.Button();
            this.btnBatchUpdateName = new System.Windows.Forms.Button();
            this.btnTEPExportConc = new System.Windows.Forms.Button();
            this.rtxtTEPShowResult = new System.Windows.Forms.RichTextBox();
            this.btnTEPExport = new System.Windows.Forms.Button();
            this.MainPanal.SuspendLayout();
            this.MainTabControl.SuspendLayout();
            this.BRIDGEWareTabPage.SuspendLayout();
            this.TeklaTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainPanal
            // 
            this.MainPanal.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.MainPanal.Controls.Add(this.MainTabControl);
            this.MainPanal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanal.Location = new System.Drawing.Point(0, 0);
            this.MainPanal.Name = "MainPanal";
            this.MainPanal.Size = new System.Drawing.Size(774, 475);
            this.MainPanal.TabIndex = 0;
            // 
            // MainTabControl
            // 
            this.MainTabControl.Controls.Add(this.BRIDGEWareTabPage);
            this.MainTabControl.Controls.Add(this.TeklaTabPage);
            this.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainTabControl.Location = new System.Drawing.Point(0, 0);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(770, 471);
            this.MainTabControl.TabIndex = 0;
            // 
            // BRIDGEWareTabPage
            // 
            this.BRIDGEWareTabPage.Controls.Add(this.btnUpdateStrandLossOfConAlt);
            this.BRIDGEWareTabPage.Controls.Add(this.btnUpdateDeteriorationProfileOfStAlt);
            this.BRIDGEWareTabPage.Controls.Add(this.btnImportConcBridge);
            this.BRIDGEWareTabPage.Controls.Add(this.btnImportBridge);
            this.BRIDGEWareTabPage.Controls.Add(this.rtxtBRPShowResult);
            this.BRIDGEWareTabPage.Controls.Add(this.btnBRPExport);
            this.BRIDGEWareTabPage.Location = new System.Drawing.Point(4, 22);
            this.BRIDGEWareTabPage.Name = "BRIDGEWareTabPage";
            this.BRIDGEWareTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.BRIDGEWareTabPage.Size = new System.Drawing.Size(762, 445);
            this.BRIDGEWareTabPage.TabIndex = 0;
            this.BRIDGEWareTabPage.Text = "BRIDGEWare Page";
            this.BRIDGEWareTabPage.UseVisualStyleBackColor = true;
            // 
            // btnUpdateStrandLossOfConAlt
            // 
            this.btnUpdateStrandLossOfConAlt.Location = new System.Drawing.Point(417, 227);
            this.btnUpdateStrandLossOfConAlt.Name = "btnUpdateStrandLossOfConAlt";
            this.btnUpdateStrandLossOfConAlt.Size = new System.Drawing.Size(240, 23);
            this.btnUpdateStrandLossOfConAlt.TabIndex = 5;
            this.btnUpdateStrandLossOfConAlt.Text = "Update Strand Loss of Concrete Alt";
            this.btnUpdateStrandLossOfConAlt.UseVisualStyleBackColor = true;
            this.btnUpdateStrandLossOfConAlt.Click += new System.EventHandler(this.btnUpdateStrandLossOfConAlt_Click);
            // 
            // btnUpdateDeteriorationProfileOfStAlt
            // 
            this.btnUpdateDeteriorationProfileOfStAlt.Location = new System.Drawing.Point(417, 169);
            this.btnUpdateDeteriorationProfileOfStAlt.Name = "btnUpdateDeteriorationProfileOfStAlt";
            this.btnUpdateDeteriorationProfileOfStAlt.Size = new System.Drawing.Size(240, 23);
            this.btnUpdateDeteriorationProfileOfStAlt.TabIndex = 4;
            this.btnUpdateDeteriorationProfileOfStAlt.Text = "Update Deterioration Profile Of Steel Alt";
            this.btnUpdateDeteriorationProfileOfStAlt.UseVisualStyleBackColor = true;
            this.btnUpdateDeteriorationProfileOfStAlt.Click += new System.EventHandler(this.btnUpdateDeteriorationProfileOfStAlt_Click);
            // 
            // btnImportConcBridge
            // 
            this.btnImportConcBridge.Location = new System.Drawing.Point(417, 103);
            this.btnImportConcBridge.Name = "btnImportConcBridge";
            this.btnImportConcBridge.Size = new System.Drawing.Size(240, 23);
            this.btnImportConcBridge.TabIndex = 3;
            this.btnImportConcBridge.Text = "Import Concrete Bridge Model From XML";
            this.btnImportConcBridge.UseVisualStyleBackColor = true;
            this.btnImportConcBridge.Click += new System.EventHandler(this.btnImportConcBridge_Click);
            // 
            // btnImportBridge
            // 
            this.btnImportBridge.Location = new System.Drawing.Point(417, 45);
            this.btnImportBridge.Name = "btnImportBridge";
            this.btnImportBridge.Size = new System.Drawing.Size(240, 23);
            this.btnImportBridge.TabIndex = 2;
            this.btnImportBridge.Text = "Import Steel Bridge From XML";
            this.btnImportBridge.UseVisualStyleBackColor = true;
            this.btnImportBridge.Click += new System.EventHandler(this.btnImportBridge_Click);
            // 
            // rtxtBRPShowResult
            // 
            this.rtxtBRPShowResult.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtxtBRPShowResult.Location = new System.Drawing.Point(3, 287);
            this.rtxtBRPShowResult.Name = "rtxtBRPShowResult";
            this.rtxtBRPShowResult.Size = new System.Drawing.Size(756, 155);
            this.rtxtBRPShowResult.TabIndex = 1;
            this.rtxtBRPShowResult.Text = "";
            // 
            // btnBRPExport
            // 
            this.btnBRPExport.Location = new System.Drawing.Point(37, 45);
            this.btnBRPExport.Name = "btnBRPExport";
            this.btnBRPExport.Size = new System.Drawing.Size(240, 23);
            this.btnBRPExport.TabIndex = 0;
            this.btnBRPExport.Text = "Export Concrete Bridge";
            this.btnBRPExport.UseVisualStyleBackColor = true;
            this.btnBRPExport.Click += new System.EventHandler(this.btnBRPExport_Click);
            // 
            // TeklaTabPage
            // 
            this.TeklaTabPage.Controls.Add(this.btnImportPlannedDateFromMSProject);
            this.TeklaTabPage.Controls.Add(this.btnExport4DSchedule);
            this.TeklaTabPage.Controls.Add(this.btnExportConBOM);
            this.TeklaTabPage.Controls.Add(this.btnImportFromXML);
            this.TeklaTabPage.Controls.Add(this.btnBatchUpdateName);
            this.TeklaTabPage.Controls.Add(this.btnTEPExportConc);
            this.TeklaTabPage.Controls.Add(this.rtxtTEPShowResult);
            this.TeklaTabPage.Controls.Add(this.btnTEPExport);
            this.TeklaTabPage.Location = new System.Drawing.Point(4, 22);
            this.TeklaTabPage.Name = "TeklaTabPage";
            this.TeklaTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.TeklaTabPage.Size = new System.Drawing.Size(762, 445);
            this.TeklaTabPage.TabIndex = 1;
            this.TeklaTabPage.Text = "Tekla Structure Page";
            this.TeklaTabPage.UseVisualStyleBackColor = true;
            // 
            // btnImportPlannedDateFromMSProject
            // 
            this.btnImportPlannedDateFromMSProject.Location = new System.Drawing.Point(423, 205);
            this.btnImportPlannedDateFromMSProject.Name = "btnImportPlannedDateFromMSProject";
            this.btnImportPlannedDateFromMSProject.Size = new System.Drawing.Size(240, 23);
            this.btnImportPlannedDateFromMSProject.TabIndex = 7;
            this.btnImportPlannedDateFromMSProject.Text = "Update Planned Date From MS Project";
            this.btnImportPlannedDateFromMSProject.UseVisualStyleBackColor = true;
            this.btnImportPlannedDateFromMSProject.Click += new System.EventHandler(this.btnImportPlannedDateFromMSProject_Click);
            // 
            // btnExport4DSchedule
            // 
            this.btnExport4DSchedule.Location = new System.Drawing.Point(423, 161);
            this.btnExport4DSchedule.Name = "btnExport4DSchedule";
            this.btnExport4DSchedule.Size = new System.Drawing.Size(239, 22);
            this.btnExport4DSchedule.TabIndex = 6;
            this.btnExport4DSchedule.Text = "Export Tekla 4D Schedule";
            this.btnExport4DSchedule.UseVisualStyleBackColor = true;
            this.btnExport4DSchedule.Click += new System.EventHandler(this.btnExport4DSchedule_Click);
            // 
            // btnExportConBOM
            // 
            this.btnExportConBOM.Location = new System.Drawing.Point(39, 161);
            this.btnExportConBOM.Name = "btnExportConBOM";
            this.btnExportConBOM.Size = new System.Drawing.Size(240, 23);
            this.btnExportConBOM.TabIndex = 5;
            this.btnExportConBOM.Text = "Export BOM for Concrete Alternate";
            this.btnExportConBOM.UseVisualStyleBackColor = true;
            this.btnExportConBOM.Click += new System.EventHandler(this.btnExportConBOM_Click);
            // 
            // btnImportFromXML
            // 
            this.btnImportFromXML.Location = new System.Drawing.Point(423, 112);
            this.btnImportFromXML.Name = "btnImportFromXML";
            this.btnImportFromXML.Size = new System.Drawing.Size(240, 23);
            this.btnImportFromXML.TabIndex = 4;
            this.btnImportFromXML.Text = "Import Bridge From XML";
            this.btnImportFromXML.UseVisualStyleBackColor = true;
            this.btnImportFromXML.Click += new System.EventHandler(this.btnImportFromXML_Click);
            // 
            // btnBatchUpdateName
            // 
            this.btnBatchUpdateName.Location = new System.Drawing.Point(423, 62);
            this.btnBatchUpdateName.Name = "btnBatchUpdateName";
            this.btnBatchUpdateName.Size = new System.Drawing.Size(240, 23);
            this.btnBatchUpdateName.TabIndex = 3;
            this.btnBatchUpdateName.Text = "Batch Update Model Name";
            this.btnBatchUpdateName.UseVisualStyleBackColor = true;
            this.btnBatchUpdateName.Click += new System.EventHandler(this.btnBatchUpdateName_Click);
            // 
            // btnTEPExportConc
            // 
            this.btnTEPExportConc.Location = new System.Drawing.Point(39, 113);
            this.btnTEPExportConc.Name = "btnTEPExportConc";
            this.btnTEPExportConc.Size = new System.Drawing.Size(240, 23);
            this.btnTEPExportConc.TabIndex = 2;
            this.btnTEPExportConc.Text = "Export Tekla Concrete Model To XML";
            this.btnTEPExportConc.UseVisualStyleBackColor = true;
            this.btnTEPExportConc.Click += new System.EventHandler(this.btnTEPExportConc_Click);
            // 
            // rtxtTEPShowResult
            // 
            this.rtxtTEPShowResult.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtxtTEPShowResult.Location = new System.Drawing.Point(3, 268);
            this.rtxtTEPShowResult.Name = "rtxtTEPShowResult";
            this.rtxtTEPShowResult.Size = new System.Drawing.Size(756, 174);
            this.rtxtTEPShowResult.TabIndex = 1;
            this.rtxtTEPShowResult.Text = "";
            // 
            // btnTEPExport
            // 
            this.btnTEPExport.Location = new System.Drawing.Point(39, 62);
            this.btnTEPExport.Name = "btnTEPExport";
            this.btnTEPExport.Size = new System.Drawing.Size(240, 23);
            this.btnTEPExport.TabIndex = 0;
            this.btnTEPExport.Text = "Export Tekla Steel Model To XML";
            this.btnTEPExport.UseVisualStyleBackColor = true;
            this.btnTEPExport.Click += new System.EventHandler(this.btnTEPExport_Click);
            // 
            // AppMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 475);
            this.Controls.Add(this.MainPanal);
            this.Name = "AppMainForm";
            this.Text = "Main Applications Window";
            this.MainPanal.ResumeLayout(false);
            this.MainTabControl.ResumeLayout(false);
            this.BRIDGEWareTabPage.ResumeLayout(false);
            this.TeklaTabPage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel MainPanal;
        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.TabPage BRIDGEWareTabPage;
        private System.Windows.Forms.TabPage TeklaTabPage;
        private System.Windows.Forms.Button btnBRPExport;
        private System.Windows.Forms.Button btnTEPExport;
        private System.Windows.Forms.RichTextBox rtxtTEPShowResult;
        private System.Windows.Forms.RichTextBox rtxtBRPShowResult;
        private System.Windows.Forms.Button btnTEPExportConc;
        private System.Windows.Forms.Button btnBatchUpdateName;
        private System.Windows.Forms.Button btnImportBridge;
        private System.Windows.Forms.Button btnImportConcBridge;
        private System.Windows.Forms.Button btnImportFromXML;
        private System.Windows.Forms.Button btnExportConBOM;
        private System.Windows.Forms.Button btnUpdateDeteriorationProfileOfStAlt;
        private System.Windows.Forms.Button btnUpdateStrandLossOfConAlt;
        private System.Windows.Forms.Button btnExport4DSchedule;
        private System.Windows.Forms.Button btnImportPlannedDateFromMSProject;
    }
}

