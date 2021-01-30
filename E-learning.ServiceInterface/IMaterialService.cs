using E_learning.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.ServiceInterface
{
  public interface IMaterialService
  {
    Material GetMaterialById(int materialId);
    bool CreateMaterial(Material m);
    bool UpdateMaterial(Material m);
    bool DeleteMaterial(int materialId);
    List<Material> GetMaterialsOf(int sectionId);
  }
}
