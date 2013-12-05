using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinNoteLib
{
    public class Message
    {
        public int ID;
        public String sender;
        public String writer;
        public String message;
        public int messageLevel;
        public Message(String sender, String writer, String message, int messageLevel)
        {
            this.sender = sender;
            this.writer = writer;
            this.message = message;
            this.messageLevel = messageLevel;
        }
    }
}
