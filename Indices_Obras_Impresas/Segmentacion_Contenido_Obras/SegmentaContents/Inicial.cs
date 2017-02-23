using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SegmentaContents
{
    public partial class Inicial : Form
    {
        public Inicial()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //BrowseDialogHTML.ShowDialog();
        }

        private void btnRutaHTML_Click(object sender, EventArgs e)
        {
            BrowseDialogHTML.ShowDialog();
            lblRuta.Text = BrowseDialogHTML.SelectedPath;
            urlHTMLSegmentados = BrowseDialogHTML.SelectedPath ;
        }

        private void btnContinueStep1_Click(object sender, EventArgs e)
        {
            editionId = Convert.ToInt32(txtEditionId.Text);
            tabla = txtTabla.Text;


           obj.Step1InsertHTMLfromFile(txtTabla.Text, urlHTMLSegmentados, editionId);
            obj.Step2ExportHTMLfromDB(editionId, urlHTMLProcesados);
            obj.Step3GenerateXMLfromHTMLDB(urlHTMLProcesados, urlXMLprocesados);
            if (obj.Step4VerifyXML(urlXMLprocesados))
            {
                obj.Step5InsertXMLinDB(urlXMLprocesados, editionId);

               grdAttributes.DataSource = obj.Step6VerificaRubros(editionId).Tables[0];
                
           btnContinuar.Visible = true;
            lblLEy.Visible = true;

        ////Tendria que insertar los contenidos aqui



            }
            else
            {

                MessageBox.Show("Error en los rubros");
            }

        }
        private string urlHTMLSegmentados;
        private string urlHTMLProcesados;
        private string urlXMLprocesados;
        private int editionId;
        private string tabla;
        SegmentHtml obj = new SegmentHtml();
        private void btnEstablecerHTMLFromDB_Click(object sender, EventArgs e)
        {
            BrowseDialogHTML.ShowDialog();
            lblDestino.Text = BrowseDialogHTML.SelectedPath ;
            urlHTMLProcesados = BrowseDialogHTML.SelectedPath ;
        }

        private void btnEstableceXML_Click(object sender, EventArgs e)
        {
            BrowseDialogHTML.ShowDialog();
            lblDestinoXML.Text = BrowseDialogHTML.SelectedPath;
            urlXMLprocesados = BrowseDialogHTML.SelectedPath ;
        }

        private void btnContinuar_Click(object sender, EventArgs e)
        {
             editionId = Convert.ToInt32(txtEditionId.Text);
             obj.Step7LlenaContenidos(editionId);
            //Iria una validacion de duplicados
            
            obj.Step8LlenaHTMLContenidos(urlHTMLProcesados);
        }
    }
}
