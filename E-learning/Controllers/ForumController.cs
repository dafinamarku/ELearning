using E_learning.DomainModels;
using E_learning.Extensions;
using E_learning.ServiceInterface;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_learning.Controllers
{
    [Authorize(Roles ="Student, Instruktor")]
    public class ForumController : Controller
    {

        ICommentsService commentService;
        public ForumController(ICommentsService serv)
        {
          this.commentService=serv;
        }

        // GET: Forum
        public ActionResult Index()
        {
            var model = commentService.GetAllComments();
            ViewBag.UserId = User.Identity.GetUserId();
            return View(model);
        }

        [HttpPost]
        public ActionResult PostComment(string CommentText)
        {
          string userId = User.Identity.GetUserId();
          //Komenti eshte bosh
          if (string.IsNullOrEmpty(CommentText))
          {
            return RedirectToAction("Index");
          }
          //Shtimi komentit
          Koment c = new Koment()
          {
            Teksti = CommentText,
            DataEKrijimit = DateTime.Now,
            AutoriId = userId
          };
          commentService.AddComment(c);
          this.AddNotification("Komenti u postua.", NotificationType.SUCCESS);
          return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult PostReply(string Replytext, int CID)
        {
          string userId = User.Identity.GetUserId();
          //Textbox i reply eshte bosh
          if (string.IsNullOrEmpty(Replytext))
          {
            return RedirectToAction("Index");
          }
          //Shtimi i reply
          Koment reply = new Koment()
          {
            Teksti = Replytext,
            AutoriId = userId,
            DataEKrijimit = DateTime.Now,
            KomentId = CID
          };
          commentService.AddComment(reply);
          this.AddNotification("Pergjigja u shtua.", NotificationType.SUCCESS);
          return RedirectToAction("Index");
        }

        public ActionResult DeleteComment(int id)
        {
          string uid = User.Identity.GetUserId();
          bool can=commentService.CanUserDeleteComment(uid, id);
          if (can)
          {
            commentService.DeleteComment(id);
            this.AddNotification("Komenti u fshi.", NotificationType.SUCCESS);
          }
          else
            this.AddNotification("Ju nuk keni te drejte te fshini komentin.", NotificationType.ERROR);
          return RedirectToAction("Index");
        }

  }
}