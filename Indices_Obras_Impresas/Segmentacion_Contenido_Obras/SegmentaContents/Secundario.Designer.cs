namespace SegmentaContents
{
    partial class Secundario
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
            this.grdFaltantes = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdFaltantes)).BeginInit();
            this.SuspendLayout();
            // 
            // grdFaltantes
            // 
            this.grdFaltantes.AllowUserToOrderColumns = true;
            this.grdFaltantes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdFaltantes.Location = new System.Drawing.Point(1, 12);
            this.grdFaltantes.Name = "grdFaltantes";
            this.grdFaltantes.RowTemplate.Height = 24;
            this.grdFaltantes.Size = new System.Drawing.Size(419, 150);
            this.grdFaltantes.TabIndex = 0;
            // 
            // Secundario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 453);
            this.Controls.Add(this.grdFaltantes);
            this.Name = "Secundario";
            this.Text = "Secundario";
            this.Load += new System.EventHandler(this.Secundario_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdFaltantes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdFaltantes;
    }
}