using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSource.Models;
using DAL;

namespace BLL
{
    public class JoinService
    {
        private JoinHandler joinHandler = new JoinHandler();

        public bool Create(Join join)
        {
            return joinHandler.Create(join);
        }

        public int GetSumByActID(Guid actid)
        {
            return joinHandler.GetSumByActID(actid);
        }

        public bool Delete(Guid actid, Guid uid)
        {
            return joinHandler.Delete(actid, uid);
        }

        public bool IsExist(Guid actid, Guid uid)
        {
            return joinHandler.IsExist(actid, uid);
        }
    }
}
