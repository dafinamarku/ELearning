using E_learning.DomainModels;
using E_learning.Extensions;
using E_learning.ServiceInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_learning.Controllers
{
    public class MaterialController : Controller
    {
        IMaterialService mService;
        ISectionService sectionService;

        public MaterialController(IMaterialService mser, ISectionService ser, ILevelService levserv)
        {
          this.mService = mser;
          this.sectionService = ser;
        }

        public ActionResult MaterialsOfSection(int id)
        {
            KursNivelTip currentSection = sectionService.GetSectionById(id);
            if (currentSection == null)
              return new HttpNotFoundResult();
            List<Material> materials = mService.GetMaterialsOf(id);
            ViewBag.Kursi = currentSection.Kursi.Emri;
            ViewBag.KursId = currentSection.Kursi.KursId;
            ViewBag.Niveli = currentSection.Niveli.Emri;
            ViewBag.NivelId = currentSection.Niveli.Id;
            ViewBag.Tipi = currentSection.Tipi.Tipi;
            ViewBag.SeksionId = currentSection.Id;
            return View(materials);
        }

        private string ValidateFile(HttpPostedFileBase file, int seksionId)
        {
            string materialType = sectionService.GetSectionById(seksionId).Tipi.Tipi;
            string fileExtension = Path.GetExtension(file.FileName).ToLower();
            string[] allowedTextFiles= { ".doc", ".pdf" };
            string[] allowedVideoFiles = { ".mp4", ".mov", ".wmv", ".flv" };
            string[] allowedAudioFiles = { ".mp3", ".wav" };
            if (materialType == "Video")
            {
              if ((file.ContentLength > 0 && file.ContentLength < 30000000) &&
                allowedVideoFiles.Contains(fileExtension))
              {
                return string.Empty;
              }
              return "Materiali duhet te jete video.";
            }
            else
            {
              if (materialType == "Audio")
              {
                if ((file.ContentLength > 0 && file.ContentLength < 30000000) &&
                allowedAudioFiles.Contains(fileExtension))
                {
                  return string.Empty;
                }
                return "Materiali duhet te jete audio.";
              }
              else //text
              {
                if ((file.ContentLength > 0 && file.ContentLength < 30000000) &&
                allowedTextFiles.Contains(fileExtension))
                {
                  return string.Empty;
                }
                return "Materiali duhet te jete tekst.";
              }
            }
        }

        private void RuajFile(HttpPostedFileBase file, int seksionId)
        {
          string materialType = sectionService.GetSectionById(seksionId).Tipi.Tipi;
          var fileName = Path.GetFileName(file.FileName);
          string path;
          if (file.ContentLength > 0)
          {
            if (materialType == "Audio")
            {
              path = Path.Combine(Server.MapPath(MaterialPaths.AudioPath), fileName);
            }
            else
            {
              if (materialType == "Video")
              {
                path = Path.Combine(Server.MapPath(MaterialPaths.VideosPath), fileName);
              }
              else //materialType="Text"
              {
                path = Path.Combine(Server.MapPath(MaterialPaths.TextPath), fileName);
              }
            }
            file.SaveAs(path);
          }
      
        }

    public ActionResult AddMaterialInSection(int id)// id e seksionit
    {
      var section = sectionService.GetSectionById(id);
      if (section == null)
        return HttpNotFound();
      ViewBag.SeksionId = section.Id;
      ViewBag.Kursi = section.Kursi.Emri;
      ViewBag.KursId = section.Kursi.KursId;
      ViewBag.Niveli = section.Niveli.Emri;
      ViewBag.NivelId = section.Niveli.Id;
      ViewBag.Tipi = section.Tipi.Tipi;
      Material model = new Material()
      {
        SeksioniId = section.Id
      };
      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult AddMaterialInSection(HttpPostedFileBase file, Material model)
    {
      if (file != null)
      {
        string error = ValidateFile(file, model.SeksioniId);
        if (!string.IsNullOrEmpty(error))
        {
          ModelState.AddModelError("err", error);
        }
      }
      else
      {
        ModelState.AddModelError("err", "Zgjidhni nje file.");
      }
      if (ModelState.IsValid)
      {
        try
        {
          RuajFile(file, model.SeksioniId);
        }
        catch (Exception)
        {
          ModelState.AddModelError("err", "Nuk u ruajt file ne disk, provo perseri");
        }
        Material newMaterial = new Material()
        {
          Titulli = model.Titulli,
          Pershkrimi = model.Pershkrimi,
          Filename = Path.GetFileName(file.FileName),
          SeksioniId = model.SeksioniId
        };
        if (mService.CreateMaterial(newMaterial))
          return RedirectToAction("MaterialsOfSection", new { id = model.SeksioniId });
        else
          ModelState.AddModelError("err", "Nje material me te njetin titull tashme ekziston. Julutem zgjidhni nje titull tjeter per materialin tuaj.");
      }
      var section = sectionService.GetSectionById(model.SeksioniId);
      ViewBag.SeksionId = section.Id;
      ViewBag.Kursi = section.Kursi.Emri;
      ViewBag.KursId = section.Kursi.KursId;
      ViewBag.Niveli = section.Niveli.Emri;
      ViewBag.NivelId = section.Niveli.Id;
      ViewBag.Tipi = section.Tipi.Tipi;
      return View(model);
    }


    public ActionResult EditMaterial(int id)
    {
      var model=mService.GetMaterialById(id);
      ViewBag.SeksionId = model.SeksioniId;
      ViewBag.Kursi = model.Seksioni.Kursi.Emri;
      ViewBag.KursId = model.Seksioni.Kursi.KursId;
      ViewBag.Niveli = model.Seksioni.Niveli.Emri;
      ViewBag.NivelId = model.Seksioni.Niveli.Id;
      ViewBag.Tipi = model.Seksioni.Tipi.Tipi;
      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult EditMaterial(HttpPostedFileBase file, Material model)
    {
      if (file != null)
      {
        string error = ValidateFile(file, model.SeksioniId);
        if (!string.IsNullOrEmpty(error))
        {
          ModelState.AddModelError("err", error);
        }
      }
      //ne kete rast nuk eshte perzgjedhur nje file dhe si i material do te mbetet ai ekzistues
      if (ModelState.IsValid)
      {
        if (file != null)
        {
          try
          {
            RuajFile(file, model.SeksioniId);
          }
          catch (Exception)
          {
            ModelState.AddModelError("err", "Nuk u ruajt file ne disk, provo perseri");
          }
        }
        bool result = mService.UpdateMaterial(model);
        if (!result)
          ModelState.AddModelError("err", "Nje material me te njetin titull tashme ekziston. Julutem zgjidhni nje titull tjeter per materialin tuaj. ");
        else
        {
          this.AddNotification("Materiali u editua me sukses.", NotificationType.SUCCESS);
          return RedirectToAction("MaterialsOfSection", new { id = model.SeksioniId });
        }
      }
      ViewBag.SeksionId = model.SeksioniId;
      ViewBag.Kursi = model.Seksioni.Kursi.Emri;
      ViewBag.KursId = model.Seksioni.Kursi.KursId;
      ViewBag.Niveli = model.Seksioni.Niveli.Emri;
      ViewBag.NivelId = model.Seksioni.Niveli.Id;
      ViewBag.Tipi = model.Seksioni.Tipi.Tipi;
      return View(model);
    }
  }
}