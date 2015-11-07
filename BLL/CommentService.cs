using DAL;
using DataSource.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CommentService
    {
        private CommentHandler commentHandler = new CommentHandler();

        #region 添加留言
        public bool AddComment(Comment comment)
        {
            comment.State = 1;
            return commentHandler.AddComment(comment);
        }
        #endregion

        #region 查看某个活动的最新的几条留言
        public List<Comment> selectComment(Guid ActID, int num)
        {
            return commentHandler.selectCommentByActID_num(ActID, num);
        }
        #endregion

        #region 留言回复
        //用户回复留言，对 ComID 的留言 回复留言comment
        public bool ReplyCommentByUser(Guid ComID, Comment comment)
        {
            comment.State = 1;
            comment.Type = 2;
            if (commentHandler.AddComment(comment))
                return commentHandler.AddNextComID(ComID, comment.ComID);
            return false;
        }
        //社团回复留言
        public bool ReplyCommentByOrg(Guid ComID, Comment comment)
        {
            comment.State = 1;
            comment.Type = 3;
            if (commentHandler.AddComment(comment))
                return commentHandler.AddNextComID(ComID, comment.ComID);
            return false;
        }
        #endregion
    }
}
