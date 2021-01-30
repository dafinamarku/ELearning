using E_learning.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.ServiceInterface
{
  public interface ILevelService
  {
    Nivel GetLevelById(int id);
  }
}
