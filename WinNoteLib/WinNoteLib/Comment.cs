using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinNoteLib
{
    public class Comment
    {
        public String comment;
        public int writer_id;
        public int note_id;
        public int ID;
        public Comment()
        {

        }
        public Comment(String comment,int writer_id,int note_id)
        {
            this.writer_id = writer_id;
            this.note_id = note_id;
            this.comment = comment;
        }
    }
}
