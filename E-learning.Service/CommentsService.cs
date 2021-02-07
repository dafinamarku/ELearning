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
  public class CommentsService:ICommentsService
  {
    ICommentsRepository repository;

    public CommentsService(ICommentsRepository rep)
    {
      this.repository = rep;
    }

    public List<Koment> GetAllComments()
    {
      //komentet renditen ne rendin zbrites sipas dates se krijimit
      return repository.GetAllComments().OrderBy(x=>x.DataEKrijimit).ToList();
    }

    public void AddComment(Koment k)
    {
      repository.AddComment(k);
    }

    //nje user mund te fshije nje koment vetem nqs ai ekziston dhe useri eshte autori i atij komenti
    public bool CanUserDeleteComment(string uid, int commentId)
    {
      var comment = repository.GetCommentById(commentId);
      if (comment == null|| comment.AutoriId != uid)
        return false;
      return true;
    }

    public void DeleteComment(int id)
    {
      repository.DeleteComment(id);
    }
  }
}
