using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using DataSource.Models;

namespace firewood.Controllers
{
    [RoutePrefix("Act")]
    public class ActController : Controller
    {
        ActService actService = new ActService();
        OrgService orgService = new OrgService();
        JoinService joinService = new JoinService();
        CommentService commentService = new CommentService();

        [HttpGet]
        [Route("Index/ID/{id:guid}")]
        public ActionResult Index(Guid id)
        {
            Guid uid;
            if (Session["User"] != null) uid = new Guid(Session["User"].ToString());
            else return Redirect("~/User/PostLogin");

            //显示活动详情
            showAct(id);

            if (joinService.IsExist(id, uid)) ViewBag.Button = "您已参与";
            else ViewBag.Button = "我想参加";

            return View();
        }

        [HttpPost]
        [Route("Join/ID/{id:guid}")]
        public ActionResult Join(Guid id)
        {
            Guid uid;
            if (Session["User"] != null) uid = new Guid(Session["User"].ToString());
            else return Redirect("~/User/PostLogin");

            //显示活动详情
            showAct(id);

            if (joinService.IsExist(id, uid))
            {
                ViewBag.Button = "您已参与";
                return View("Index");
            }
            else ViewBag.Button = "我想参加";

            Join join = new Join();
            join.ActID = id;
            join.UserID = uid;
            if (!joinService.Create(join)) return Content("<script>alert('系统异常');</script>");
            return View("Index");
        }

        //留言
        [HttpPost]
        [Route("Comment/ID/{id:guid}")]
        public ActionResult Comment(Guid id, firewood.CommentModels model)
        {
            Guid uid;
            if (Session["User"] != null) uid = new Guid(Session["User"].ToString());
            else return Redirect("~/User/PostLogin");
            model.ActID = id;

            //显示活动详情
            Activity act = showAct(id);

            if (joinService.IsExist(model.ActID, uid)) ViewBag.Button = "您已参与";
            else ViewBag.Button = "我想参加";

            if (!ModelState.IsValid) return View("Index", model);

            Comment comment = new Comment();
            comment.ComID = Guid.NewGuid();
            comment.UserID = uid;
            comment.OrgID = act.OrgID;
            comment.ActID = model.ActID;
            comment.ComCon = model.ComCon;
            comment.ComTime = DateTime.Now;
            comment.Type = 1;
            if (!commentService.AddComment(comment)) return Content("<script>alert('系统异常');</script>");

            showComment(id);

            return View("Index");
        }

        private void showComment(Guid id)
        {
            List<CommentModels> viewComment = new List<CommentModels>();
            //查询最新的留言，并显示，默认显示最新的5条
            List<Comment> commentList = commentService.selectComment(id, 5);
            //根据查出的留言，判断用户是否已经参加了活动
            if (commentList.Count != 0)
            {
                for (int i = 0; i < commentList.Count; i++)
                {
                    CommentModels newCom = new CommentModels();
                    newCom.ActID = commentList[i].ActID;
                    newCom.ComCon = commentList[i].ComCon;
                    newCom.ComID = commentList[i].ComID;
                    newCom.hasJoin = joinService.IsExist(commentList[i].ActID, commentList[i].UserID);
                    //newCom.NickName = userService.getNickName(commentList[i].UserID);
                    //newCom.UserPic = userService.getUserPic(commentList[i].UserID);
                    viewComment.Add(newCom);
                }
                ViewData["commentList"] = viewComment;
            }
        }

        private Activity showAct(Guid id)
        {
            ViewBag.NickName = Session["NickName"].ToString();

            Activity act = actService.GetActByActID(id);

            ViewData["Act"] = act;
            ViewBag.OrgName = orgService.GetNameByID(act.OrgID);
            ViewBag.Type = actService.GetTypeByID(id);
            ViewBag.Sum = joinService.GetSumByActID(id);

            return act;
        }
    }
}