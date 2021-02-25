using E_learning.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.RepositoryInterface
{
  public interface ILevelRepository
  {
    Nivel GetLevelById(int id);
    List<Nivel> GetCourseLevels(int courseId);
    List<Nivel> GetAvailableLevelsForCourse(int courseId);
  }
}
