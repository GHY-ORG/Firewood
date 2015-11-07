using System;
using System.Collections.Generic;

namespace DataSource.Models
{
    public partial class Join
    {
        public System.Guid UserID { get; set; }
        public System.Guid ActID { get; set; }
        public int Status { get; set; }
    }
}
