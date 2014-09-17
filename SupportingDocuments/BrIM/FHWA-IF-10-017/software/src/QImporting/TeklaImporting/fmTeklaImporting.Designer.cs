namespace TeklaImporting
{
    partial class fmTeklaImporting
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
            this.dlgChooseXml = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // dlgChooseXml
            // 
            this.dlgChooseXml.FileOk += new System.ComponentModel.CancelEventHandler(this.dlgChooseXml_FileOk);
            // 
            // fmTeklaImporting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(206, 80);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "fmTeklaImporting";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Importing for Tekla Structure";
            this.Load += new System.EventHandler(this.fmTeklaImporting_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog dlgChooseXml;
    }
}

