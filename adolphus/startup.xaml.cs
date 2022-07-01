using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace adolphus
{
    /// <summary>
    /// Interaction logic for startup.xaml
    /// </summary>
    public partial class startup : Page
    {        

        string _imageDirectory = System.IO.Directory.GetCurrentDirectory();
        private DispatcherTimer htimer;
        Random _random = new Random();

        int numbers = 0;
        public startup()
        {
            InitializeComponent();         
        }

        string aterm, asession;
        private void GetSettings()
        {
            MySqlConnection connection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
            MySqlCommand ccommand;

            var cQuery = "SELECT Term,Session FROM settings";

            ccommand = new MySqlCommand(cQuery, connection);

            try
            {
                //for ecommandc
                connection.Open();

                MySqlDataReader readerc = ccommand.ExecuteReader();
                while (readerc.Read())
                {
                    aterm = readerc["Term"].ToString();
                    asession = readerc["Session"].ToString();                    
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void getLastEnteredStud()
        {
            string afname, asname, alname;

            MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
            MySqlCommand ccommand;
            var cQuery = "SELECT * FROM studentdetails ORDER BY Id DESC";
            ccommand = new MySqlCommand(cQuery, cconnection);
            try
            {
                cconnection.Open();
                MySqlDataReader readerc = ccommand.ExecuteReader();
                while (readerc.Read())
                {
                    afname = readerc["First_Name"].ToString();
                    asname = readerc["Second_Name"].ToString();
                    alname = readerc["Surname"].ToString();

                    txt1.Text = "New Student " + alname + ", " + afname + " " + asname + " was Entered recently.";

                    //Store binary data read from the database in a byte array
                    byte[] blob = (byte[])readerc["Student_Image"];
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
                    img1.Source = bi;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void getLastEnteredStaff()
        {
            string fname, sname, lname;
            MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
            MySqlCommand ccommand;
            var cQuery = "SELECT * FROM staffdetails ORDER BY Id DESC";
            ccommand = new MySqlCommand(cQuery, cconnection);
            try
            {
                cconnection.Open();
                MySqlDataReader readerc = ccommand.ExecuteReader();
                while (readerc.Read())
                {
                    fname = readerc["First_Name"].ToString();
                    sname = readerc["Second_Name"].ToString();
                    lname = readerc["Surname"].ToString();
                    
                    txt2.Text = "New Staff " +lname+", "+fname+" "+sname+" was Entered recently.";

                    //Store binary data read from the database in a byte array
                    byte[] blob = (byte[])readerc["Staff_Image"];
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
                    img2.Source = bi;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void getLastStudPaid()
        {
            GetSettings();

            MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
            MySqlCommand ccommand;
            var cQuery = "SELECT * FROM fess WHERE Term='"+ aterm + "' AND Session='"+asession+"' ORDER BY Id DESC";
            ccommand = new MySqlCommand(cQuery, cconnection);
            try
            {
                cconnection.Open();
                MySqlDataReader readerc = ccommand.ExecuteReader();
                while (readerc.Read())
                {
                    txt3.Text = "Student " + readerc["Stud_Name"].ToString() + " Recently Paid School Fees.";                                        

                    //Store binary data read from the database in a byte array
                    //byte[] blob = (byte[])readerc["Image"];
                    //MemoryStream stream = new MemoryStream();
                    //stream.Write(blob, 0, blob.Length);
                    //stream.Position = 0;

                    //System.Drawing.Image img = System.Drawing.Image.FromStream(stream);
                    //BitmapImage bi = new BitmapImage();
                    //bi.BeginInit();

                    //MemoryStream ms = new MemoryStream();
                    //img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                    //ms.Seek(0, SeekOrigin.Begin);
                    //bi.StreamSource = ms;
                    //bi.EndInit();
                    //img3.Source = bi;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //page loaded display pictures
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            string[] imagePaths = System.IO.Directory.GetFiles(_imageDirectory, "*.jpg");

            BitmapImage bitmapImage = new BitmapImage(new Uri(imagePaths[0]));
            sliderImage.Source = bitmapImage;

            htimer = new DispatcherTimer();
            htimer.Interval = new TimeSpan(0, 0, 5);
            htimer.Tick += timer_tick;
            htimer.Start();
                        
            txt4.Text = "Student Atah Paid for PTA";            

            getLastEnteredStud();
            getLastEnteredStaff();
            getLastStudPaid();

            todolistmain objOpenDolist = new todolistmain();
            toframe.NavigationService.Navigate(objOpenDolist);            
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
            sliderImage.BeginAnimation(OpacityProperty, badass);
            sliderImage.Opacity = 100;

            string[] imagePaths = System.IO.Directory.GetFiles(_imageDirectory, "*.jpg");
            if (numbers == imagePaths.Length)
            {
                numbers = 0;
                BitmapImage bitmapImage = new BitmapImage(new Uri(imagePaths[numbers]));
                sliderImage.Source = bitmapImage;
            }
            else
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri(imagePaths[numbers]));
                sliderImage.Source = bitmapImage;
            }
            htimer.Stop();
            htimer.Start();
        }

        //previous
        private void button_Click(object sender, RoutedEventArgs e)
        {
            string[] imagePaths = System.IO.Directory.GetFiles(_imageDirectory, "*.jpg");
            if (numbers != 0)
            {
                numbers--;
                htimer.Stop();
                DoubleAnimation badass = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = new Duration(TimeSpan.FromSeconds(0)),
                    AutoReverse = false
                };
                sliderImage.BeginAnimation(OpacityProperty, badass);
                sliderImage.Opacity = 100;

                BitmapImage bitmapImage = new BitmapImage(new Uri(imagePaths[numbers]));
                sliderImage.Source = bitmapImage;

                htimer.Start();
            }
            else
            {
                numbers = imagePaths.Length - 1;

                htimer.Stop();
                DoubleAnimation badass = new DoubleAnimation
                {
                    From = 0,
                    To = 1,
                    Duration = new Duration(TimeSpan.FromSeconds(0)),
                    AutoReverse = false
                };
                sliderImage.BeginAnimation(OpacityProperty, badass);
                sliderImage.Opacity = 100;

                BitmapImage bitmapImage = new BitmapImage(new Uri(imagePaths[numbers]));
                sliderImage.Source = bitmapImage;

                htimer.Start();
            }
        }

        //next
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            htimer.Stop();
            numbers++;
            DoubleAnimation badass = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = new Duration(TimeSpan.FromSeconds(0)),
                AutoReverse = false
            };
            sliderImage.BeginAnimation(OpacityProperty, badass);
            sliderImage.Opacity = 100;

            string[] imagePaths = System.IO.Directory.GetFiles(_imageDirectory, "*.jpg");

            if (numbers == imagePaths.Length)
            {
                numbers = 0;
                BitmapImage bitmapImage = new BitmapImage(new Uri(imagePaths[numbers]));
                sliderImage.Source = bitmapImage;
            }
            else
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri(imagePaths[numbers]));
                sliderImage.Source = bitmapImage;
            }

            htimer.Start();
        }

        private void buttona_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void buttonb_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonc_Click(object sender, RoutedEventArgs e)
        {
            mainframe.Visibility = Visibility.Visible;
            allClass objOpen1 = new allClass();
            mainframe.NavigationService.Navigate(objOpen1);
        }

        private void buttond_Click(object sender, RoutedEventArgs e)
        {
                       
        }
    }
}
