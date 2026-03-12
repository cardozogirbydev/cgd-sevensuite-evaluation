using SevenSuite.PruebaWebApp.AccesoDatos;
using SevenSuite.PruebaWebApp.Objetos;
using System.Collections.Generic;

namespace SevenSuite.PruebaWebApp.Logica
{
  public class EstadoCivilBLL
  {
    private EstadoCivilDAL estadoCivilDal = new EstadoCivilDAL();

    public List<EstadoCivil> GetCivilStatuses()
    {
      return estadoCivilDal.Get();
    }
  }
}
