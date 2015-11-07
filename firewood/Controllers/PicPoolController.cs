using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Caching;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using BLL;

namespace firewood.Controllers
{
    [RoutePrefix("PicPool")]
    public class PicPoolController : Controller
    {
        static Cache cache = HttpRuntime.Cache;

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="id">社团组织或活动的Guid</param>
        /// <param name="type">1:社团组织 2:活动</param>
        /// <param name="size">300x600 0x0是原图</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Get/ID/{id:guid}/Type/{type:int}/Size/{size}")]
        public ActionResult Get(Guid id, int type, string size)
        {
            var width = Int32.Parse(size.Split('x')[0]);
            var height = Int32.Parse(size.Split('x')[1]);
            Image img = GetImg(type, id);
            Bitmap bmp;

            if (width == 0 || height == 0) bmp = new Bitmap(img);
            else bmp = new Bitmap(img, width, height);

            var result = SaveBmp(bmp, img);
            Response.AddHeader("Cache-Control", "cache, store, must-revalidate");
            return File(result, "image/png");
        }

        /// <summary>
        /// 获取图片
        /// </summary>
        /// <param name="id">社团组织或活动的Guid</param>
        /// <param name="type">1:社团组织 2:活动</param>
        /// <param name="width">0代表原图</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Get/ID/{id:guid}/Type/{type:int}/Width/{width:int}")]
        public ActionResult Get(Guid id, int type, int width)
        {
            Image img = GetImg(type, id);
            Bitmap bmp;

            if (width == 0)
            {
                bmp = new Bitmap(img);
            }
            else
            {
                var height = width * img.Width / img.Height;
                bmp = new Bitmap(img, width, height);
            }

            var result = SaveBmp(bmp, img);
            Response.AddHeader("Cache-Control", "cache, store, must-revalidate");
            return File(result, "image/png");
        }

        #region 方法
        public Image GetImg(int type, Guid id)
        {
            OrgService orgService = new OrgService();
            ActService actService = new ActService();
            Image img;
            string cacheID = id.ToString();

            if (cache[cacheID] != null)
            {
                img = cache[cacheID] as Image;
                return img;
            }
            else
            {
                string path = "";

                if (type == 1) path = SiteConfig.SitePath + orgService.GetPathByID(id);
                else if (type == 2) path = SiteConfig.SitePath + actService.GetPathByID(id);

                if (!System.IO.File.Exists(path)) return null;
                else
                {
                    img = Bitmap.FromFile(path);
                    cache.Add(cacheID, img, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
                    return img;
                }
            }
        }

        public byte[] SaveBmp(Bitmap bmp, Image img)
        {
            MemoryStream ms = new MemoryStream();
            ImageFormat imgFormat = img.RawFormat;

            if (imgFormat.Equals(ImageFormat.Png) || imgFormat.Equals(ImageFormat.Gif))
            {
                bmp.Save(ms, ImageFormat.Png);
            }
            else
            {
                bmp.Save(ms, ImageFormat.Jpeg);
            }

            var result = ms.ToArray();
            ms.Dispose();

            return result;
        }
        #endregion
    }
}