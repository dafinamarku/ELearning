using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.DomainModels
{
  [Table("Materialet")]
  public class Material
  {
    public int Id { get; set; }
    public string Titulli { get; set; }
    public string Filename { get; set; }
    public string Pershkrimi { get; set; }
    [ForeignKey("Seksioni")]
    public int SeksioniId { get; set; }

    public virtual KursNivelTip Seksioni { get; set; }
  }
}
