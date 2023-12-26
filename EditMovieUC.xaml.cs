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

namespace MovieApp_WPF
{
    /// <summary>
    /// Interaction logic for EditMovieUC.xaml
    /// </summary>
    public partial class EditMovieUC : UserControl
    {
        public EditMovieUC()
        {
            InitializeComponent();
        }

        public EventHandler<EventArgs> editButton_Click { get;  set; }
        public bool IsClicked { get; set; } = false;
        private void changeNameBtn_Click(object sender, RoutedEventArgs e)
        {

                editButton_Click.Invoke(sender, e);
                this.movieNameLbl.Content = this.newNameTxtb.Text;
        }
    }
}
