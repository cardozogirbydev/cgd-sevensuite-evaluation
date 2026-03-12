using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SevenSuite.PruebaWebApp.AccesoDatos
{
  public class Conexion
  {
    public static string GetConnectionString()
    {
      return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
    }

    public static SqlConnection ConnectionRead()
    {
      SqlConnection cn = new SqlConnection(GetConnectionString());
      if (cn.State == ConnectionState.Closed) cn.Open();

      return cn;
    }
  }
}
