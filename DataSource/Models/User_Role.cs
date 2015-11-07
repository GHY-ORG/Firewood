using System;
using System.Collections.Generic;

namespace DataSource.Models
{
    public partial class User_Role
    {
        public int RoleID { get; set; }
        public System.Guid UserID { get; set; }
        public int Status { get; set; }
    }
}
