using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace WinNoteLib
{
    public class CommonPara
    {
        public static string ConnectionString; //数据库连接字符串
        public static string ErrorMessage; //错误信息记录
        public static int tempUserID; //临时用户ID
        public static int tempUserLevel; //临时用户级别
        public static string tempUserName; //临时用户名
        public static string tempGroupName;
        public static string xxsql = "select groups.group_name from groups,user_detailed_info where groups.id=user_detailed_info.group_id and user_detailed_info.user_id=";
    }
}
