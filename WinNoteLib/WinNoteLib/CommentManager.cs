using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace WinNoteLib
{
    public class CommentManager 
    {
         public List<Comment> getAllComments(int noteID) //获取目标笔记全部的评论
          {
              List<Comment> comments = new List<Comment>();
              BaseConnection conn = new BaseConnection("comments");
              DataTable dt = new DataTable();
              try
              {
                  dt = conn.BaseSelect(AssistDll.SelectConstructor(new string[] { "*" }, "note_id=" + noteID, "comments"));
                  foreach (DataRow dr in dt.Rows)
                  {
                      Comment tempComment = new Comment((string)dr.ItemArray[1], (int)dr.ItemArray[2], (int)noteID);
                      tempComment.ID = (int)dr.ItemArray[0];
                      comments.Add(tempComment);
                  }
              }
              catch (Exception e)
              {
                  CommonPara.ErrorMessage = e.Message;
              }
              return comments;
          }
        public String AddComment(Comment comment) //对指定笔记提那件一条评论
         {
             String message = "发送成功";
             BaseConnection conn = new BaseConnection("comments");
             try
             {
                 conn.BaseSP("sp_note_addcomment", AssistDll.ParaConstructor(new String[] {
                "comment",
                "note_id",
                "writer_id"
                }, new Object[] { comment.comment,comment.note_id,comment.writer_id }));
             }
             catch (Exception e)
             {
                 message = "您输入的信息有误";
                 CommonPara.ErrorMessage = e.Message;
             }
             return message;
         }

        public String DeleteComment(Comment comment) //删除一条指定的评论
        {
            String message = "发送成功";
            BaseConnection conn = new BaseConnection("comments");
            try
            {
                conn.BaseSelect(AssistDll.DeleteConstructor("id="+comment.ID,"comments"));
             
            }
            catch (Exception e)
            {
                message = "表已空";
                CommonPara.ErrorMessage = e.Message;
            }
            return message;
        }
         
    }
}
