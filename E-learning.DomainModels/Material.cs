using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    [Required(ErrorMessage ="Fusha Titulli eshte e kerkuar.")]
    public string Titulli { get; set; }
    public string Filename { get; set; }
    [StringLength(300, ErrorMessage ="Pershkrimi nuk mund te permbaje me shume se 300 karaktere.")]
    public string Pershkrimi { get; set; }
    [ForeignKey("Seksioni")]
    public int SeksioniId { get; set; }

    public virtual KursNivelTip Seksioni { get; set; }
  }
}
