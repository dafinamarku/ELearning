using E_learning.DomainModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.DataLayer
{
  public class ApplicationUserManager:UserManager<ApplicationUser>
  {
    public ApplicationUserManager(ApplicationUserStore userStore):base(userStore)
    {

    }
  }
}
