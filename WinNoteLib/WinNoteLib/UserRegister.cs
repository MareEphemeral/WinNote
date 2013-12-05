using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinNoteLib
{
    public class UserRegister 
    {
        private bool ValidateUserExistance(User user) //检查用户名是否存在
        {
            bool flag = true;
             BaseConnection conn = new BaseConnection("user_login_info");
             if (conn.BaseSelect(AssistDll.SelectConstructor("user_name=\'" + user.userName+"\'", "user_login_info")).Rows.Count > 0)
            {
                flag = false;
            }
            return flag;
        }
        public String Register(User user) //输入用户信息，注册一个新用户
        {
            String message = "注册成功";
            BaseConnection conn = new BaseConnection("user_login_info");
            if (ValidateUserExistance(user)){
                try
                {
                    conn.BaseSP("sp_note_adduser", AssistDll.ParaConstructor(new String[] {
                "user_name",
                "user_pass",
                "age",
                "email",
                "school_name",
                "sex",
                "real_name",
                "tags_like"}, new Object[] { user.userName, user.userPass, user.age, user.email, user.school_name, user.sex, user.realName,AssistDll.tagsEncapsulation(user.tagsLike) }));
                }
                catch(Exception e)
                {
                    message = "您输入的信息有误";
                    CommonPara.ErrorMessage = e.Message;
                }
            }
            else
            {
                message = "您输入的用户名已存在";
            }
            return message;
        }
    }
}
