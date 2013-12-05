using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace WinNoteLib
{
    public class NoteBookManager 
    {
        
        public List<NoteBook> getPersonalNoteBook(int id) //获取当前用户的笔记本列表
        {
            List<NoteBook> noteBook=new List<NoteBook>();
            BaseConnection conn=new BaseConnection("note_book");
            DataTable dt = new DataTable();
            try
            {
                dt = conn.BaseSelect(AssistDll.SelectConstructor(new string[] { "*" }, "owner_id=\'" + id.ToString() + "\'", "note_book"));
                foreach (DataRow dr in dt.Rows)
                {
                    NoteBook tempBook = new NoteBook((int)dr.ItemArray[0], (string)dr.ItemArray[1], (int)dr.ItemArray[2]);
                    noteBook.Add(tempBook);
                }
            }
            catch(Exception e)
            {
                CommonPara.ErrorMessage = e.Message;
            }
            return noteBook;
        }
        public String CreateNewNoteBook(NoteBook noteBook) //创建一本新的笔记本
        {
            String message = "发送成功";
            BaseConnection conn = new BaseConnection("note_book");
            try
            {
                conn.BaseSP("sp_note_addnotebook", AssistDll.ParaConstructor(new String[] {
                "name",
                "owner_id"
                }, new Object[] { noteBook.name, noteBook.owner }));
            }
            catch (Exception e)
            {
                message = "您输入的信息有误";
                CommonPara.ErrorMessage = e.Message;
            }
            return message;
        }

        public String ModifyNoteBookInfo(String newTitle,int noteBookID) //创建一本新的笔记本
        {
            String message = "发送成功";
            BaseConnection conn = new BaseConnection("note_book");
            try
            {
                conn.BaseSP("sp_note_editnotebook", AssistDll.ParaConstructor(new String[] {
                "name",
                "note_id"
                }, new Object[] { newTitle, noteBookID }));
            }
            catch (Exception e)
            {
                message = "您输入的信息有误";
                CommonPara.ErrorMessage = e.Message;
            }
            return message;
        }
        public String DeleteNoteBook(NoteBook noteBook)//删除输入的笔记本
        {
            String message = "发送成功";
            BaseConnection conn = new BaseConnection("note_book");
            try
            {
                conn.BaseSelect(AssistDll.DeleteConstructor("id=" + noteBook.ID, "note_book"));
            }
            catch (Exception e)
            {
                message = "您输入的信息有误";
                CommonPara.ErrorMessage = e.Message;
            }
            return message;
        }
        public String deleteBook(int bookID) //删除输入的笔记本，存储过程形式
        {
            String message = "删除成功";
            BaseConnection conn = new BaseConnection("note_book");

            try
            {
                conn.BaseSP("sp_note_delete_note_book", AssistDll.ParaConstructor(new String[] {
                "book_id"
                 }, new Object[] { bookID }));
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
