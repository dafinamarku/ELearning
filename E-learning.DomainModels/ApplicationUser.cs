using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.DomainModels
{
  public class ApplicationUser : IdentityUser
  {
    public string Emri { get; set; }
    public string Mbiemri { get; set; }
    public string Photo { get; set; }
    public virtual ICollection<Kurs> KursetEPreferuara { get; set; }
    public virtual ICollection<Koment> Komentet { get; set; }
  }
}
