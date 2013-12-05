using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WinNoteLib;

namespace WinNote
{
    /// <summary>
    /// NoteWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NoteWindow : Window
    {
        private static bool isMain;
        private PersonNote rootNote;
        private PersonNote rootGroup;
        private PersonNote[] Folder;
        private PersonNote[] Note;
        private PersonNote Group;
        private PersonNote[] Member;
        public NoteWindow()
        {
            InitializeComponent();
            rootNote = new PersonNote("My Document", 0);
            rootGroup = new PersonNote("My Group", 0);
            Lb_username.Content = " " + CommonPara.tempUserName;
            isMain = true;
            BuildTree();
            BuildGroup();
        }

        public class PersonNote:TreeViewItem
        {
            private int Level {get;set;}
            public PersonNote(string Displayname, int level)
            {
                this.Header = Displayname;
                this.Level = level;
            }
            
        }

        public void BuildGroup()
        {
            UserGroupManager init_grop = new UserGroupManager();
            CommonPara.tempGroupName=init_grop.getMyGroupName(CommonPara.tempUserID);
            Group = new PersonNote(CommonPara.tempGroupName, 0);
            
            int i = 0;
            if ( CommonPara.tempGroupName != null)
            {
                string[] grop_member = init_grop.getGroupMember(init_grop.getGroupID(CommonPara.tempGroupName));
                Member = new PersonNote[grop_member.Length];
                foreach (string temp_member in grop_member)
                {
                    Member[i++] = new PersonNote(temp_member, 2);
                    Group.Items.Add(Member[i - 1]);
                }
                rootGroup.Items.Add(Group);

            }
            Trev_groupView.Items.Add(rootGroup);
            
	    }

        public void BuildTree()
        {
            NoteBookManager init_mag = new NoteBookManager();
            List<NoteBook> bookList = init_mag.getPersonalNoteBook(CommonPara.tempUserID);
            NoteManager note_mag = new NoteManager();
            List<OnlineNote> onlineList = note_mag.getPersonalNote(CommonPara.tempUserID);
            string[] bookName = new string[bookList.Count];
            string[] noteName = new string[onlineList.Count];
            Folder = new PersonNote[bookList.Count];
            Note = new PersonNote[onlineList.Count];
            int i = 0;
            int j = 0;
            foreach (NoteBook temp in bookList)
            {
                bookName[i++] = temp.name;
                Folder[i-1] = new PersonNote(temp.name, 1);
                rootNote.Items.Add(Folder[i-1]);
                foreach(OnlineNote newNote in onlineList)//取出书
                {
                    if( newNote.ID == temp.ID )
                    {
                        Note[j++] = new PersonNote(newNote.title, 2);
                        Folder[i-1].Items.Add(Note[j-1]);
                    }
                }
            }
            Trev_noteView.Items.Add(rootNote);
        }

        private void MoveNote(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }  
        }

        private void Click_changemodel(object sender, RoutedEventArgs e)
        {
            if (isMain == false)
            {
                this.Cvs_model.Children.Clear();
                RichTextBox mainPage = new RichTextBox();
                mainPage.Width = 590;
                mainPage.Height = 530;
                mainPage.Margin = new Thickness(0, 0, 0, 0);
                mainPage.BorderThickness = new Thickness(0, 0, 0, 0);
                Cvs_model.Children.Add(mainPage);
                isMain = true;
            }
            else
            {
                this.Cvs_model.Children.Clear();
                Cvs_model.Children.Add(Cvs_note);
                isMain = false;
            }
        }

        private void Click_close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Click_logout(object sender, RoutedEventArgs e)
        {
            this.Close();
            Login x = new Login();
            x.Show();
        }

        private void Click_save(object sender, RoutedEventArgs e)
        {
            FlowDocument document = Rtb_note.Document;
            System.IO.Stream s = new System.IO.MemoryStream();
            System.Windows.Markup.XamlWriter.Save(document, s);
            byte[] data = new byte[s.Length];
            s.Position = 0;
            s.Read(data, 0, data.Length);
            s.Close();
            OnlineNote myNote = new OnlineNote(new string[] { "s", "b", "c" }, 12, 1, "ss", "ssbb", data);
            NoteManager myMag = new NoteManager();
            myMag.OnlineNoteSaving(myNote);


        }

        private void Click_newteacher(object sender, RoutedEventArgs e)
        {
            SystemRequest newTeacher = new SystemRequest();
            newTeacher.wantToLevelUp(CommonPara.tempUserName);
        }

        /*private void aaa(object sender, RoutedEventArgs e)
        {
            NoteManager x = new NoteManager();
            List<OnlineNote> p = x.getPersonalNoteBook(CommonPara.tempUserID);
            byte[] read = p[1].doc;
            System.IO.Stream s = new System.IO.MemoryStream(read);

            //System.Windows.Markup.XamlReader.Load(s);
            Rtb_note.Document = (FlowDocument)System.Windows.Markup.XamlReader.Load(s);
        }*/
    }
}
