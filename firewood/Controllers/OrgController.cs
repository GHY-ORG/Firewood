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
            if (Session["RoleList"] == null) return Redirect(SiteConfig.SiteUrl+"/Home/Index");
            ShowUser();

            ViewData["OrgList"] = orgService.GetOrgList(AuthorizeStrategy.GetRoleName(Session["RoleList"] as int[]));
            return View();
        }

        [HttpGet]
        [Route("ShowAct/ID/{id:guid}/Page/{page:int}")]
        public ActionResult ShowAct(Guid id, int page)
        {
            if (Session["RoleList"] == null) return Redirect(SiteConfig.SiteUrl+"/Home/Index");
            ShowUser();

            ViewBag.OrgID = id;
            ViewBag.Count = actService.GetActCountByOrgID(id);
            ViewData["ActList"] = actService.GetActListByOrgID(id, 8, page);
            return View();
        }

        [HttpGet]
        [Route("Register")]
        public ActionResult Register()
        {
            int[] roleList = Session["RoleList"] as int[];
            if (roleList == null || roleList.Except(new int[1] { 1 }).Count() != 0) return Redirect(SiteConfig.SiteUrl+"/Home/Index");

            ShowUser();
            return View();
        }

        [HttpGet]
        [Route("Search/Page/{page:int}")]
        public ActionResult Search(int page)
        {
            int[] roleList = Session["RoleList"] as int[];
            if (roleList == null || roleList.Except(new int[1] { 1 }).Count() != 0) return Redirect(SiteConfig.SiteUrl+"/Home/Index");
            ShowUser();

            ViewBag.Count = orgService.GetOrgCount();
            ViewData["OrgList"] = orgService.ShowAllOrg(10, page);
            return View();
        }

        [HttpGet]
        [Route("Update/OrgID/{id:guid}")]
        public ActionResult Update(Guid id)
        {
            int[] roleList = Session["RoleList"] as int[];
            if (roleList == null || roleList.Except(new int[1] { 1 }).Count() != 0) return Redirect(SiteConfig.SiteUrl+"/Home/Index");
            ShowUser();

            Session["OrgID"] = id;
            Org org = orgService.GetOrgByID(id);
            ViewBag.Org = org;
            return View();
        }

        [HttpGet]
        [Route("Publish/ID/{id:guid}")]
        public ActionResult Publish(Guid id)
        {
            if (Session["RoleList"] == null) return Redirect(SiteConfig.SiteUrl+"/Home/Index");
            ShowUser();

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

            Org org = new Org
            {
                OrgID = Guid.NewGuid(),
                OrgName = model.OrgName,
                OrgContact = model.OrgContact,
                OrgDepartment = model.OrgDepartment,
                OrgIntroduction = model.OrgIntro
            };

            //图片不超过5M
            if (file == null && file.ContentLength > 1024 * 1024 * 5)
            {
                ModelState.AddModelError("", "请上传规定大小的图片");
                return View("Register", model);
            }

            //验证通过后
            string absolutePath = "E:\\web\\Firewood\\";// SiteConfig.SitePath;
            string path = GetPath("Org");
            string fullPath = absolutePath + path;
            string url = path + org.OrgID.ToString() + ".png";
            using (var stream = file.InputStream)
            {
                Image img = Image.FromStream(stream);
                var bmp = ResizeImg(img);
                if (!System.IO.Directory.Exists(fullPath))
                    System.IO.Directory.CreateDirectory(fullPath);
                bmp.Save(absolutePath+url, ImageFormat.Png);
            }
            org.OrgPic = url;

            if (orgService.Register(org))
            {
                Session["Org"] = org;
                return Content("<script>alert('注册成功~');window.location.href='Search/Page/1'</script>");
            }
            else
            {
                ModelState.AddModelError("", "注册异常");
                return View("Register", model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostUpdate(UpdateModels model)
        {
            Guid OrgID = new Guid(Session["OrgID"].ToString());
            Org org = orgService.GetOrgByID(OrgID);
            ViewBag.Org = org;

            if (!ModelState.IsValid)
            {
                return View("Update",model);
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

            //图片不超过20M
            if (file == null && file.ContentLength > 1024 * 1024 * 20)
            {
                ModelState.AddModelError("", "请上传规定大小的图片");
                return View("Publish", model);
            }

            //验证通过后
            Activity act = new Activity
            {
                ActID = Guid.NewGuid(),
                OrgID = new Guid(Session["OrgID"].ToString()),
                ActName = model.ActName,
                Place = model.Place,
                Class1 = model.Class1,
                Class2 = model.Class2,
                BeginTime = model.BeginTime,
                EndTime = model.EndTime,
                ActIntro = model.ActIntro,
                MoneyState = model.Money.Equals("yes") ? 1 : 0,
                ScoreState = model.Score.Equals("yes") ? 1 : 0,
                AwardState = model.Award.Equals("yes") ? 1 : 0,
                VoteState = model.Vote.Equals("yes") ? 1 : 0
            };
            string absolutePath = "E:\\web\\Firewood\\";// SiteConfig.SitePath;
            string path = GetPath("Act");
            string fullPath = absolutePath + path;
            string url = path + act.ActID.ToString() + ".png";
            using (var stream = file.InputStream)
            {
                Image img = Image.FromStream(stream);
                var bmp = ResizeImg(img);
                if (!System.IO.Directory.Exists(fullPath))
                    System.IO.Directory.CreateDirectory(fullPath);
                bmp.Save(absolutePath + url, ImageFormat.Png);
            }
            act.ActPic = url;

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

        [HttpGet]
        [Route("Del/OrgID/{id:guid}")]
        public ActionResult Del(Guid id)
        {
            if (orgService.Delete(id))
            {
                return Redirect(SiteConfig.SiteUrl+"/Org/Search");
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
        private string GetPath(string foldername)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("FirewoodImages\\");
            sb.Append(foldername);
            sb.Append("\\");
            sb.Append(DateTime.Now.Year+"\\"+DateTime.Now.Month+"\\"+DateTime.Now.Day);
            sb.Append("\\");
            return sb.ToString();
        }
        private void ShowUser()
        {
            ViewBag.NickName = Session["NickName"].ToString();
            ViewBag.UserID = Session["User"].ToString();
        }
        #endregion
    }
}