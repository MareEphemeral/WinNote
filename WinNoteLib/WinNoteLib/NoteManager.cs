using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
namespace WinNoteLib
{
    public class NoteManager 
    {

        public List<OnlineNote> getHotNotes(string like) //获取首页所需的推荐笔记
        {
           List<OnlineNote> hotNote = new List<OnlineNote>();
           BaseConnection conn=new BaseConnection("note_info");
           int[] tagsInt=TagsHandler.tagsTrans(AssistDll.tagsDearchive(like));
           DataTable dt=new DataTable();
           try
           {
               dt = conn.BaseSelect(
               AssistDll.TopSelectConstructor(new string[] { "*" }, "tag1=" + tagsInt[0] + " or " + "tag2=" + tagsInt[0] + " or " + "tag3=" + tagsInt[0] + " or " +
                                                                    "tag1=" + tagsInt[1] + " or " + "tag2=" + tagsInt[1] + " or " + "tag3=" + tagsInt[1] + " or " +
                                                                    "tag1=" + tagsInt[2] + " or " + "tag2=" + tagsInt[2] + " or " + "tag3=" + tagsInt[2], "note_info", 8, true, "value"));
               foreach (DataRow dr in dt.Rows)
               {
                   int[] tagsInt2 = new int[3] { (int)dr.ItemArray[4], (int)dr.ItemArray[8], (int)dr.ItemArray[9] };
                   string[] tags = TagsHandler.reTagsTrans(tagsInt2);
                   OnlineNote tempNote = new OnlineNote((int)dr.ItemArray[0], tags, (int)dr.ItemArray[5], (int)dr.ItemArray[2], (string)dr.ItemArray[1], (string)dr.ItemArray[3], (byte[])dr.ItemArray[6]);
                   hotNote.Add(tempNote);
               }
           }
            catch(Exception e)
           {
               CommonPara.ErrorMessage = e.Message;
           }
           return hotNote;
        }
        public String OnlineNoteSaving(OnlineNote note) //存一篇笔记入数据库
        {

            String result = "Success";
            BaseConnection conn = new BaseConnection("note_info");
            String[] tags=note.tags;
            int[] tagsInt = TagsHandler.tagsTrans(tags);
            try
            {
                if (conn.BaseSP("sp_note_addnote",
                                   AssistDll.ParaConstructor(new string[] { "title", "writer", "digest", "tag1", "book", "article", "tag2", "tag3" },
                                   new object[] { note.title, note.writer, note.digest, tagsInt[0], note.bookID, note.doc, tagsInt[1], tagsInt[2] })) <= 0)
                {
                    result = "Fail";
                }
            }
            catch(Exception e)
            {
                result = "fail";
                CommonPara.ErrorMessage = e.Message;
            }
             return result;
        }

