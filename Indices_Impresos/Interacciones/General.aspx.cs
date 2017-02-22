using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class General : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            this.clearProperties();
        }
        

    }



   protected void btnInt_Click(object sender, EventArgs e)
   {
       EditionIdI = Convert.ToInt32(txtEditionInt.Text);

       this.Response.Redirect("Interactions.aspx?Edition=" + EditionIdI );

       

        
   }

 protected void btnCont_Click(object sender, EventArgs e)
   {
       EditionIdC = Convert.ToInt32(txtEditionCont.Text);

       this.Response.Redirect("Contraindications.aspx?Edition=" + EditionIdC);
   }


 protected void btnIMSF_Click(object sender, EventArgs e)
 {
     EditionIMSF = Convert.ToInt32(txtEditionIMSF.Text);

     this.Response.Redirect("IMSubstanceFoods.aspx?Edition=" + EditionIMSF);
 }

 protected void btnPIACT_Click(object sender, EventArgs e)
 {
     EditionICDATC = Convert.ToInt32(txtEditionPICDATC.Text);

     this.Response.Redirect("ProductCIEATC.aspx?Edition=" + EditionICDATC);
 }


 private void clearProperties()
 {

     this.txtEditionInt.Text = "";
     this.txtEditionCont.Text = "";

 }
   private int EditionIdI;
   private int EditionIdC;
   private int EditionIMSF;
   private int EditionICDATC;

 
}