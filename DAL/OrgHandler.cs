using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSource.Models;
using Common;

namespace DAL
{
    public class OrgHandler : IOrgHandler
    {
        public bool Create(Org org)
        {
            org.RegisterTime = DateTime.Now;
            org.LastLogin = DateTime.Now;
            org.State = 1;
            using (var db = new FirewoodContext())
            {
                db.Orgs.Add(org);
                return db.SaveChanges() == 1;
            }
        }

        public bool Delete(Guid orgid)
        {
            using (var db = new FirewoodContext())
            {
                var org = db.Orgs.Find(orgid);
                org.State = -1;
                return db.SaveChanges() == 1;
            }
        }

        public bool Update(Org org)
        {
            using (var db = new FirewoodContext())
            {
                var x = db.Orgs.Find(org.OrgID);
                x.OrgDepartment = org.OrgDepartment;
                x.OrgIntroduction = org.OrgIntroduction;
                x.OrgContact = org.OrgContact;
                return db.SaveChanges() == 1;
            }
        }

        public bool UpdateLoginTime(string orgname)
        {
            using (var db = new FirewoodContext())
            {
                var org = db.Orgs.Where(x => x.OrgName == orgname).FirstOrDefault();
                org.LastLogin = DateTime.Now;
                return db.SaveChanges() == 1;
            }
        }

        public int GetOrgCount()
        {
            using (var db = new FirewoodContext())
            {
                var org = db.Orgs.Where(x => x.State > 0);
                return org.Count();
            }
        }

        public List<Org> GetAllOrg(int n, int page)
        {
            using (var db = new FirewoodContext())
            {
                var org = db.Orgs.Where(x => x.State > 0).OrderByDescending(x => x.RegisterTime);
                return org.Skip(n * (page - 1)).Take(n).ToList<Org>();
            }
        }

        public Org GetOrgByID(Guid orgid)
        {
            using (var db = new FirewoodContext())
            {
                var org = db.Orgs.Where(x => x.State > 0 && x.OrgID.Equals(orgid)).FirstOrDefault();
                return org;
            }
        }

        public Org GetOrgByName(string orgname)
        {
            using (var db = new FirewoodContext())
            {
                var org = db.Orgs.Where(x => x.OrgName == orgname).FirstOrDefault();
                return org;
            }
        }

        public bool OrgNameRegistered(string orgname)
        {
            using (var db = new FirewoodContext())
            {
                var org = db.Orgs.Where(x => x.OrgName == orgname && x.State > 0).FirstOrDefault();
                return org != null;
            }
        }
    }
}
