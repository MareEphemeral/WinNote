using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinNoteLib
{
    [Serializable] 
    public class OnlineNote : NoteBase
    {
        public int ID;
        public String[] tags;
        public int bookID;
        public int writer;
        public OnlineNote(int id, String[] tags, int bookID, int writer, String title, String digest, byte[] doc)
        {
            this.ID = id;
            this.tags = tags;
            this.bookID = bookID;
            this.writer = writer;
            this.title = title;
            this.doc = doc;
            this.digest = digest;
        }
        public OnlineNote(String[] tags, int bookID, int writer, String title, String digest, byte[] doc)
        {
            this.tags = tags;
            this.bookID = bookID;
            this.writer = writer;
            this.title = title;
            this.doc = doc;
            this.digest = digest;
        }

    }
}
