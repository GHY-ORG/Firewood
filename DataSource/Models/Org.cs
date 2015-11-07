using System;
using System.Collections.Generic;

namespace DataSource.Models
{
    public partial class Org
    {
        public System.Guid OrgID { get; set; }
        public string OrgName { get; set; }
        public string OrgPic { get; set; }
        public string OrgDepartment { get; set; }
        public string OrgIntroduction { get; set; }
        public string OrgContact { get; set; }
        public System.DateTime RegisterTime { get; set; }
        public System.DateTime LastLogin { get; set; }
        public int State { get; set; }
    }
}
