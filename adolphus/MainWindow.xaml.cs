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
using System.Xml;

namespace adolphus
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ToDoList tdl = new ToDoList();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = tdl;
        }
        public void maddfunc()
        {
            DataContext = tdl;
            lvToDo.Items.Refresh();
        }

        private void drag(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!txtItemDesc.Text.Trim().Equals(string.Empty))
            {
                tdl.Additem(txtItemDesc.Text.Trim());
            }
            txtItemDesc.Text = "";
            lvToDo.Items.Refresh();

            todolistmain apage = new todolistmain();
            apage.func();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            tdl.Save();

            todolistmain apage = new todolistmain();
            apage.func();
        }

        private void MarkAsDone(object sender, RoutedEventArgs e)
        {
            if (lvToDo.SelectedItem != null)
            {
                MessageBoxResult done = MessageBox.Show("Mark this as done?", "Done?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (done == MessageBoxResult.Yes)
                {
                    (lvToDo.SelectedItem as ToDoItem).DoneDateTime = DateTime.Now.ToString("yyyy-MM-ddThh:mm:ss.ms");
                }
                lvToDo.Items.Refresh();
            }
        }

        private void lvToDo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MarkAsDone(sender, e);
        }

        private void EditMenu_Click(object sender, RoutedEventArgs e)
        {
            if (lvToDo.SelectedItem != null)
            {
                EditPopup ep = new EditPopup(lvToDo.SelectedItem as ToDoItem);
                ep.Owner = this;
                ep.ShowDialog();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (lvToDo.SelectedItem != null)
            {
                MessageBoxResult del = MessageBox.Show("Delete this item?", "Delete?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (del == MessageBoxResult.Yes)
                {
                    tdl.DeleteItem(lvToDo.SelectedItem as ToDoItem);
                }
                lvToDo.Items.Refresh();
            }
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
