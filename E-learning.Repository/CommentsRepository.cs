using E_learning.DataLayer;
using E_learning.DomainModels;
using E_learning.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learning.Repository
{
  public class CommentsRepository:ICommentsRepository
  {
    ProjectDbContext db;
    public CommentsRepository()
    {
      this.db = new ProjectDbContext();
    }

    public List<Koment> GetAllComments()
    {
      return db.Komentet.ToList();
    }

    public void AddComment(Koment comment)
    {
      db.Komentet.Add(comment);
      db.SaveChanges();
    }

    public Koment GetCommentById(int id)
    {
      return db.Komentet.FirstOrDefault(x => x.Id == id);
    }

    public void DeleteComment(int id)
    {
      var comment = db.Komentet.FirstOrDefault(x => x.Id == id);
      if (comment != null)
      {
        db.Komentet.RemoveRange(comment.Pergjigjet);
        db.Komentet.Remove(comment);
        db.SaveChanges();
      }
       
    }
  }
}
