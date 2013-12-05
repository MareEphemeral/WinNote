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
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            TreeViewItem newItem = new TreeViewItem();
            newItem.Tag = "aaa";
            newItem.Header = "bbb";
            newItem.Items.Add("*");

            

        }

        private void WindowMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }  
        }

        private void newtext(object sender, RoutedEventArgs e)
        {
                   
            /*FlowDocument document = AB.Document;
            System.IO.Stream s = new System.IO.MemoryStream();
            System.Windows.Markup.XamlWriter.Save(document, s);
            byte[] data = new byte[s.Length]; 
            s.Position = 0;
            s.Read(data, 0, data.Length);
            s.Close();
            NoteBase myNote = new NoteBase(data, "Fir", "SXJIE");
            NoteManager myMag = new NoteManager();
            OnlineNote onlineNote = new OnlineNote(new string[] { "a", "b", "c" }, 12, 23, "ccc", "dowkdc", data);
            myMag.OnlineNoteSaving(onlineNote);
            myMag.NoteSavingLocalMode(myNote);*/
        }
    }
}
