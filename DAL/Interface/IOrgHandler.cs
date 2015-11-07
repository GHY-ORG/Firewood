using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSource.Models;

namespace DAL
{
    /// <summary>
    /// 社团组织类数据处理层接口
    /// </summary>
    interface IOrgHandler
    {
        #region 增
        /// <summary>
        /// 社团组织注册
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        bool Create(Org org);
        #endregion

        #region 删
        /// <summary>
        /// 根据OrgID注销社团组织
        /// </summary>
        /// <param name="orgid"></param>
        /// <returns></returns>
        bool Delete(Guid orgid);
        #endregion

        #region 改
        /// <summary>
        /// 更新社团组织基本信息（logo、负责人、负责人电话、所属部门、介绍、联系方式）
        /// </summary>
        /// <param name="org"></param>
        /// <returns></returns>
        bool Update(Org org);
        /// <summary>
        /// 根据社团组织名称更新登录时间
        /// </summary>
        /// <param name="orgname"></param>
        /// <returns></returns>
        bool UpdateLoginTime(string orgname);
        #endregion

        #region 查
        /// <summary>
        /// 根据OrgID获取Org实体
        /// </summary>
        /// <param name="orgid"></param>
        /// <returns></returns>
        Org GetOrgByID(Guid orgid);
        /// <summary>
        /// 根据OrgName获取Org实体
        /// </summary>
        /// <param name="orgname"></param>
        /// <returns></returns>
        Org GetOrgByName(string orgname);
        /// <summary>
        /// 查询OrgName是否已被注册
        /// </summary>
        /// <param name="orgname"></param>
        /// <returns></returns>
        bool OrgNameRegistered(string orgname);
        #endregion
    }
}
