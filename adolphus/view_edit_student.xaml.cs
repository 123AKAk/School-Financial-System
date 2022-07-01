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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace adolphus
{
    /// <summary>
    /// Interaction logic for view_edit_student.xaml
    /// </summary>
    public partial class view_edit_student : Page
    {
        string strName, imageName;
        public view_edit_student()
        {
            InitializeComponent();
            this.GetStudData();
        }

        MySqlConnection con;
        MySqlDataAdapter daa;
        DataSet dss;
        MySqlCommandBuilder scmbl;

        MySqlConnection connection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
        MySqlCommand insertCommand;

        private void GetStudData()
        {
            //Gets data and inserts into datagrid
            try
            {
                string sql = "SELECT Id,Student_Image,First_Name,Second_Name,Surname,Student_Class,Registration_Number,Gender,Marital_Status,Phone_Number,Date_of_Birth,Age,Address,LGA,State," +
                    "Nationality,Religion,Disability,Blood_Group FROM studentdetails WHERE reme IS NULL OR reme = '' ORDER BY Id DESC";
                con = dtba.connectToDb();
                dss = new DataSet();
                DataTable dtt= new DataTable();
                daa = new MySqlDataAdapter(sql, con);
                daa.Fill(dss);
                studdatagrid.ItemsSource = dss.Tables[0].DefaultView;
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
            else if (e.Column.Header.ToString() == "Student_Image")
            {
                e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }
            else if (e.Column.Header.ToString() == "Student_Class")
            {
                e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }
            //else if (e.Column.Header.ToString() == "Registration_Number")
            //{
            //    e.Cancel = true;   // For not to include 
            //    e.Column.IsReadOnly = true; // Makes the column as read only
            //}
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

        string firstname, secondname, surname;
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            try
            {
                DataGrid dg = (DataGrid)sender;
                DataRowView rowSelected = dg.SelectedItem as DataRowView;                
                if (rowSelected != null)
                {
                    idds.Text = rowSelected[0].ToString();
                    firstname = rowSelected[2].ToString();
                    secondname = rowSelected[3].ToString();
                    surname = rowSelected[4].ToString();

                    Stud_Class.Text = rowSelected[5].ToString();                    
                    Stud_Reg.Text = rowSelected[6].ToString();

                    studname.Visibility = Visibility.Visible;
                    studname.Text = surname + ", " + firstname + " " + secondname;
                    Stud_Name.Text = surname + ", " + firstname + " " + secondname;

                    printstuddata.IsEnabled = true;

                    studimage.Source = eximageSource.Source;

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
                    studimage.Source = bi;                   
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
                    imageName = fldlg.FileName;

                    if (imageName == "")
                    {
                        MessageBox.Show("YOU DID NOT CHOOSE AN IMAGE");
                    }
                    else
                    {
                        strName = fldlg.SafeFileName;
                        ImageSourceConverter isc = new ImageSourceConverter();
                        studimage.SetValue(Image.SourceProperty, isc.ConvertFromString(imageName));

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
            if(idds.Text != "")
            {
                FileStream fs = new FileStream(imageName, FileMode.Open, FileAccess.Read);
                byte[] imgByteArr = new byte[fs.Length];
                fs.Read(imgByteArr, 0, Convert.ToInt32(fs.Length));
                fs.Close();
                //create insert query                                            
                string insertQuery = "UPDATE studentdetails SET Student_Image=@dStudent_Image, imagepath='" + strName + "' WHERE Id='" + idds.Text + "'";
                //string insertQuery = "INSERT INTO studentdetails(imagepath,Student_Image)values('" + strName + "', @dStudent_Image) WHERE Id='" + idds.Text + "'";
                insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@dStudent_Image", imgByteArr);
                try
                {
                    connection.Open();
                    int result = insertCommand.ExecuteNonQuery();
                    if (result > 0) { notify.IsOpen = true; showing.Text = "STUDENT IMAGE UPDATED"; GetStudData(); updateimage.Visibility = Visibility.Hidden; }
                    else
                    { notify.IsOpen = true; showing.Text = "ERROR!!! UNABLE TO UPDATE " + studname.Text + " IMAGE"; }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning); }
                finally { connection.Close(); }
            }
            else
            {
                notify.IsOpen = true;
                showing.Text = "SELECT A STUDENT FROM THE DATA-GRID BEFORE PROCEEDING ";
            }
        }        
        
        //update student info
        private void Updatestuddtata_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                scmbl = new MySqlCommandBuilder(daa);
                daa.Update(dss);                

                notify.IsOpen = true;
                showing.Text = "STUDENT INFROMATION UPDATED";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //Remove student from database
        string remv = "Removed";
        private void Removestuddata_Click(object sender, RoutedEventArgs e)
        {
            if (idds.Text != "")
            {
                //create insert query                                            
                string insertQuery = "UPDATE studentdetails SET reme=@dreme WHERE Id='" + idds.Text + "'"; ;
                //string insertQuery = "INSERT INTO studentdetails(imagepath,Student_Image)values('" + strName + "', @dStudent_Image) WHERE Id='" + idds.Text + "'";
                insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@dreme", remv);
                try
                {
                    connection.Open();
                    int result = insertCommand.ExecuteNonQuery();
                    if (result > 0) { MessageBox.Show("STUDENT HAS BEEN REMOVED", "Removed", MessageBoxButton.OK, MessageBoxImage.Information); GetStudData(); }
                    else
                    { MessageBox.Show("UNABLE TO REMOVE "+studname+"", "Error", MessageBoxButton.OK, MessageBoxImage.Warning); }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning); }
                finally { connection.Close(); }
            }
            else
            {
                notify.IsOpen = true;
                showing.Text = "SELECT A STUDENT FROM THE DATA-GRID BEFORE PROCEEDING ";
            }
        }             

        //search for student data
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            if (studsearch.Text != "")
            {
                if (typesearch.Text != "")
                {                
                    try
                    {
                        string sqlQuery = "SELECT Id,Student_Image,First_Name,Second_Name,Surname,Student_Class,Registration_Number,Gender,Marital_Status,Phone_Number,Date_of_Birth,Age,Address,LGA,State," +
                        "Nationality,Religion,Disability,Blood_Group FROM studentdetails WHERE " + typesearch.Text + "='" + studsearch.Text + "' AND reme IS NULL OR reme = '' ORDER BY Id DESC";
                        con = dtba.connectToDb();
                        dss = new DataSet();
                        DataTable dtt = new DataTable();
                        daa = new MySqlDataAdapter(sqlQuery, con);
                        daa.Fill(dss);
                        studdatagrid.ItemsSource = dss.Tables[0].DefaultView;
                   
                        studname.Text = "";
                        studimage.Source = eximageSource.Source;

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
                    showing.Text = "SELECT SEARCH TYPE";
                }
            }
            else
            {
                notify.IsOpen = true;
                showing.Text = "SEARCH FEILD IS EMPTY";
            }
        }
        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            datepick.Visibility = Visibility.Hidden;
            studsearch.Visibility = Visibility.Visible;
            studsearch.Text = "";
        }

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            datepick.Visibility = Visibility.Visible;
            studsearch.Visibility = Visibility.Hidden;
            studsearch.Text = "Date of things";
        }

        private void Studsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            search.IsEnabled = true;
        }

        int solid = 0;
        //view all data in database
        private void Viewall_Click(object sender, RoutedEventArgs e)
        {
            solid = 0;
            studname.Visibility = Visibility.Visible;
            ssss.Visibility = Visibility.Visible;
            eeee.Visibility = Visibility.Visible;
            studClassGrid.Visibility = Visibility.Hidden;
            autogenGrid.Visibility = Visibility.Hidden;

            studname.Text = "";
            GetStudData();
        }       

        private void Viewadmissindetails_Click(object sender, RoutedEventArgs e)
        {
            solid = 1;
            studname.Visibility = Visibility.Hidden;
            ssss.Visibility = Visibility.Hidden;
            eeee.Visibility = Visibility.Hidden;
            studClassGrid.Visibility = Visibility.Hidden;
            autogenGrid.Visibility = Visibility.Hidden;

            if (idds.Text != "")
            {
                try
                {
                    string sqlQuery = "SELECT Id,Student_Image,Last_School_Attended,Admission_Class,Date_of_Entry,Attended_Interview,Registration_Number FROM studentdetails WHERE id='" + idds.Text + "'";
                    con = dtba.connectToDb();
                    dss = new DataSet();
                    DataTable dtt = new DataTable();
                    daa = new MySqlDataAdapter(sqlQuery, con);
                    daa.Fill(dss);
                    studdatagrid.ItemsSource = dss.Tables[0].DefaultView;

                    animate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                notify.IsOpen = true;
                showing.Text = "SELECT A STUDENT FROM THE DATA-GRID BEFORE PROCEEDING ";
            }
        }

        private void Viewfamilydetails_Click(object sender, RoutedEventArgs e)
        {
            solid = 1;
            studname.Visibility = Visibility.Hidden;
            ssss.Visibility = Visibility.Hidden;
            eeee.Visibility = Visibility.Hidden;
            studClassGrid.Visibility = Visibility.Hidden;
            autogenGrid.Visibility = Visibility.Hidden;

            if (idds.Text != "")
            {
                try
                {
                    string sqlQuery = "SELECT Id,Student_Image,Father_Name,Mother_Name,Father_Occupation,Mother_Occupation,Father_Phone_Number," +
                        "Mother_Phone_Number,Father_Address,Mother_Address,Child_Position_in_Family,Number_of_Siblings,Madien_Language FROM studentdetails WHERE id='" + idds.Text + "'";
                    con = dtba.connectToDb();
                    dss = new DataSet();
                    DataTable dtt = new DataTable();
                    daa = new MySqlDataAdapter(sqlQuery, con);
                    daa.Fill(dss);
                    studdatagrid.ItemsSource = dss.Tables[0].DefaultView;

                    animate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                notify.IsOpen = true;
                showing.Text = "SELECT A STUDENT FROM THE DATA-GRID BEFORE PROCEEDING ";
            }
        }      

        private void Viewotherdetails_Click(object sender, RoutedEventArgs e)
        {
            solid = 1;
            studname.Visibility = Visibility.Hidden;
            ssss.Visibility = Visibility.Hidden;
            eeee.Visibility = Visibility.Hidden;
            studClassGrid.Visibility = Visibility.Hidden;
            autogenGrid.Visibility = Visibility.Hidden;

            if (idds.Text != "")
            {
                try
                {
                    string sqlQuery = "SELECT Id,Student_Image,First_Name,Second_Name,Surname,Special_Education,Medical_Exceptions,Local_Authority,Others FROM studentdetails WHERE id='" + idds.Text + "'";
                    con = dtba.connectToDb();
                    dss = new DataSet();
                    DataTable dtt = new DataTable();
                    daa = new MySqlDataAdapter(sqlQuery, con);
                    daa.Fill(dss);
                    studdatagrid.ItemsSource = dss.Tables[0].DefaultView;                    

                    animate();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                notify.IsOpen = true;
                showing.Text = "SELECT A STUDENT FROM THE DATA-GRID BEFORE PROCEEDING ";
            }
        }

        
        public string[] customer_Namea;
        List<TextBox> allAutoCreatedObjects = new List<TextBox>();        

        const int WIDTH = 160;
        const int HEIGHT = 30;

        int currentX = 0;
        const int STEP = 7;
        int calAll = 0;
        bool isButtonssOnScreen = false;        

        public static int setvalue;
        private void Vieweschoolxpenses_Click(object sender, RoutedEventArgs e)
        {            
            solid = 1;
            mainClass = "";
            string customer_Name = "";
            specialInfo.Text = "";
            itemsDate.Text = "";

            if (idds.Text != "" & Stud_Class.Text != "")
            {                
                if (Stud_Class.Text == "SS3")
                {
                    mainClass = "ss3class";
                }
                else if (Stud_Class.Text == "SS2")
                {
                    mainClass = "ss2class";
                }
                else if (Stud_Class.Text == "SS1")
                {
                    mainClass = "ss1class";
                }
                else if (Stud_Class.Text == "JSS3")
                {
                    mainClass = "jss3class";
                }
                else if (Stud_Class.Text == "JSS2")
                {
                    mainClass = "jss2class";
                }
                else if (Stud_Class.Text == "JSS1")
                {
                    mainClass = "jss1class";
                }
                else
                {
                    MessageBox.Show("Classes Has a Problem Contact Developer To Resolve Issue", "Problem Dey", MessageBoxButton.OK, MessageBoxImage.Stop);
                }

                MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
                MySqlConnection aconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
                MySqlCommand ccommand, acommand;
                var cQuery = "SELECT * FROM items WHERE Status IS NULL OR Status = ''";
                ccommand = new MySqlCommand(cQuery, cconnection);

                //parameter                        
                //ccommand.Parameters.AddWithValue("@dGfirstname", "OBLA");
                try
                {
                    var acQuery = "SELECT Term,Session," + mainClass + " FROM settings";

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

                    //for(int i = 0; i<customer_Namea.Length-1;  i++)
                    //{
                    //MessageBox.Show(customer_Name);
                    if(customer_Name == "")
                    {
                        MessageBox.Show("There are no Student Items to Display");
                        return;
                    }
                    else
                    {                         
                        try
                        {
                            string books = "";                            
                            MySqlConnection connectiona = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");                            

                            MySqlCommand command, commadf;                            
                            //checking if students info has been entered before
                            var aQuery = "SELECT * FROM studitemdisplay WHERE Stud_Reg = '" + Stud_Reg.Text + "' AND Term = '" + Term.Text + "' AND Session = '" + Session.Text + "'";
                            command = new MySqlCommand(aQuery, connectiona);
                            connectiona.Open();
                            MySqlDataReader readera = command.ExecuteReader();
                            while (readera.Read())
                            {
                                gadid = readera["Id"].ToString();
                                gadda = readera["Stud_Name"].ToString();
                                gaddb = readera["Term"].ToString();
                                gaddc = readera["Session"].ToString();
                                gaddd = readera["Stud_Reg"].ToString();
                                specialInfo.Text = readera["Notes"].ToString();
                                itemsDate.Text = readera["Date"].ToString();
                                books = readera["books"].ToString();
                            }
                            if (gadda == Stud_Name.Text && gaddd == Stud_Reg.Text && gaddb == Term.Text && gaddc == Session.Text)
                            {
                                //if the same insert values into the generated texboxes and button becomes update not add
                                if (!isButtonssOnScreen)
                                {
                                    MySqlConnection connectionf = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
                                    string hhhhh = "";
                                    for (int i = 0; i <= customer_Namea.Length - 2; i++)
                                    {
                                        //MessageBox.Show(Stud_Reg.Text);
                                        var eQuery = "SELECT * FROM studitemdisplay WHERE Stud_Reg = '" + Stud_Reg.Text + "' AND Term = '" + Term.Text + "' AND Session = '" + Session.Text + "'";
                                        commadf = new MySqlCommand(eQuery, connectionf);
                                        connectionf.Close();
                                        connectionf.Open();
                                        MySqlDataReader readerg = commadf.ExecuteReader();
                                        while (readerg.Read())
                                        {
                                            hhhhh = readerg[""+ customer_Namea[i]].ToString();
                                            //MessageBox.Show(hhhhh);
                                        }

                                        TextBox TextBoxa = new TextBox();                                    
                                        TextBoxa.Name = customer_Namea[i];
                                        TextBoxa.BorderBrush = Brushes.LightGray;
                                        TextBoxa.BorderThickness = new Thickness(1, 1, 1, 1);
                                        TextBoxa.Text = hhhhh;
                                        TextBoxa.FontSize = 13;
                                        TextBoxa.ToolTip = customer_Namea[i];
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
                                        calAll = currentX * STEP;
                                        MainCanvas.Children.Add(item);
                                    }
                                    isButtonssOnScreen = true;
                                }
                                else
                                {                                 
                                    foreach (TextBox TextBoxa in allAutoCreatedObjects)
                                    {                                
                                        MainCanvas.Children.Remove(TextBoxa);
                                        MainCanvas.Children.Clear();
                                    }

                                    MySqlConnection connectionf = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
                                    string hhhhh = "";
                                    for (int i = 0; i <= customer_Namea.Length - 2; i++)
                                    {
                                        //MessageBox.Show(Stud_Reg.Text);
                                        var eQuery = "SELECT * FROM studitemdisplay WHERE Stud_Reg = '" + Stud_Reg.Text + "' AND Term = '" + Term.Text + "' AND Session = '" + Session.Text + "'";
                                        commadf = new MySqlCommand(eQuery, connectionf);
                                        connectionf.Close();
                                        connectionf.Open();
                                        MySqlDataReader readerg = commadf.ExecuteReader();
                                        while (readerg.Read())
                                        {
                                            hhhhh = readerg["" + customer_Namea[i]].ToString();
                                            //MessageBox.Show(hhhhh);
                                        }

                                        TextBox TextBoxa = new TextBox();
                                        TextBoxa.Name = customer_Namea[i];
                                        TextBoxa.BorderBrush = Brushes.LightGray;
                                        TextBoxa.BorderThickness = new Thickness(1, 1, 1, 1);
                                        TextBoxa.ToolTip = customer_Namea[i];
                                        TextBoxa.Text = hhhhh;
                                        TextBoxa.FontSize = 13;
                                        TextBoxa.Width = WIDTH; TextBoxa.Height = HEIGHT;
                                        TextBoxa.TextAlignment = TextAlignment.Center;

                                        TextBoxa.Padding = new Thickness(0, 0, 0, 0);
                                        TextBoxa.Margin = new Thickness(5, 0, 5, 0);

                                        allAutoCreatedObjects.RemoveAt(0);                                       
                                        allAutoCreatedObjects.Add(TextBoxa);

                                        TextBoxa.TextChanged += new TextChangedEventHandler(autoGenTextChange);
                                    }
                                    int currentXA = 0;                                    
                                    const int STEPA = 7;                                    
                                    foreach (TextBox TextBoxa in allAutoCreatedObjects)
                                    {
                                        Canvas.SetLeft(TextBoxa, STEPA * currentXA/ 7);
                                        currentXA += 175;
                                        MainCanvas.Width = calAll;
                                        MainCanvas.Children.Add(TextBoxa);
                                    }
                                    isButtonssOnScreen = true;
                                }
                            }
                            else
                            {
                                //if not the same generated the texboxes and button becomes add not update                                
                                if (isButtonssOnScreen)
                                {
                                    foreach (TextBox TextBoxa in allAutoCreatedObjects)
                                    {
                                        MainCanvas.Children.Remove(TextBoxa);
                                        MainCanvas.Children.Clear();
                                    }
                                    for (int i = 0; i <= customer_Namea.Length - 2; i++)
                                    {
                                        TextBox TextBoxa = new TextBox();
                                        TextBoxa.Name = customer_Namea[i];
                                        TextBoxa.BorderBrush = Brushes.LightGray;
                                        TextBoxa.BorderThickness = new Thickness(1, 1, 1, 1);
                                        TextBoxa.Text = customer_Namea[i];
                                        TextBoxa.ToolTip = customer_Namea[i];
                                        TextBoxa.FontSize = 13;
                                        TextBoxa.Width = WIDTH; TextBoxa.Height = HEIGHT;
                                        TextBoxa.TextAlignment = TextAlignment.Center;

                                        TextBoxa.Padding = new Thickness(0, 0, 0, 0);
                                        TextBoxa.Margin = new Thickness(5, 0, 5, 0);
                                        allAutoCreatedObjects.RemoveAt(0);
                                        allAutoCreatedObjects.Add(TextBoxa);

                                        TextBoxa.TextChanged += new TextChangedEventHandler(autoGenTextChange);
                                    }
                                    int currentXA = 0;
                                    const int STEPA = 7;
                                    foreach (TextBox TextBoxa in allAutoCreatedObjects)
                                    {
                                        Canvas.SetLeft(TextBoxa, STEPA * currentXA / 7);
                                        currentXA += 175;
                                        MainCanvas.Width = calAll;
                                        MainCanvas.Children.Add(TextBoxa);
                                    }
                                    isButtonssOnScreen = true;
                                }                            
                                else
                                {
                                    for (int i = 0; i <= customer_Namea.Length - 2; i++)
                                    {
                                        TextBox TextBoxa = new TextBox();
                                        TextBoxa.Name = customer_Namea[i];
                                        TextBoxa.BorderBrush = Brushes.LightGray;
                                        TextBoxa.BorderThickness = new Thickness(1, 1, 1, 1);
                                        TextBoxa.Text = customer_Namea[i];
                                        TextBoxa.ToolTip = customer_Namea[i];
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
                                        calAll = currentX * STEP;
                                        MainCanvas.Children.Add(item);
                                    }
                                    isButtonssOnScreen = true;
                                }

                                MySqlCommand insertCommand;
                                MySqlConnection connection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");                                
                                string insertQuery = "INSERT INTO studitemdisplay(Stud_Name,Stud_Reg,Session,Term,Stud_Class,Notes)values(@dStud_Name,@dStud_Reg,@dSession,@dTerm,@dStud_Class,@dNotes)";
                                insertCommand = new MySqlCommand(insertQuery, connection);                                
                                insertCommand.Parameters.AddWithValue("@dStud_Name", Stud_Name.Text);
                                insertCommand.Parameters.AddWithValue("@dStud_Reg", Stud_Reg.Text);
                                insertCommand.Parameters.AddWithValue("@dSession", Session.Text);
                                insertCommand.Parameters.AddWithValue("@dTerm", Term.Text);
                                insertCommand.Parameters.AddWithValue("@dStud_Class", Stud_Class.Text);                                
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
                                        MessageBox.Show("THERE IS A PROBLEM REQUEST DEVELOPER");
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
                            studname.Visibility = Visibility.Hidden;
                            ssss.Visibility = Visibility.Hidden;
                            eeee.Visibility = Visibility.Hidden;
                            studClassGrid.Visibility = Visibility.Hidden;
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
                    MessageBox.Show(ex.Message+"...#");
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
                showing.Text = "STUDENT DOES NOT HAVE A CLASS";
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

            if(textBox.Name.Contains(substr1))
            {
                MySqlConnection connection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
                MySqlCommand gucommand;
                string Query = "UPDATE studitemdisplay SET " + txtName + "=@dtxtName WHERE Stud_Reg='" + Stud_Reg.Text + "' AND Term = '" + Term.Text + "' AND Session = '" + Session.Text + "'";
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
                if(textBox.Text == "")
                {

                }
                else if (int.TryParse(textBox.Text, out parsedValue))
                {
                    MySqlConnection connection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
                    MySqlCommand gucommand;
                    string Query = "UPDATE studitemdisplay SET " + txtName + "=@dtxtName WHERE Stud_Reg='" + Stud_Reg.Text + "' AND Term = '" + Term.Text + "' AND Session = '" + Session.Text + "'";
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
                    showing.Text = textBox.Name+" Is a Numbers Only Feild";
                    textBox.Text = "";
                }
            }            
        }
        private void specialInfoChange(object sender, TextChangedEventArgs e)
        {
            if (autogenGrid.Visibility == Visibility.Visible)
            {
                MySqlConnection connection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
                MySqlCommand gucommand;
                string Query = "UPDATE studitemdisplay SET Notes=@dtxtName WHERE Stud_Reg='" + Stud_Reg.Text + "' AND Term = '" + Term.Text + "' AND Session = '" + Session.Text + "'";
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
            else
            {

            }
        }
        private void itemsDate_CalendarClosed(object sender, RoutedEventArgs e)
        {
            if (autogenGrid.Visibility == Visibility.Visible)
            {
                MySqlConnection connection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
                MySqlCommand gucommand;
                string Query = "UPDATE studitemdisplay SET Date=@ddate WHERE Stud_Reg='" + Stud_Reg.Text + "' AND Term = '" + Term.Text + "' AND Session = '" + Session.Text + "'";
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
            else
            { }
        }
       
        private void closeItem_Click(object sender, RoutedEventArgs e)
        {
            autogenGrid.Visibility = Visibility.Hidden;
        }

        //GET STUD CLASS
        private void getStudClass(object sender, RoutedEventArgs e)
        {
            solid = 1;
            studname.Visibility = Visibility.Hidden;
            ssss.Visibility = Visibility.Hidden;
            eeee.Visibility = Visibility.Hidden;            

            current_class.Text = Stud_Class.Text;

            names.Text = Stud_Name.Text;
            if (idds.Text != ""){try{string sqlQuery = "SELECT Id,Student_Image,Student_Class,Others FROM studentdetails WHERE id='" + idds.Text + "'";
            con = dtba.connectToDb();dss = new DataSet();DataTable dtt = new DataTable();
            daa = new MySqlDataAdapter(sqlQuery, con);daa.Fill(dss);studdatagrid.ItemsSource = dss.Tables[0].DefaultView;}
            catch (Exception ex){MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);}}
            else{notify.IsOpen = true;showing.Text = "SELECT A STUDENT FROM THE DATA-GRID BEFORE PROCEEDING ";}

            studClassGrid.Visibility = Visibility.Visible;
        }
        private void changeClass_Click(object sender, RoutedEventArgs e)
        {
            solid = 0;
            studname.Visibility = Visibility.Visible;
            ssss.Visibility = Visibility.Visible;
            eeee.Visibility = Visibility.Visible;

            MySqlConnection connection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
            MySqlCommand gucommand;
            string Query = "UPDATE studentdetails SET Student_Class=@dStud_Class WHERE @idds = Id";
            gucommand = new MySqlCommand(Query, connection);            
            gucommand.Parameters.AddWithValue("@idds", idds.Text);
            gucommand.Parameters.AddWithValue("@dStud_Class", current_class.Text);
            try
            {
                connection.Open();
                int result = gucommand.ExecuteNonQuery();
                if (result > 0)
                { MessageBox.Show("Class Changed"); studname.Text = "";GetStudData();}
                else
                { MessageBox.Show("Unable To Change Class", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);}
            }
            catch (Exception ex)
            {MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);}
            finally
            {connection.Close(); studClassGrid.Visibility = Visibility.Hidden; }                        
        }
        private void bacck_Click(object sender, RoutedEventArgs e)
        {
            solid = 0;
            studname.Visibility = Visibility.Visible;
            ssss.Visibility = Visibility.Visible;
            eeee.Visibility = Visibility.Visible;
            GetStudData();
            studClassGrid.Visibility = Visibility.Hidden;
        }

        private void animate()
        {
            studname.Text = "";
            //studimage.Source = eximageSource.Source;
            printstuddata.IsEnabled = false;

            //create animation on datagrid
            //DoubleAnimation doubleanimation = new DoubleAnimation();
            //doubleanimation.From = 50;
            //doubleanimation.To = 275;
            //doubleanimation.Duration = TimeSpan.FromSeconds(1);

            //doubleanimation.EasingFunction = new QuarticEase();
            //studdatagrid.BeginAnimation(HeightProperty, doubleanimation);
            //animation ends here
        }

        //PRINT STUDENTS DETAILS
        private void Printstuddata_Click(object sender, RoutedEventArgs e)
        {
            Settings1.Default.studentId = Convert.ToInt32(idds.Text);
            study objOpen = new study();
            mainframe.NavigationService.Navigate(objOpen);
            mainframe.Visibility = Visibility.Visible;
            closeprint.Visibility = Visibility.Visible;
            printout.Visibility = Visibility.Visible;
            scroller.Visibility = Visibility.Hidden;
        }

        private void closeprint_Click(object sender, RoutedEventArgs e)
        {
            mainframe.Visibility = Visibility.Hidden;
            closeprint.Visibility = Visibility.Hidden;
            printout.Visibility = Visibility.Hidden;
            scroller.Visibility = Visibility.Visible;
        }       

        //gets the neccessary things for for student school fees
        string gadid, gadda, gaddb, gaddc, gaddd, gadde, gaddf, gaddg, gaddh, mainClass;        
        private void schoolfees(object sender, RoutedEventArgs e)
        {
            mainClass = "";

            Term.Text = "";
            Session.Text = "";
            School_Fees.Text = "";
            Paid_School_Fees.Text = "";
            Fees_owed.Text = "";
            Notes.Text = "";
            Date.Text = "";

            if (Stud_Class.Text != "")
            {
                if(Stud_Class.Text == "SS3")
                {
                    mainClass = "ss3class";
                }
                else if(Stud_Class.Text == "SS2")
                {
                    mainClass = "ss2class";
                }
                else if (Stud_Class.Text == "SS1")
                {
                    mainClass = "ss1class";
                }
                else if (Stud_Class.Text == "JSS3")
                {
                    mainClass = "jss3class";
                }
                else if (Stud_Class.Text == "JSS2")
                {
                    mainClass = "jss2class";
                }
                else if (Stud_Class.Text == "JSS1")
                {
                    mainClass = "jss1class";
                }
                else
                {
                    MessageBox.Show("Classes Has a Problem Contact Developer To Resolve Issue", "Problem Dey", MessageBoxButton.OK, MessageBoxImage.Stop);
                }

                MySqlConnection connection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
                MySqlConnection connectiona = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");

                schoolfeesbox.Visibility = Visibility.Visible;

                MySqlCommand ccommand, command;

                var cQuery = "SELECT Term,Session,"+mainClass+" FROM settings";

                ccommand = new MySqlCommand(cQuery, connection);

                try
                {
                    //for ecommandc
                    connection.Open();

                    MySqlDataReader readerc = ccommand.ExecuteReader();
                    while (readerc.Read())
                    {
                        Term.Text = readerc["Term"].ToString();
                        Session.Text = readerc["Session"].ToString();
                        School_Fees.Text = readerc[mainClass].ToString();
                    }

                    var aQuery = "SELECT * FROM fess WHERE Stud_Reg = '" + Stud_Reg.Text + "' AND Term = '" + Term.Text + "' AND Session = '" + Session.Text + "'";
                    command = new MySqlCommand(aQuery, connectiona);
                    connectiona.Open();
                    MySqlDataReader readera = command.ExecuteReader();
                    while (readera.Read())
                    {
                        gadid = readera["Id"].ToString();
                        gadda = readera["Stud_Name"].ToString();
                        gaddb = readera["Term"].ToString();
                        gaddc = readera["Session"].ToString();
                        gaddd = readera["Stud_Reg"].ToString();
                        gadde = readera["Paid_School_Fees"].ToString();
                        gaddf = readera["Fees_owed"].ToString();
                        gaddg = readera["Notes"].ToString();
                        gaddh = readera["Date"].ToString();
                    }
                    if (gadda == Stud_Name.Text && gaddb == Term.Text && gaddc == Session.Text)
                    {
                        Paid_School_Fees.Text = gadde;
                        Fees_owed.Text = gaddf;
                        Notes.Text = gaddg;
                        Date.Text = gaddh;
                        add.Visibility = Visibility.Hidden;
                        update.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        add.Visibility = Visibility.Visible;
                        update.Visibility = Visibility.Hidden;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                    connectiona.Close();
                }
            }
            else
            {
                notify.IsOpen = true;
                showing.Text = "STUDENT DOES NOT HAVE A CLASS";
            }
        }

        //does the subtraction of school fees
        int parsedValue, paidschoolfees, aschool_fees, result;        
        private void Paid_School_Fees_TextChanged(object sender, TextChangedEventArgs e)
        {            
            if (!int.TryParse(Paid_School_Fees.Text, out parsedValue))
            {                                
                return;
            }
            else
            {
                paidschoolfees = Convert.ToInt32(Paid_School_Fees.Text);
                aschool_fees = Convert.ToInt32(School_Fees.Text);                
                result = aschool_fees - paidschoolfees;
                Fees_owed.Text = result.ToString();
                if(Paid_School_Fees.Text == "")
                {
                    Fees_owed.Text = "";
                }
                else if (paidschoolfees > aschool_fees)
                {
                    MessageBox.Show("Paid School Fees Cannot Be Greater Than School Fees", "Problem Dey", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void addschoolfees(object sender, RoutedEventArgs e)
        {
            if(Paid_School_Fees.Text == "")
            {
                MessageBox.Show("School Fees Feild is Empty");
            }
            else if (Date.Text == "")
            {
                var Result = MessageBox.Show("Date of Payement Feild is Empty. \n \nDo you want to Continue? If 'Yes' Date will be Null", "Question:", MessageBoxButton.YesNo, MessageBoxImage.Question);
                //MessageBoxResult result = MessageBox.Show("");
                if (Result == MessageBoxResult.Yes)
                {
                    Date.Text = null;
                }
                else if (Result == MessageBoxResult.No)
                {
                    MessageBox.Show("You have to Select a Date");
                }
            }
            else if (!int.TryParse(Paid_School_Fees.Text, out parsedValue))
            {
                MessageBox.Show("School Fees Feild is a number only Feild", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            else if (Paid_School_Fees.Text == "")
            {
                Fees_owed.Text = "";
            }
            else if (paidschoolfees > aschool_fees)
            {
                MessageBox.Show("Paid School Fees Cannot Be Greater Than School Fees", "Problem Dey", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                string Date_Of_Insertion = DateTime.Now.ToString();
                string Datea = DateTime.Now.ToString("dd/MM/yyyy");

                //MySql connection
                MySqlCommand insertCommand;

                MySqlConnection connection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");

                //create insert query
                string insertQuery = "INSERT INTO fess(Stud_Name,Stud_Reg,Session,Term,Stud_Class,School_Fees,Paid_School_Fees,Fees_owed,Date_Of_Insertion,Date,Notes)values(@dStud_Name,@dStud_Reg,@dSession,@dTerm,@dStud_Class,@dSchool_Fees,@dPaid_School_Fees,@dFees_owed,@dDate_Of_Insertion,@dDate,@dNotes)";

                // Create Sql Command                 
                insertCommand = new MySqlCommand(insertQuery, connection);

                //Add parameters to the command                                                                                                          
                insertCommand.Parameters.AddWithValue("@dStud_Name", Stud_Name.Text);
                insertCommand.Parameters.AddWithValue("@dStud_Reg", Stud_Reg.Text);
                insertCommand.Parameters.AddWithValue("@dSession", Session.Text);
                insertCommand.Parameters.AddWithValue("@dTerm", Term.Text);
                insertCommand.Parameters.AddWithValue("@dStud_Class", Stud_Class.Text);
                insertCommand.Parameters.AddWithValue("@dSchool_Fees", School_Fees.Text);
                insertCommand.Parameters.AddWithValue("@dPaid_School_Fees", Paid_School_Fees.Text);
                insertCommand.Parameters.AddWithValue("@dFees_owed", Fees_owed.Text);
                insertCommand.Parameters.AddWithValue("@dDate_Of_Insertion", Date_Of_Insertion);
                insertCommand.Parameters.AddWithValue("@dDate", Date.Text);
                insertCommand.Parameters.AddWithValue("@dNotes", Notes.Text);

                try
                {
                    connection.Open();
                    int result = insertCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        notify.IsOpen = true;
                        showing.Text = "FEES INFO ADDED";

                        add.Visibility = Visibility.Hidden;
                        update.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        notify.IsOpen = true;
                        showing.Text = "ERROR!!! UNABLE TO ADD FEES INFO";
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
        private void updateschoolfees(object sender, RoutedEventArgs e)
        {
            if (Date.Text == "")
            {
                MessageBox.Show("Date of Payement Feild is Empty");
            }
            else
            {
                MySqlConnection connection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");

                MySqlCommand gucommand;

                string Query = "UPDATE fess SET Stud_Name=@dStud_Name,Stud_Reg=@dStud_Reg,Session=@dSession,Term=@dTerm,Stud_Class=@dStud_Class,School_Fees=@dSchool_Fees,Paid_School_Fees=@dPaid_School_Fees,Fees_owed=@dFees_owed,Date=@ddatea,Notes=@dNotes WHERE @idds = Id";

                gucommand = new MySqlCommand(Query, connection);

                //Add parameters to the command                                           
                //guarantors            
                gucommand.Parameters.AddWithValue("@idds", gadid);
                gucommand.Parameters.AddWithValue("@dStud_Name", Stud_Name.Text);
                gucommand.Parameters.AddWithValue("@dStud_Reg", Stud_Reg.Text);
                gucommand.Parameters.AddWithValue("@dSession", Session.Text);
                gucommand.Parameters.AddWithValue("@dTerm", Term.Text);
                gucommand.Parameters.AddWithValue("@dStud_Class", Stud_Class.Text);
                gucommand.Parameters.AddWithValue("@dSchool_Fees", School_Fees.Text);
                gucommand.Parameters.AddWithValue("@dPaid_School_Fees", Paid_School_Fees.Text);
                gucommand.Parameters.AddWithValue("@dFees_owed", Fees_owed.Text);
                insertCommand.Parameters.AddWithValue("@ddatea", Date.Text);
                gucommand.Parameters.AddWithValue("@dNotes", Notes.Text);

                try
                {
                    connection.Open();
                    int result = gucommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        notify.IsOpen = true;
                        showing.Text = "STUDENT FEES INFO UPDATED";
                    }
                    else
                    {
                        notify.IsOpen = true;
                        showing.Text = "ERROR!!! UNABLE TO UPDATE FEES INFO";
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
        private void hideschoolfeesbox(object sender, RoutedEventArgs e)
        {
            schoolfeesbox.Visibility = Visibility.Hidden;
        }

        //promoting student
        private void promote_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("NOT AVAILABLE AT THE MOMENT");
        }

        //exporting datagrid data
        private void excportdata(object sender, RoutedEventArgs e)
        {
            if (solid == 0)
            {
                try
                {
                    studdatagrid.SelectAllCells();
                    studdatagrid.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
                    ApplicationCommands.Copy.Execute(null, studdatagrid);
                    String resultat = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
                    String result = (string)Clipboard.GetData(DataFormats.Text);
                    studdatagrid.UnselectAllCells();
                    System.IO.StreamWriter file1 = new System.IO.StreamWriter(@"viewAllStudent.xls");
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
