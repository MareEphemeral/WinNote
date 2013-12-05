using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace WinNoteLib
{
    public class UserGroupManager 
    {
        public string getMyGroupName(int userID)
        {
            BaseConnection conn = new BaseConnection("groups");
            return (string)conn.BaseSelect(CommonPara.xxsql+userID).Rows[0].ItemArray[0];
        }
        public string[] getGroupMember(int groupID) //获取当前群组的用户名
        {
            
            BaseConnection conn = new BaseConnection("user_detailed_info");
            DataTable dt = new DataTable();
            dt = conn.BaseSelect(AssistDll.SelectConstructor("group_id=" + groupID, "user_detailed_info"));
            String[] list = new String[dt.Rows.Count];
            int i=0;
            foreach (DataRow dr in dt.Rows)
            {
                list[i++] = (string)dr.ItemArray[2];
            }
            dt = conn.BaseSelect(AssistDll.SelectConstructor("id=" + groupID, "groups"));
            String groupName=(String)dt.Rows[0].ItemArray[1];
            CommonPara.tempGroupName = groupName;
            //UserGroup group = new UserGroup(groupName, user.groupID, list, (int)dt.Rows[0].ItemArray[2]);
            return list;
        }
        public int getGroupID(String name) //通过输入的用户组名获取对应的ID
        {
            return AssistDll.ChangeNameToID("group_name", name, "groups");
        }
        public string CreateNewGroup(string groupName,int userID) //创建一个新用户组
        {
            String message = "创建成功";
            BaseConnection conn = new BaseConnection("sp_note_addgroup");

            try
            {
                conn.BaseSP("sp_note_addgroup", AssistDll.ParaConstructor(new String[] {
                "group_name",
                "creator_ID"
                 }, new Object[] { groupName, userID }));
            }
            catch (Exception e)
            {
                message = "您输入的信息有误";
                CommonPara.ErrorMessage = e.Message;
            }

            return message;
        }
        public string enterGroup(int groupID,int userID) //选择加入一个指定用户组
        {
            String message = "加入成功";
            BaseConnection conn = new BaseConnection("sp_note_entergroup");

            try
            {
                conn.BaseSP("sp_note_entergroup", AssistDll.ParaConstructor(new String[] {
                "group_id",
                "user_id"
                 }, new Object[] { groupID, userID }));
            }
            catch (Exception e)
            {
                message = "您输入的信息有误";
                CommonPara.ErrorMessage = e.Message;
            }

            return message;
        }
        public string leaveGroup( int userID) //离开当前的用户组
        {
            return enterGroup(-1, userID);
        }
        public String deleteGroup( int groupID) //群主解散当前群
        {
            String message = "删除成功";
            BaseConnection conn = new BaseConnection("note_info");

            try
            {
                conn.BaseSP("sp_note_delete_user_group", AssistDll.ParaConstructor(new String[] {
                "group_id"
                 }, new Object[] { groupID }));
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
