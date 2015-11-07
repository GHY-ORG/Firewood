using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace firewood
{
    public class PublishModels
    {
        [Display(Name = "活动名称")]
        [Required(ErrorMessage = "活动名称必填")]
        [MinLength(1, ErrorMessage = "活动名称不少于1个字")]
        [MaxLength(50, ErrorMessage = "活动名称不超过50个字")]
        public string ActName { set; get; }

        [Display(Name = "活动地点")]
        [Required(ErrorMessage = "活动地点必填")]
        [MinLength(1, ErrorMessage = "活动地点不少于1个字")]
        [MaxLength(20, ErrorMessage = "活动地点不超过20个字")]
        public string Place { set; get; }

        [Display(Name = "一级分类")]
        [Required(ErrorMessage = "一级分类必填")]
        [MinLength(1, ErrorMessage = "一级分类不少于1个字")]
        [MaxLength(20, ErrorMessage = "一级分类不超过20个字")]
        public string Class1 { set; get; }

        [Display(Name = "二级分类")]
        [Required(ErrorMessage = "二级分类必填")]
        [MinLength(1, ErrorMessage = "二级分类不少于1个字")]
        [MaxLength(20, ErrorMessage = "二级分类不超过20个字")]
        public string Class2 { set; get; }

        [Display(Name = "开始时间")]
        [Required(ErrorMessage = "开始时间必填")]
        [DataType(DataType.DateTime, ErrorMessage = "请填写正确格式的时间")]
        public DateTime BeginTime { set; get; }

        [Display(Name = "结束时间")]
        [Required(ErrorMessage = "结束时间必填")]
        [DataType(DataType.DateTime, ErrorMessage = "请填写正确格式的时间")]
        public DateTime EndTime { set; get; }

        [Display(Name = "是否有奖金")]
        [Required(ErrorMessage = "必选")]
        public string Money { set; get; }

        [Display(Name = "是否有第二课程学分认定")]
        [Required(ErrorMessage = "必选")]
        public string Score { set; get; }

        [Display(Name = "是否有加分")]
        [Required(ErrorMessage = "必选")]
        public string Award { set; get; }

        [Display(Name = "是否需要投票")]
        [Required(ErrorMessage = "必选")]
        public string Vote { set; get; }

        [Display(Name = "活动简介")]
        [Required(ErrorMessage = "活动简介必填")]
        [MinLength(1, ErrorMessage = "活动简介不少于1个字")]
        [MaxLength(1000, ErrorMessage = "活动简介不超过1000个字")]
        public string ActIntro { set; get; }
    }
}