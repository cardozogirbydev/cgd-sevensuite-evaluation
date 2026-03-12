using Microsoft.Reporting.WebForms;
using SevenSuite.PruebaWebApp.Logica;
using SevenSuite.PruebaWebApp.Objetos;
using System;
using System.Collections.Generic;
using System.Web;

namespace SevenSuite.PruebaWebApp.Paginas
{
  public partial class ReporteClientes:System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      HttpCookie cookieUser = Request.Cookies["CookieUser"];

      if (cookieUser == null)
      {
        Response.Redirect("~/Paginas/Login.aspx");
        return;
      }

      if (!IsPostBack) this.ReportGenerate();
    }

    private void ReportGenerate()
    {
      string filtro = Request.QueryString["filtro"] ?? "";

      ClienteBLL clienteBLL = new ClienteBLL();
      List<Seveclie> clients = clienteBLL.GetClients(filtro);

      rvClientes.LocalReport.ReportPath = Server.MapPath("~/Reportes/rptClientes.rdlc");
      rvClientes.LocalReport.DataSources.Clear();

      ReportDataSource rds = new ReportDataSource("DataSetClientes", clients);

      rvClientes.LocalReport.DataSources.Add(rds);
      rvClientes.LocalReport.Refresh();
    }
  }
}
