using SevenSuite.PruebaWebApp.Objetos;
using System;
using System.Data.SqlClient;

namespace SevenSuite.PruebaWebApp.AccesoDatos
{
  public class UsuarioDAL
  {
    public Usuario UserValidate(string user, string pass)
    {
      Usuario userFound = null;

      using (SqlConnection cn = Conexion.ConnectionRead())
      {
        string query = @"
          SELECT
            [id_usuario],
            [username]
          FROM [USUARIO]
          WHERE [username] = @user AND [password] = @pass
        ";

        SqlParameter[] paramsList = new[]
        {
          new SqlParameter("@user", user),
          new SqlParameter("@pass", pass)
        };

        SqlCommand cmd = new SqlCommand(query, cn);
        cmd.Parameters.AddRange(paramsList);

        using (SqlDataReader dr = cmd.ExecuteReader())
        {
          if (dr.Read())
          {
            userFound = new Usuario
            {
              id_usuario = Convert.ToInt32(dr["id_usuario"]),
              username = dr["username"].ToString()
            };
          }
        }
      }

      return userFound;
    }
  }
}
