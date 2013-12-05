using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace WinNoteLib
{
    public class TagsHandler
    {
        public bool ValidateTagExistance(String name) //确认输入的标签名是否已经存在
        {
            bool flag = true;
            BaseConnection conn = new BaseConnection("tags");
            if (conn.BaseSelect(AssistDll.SelectConstructor("tag_name=\'" + name + "\'", "tags")).Rows.Count > 0)
            {
                flag = false;
            }
            return flag;
        }
        public String createNewTag(String name) //尝试创建一个新标签，若已经存在返回提示
        {
            String message = "创建成功";
            BaseConnection conn = new BaseConnection("user_login_info");
            if (ValidateTagExistance(name))
            {
                try
                {
                    conn.BaseSP("sp_note_addtag", AssistDll.ParaConstructor(new String[] {
                "tag_name",
                 }, new Object[] { name }));
                }
                catch (Exception e)
                {
                    message = "您输入的信息有误";
                    CommonPara.ErrorMessage = e.Message;
                }
            }
            else
            {
                message = "已存在";
            }
            return message;
        }
        public Tag[] getHotTags() //获取几条涉及最多的标签
        {
            Tag[] tags=new Tag[10];
            BaseConnection conn = new BaseConnection("tags");
            DataTable dt = new DataTable();
            try
            {
                dt=conn.BaseSelect(AssistDll.TopSelectConstructor(new String[]{"*"},"1=1","tags",10,true,"tag_count"));
                int i=0;
                foreach(DataRow dr in dt.Rows)
                {
                    Tag tag = new Tag((int)dr.ItemArray[0], (string)dr.ItemArray[1], (int)dr.ItemArray[2]);
                    tags[i++] = tag;
                }
            }
            catch(Exception e)
            {
                CommonPara.ErrorMessage=e.Message;
            }
            return tags;
        }
        public static int[] tagsTrans(string[] orgin) //将标签组从标签名转换为ID
        {
            int[] result = new int[3];
            for (int i = 0; i < orgin.Length; i++)
            {
                    result[i] = AssistDll.ChangeNameToID("tag_name", orgin[i], "tags");
            }
            return result;
        }
        public static string[] reTagsTrans(int[] orgin) //将标签组从ID转换为标签名
        {
            string[] result = new string[3];
            for (int i = 0; i < orgin.Length; i++)
                result[i] = AssistDll.ChangeIDToName("tag_name",orgin[i],"tags");
            return result;
        }
    }
}
