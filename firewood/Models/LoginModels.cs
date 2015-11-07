using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace firewood
{
    public class LoginModels
    {
        [Display(Name = "用户名")]
        [Required(ErrorMessage = "必填")]
        [MaxLength(50, ErrorMessage = "50字以内")]
        public string OrgName { set; get; }

        [Display(Name = "密码")]
        [Required(ErrorMessage = "必填")]
        [MaxLength(50, ErrorMessage = "标题50字以内")]
        public string Password { set; get; }
    }
}