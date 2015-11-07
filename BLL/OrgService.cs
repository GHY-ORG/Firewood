using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DAL;
using DataSource.Models;

namespace BLL
{
    public class OrgService
    {
        private OrgHandler orgHandler = new OrgHandler();
        private ActHandler actHandler = new ActHandler();

        #region 注册
        /// <summary>
        /// 判断注册字段长度（社团名称、所属部门、简介、联系方式）
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        public string IsBlankReg(Org org)
        {
            if (org.OrgName.Length == 0 || org.OrgName.Length > 20)
            {
                return "orgname";
            }
            else if (org.OrgDepartment.Length == 0 || org.OrgDepartment.Length > 20)
            {
                return "orgdepartment";
            }
            else if (org.OrgIntroduction.Length == 0 || org.OrgIntroduction.Length > 500)
            {
                return "orgintroduction";
            }
            else if (org.OrgContact.Length == 0 || org.OrgContact.Length > 20)
            {
                return "orgcontact";
            }
            else
            {
                return "ok";
            }
        }

        public bool Register(Org org)
        {
            if (IsBlankReg(org).Equals("ok"))
            {
                return orgHandler.Create(org);
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 修改信息
        public bool UpdateInfo(Org org)
        {
            return orgHandler.Update(org);
        }
        #endregion

        #region 注销
        public bool Delete(Guid orgid)
        {
            //同时删除其发布的活动，以及join表中相关信息
            return orgHandler.Delete(orgid) && actHandler.DeleteByOrgID(orgid);
        }
        #endregion

        #region 显示所有社团
        public List<Org> ShowAllOrg(int n, int page)
        {
            return orgHandler.GetAllOrg(n, page);
        }
        #endregion

        #region 其他
        /// <summary>
        /// 获取Org实体
        /// </summary>
        /// <param name="orgid"></param>
        /// <returns></returns>
        public Org GetOrgByID(Guid orgid)
        {
            return orgHandler.GetOrgByID(orgid);
        }
        /// <summary>
        /// 获取OrgList
        /// </summary>
        /// <param name="orgidList"></param>
        /// <returns></returns>
        public List<Org> GetOrgList(string[] orgidList)
        {
            List<Org> orgList = new List<Org>(orgidList.Length);
            foreach (string item in orgidList)
            {
                orgList.Add(orgHandler.GetOrgByID(new Guid(item)));
            }
            return orgList;
        }
        /// <summary>
        /// 由名称获取ID
        /// </summary>
        /// <param name="orgname"></param>
        /// <returns></returns>
        public Guid GetIDByName(string orgname)
        {
            return orgHandler.GetOrgByName(orgname).OrgID;
        }
        /// <summary>
        /// 通过OrgID获取社团组织logo路径
        /// </summary>
        /// <param name="orgid"></param>
        /// <returns></returns>
        public string GetPathByID(Guid orgid)
        {
            return orgHandler.GetOrgByID(orgid).OrgPic;
        }
        /// <summary>
        /// 通过OrgID获取社团组织名称
        /// </summary>
        /// <param name="orgid"></param>
        /// <returns></returns>
        public string GetNameByID(Guid orgid)
        {
            return orgHandler.GetOrgByID(orgid).OrgName;
        }
        /// <summary>
        /// 判断名称是否已被注册
        /// </summary>
        /// <param name="orgname"></param>
        /// <returns></returns>
        public bool OrgNameRegistered(string orgname)
        {
            return orgHandler.OrgNameRegistered(orgname);
        }
        /// <summary>
        /// 一共有多少社团
        /// </summary>
        /// <returns></returns>
        public int GetOrgCount()
        {
            return orgHandler.GetOrgCount();
        }
        #endregion
    }
}
