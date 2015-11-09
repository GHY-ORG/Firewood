using System;
using System.ComponentModel.Composition;
using System.Web.Mvc;
using Hub.Interface.User;
using BLL;
using System.Web;

namespace firewood.Controllers
{
    [RoutePrefix("Home")]
    public class HomeController : Controller
    {

        public IAccountStrategy AccountStrategy
        {
            get
            {
                return SiteConfig.Container.GetExportedValueOrDefault<IAccountStrategy>();
            }
        }
        public IAuthorizeStrategy AuthorizeStrategy
        {
            get
            {
                return SiteConfig.Container.GetExportedValueOrDefault<IAuthorizeStrategy>();
            }
        }

        ActService actService = new ActService();
        OrgService orgService = new OrgService();

        [HttpGet]
        public ActionResult Index()
        {
            validUser();

            //显示右边的东西
            ViewData["TopActList"] = actService.GetTopActList();
            ViewData["OrgList"] = orgService.ShowAllOrg(9, 1);

            //显示活动（标题、简介、社团组织logo）
            ViewData["ActList"] = actService.ShowIndexAct(8, 1);
            ViewData["TopActList"] = actService.GetTopActList();
            ViewData["OrgList"] = orgService.ShowAllOrg(100, 1);
            return View();
        }

        [HttpGet]
        [Route("Index/Type/{type:int}/Class/{classname}")]
        public ActionResult Index(int type, string classname)
        {
            validUser();

            //显示右边的东西
            ViewData["TopActList"] = actService.GetTopActList();
            ViewData["OrgList"] = orgService.ShowAllOrg(9, 1);

            //显示活动（标题、简介、社团组织logo）
            ViewData["ActList"] = actService.GetActByClass(type, classname, 8, 1);

            return View();
        }

        [HttpGet]
        [Route("Index/Place/{place}")]
        public ActionResult Index(string place)
        {
            validUser();

            //显示右边的东西
            ViewData["TopActList"] = actService.GetTopActList();
            ViewData["OrgList"] = orgService.ShowAllOrg(9, 1);

            //显示活动（标题、简介、社团组织logo）
            ViewData["ActList"] = actService.GetActByPlace(place, 8, 1);

            return View();
        }

        [HttpGet]
        [Route("Index/Type/{type:int}")]
        public ActionResult Index(int type)
        {
            validUser();

            //显示右边的东西
            ViewData["TopActList"] = actService.GetTopActList();
            ViewData["OrgList"] = orgService.ShowAllOrg(9, 1);

            //显示活动（标题、简介、社团组织logo）
            if (type == 1) ViewData["ActList"] = actService.GetActByMoney(8, 1);
            else if (type == 2) ViewData["ActList"] = actService.GetActByScore(8, 1);
            else if (type == 3) ViewData["ActList"] = actService.GetActByAward(8, 1);

            return View();
        }

        [HttpPost]
        public ActionResult Search(string title)
        {
            validUser();

            //显示右边的东西
            ViewData["TopActList"] = actService.GetTopActList();
            ViewData["OrgList"] = orgService.ShowAllOrg(9, 1);

            //显示活动（标题、简介、社团组织logo）
            ViewData["ActList"] = actService.GetActByTitle(title, 8, 1);

            return View("Index");
        }

        private void validUser()
        {
            //判断用户
            if (Session["User"] == null)
            {
                ViewBag.NickName = null;
                ViewBag.RoleList = null;
            }
            else
            {
                Guid userid = new Guid(Session["User"].ToString());
                ViewBag.UserID = userid;
                ViewBag.NickName = AccountStrategy.GetNickNameByUserID(userid);
                Session["NickName"] = ViewBag.NickName;

                //判断权限
                ViewBag.RoleList = AuthorizeStrategy.GetRole(userid);
                Session["RoleList"] = ViewBag.RoleList;
            }
        }
    }
}