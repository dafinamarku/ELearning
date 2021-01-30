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
  public class MaterialRepository:IMaterialRepository
  {
    ProjectDbContext db;

    public MaterialRepository()
    {
      this.db = new ProjectDbContext();
    }
    
    public Material GetMaterialById(int materialId)
    {
      return db.Materialet.FirstOrDefault(x => x.Id == materialId);
    }

    public bool CreateMaterial(Material m)
    {
      //nuk lejohet qe te kete materiale me titull te njejte ne nivel tabele
      var countSameTitleMaterials = db.Materialet.Where(x => x.Titulli == m.Titulli).Count();
      if (countSameTitleMaterials > 0)
        return false;
      db.Materialet.Add(m);
      db.SaveChanges();
      return true;
    }

    public bool UpdateMaterial(Material m)
    {
      //nuk lejohet qe te kete materiale me titull te njejte ne nivel tabele
      var countSameTitleMaterials = db.Materialet.Where(x => x.Titulli == m.Titulli && x.Id!=m.Id).Count();
      if (countSameTitleMaterials > 0)
        return false;
      var currentMaterial = db.Materialet.FirstOrDefault(x => x.Id == m.Id);
      currentMaterial.Titulli = m.Titulli;
      currentMaterial.Pershkrimi = m.Pershkrimi;
      //nqs nuk eshte ngarkuar nje file i ri ateher emri i file-it nuk do te ndryshoje
      if(!string.IsNullOrEmpty(m.Filename))
        currentMaterial.Filename = m.Filename;
      currentMaterial.Seksioni = m.Seksioni;
      db.SaveChanges();
      return true;
    }

    public bool DeleteMaterial(int materialId)
    {
      var currentMaterial=db.Materialet.FirstOrDefault(x=>x.Id==materialId);
      if (currentMaterial == null)
        return false;
      db.Materialet.Remove(currentMaterial);
      db.SaveChanges();
      return true;
    }

    public List<Material> GetMaterialsOf(int sectionId)
    {
      return db.Materialet.Where(x => x.Seksioni.Id == sectionId).ToList();
    }


  }
}
