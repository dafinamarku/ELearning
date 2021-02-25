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
  public class TypeRepository:ITypeRepository
  {
    ProjectDbContext db;

    public TypeRepository()
    {
      this.db = new ProjectDbContext();
    }

    public Tip GetTypeById(int id)
    {
      return db.Tipet.FirstOrDefault(x => x.Id == id);
    }
    public List<Tip> GetAvailableTypesForCourseLevel(int courseId, int levelId)
    {
      var currentTypes= db.KursNivelTip.Where(x => x.KursiId == courseId && x.NiveliId == levelId && x.TipiId!=null)
                           .Select(x => x.Tipi).Distinct().ToList();
      List<Tip> availableTypes = new List<Tip>();
      foreach(var type in db.Tipet.ToList())
      {
        if (!currentTypes.Contains(type))
          availableTypes.Add(type);
      }
      return availableTypes;
    }
  }
}
