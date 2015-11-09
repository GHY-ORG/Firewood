using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace firewood
{
    public class CommentModels
    {
        public System.Guid ActID { get; set; }

        [Required(ErrorMessage = "必填")]
        [MaxLength(50, ErrorMessage = "50字以内")]
        public string ComCon { get; set; }

        public System.Guid ComID { get; set; }

        public bool hasJoin { get; set; }

        public Guid UserID { get; set; }

        public string NickName { get; set; }
    }
}