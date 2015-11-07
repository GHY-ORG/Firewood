using System;
using System.Collections.Generic;

namespace DataSource.Models
{
    public partial class Comment
    {
        public System.Guid ComID { get; set; }
        public System.Guid UserID { get; set; }
        public System.Guid OrgID { get; set; }
        public System.Guid ActID { get; set; }
        public string ComCon { get; set; }
        public System.DateTime ComTime { get; set; }
        public Nullable<System.Guid> NextComID { get; set; }
        public int Type { get; set; }
        public int State { get; set; }
    }
}
