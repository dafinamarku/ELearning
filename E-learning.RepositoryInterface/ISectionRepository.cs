using E_learning.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.RepositoryInterface
{
  public interface ISectionRepository
  {
    KursNivelTip GetSectionById(int id);
    void CreateSection(KursNivelTip newSection);
    bool AddLevelInSection(int sectionId, int levelId);
    bool DeleteSection(int courseId, int levelId, int? typeId);
    bool AddTypeInCourseLevel(int courseId, int levelId, int typeId);
  }


}
