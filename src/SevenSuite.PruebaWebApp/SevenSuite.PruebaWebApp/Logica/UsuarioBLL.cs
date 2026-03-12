using SevenSuite.PruebaWebApp.AccesoDatos;
using SevenSuite.PruebaWebApp.Objetos;

namespace SevenSuite.PruebaWebApp.Logica
{
  public class UsuarioBLL
  {
    private UsuarioDAL usuarioDal = new UsuarioDAL();

    public Usuario Login(string user, string pass)
    {
      if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass)) return null;

      return usuarioDal.UserValidate(user, pass);
    }
  }
}
