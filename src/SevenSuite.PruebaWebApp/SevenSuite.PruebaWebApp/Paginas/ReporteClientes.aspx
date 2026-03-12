<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReporteClientes.aspx.cs" Inherits="SevenSuite.PruebaWebApp.Paginas.ReporteClientes" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>
<html lang="es">
  <head>
    <title>Visor de Reportes</title>
    <style>
      html, body, form, #rvClientes {
        height: 100%;
        margin: 0;
        padding: 0;
      }
    </style>
  </head>

  <body>
    <form id="form" runat="server">
      <asp:ScriptManager ID="ScriptManagerOne" runat="server"></asp:ScriptManager>
      <rsweb:ReportViewer
        ID="rvClientes"
        runat="server"
        width="100%"
        height="100%"
        ExportContentDisposition="AlwaysInline"
      >
      </rsweb:ReportViewer>
    </form>
  </body>
</html>
