using System;
using System.Collections.Generic;

namespace DataSource.Models
{
    public partial class Activity
    {
        public System.Guid ActID { get; set; }
        public string ActName { get; set; }
        public string ActIntro { get; set; }
        public string ActPic { get; set; }
        public System.Guid OrgID { get; set; }
        public string Class1 { get; set; }
        public string Class2 { get; set; }
        public string Place { get; set; }
        public System.DateTime BeginTime { get; set; }
        public System.DateTime EndTime { get; set; }
        public int MoneyState { get; set; }
        public int ScoreState { get; set; }
        public int AwardState { get; set; }
        public int VoteState { get; set; }
        public int State { get; set; }
    }
}
