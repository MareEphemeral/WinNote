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
    /// Register.xaml 的交互逻辑
    /// </summary>
    public partial class Register : Window
    {
        private string sex;
        public static string newTagName;
        public static int opTag = 0;
        private static TagsHandler init_tag;
        public Register()
        {
            InitializeComponent();
            sex = "male";
            List<int> ageList = new List<int>();
            for( int i = 14; i < 80; i++)
            {
                ageList.Add(i);
            }
            Combo_age.ItemsSource = ageList;
            init_tag = new TagsHandler();
            WinNoteLib.Tag[] hottags = new WinNoteLib.Tag[8];
            hottags = init_tag.getHotTags();
            Btn_hottag1.Content = hottags[0].tagName;
            Btn_hottag2.Content = hottags[1].tagName;
            Btn_hottag3.Content = hottags[2].tagName;
            Btn_hottag4.Content = hottags[3].tagName;
            Btn_hottag5.Content = hottags[4].tagName;
            Btn_hottag6.Content = hottags[5].tagName;
            Btn_hottag7.Content = hottags[6].tagName;
        }

        private void WindowMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }  
        }

        private void Click_confirm(object sender, RoutedEventArgs e)
        {
            UserRegister newCustomer = new UserRegister();
            User newUser = new User();
            IntPtr s = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(this.Txt_Password.SecurePassword);
            string str_pas = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(s);
            IntPtr p = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(this.Txt_ComfirmPassword.SecurePassword);
            string cof_str_pas = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(p);
           
            
            if( Txt_Username.Text.Length > 6)
            {
                Lb_username.Content = "Please make your name within 6 letters!";
            }
            else if( str_pas.Length > 8 )
            {
                Lb_pas.Content = "Please make your password within 8 letters!";
            }
            else if( str_pas != cof_str_pas ){
                Lb_conf.Content = "Please input your correct password again!";
            }
            else{
               // newUser.NewUser(Txt_Username.Text, str_pas, Txt_RealName.Text, Convert.ToInt32(Combo_age.Text), sex, Txt_School.Text, Txt_Email.Text, 
               // newCustomer.Register(newUser);
            }

            

          
        }

        private void Click_sex(object sender, RoutedEventArgs e)
        {
            if (sex == "female")
            {
                sex = "male";
            }
            else { }
        }

        private void Click_sex2(object sender, RoutedEventArgs e)
        {
            if( sex == "male")
            {
                sex = "female";
            }
            else { }
        }

        private void Click_addTag(object sender, RoutedEventArgs e)
        {
            InputBox inTag = new InputBox();
            inTag.ShowDialog();
            switch(opTag)
            {
                case 0: Btn_tag1.Content = newTagName; break;
                case 1: Btn_tag2.Content = newTagName; break;
                case 2: Btn_tag3.Content = newTagName; break;
            }
           

        }

        private void Click_tag1(object sender, RoutedEventArgs e)
        {
            if(opTag > 0 && opTag <= 3)
            {
                Btn_tag1.Content = "";
            }
        }
    }
}
