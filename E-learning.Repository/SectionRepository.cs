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
  }
}
