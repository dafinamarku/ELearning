using E_learning.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.ServiceInterface
{
  public interface ICourseService
  {
    Kurs GetCourseById(int id);
    List<Kurs> GetAllCourses();
    List<KursNivelTip> GetCourseSections(int id);
    bool CreateCourse(Kurs k);
    bool UpdateCourse(Kurs k);
    void DeleteCourse(int id);
    List<Kurs> CoursesWithoutInstructor();
    void RemoveInstructorFromCourse(string instructorId);
    bool CanUserModifyCourseLevelsAndTypes(string uid, int courseId);
    Kurs GetInstructorsCourse(string instructorId);
    List<Kurs> SearchCourses(string search);
  }
}
