using SevenSuite.PruebaWebApp.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SevenSuite.PruebaWebApp.AccesoDatos
{
  public class EstadoCivilDAL
  {
    public List<EstadoCivil> Get()
    {
      List<EstadoCivil> estadosCivil = new List<EstadoCivil>();

      using (SqlConnection cn = Conexion.ConnectionRead())
      {
        SqlCommand cmd = new SqlCommand("usp_CivilStatusesGet", cn);
        cmd.CommandType = CommandType.StoredProcedure;

        using (SqlDataReader dr = cmd.ExecuteReader())
        {
          while (dr.Read())
          {
            estadosCivil.Add(new EstadoCivil
            {
              id_estado_civil = Convert.ToInt32(dr["id_estado_civil"]),
              descripcion = dr["descripcion"].ToString()
            });
          }
        }
      }

      return estadosCivil;
    }
  }
}
