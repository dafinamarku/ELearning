using E_learning.DomainModels;
using E_learning.DomainModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.ServiceInterface
{
  public interface IUserService
  {
    ApplicationUser GetUserById(string id);
    void UpdateUser(ApplicationUser u);
    bool CreateUser(ApplicationUser u, string role);
    List<InstructorViewModel> GetAllInstructors();
    string GetUserRole(string uid);
    bool AddOrRemoveCourseFromMyFavorites(int courseId, string uid);
  }
}
