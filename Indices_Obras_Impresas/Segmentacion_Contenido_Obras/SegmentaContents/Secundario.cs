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
    public partial class Secundario : Form
    {
        int edition;
        public Secundario(int editionId)
        {
            edition = editionId;
            InitializeComponent();
        }

        private void Secundario_Load(object sender, EventArgs e)
        {
            
        }
    }
}
