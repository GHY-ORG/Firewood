using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSource.Models;

namespace DAL
{
    public class ActHandler : IActHandler
    {
        public bool Create(Activity act)
        {
            act.State = 1;
            using (var db = new FirewoodContext())
            {
                db.Activities.Add(act);
                return db.SaveChanges() == 1;
            }
        }

        public bool Delete(Guid actid)
        {
            using (var db = new FirewoodContext())
            {
                var act = db.Activities.Find(actid);
                act.State = -1;
                return db.SaveChanges() == 1;
            }
        }

        public bool DeleteByOrgID(Guid orgid)
        {
            using (var db = new FirewoodContext())
            {
                var activities = db.Activities.Where(x => x.OrgID == orgid && x.State > 0);
                foreach (Activity act in activities)
                {
                    var results = db.Joins.Where(x => x.ActID == act.ActID && x.Status > 0);
                    foreach (Join join in results)
                    {
                        join.Status = -1;
                    }
                    act.State = -1;
                }
                return db.SaveChanges() >= 0;
            }
        }

        public List<Activity> GetAll(int n, int page)
        {
            using (var db = new FirewoodContext())
            {
                var activities = db.Activities.Where(x => x.State > 0).OrderByDescending(x => x.BeginTime);
                return activities.Skip(n * (page - 1)).Take(n).ToList<Activity>();
            }
        }

        public int GetActCount(Guid orgid)
        {
            using (var db = new FirewoodContext())
            {
                var org = db.Activities.Where(x => x.OrgID.Equals(orgid));
                return org.Count();
            }
        }

        public Activity GetActByID(Guid actid)
        {
            using (var db = new FirewoodContext())
            {
                return db.Activities.Find(actid);
            }
        }

        public List<Activity> GetActByOrgID(Guid orgid, int n, int page)
        {
            using (var db = new FirewoodContext())
            {
                var activities = db.Activities.Where(x => x.OrgID == orgid && x.State > 0).OrderByDescending(x => x.BeginTime);
                return activities.Skip(n * (page - 1)).Take(n).ToList<Activity>();
            }
        }

        public List<Activity> GetActByClass1(string class1, int n, int page)
        {
            using (var db = new FirewoodContext())
            {
                var activities = db.Activities.Where(x => x.Class1 == class1 && x.State > 0).OrderByDescending(x => x.BeginTime);
                return activities.Skip(n * (page - 1)).Take(n).ToList<Activity>();
            }
        }

        public List<Activity> GetActByClass2(string class2, int n, int page)
        {
            using (var db = new FirewoodContext())
            {
                var activities = db.Activities.Where(x => x.Class2 == class2 && x.State > 0).OrderByDescending(x => x.BeginTime);
                return activities.Skip(n * (page - 1)).Take(n).ToList<Activity>();
            }
        }

        public List<Activity> GetActByPlace(string place, int n, int page)
        {
            using (var db = new FirewoodContext())
            {
                var activities = db.Activities.Where(x => x.Place == place && x.State > 0).OrderByDescending(x => x.BeginTime);
                return activities.Skip(n * (page - 1)).Take(n).ToList<Activity>();
            }
        }

        public List<Activity> GetActByMoney(int n, int page)
        {
            using (var db = new FirewoodContext())
            {
                var activities = db.Activities.Where(x => x.MoneyState == 1 && x.State > 0).OrderByDescending(x => x.BeginTime);
                return activities.Skip(n * (page - 1)).Take(n).ToList<Activity>();
            }
        }

        public List<Activity> GetActByScore(int n, int page)
        {
            using (var db = new FirewoodContext())
            {
                var activities = db.Activities.Where(x => x.ScoreState == 1 && x.State > 0).OrderByDescending(x => x.BeginTime);
                return activities.Skip(n * (page - 1)).Take(n).ToList<Activity>();
            }
        }

        public List<Activity> GetActByAward(int n, int page)
        {
            using (var db = new FirewoodContext())
            {
                var activities = db.Activities.Where(x => x.AwardState == 1 && x.State > 0).OrderByDescending(x => x.BeginTime);
                return activities.Skip(n * (page - 1)).Take(n).ToList<Activity>();
            }
        }

        public List<Activity> GetActByVote(int n, int page)
        {
            using (var db = new FirewoodContext())
            {
                var activities = db.Activities.Where(x => x.VoteState == 1 && x.State > 0).OrderByDescending(x => x.BeginTime);
                return activities.Skip(n * (page - 1)).Take(n).ToList<Activity>();
            }
        }

        public List<Activity> GetActByBeginTime(int n, int page)
        {
            using (var db = new FirewoodContext())
            {
                var activities = db.Activities.Where(x => x.BeginTime > DateTime.Now && x.State > 0).OrderByDescending(x => x.BeginTime);
                return activities.Skip(n * (page - 1)).Take(n).ToList<Activity>();
            }
        }

        public List<Activity> GetActByEndTime(int n, int page)
        {
            using (var db = new FirewoodContext())
            {
                var activities = db.Activities.Where(x => x.EndTime > DateTime.Now && x.State > 0).OrderByDescending(x => x.BeginTime);
                return activities.Skip(n * (page - 1)).Take(n).ToList<Activity>();
            }
        }


        public List<Activity> GetActByTitle(string title, int n, int page)
        {
            using (var db = new FirewoodContext())
            {
                var activities = db.Activities.Where(x => x.ActName.Contains(title) && x.EndTime > DateTime.Now && x.State > 0).OrderByDescending(x => x.BeginTime);
                return activities.Skip(n * (page - 1)).Take(n).ToList<Activity>();
            }
        }
    }
}
