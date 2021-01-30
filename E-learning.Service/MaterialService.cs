using E_learning.DomainModels;
using E_learning.RepositoryInterface;
using E_learning.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Service
{
  public class MaterialService:IMaterialService
  {
    IMaterialRepository repository;

    public MaterialService(IMaterialRepository rep)
    {
      this.repository = rep;
    }

    public Material GetMaterialById(int materialId)
    {
      return repository.GetMaterialById(materialId);
    }


    public bool CreateMaterial(Material m)
    {
      return repository.CreateMaterial(m);
    }

    public bool UpdateMaterial(Material m)
    {
      return repository.UpdateMaterial(m);
    }

    public bool DeleteMaterial(int materialId)
    {
      return repository.DeleteMaterial(materialId);
    }

    public List<Material> GetMaterialsOf(int sectionId)
    {
      //materialet e seksionit pasi merren nga shtresa e repository renditen ne rendin rrites sipas id
      //pra te parat do te shfaqen materialet e postuar me heret 
      return repository.GetMaterialsOf(sectionId).OrderBy(x => x.Id).ToList();
    }
  }
}
