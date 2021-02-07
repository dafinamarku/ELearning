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

    public bool CreateCourse(Kurs k)
    {
      var sameNameCourses = db.Kurset.Where(x => x.Emri == k.Emri).ToList();
      if (sameNameCourses.Count() > 0)
        return false;
      db.Kurset.Add(k);
      db.SaveChanges();
      return true;
    }

    public bool UpdateCourse(Kurs k)
    {
      Kurs currentCourse = db.Kurset.FirstOrDefault(x => x.KursId == k.KursId);
      if (currentCourse == null)
        return false;
      var sameNameCourses = db.Kurset.Where(x => x.Emri == k.Emri && x.KursId!=k.KursId).ToList();
      if (sameNameCourses.Count() > 0)
        return false;
      currentCourse.Emri = k.Emri;
      currentCourse.Photo = k.Photo;
      currentCourse.InstruktoriId = k.InstruktoriId;
      db.SaveChanges();
      return true;
    }

    public List<Kurs> CoursesWithoutInstructor()
    {
      return
        db.Kurset.Where(x => x.InstruktoriId == null).ToList();
    }

    //heq instruktorin me id perkatese nga nje kurs (nqs ai drejton nje kurs)
    public void RemoveInstructorFromCourse(string instructorId)
    {
      var instructorsCourse = db.Kurset.Where(x => x.InstruktoriId == instructorId).FirstOrDefault();
      if (instructorsCourse != null)
      {
        instructorsCourse.InstruktoriId = null;
        db.SaveChanges();
      }
    }
  }
}
