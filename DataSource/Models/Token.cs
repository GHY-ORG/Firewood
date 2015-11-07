using System;
using System.Collections.Generic;

namespace DataSource.Models
{
    public partial class Token
    {
        public System.Guid TokenID { get; set; }
        public System.Guid UserID { get; set; }
        public string TokenCode { get; set; }
        public string CheckCode { get; set; }
        public System.DateTime Expire { get; set; }
        public int Status { get; set; }
    }
}
