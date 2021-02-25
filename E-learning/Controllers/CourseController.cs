using E_learning.DomainModels;
using E_learning.Extensions;
using E_learning.ServiceInterface;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
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
        public ActionResult AllCourses(string search)
        {
            if (Request.IsAuthenticated)
            {
              string uid = User.Identity.GetUserId();
              ViewBag.CurrentUser = userService.GetUserById(uid);
            }
            List<Kurs> model = new List<Kurs>();
            if (string.IsNullOrEmpty(search))
            {
              model = courseService.GetAllCourses();
              ViewBag.Search = null;
            }
            else
            {
              model = courseService.SearchCourses(search);
              ViewBag.Search = search;
            }
              
            return View(model);
        }
        [Authorize]
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
          ViewBag.InstruktorId = course.InstruktoriId;
          return View(course_levels.OrderBy(x=>x.Key).ToDictionary(x => x.Key, x=>course_levels[x.Key]));
        }

        [Authorize]
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
          Dictionary<int, Tip> section_types = new Dictionary<int, Tip>();
          foreach(var section in courseSections)
          {
            if (section.Tipi!=null && !section_types.ContainsValue(section.Tipi))
              section_types.Add(section.Id, section.Tipi);
          }
          ViewBag.Kursi = course.Emri;
          ViewBag.KursId = course.KursId;
          ViewBag.Niveli = level.Emri;
          ViewBag.NivelId = level.Id;
          ViewBag.InstruktorId = course.InstruktoriId;
          return View(section_types.OrderBy(x=>x.Value.Id).ToDictionary(x => x.Key, x => section_types[x.Key]));
        }

        [Authorize(Roles ="Admin")]
        public ActionResult CreateCourse()
        {
          Kurs model = new Kurs();
          return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CreateCourse(HttpPostedFileBase file, Kurs model)
        {
          if (file != null)
          {
            string[] allowedFileTypes = { ".png", ".jpeg", ".jpg" };
            var extension = Path.GetExtension(file.FileName);
            if (allowedFileTypes.Contains(extension))
            {
              if (ModelState.IsValid)
              {

              }
              //Krijojme Kursin
              Kurs newKurs = new Kurs()
              {
                Emri = model.Emri,
                Photo = file.FileName
              };
              bool wasCreated = courseService.CreateCourse(newKurs);
              if (!wasCreated)
              {
                ModelState.AddModelError("Err", "Emri qe ju zgjodhet per kursin tashme ekziston ne sistem. Ju lutem, zgjidhni nje emer tjeter.");
                return View(model);
              }
              var path = Path.Combine(Server.MapPath(MaterialPaths.CoursePhotoPath), file.FileName);
              file.SaveAs(path);
              this.AddNotification("Kursi u shtua me suskses.", NotificationType.SUCCESS);
              return RedirectToAction("AllCourses");
            }
          }
          ViewBag.Photo = "Duhet te zgjidhni nje foto.";
          return View(model);
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

        [Authorize(Roles="Admin")]
        public ActionResult DeleteCourse(int id)
        {
          var courseToDelete = courseService.GetCourseById(id);
          if (courseToDelete == null)
            return new HttpNotFoundResult();
          courseService.DeleteCourse(id);
          this.AddNotification("Kursi" + courseToDelete.Emri + " u fshi", NotificationType.SUCCESS);
          return RedirectToAction("AllCourses");
        }
    
        [Authorize(Roles ="Instruktor")]
        public ActionResult MyCourse()
        {
          string uid = User.Identity.GetUserId();
          var myCourse = courseService.GetInstructorsCourse(uid);
          return RedirectToAction("CourseLevels", new { @id = myCourse.KursId });
        }

    [Authorize(Roles ="Admin")]
    public ActionResult EditCourse(int id)
    {
      var courseToEdit = courseService.GetCourseById(id);
      if (courseToEdit == null)
        return new HttpNotFoundResult();
      return View(courseToEdit);
    }

    [Authorize(Roles ="Admin")]
    [HttpPost]
    public ActionResult EditCourse(HttpPostedFileBase file, Kurs model)
    {
      if (ModelState.IsValid)
      {
        if (file != null)
        {
          string userId = User.Identity.GetUserId();
          string[] allowedFileTypes = { ".png", ".jpeg", ".jpg" };
          var extension = Path.GetExtension(file.FileName);
          if (allowedFileTypes.Contains(extension))
          {
            model.Photo = file.FileName;
            var path = Path.Combine(Server.MapPath(MaterialPaths.CoursePhotoPath), file.FileName);
            file.SaveAs(path);
          }
          else
          {
            ModelState.AddModelError("err", "Duhet te zgjidhni nje imazh.");
            return View();
          }
        }
        bool wasEdited = courseService.UpdateCourse(model);
        if (wasEdited)
          this.AddNotification("Kursi u editua", NotificationType.SUCCESS);
        else
          this.AddNotification("Kursi nuk u editua", NotificationType.ERROR);
        return RedirectToAction("AllCourses");

      }
      return View(model);
    }

    [Authorize(Roles ="Student")]
    public ActionResult CourseQuizzes()
    {
      return View();
    }
    }
}