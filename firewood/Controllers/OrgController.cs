using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using BLL;
using DataSource.Models;
using System.ComponentModel.Composition;
using Hub.Interface.User;

namespace firewood.Controllers
{
    [RoutePrefix("Org")]
    public class OrgController : Controller
    {
        public IAuthorizeStrategy AuthorizeStrategy
        {
            get
            {
                return SiteConfig.Container.GetExportedValueOrDefault<IAuthorizeStrategy>();
            }
        }

        OrgService orgService = new OrgService();
        ActService actService = new ActService();

        #region 视图显示
        [HttpGet]
        [Route("Index")]
        public ActionResult Index()
        {
            ViewBag.NickName = Session["NickName"].ToString();
            int[] roleList = Session["RoleList"] as int[];
            if (roleList == null) return Redirect("~/Home/Index");
            ViewData["OrgList"] = orgService.GetOrgList(AuthorizeStrategy.GetRoleName(roleList));
            return View();
        }

        [HttpGet]
        [Route("ShowAct/ID/{id:guid}/Page/{page:int}")]
        public ActionResult ShowAct(Guid id, int page)
        {
            ViewBag.NickName = Session["NickName"].ToString();
            if (Session["RoleList"] == null) return Redirect("~/Home/Index");
            ViewBag.OrgID = id;
            ViewData["ActList"] = actService.GetActListByOrgID(id, 8, page);
            ViewBag.Count = actService.GetActCountByOrgID(id);
            return View();
        }

        [HttpGet]
        [Route("Register")]
        public ActionResult Register()
        {
            ViewBag.NickName = Session["NickName"].ToString();
            int[] roleList = Session["RoleList"] as int[];
            if (roleList == null || roleList.Except(new int[1] { 1 }).Count() != 0) return Redirect("~/Home/Index");
            return View();
        }

        [HttpGet]
        [Route("Search")]
        public ActionResult Search()
        {
            ViewBag.NickName = Session["NickName"].ToString();
            int[] roleList = Session["RoleList"] as int[];
            if (roleList == null || roleList.Except(new int[1] { 1 }).Count() != 0) return Redirect("~/Home/Index");
            ViewBag.Count = orgService.GetOrgCount();
            ViewData["OrgList"] = orgService.ShowAllOrg(10, 1);
            return View();
        }

        [HttpGet]
        [Route("Search/Page/{page:int}")]
        public ActionResult Search(int page)
        {
            ViewBag.NickName = Session["NickName"].ToString();
            int[] roleList = Session["RoleList"] as int[];
            if (roleList == null || roleList.Except(new int[1] { 1 }).Count() != 0) return Redirect("~/Home/Index");
            ViewBag.Count = orgService.GetOrgCount();
            ViewData["OrgList"] = orgService.ShowAllOrg(10, page);
            return View();
        }

        [HttpGet]
        [Route("Update/OrgID/{id:guid}")]
        public ActionResult Update(Guid id)
        {
            ViewBag.NickName = Session["NickName"].ToString();
            int[] roleList = Session["RoleList"] as int[];
            if (roleList == null || roleList.Except(new int[1] { 1 }).Count() != 0) return Redirect("~/Home/Index");

            Session["OrgID"] = id;
            Org org = orgService.GetOrgByID(id);
            ViewBag.OrgID = id;
            ViewBag.OrgName = org.OrgName;
            ViewBag.OrgDepartment = org.OrgDepartment;
            ViewBag.OrgContact = org.OrgContact;
            ViewBag.OrgIntro = org.OrgIntroduction;
            return View();
        }

        [HttpGet]
        [Route("Publish/ID/{id:guid}")]
        public ActionResult Publish(Guid id)
        {
            ViewBag.NickName = Session["NickName"].ToString();
            Session["OrgID"] = id;
            return View();
        }
        #endregion

        #region 表单提交

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostRegister(RegisterModels model, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View("Register", model);
            }
            if (orgService.OrgNameRegistered(model.OrgName))
            {
                ModelState.AddModelError("OrgName", "用户名已被注册咯~");
                return View("Register", model);
            }

            Org org = new Org();
            org.OrgID = Guid.NewGuid();
            org.OrgName = model.OrgName;
            org.OrgContact = model.OrgContact;
            org.OrgDepartment = model.OrgDepartment;
            org.OrgIntroduction = model.OrgIntro;

