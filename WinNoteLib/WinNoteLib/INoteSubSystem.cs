using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinNoteLib
{
    public interface INoteSubSystem
    {
         List<OnlineNote> getHotNotes(string like);
         String OnlineNoteSaving(OnlineNote note);
         List<OnlineNote> getPersonalNote(int id);
         String NoteSavingLocalMode(NoteBase note);
         NoteBase NoteLoadingLocalMode(String filePath);
         List<OnlineNote> SearchNote(String noteName);
         List<OnlineNote> GetNote(String noteName);
         String DeleteNote(OnlineNote note);
         List<NoteBook> getPersonalNoteBook(int id);
         String CreateNewNoteBook(NoteBook noteBook);
         String DeleteNoteBook(NoteBook noteBook);
         String deleteBook(int bookID);

    }
}
