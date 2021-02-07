using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.DomainModels.ViewModels
{
  public class InstructorViewModel
  {
    public ApplicationUser Instructor { get; set; }
    public Kurs InstructorsCourse { get; set; }
  }
}
