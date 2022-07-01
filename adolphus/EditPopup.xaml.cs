﻿using System;
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

namespace adolphus
{
    /// <summary>
    /// Interaction logic for EditPopup.xaml
    /// </summary>
    public partial class EditPopup : Window
    {
        private string tempdesc;

        public EditPopup(ToDoItem item)
        {
            InitializeComponent();
            DataContext = item;
            tempdesc = item.Description;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            bool result = DialogResult ?? false;
            if (!result)
                (DataContext as ToDoItem).Description = tempdesc;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            MainWindow awin = new MainWindow();
            awin.maddfunc();

            todolistmain apage = new todolistmain();
            apage.func();

            DialogResult = true;
            Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
