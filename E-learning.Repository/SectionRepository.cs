using E_learning.DataLayer;
using E_learning.DomainModels;
using E_learning.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Repository
{
  public class SectionRepository: ISectionRepository
  {
    ProjectDbContext db;

    public SectionRepository()
    {
      this.db = new ProjectDbContext();
    }

    public KursNivelTip GetSectionById(int sectionId)
    {
      return
        db.KursNivelTip.FirstOrDefault(x=>x.Id==sectionId);
    }

    public bool AddLevelInSection(int sectionId, int levelId)
    {
      var section = db.KursNivelTip.FirstOrDefault(x => x.Id == sectionId);
      var level = db.Nivelet.FirstOrDefault(x => x.Id == levelId);
      //shtimi i nivelit nuk behet nqs seksioni ose niveli me id-te perkatese nuk ekzistojne ose
      //seksioni tashme ka nje nivel
      if (section == null || level==null || section.NiveliId!=null)
        return false;
      section.NiveliId = levelId;
      db.SaveChanges();
      return true;
    }

    public void CreateSection(KursNivelTip newSection)
    {
      db.KursNivelTip.Add(newSection);
      db.SaveChanges();
    }

    public bool DeleteSection(int courseId, int levelId, int? typeId)
    {
      
      var course = db.Kurset.FirstOrDefault(x => x.KursId == courseId);
      var level = db.Nivelet.FirstOrDefault(x => x.Id == levelId);
      if (course == null || level == null)
        return false;
      //ne kete rast metoda thirret per te fshire nje nivel te kursit
      if (typeId == null) { 
        var sectionsToDelete = db.KursNivelTip.Where(x => x.KursiId == courseId && x.NiveliId == levelId).ToList();
        if (sectionsToDelete == null)
          return false;
        foreach (var section in sectionsToDelete)
        {
          var materialsToDelete = db.Materialet.Where(x => x.SeksioniId == section.Id);
          db.Materialet.RemoveRange(materialsToDelete);
        }
        db.KursNivelTip.RemoveRange(sectionsToDelete);
      }
      else //ne kete rast metoda thirret per te fshire nje tip ne nje nivel dhe kurs te caktuar
      {
        var type = db.Tipet.FirstOrDefault(x => x.Id == typeId);
        if (type == null)
          return false;
        var sectionToDelete = db.KursNivelTip.Where(x => x.KursiId == courseId && x.NiveliId == levelId && x.TipiId==typeId).FirstOrDefault();
        if (sectionToDelete == null)
          return false;
        var materialsToDelete = db.Materialet.Where(x => x.SeksioniId == sectionToDelete.Id);
        db.Materialet.RemoveRange(materialsToDelete);
        db.KursNivelTip.Remove(sectionToDelete);
      }
      db.SaveChanges();
      return true;
    }

    public bool AddTypeInCourseLevel(int courseId, int levelId, int typeId)
    {
      var course = db.Kurset.FirstOrDefault(x => x.KursId == courseId);
      var level = db.Nivelet.FirstOrDefault(x => x.Id == levelId);
      var type = db.Tipet.FirstOrDefault(x => x.Id == typeId);
      //ne nje seksion nuk mund te shtohen kurse,nivele apo tipe qe nuk ekzistojne
      if (level == null || course == null || type==null)
        return false;
      var sectionToUpdate = db.KursNivelTip.Where(x => x.KursiId == courseId && x.NiveliId == levelId && x.TipiId == null).FirstOrDefault();
      //ne rast se ka nje seksion me kurs dhe nivel perkates dhe tipId=null 
      //(rasti kur niveli i nje kursi nuk ka asnje tip)
      //modifikohet seksioni ekzistues ne te kundert behet shtimi i nje seksioni te ri
      if (sectionToUpdate != null)
      {
        sectionToUpdate.TipiId = typeId;
      }
      else
      {
        KursNivelTip newSection = new KursNivelTip()
        {
          KursiId = courseId,
          NiveliId = levelId,
          TipiId = typeId
        };
        db.KursNivelTip.Add(newSection);
      }
      db.SaveChanges();
      return true;
    }
  }
}
