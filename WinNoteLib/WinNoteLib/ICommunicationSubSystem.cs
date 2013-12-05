using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinNoteLib
{
    public interface ICommunicationSubSystem
    {
         List<Message> GetMessageList(int userID);
         String SendAMessage(Message msg);
         String DeleteMessage(Message msg);
         List<Comment> getAllComments(int noteID);
         String AddComment(Comment comment);
         String DeleteComment(Comment comment);

    }
}
