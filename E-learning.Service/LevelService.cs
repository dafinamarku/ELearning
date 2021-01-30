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
  public class LevelService: ILevelService
  {
    ILevelRepository repository;

    public LevelService(ILevelRepository rep)
    {
      this.repository = rep;
    }

    public Nivel GetLevelById(int id)
    {
      return repository.GetLevelById(id);
    }
  }
}
