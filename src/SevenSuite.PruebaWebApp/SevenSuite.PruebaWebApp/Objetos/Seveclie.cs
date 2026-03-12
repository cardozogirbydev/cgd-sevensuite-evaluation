using System;

namespace SevenSuite.PruebaWebApp.Objetos
{
  public class Seveclie
  {
    public int id_clie { get; set; }
    public string cedula { get; set; }
    public string nombre { get; set; }
    public string genero { get; set; }
    public string fecha_nac { get; set; }
    public int id_estado_civil { get; set; }

    public string desc_estado_civil { get; set; }
  }
}
