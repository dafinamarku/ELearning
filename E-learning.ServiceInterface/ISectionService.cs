using E_learning.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.ServiceInterface
{
  public interface ISectionService
  {
    KursNivelTip GetSectionById(int id);
    bool AddLevelInSection(int sectionId, int levelId);
    void CreateSection(KursNivelTip newSection);
    bool DeleteSection(int courseId, int levelId, int? typeId);
    bool AddTypeInCourseLevel(int courseId, int levelId, int typeId);
  }
}
