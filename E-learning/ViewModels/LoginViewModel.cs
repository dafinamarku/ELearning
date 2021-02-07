using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_learning.ViewModels
{
  public class LoginViewModel
  {
    [Required(ErrorMessage = "Fusha {0} duhet te plotesohet")]
    public string Username { get; set; }
    [Required(ErrorMessage = "Fusha {0} duhet te plotesohet"), DataType(DataType.Password)]
    public string Password { get; set; }
  }
}