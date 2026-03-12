using SevenSuite.PruebaWebApp.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace SevenSuite.PruebaWebApp.AccesoDatos
{
  public class ClienteDAL
  {
    public List<Seveclie> Get(string filtro)
    {
      List<Seveclie> clients = new List<Seveclie>();

      using (SqlConnection cn = Conexion.ConnectionRead())
      {
        SqlCommand cmd = new SqlCommand("usp_ClientsGet", cn);
        cmd.CommandType = CommandType.StoredProcedure;

        if (!string.IsNullOrEmpty(filtro)) cmd.Parameters.AddWithValue("@Filtro", filtro);

        using (SqlDataReader dr = cmd.ExecuteReader())
        {
          while (dr.Read())
         {
            string fechaNac = Convert.ToDateTime(dr["fecha_nac"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

            clients.Add(new Seveclie()
            {
              id_clie = Convert.ToInt32(dr["id_clie"]),
              cedula = dr["cedula"].ToString(),
              nombre = dr["nombre"].ToString(),
              genero = dr["genero"].ToString(),
              fecha_nac = fechaNac,
              id_estado_civil = Convert.ToInt32(dr["id_estado_civil"]),
              desc_estado_civil = dr["desc_estado_civil"].ToString()
            });
          }
        }
      }

      return clients;
    }

    public Seveclie GetById(int id)
    {
      Seveclie client = null;

      using (SqlConnection cn = Conexion.ConnectionRead())
      {
        SqlCommand cmd = new SqlCommand("usp_ClientGetById", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@id", id);

        using (SqlDataReader dr = cmd.ExecuteReader())
        {
          if (dr.Read())
          {
            string fechaNac = Convert.ToDateTime(dr["fecha_nac"]).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

            client = new Seveclie()
            {
              id_clie = Convert.ToInt32(dr["id_clie"]),
              cedula = dr["cedula"].ToString(),
              nombre = dr["nombre"].ToString(),
              genero = dr["genero"].ToString(),
              fecha_nac = fechaNac,
              id_estado_civil = Convert.ToInt32(dr["id_estado_civil"])
            };
          }
        }
      }

      return client;
    }

    public bool Save(Seveclie clientData)
    {
      using (SqlConnection cn = Conexion.ConnectionRead())
      {
        SqlCommand cmd = new SqlCommand("usp_ClientUpsert", cn);
        cmd.CommandType = CommandType.StoredProcedure;

        DateTime fecha_nac = DateTime.ParseExact(clientData.fecha_nac, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        SqlParameter[] paramsList = new[]
        {
          new SqlParameter("@id_clie", clientData.id_clie),
          new SqlParameter("@cedula", clientData.cedula),
          new SqlParameter("@nombre", clientData.nombre),
          new SqlParameter("@genero", clientData.genero),
          new SqlParameter("@fecha_nac", fecha_nac),
          new SqlParameter("@id_estado_civil", clientData.id_estado_civil)
        };

        cmd.Parameters.AddRange(paramsList);

        return cmd.ExecuteNonQuery() > 0;
      }
    }

    public bool Delete(int id)
    {
      using (SqlConnection cn = Conexion.ConnectionRead())
      {
        SqlCommand cmd = new SqlCommand("usp_ClientDelete", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@id_clie", id);

        return cmd.ExecuteNonQuery() > 0;
      }
    }
  }
}
