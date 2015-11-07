using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    interface ICommentHandler
    {
        #region 增
        /// <summary>
        /// 用户点击“添加评论”后，创建一个Comment实体
        /// </summary>
        #endregion

        #region 删
        /// <summary>
        /// 删除某个活动时，需将其留言关系删除
        /// </summary>
        /// <summary>
        /// 用户可以删除自己的留言
        /// </summary>
        #endregion

        #region 查
        /// <summary>
        /// 查询自己的留言
        /// </summary>
        #endregion

        #region 改
        /// <summary>
        /// 修改自己的留言
        /// </summary>
        #endregion
    }
}
