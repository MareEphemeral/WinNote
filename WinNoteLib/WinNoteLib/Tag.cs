using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinNoteLib
{
    public class Tag
    {
        public int tagID;
        public String tagName;
        public int hotPoint;
        public Tag(int tagID,  String tagName,  int hotPoint)
        {
            this.tagID = tagID;
            this.tagName=tagName;
            this.hotPoint = hotPoint;
        }
        public Tag()
        {
        }
    }
}
