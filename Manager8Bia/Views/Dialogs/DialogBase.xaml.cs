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

namespace Manager8Bia.Views.Dialogs
{
    /// <summary>
    /// Interaction logic for DialogBase.xaml
    /// </summary>
    public partial class DialogBase : UserControl
    {
        public DialogBase()
        {
            InitializeComponent();
        }

        public DialogBase(string title, UIElement content)
        {
            InitializeComponent();

            this.tbTitle.Text = title;
            this.gContent.Children.Add(content);
        }
    }
}
