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
    /// InputBox.xaml 的交互逻辑
    /// </summary>
    public partial class InputBox : Window
    {
        public static int operation;
        public InputBox()
        {
            InitializeComponent();
            operation = 0;
            
        }

        private void Click_conf(object sender, RoutedEventArgs e)
        {
            switch(operation)
            {
                case 0:
                    {   if(Register.opTag > 2)
                        {
                            this.Close();
                            break;
                        }
                        else
                        {
                            WinNoteLib.TagsHandler p = new TagsHandler();
                            p.createNewTag(Txt_info.Text);
                            Register.newTagName = Txt_info.Text;
                            Register.opTag++;
                            this.Close();
                            break;
                        }
                    }     
                case 1:
                    {
                        break;
                    }
            }
        }

    }
}
