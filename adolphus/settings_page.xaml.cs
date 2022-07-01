using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace adolphus
{
    /// <summary>
    /// Interaction logic for settings_page.xaml
    /// </summary>
    public partial class settings_page : Window
    {
        public settings_page()
        {
            InitializeComponent();
            settings_item();
            getClasses();
            GetStudData();
        }

        MySqlConnection con;
        MySqlDataAdapter daa;
        DataSet dss;
        
        private DispatcherTimer htimer;
        string astrName, aimageName;

        //MySql connection
        MySqlConnection connection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
        MySqlCommand insertCommand, insertCommanda;

        private void goback_Click(object sender, RoutedEventArgs e)
        {
            main win = new main();
            win.Show();

            this.Close();
        }

        //get items from settings table into textboxes
        public void settings_item()
        {
            MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");

            MySqlCommand ccommand;

            var cQuery = "SELECT * FROM settings";

            ccommand = new MySqlCommand(cQuery, cconnection);

            try
            {
                //for ecommandc
                cconnection.Open();
                MySqlDataReader readerc = ccommand.ExecuteReader();
                while (readerc.Read())
                {
                    current_term.Text = readerc["Term"].ToString();
                    current_session.Text = readerc["Session"].ToString();
                    classS3a.Text = readerc["ss3class"].ToString();
                    classS2a.Text = readerc["ss2class"].ToString();
                    classS1a.Text = readerc["ss1class"].ToString();
                    classJ3a.Text = readerc["jss3class"].ToString();
                    classJ2a.Text = readerc["jss2class"].ToString();
                    classJ1a.Text = readerc["jss1class"].ToString();

                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GetStudData()
        {
            //Gets data and inserts into datagrid
            try
            {
                string sql = "SELECT Id,Image,Username,Email,Password,Position FROM login WHERE Status='' ORDER BY Id";
                con = dtba.connectToDb();
                dss = new DataSet();
                DataTable dtt = new DataTable();
                daa = new MySqlDataAdapter(sql, connection);
                daa.Fill(dss);
                dataGrid.ItemsSource = dss.Tables[0].DefaultView;

                staffUsername.Clear();
                staffEmail.Clear();
                staffPassword.Clear();
                staffposition.Text = "";

                idds.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //AUTO GENERATING COLUMNS
        private void studdatagrid_AutoGeneratingColumn_1(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Id")
            {
                e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }
            else if (e.Column.Header.ToString() == "Image")
            {
                e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }
            else if (e.Column.Header.ToString() == "Status")
            {
                e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }
            else if (e.Column.Header.ToString() == "Password")
            {
                e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }
        }
        
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            staffUsername.Clear();
            staffEmail.Clear();
            staffPassword.Clear();
            staffposition.Text = "";

            try
            {
                DataGrid dg = (DataGrid)sender;
                DataRowView rowSelected = dg.SelectedItem as DataRowView;
                if (rowSelected != null)
                {
                    idds.Text = rowSelected[0].ToString();                                        
                    staffimage.Source = eximageSource.Source;

                    //Store binary data read from the database in a byte array
                    byte[] blob = (byte[])rowSelected[1];
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
                    staffimage.Source = bi;

                    if(idds.Text == "1")
                    {
                        idds.Text = rowSelected[0].ToString();
                        staffUsername.Text = rowSelected[2].ToString();
                        staffEmail.Text = rowSelected[3].ToString();
                        staffPassword.Text = rowSelected[4].ToString();

                        //updatestaff.IsEnabled = true;
                        //addstaff.IsEnabled = false;
                    }
                    else
                    {
                        //updatestaff.IsEnabled = false;
                        //addstaff.IsEnabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Browseimage_Click(object sender, RoutedEventArgs e)
        {
            //Browse image from the computer and inserts it into the app
            try
            {
                FileDialog fldlg = new OpenFileDialog();
                fldlg.InitialDirectory = Environment.SpecialFolder.MyPictures.ToString();
                fldlg.Filter = "Image File (*.jpg;*.png;*.gif)|*.jpg;*.png;*.gif";
                fldlg.ShowDialog();
                {
                    aimageName = fldlg.FileName;

                    if (aimageName == "")
                    {
                        MessageBox.Show("YOU DID NOT CHOOSE AN IMAGE");
                    }
                    else
                    {
                        astrName = fldlg.SafeFileName;
                        ImageSourceConverter isc = new ImageSourceConverter();
                        staffimage.SetValue(Image.SourceProperty, isc.ConvertFromString(aimageName));
                    }
                }
                fldlg = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Image Feild Cannot be empty", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void clear_Click(object sender, RoutedEventArgs e)
        {

        }
        private void addstaff_Click(object sender, RoutedEventArgs e)
        {
            if(String.IsNullOrEmpty(aimageName))
                {
                notify.IsOpen = true;
                showing.Text = "Staff Image is Compulsory";
            }
            else if (staffUsername.Text == "")
            {
                notify.IsOpen = true;
                showing.Text = "Staff Username Cannot be Empty";
            }
            else if (staffEmail.Text == "")
            {
                notify.IsOpen = true;
                showing.Text = "Staff Email Cannot be Empty";
            }
            else if (staffPassword.Text == "")
            {
                notify.IsOpen = true;
                showing.Text = "Staff Password Cannot be Empty";
            }
            else if (staffposition.Text == "")
            {
                notify.IsOpen = true;
                showing.Text = "Staff Position Cannot be Empty";
            }
            else
            {
                //Initialize a file stream to read the image file
                FileStream fs = new FileStream(aimageName, FileMode.Open, FileAccess.Read);

                //Initialize a byte array with size of stream
                byte[] aimgByteArr = new byte[fs.Length];

                //Read data from the file stream and put into the byte array
                fs.Read(aimgByteArr, 0, Convert.ToInt32(fs.Length));

                //Close a file stream
                fs.Close();

                //create insert query                                
                string insertQuery = "INSERT INTO login(imagepath,Image,Username,Email,Password,Position)values('" + astrName + "',@dstaffImage,@dstaffUsername,@dstaffEmail,@dstaffPassword,@dstaffPosition)";

                // Create Sql Command                 
                insertCommanda = new MySqlCommand(insertQuery, connection);
                insertCommanda.Parameters.AddWithValue("@dstaffImage", aimgByteArr);
                insertCommanda.Parameters.AddWithValue("@dstaffUsername", staffUsername.Text);
                insertCommanda.Parameters.AddWithValue("@dstaffEmail", staffEmail.Text);
                insertCommanda.Parameters.AddWithValue("@dstaffPassword", staffPassword.Text);
                insertCommanda.Parameters.AddWithValue("@dstaffPosition", staffposition.Text);

                try
                {
                    connection.Open();
                    int result = insertCommanda.ExecuteNonQuery();
                    if (result > 0)
                    {
                        notify.IsOpen = true;
                        showing.Text = "STAFF INFORAMTION ACCEPTED";
                        GetStudData();
                    }
                    else
                    {
                        notify.IsOpen = true;
                        showing.Text = "ERROR!!! UNABLE TO ENTER INFORMATION";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                finally
                {
                    connection.Close();
                }
            }    
        }

        private void blockstaff_Click(object sender, RoutedEventArgs e)
        {
            if (idds.Text != "")
            {
                //create insert query                                            
                string insertQuery = "UPDATE login SET Status=@dStatus WHERE Id='" + idds.Text + "'"; ;
                insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@dStatus", "blocked");
                try
                {
                    connection.Open();
                    int result = insertCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        notify.IsOpen = true;
                        showing.Text = "Updated Successfully";
                        GetStudData();
                    }
                    else
                    {
                        notify.IsOpen = true;
                        showing.Text = "Error!!! Unable to Update";
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning); }
                finally { connection.Close(); }
            }
            else
            {
                notify.IsOpen = true;
                showing.Text = "Select staff before proceeding";
            }
        }

        private void updatestaff_Click(object sender, RoutedEventArgs e)
        {
            if (idds.Text != "")
            {
                //create insert query                                            
                string insertQuery = "UPDATE login SET Status=@dStatus WHERE Id='" + idds.Text + "'"; ;
                insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@dStatus", null);
                try
                {
                    connection.Open();
                    int result = insertCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        notify.IsOpen = true;
                        showing.Text = "Updated Successfully";
                        GetStudData();
                    }
                    else
                    {
                        notify.IsOpen = true;
                        showing.Text = "Error!!! Unable to Update";
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning); }
                finally { connection.Close(); }
            }
            else
            {
                notify.IsOpen = true;
                showing.Text = "Select Overall Admin before proceeding";
            }
        }

        private void removestaff_Click(object sender, RoutedEventArgs e)
        {
            if (idds.Text != "")
            {
                string sQuery = "DELETE FROM `login` WHERE Id='" + idds.Text + "'";
                insertCommand = new MySqlCommand(sQuery, connection);
                try
                {
                    connection.Open();
                    int result = insertCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        notify.IsOpen = true;
                        showing.Text = "Staff Successfully Removed";
                        GetStudData();
                    }
                    else
                    {
                        notify.IsOpen = true;
                        showing.Text = "Error!!! Unable to Remove Staff";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "1", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                notify.IsOpen = true;
                showing.Text = "Select Staff from the Data Grid before proceeding";
            }
        }
     

        //add items into settings table
        private void update_Click_1(object sender, RoutedEventArgs e)
        {
            int parsedValue;
         
            if (!int.TryParse(classS3a.Text, out parsedValue))
            {
                MessageBox.Show("School Fees Feild is a number only Feild", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!int.TryParse(classS2a.Text, out parsedValue))
            {
                MessageBox.Show("School Fees Feild is a number only Feild", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!int.TryParse(classS1a.Text, out parsedValue))
            {
                MessageBox.Show("School Fees Feild is a number only Feild", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!int.TryParse(classJ3a.Text, out parsedValue))
            {
                MessageBox.Show("School Fees Feild is a number only Feild", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!int.TryParse(classJ2a.Text, out parsedValue))
            {
                MessageBox.Show("School Fees Feild is a number only Feild", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!int.TryParse(classJ1a.Text, out parsedValue))
            {
                MessageBox.Show("School Fees Feild is a number only Feild", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;                
            }
            else
            {                
                //create insert query
                string insertQuery = "UPDATE settings SET Term=@dTerm,Session=@dSession,ss3class=@dss3class,ss2class=@dss2class,ss1class=@dss1class,jss3class=@djss3class,jss2class=@djss2class,jss1class=@djss1class WHERE Id=1";

                // Create Sql Command                 
                insertCommand = new MySqlCommand(insertQuery, connection);                

                //Add parameters to the command                
                insertCommand.Parameters.AddWithValue("@dTerm", current_term.Text);
                insertCommand.Parameters.AddWithValue("@dSession", current_session.Text);
                insertCommand.Parameters.AddWithValue("@dss3class", classS3a.Text);
                insertCommand.Parameters.AddWithValue("@dss2class", classS2a.Text);
                insertCommand.Parameters.AddWithValue("@dss1class", classS1a.Text);
                insertCommand.Parameters.AddWithValue("@djss3class", classJ3a.Text);
                insertCommand.Parameters.AddWithValue("@djss2class", classJ2a.Text);
                insertCommand.Parameters.AddWithValue("@djss1class", classJ1a.Text);
                try
                {
                    connection.Open();
                    int result = insertCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        notify.IsOpen = true;
                        showing.Text = "DATA UPDATED";
                    }
                    else
                    {
                        notify.IsOpen = true;
                        showing.Text = "ERROR!!! UNABLE TO UPDATE DATA";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                finally
                {
                    connection.Close();
                }
            }            
        }

        //PROMOTING STARTS HERE
        //
        string classes, done;
        public string[] classesa, donea;       
        private void addItem_Click(object sender, RoutedEventArgs e)
        {
            string itemNamee;
            if (itemname.Text == "")
            {
                MessageBox.Show("Item Name is Empty");
            }
            else if (stud_promo_class.Items.Contains(itemname.Text.ToUpper()))
            {MessageBox.Show("Item is Already resent in the Promotional Combo Box");}
            else
            {itemNamee = itemname.Text;
            stud_promo_class.Items.Add(itemNamee.ToUpper());
            MessageBox.Show("Added");}
        }             
       
        private void loginpage(object sender, RoutedEventArgs e)
        {
            mainLogin win = new mainLogin();
            win.Show();
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //mainLogin win = new mainLogin();
            //win.Show();

            //this.Hide();
            //htimer = new DispatcherTimer();
            //htimer.Interval = new TimeSpan(0, 0, 4);
            //htimer.Tick += timer_tick;
            //htimer.Start();            
        }
        private void timer_tick(object sender, EventArgs e)
        {
            this.Close();
        }
        private void promoteStud_Click(object sender, RoutedEventArgs e)
        {
            if (studclass.Text == "" || stud_promo_class.Text == "")
            {
                MessageBox.Show("SELECT CLASSES");
            }
            else if (studclass.Text == stud_promo_class.Text)
            {
                MessageBox.Show("BOTH CURRENT CLASS AND PROMOTIONAL CLASS CANNOT BE THE SIMILAR");
            }
            else
            {
                MySqlConnection connection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");

                MySqlCommand gucommand;

                string Query = "UPDATE studentdetails SET Student_Class=@clas WHERE Student_Class = '" + studclass.Text + "'";

                gucommand = new MySqlCommand(Query, connection);

                gucommand.Parameters.AddWithValue("@clas", stud_promo_class.Text);

                try
                {
                    connection.Open();
                    int result = gucommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        notify.IsOpen = true;
                        showing.Text = "STUDENTS PROMOTED";
                    }
                    else
                    {
                        notify.IsOpen = true;
                        showing.Text = "ERROR!!! UNABLE TO PROMOTE STUDENTS";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            
        }

        public void getClasses()
        {
            MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");

            MySqlCommand ccommand;

            var cQuery = "SELECT Student_Class FROM studentdetails";

            ccommand = new MySqlCommand(cQuery, cconnection);

            try
            {
                //for ecommandc
                cconnection.Open();
                MySqlDataReader readerc = ccommand.ExecuteReader();
                while (readerc.Read())
                {
                    classes += readerc["Student_Class"].ToString() + ",";
                }

                classesa = classes.Split(',');

                HashSet<string> hset = new HashSet<string>(classesa);
                foreach (var n in hset)
                {
                    done += n + ",";
                    donea = done.Split(',');
                }

                for (int i = 0; i <= donea.Length - 3; i++)
                {
                    studclass.Items.Add(donea[i].ToUpper());
                    stud_promo_class.Items.Add(donea[i].ToUpper());
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //REMEMBER TO CONVERT EVERYTHING ON THE DATAGRID TO UPPER CASE SO THAT WHEN INSERTING INTO THE DB EVERYTHING WILL GO AS CAPITAL STRINGS
        
    }
}
