using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSource.Models;

namespace DAL
{
    public class JoinHandler : IJoinHandler
    {
        public bool Create(Join join)
        {
            join.Status = 1;
            using (var db = new FirewoodContext())
            {
                db.Joins.Add(join);
                return db.SaveChanges() == 1 ? true : false;
            }
        }

        public bool DeleteByActID(Guid actid)
        {
            using (var db = new FirewoodContext())
            {
                var joins = db.Joins.Where(x => x.ActID == actid && x.Status == 1);
                foreach (Join join in joins)
                {
                    join.Status = -1;
                }
                return db.SaveChanges() > 0 ? true : false;
            }
        }

        public bool Delete(Guid actid, Guid userid)
        {
            using (var db = new FirewoodContext())
            {
                var join = db.Joins.Where(x => x.ActID == actid && x.UserID == userid && x.Status == 1).FirstOrDefault();
                join.Status = -1;
                return db.SaveChanges() == 1 ? true : false;
            }
        }


        public int GetSumByActID(Guid actid)
        {
            using (var db = new FirewoodContext())
            {
                var joinList = db.Joins.Where(x => x.ActID == actid && x.Status == 1);
                return joinList.Count();
            }
        }

        public bool IsExist(Guid actid, Guid uid)
        {
            using (var db = new FirewoodContext())
            {
                var join = db.Joins.Where(x => x.ActID == actid && x.UserID == uid).FirstOrDefault();
                if (join == null) return false;
                else if (join.Status == -1) return false;
                else if (join.Status == 1) return true;
                return true;
            }
        }

        public List<Guid> GetActListByUserID(Guid userid)
        {
            using(var db = new FirewoodContext())
            {
                var join = (from o in db.Joins
                            where o.UserID.Equals(userid) && o.Status > 0
                            select o.ActID);
                return join.ToList<Guid>();
            }
        }
    }
}
