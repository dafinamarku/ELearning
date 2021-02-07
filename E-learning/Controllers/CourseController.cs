using E_learning.DomainModels;
using E_learning.Extensions;
using E_learning.ServiceInterface;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_learning.Controllers
{
    public class CourseController : Controller
    {
        ICourseService courseService;
        ILevelService levelService;
        IUserService userService;

        public CourseController(ICourseService serv, ILevelService lserv, IUserService userv)
        {
          this.courseService = serv;
          this.levelService = lserv;
          this.userService = userv;
        }
        // GET: Course
        public ActionResult AllCourses()
        {
            if (Request.IsAuthenticated)
            {
              string uid = User.Identity.GetUserId();
              ViewBag.CurrentUser = userService.GetUserById(uid);
            }
            List<Kurs> model = courseService.GetAllCourses();
            return View(model);
        }
        
        public ActionResult CourseLevels(int id)
        {
          Kurs course= courseService.GetCourseById(id);
          if (course == null)
            return HttpNotFound();
          List<KursNivelTip> courseSections = courseService.GetCourseSections(id);
          Dictionary<int, string> course_levels = new Dictionary<int, string>();
          foreach(var section in courseSections)
          {
            if (!course_levels.ContainsValue(section.Niveli.Emri))
            {
              course_levels.Add(section.Niveli.Id, section.Niveli.Emri);
            }
          }
          ViewBag.Kursi = course.Emri;
          ViewBag.KursId = id;
          return View(course_levels);
        }

        public ActionResult CourseLevelTypes(int courseId, int levelId)
        {
          Kurs course = courseService.GetCourseById(courseId);
          Nivel level = levelService.GetLevelById(levelId);

          if (course == null || level==null)
            return HttpNotFound();

          List<KursNivelTip> courseSections = courseService.GetCourseSections(courseId);
          //marrim nga lista vetem seksionet e kursit perkates qe kane si nivel 
          //nivelin e perzgjedhur nga useri
          courseSections = courseSections.Where(x => x.Kursi.KursId == courseId && x.Niveli.Id == levelId).ToList(); 
          Dictionary<int, string> section_types = new Dictionary<int, string>();
          foreach(var section in courseSections)
          {
            if (!section_types.ContainsValue(section.Tipi.Tipi))
              section_types.Add(section.Id, section.Tipi.Tipi);
          }
          ViewBag.Kursi = course.Emri;
          ViewBag.KursId = course.KursId;
          ViewBag.Niveli = level.Emri;
          return View(section_types);
        }

    //[Authorize(Roles ="Admin")]
        public ActionResult CreateCourse()
        {
          return View();
        }

        [Authorize(Roles ="Student")]
        public ActionResult AddOrRemoveCourseFromMyFavorites(int id, string redirectTo)//id e kursit
        {
          string uid = User.Identity.GetUserId();
          //kthen false vetem nqs useri ose kursi me id perkatese nuk ekzistojne ne db
          bool result = userService.AddOrRemoveCourseFromMyFavorites(id, uid);
          if (!result)
            return new HttpNotFoundResult();
          var course = courseService.GetCourseById(id);
          if (redirectTo == "MyFavoriteCourses")
            return RedirectToAction("MyFavoriteCourses");
          else
              return RedirectToAction("AllCourses");
        }

        [Authorize(Roles ="Student")]
        public ActionResult MyFavoriteCourses()
        {
          string uid = User.Identity.GetUserId();
          ApplicationUser currentUser=userService.GetUserById(uid);
          return View(currentUser.KursetEPreferuara);
        }
    }
}