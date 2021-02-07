using E_learning.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.ServiceInterface
{
  public interface ICommentsService
  {
    List<Koment> GetAllComments();
    void AddComment(Koment k);
    bool CanUserDeleteComment(string uid, int commentId);
    void DeleteComment(int id);
  }
}