        public List<OnlineNote> getPersonalNote(int id) //获取指定用户的所有笔记
        { 
            List<OnlineNote> note = new List<OnlineNote>();
            note = OnlineNoteReader("writer=\'" + id.ToString() + "\'");
            return note;
        }
        private List<OnlineNote> OnlineNoteReader(String situation) //按照条件搜索复合的笔记
        {
            List<OnlineNote> noteList=new List<OnlineNote>();
            bool flag = true;
            BaseConnection conn = new BaseConnection("note_info");
            DataTable dt=new DataTable();
            try
            {
                dt = conn.BaseSelect(AssistDll.SelectConstructor(situation, "note_info"));
                foreach (DataRow dr in dt.Rows)
                {
                    int[] tagsInt = new int[3] { (int)dr.ItemArray[4], (int)dr.ItemArray[8], (int)dr.ItemArray[9] };
                    string[] tags = TagsHandler.reTagsTrans(tagsInt);
                    OnlineNote tempNote = new OnlineNote((int)dr.ItemArray[0], tags, (int)dr.ItemArray[5], (int)dr.ItemArray[2], (string)dr.ItemArray[1], (string)dr.ItemArray[3], (byte[])dr.ItemArray[6]);
                    noteList.Add(tempNote);
                }
            }
            catch(Exception e)
            {
                flag = false;
                CommonPara.ErrorMessage = e.Message;
            }
            return noteList;
        }
        public String NoteSavingLocalMode(NoteBase note) //将一篇笔记以wnt文件形式存至目录下
        {
            String result = "Success";
            byte[] biNote = AssistDll.Serialize(note);
            String tempFileName=CommonPara.tempUserID+@"-"+note.title+".wnt";

            FileStream fs = null;
            BinaryWriter bw = null;
            try
            {
                if (File.Exists(tempFileName))
                {
                    File.Delete(tempFileName);
                }
                fs = new FileStream(tempFileName, FileMode.Create, FileAccess.Write);
                bw = new BinaryWriter(fs);

                foreach(byte data in biNote)
                {
                     bw.Write(data);
                }
                bw.Close();
                fs.Close();
                bw = null;
                fs = null;
            }
            catch (Exception e)
            {
                CommonPara.ErrorMessage=e.Message;
                result = "Fail";;
            }
            finally
            {
                try
                {
                    bw.Close();
                    fs.Close();
                }
                catch(Exception e){
                    CommonPara.ErrorMessage = e.Message;
                }

            }
            return result;
        }
        public NoteBase NoteLoadingLocalMode(String filePath) //从本地读一篇笔记
        {
            FileStream fs = null;
            BinaryReader br = null;
            String result = "Success";
            NoteBase note = new NoteBase();
            try
            {
                if (!File.Exists(filePath))
                {
                    result="不存在";
                }


                fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                br = new BinaryReader(fs);
                List<byte> bt = new List<byte>();
                try{
                    while (true){
                         bt.Add(br.ReadByte());
                                }
                    }
                    catch(Exception e){
                        CommonPara.ErrorMessage = e.Message;
                    }

                byte[] biNote = new byte[bt.Count];
                for (int i = 0; i < bt.Count;i++ )
                {
                    byte temp = biNote[i];
                    biNote[i] = bt[i];

                }
                note = (NoteBase)AssistDll.Deserialize(biNote);

                br.Close();
                fs.Close();
                br = null;
                fs = null;
            }
            catch (Exception e)
            {
                result = "fail";
                CommonPara.ErrorMessage = e.Message;            
            }
            finally
            {
                try{
                br.Close();
                fs.Close();
                                }
                catch(Exception e){
                    CommonPara.ErrorMessage = e.Message;
                }
            }

            return note;
        }

        public String AddBookValue(String bookName) //将一篇日记转为推荐级
        {
            String message = "推荐成功";
            BaseConnection conn = new BaseConnection("note_info");
            try
            {
                conn.BaseSP("sp_note_editvalue", AssistDll.ParaConstructor(new String[] {
                "book_name"
                }, new Object[] { bookName }));
            }
            catch (Exception e)
            {
                message = "您输入的信息有误";
                CommonPara.ErrorMessage = e.Message;
            }
            return message;
        }

        public List<OnlineNote> SearchNote(String noteName) //查询包含输入关键字的笔记
        {
            return OnlineNoteReader("title like " + "\'%" + noteName + "%\'");
        }
        public List<OnlineNote> GetNote(String noteName) //获取指定标题的笔记
        {
            return OnlineNoteReader("title =\'" + noteName + "\'");
        }
        public String DeleteNote(OnlineNote note) //删除一篇指定笔记
        {
            String message = "发送成功";
            BaseConnection conn = new BaseConnection("note_info");
            try
            {
                conn.BaseSelect(AssistDll.DeleteConstructor("id=" + note.ID, "note_info"));

            }
            catch (Exception e)
            {
                message = "您输入的信息有误";
                CommonPara.ErrorMessage = e.Message;
            }
            return message;
        }

        public String ModifyNoteInfo(String newTitle,byte[] newArticle,int   noteID) //创建一本新的笔记本
        {
            String message = "发送成功";
            BaseConnection conn = new BaseConnection("note_info");
            try
            {
                conn.BaseSP("sp_note_editnote", AssistDll.ParaConstructor(new String[] {
                "title",
                "article",
                "note_id"
                }, new Object[] { newTitle, newArticle, noteID }));
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
