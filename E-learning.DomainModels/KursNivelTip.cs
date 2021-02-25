using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.DomainModels
{
  [Table("KursNivelTip")]
  public class KursNivelTip
  {
    public int Id { get; set; }
    [ForeignKey("Kursi")]
    public int? KursiId { get; set; }
    [ForeignKey("Niveli")]
    public int? NiveliId { get; set; }
    [ForeignKey("Tipi")]
    public int? TipiId { get; set; }
    public virtual Kurs Kursi { get; set; }
    public virtual Tip Tipi { get; set; }
    public virtual Nivel Niveli { get; set; }
    public virtual ICollection<Material> Materialet { get; set; }
  }
}
