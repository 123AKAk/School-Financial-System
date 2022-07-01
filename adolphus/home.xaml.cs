using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using System.Windows.Threading;

namespace adolphus
{
    /// <summary>
    /// Interaction logic for home.xaml
    /// </summary>
    public partial class home : Window
    {
        private DispatcherTimer htimer;

        int mainId;
        string position;
        public home()
        {
            InitializeComponent();

            mainId = mainLogin.userId;
            account.Content = mainLogin.userName;
            position = mainLogin.userPosition;
            //automatically click the dashboard button on window load
            typeof(System.Windows.Controls.Primitives.ButtonBase).GetMethod("OnClick", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(dashboardBtn, new object[0]);

            getImage();
        }

        private void getImage()
        {

            MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
            MySqlCommand ccommand;
            var cQuery = "SELECT * FROM login WHERE Id='"+mainId+"'";
            ccommand = new MySqlCommand(cQuery, cconnection);
            try
            {
                cconnection.Open();
                MySqlDataReader readerc = ccommand.ExecuteReader();
                while (readerc.Read())
                {                                        
                    //Store binary data read from the database in a byte array
                    byte[] blob = (byte[])readerc["Image"];
                    MemoryStream stream = new MemoryStream();
                    stream.Write(blob, 0, blob.Length);
                    stream.Position = 0;

                    System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                    BitmapImage bi = new BitmapImage();
                    bi.BeginInit();

                    MemoryStream ms = new MemoryStream();
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                    ms.Seek(0, SeekOrigin.Begin);
                    bi.StreamSource = ms;
                    bi.EndInit();
                    image.Source = bi;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            startup objOpen = new startup();
            mainframe.NavigationService.Navigate(objOpen);            
        }
        private void drag(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }      
        private void minimise(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void maximise(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;

                maxia.Visibility = Visibility.Hidden;
                resto.Visibility = Visibility.Visible;
                maxi.ToolTip = "Restore Down";
            }
            else if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;

                resto.Visibility = Visibility.Hidden;
                maxia.Visibility = Visibility.Visible;
                maxi.ToolTip = "Maximise";
            }
        }
        private void closea(object sender, RoutedEventArgs e)
        {            
            this.Close();            
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {         
            //this.Close();
        }
        private void timer_tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sideBbtnEnter(object sender, MouseEventArgs e)
        {
            Button sideBbtn = (Button)sender;

            //setting hover color for sidebtn when mouse enters            
            var hover_color = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF800080");
            //var hover_background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FFFFFFFF");
            sideBbtn.Foreground = hover_color;
            //sideBbtn.Background = hover_background;
        }

        private void sideBbtnLeave(object sender, MouseEventArgs e)
        {
            Button sideBbtn = (Button)sender;

            if(sideBbtn.Name == clcikedB & btnIsClicked == true)
            {

            }
            else
            {
                //setting hover color for sidebtn when mouse leave
                var hover_color = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FFA98BAE");
                //var hover_background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#00000000");
                sideBbtn.Foreground = hover_color;
                //sideBbtn.Background = hover_background;
            }
        }

        string clcikedB;
        bool btnIsClicked;
        public Button targetbtn;
        private void sidebutton_Click(object sender, RoutedEventArgs e)
        {
            targetbtn = (Button)sender;
            var default_color = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FFA98BAE");
            button1.Foreground = default_color;button2.Foreground = default_color;button3.Foreground = default_color;
            button4.Foreground = default_color;button5.Foreground = default_color;button6.Foreground = default_color;
            dashboardBtn.Foreground = default_color; deletionBtn.Foreground = default_color; analyticsBtn.Foreground = default_color;
            account.Foreground = default_color;

            clcikedB = targetbtn.Name;

            var m_color = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF800080");            
            switch (targetbtn.Name.ToString())
            {
                case "button1":
                    if (position == "Admin" || position == "Receptionist")
                    {
                        addstudent objOpen1 = new addstudent();
                        mainframe.NavigationService.Navigate(objOpen1);
                        targetbtn.Foreground = m_color;
                    }
                    else
                    {
                        MessageBox.Show("You Don't have Permission to access this page.", "No Access:", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                    }
                    break;
                case "button2":
                    if (position == "Admin" || position == "Receptionist")
                    {
                        teachers objOpen2 = new teachers();
                        mainframe.NavigationService.Navigate(objOpen2);
                        targetbtn.Foreground = m_color;
                    }
                    else
                    {
                        MessageBox.Show("You Don't have Permission to access this page.", "No Access:", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                    }
                    break;
                case "button3":
                    if (position == "Admin" || position == "Receptionist" || position == "Busar")
                    {
                        view_edit_student objOpen3 = new view_edit_student();
                        mainframe.NavigationService.Navigate(objOpen3);
                        targetbtn.Foreground = m_color;
                    }
                    else
                    {
                        MessageBox.Show("You Don't have Permission to access this page.", "No Access:", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                    }
                    break;
                case "button4":
                    if (position == "Admin" || position == "Accountant")
                    {
                        view_edit_teachers objOpen4 = new view_edit_teachers();
                        mainframe.NavigationService.Navigate(objOpen4);
                        targetbtn.Foreground = m_color;
                    }                    
                    else
                    {
                        MessageBox.Show("You Don't have Permission to access this page.", "No Access:", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                    }
                    break;
                case "button5":
                    if (position == "Admin" || position == "Busar" || position == "Accountant")
                    {
                        expenditure objOpen5 = new expenditure();
                        mainframe.NavigationService.Navigate(objOpen5);
                        targetbtn.Foreground = m_color;
                    }
                    else
                    {
                        MessageBox.Show("You Don't have Permission to access this page.", "No Access:", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                    }
                    break;
                case "button6":
                    if (position == "Admin")
                    {
                        add_items objOpen6 = new add_items();
                        mainframe.NavigationService.Navigate(objOpen6);
                        targetbtn.Foreground = m_color;
                    }
                    else
                    {
                        MessageBox.Show("You Don't have Permission to access this page.", "No Access:", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                    }
                    break;
                case "dashboardBtn":                    
                        startup objOpen7 = new startup();
                        mainframe.NavigationService.Navigate(objOpen7);
                        targetbtn.Foreground = m_color;                    
                    break;
                case "deletionBtn":
                    if (position == "Admin")
                    {
                        deleteAll objOpen8 = new deleteAll();
                        mainframe.NavigationService.Navigate(objOpen8);
                        targetbtn.Foreground = m_color;
                    }
                    else
                    {
                        MessageBox.Show("You Don't have Permission to access this page.", "No Access:", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                    }
                    break;
                case "analyticsBtn":
                    if (position == "Admin")
                    {
                        total_system objOpen9 = new total_system();
                        var Result = MessageBox.Show("Analysis is Best done when there are no users currently active on the Application. \n \nAre You sure you want to Continue?", "Question:", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                        if (Result == MessageBoxResult.Yes)
                        {
                            mainframe.NavigationService.Navigate(objOpen9);
                            targetbtn.Foreground = m_color;
                        }
                        else if (Result == MessageBoxResult.No)
                        {
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("You Don't have Permission to access this page.", "No Access::", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                    }
                    break;
                case "account":                  
                        accountPage objOpen10 = new accountPage();
                        mainframe.NavigationService.Navigate(objOpen10);
                        targetbtn.Foreground = m_color;                    
                    break;
            }
            btnIsClicked = true;            
        }

        private void button7_Click(object sender, RoutedEventArgs e)
        {
            //logout
            mainLogin openWin = new mainLogin();
            openWin.Show();
            this.Close();
        }

        private void Grid_TouchEnter(object sender, TouchEventArgs e)
        {
            MessageBox.Show("madddd");
        }

        private void Grid_GotFocus(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("madddd");
        }
    }
}
