using DataSource.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CommentHandler : ICommentHandler
    {
        #region 添加留言
        public bool AddComment(Comment comment)
        {
            using (var db = new FirewoodContext())
            {
                db.Comments.Add(comment);
                return db.SaveChanges() == 1 ? true : false;
            }
        }
        #endregion

        #region 查看留言
        //查看某个活动的最新的几条留言
        public List<Comment> selectCommentByActID_num(Guid ActID, int num)
        {
            using (var db = new FirewoodContext())
            {
                var a = db.Comments.Where(x => x.ActID == ActID && x.Type == 1 && x.State > 0);
                var b = a.OrderByDescending(x => x.ComTime);
                if (b.ToList<Comment>().Count >= 5)
                {
                    return b.Take(num).ToList<Comment>();
                }
                return b.ToList<Comment>();
            }
        }
        //查看回复情况
        public List<Comment> selectCommentByComID(Guid ComID)
        {
            using (var db = new FirewoodContext())
            {
                var a = db.Comments.Where(x => x.ComID == ComID).FirstOrDefault().NextComID;
                List<Comment> commentList = new List<Comment>();
                while (a != null)
                {
                    var b = db.Comments.Where(x => x.ComID == a && x.State > 0).FirstOrDefault();
                    commentList.Add(b);
                    a = b.NextComID;
                }
                return commentList;
            }
        }
        //根据用户id查看留言
        public List<Comment> selectByUserID(Guid userID, int n, int page)
        {
            using (var db = new FirewoodContext())
            {
                var a = db.Comments.Where(x => x.UserID == userID && x.Type == 1 && x.State > 0);
                var b = a.OrderByDescending(x => x.ComTime).Skip(n * (page - 1)).Take(n).ToList<Comment>();
                return b;
            }
        }
        #endregion

        #region 留言回复
        //其实就是添加留言
        #endregion

        #region 修改被回复留言的NextComID
        public bool AddNextComID(Guid ComID, Guid NextComID)
        {
            using (var db = new FirewoodContext())
            {
                var a = db.Comments.Where(x => x.ComID == ComID).FirstOrDefault();
                if (a == null)
                    return false;
                a.NextComID = NextComID;
                return db.SaveChanges() == 1 ? true : false;
            }
        }
        #endregion
    }
}
