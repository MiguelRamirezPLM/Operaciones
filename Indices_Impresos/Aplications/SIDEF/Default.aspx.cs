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
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //int labid = this.Request.QueryString["LabId"].ToString() == "" ? 60 : Convert.ToInt32(this.Request.QueryString["LabId"].ToString());

       
        if (this.Request.QueryString["DivisionId"] != null)
        {
           this._divisionId = Convert.ToInt32(this.Request.QueryString["DivisionId"].ToString());

            SqlDataAdapter sql;
           // DataSet ds = new DataSet();
            //string cadena = "SELECT DISTINCT T.PRODUCTID as ProductId,TMP.PHARMAFORMID as PharmaFormId,T.IMAGE as ImageName " +
            //                "FROM TMPSIDEF2 T INNER JOIN " + 
            //                "(SELECT DISTINCT v.ProductId,v.Brand,v.Page," + 
            //                "dbo.[plm_dfGetPharmaFormsByDiffPagedProduct] (v.ProductId, 1, v.Page) AS Forma," +
            //                "(SELECT TOP 1 PharmaFormId FROM ProductVersionPages WHERE ProductId = v.ProductId AND " +
            //                "Page = v.Page AND VersionId = 1 ORDER BY PharmaFormId ) as PharmaformId " +
            //                "FROM plm_vwPagedProductsMx v ) TMP " +
            //                "ON(T.PRODUCTID = TMP.PRODUCTID AND T.PAGE = TMP.PAGE) " +
            //                "WHERE T.LABORATORYID = " + labid +
            //                "AND T.IMAGE NOT LIKE '%health%' " +
            //                " ORDER BY T.PRODUCTID ";




            string cadena = "SELECT DISTINCT(edp.DivisionId), edp.ProductId, v.DivisionShortName, v.Brand, edp.ProductShot, edp.PharmaFormId " +
                             " FROM EditionProductShots edp " +
                              "      INNER JOIN plm_vwProductsByEdition v ON (edp.ProductId = v.ProductId and" +
                               "                      edp.EditionId = v.EditionId and " +
                                "                     edp.DivisionId = v.DivisionId and " +
                                 "                    edp.CategoryId = v.CategoryId and " +
                                  "                   edp.PharmaFormId = v.PharmaFormId) " +
                                 "WHERE edp.ProductShot != '' and edp.EditionId = 32 and v.TypeInEdition='P'  and edp.DivisionId = " + _divisionId +
                                 "order by  4";
                            



            sql = new SqlDataAdapter(cadena, System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionImg"].ToString());
            sql.Fill(_ds, "Images");
            this.lblLab.Text = this._ds.Tables["Images"].Rows[0]["DivisionShortName"].ToString();
            
        }

        

        DataList1.DataSource = this._ds.Tables["Images"];
       
        DataList1.DataBind();
        //ASPxDataView1.DataSource = ds.Tables["Images"];
        //ASPxDataView1.DataBind();


    }

    private int _divisionId;
    private DataSet _ds = new DataSet();

}
