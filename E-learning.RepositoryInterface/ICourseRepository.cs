using E_learning.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.RepositoryInterface
{
  public interface ICourseRepository
  {
    Kurs GetCourseById(int id);
    List<Kurs> GetAllCourses();
    List<KursNivelTip> GetCourseSections(int id);
    bool CreateCourse(Kurs k);
    bool UpdateCourse(Kurs k);
    void DeleteCourse(int id);
    List<Kurs> CoursesWithoutInstructor();
    void RemoveInstructorFromCourse(string instructorId);
  }
}
