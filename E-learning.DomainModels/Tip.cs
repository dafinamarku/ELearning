using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.DomainModels
{
  [Table("Tipet")]
  public class Tip
  {
    public int Id { get; set; }
    public string Tipi { get; set; }
    public virtual ICollection<KursNivelTip> kursNivelTip { get; set; }
  }
}
