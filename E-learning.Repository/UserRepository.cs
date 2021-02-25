using E_learning.DataLayer;
using E_learning.DomainModels;
using E_learning.DomainModels.ViewModels;
using E_learning.RepositoryInterface;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Repository
{
  public class UserRepository:IUserRepository
  {
    ProjectDbContext db;
    ApplicationUserStore userStore;
    ApplicationUserManager userManager;

    public UserRepository()
    {
      this.db = new ProjectDbContext();
      this.userStore = new ApplicationUserStore(db);
      this.userManager = new ApplicationUserManager(userStore);
    }

    public ApplicationUser GetUserById(string id)
    {
      return db.Users.Include(x=>x.KursetEPreferuara).FirstOrDefault(x => x.Id == id);
    }

    public void UpdateUser(ApplicationUser u)
    {
      ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == u.Id);
      currentUser.Photo = u.Photo;
      currentUser.PasswordHash = u.PasswordHash;
      currentUser.Emri = u.Emri;
      currentUser.Mbiemri = u.Mbiemri;
      currentUser.Email = u.Email;
      currentUser.UserName = u.UserName;
      db.SaveChanges();
    }

    public bool CreateUser(ApplicationUser u, string role)
    {
      //kontrollojme nqs ka usera qe kane te njetin email ose username si ai i userit qe po shtohet
      List<ApplicationUser> searchUsers = db.Users.Where(m => m.UserName == u.UserName || m.Email == u.Email).ToList();
      if (searchUsers.Count > 0)
        return false;
      
      IdentityResult result =userManager.Create(u);
      userManager.AddToRole(u.Id, role);
      return result.Succeeded;
    }

    public List<InstructorViewModel> GetAllInstructors()
    {
      var instructorRoleId = db.Roles.Where(x => x.Name == "Instruktor").Select(x => x.Id).FirstOrDefault();
      List<InstructorViewModel> allInstructors = db.Users.Where(x => x.Roles.Any(r => r.RoleId == instructorRoleId) == true)
                                                    .GroupJoin(db.Kurset, u => u.Id, k => k.InstruktoriId,
                                                            (u, k) => new InstructorViewModel {
                                                              Instructor = u,
                                                              InstructorsCourse = k.FirstOrDefault()
                                                            }).ToList();
      return allInstructors;

    }

    public string GetUserRole(string uid)
    {
      var user = db.Users.Where(x => x.Id == uid).FirstOrDefault();
      if (user == null)
        return null;
      var roleId = user.Roles.Select(x => x.RoleId).FirstOrDefault();
      return
        db.Roles.Where(x => x.Id == roleId).Select(x => x.Name).FirstOrDefault();
    }

    public bool AddOrRemoveCourseFromMyFavorites(int courseId, string uid)
    {
      var user = db.Users.FirstOrDefault(x => x.Id == uid);
      var course = db.Kurset.FirstOrDefault(x => x.KursId == courseId);
      if (user == null || course == null)
        return false;
      //ne kete rast kursi eshte ne listen e kurseve te preferuara te userit =>duhet ta fshijme nga kjo liste
      if (user.KursetEPreferuara.Contains(course))
        user.KursetEPreferuara.Remove(course);
      else
        user.KursetEPreferuara.Add(course);
      db.SaveChanges();
      return true;
    }

    public bool DeleteUser(string UserId)
    {
      ApplicationUser existingApplicationUser = db.Users.Where(x => x.Id == UserId).FirstOrDefault();
      IdentityResult result = userManager.Delete(existingApplicationUser);
      if (result.Succeeded)
        return true;
      else
        return false;
    }

  }
}
