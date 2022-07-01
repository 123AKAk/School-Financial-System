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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace adolphus
{
    /// <summary>
    /// Interaction logic for view_edit_teachers.xaml
    /// </summary>
    public partial class view_edit_teachers : Page
    {
        string strName, imageName;

        public view_edit_teachers()
        {
            InitializeComponent();
            this.GetSaffData();
        }

        MySqlConnection con;
        MySqlDataAdapter daa;
        DataSet dss;
        MySqlCommandBuilder scmbl;

        //MySql connection
        MySqlConnection connection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
        MySqlCommand insertCommand;

        private void GetSaffData()
        {
            //Gets data and inserts into datagrid
            try
            {
                string sql = "SELECT Id,Staff_Image,First_Name,Second_Name,Surname,Gender,Marital_Status,Phone_Number,Date_of_Birth,Address,LGA,State," +
                    "Nationality,Religion,Blood_Group FROM staffdetails WHERE reme IS NULL OR reme = '' ORDER BY Id DESC";
                con = dtba.connectToDb();
                dss = new DataSet();
                DataTable dtt = new DataTable();
                daa = new MySqlDataAdapter(sql, con);
                daa.Fill(dss);
                staffdatagrid.ItemsSource = dss.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string firstname, secondname, surname;
            try
            {
                DataGrid dg = (DataGrid)sender;
                DataRowView rowSelected = dg.SelectedItem as DataRowView;
                if (rowSelected != null)
                {                    
                    idds.Text = rowSelected[0].ToString();
                    Valid.Text = rowSelected[0].ToString();
                    firstname = rowSelected[2].ToString();
                    secondname = rowSelected[3].ToString();
                    surname = rowSelected[4].ToString();

                    staffname.Text = surname + ", " + firstname + " " + secondname;

                    printstaffata.IsEnabled = true;

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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //NOT IN USE
        int solid = 0;
        string item_Name;        
        private void Viewadmissindetails_Click(object sender, RoutedEventArgs e)
        {
            //solid = 1;
            //if (idds.Text != "")
            //{
            //    MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");

            //    MySqlCommand ccommand;

            //    var cQuery = "SELECT * FROM staffitem";

            //    ccommand = new MySqlCommand(cQuery, cconnection);

            //    //parameter                        
            //    //ccommand.Parameters.AddWithValue("@dGfirstname", "OBLA");
            //    try
            //    {
            //        //for ecommandc
            //        cconnection.Open();
            //        MySqlDataReader readerc = ccommand.ExecuteReader();
            //        while (readerc.Read())
            //        {
            //            //id += readerc["id"].ToString() + ",";
            //            item_Name += readerc["Item_Name"].ToString() + ",";
            //        }
            //        try
            //        {                        
            //            string sqlQuery = "SELECT Id,Staff_Image," + item_Name + "Others FROM staffdetails WHERE id='" + idds.Text + "'";
            //            con = dtba.connectToDb();
            //            dss = new DataSet();
            //            DataTable dtt = new DataTable();
            //            daa = new MySqlDataAdapter(sqlQuery, con);
            //            daa.Fill(dss);
            //            staffdatagrid.ItemsSource = dss.Tables[0].DefaultView;

            //            animate();
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            //        }                    
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }                
            //}
            //else
            //{
            //    notify.IsOpen = true;
            //    showing.Text = "SELECT A STAFF FROM THE DATA-GRID BEFORE PROCEEDING ";
            //}
        }

        private void Viewall_Click(object sender, RoutedEventArgs e)
        {
            solid = 0;            
            autogenGrid.Visibility = Visibility.Hidden;                        
            GetSaffData();
        }

        //search for TEACHER data
        private void Search_Click(object sender, RoutedEventArgs e)
        {            
            if (staffsearch.Text != "")
            {
                if (typesearch.Text != "")
                {
                    try
                    {
                        string sqlQuery = "SELECT Id,Staff_Image,First_Name,Second_Name,Surname,Gender,Marital_Status,Phone_Number,Date_of_Birth,Address,LGA,State,Nationality,Religion," +
                                    "Blood_Group FROM staffdetails WHERE " + typesearch.Text + "='" + staffsearch.Text + "' AND reme IS NULL OR reme = '' ORDER BY Id DESC";
                        con = dtba.connectToDb();
                        dss = new DataSet();
                        DataTable dtt = new DataTable();
                        daa = new MySqlDataAdapter(sqlQuery, con);
                        daa.Fill(dss);
                        staffdatagrid.ItemsSource = dss.Tables[0].DefaultView;
                        
                        staffname.Text = "";
                        staffimage.Source = eximageSource.Source;

                        solid = 1;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    notify.IsOpen = true;
                    showing.Text = "SELECT SEARCH TYPE FIRST";
                }
            }
            else
            {
                notify.IsOpen = true;
                showing.Text = "SEARCH FEILD IS EMPTY";
            }
        }

        private void Staffsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            search.IsEnabled = true;
        }

        private void Updatestafftata_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                scmbl = new MySqlCommandBuilder(daa);
                daa.Update(dss);

                notify.IsOpen = true;
                showing.Text = "STAFF INFROMATION UPDATED";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        string remv = "Removed";
        private void Removestaffata_Click(object sender, RoutedEventArgs e)
        {          
            if (idds.Text != "")
            {
                //create insert query                                            
                string insertQuery = "UPDATE staffdetails SET reme=@dreme WHERE Id='" + idds.Text + "'"; ;
                //string insertQuery = "INSERT INTO studentdetails(imagepath,Student_Image)values('" + strName + "', @dStudent_Image) WHERE Id='" + idds.Text + "'";
                insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@dreme", remv);
                try
                {
                    connection.Open();
                    int result = insertCommand.ExecuteNonQuery();
                    if (result > 0) { MessageBox.Show("STUDENT HAS BEEN REMOVED", "Removed", MessageBoxButton.OK, MessageBoxImage.Information); GetSaffData(); }
                    else
                    { MessageBox.Show("UNABLE TO REMOVE " + staffname + "", "Error", MessageBoxButton.OK, MessageBoxImage.Warning); }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning); }
                finally { connection.Close(); }
            }
            else
            {
                notify.IsOpen = true;
                showing.Text = "SELECT A STAFF FROM THE DATA-GRID BEFORE PROCEEDING ";
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
                    imageName = fldlg.FileName;

                    if (imageName == "")
                    {
                        MessageBox.Show("YOU DID NOT CHOOSE AN IMAGE");
                    }
                    else
                    {
                        strName = fldlg.SafeFileName;
                        ImageSourceConverter isc = new ImageSourceConverter();
                        staffimage.SetValue(Image.SourceProperty, isc.ConvertFromString(imageName));
                        updateimage.Visibility = Visibility.Visible;
                    }                                        
                }
                fldlg = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IMAGE FEILD CANNOT BE EMPTY", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        //update student image
        private void Updateimage_Click(object sender, RoutedEventArgs e)
        {
            if (idds.Text != "")
            {
                FileStream fs = new FileStream(imageName, FileMode.Open, FileAccess.Read);
                byte[] imgByteArr = new byte[fs.Length];
                fs.Read(imgByteArr, 0, Convert.ToInt32(fs.Length));
                fs.Close();
                //create insert query                                            
                string insertQuery = "UPDATE staffdetails SET Staff_Image=@dStaff_Image, imagepath='" + strName + "' WHERE Id='" + idds.Text + "'";
                //string insertQuery = "INSERT INTO studentdetails(imagepath,Student_Image)values('" + strName + "', @dStudent_Image) WHERE Id='" + idds.Text + "'";
                insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@dStaff_Image", imgByteArr);
                try
                {
                    connection.Open();
                    int result = insertCommand.ExecuteNonQuery();
                    if (result > 0) { notify.IsOpen = true; showing.Text = "STAFF IMAGE UPDATED"; GetSaffData(); updateimage.Visibility = Visibility.Hidden; }
                    else
                    { notify.IsOpen = true; showing.Text = "ERROR!!! UNABLE TO UPDATE " + staffname.Text + " IMAGE"; }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning); }
                finally { connection.Close(); }
            }
            else
            {
                notify.IsOpen = true;
                showing.Text = "SELECT A STAFF FROM THE DATA-GRID BEFORE PROCEEDING ";
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
            else if (e.Column.Header.ToString() == "Staff_Image")
            {
                e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }            
            else if (e.Column.Header.ToString() == "Others")
            {
                e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }
            else if (e.Column.Header.ToString() == "reme")
            {
                e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }
            else if (e.Column.Header.ToString() == "imagepath")
            {
                e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }
        }

        //AUTO GENERATED OBJECTS
        public string[] customer_Namea;
        List<TextBox> allAutoCreatedObjects = new List<TextBox>();

        const int WIDTH = 160;
        const int HEIGHT = 30;

        int currentX = 0;
        const int STEP = 7;
        bool isButtonssOnScreen = false;

        public static int setvalue;
        string gadid, gadda, gaddb, gaddc, gaddd;

        private void Viewestaffxpenses_Click(object sender, RoutedEventArgs e)
        {
            solid = 1;            
            string customer_Name = "";

            if (idds.Text != "")
            {                
                MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
                MySqlConnection aconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
                MySqlCommand ccommand, acommand;
                var cQuery = "SELECT * FROM staffitem WHERE Status IS NULL OR Status = ''";
                ccommand = new MySqlCommand(cQuery, cconnection);

                //parameter                        
                //ccommand.Parameters.AddWithValue("@dGfirstname", "OBLA");
                try
                {
                    var acQuery = "SELECT Term,Session FROM settings";

                    acommand = new MySqlCommand(acQuery, aconnection);

                    aconnection.Open();

                    MySqlDataReader areaderc = acommand.ExecuteReader();
                    while (areaderc.Read())
                    {
                        Term.Text = areaderc["Term"].ToString();
                        Session.Text = areaderc["Session"].ToString();                        
                    }

                    //for ecommandc
                    cconnection.Open();
                    MySqlDataReader readerc = ccommand.ExecuteReader();
                    while (readerc.Read())
                    {
                        customer_Name += readerc["Item_Name"].ToString() + "," + readerc["Mode_of_Payement"].ToString() + ",";
                    }

                    customer_Namea = customer_Name.Split(',');               
                    if (customer_Name == "")
                    {
                        MessageBox.Show("There are no Student Items to Display");
                        return;
                    }
                    else
                    {
                        try
                        {
                            MySqlConnection connectiona = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
                            MySqlCommand command;
                            //checking if students info has been entered before
                            var aQuery = "SELECT * FROM staffitemdisplay WHERE Valid = '" + Valid.Text + "' AND Term = '" + Term.Text + "' AND Session = '" + Session.Text + "'";
                            command = new MySqlCommand(aQuery, connectiona);
                            connectiona.Open();
                            MySqlDataReader readera = command.ExecuteReader();
                            while (readera.Read())
                            {
                                gadid = readera["Id"].ToString();
                                gadda = readera["Staff_Name"].ToString();
                                gaddb = readera["Term"].ToString();
                                gaddc = readera["Session"].ToString();
                                gaddd = readera["Valid"].ToString();
                                specialInfo.Text = readera["Notes"].ToString();
                                itemsDate.Text = readera["Date"].ToString();
                            }
                            if (gadda == staffname.Text && gaddd == Valid.Text && gaddb == Term.Text && gaddc == Session.Text)
                            {
                                //if the same insert values into the generated texboxes and button becomes update not add
                                if (!isButtonssOnScreen)
                                {
                                    for (int i = 0; i <= customer_Namea.Length - 2; i++)
                                    {
                                        TextBox TextBoxa = new TextBox();
                                        TextBoxa.Name = customer_Namea[i];
                                        TextBoxa.BorderBrush = Brushes.LightGray;
                                        TextBoxa.BorderThickness = new Thickness(1, 1, 1, 1);
                                        TextBoxa.Text = "";
                                        TextBoxa.FontSize = 13;
                                        TextBoxa.Width = WIDTH; TextBoxa.Height = HEIGHT;
                                        TextBoxa.TextAlignment = TextAlignment.Center;

                                        TextBoxa.Padding = new Thickness(0, 0, 0, 0);
                                        TextBoxa.Margin = new Thickness(5, 0, 5, 0);
                                        allAutoCreatedObjects.Add(TextBoxa);

                                        TextBoxa.TextChanged += new TextChangedEventHandler(autoGenTextChange);
                                    }
                                    foreach (var item in allAutoCreatedObjects)
                                    {
                                        Canvas.SetLeft(item, STEP * currentX);
                                        currentX += 25;
                                        MainCanvas.Width = currentX * STEP;
                                        MainCanvas.Children.Add(item);
                                    }
                                    isButtonssOnScreen = true;
                                }
                                else
                                {
                                    foreach (var item in allAutoCreatedObjects)
                                    {
                                        MainCanvas.Children.Remove(item);
                                    }

                                    foreach (var item in allAutoCreatedObjects)
                                    {
                                        MainCanvas.Children.Add(item);
                                    }
                                    isButtonssOnScreen = true;
                                }
                            }
                            else
                            {
                                //if not the same generated the texboxes and button becomes add not update
                                if (!isButtonssOnScreen)
                                {
                                    for (int i = 0; i <= customer_Namea.Length - 2; i++)
                                    {
                                        TextBox TextBoxa = new TextBox();
                                        TextBoxa.Name = customer_Namea[i];
                                        TextBoxa.BorderBrush = Brushes.LightGray;
                                        TextBoxa.BorderThickness = new Thickness(1, 1, 1, 1);
                                        TextBoxa.Text = customer_Namea[i];
                                        TextBoxa.FontSize = 13;
                                        TextBoxa.Width = WIDTH; TextBoxa.Height = HEIGHT;
                                        TextBoxa.TextAlignment = TextAlignment.Center;

                                        TextBoxa.Padding = new Thickness(0, 0, 0, 0);
                                        TextBoxa.Margin = new Thickness(5, 0, 5, 0);
                                        allAutoCreatedObjects.Add(TextBoxa);

                                        TextBoxa.TextChanged += new TextChangedEventHandler(autoGenTextChange);
                                    }
                                    foreach (var item in allAutoCreatedObjects)
                                    {
                                        Canvas.SetLeft(item, STEP * currentX);
                                        currentX += 25;
                                        MainCanvas.Width = currentX * STEP;
                                        MainCanvas.Children.Add(item);
                                    }
                                    isButtonssOnScreen = true;
                                }
                                else
                                {
                                    foreach (var item in allAutoCreatedObjects)
                                    {
                                        MainCanvas.Children.Remove(item);
                                    }

                                    foreach (var item in allAutoCreatedObjects)
                                    {
                                        MainCanvas.Children.Add(item);
                                    }
                                    isButtonssOnScreen = true;
                                }

                                MySqlCommand insertCommand;
                                MySqlConnection connection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
                                string insertQuery = "INSERT INTO staffitemdisplay(Staff_Name,Valid,Session,Term,Notes)values(@dStaff_Name,@dValid,@dSession,@dTerm,@dNotes)";
                                insertCommand = new MySqlCommand(insertQuery, connection);
                                insertCommand.Parameters.AddWithValue("@dStaff_Name", staffname.Text);
                                insertCommand.Parameters.AddWithValue("@dValid", Valid.Text);
                                insertCommand.Parameters.AddWithValue("@dSession", Session.Text);
                                insertCommand.Parameters.AddWithValue("@dTerm", Term.Text);                                
                                insertCommand.Parameters.AddWithValue("@dNotes", specialInfo.Text);
                                try
                                {
                                    connection.Open();
                                    int result = insertCommand.ExecuteNonQuery();
                                    if (result > 0)
                                    {

                                    }
                                    else
                                    {
                                        MessageBox.Show("THERE IS A PROBLEM REQUEST APP DEVELOPER");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                                }
                                finally
                                {
                                    connection.Close();
                                    cconnection.Close();
                                }
                            }
                            //checking if students info has been entered before
                            //                            
                            autogenGrid.Visibility = Visibility.Visible;
                            animate();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        //}
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "...#");
                }
                finally
                {
                    aconnection.Close();
                }
                //int[] ar = new int[]
                // {
                //2,4,6,8,10
                // };
                //MessageBox.Show(ar[3].ToString());                
            }
            else
            {
                notify.IsOpen = true;
                showing.Text = "SELECT A STUDENT FROM THE DATA-GRID BEFORE PROCEEDING ";
            }
        }
        //event handler for the auto generated text
        string txtName;
        private void autoGenTextChange(object sender, RoutedEventArgs e)
        {
            int parsedValue;

            string substr1 = "Payement_Type_of";

            TextBox textBox = (TextBox)sender;
            txtName = textBox.Name;

            if (textBox.Name.Contains(substr1))
            {
                MySqlConnection connection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
                MySqlCommand gucommand;
                string Query = "UPDATE staffitemdisplay SET " + txtName + "=@dtxtName WHERE Valid='" + Valid.Text + "' AND Term = '" + Term.Text + "' AND Session = '" + Session.Text + "'";
                gucommand = new MySqlCommand(Query, connection);
                gucommand.Parameters.AddWithValue("@dtxtName", textBox.Text);
                try
                {
                    connection.Open();
                    int result = gucommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                    }
                    else
                    {
                        MessageBox.Show("Unable To Update Text Feild", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                if (textBox.Text == "")
                {

                }
                else if (int.TryParse(textBox.Text, out parsedValue))
                {
                    MySqlConnection connection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
                    MySqlCommand gucommand;
                    string Query = "UPDATE staffitemdisplay SET " + txtName + "=@dtxtName WHERE Valid='" + Valid.Text + "' AND Term = '" + Term.Text + "' AND Session = '" + Session.Text + "'";
                    gucommand = new MySqlCommand(Query, connection);
                    gucommand.Parameters.AddWithValue("@dtxtName", textBox.Text);
                    try
                    {
                        connection.Open();
                        int result = gucommand.ExecuteNonQuery();
                        if (result > 0)
                        {
                        }
                        else
                        {
                            MessageBox.Show("Unable To Update Text Feild", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
                else
                {
                    notify.IsOpen = true;
                    showing.Text = textBox.Name + " Is a Numbers Only Feild";
                    textBox.Text = "";
                }
            }
        }
        private void specialInfoChange(object sender, TextChangedEventArgs e)
        {
            MySqlConnection connection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
            MySqlCommand gucommand;
            string Query = "UPDATE staffitemdisplay SET Notes=@dtxtName WHERE Valid='" + Valid.Text + "' AND Term = '" + Term.Text + "' AND Session = '" + Session.Text + "'";
            gucommand = new MySqlCommand(Query, connection);
            gucommand.Parameters.AddWithValue("@dtxtName", specialInfo.Text);
            try
            {
                connection.Open();
                int result = gucommand.ExecuteNonQuery();
                if (result > 0)
                {
                }
                else
                {
                    MessageBox.Show("Unable To Update Special Info Feild", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                connection.Close();
            }
        }
        private void itemsDate_CalendarClosed(object sender, RoutedEventArgs e)
        {
            MySqlConnection connection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
            MySqlCommand gucommand;
            string Query = "UPDATE staffitemdisplay SET Date=@ddate WHERE Valid='" + Valid.Text + "' AND Term = '" + Term.Text + "' AND Session = '" + Session.Text + "'";
            gucommand = new MySqlCommand(Query, connection);
            gucommand.Parameters.AddWithValue("@ddate", itemsDate.Text);
            try
            {
                connection.Open();
                int result = gucommand.ExecuteNonQuery();
                if (result > 0)
                {
                }
                else
                {
                    MessageBox.Show("Unable To Update Date Feild", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                connection.Close();
            }
        }

        private void closeItem_Click(object sender, RoutedEventArgs e)
        {
            autogenGrid.Visibility = Visibility.Hidden;
        }

        private void animate()
        {
            staffname.Text = "";
            staffimage.Source = eximageSource.Source;
            printstaffata.IsEnabled = false;

            //create animation on datagrid
            //DoubleAnimation doubleanimation = new DoubleAnimation();
            //doubleanimation.From = 50;
            //doubleanimation.To = 275;
            //doubleanimation.Duration = TimeSpan.FromSeconds(1);

            //doubleanimation.EasingFunction = new QuarticEase();
            //studdatagrid.BeginAnimation(HeightProperty, doubleanimation);
            //animation ends here
        }

        //PRINT STAFF DETAILS
        private void Printstaffdata_Click(object sender, RoutedEventArgs e)
        {
            Settings1.Default.studentId = Convert.ToInt32(idds.Text);
            staffStudy objOpen = new staffStudy();
            mainframe.NavigationService.Navigate(objOpen);
            mainframe.Visibility = Visibility.Visible;
            closeprint.Visibility = Visibility.Visible;
            printout.Visibility = Visibility.Visible;
            scrolller.Visibility = Visibility.Hidden;
        }

        private void closeprint_Click(object sender, RoutedEventArgs e)
        {
            mainframe.Visibility = Visibility.Hidden;
            closeprint.Visibility = Visibility.Hidden;
            printout.Visibility = Visibility.Hidden;
            scrolller.Visibility = Visibility.Visible;            
        }

        private void excportdata(object sender, RoutedEventArgs e)
        {
            if (solid == 0)
            {
                try
                {
                    staffdatagrid.SelectAllCells();
                    staffdatagrid.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
                    ApplicationCommands.Copy.Execute(null, staffdatagrid);
                    String resultat = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
                    String result = (string)Clipboard.GetData(DataFormats.Text);
                    staffdatagrid.UnselectAllCells();
                    System.IO.StreamWriter file1 = new System.IO.StreamWriter(@"viewAllStaff.xls");
                    file1.WriteLine(result.Replace(',', ' '));
                    file1.Close();

                    MessageBox.Show(" Exporting DataGrid data to Excel file created.xls");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("ONLY EXPORTS GENERAL DATA", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }            
        }
    }
}
