using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Hub.Interface.User;

namespace firewood.Controllers
{
    [RoutePrefix("User")]
    public class UserController : Controller
    {
        ActService actService = new ActService();

        public IAuthenticationStrategy AuthentiationStrategy
        {
            get
            {
                return SiteConfig.Container.GetExportedValueOrDefault<IAuthenticationStrategy>();
            }
        }

        #region 登录
        [Route("PostLogin")]
        public ActionResult PostLogin()
        {
            //HttpCookie cookie = Request.Cookies["ghy_sso_token"];
            //if (cookie != null)
            //{
            //    string token = cookie.Value;
            //    Hub.Models.Token token_record = AuthentiationStrategy.GetSessionByToken(token, AuthentiationStrategy.CreateCheckCode(Request.UserAgent, Request.UserHostAddress));
            //    if (token_record != null)
            //    {
            //        Session["User"] = token_record.UserID;
            //        return Redirect("~/Home/Index");
            //    }
            //}
            //return Redirect("http://ghy.swufe.edu.cn/ghy_sso/user/login?ReturnUrl=" + SiteConfig.SiteUrl + "/user/login");


            Session["User"] = "4c36a36d-b58a-417e-b62b-dd72fad39371";
            return Redirect(SiteConfig.SiteUrl);
        }

        [Route("Login")]
        public ActionResult Login()
        {
            string token = Request.QueryString["token"];
            if (token != null)
            {
                HttpCookie cookie = new HttpCookie("ghy_sso_token", token);
                cookie.HttpOnly = true;
                cookie.Expires = DateTime.Now + TimeSpan.FromDays(30);
                Response.SetCookie(cookie);
            }

            Hub.Models.Token token_record = AuthentiationStrategy.GetSessionByToken(token, AuthentiationStrategy.CreateCheckCode(Request.UserAgent, Request.UserHostAddress));

            if (token_record != null)
            {
                Session["User"] = token_record.UserID;
                return Redirect("~/Home/Index");
            }
            return Redirect("~/User/PostLogin");
        }
        #endregion

        #region 退出
        [Route("Exit")]
        public ActionResult Exit()
        {
            if (Session["User"] != null)
            {
                Session.Clear();
            }
            if (Request.Cookies["ghy_sso_token"] != null)
            {
                if (!AuthentiationStrategy.DelToken(Request.Cookies["ghy_sso_token"].Value))
                {
                    return Content("<script>alert('token更新异常')</script>");
                }
                HttpCookie myCookie = new HttpCookie("ghy_sso_token");
                myCookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(myCookie);
            }
            return Redirect("~/Home/Index");
        }
        #endregion

        #region 注册
        [Route("Register")]
        public ActionResult Register()
        {
            return Redirect("http://ghy.swufe.edu.cn/ghy_sso/user/register");
        }
        #endregion

        #region 个人中心
        [HttpGet]
        [Route("Center")]
        public ActionResult Center()
        {
            if (Session["User"] == null) return Redirect("~/Home/Index");
            ViewData["ActList"] = actService.GetActByUserID(new Guid(Session["User"].ToString()));
            ViewBag.NickName = Session["NickName"].ToString();
            return View();
        }
        #endregion
    }
}