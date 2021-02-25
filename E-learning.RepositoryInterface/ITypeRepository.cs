using E_learning.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.RepositoryInterface
{
  public interface ITypeRepository
  {
    Tip GetTypeById(int id);
    List<Tip> GetAvailableTypesForCourseLevel(int courseId, int levelId);
  }
}
