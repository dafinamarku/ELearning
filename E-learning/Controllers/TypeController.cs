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
    [Authorize(Roles ="Instruktor")]
    public class TypeController : Controller
    {
        ITypeService typeService;
        ILevelService levelService;
        ICourseService courseService;
        ISectionService sectionService;

        public TypeController(ITypeService tserv, ILevelService lserv, ICourseService cserv, ISectionService sserv)
        {
          this.typeService = tserv;
          this.levelService = lserv;
          this.courseService = cserv;
          this.sectionService = sserv;
        }

        public ActionResult AddTypeInCourseLevel(int courseId, int levelId)
        {
          string uid = User.Identity.GetUserId();
          if (!courseService.CanUserModifyCourseLevelsAndTypes(uid, courseId))
          {
            this.AddNotification("Ju nuk mund te shtoni tipe ne kursin me id:" + courseId, NotificationType.ERROR);
            return RedirectToAction("CourseLevelTypes", "Course", new { @courseId = courseId, @levelId=levelId});
          }
          var course = courseService.GetCourseById(courseId);
          var level = levelService.GetLevelById(levelId);
          if (level == null || course == null)
            return new HttpNotFoundResult();
          var availableTypes = typeService.GetAvailableTypesForCourseLevel(courseId, levelId);
          ViewBag.CourseId = courseId;
          ViewBag.AvaliableTypes = new SelectList(availableTypes, "Id", "Tipi");
          ViewBag.Course = course.Emri;
          ViewBag.Level = level.Emri;
          ViewBag.LevelId = levelId;
          return View();
        }

        [HttpPost]
        public ActionResult AddTypeInCourseLevel(Tip selectedtype, int courseId, int levelId)
        {
          string uid = User.Identity.GetUserId();
          if (!courseService.CanUserModifyCourseLevelsAndTypes(uid, courseId))
          {
            this.AddNotification("Ju nuk mund te shtoni tipe ne kursin me id:" + courseId, NotificationType.ERROR);
            return RedirectToAction("CourseLevelTypes", "Course", new { @courseId = courseId, @levelId = levelId });
          }
          if (selectedtype.Id == 0)
          {
            this.AddNotification("Opsioni qe ju zgjodhet nuk eshte i vlefshem.", NotificationType.ERROR);
            return RedirectToAction("AddTypeInCourseLevel", new { @courseId = courseId, @levelId = levelId });
          }
          bool wasAdded = sectionService.AddTypeInCourseLevel(courseId, levelId, selectedtype.Id);
          var type = typeService.GetTypeById(selectedtype.Id);
          var level = levelService.GetLevelById(levelId);
          if (wasAdded)
            this.AddNotification("Tipi " + type.Tipi + " u shtua ne ne nivelin " + level.Emri, NotificationType.SUCCESS);
          else
            this.AddNotification("Tipi " + type.Tipi + " nuk u shtua ne ne nivelin " + level.Emri, NotificationType.ERROR);
          return RedirectToAction("CourseLevelTypes", "Course", new { @courseId = courseId, @levelId = levelId });
        }

        [HttpPost]
        public ActionResult DeleteTypeInCourseLevel(int courseId, int levelId, int typeId)
        {
          string uid = User.Identity.GetUserId();
          if (!courseService.CanUserModifyCourseLevelsAndTypes(uid, courseId))
          {
            this.AddNotification("Ju nuk mund te fshini tipe ne kursin me id:" + courseId, NotificationType.ERROR);
            return RedirectToAction("CourseLevelTypes", "Course", new { @courseId = courseId, @levelId=levelId });
          }
          bool result = sectionService.DeleteSection(courseId, levelId, typeId);
          if (!result)
            return new HttpNotFoundResult();
          var level = levelService.GetLevelById(levelId);
          var type = typeService.GetTypeById(typeId);
          this.AddNotification("Tipi " + type.Tipi + " u fshi nga niveli " + level.Emri, NotificationType.SUCCESS);
          return RedirectToAction("CourseLevelTypes", "Course", new { @courseId = courseId, @levelId=levelId });
        }
  }
}