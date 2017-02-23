namespace SegmentaContents
{
    partial class Inicial
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.BrowseDialogHTML = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.btnRutaHTML = new System.Windows.Forms.Button();
            this.lblRuta = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtEditionId = new System.Windows.Forms.TextBox();
            this.txtTabla = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnContinueStep1 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.btnEstablecerHTMLFromDB = new System.Windows.Forms.Button();
            this.lblDestino = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnEstableceXML = new System.Windows.Forms.Button();
            this.lblDestinoXML = new System.Windows.Forms.Label();
            this.grdAttributes = new System.Windows.Forms.DataGridView();
            this.lblLEy = new System.Windows.Forms.Label();
            this.btnContinuar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdAttributes)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(513, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Paso 1: Establecer ruta de ubicacion de archivos HTML segmentados";
            // 
            // btnRutaHTML
            // 
            this.btnRutaHTML.Location = new System.Drawing.Point(43, 78);
            this.btnRutaHTML.Name = "btnRutaHTML";
            this.btnRutaHTML.Size = new System.Drawing.Size(91, 23);
            this.btnRutaHTML.TabIndex = 1;
            this.btnRutaHTML.Text = "Establecer";
            this.btnRutaHTML.UseVisualStyleBackColor = true;
            this.btnRutaHTML.Click += new System.EventHandler(this.btnRutaHTML_Click);
            // 
            // lblRuta
            // 
            this.lblRuta.AutoSize = true;
            this.lblRuta.Location = new System.Drawing.Point(156, 83);
            this.lblRuta.Name = "lblRuta";
            this.lblRuta.Size = new System.Drawing.Size(104, 17);
            this.lblRuta.TabIndex = 2;
            this.lblRuta.Text = "Sin ubicacion...";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(43, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "EditionId";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(156, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Nombre de tabla en BD";
            // 
            // txtEditionId
            // 
            this.txtEditionId.Location = new System.Drawing.Point(205, 147);
            this.txtEditionId.Name = "txtEditionId";
            this.txtEditionId.Size = new System.Drawing.Size(206, 22);
            this.txtEditionId.TabIndex = 4;
            // 
            // txtTabla
            // 
            this.txtTabla.Location = new System.Drawing.Point(205, 170);
            this.txtTabla.Name = "txtTabla";
            this.txtTabla.Size = new System.Drawing.Size(206, 22);
            this.txtTabla.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(43, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(496, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Paso 2: Establecer Edicion y Nombre de Tabla temporal de Empate";
            // 
            // btnContinueStep1
            // 
            this.btnContinueStep1.Location = new System.Drawing.Point(43, 383);
            this.btnContinueStep1.Name = "btnContinueStep1";
            this.btnContinueStep1.Size = new System.Drawing.Size(88, 23);
            this.btnContinueStep1.TabIndex = 5;
            this.btnContinueStep1.Text = "Iniciar";
            this.btnContinueStep1.UseVisualStyleBackColor = true;
            this.btnContinueStep1.Click += new System.EventHandler(this.btnContinueStep1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(40, 211);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(510, 17);
            this.label5.TabIndex = 0;
            this.label5.Text = "Paso 3: Establecer Ubicacion de destino de HTML procesados de BD";
            // 
            // btnEstablecerHTMLFromDB
            // 
            this.btnEstablecerHTMLFromDB.Location = new System.Drawing.Point(43, 240);
            this.btnEstablecerHTMLFromDB.Name = "btnEstablecerHTMLFromDB";
            this.btnEstablecerHTMLFromDB.Size = new System.Drawing.Size(91, 23);
            this.btnEstablecerHTMLFromDB.TabIndex = 6;
            this.btnEstablecerHTMLFromDB.Text = "Establecer";
            this.btnEstablecerHTMLFromDB.UseVisualStyleBackColor = true;
            this.btnEstablecerHTMLFromDB.Click += new System.EventHandler(this.btnEstablecerHTMLFromDB_Click);
            // 
            // lblDestino
            // 
            this.lblDestino.AutoSize = true;
            this.lblDestino.Location = new System.Drawing.Point(141, 245);
            this.lblDestino.Name = "lblDestino";
            this.lblDestino.Size = new System.Drawing.Size(92, 17);
            this.lblDestino.TabIndex = 7;
            this.lblDestino.Text = "Sin Destino...";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(40, 289);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(437, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "Paso 4: Establecer Ubicacion de destino de XML a generar";
            // 
            // btnEstableceXML
            // 
            this.btnEstableceXML.Location = new System.Drawing.Point(43, 327);
            this.btnEstableceXML.Name = "btnEstableceXML";
            this.btnEstableceXML.Size = new System.Drawing.Size(88, 23);
            this.btnEstableceXML.TabIndex = 8;
            this.btnEstableceXML.Text = "Establecer";
            this.btnEstableceXML.UseVisualStyleBackColor = true;
            this.btnEstableceXML.Click += new System.EventHandler(this.btnEstableceXML_Click);
            // 
            // lblDestinoXML
            // 
            this.lblDestinoXML.AutoSize = true;
            this.lblDestinoXML.Location = new System.Drawing.Point(138, 332);
            this.lblDestinoXML.Name = "lblDestinoXML";
            this.lblDestinoXML.Size = new System.Drawing.Size(92, 17);
            this.lblDestinoXML.TabIndex = 9;
            this.lblDestinoXML.Text = "Sin Destino...";
            // 
            // grdAttributes
            // 
            this.grdAttributes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdAttributes.Location = new System.Drawing.Point(43, 437);
            this.grdAttributes.Name = "grdAttributes";
            this.grdAttributes.RowTemplate.Height = 24;
            this.grdAttributes.Size = new System.Drawing.Size(360, 150);
            this.grdAttributes.TabIndex = 10;
            // 
            // lblLEy
            // 
            this.lblLEy.AutoSize = true;
            this.lblLEy.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLEy.Location = new System.Drawing.Point(43, 417);
            this.lblLEy.Name = "lblLEy";
            this.lblLEy.Size = new System.Drawing.Size(853, 17);
            this.lblLEy.TabIndex = 0;
            this.lblLEy.Text = "Para continuar es necesario insertar los siguientes atributos, Inserte los atribu" +
                "tos y a continacion Click en continuar";
            this.lblLEy.Visible = false;
            // 
            // btnContinuar
            // 
            this.btnContinuar.Location = new System.Drawing.Point(464, 437);
            this.btnContinuar.Name = "btnContinuar";
            this.btnContinuar.Size = new System.Drawing.Size(105, 23);
            this.btnContinuar.TabIndex = 11;
            this.btnContinuar.Text = "Continuar";
            this.btnContinuar.UseVisualStyleBackColor = true;
            this.btnContinuar.Visible = false;
            this.btnContinuar.Click += new System.EventHandler(this.btnContinuar_Click);
            // 
            // Inicial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(905, 620);
            this.Controls.Add(this.btnContinuar);
            this.Controls.Add(this.grdAttributes);
            this.Controls.Add(this.lblDestinoXML);
            this.Controls.Add(this.btnEstableceXML);
            this.Controls.Add(this.lblDestino);
            this.Controls.Add(this.btnEstablecerHTMLFromDB);
            this.Controls.Add(this.btnContinueStep1);
            this.Controls.Add(this.txtTabla);
            this.Controls.Add(this.txtEditionId);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblRuta);
            this.Controls.Add(this.btnRutaHTML);
            this.Controls.Add(this.lblLEy);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Name = "Inicial";
            this.Text = "Inicial";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdAttributes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog BrowseDialogHTML;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnRutaHTML;
        private System.Windows.Forms.Label lblRuta;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtEditionId;
        private System.Windows.Forms.TextBox txtTabla;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnContinueStep1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnEstablecerHTMLFromDB;
        private System.Windows.Forms.Label lblDestino;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnEstableceXML;
        private System.Windows.Forms.Label lblDestinoXML;
        private System.Windows.Forms.DataGridView grdAttributes;
        private System.Windows.Forms.Label lblLEy;
        private System.Windows.Forms.Button btnContinuar;
    }
}

