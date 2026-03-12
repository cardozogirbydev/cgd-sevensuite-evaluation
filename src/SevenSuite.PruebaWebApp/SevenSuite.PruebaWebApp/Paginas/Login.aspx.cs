using SevenSuite.PruebaWebApp.Logica;
using SevenSuite.PruebaWebApp.Objetos;
using System;
using System.Web;
using System.Web.Services;

namespace SevenSuite.PruebaWebApp.Paginas
{
  public partial class Login:System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      HttpCookie cookieUser = Request.Cookies["CookieUser"];

      if (cookieUser != null) Response.Redirect("~/Paginas/Home.aspx");
    }

    [WebMethod]
    public static object LoginProcess(string user, string pass)
    {
      try
      {
        if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(pass))
        {
          return new { Success = false, Info = "Debe indicar usuario y contraseña." };
        }

        UsuarioBLL usuarioBll = new UsuarioBLL();
        Usuario usuario = usuarioBll.Login(user.Trim(), pass);

        if (usuario == null) return new { Success = false, Info = "Usuario o contraseña incorrecta." };

        HttpCookie cookieUser = new HttpCookie("CookieUser");
        cookieUser["UserID"] = usuario.id_usuario.ToString();
        cookieUser["UserName"] = usuario.username;

        cookieUser.Expires = DateTime.Now.AddHours(5);
        cookieUser.HttpOnly = true;

        HttpContext.Current.Response.Cookies.Add(cookieUser);

        return new { Success = true };
      }
      catch (Exception ex)
      {
        return new { Success = false, Info = $"Error en el servidor: {ex.Message}" };
      }
    }
  }
}
