using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSource.Models;

namespace DAL
{
    /// <summary>
    /// 活动类数据处理层接口
    /// </summary>
    interface IActHandler
    {
        #region 增
        /// <summary>
        /// 添加一个活动
        /// </summary>
        /// <param name="act"></param>
        /// <returns></returns>
        bool Create(Activity act);
        #endregion

        #region 删
        /// <summary>
        /// 根据ActID删除某个活动
        /// </summary>
        /// <param name="actid"></param>
        /// <returns></returns>
        bool Delete(Guid actid);
        /// <summary>
        /// 删除某个社团组织的所有活动，同时删除Join表中相关信息
        /// </summary>
        /// <param name="orgid"></param>
        /// <returns></returns>
        bool DeleteByOrgID(Guid orgid);
        #endregion

        #region 查
        /// <summary>
        /// 查询所有活动
        /// </summary>
        /// <param name="n">一页显示n个活动</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        List<Activity> GetAll(int n, int page);
        /// <summary>
        /// 根据ActID获取Activity实体
        /// </summary>
        /// <param name="actid"></param>
        /// <returns></returns>
        Activity GetActByID(Guid actid);
        /// <summary>
        /// 查询社团组织举办过或正在举办的所有活动
        /// </summary>
        /// <param name="orgid"></param>
        /// <param name="n">一页显示n个活动</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        List<Activity> GetActByOrgID(Guid orgid, int n, int page);
        /// <summary>
        /// 根据Class1查询所有未结束的活动
        /// </summary>
        /// <param name="class1"></param>
        /// /// <param name="n">一页显示n个活动</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        List<Activity> GetActByClass1(string class1, int n, int page);
        /// <summary>
        /// 根据Class2查询所有未结束的活动
        /// </summary>
        /// <param name="class2"></param>
        /// <param name="n">一页显示n个活动</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        List<Activity> GetActByClass2(string class2, int n, int page);
        /// <summary>
        /// 根据Place查询所有未结束的活动
        /// </summary>
        /// <param name="place"></param>
        /// <param name="n">一页显示n个活动</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        List<Activity> GetActByPlace(string place, int n, int page);
        /// <summary>
        /// 查询所有有奖金但未结束的活动
        /// </summary>
        /// <param name="n">一页显示n个活动</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        List<Activity> GetActByMoney(int n, int page);
        /// <summary>
        /// 查询所有有第二课堂学分认定但未结束的活动
        /// </summary>
        /// <param name="n">一页显示n个活动</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        List<Activity> GetActByScore(int n, int page);
        /// <summary>
        /// 查询所有有加分但未结束的活动
        /// </summary>
        /// <param name="n">一页显示n个活动</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        List<Activity> GetActByAward(int n, int page);
        /// <summary>
        /// 查询所有需要投票但未结束的活动
        /// </summary>
        /// <param name="n">一页显示n个活动</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        List<Activity> GetActByVote(int n, int page);
        /// <summary>
        /// 查询所有还没开始的活动
        /// </summary>
        /// <param name="n">一页显示n个活动</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        List<Activity> GetActByBeginTime(int n, int page);
        /// <summary>
        /// 查询所有还没结束的活动（首页展示的活动）
        /// </summary>
        /// <param name="n">一页显示n个活动</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        List<Activity> GetActByEndTime(int n, int page);
        /// <summary>
        /// 根据用户输入的搜索内容模糊匹配活动标题查询
        /// </summary>
        /// <param name="title"></param>
        /// <param name="n"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        List<Activity> GetActByTitle(string title, int n, int page);
        #endregion
    }
}
