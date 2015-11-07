using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Web;

namespace firewood
{
    public class SiteConfig
    {
        private static string _siteUrl;
        public static string SiteUrl
        {
            get
            {
                if (String.IsNullOrEmpty(_siteUrl))
                {
                    _siteUrl = HttpRuntime.AppDomainAppVirtualPath;
                }
                return _siteUrl;
            }
            set { _siteUrl = value; }
        }


        private static string _sitePath;

        public static string SitePath
        {
            get { return SiteConfig._sitePath; }
            set { SiteConfig._sitePath = value; }
        }

        public  static CompositionContainer Container;
    }
}