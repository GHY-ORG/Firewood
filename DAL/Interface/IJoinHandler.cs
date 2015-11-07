using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSource.Models;

namespace DAL
{
    /// <summary>
    /// 参与活动类数据处理层接口
    /// </summary>
    interface IJoinHandler
    {
        #region 增
        /// <summary>
        /// 用户点击“我想要参加”后，创建一个Join实体
        /// </summary>
        /// <param name="join"></param>
        /// <returns></returns>
        bool Create(Join join);
        #endregion

        #region 删
        /// <summary>
        /// 删除某个活动时，需将其参加关系删除
        /// </summary>
        /// <param name="actid"></param>
        /// <returns></returns>
        bool DeleteByActID(Guid actid);
        /// <summary>
        /// 用户不想参加某个活动的时候，根据ActID和UserID删除记录
        /// </summary>
        /// <param name="actid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        bool Delete(Guid actid, Guid userid);
        #endregion

        #region 查
        /// <summary>
        /// 查询某个活动一共想参与的人数
        /// </summary>
        /// <param name="actid"></param>
        /// <returns></returns>
        int GetSumByActID(Guid actid);
        /// <summary>
        /// 判断某条记录是否存在
        /// </summary>
        /// <param name="actid"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        bool IsExist(Guid actid, Guid uid);
        #endregion
    }
}
