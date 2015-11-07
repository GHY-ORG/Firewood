using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace firewood
{
    public class RegisterModels
    {
        [Display(Name = "社团/组织名称")]
        [Required(ErrorMessage = "社团/组织名称必填")]
        [MinLength(1, ErrorMessage = "社团/组织名称不少于1个字")]
        [MaxLength(20, ErrorMessage = "社团/组织名称不超过20个字")]
        public string OrgName { set; get; }

        [Display(Name = "所属部门")]
        [Required(ErrorMessage = "所属部门必填")]
        [MinLength(2, ErrorMessage = "所属部门不少于2个字")]
        [MaxLength(20, ErrorMessage = "所属部门不超过20个字")]
        public string OrgDepartment { set; get; }

        [Display(Name = "社团/组织联系方式")]
        [Required(ErrorMessage = "社团/组织联系方式必填")]
        [MinLength(6, ErrorMessage = "联系方式不少于6位")]
        [MaxLength(20, ErrorMessage = "联系方式超过20位")]
        public string OrgContact { set; get; }

        [Display(Name = "简介")]
        [Required(ErrorMessage = "简介必填")]
        [MinLength(5, ErrorMessage = "简介不少于5个字")]
        [MaxLength(500, ErrorMessage = "简介不超过500个字")]
        public string OrgIntro { set; get; }
    }
}