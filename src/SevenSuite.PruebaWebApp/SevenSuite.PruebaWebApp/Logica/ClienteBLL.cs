using SevenSuite.PruebaWebApp.AccesoDatos;
using SevenSuite.PruebaWebApp.Objetos;
using System.Collections.Generic;

namespace SevenSuite.PruebaWebApp.Logica
{
  public class ClienteBLL
  {
    private ClienteDAL clienteDal = new ClienteDAL();

    public List<Seveclie> GetClients(string filtro)
    {
      return clienteDal.Get(filtro);
    }

    public Seveclie GetClientById(int id)
    {
      return clienteDal.GetById(id);
    }

    public bool SaveClient(Seveclie clientData)
    {
      return clienteDal.Save(clientData);
    }

    public bool DeleteClient(int id)
    {
      return clienteDal.Delete(id);
    }
  }
}
