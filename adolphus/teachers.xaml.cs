using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interaction logic for teachers.xaml
    /// </summary>
    public partial class teachers : Page
    {
        string strName, imageName;

        public teachers()
        {
            InitializeComponent();
        }

        public string gender = null;

        //MySql connection
        MySqlConnection connection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
        MySqlCommand insertCommand;

        private void Female_Checked(object sender, RoutedEventArgs e)
        {
            gender = "FEMALE";
        }

        private void Male_Checked(object sender, RoutedEventArgs e)
        {
            gender = "MALE";
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

                    strName = fldlg.SafeFileName;
                    ImageSourceConverter isc = new ImageSourceConverter();
                    staffimage.SetValue(Image.SourceProperty, isc.ConvertFromString(imageName));
                }
                fldlg = null;                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IMAGE FEILD CANNOT BE EMPTY", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Insertstaff_Click(object sender, RoutedEventArgs e)
        {
            if (ignorevalidation.IsChecked == true)
            {
                if (String.IsNullOrEmpty(imageName))
                {
                    notify.IsOpen = true;
                    showing.Text = "ERROR!!! THERE IS NO IMAGE IN THE IMAGE AREA";
                }
                else if (firstname.Text == "")
                {
                    notify.IsOpen = true;
                    showing.Text = "FIRST NAME FEILD IS EMPTY";
                }
                else if (secondname.Text == "")
                {
                    notify.IsOpen = true;
                    showing.Text = "SECOND NAME FEILD IS EMPTY";
                }
                else if (surname.Text == "")
                {
                    notify.IsOpen = true;
                    showing.Text = "SURNAME FEILD IS EMPTY";
                }
                else if (gender != ("MALE") && gender != ("FEMALE"))
                {
                    notify.IsOpen = true;
                    showing.Text = "SELECT GENDER";
                }
            }
            else
            {
                if (String.IsNullOrEmpty(imageName))
                {
                    notify.IsOpen = true;
                    showing.Text = "ERROR!!! THERE IS NO IMAGE IN THE IMAGE AREA";
                }
                else if (firstname.Text == "")
                {
                    notify.IsOpen = true;
                    showing.Text = "FIRST NAME FEILD IS EMPTY";
                }
                else if (secondname.Text == "")
                {
                    notify.IsOpen = true;
                    showing.Text = "SECOND NAME FEILD IS EMPTY";
                }
                else if (surname.Text == "")
                {
                    notify.IsOpen = true;
                    showing.Text = "SURNAME FEILD IS EMPTY";
                }
                else if (gender != ("MALE") && gender != ("FEMALE"))
                {
                    notify.IsOpen = true;
                    showing.Text = "SELECT GENDER";
                }
                else if (phonenumber.Text == "")
                {
                    notify.IsOpen = true;
                    showing.Text = "PHONE NUMBER FEILD IS EMPTY";
                }
                else if (maritalstatus.Text == "")
                {
                    notify.IsOpen = true;
                    showing.Text = "SELECT STAFF'S MARITAL STATUS";
                }
                else if (dateofbirth.Text == "")
                {

                    showing.Text = "DATE OF BIRTH FEILD IS EMPTY";
                }
                else if (address.Text == "")
                {
                    notify.IsOpen = true;
                    showing.Text = "HOME ADDRESS FEILD IS EMPTY";
                }
                else if (address.Text == "")
                {
                    notify.IsOpen = true;
                    showing.Text = "ADDRESS FEILD IS EMPTY";
                }
                else if (lga.Text == "")
                {
                    notify.IsOpen = true;
                    showing.Text = "LGA FEILD IS EMPTY";
                }
                else if (state.Text == "")
                {
                    notify.IsOpen = true;
                    showing.Text = "STATE FEILD IS EMPTY";
                }
                else if (nationality.Text == "")
                {
                    notify.IsOpen = true;
                    showing.Text = "NATIONALITY FEILD IS EMPTY";
                }
                else if (religion.Text == "")
                {
                    notify.IsOpen = true;
                    showing.Text = "SELECT STAFF'S RELIGION";
                }               
                else
                {
                    //Inserts student's information into database
                    insertstaffdata();
                }
            }
        }

        private void Clearstaff_Click(object sender, RoutedEventArgs e)
        {
            staffimage.Source = null;
            firstname.Clear();
            secondname.Clear();
            surname.Clear();
            male.IsChecked = false;
            female.IsChecked = false;
            maritalstatus.Text = "";
            phonenumber.Clear();
            dateofbirth.Text = "";
            address.Clear();
            lga.Text = "";
            state.Text = "";
            nationality.Clear();
            religion.Text = "";            
            bloodgroup.Clear();
            subjectteaching.Clear();
            classteaching.Clear();
            salary.Clear();
            positioninschool.Clear();            
        }

        //function inserts data into database
        private void insertstaffdata()
        {
            //Initialize a file stream to read the image file
            FileStream fs = new FileStream(imageName, FileMode.Open, FileAccess.Read);

            //Initialize a byte array with size of stream
            byte[] imgByteArr = new byte[fs.Length];

            //Read data from the file stream and put into the byte array
            fs.Read(imgByteArr, 0, Convert.ToInt32(fs.Length));

            //Close a file stream
            fs.Close();

            //create insert query                                
            string insertQuery = "INSERT INTO staffdetails(imagepath,Staff_Image,First_Name,Second_Name,Surname,Gender,Marital_Status,Phone_Number,Date_of_Birth,Address,LGA,State,Nationality,Religion,Blood_Group,Subject_Teaching,Class_Teaching,Salary,Position_in_School,Others)values('" + strName + "',@dStaff_Image,@dFirst_Name,@dSecond_Name,@dSurname,@dGender,@dMarital_Status,@dPhone_Number,@dDate_of_Birth,@dAddress,@dLGA,@dState,@Nationality,@dReligion,@dBlood_Group,@dSubject_Teaching,@dClass_Teaching,@dSalary,@dPosition_in_School,@dOthers)";

            // Create Sql Command                 
            insertCommand = new MySqlCommand(insertQuery, connection);

            //Add parameters to the command                                                                              
            insertCommand.Parameters.AddWithValue("@dStaff_Image", imgByteArr);
            insertCommand.Parameters.AddWithValue("@dFirst_Name", firstname.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dSecond_Name", secondname.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dSurname", surname.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dGender", gender.ToUpper());
            insertCommand.Parameters.AddWithValue("@dMarital_Status", maritalstatus.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dPhone_Number", phonenumber.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dDate_of_Birth", dateofbirth.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dAddress", address.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dLGA", lga.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dState", state.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@Nationality", nationality.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dReligion", religion.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dBlood_Group", bloodgroup.Text.ToUpper());

            //school info
            insertCommand.Parameters.AddWithValue("@dSubject_Teaching", subjectteaching.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dClass_Teaching", classteaching.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dSalary", salary.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dPosition_in_School", positioninschool.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dOthers", "");


            try
            {
                connection.Open();
                int result = insertCommand.ExecuteNonQuery();
                if (result > 0)
                {
                    notify.IsOpen = true;
                    showing.Text = "STAFF INFORAMTION ACCEPTED";
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
}
