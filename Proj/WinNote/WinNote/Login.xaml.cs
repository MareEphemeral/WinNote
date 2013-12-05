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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WinNoteLib;
using System.Runtime.InteropServices;

namespace WinNote
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class Login : Window
    {

        private Register reg = new Register();
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Validate()
        {
            LoginControler login = new LoginControler();
            IntPtr s = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(this.Txb_password.SecurePassword);
            string str_pas = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(s);
            string userlevel = login.Validate(Txb_username.Text, str_pas);
            if (userlevel == "1")
            {
                this.Close();
                NoteWindow std_note = new NoteWindow();
                std_note.Show();
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Login_Validate();
        }

        private void Click_Register(object sender, RoutedEventArgs e)
        {
            reg.Show();
            
        }

        private void Click_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
            reg.Close();
        }

        private void WindowMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }  
        }
    }
}
