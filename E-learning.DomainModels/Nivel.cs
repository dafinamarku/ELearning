using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.DomainModels
{
  [Table("Nivelet")]
  public class Nivel
  {
    public int Id { get; set; }
    public string Emri { get; set; }
    public virtual ICollection<KursNivelTip> kursNivelTip { get; set; }
  }
}
