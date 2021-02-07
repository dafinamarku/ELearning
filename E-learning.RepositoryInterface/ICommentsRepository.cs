using E_learning.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.RepositoryInterface
{
  public interface ICommentsRepository
  {
    Koment GetCommentById(int id);
    List<Koment> GetAllComments();
    void AddComment(Koment comment);
    void DeleteComment(int id);
  }
}
