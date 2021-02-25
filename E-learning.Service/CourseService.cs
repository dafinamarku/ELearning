using E_learning.DomainModels;
using E_learning.RepositoryInterface;
using E_learning.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Service
{
  public class CourseService:ICourseService
  {
    ICourseRepository repository;

    public CourseService(ICourseRepository rep)
    {
      this.repository = rep;
    }

    public Kurs GetCourseById(int id)
    {
      return repository.GetCourseById(id);
    }

    public List<Kurs> GetAllCourses()
    {
      return repository.GetAllCourses();
    }

    public List<KursNivelTip> GetCourseSections(int id)
    {
      return repository.GetCourseSections(id);
    }

    public bool CreateCourse(Kurs k)
    {
      return repository.CreateCourse(k);
    }

    public bool UpdateCourse(Kurs k)
    {
      return repository.UpdateCourse(k);
    }

    public List<Kurs> CoursesWithoutInstructor()
    {
      return repository.CoursesWithoutInstructor();
    }

    public void RemoveInstructorFromCourse(string instructorId)
    {
      repository.RemoveInstructorFromCourse(instructorId);
    }

    public void DeleteCourse(int id)
    {
      repository.DeleteCourse(id);
    }

    public bool CanUserModifyCourseLevelsAndTypes(string uid, int courseId)
    {
      var course = repository.GetCourseById(courseId);
      return (course != null && course.InstruktoriId == uid);
    }

    public Kurs GetInstructorsCourse(string instructorId)
    {
      return
        repository.GetAllCourses().FirstOrDefault(x => x.InstruktoriId == instructorId);
    }

    public List<Kurs> SearchCourses(string search)
    {
      var allCourses = this.GetAllCourses();
      search = search.ToUpper();
      return allCourses.Where(x => x.Emri.ToUpper().Contains(search)).ToList();
    }
  }
}
