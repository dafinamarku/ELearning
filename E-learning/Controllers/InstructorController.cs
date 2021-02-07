using E_learning.DomainModels;
using E_learning.Extensions;
using E_learning.ServiceInterface;
using E_learning.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace E_learning.Controllers
{
    [Authorize(Roles ="Admin")]
    public class InstructorController : Controller
    {
          IUserService userService;
          ICourseService courseService;

          public InstructorController(IUserService serv, ICourseService cserv)
          {
            this.userService = serv;
            this.courseService = cserv;
          }

          public ActionResult AllInstructors()
          {
            ViewBag.NrCoursesWithoutInstructor = courseService.CoursesWithoutInstructor().Count();
            var model = userService.GetAllInstructors();
            return View(model);
          }

         public ActionResult CreateInstructor()
         {
          //perdoret per te thirrur action metoden e duhur (CreateInstructor ose Register) ne view-ne Register 
          ViewBag.CreateInstructor = true;

          return View();
         }

        [HttpPost]
        public ActionResult CreateInstructor(RegisterViewModel model)
        {
          if (ModelState.IsValid)
          {
            var passwordHash = Crypto.HashPassword(model.Password);
            ApplicationUser user = new ApplicationUser() {
              Email = model.Email,
              UserName = model.Username,
              PasswordHash = passwordHash,
              Emri = model.Emri,
              Mbiemri = model.Mbiemri
            };
            if (userService.CreateUser(user, "Instruktor"))
            {
              this.AddNotification("Instruktori u shtua", NotificationType.SUCCESS);
              return RedirectToAction("AllInstructors");
            }
            else
              ModelState.AddModelError("Err", "Nje perdorues me te njetin Email OSE Username tashme ekziston ne sistem.");
          }
          return View(model);
        }

        public ActionResult CaktoiNjeKurs(string id)//id e instruktorit
        {
          ViewBag.InstructorId = id;
          ViewBag.KursetEDisponueshme = new SelectList(courseService.CoursesWithoutInstructor(), "KursId", "Emri");
          return View();
        }

        [HttpPost]
        public ActionResult CaktoiNjeKurs(string id, Kurs model)
        {
          if (userService.GetUserRole(id) != "Instruktor")
            return new HttpNotFoundResult();
          //ne kete rast eshte perzgjedhur opsioni "Nuk ka asnje kurs ---" dhe instruktori duhet te hiqet nga kursi qe ai drejton (nqs ka)
          if (model.KursId == 0)
          {
            courseService.RemoveInstructorFromCourse(id);
            this.AddNotification("Veprimi u krye me sukses", NotificationType.SUCCESS);
          }
          else
          {
            var course = courseService.GetCourseById(model.KursId);
            if (course == null)
              return new HttpNotFoundResult();
            if (course.InstruktoriId != null)
              this.AddNotification("Instruktori qe ju perzgjodhet nuk mund te behet instruktor i kursit " + course.Emri, NotificationType.ERROR);
            else
            {
              course.InstruktoriId = id;
              courseService.UpdateCourse(course);
              this.AddNotification("Veprimi u krye me sukses", NotificationType.SUCCESS);
            }
          }
          return RedirectToAction("AllInstructors");
        }
    }
}