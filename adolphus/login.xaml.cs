using Microsoft.Win32;
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
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Window
    {       

        public login()
        {
            InitializeComponent();            
            load();            
        }      

        DispatcherTimer timer = new DispatcherTimer();          

        private void drag(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void closea(object sender, RoutedEventArgs e)
        {
            this.Close();
            login win = new login();
            win.Close();
        }       

        private void signup_MouseEnter(object sender, MouseEventArgs e)
        {
            signup.Foreground = Brushes.DodgerBlue;
        }
        private void signup_MouseLeave(object sender, MouseEventArgs e)
        {
            signup.Foreground = Brushes.CornflowerBlue;
        }

        private void help_MouseEnter(object sender, MouseEventArgs e)
        {
            help.Foreground = Brushes.DodgerBlue;
        }
        private void help_MouseLeave(object sender, MouseEventArgs e)
        {
            help.Foreground = Brushes.Gray;
        }       

        private void button_MouseEnter(object sender, MouseEventArgs e)
        {
            clos.Foreground = Brushes.DodgerBlue;
        }
        private void button_MouseLeave(object sender, MouseEventArgs e)
        {
            clos.Foreground = Brushes.CornflowerBlue;
        }

        //this is the event handler for the login button
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation doubleanimation = new DoubleAnimation();
            doubleanimation.From = 2;
            doubleanimation.To = 45;
            doubleanimation.Duration = TimeSpan.FromSeconds(1);

            doubleanimation.EasingFunction = new QuarticEase();            
            if(username.Text == "" | passwrd.Password == "")
            {
                display_text.Text = "Fill All Fields";
                display.Background = Brushes.Crimson;
                display.Visibility = Visibility.Visible;
                display.BeginAnimation(HeightProperty, doubleanimation);
                timing2();
            }
            else if (Settings1.Default.emailS == username.Text & Settings1.Default.passwordS == passwrd.Password )
            {                
                display_text.Text = "Login Success";
                display.Background = Brushes.DodgerBlue;
                display.Visibility = Visibility.Visible;
                display.BeginAnimation(HeightProperty, doubleanimation);
                button1.IsEnabled = false;
                timing();
            }
            else
            {
                display_text.Text = "Login Failed, Incorrect Details";             
                display.Background = Brushes.Crimson;
                display.Visibility = Visibility.Visible;
                display.BeginAnimation(HeightProperty, doubleanimation);
                timing2();
            }                                                                                                   
        }
        void timing()
        {
            timer.Tick += timer_tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }
        void timing2()
        {
            timer.Tick += timer_tick2;
            timer.Interval = new TimeSpan(0, 0, 4);
            timer.Start();
        }
        private void timer_tick(object sender, EventArgs e)
        {
            timer.Stop();
            main objMainWindow = new main();
            objMainWindow.Show();
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
        }

        private void open_settings(object sender, RoutedEventArgs e)
        {
            set_window.Visibility = Visibility.Visible;            
           
            DoubleAnimation doubleanimation = new DoubleAnimation();
            doubleanimation.From = 0;
            doubleanimation.To = 500;
            doubleanimation.Duration = TimeSpan.FromSeconds(2);

            doubleanimation.EasingFunction = new QuarticEase();

            set_window.BeginAnimation(HeightProperty, doubleanimation);

            set.IsEnabled = false;
        }

        private void backa(object sender, RoutedEventArgs e)
        {            
            DoubleAnimation doubleanimation = new DoubleAnimation();
            doubleanimation.From = 500;
            doubleanimation.To = 0;
            doubleanimation.Duration = TimeSpan.FromSeconds(2);

            doubleanimation.EasingFunction = new QuarticEase();

            set_window.BeginAnimation(HeightProperty, doubleanimation);

            set.IsEnabled = true;
        }

        private void loadsettings(object sender, RoutedEventArgs e)
        {
            load();
        }

        private void savesettings(object sender, RoutedEventArgs e)
        {
                Settings1.Default.nameS = name.Text;
                Settings1.Default.passwordS = pass.Password;
                Settings1.Default.emailS = email.Text;
                //Settings1.Default.imageS = Studimage.sp;

                Settings1.Default.Save();
                MessageBox.Show("Settings Saved!");          
        }

        void load()
        {            
            name.Text = Settings1.Default.nameS;
            pass.Password = Settings1.Default.passwordS;
            email.Text = Settings1.Default.emailS;
        }

        private void removeaccount(object sender, RoutedEventArgs e)
        {
           
        }

        private void set_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        string filelocation;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show(Settings1.Default.FileString);

            //ImageSourceConverter iscc = new ImageSourceConverter();
            //Studimage.SetValue(Image.SourceProperty, iscc.ConvertFromString(Settings1.Default.FileString));
            //image.SetValue(Image.SourceProperty, iscc.ConvertFromString(Settings1.Default.FileString));

            if (filelocation != "")
            {
                if(Settings1.Default.FileString == "")
                {
                    MessageBox.Show("Sorry User last time the application was loaded, it encountered an error");
                }
                else
                {
                    ImageSourceConverter isc = new ImageSourceConverter();
                    Studimage.SetValue(Image.SourceProperty, isc.ConvertFromString(Settings1.Default.FileString));
                    image.SetValue(Image.SourceProperty, isc.ConvertFromString(Settings1.Default.FileString));
                }                
            }
        }

        private void select_image(object sender, RoutedEventArgs e)
        {
            try
            {
                string filelocation = "";
                FileDialog fldlg = new OpenFileDialog();
                fldlg.Title = "Browse Image File";
                fldlg.InitialDirectory = Environment.SpecialFolder.MyPictures.ToString();
                fldlg.Filter = "Image File (*.jpg;*.bmp;*.gif)|*.jpg;*.bmp;*.gif";
                fldlg.FilterIndex = 2;
                fldlg.RestoreDirectory = true;
                fldlg.ShowDialog();
                {
                    filelocation = fldlg.FileName;
                }                

                Class1.path = filelocation;
                
                Settings1.Default.FileString = Class1.path;

                Settings1.Default.Save();                

                ImageSourceConverter isc = new ImageSourceConverter();
                Studimage.SetValue(Image.SourceProperty, isc.ConvertFromString(Settings1.Default.FileString));
                image.SetValue(Image.SourceProperty, isc.ConvertFromString(Settings1.Default.FileString));

                //fldlg = null;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }       
    }
}
