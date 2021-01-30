using E_learning.DataLayer;
using E_learning.DomainModels;
using E_learning.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Repository
{
  public class LevelRepository:ILevelRepository
  {
    ProjectDbContext db;

    public LevelRepository()
    {
      this.db = new ProjectDbContext();
    }

    public Nivel GetLevelById(int id)
    {
      return db.Nivelet.FirstOrDefault(x => x.Id == id);
    }
  }
}
