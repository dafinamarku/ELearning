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
  public class TypeService:ITypeService
  {
    ITypeRepository repository;

    public TypeService(ITypeRepository repo)
    {
      this.repository = repo;
    }

    public Tip GetTypeById(int id)
    {
      return repository.GetTypeById(id);
    }

    public List<Tip> GetAvailableTypesForCourseLevel(int courseId, int levelId)
    {
      return repository.GetAvailableTypesForCourseLevel(courseId, levelId);
    }
  }
}
