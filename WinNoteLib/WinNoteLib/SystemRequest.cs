using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace WinNoteLib
{
    public class SystemRequest
    {
        public String getAdminName()    //获取随即一位管理员的用户名
        {
            List<String> Admin=new List<String>();
            BaseConnection conn = new BaseConnection("user_login_info");
            DataTable dt = new DataTable();
            dt = conn.BaseSelect(AssistDll.SelectConstructor("user_level=3", "user_login_info"));
            foreach (DataRow dr in dt.Rows)
            {
                Admin.Add((string)dr.ItemArray[1]);
            }
            Random ra = new Random();
            return Admin[ra.Next(0,Admin.Count)];
        }
        public String wantToLevelUp(String userName) //像管理员发出声请成为高级别用户
        {
            String message = "成功";
            MessageManager msgMana=new MessageManager();
            msgMana.SendAMessage( new Message(userName, "System", "Apply:LevelUp", 2));
            return message;
        }
        public String acceptLevelUp(int userID)//批准用户级别声请
        {
            String message = "成功";
            UserManager usrMana = new UserManager();
            usrMana.LevelUP(userID);
            return message;
        }
        public String wantToCreateGroup(String userName, String groupName)//像管理员发出声请创建用户群
        {
            String message = "成功";
            MessageManager msgMana = new MessageManager();
            msgMana.SendAMessage(new Message(userName, "System", "Apply:CreateGroup:" + groupName, 2));
            return message;

        }
        public String wantToEnterGroup(int groupID, int user_id)//像群主发出请求加入该用户群
        {
            String message = "发送成功";
            BaseConnection conn = new BaseConnection("groups");
            try
            {
                conn.BaseSP("sp_note_apply_enter_group", AssistDll.ParaConstructor(new String[] {
                "group_id",
                "user_id"
                }, new Object[] { groupID, user_id }));
            }
            catch (Exception e)
            {
                message = "您输入的信息有误";
                CommonPara.ErrorMessage = e.Message;
            }
            return message;

        }
        public String acceptCreateGroup(int userID, String groupName)//批准用户群创建声请
        {
            String message = "成功";
            UserGroupManager usrMana = new UserGroupManager();
            usrMana.CreateNewGroup(groupName, userID);
            return message;
        }

        public String acceptCreateGroup(int gourpID, int UserID)//批准用户加入该用户群
        {
            String message = "成功";
            UserGroupManager groupMana = new UserGroupManager();
            groupMana.enterGroup(gourpID,UserID);
            return message;
        }
        public String wantToRecommend(String userName, String BookName)//像管理员发出声请推荐一篇笔记
        {
            String message = "成功";
            MessageManager msgMana = new MessageManager();
            msgMana.SendAMessage(new Message(userName, "System", "Apply:RecommendBook:" + BookName, 2));
            return message;
        }

        public String acceptRecommend(int userID, String bookName)//批准推荐笔记声请
        {
            String message = "成功";
            NoteManager noteMana = new NoteManager();
            noteMana.AddBookValue(bookName);
            return message;
        }
    }
}
