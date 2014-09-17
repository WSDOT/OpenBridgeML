using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.Text;
using System.Windows.Forms;
using QCS.Utils;
using Tekla.Structures.Model;
using System.Xml;
using QImporting.Utils;
using QImporting.Beans;
using System.Collections;
using TeklaBeans;
using Tekla.Structures;

namespace TeklaImporting
{
    public partial class fmTeklaImporting : Form
    {
        public fmTeklaImporting()
        {
            InitializeComponent();
        }

        private void fmTeklaImporting_Load(object sender, EventArgs e)
        {
            dlgChooseXml.ShowDialog();
        }

        private void dlgChooseXml_FileOk(object sender, CancelEventArgs e)
        {
            string fileName = dlgChooseXml.FileName;
            if (!Utils.IsValiateXmlFileName(fileName))
            {
                return;
            }
            Model model = new Model();

            if (model.GetConnectionStatus())
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fileName);
                ArrayList girderInfos = ImportingConvert.convertToGirderInfos(fileName);
                Point stPoint = null;
                Point enPoint = null;
                foreach (IGirderInfo girderInfo in girderInfos)
                {
                    TeklaISteelGirder girder = ImportingConvert.convertToTeklaGirder(girderInfo);
                    stPoint = girder.getMainBeam().StartPoint;
                    enPoint = girder.getMainBeam().EndPoint;
                    girder.getMainBeam().Profile.ProfileString = "1100X14";
                    girder.getMainBeam().Name = "main beam";
                    girder.Insert();
                    
                    //MessageBox.Show("center: X--" + coSys.Origin.X + ": Y--" + coSys.Origin.Y + ": Z" + coSys.Origin.Z);
                    //MessageBox.Show("Axis X: " + coSys.AxisX + "Axis Y: " + coSys.AxisY);
                }

                if (model.CommitChanges())
                {
                    MessageBox.Show("Importing succeeds!");
                }
                else
                {                    
                    MessageBox.Show("Got some errors from importing......");
                }
                this.Close();
            }
        }
    }
}