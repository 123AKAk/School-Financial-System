using MySql.Data.MySqlClient;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace adolphus
{
    /// <summary>
    /// Interaction logic for mainLogin.xaml
    /// </summary>
    public partial class mainLogin : Window
    {
        public mainLogin()
        {
            InitializeComponent();

            username.Text = Settings1.Default.nameS;
        }
        
        private DispatcherTimer htimer;
        Random _random = new Random();

        int numbers = 0;

        private void drag(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //page loaded display pictures        
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {                        
            BitmapImage bitmapImage = new BitmapImage(new Uri(@"\images\stud"+ 0 + ".jpg", UriKind.Relative));
            sliderImageLogin.Source = bitmapImage;

            htimer = new DispatcherTimer();
            htimer.Interval = new TimeSpan(0, 0, 5);
            htimer.Tick += timer_tick;
            htimer.Start();            
        }
        // all timing starts here        
        private void timer_tick(object sender, EventArgs e)
        {
            numbers++;
            DoubleAnimation badass = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = new Duration(TimeSpan.FromSeconds(0)),
                AutoReverse = false
            };
            sliderImageLogin.BeginAnimation(OpacityProperty, badass);
            sliderImageLogin.Opacity = 100;
            
            if (numbers == 3)
            {
                numbers = 0;
                BitmapImage bitmapImage = new BitmapImage(new Uri(@"\images\stud" + numbers + ".jpg", UriKind.Relative));
                sliderImageLogin.Source = bitmapImage;
            }
            else
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri(@"\images\stud" + numbers + ".jpg", UriKind.Relative));
                sliderImageLogin.Source = bitmapImage;
            }
            htimer.Stop();
            htimer.Start();
        }
        private void BbtnEnter(object sender, MouseEventArgs e)
        {
            Button sideBbtn = (Button)sender;

            //setting hover color for sidebtn when mouse enters            
            var hover_color = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF800080");
            //var hover_background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FFFFFFFF");
            sideBbtn.Foreground = hover_color;
            //sideBbtn.Background = hover_background;
        }

        private void BbtnLeave(object sender, MouseEventArgs e)
        {
            Button sideBbtn = (Button)sender;
            //setting hover color for sidebtn when mouse leave
            var hover_color = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF7E467E");
            //var hover_background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#00000000");
            sideBbtn.Foreground = hover_color;
            //sideBbtn.Background = hover_background;
        }

        public static string userName = "";
        public static string userPosition = "";
        public static int userId = 0;

        DispatcherTimer timer = new DispatcherTimer();
        private void login_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation doubleanimation = new DoubleAnimation();
            doubleanimation.From = 2;
            doubleanimation.To = 45;
            doubleanimation.Duration = TimeSpan.FromSeconds(1);

            doubleanimation.EasingFunction = new QuarticEase();

            if (username.Text != "" & password.Password != "")
            {

                int idd = 0;
                string position = "";
                MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
                MySqlCommand ccommand;
                var cQuery = "SELECT * FROM login WHERE Username='" + username.Text + "'AND Password='" + password.Password + "'";
                ccommand = new MySqlCommand(cQuery, cconnection);
                try
                {
                    cconnection.Open();
                    MySqlDataReader readerc = ccommand.ExecuteReader();
                    while (readerc.Read())
                    {
                        idd = Convert.ToInt32(readerc["Id"].ToString());
                        position = readerc["Position"].ToString();                        
                    }

                    if (idd != 0 && position != "")
                    {
                        userId = idd;
                        userName = username.Text;
                        userPosition = position;

                        Settings1.Default.nameS = username.Text;
                        Settings1.Default.Save();
                        
                        display_text.Text = "Login Successful";
                        display.Background = Brushes.Purple;
                        display.Visibility = Visibility.Visible;
                        display.BeginAnimation(HeightProperty, doubleanimation);
                        login.IsEnabled = false;
                        timing();
                    }
                    else
                    {
                        display_text.Text = "Login Failed, Incorrect Details";
                        display.Background = Brushes.Crimson;
                        display.Visibility = Visibility.Visible;
                        display.BeginAnimation(HeightProperty, doubleanimation);
                        timing2();

                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                display_text.Text = "Fill All Fields";
                display.Background = Brushes.Crimson;
                display.Visibility = Visibility.Visible;
                display.BeginAnimation(HeightProperty, doubleanimation);
                timing2();
            }
        }

        void timing()
        {
            timer.Tick += timer_tick1;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }
        void timing2()
        {
            timer.Tick += timer_tick2;
            timer.Interval = new TimeSpan(0, 0, 4);
            timer.Start();
        }
        private void timer_tick1(object sender, EventArgs e)
        {
            timer.Stop();            
            home win = new home();
            win.Show();
            this.Close();
        }
        private void timer_tick2(object sender, EventArgs e)
        {
            timer.Stop();
            DoubleAnimation doubleanimation = new DoubleAnimation();
            doubleanimation.From = 43;
            doubleanimation.To = 0;
            doubleanimation.Duration = TimeSpan.FromSeconds(1);

            doubleanimation.EasingFunction = new QuarticEase();
            display.BeginAnimation(HeightProperty, doubleanimation);

            //display.Visibility = Visibility.Hidden;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            notify.IsOpen = true;
        }      

        private void showing_TextChanged(object sender, TextChangedEventArgs e)
        {
            string userP = "";
            MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
            MySqlCommand ccommand;
            var cQuery = "SELECT * FROM login WHERE Password='" + showing.Text + "' AND Position='Admin'";
            ccommand = new MySqlCommand(cQuery, cconnection);
            try
            {
                cconnection.Open();
                MySqlDataReader readerc = ccommand.ExecuteReader();
                while (readerc.Read())
                {                    
                    userP = readerc["Position"].ToString();
                }
                if (userP == "Admin")
                {
                    settings_page win = new settings_page();
                    win.Show();
                    this.Close();
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }
    }
}
