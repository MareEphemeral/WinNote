using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace WinNoteLib
{
    public class MessageManager 
    {
        public static String TableName="messages";
        public List<Message> GetMessageList(int userID) //获取该用户收到的所有信息
        {
            List<Message> messageList=new List<Message>();
            String selectSQL = AssistDll.SelectConstructor(new String[]{"id"}, " user_id=\'" + userID + "\'", TableName);
            DataTable tempTb = new DataTable();
            BaseConnection conn=new BaseConnection(TableName);
            try { 
            tempTb = conn.BaseSelect(selectSQL);
            foreach (DataRow dr in tempTb.Rows)
            {
                Message message=new Message(
                    AssistDll.ChangeIDToName("sender_id",Convert.ToInt32(dr.ItemArray[1]), TableName),
                    AssistDll.ChangeIDToName("receiver_id",Convert.ToInt32(dr.ItemArray[2]), TableName),
                    dr.ItemArray[3].ToString(),
                    Convert.ToInt32(dr.ItemArray[4]));
                message.ID = (int) dr.ItemArray[0];
                messageList.Add(message);
            }
            }
            catch(Exception e)
            {
                CommonPara.ErrorMessage = e.Message;
            }
            return messageList;
        }
        public String SendAMessage(Message msg) //发送一条消息
        {
            int writeID = AssistDll.ChangeNameToID("user_name", msg.writer, "user_login_info");
            int receiverID = AssistDll.ChangeNameToID("user_name", msg.sender, "user_login_info");
            String message = "发送成功";
            BaseConnection conn = new BaseConnection("messages");
            try
                {
                    conn.BaseSP("sp_note_addmessage", AssistDll.ParaConstructor(new String[] {
                "sender_id",
                "receiver_id",
                "message",
                "message_level",
                }, new Object[] { writeID, receiverID, msg.message, msg.messageLevel }));
                }
                catch (Exception e)
                {
                    message = "您输入的信息有误";
                    CommonPara.ErrorMessage = e.Message;
                }
            return message;
        }
        public String DeleteMessage(Message msg) //删除一条消息
        {
            String message = "发送成功";
            BaseConnection conn = new BaseConnection("messages");
            try
            {
                conn.BaseSelect(AssistDll.DeleteConstructor("id=" + msg.ID, "messages"));

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
