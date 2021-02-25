using E_learning.DomainModels;
using E_learning.RepositoryInterface;
using E_learning.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Repository
{
  public class SectionService:ISectionService
  {
    ISectionRepository sectionRepository;

    public SectionService(ISectionRepository rep)
    {
      this.sectionRepository = rep;
    }

    public KursNivelTip GetSectionById(int id)
    {
      return sectionRepository.GetSectionById(id);
    }

    public bool AddLevelInSection(int sectionId, int levelId)
    {
      return sectionRepository.AddLevelInSection(sectionId, levelId);
    }

    public void CreateSection(KursNivelTip newSection)
    {
      sectionRepository.CreateSection(newSection);
    }

    public bool DeleteSection(int courseId, int levelId, int? typeId)
    {
      return sectionRepository.DeleteSection(courseId, levelId, typeId);
    }

    public bool AddTypeInCourseLevel(int courseId, int levelId, int typeId)
    {
      return sectionRepository.AddTypeInCourseLevel(courseId, levelId, typeId);
    }
  }
}
