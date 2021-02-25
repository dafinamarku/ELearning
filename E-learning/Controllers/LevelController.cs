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
    [Authorize(Roles="Instruktor")]
    public class LevelController : Controller
    {

        ILevelService levelService;
        ICourseService courseService;
        ISectionService sectionService;

        public LevelController(ILevelService serv, ICourseService cserv, ISectionService sserv)
        {
          this.levelService = serv;
          this.courseService = cserv;
          this.sectionService = sserv;
        }

        public ActionResult AddLevelInCourse(int id)//id e kursit
        {
          string uid = User.Identity.GetUserId();
          if(!courseService.CanUserModifyCourseLevelsAndTypes(uid, id))
          {
            this.AddNotification("Ju nuk mund te shtoni nivele ne kursin me id:" + id.ToString(), NotificationType.ERROR);
            return RedirectToAction("CourseLevels", "Course", new { @id = id });
          }
          var course = courseService.GetCourseById(id);
          if (course == null)
            return new HttpNotFoundResult();
          var availableLevels = levelService.GetAvailableLevelsForCourse(id);
          ViewBag.CourseId = id;
          ViewBag.AvaliableLevels = new SelectList(availableLevels, "Id", "Emri");
          ViewBag.Course = course.Emri;
          TempData["Course"] = course.Emri;
          return View();
        }

        [HttpPost]
        public ActionResult AddLevelInCourse(Nivel selectedLevel, int courseId)
        {
          string uid = User.Identity.GetUserId();
          if (!courseService.CanUserModifyCourseLevelsAndTypes(uid, courseId)) 
          {
            this.AddNotification("Ju nuk mund te shtoni nivele ne kursin me id:" + courseId, NotificationType.ERROR);
            return RedirectToAction("CourseLevels", "Course", new { @id = courseId});
          }
          if (selectedLevel.Id == 0)
          {
            this.AddNotification("Opsioni qe ju zgjodhet nuk eshte i vlefshem.", NotificationType.ERROR);
            return RedirectToAction("AddLevelInCourse", "Level", new { @id = courseId });
          }
          var levelToAdd = levelService.GetLevelById(selectedLevel.Id);
          if (levelToAdd== null)
            return new HttpNotFoundResult();
          KursNivelTip newSection = new KursNivelTip()
          {
            KursiId = courseId,
            NiveliId = selectedLevel.Id
          };

          sectionService.CreateSection(newSection);
          this.AddNotification(levelToAdd.Emri + " u shtua me sukses ne kursin " + TempData["Course"], NotificationType.SUCCESS);
          return RedirectToAction("CourseLevels", "Course", new { @id = courseId });

        }

        [HttpPost]
        public ActionResult DeleteLevelInCourse(int levelId, int courseId)
        {
          string uid = User.Identity.GetUserId();
          if (!courseService.CanUserModifyCourseLevelsAndTypes(uid, courseId))
          {
            this.AddNotification("Ju nuk mund te fshini nivele ne kursin me id:" + courseId, NotificationType.ERROR);
            return RedirectToAction("CourseLevels", "Course", new { @id = courseId });
          }
          bool result = sectionService.DeleteSection(courseId, levelId, null);
          if (!result)
            return new HttpNotFoundResult();
          var level = levelService.GetLevelById(levelId);
          var course = courseService.GetCourseById(courseId);
          this.AddNotification("Niveli " + level.Emri + " u fshi nga kursi " + course.Emri, NotificationType.SUCCESS);
          return RedirectToAction("CourseLevels", "Course", new { @id = courseId });
        }
    }
}