using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_learning.DomainModels;
using Microsoft.AspNet.Identity.EntityFramework;

namespace E_learning.DataLayer
{
  public class ProjectDbContext:IdentityDbContext<ApplicationUser>
  {
    public ProjectDbContext():base("ELearningCon")
    {

    }
    
    public DbSet<Kurs> Kurset { get; set; }
    public DbSet<Material> Materialet { get; set; }
    public DbSet<Nivel> Nivelet { get; set; }
    public DbSet<Tip> Tipet { get; set; }
    public DbSet<KursNivelTip> KursNivelTip { get; set; }
    public DbSet<Koment> Komentet { get; set; }
  }
}
