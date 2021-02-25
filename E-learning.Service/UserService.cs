using E_learning.DomainModels;
using E_learning.DomainModels.ViewModels;
using E_learning.RepositoryInterface;
using E_learning.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Service
{
  public class UserService:IUserService
  {
    IUserRepository repository;

    public UserService(IUserRepository rep)
    {
      this.repository = rep;
    }

    public ApplicationUser GetUserById(string id)
    {
      return repository.GetUserById(id);
    }

    public void UpdateUser(ApplicationUser u)
    {
      repository.UpdateUser(u);
    }

    public bool CreateUser(ApplicationUser u, string role)
    {
      return
        repository.CreateUser(u, role);
    }

    public List<InstructorViewModel> GetAllInstructors()
    {
      return repository.GetAllInstructors();
    }

    public string GetUserRole(string uid)
    {
      return repository.GetUserRole(uid);
    }

    public bool AddOrRemoveCourseFromMyFavorites(int courseId, string uid)
    {
      return repository.AddOrRemoveCourseFromMyFavorites(courseId, uid);
    }

    public bool DeleteUser(string uid)
    {
      return repository.DeleteUser(uid);
    }
  }
}
