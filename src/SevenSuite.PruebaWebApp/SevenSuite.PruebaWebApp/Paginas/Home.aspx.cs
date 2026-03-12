using SevenSuite.PruebaWebApp.Logica;
using SevenSuite.PruebaWebApp.Objetos;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;

namespace SevenSuite.PruebaWebApp.Paginas
{
  public partial class Home:System.Web.UI.Page
  {
    public string UserName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
      HttpCookie cookieUser = this.GetCookieUser();

      if (cookieUser == null) Response.Redirect("~/Paginas/Login.aspx");

      this.UserName = cookieUser["UserName"] ?? "Usuario";

      if (Request.QueryString["action"] == "logout") this.logout();
    }

    protected void logout()
    {
      HttpCookie cookieUser = this.GetCookieUser();

      if (cookieUser != null)
      {
        HttpCookie cookieUserReplace = new HttpCookie("CookieUser");
        cookieUserReplace.Expires = DateTime.Now.AddDays(-1);

        Response.Cookies.Add(cookieUserReplace);
      };

      Response.Redirect("~/Paginas/Login.aspx");
    }

    [WebMethod]
    public static object GetClients(string filtro)
    {
      try
      {
        if (!HasValidSession()) return new { Success = false, Info = "Sesion no valida." };

        ClienteBLL clienteBll = new ClienteBLL();
        List<Seveclie> clients = clienteBll.GetClients(filtro);

        return new { Success = true, Data = clients };
      }
      catch (Exception ex)
      {
        return new { Success = false, Info = ex.Message };
      }
    }

    [WebMethod]
    public static object GetClientById(int id)
    {
      try
      {
        if (!HasValidSession()) return new { Success = false, Info = "Sesion no valida." };

        ClienteBLL clienteBll = new ClienteBLL();
        Seveclie client = clienteBll.GetClientById(id);

        return new { Success = true, Data = client };
      }
      catch (Exception ex)
      {
        return new { Success = false, Info = ex.Message };
      }
    }

    [WebMethod]
    public static object GetCivilStatuses()
    {
      try
      {
        if (!HasValidSession()) return new { Success = false, Info = "Sesion no valida." };

        EstadoCivilBLL estadoCivilBll = new EstadoCivilBLL();
        List<EstadoCivil> civilStatuses = estadoCivilBll.GetCivilStatuses();

        return new { Success = true, Data = civilStatuses };
      }
      catch (Exception ex)
      {
        return new { Success = false, Info = ex.Message };
      }
    }

    [WebMethod]
    public static object SaveClient(Seveclie clientData)
    {
      try
      {
        if (!HasValidSession()) return new { Success = false, Info = "Sesion no valida." };

        ClienteBLL clienteBll = new ClienteBLL();
        bool Success = clienteBll.SaveClient(clientData);

        return new { Success };
      }
      catch (Exception ex)
      {
        return new { Success = false, Info = ex.Message };
      }
    }

    [WebMethod]
    public static object DeleteClient(int id)
    {
      try
      {
        if (!HasValidSession()) return new { Success = false, Info = "Sesion no valida." };

        ClienteBLL clienteBll = new ClienteBLL();
        bool Success = clienteBll.DeleteClient(id);

        return new { Success };
      }
      catch (Exception ex)
      {
        return new { Success = false, Info = ex.Message };
      }
    }

    private HttpCookie GetCookieUser()
    {
      return Request.Cookies["CookieUser"];
    }

    private static bool HasValidSession()
    {
      HttpCookie CookieUser = HttpContext.Current != null ? HttpContext.Current.Request.Cookies["CookieUser"] : null;

      return CookieUser != null;
    }
  }
}
