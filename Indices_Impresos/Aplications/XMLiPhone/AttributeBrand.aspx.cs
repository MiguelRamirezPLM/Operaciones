using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Serialization;

public partial class Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PharmaSearchEngine.ProductByEditionInfo[] prods = this._service.getDrugs("",11,true,19,true,"%");

        foreach (PharmaSearchEngine.ProductByEditionInfo prod in prods)
            this.getAttributes(prod);




    }

    public void getAttributes(PharmaSearchEngine.ProductByEditionInfo prod)
    {
        PharmaSearchEngine.AttributeByProductInfo[] attribs = 
            this._service.getAttributesByDrug("",19,true,prod.DivisionId,true,prod.CategotyId,true,
                prod.ProductId,true,prod.PharmaFormId,true);

        foreach (PharmaSearchEngine.AttributeByProductInfo attrib in attribs)
            this.getContent(attrib);


    }

    public void getContent(PharmaSearchEngine.AttributeByProductInfo attributes)
    {
        PharmaSearchEngine.ProductContentsInfo content =
            this._service.getContentByAttribute("", 19, true, attributes.DivisionId, true,
                attributes.CategoryId, true, attributes.ProductId, true, attributes.PharmaFormId, true,
                attributes.AttributeId, true);

        AttributeBrand ds = new AttributeBrand();

        ds.root.AddrootRow(attributes.AttributeName);

        ds.article.AddarticleRow(attributes.AttributeName, "", "", content.PlainContent, (AttributeBrand.rootRow)ds.root.Rows[0]);

        ds.WriteXml("C:/XML/attributes/" + content.ProductId.ToString() + "_" +
            content.PharmaFormId.ToString() + "_" + content.AttributeId.ToString() + ".xml");


    }


    private PharmaSearchEngine.iOSPharmaSearchEngine _service = new PharmaSearchEngine.iOSPharmaSearchEngine();

}
