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
  public class CourseRepository:ICourseRepository
  {
    ProjectDbContext db;

    public CourseRepository()
    {
      this.db = new ProjectDbContext();
    }
    public Kurs GetCourseById(int id)
    {
      return db.Kurset.FirstOrDefault(x => x.KursId == id);
    }

    public List<Kurs> GetAllCourses()
    {
      return db.Kurset.ToList();
    }

    public List<KursNivelTip> GetCourseSections(int courseId)
    {
      return db.KursNivelTip.Where(x => x.Kursi.KursId == courseId).ToList();
    }
  }
}
