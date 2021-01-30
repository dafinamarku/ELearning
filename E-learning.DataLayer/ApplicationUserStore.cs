using E_learning.DomainModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.DataLayer
{
  public class ApplicationUserStore:UserStore<ApplicationUser>
  {
    public ApplicationUserStore(ProjectDbContext db):base(db)
    {

    }
  }
}
