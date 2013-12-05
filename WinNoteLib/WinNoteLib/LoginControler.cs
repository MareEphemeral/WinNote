using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace WinNoteLib
{
    public class LoginControler 
    {
        public static String TalbeName = "user_login_info";
        public String Validate(String userName, String passWord) //确认输入用户密码是否正确，返回用户级别
        {
            String result = "用户不存在";
            String targetPass = "";
            String[] targetFields = { "*" };
            String selectSQL = AssistDll.SelectConstructor(targetFields, " user_name=\'" + userName + "\'", TalbeName);
            try
            {
                BaseConnection conn = new BaseConnection(TalbeName);
                DataTable tempSet = new DataTable();
                tempSet = conn.BaseSelect(selectSQL);
                if (tempSet.Rows.Count > 0)
                {
                    targetPass = (String)tempSet.Rows[0].ItemArray[2];
                    if (targetPass == passWord)
                    {
                        result = tempSet.Rows[0].ItemArray[3].ToString();
                        CommonPara.tempUserID = (int)tempSet.Rows[0].ItemArray[0];
                        CommonPara.tempUserLevel = Convert.ToInt32(result);
                        CommonPara.tempUserName = userName;
                    }
                    else
                    {
                        result = "密码错误";
                    }
                }
                else
                {
                    result = "不存在";
                }
            }
            catch (Exception e) {
                CommonPara.ErrorMessage = e.Message;
            }
            return result;
        }
    }
}
