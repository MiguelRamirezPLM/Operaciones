using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;


public class IndiceUsos : DEAQDataAccessAdapter<object>
{
    #region Constructors

    public IndiceUsos() { }

    #endregion

    #region Public Methods

    public DataTable getAlphabet()
    {
        StringBuilder sb = new StringBuilder();

        //sb.Append("\nSelect Distinct Substring(Usos,1,1) ");
        //sb.Append("\nFrom Usos u ");
        //sb.Append("\nInner Join RelUsos r On(u.IdUsos = r.IdUso) ");
        //sb.Append("\nInner Join Producto p On(r.IdProducto = p.IdProd) ");
        //sb.Append("\nWhere p.DEdicion = 1 And p.Pagina > 0 Order by 1");

        //sb.Append("\nSELECT DISTINCT Substring(AGROCHEMICALUSENAME,1,1)  FROM AGROCHEMICALUSES U ");
        //sb.Append("\nINNER JOIN PRODUCTAGROCHEMICALUSES PU ON U.AGROCHEMICALUSEID = PU.AGROCHEMICALUSEID ");
        //sb.Append("\nINNER JOIN PARTICIPANTPRODUCTS PP ON PU.PRODUCTID = PP.PRODUCTID ");
        //sb.Append("\nWHERE PP.EDITIONID=3 ");
        //sb.Append("\nORDER BY 1 ");

        sb.Append("\nSELECT DISTINCT SUBSTRING(AGROCHEMICALUSENAME,1,1) ");
        sb.Append("\nFROM dbo.plm_vwProductsByEdition V ");
        sb.Append("\nINNER JOIN PRODUCTAGROCHEMICALUSES PU ON (V.PRODUCTID = PU.PRODUCTID) ");
        sb.Append("\nINNER JOIN AGROCHEMICALUSES U ON (PU.AGROCHEMICALUSEID = U.AGROCHEMICALUSEID) ");
        sb.Append("\nWHERE V.EDITIONID = 5  AND U.ACTIVE=1 AND V.TYPEINEDITION = 'P' ");
        sb.Append("\nORDER BY 1 ");
        DataSet ds = IndiceUsos.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];

    }

    public DataTable getUsos(string letter)
    {
        StringBuilder sb = new StringBuilder();

        //sb.Append("\nSelect Distinct IdUsos,UPPER(Usos) as Usos ");
        //sb.Append("\nFrom Usos u ");
        //sb.Append("\nInner Join RelUsos r On(u.IdUsos = r.IdUso) ");
        //sb.Append("\nInner Join Producto p On(r.IdProducto = p.IdProd) ");
        //sb.Append("\nWhere p.DEdicion = 1 And p.Pagina > 0 And Usos Like ('" + letter + "%') Order by 2");

        //sb.Append("SELECT DISTINCT U.AGROCHEMICALUSEID AS IdUsos, UPPER(U.AGROCHEMICALUSENAME) AS Usos FROM AGROCHEMICALUSES U ");
        //sb.Append("\nINNER JOIN PRODUCTAGROCHEMICALUSES PU ON U.AGROCHEMICALUSEID = PU.AGROCHEMICALUSEID ");
        //sb.Append("\nINNER JOIN PARTICIPANTPRODUCTS PP ON PU.PRODUCTID = PP.PRODUCTID ");
        //sb.Append("\nWHERE PP.EDITIONID=3 AND U.AGROCHEMICALUSENAME LIKE ('" + letter + "%') ");
        //sb.Append("\nORDER BY 2 ");

         sb.Append("\nSELECT DISTINCT U.AGROCHEMICALUSEID AS IdUsos, UPPER(U.AGROCHEMICALUSENAME) AS Usos ");
         sb.Append("\nFROM dbo.plm_vwProductsByEdition V ");
         sb.Append("\nINNER JOIN PRODUCTAGROCHEMICALUSES PU ON (V.PRODUCTID = PU.PRODUCTID)");
         sb.Append("\nINNER JOIN AGROCHEMICALUSES U ON (PU.AGROCHEMICALUSEID = U.AGROCHEMICALUSEID)"); 
         sb.Append("\nWHERE V.EDITIONID=5 AND U.ACTIVE=1  AND V.TYPEINEDITION = 'P' "); 
         sb.Append("\nAND U.AGROCHEMICALUSENAME LIKE ('" + letter + "%')");
         sb.Append("\nORDER BY 2 ");

        DataSet ds = IndiceUsos.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    public DataTable getProducts(int idUso)
    {
        StringBuilder sb = new StringBuilder();
        /*
        sb.Append("\nSelect Distinct p.Nombre,dbo.deaq_dfGetIngActByProduct(p.IDProd) as Sustancias, ");
        sb.Append("\ndbo.deaq_dfGetFormaByProduct(p.IDProd) as FFarmaceutica,p.Laboratorio,p.Pagina ");
        sb.Append("\nFrom Producto p ");
        sb.Append("\nInner Join RelUsos r On(p.IdProd = r.IdProducto) ");
        //sb.Append("\nLeft Join RelFFarmaceutica rf On(p.IdProd = rf.IdProducto) ");
        //sb.Append("\nLeft Join FFarmaceutica f On(rf.IdFFarmaceutica = f.IdFF) ");
        sb.Append("\nWhere p.DEdicion = 1 And p.Pagina > 0 And IdUso = " + idUso + " Order by 1");*/


        //sb.Append("\nSELECT	DISTINCT P.PRODUCTNAME AS Nombre, ");
        //sb.Append("\ndbo.plm_dfGetActiveSubstancesByProduct(P.PRODUCTID, '') AS Sustancias, ");
        //sb.Append("\ndbo.plm_dfGetPharmaFormsByDiffPagedProduct(P.PRODUCTID, PP.EDITIONID, PP.PAGE) AS FFarmaceutica, ");
        //sb.Append("\nD.SHORTNAME AS Laboratorio, ");
        //sb.Append("\nPP.PAGE AS Pagina ");
        //sb.Append("\nFROM PRODUCTS P ");
        //sb.Append("\nINNER JOIN PRODUCTPHARMAFORMS PPF ON P.PRODUCTID = PPF.PRODUCTID ");
        //sb.Append("\nINNER JOIN PARTICIPANTPRODUCTS PP ON P.PRODUCTID = PP.PRODUCTID AND PPF.PHARMAFORMID = PP.PHARMAFORMID ");
        //sb.Append("\nINNER JOIN PRODUCTAGROCHEMICALUSES PU ON PP.PRODUCTID = PU.PRODUCTID ");
        //sb.Append("\nINNER JOIN DIVISIONS D ON PP.DIVISIONID = D.DIVISIONID ");
        //sb.Append("\nWHERE EDITIONID=3 AND AGROCHEMICALUSEID = " + idUso.ToString() + " ");
        //sb.Append("\nORDER BY 1 ");

          sb.Append("\nSELECT DISTINCT  UPPER(V.PRODUCTNAME) AS NOMBRE,"); 
          sb.Append("\ndbo.plm_dfGetActiveSubstancesByProduct(V.PRODUCTID, '') AS Sustancias,"); 
		  sb.Append("\ndbo.plm_dfGetPharmaFormsByDiffPagedProduct(V.PRODUCTID, V.EDITIONID, V.PAGE) AS FFarmaceutica,");
          sb.Append("\nV.DIVISIONSHORTNAME AS LABORATORIO,");
          sb.Append("\nV.PAGE AS PAGINA ");
          sb.Append("\nFROM dbo.plm_vwProductsByEdition V ");
          sb.Append("\nINNER JOIN PRODUCTAGROCHEMICALUSES PSA ON (V.PRODUCTID = PSA.PRODUCTID) ");
          sb.Append("\nINNER JOIN AGROCHEMICALUSES AU ON (PSA.AGROCHEMICALUSEID = AU.AGROCHEMICALUSEID)");
          sb.Append("\nWHERE V.EDITIONID = 5 AND V.TYPEINEDITION = 'P' AND AU.AGROCHEMICALUSEID = " + idUso.ToString() + " ");
          sb.Append("\nAND V.CATEGORYID NOT IN(2)");
          sb.Append("\nORDER BY 1");

        DataSet ds = IndiceUsos.DEAQInstanceDatabase.ExecuteDataSet(CommandType.Text, sb.ToString());

        return ds.Tables[0];
    }

    #endregion


    public static readonly IndiceUsos Instance = new IndiceUsos();

}
