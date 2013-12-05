using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
namespace WinNoteLib
{
    [Serializable] 
    public class NoteBase
    {
        public byte[] doc;
        public String title;
        public String digest;
        public NoteBase() { }
        public NoteBase(byte[] doc, String title, String digest)
        {
            this.doc = doc;
            this.title = title;
            this.digest = digest;
        }

    }
}
