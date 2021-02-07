using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.DomainModels
{
  [Table("Komentet")]
  public class Koment
  {
    public int Id { get; set; }
    [Required]
    public string Teksti { get; set; }
    public DateTime DataEKrijimit { get; set; }
    [ForeignKey("Autori")]
    public string AutoriId { get; set; }
    [ForeignKey("Pergjigjet")]
    public int? KomentId { get; set; }
    public virtual ApplicationUser Autori { get; set; }
    public virtual ICollection<Koment> Pergjigjet { get; set; }
  }
}
