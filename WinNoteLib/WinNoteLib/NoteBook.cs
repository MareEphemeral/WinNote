using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinNoteLib
{
    public class NoteBook
    {
        public int ID;
        public String name;
        public int owner;
        public NoteBook(int ID,String name,int owner)
        {
            this.ID = ID;
            this.name = name;
            this.owner = owner;
        }
    }
}