            //图片不超过5M
            if (file != null && file.ContentLength < 1024 * 1024 * 5)
            {
                string path = GetPath(org.OrgID.ToString(), "Org") + DateTime.Now.Ticks + ".png";
                string absolutePath = SiteConfig.SitePath;
                string pathWithFileName = absolutePath + path;
                using (var stream = file.InputStream)
                {
                    Image img = Image.FromStream(stream);
                    var bmp = ResizeImg(img);
                    if (!System.IO.Directory.Exists(absolutePath))
                        System.IO.Directory.CreateDirectory(absolutePath);
                    bmp.Save(pathWithFileName, ImageFormat.Png);
                }
                org.OrgPic = path;

                if (orgService.Register(org))
                {
                    Session["Org"] = org;
                    return Content("<script>alert('注册成功~');window.location.href='Search'</script>");
                }
                else
                {
                    ModelState.AddModelError("", "注册异常");
                    return View("Register", model);
                }
            }
            else
            {
                ModelState.AddModelError("", "请上传规定大小的图片");
                return View("Register", model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostUpdate(UpdateModels model)
        {
            Guid OrgID = new Guid(Session["OrgID"].ToString());
            Org org = orgService.GetOrgByID(OrgID);
            ViewBag.OrgName = org.OrgName;
            ViewBag.OrgDepartment = org.OrgDepartment;
            ViewBag.OrgContact = org.OrgContact;
            ViewBag.OrgIntro = org.OrgIntroduction;

            if (!ModelState.IsValid)
            {
                return Redirect("http://www.ghy.cn");
            }
            org.OrgID = OrgID;
            org.OrgContact = model.OrgContact;
            org.OrgDepartment = model.OrgDepartment;
            org.OrgIntroduction = model.OrgIntro;

            if (orgService.UpdateInfo(org))
            {
                Session["Org"] = org;
                return Content("<script>alert('修改信息成功~');window.location.href='Search'</script>");
            }
            else
            {
                ModelState.AddModelError("", "修改异常");
                return View("Update", model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostPublish(PublishModels model, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid) return View("Publish", model);

            Activity act = new Activity();
            act.ActID = Guid.NewGuid();
            act.OrgID = new Guid(Session["OrgID"].ToString());
            act.ActName = model.ActName;
            act.Place = model.Place;
            act.Class1 = model.Class1;
            act.Class2 = model.Class2;
            act.BeginTime = model.BeginTime;
            act.EndTime = model.EndTime;
            act.ActIntro = model.ActIntro;
            act.MoneyState = model.Money.Equals("yes") ? 1 : 0;
            act.ScoreState = model.Score.Equals("yes") ? 1 : 0;
            act.AwardState = model.Award.Equals("yes") ? 1 : 0;
            act.VoteState = model.Vote.Equals("yes") ? 1 : 0;

            //图片不超过20M
            if (file != null && file.ContentLength < 1024 * 1024 * 20)
            {

                string path = GetPath(act.ActID.ToString(), "Act") + DateTime.Now.Ticks + ".png";
                string absolutePath = SiteConfig.SitePath;
                string pathWithFileName = absolutePath + path;
                using (var stream = file.InputStream)
                {
                    Image img = Image.FromStream(stream);
                    var bmp = ResizeImg(img);
                    if (!System.IO.Directory.Exists(absolutePath))
                        System.IO.Directory.CreateDirectory(absolutePath);
                    bmp.Save(pathWithFileName, ImageFormat.Png);
                }
                act.ActPic = path;

                if (actService.Publish(act))
                {
                    return Content("<script>alert('发布成功~');window.location.href='Index'</script>");
                }
                else
                {
                    ModelState.AddModelError("", "注册异常");
                    return View("Publish", model);
                }
            }
            else
            {
                ModelState.AddModelError("", "请上传规定大小的图片");
                return View("Publish", model);
            }
        }

        [HttpGet]
        [Route("Del/OrgID/{id:guid}")]
        public ActionResult Del(Guid id)
        {
            if (orgService.Delete(id))
            {
                return Redirect("~/Org/Search");
            }
            else return Content("<script>alert('注销异常~');</script>");
        }

        #endregion

        #region 私有成员
        private Bitmap ResizeImg(Image input)
        {
            if (input.Width > 1600 || input.Height > 1600)
            {
                if (input.Width > input.Height)
                {
                    return new Bitmap(input, 1600, input.Height * 1600 / input.Width);
                }
                else
                {
                    return new Bitmap(input, input.Width * 1600 / input.Height, 1600);
                }
            }
            return new Bitmap(input);
        }
        private string GetPath(string name, string foldername)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("FirewoodImages\\");
            sb.Append(foldername);
            sb.Append("\\");
            sb.Append(name);
            sb.Append("\\");
            return sb.ToString();
        }

        private void validAdminUser()
        {

        }
        #endregion
    }
}