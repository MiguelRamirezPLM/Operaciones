using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraReports.UI;
using DevExpress.XtraReports.Parameters;
using System.Drawing;
using System.IO;
using System.Drawing.Printing;
public partial class downFile : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    { 
        this._editionId = this.Session["editionId"] == null ? -1 : Convert.ToInt32(this.Session["editionId"]);
        this._pathFile = Request.QueryString["p"] == null ? "" : (Request.QueryString["p"]).ToString();
        this._fileName = Request.QueryString["f"] == null ? "" : (Request.QueryString["f"]).ToString();
        this._contetType = Request.QueryString["c"] == null ? "" : (Request.QueryString["c"]).ToString();
        this._roleId = this.Session["roleId"] == null ? -1 : Convert.ToInt32(this.Session["roleId"]);
        this._bookName = this.Session["bookName"] == null ? "" : this.Session["bookName"].ToString();
        this._editionName = this.Session["edition"] == null ? "" : (this.Session["edition"]).ToString();
        this._userId = this.Session["userId"] == null ? -1 : Convert.ToInt32(this.Session["userId"]);
        this._gen = this.Session["gen"] == null ? -1 : Convert.ToInt32(this.Session["gen"]);
        this._user = this.Session["user"] == null ? "" : this.Session["user"].ToString();
        this._doc= this.Session["doc"] == null ? null : (byte[])this.Session["doc"];
            
            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + _fileName);
            //Response.ContentType = _contetType;
            //Response.WriteFile(_pathFile);
            Response.BinaryWrite(_doc);
            Response.End();

            
                
    }
    private string getPrefixApplication()
    {
        switch (this._roleId)
        {
            case (int)Utilities.Roles.Administrador:
                return "MA";
            case (int)Utilities.Roles.Medico:
                return "MM";
            default:
                return "MA";
        }
    }
    private DevExpress.XtraReports.UI.XtraReport createReport(System.Data.DataTable data, List<string> titles)
    {
        System.Data.DataTable dataContent = data;
        Report report = new Report();
        ReportHeaderBand header = new ReportHeaderBand();
        XRTable tableHeader = getReportHeader(titles);
        header.Controls.Add(tableHeader);
        DetailReportBand rband = new DetailReportBand();
        DetailBand band = new DetailBand();
        XRTable table = new XRTable();
        table.Borders = DevExpress.XtraPrinting.BorderSide.All;
        table.SizeF = new System.Drawing.SizeF(650f, 10f);
        band.Controls.Add(table);
        XRTableRow tRow = new DevExpress.XtraReports.UI.XRTableRow();
        XRTableCell col = new DevExpress.XtraReports.UI.XRTableCell();
        foreach (System.Data.DataColumn colu in dataContent.Columns)
        {
            col = new DevExpress.XtraReports.UI.XRTableCell();
            col.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(8)))), ((int)(((byte)(1)))), ((int)(((byte)(41)))));
            col.ForeColor = Color.White;
            col.SizeF = new System.Drawing.SizeF(50F, 45F);
            col.Text = colu.ColumnName;
            col.Font = new Font("Tahoma", 10, FontStyle.Bold);
            tRow.Cells.Add(col);
        }
        table.Rows.Add(tRow);
        List<XRTableRow> ROWS = new List<XRTableRow>();
        XRTableRow tContentRow = new DevExpress.XtraReports.UI.XRTableRow();
        XRTableCell column = new DevExpress.XtraReports.UI.XRTableCell();
        column.Font = new Font("tahoma", 10, FontStyle.Regular);
        column.SizeF = new System.Drawing.SizeF(50F, 45F);
        for (int r = 0; r < dataContent.Rows.Count; r++)
        {
            tContentRow = new DevExpress.XtraReports.UI.XRTableRow();
            for (int c = 0; c < dataContent.Columns.Count; c++)
            {
                column = new DevExpress.XtraReports.UI.XRTableCell();
                column.Text = dataContent.Rows[r][c].ToString();
                tContentRow.Cells.Add(column);
            }
            ROWS.Add(tContentRow);

        }
        XRTableRow[] arr = new XRTableRow[ROWS.Count];
        for (int i = 0; i < ROWS.Count; i++)
        {
            arr[i] = ROWS[i];
        }
        table.Rows.AddRange(arr);
        rband.Bands.Add(band);
        report.Bands.Add(header);
        report.Bands.Add(rband);
        return report;
    }
    public XRTable getReportHeader(List<string> titles)
    {
        XRTable tableHeader = new XRTable();

        tableHeader.SizeF = new System.Drawing.SizeF(850f, 10f);

        foreach (string val in titles)
        {
            XRTableCell title = new DevExpress.XtraReports.UI.XRTableCell();
            title.Text = val;
            title.Font = new Font("Tahoma", 14, FontStyle.Bold);
            XRTableRow tRowHeader = new DevExpress.XtraReports.UI.XRTableRow();
            tRowHeader.Cells.AddRange(new XRTableCell[] { title });
            tableHeader.Rows.Add(tRowHeader);
        }
        return tableHeader;
    }
    public void builFile() {

        //if (this._report!=null)
        //{
            
            MemoryStream ms = new MemoryStream();
            //this._report.ExportToXls(ms);
            byte[] buffer= new byte[ms.Length];
            ms.Position = 0;
            ms.Read(buffer, 0, (int) ms.Length);
            ms.Close();
            this.Session["down"] = "yes";
            this.Session["doc"] = buffer;
            this.Session["file"] = null;
           
            Response.Redirect("downFile.aspx");
           // Response.AddHeader("Content-Disposition", "attachment; filename=Archivo.xls");
            //Response.BinaryWrite(buffer);
            
           // this.btnDownload.Visible = false;
            //Response.End();
        //} 
    
    }

    private int _roleId;
    private string _fileName;
    private string _pathFile;
    private string _contetType;
    private int _editionId;
    private int _gen;
    private string _user;
    private string _bookName;
    private string _editionName;
    private int _userId;
    private byte[] _doc;
    
    
}