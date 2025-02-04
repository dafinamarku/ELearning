﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.DomainModels
{
  [Table("Kurset")]
  public class Kurs
  {
    public int KursId { get; set; }
    [Required(ErrorMessage ="Fusha {0} eshte e kerkuar.")]
    [StringLength(30, ErrorMessage ="Emri i Kursit nuk mund te jete me i gjate se 30 karaktere.")]
    public string Emri { get; set; }
    public string InstruktoriId { get; set; }
    public string Photo { get; set; }
    //lista e perdoruesve qe kete kurs e kane pjese te kurseve te preferuar
    public virtual ICollection<ApplicationUser> Perdoruesit { get; set; }
    public virtual ICollection<KursNivelTip> kursNivelTip { get; set; }

  }
}
