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
  public class LevelRepository : ILevelRepository
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

    public List<Nivel> GetCourseLevels(int courseId)
    {
      List<Nivel> courseLevels = db.KursNivelTip.Where(x => x.KursiId == courseId).Select(x => x.Niveli).Distinct().ToList();

      return courseLevels;
    }

    public List<Nivel> GetAvailableLevelsForCourse(int courseId)
    {
      var currentCourseLevels = this.GetCourseLevels(courseId);
      List<Nivel> availableLevels = new List<Nivel>();
      foreach (var level in db.Nivelet.ToList())
      {
        if (!currentCourseLevels.Contains(level))
          availableLevels.Add(level);
      }
      return availableLevels;
    }

  }
}
