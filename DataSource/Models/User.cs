using System;
using System.Collections.Generic;

namespace DataSource.Models
{
    public partial class User
    {
        public System.Guid UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public string StuNumber { get; set; }
        public string Avatar { get; set; }
        public Nullable<int> Sex { get; set; }
        public string Tel { get; set; }
        public string TrueName { get; set; }
        public System.DateTime RegisterTime { get; set; }
        public int Status { get; set; }
    }
}
