using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinNoteLib
{
    public class UserManager 
    {

        public String UpdateUserInfo(User user) //更新用户信息
        {

            String message = "注册成功";
            BaseConnection conn = new BaseConnection("user_login_info");

                try
                {
                    conn.BaseSP("sp_note_adduser", AssistDll.ParaConstructor(new String[] {
                "user_id",
                "user_name",
                "user_pass",
                "age",
                "email",
                "school_name",
                "sex",
                "real_name",
                "tags_like",
                "group_id"
                 }, new Object[] { user.userID,user.userName, user.userPass, user.age, user.email, user.school_name, user.sex, user.realName, AssistDll.tagsEncapsulation(user.tagsLike) ,user.groupID}));
                }
                catch (Exception e)
                {
                    message = "您输入的信息有误";
                    CommonPara.ErrorMessage = e.Message;
                }

            return message;




        }
        public String LevelUP(int UserID) //实际处理用户升级
        {
            String message = "升级成功";
            BaseConnection conn = new BaseConnection("user_login_info");

            try
            {
                conn.BaseSP("sp_note_edituser_level", AssistDll.ParaConstructor(new String[] {
                "user_id",
                "user_level"
                 }, new Object[] { UserID, 2 }));
            }
            catch (Exception e)
            {
                message = "意外错误";
                CommonPara.ErrorMessage = e.Message;
            }

            return message;
        }

        public String DeleteUser(User user) //管理员删除指定用户
        {
            String message = "发送成功";
            BaseConnection conn = new BaseConnection("user_login_info");
            try
            {
                conn.BaseSelect(AssistDll.DeleteConstructor("id=" + user.userID, "user_login_info"));
                conn.BaseSelect(AssistDll.DeleteConstructor("user_id=" + user.userID, "user_detailed_info"));

            }
            catch (Exception e)
            {
                message = "您输入的信息有误";
                CommonPara.ErrorMessage = e.Message;
            }
            return message;
        }

    }
}
