using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DataSource.Models;

namespace BLL
{
    public class ActService
    {
        private OrgHandler orgHandler = new OrgHandler();
        private ActHandler actHandler = new ActHandler();
        private JoinHandler joinHandler = new JoinHandler();

        #region 显示首页活动（标题、简介）
        public List<Activity> ShowIndexAct(int n, int page)
        {
            return actHandler.GetActByEndTime(n, page);
        }
        #endregion

        #region 获取活动图片路径
        public string GetPathByID(Guid actid)
        {
            return actHandler.GetActByID(actid).ActPic;
        }
        #endregion

        #region 发布活动
        public bool Publish(Activity act)
        {
            return actHandler.Create(act);
        }
        #endregion

        #region 通过OrgID获取活动
        public List<Activity> GetActListByOrgID(Guid orgid, int n, int page){
            return actHandler.GetActByOrgID(orgid, n, page);
        }
        #endregion

        #region 通过ActID获取活动
        public Activity GetActByActID(Guid actid)
        {
            return actHandler.GetActByID(actid);
        }
        #endregion

        #region 查询活动所属项（奖金、加分、第二课堂学分）
        public string GetTypeByID(Guid actid)
        {
            string type = "";
            Activity act = actHandler.GetActByID(actid);

            if (act != null)
            {
                if (act.MoneyState == 1)
                {
                    type += "奖金";
                }
                if (act.ScoreState == 1)
                {
                    type += "/第二课堂认定";
                }
                if (act.AwardState == 1)
                {
                    type += "/加分";
                }
            }
            return type;
        }
        #endregion 查询活动所属项（奖金、加分、第二课堂学分）

        #region 通过xx查询活动
        public List<Activity> GetActByClass(int type, string classname, int n, int page)
        {
            if (type == 1) return actHandler.GetActByClass1(classname, n, page);
            else if (type == 2) return actHandler.GetActByClass2(classname, n, page);
            else return null;
        }

        public List<Activity> GetActByPlace(string place, int n, int page)
        {
            return actHandler.GetActByPlace(place, n, page);
        }

        public List<Activity> GetActByMoney(int n, int page)
        {
            return actHandler.GetActByMoney(n, page);
        }

        public List<Activity> GetActByScore(int n, int page)
        {
            return actHandler.GetActByScore(n, page);
        }

        public List<Activity> GetActByAward(int n, int page)
        {
            return actHandler.GetActByAward(n, page);
        }

        public List<Activity> GetActByTitle(string title, int n, int page)
        {
            return actHandler.GetActByTitle(title, n, page);
        }
        #endregion

        #region 通过用户ID查询活动
        public List<Activity> GetActByUserID(Guid userid)
        {
            List<Guid> actidList = joinHandler.GetActListByUserID(userid);
            List<Activity> actList = new List<Activity>(actidList.Count());
            foreach (Guid item in actidList)
            {
                actList.Add(actHandler.GetActByID(item));
            }
            return actList;
        }
        #endregion

        #region GetActCount
        public int GetActCountByOrgID(Guid orgid)
        {
            return actHandler.GetActCount(orgid);
        }
        #endregion
    }
}
