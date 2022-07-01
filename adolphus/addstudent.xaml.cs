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
    /// Interaction logic for addstudent.xaml
    /// </summary>
    public partial class addstudent : Page
    {

        string strName, imageName;

        public addstudent()
        {
            InitializeComponent();           
        }                

        public string gender = null;
        public string attended_interview = "NO";
        public string special_education = "NO";
        public string medical_exceptions = "NO";
        public string local_authority = "NO";

        //MySql connection
        MySqlConnection connection = new MySqlConnection("server=localhost:8081;database=eyosms;uid=root;pwd=;");
        MySqlCommand insertCommand;

        private void Female_Checked(object sender, RoutedEventArgs e)
        {
            gender = "MALE";
        }

        private void Male_Checked(object sender, RoutedEventArgs e)
        {
            gender = "FEMALE";
        }

        private void Attendedinterview_Checked(object sender, RoutedEventArgs e)
        {
            attended_interview = "YES";
        }

        private void Specialeducation_Checked(object sender, RoutedEventArgs e)
        {
            special_education = "YES";
        }

        private void Medicalexceptions_Checked(object sender, RoutedEventArgs e)
        {
            medical_exceptions = "YES";
        }

        private void Localauthority_Checked(object sender, RoutedEventArgs e)
        {
            local_authority = "YES";
        }

        private void Browseimage_Click(object sender, RoutedEventArgs e)
        {
            //Browse image from the computer and inserts it into the app
            imageName = "C:/dever.jpg";
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
                    }                   
                }
                fldlg = null;                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "IMAGE FEILD CANNOT BE EMPTY", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        
        private void Insertdata_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(imageName);            

            int parsedValue;

            if (ignorevalidation.IsChecked == true)
            {
                if (String.IsNullOrEmpty(imageName))
                {                    
                    notify.IsOpen = true;
                    showing.Text = "ERROR!!! THERE IS NO IMAGE IN THE IMAGE AREA";
                }
                else if(registrationNo.Text == "")
                {
                    notify.IsOpen = true;
                    showing.Text = "GENERATE REGISTRATION NUMBER";
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
                else
                {
                    //inserts students info into db
                    insertstuddata();                    
                }
            }
            else
            {
                if (String.IsNullOrEmpty(imageName))
                {
                    notify.IsOpen = true;
                    showing.Text = "ERROR!!! THERE IS NO IMAGE IN THE IMAGE AREA";
                }
                else if (registrationNo.Text == "")
                {
                    notify.IsOpen = true;
                    showing.Text = "GENERATE REGISTRATION NUMBER";
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
                else if (maritalstatus.Text == "")
                {
                    notify.IsOpen = true;
                    showing.Text = "SELECT STUDENT'S MARITAL STATUS";
                }
                else if (dateofbirth.Text == "")
                {
                    notify.IsOpen = true;
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
                    showing.Text = "SELECT STUDENT'S RELIGION";
                }
                else if (disability.Text == "")
                {
                    notify.IsOpen = true;
                    showing.Text = "DISABILITY FEILD IS EMPTY";
                }
                else if (!int.TryParse(age.Text, out parsedValue))
                {
                    MessageBox.Show("Age Feild is a number only Feild", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else if (!int.TryParse(phonenumber.Text, out parsedValue))
                {
                    MessageBox.Show("Phone Number Feild is a number only Feild", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    //inserts students info into db
                    insertstuddata();
                }
            }
        }        

        private void Cleardata_Click(object sender, RoutedEventArgs e)
        {
            imageName = "";
            strName = "";
            studimage.Source = null;
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
            disability.Clear();
            bloodgroup.Clear();
            fathername.Clear();
            mothername.Clear();
            fatheroccupation.Clear();
            motheroccupation.Clear();
            fatherphonenumber.Clear();
            motherphonenumber.Clear();
            fatheraddress.Clear();
            motheraddress.Clear();
            childpositioninfamily.Clear();
            numberofsiblings.Clear();
            madienlanguage.Clear();
            lastschoolattended.Clear();
            admissionclass.Text = "";
            proposeddateofentry.Text = "";
            attendedinterview.IsChecked = false;
            specialeducation.IsChecked = false;
            medicalexceptions.IsChecked = false;
            localauthority.IsChecked = false;
            ignorevalidation.IsChecked = false;
            registrationNo.Clear();
            generate_regno.IsEnabled = true;
        }

        //function inserts data into database
        private void insertstuddata()
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
            string insertQuery = "INSERT INTO studentdetails(imagepath,Student_Image,First_Name,Second_Name,Surname,Gender,Marital_Status,Phone_Number,Date_of_Birth,Age,Address,LGA,State,Nationality,Religion,Disability,Blood_Group,Father_Name,Mother_Name,Father_Occupation,Mother_Occupation,Father_Phone_Number,Mother_Phone_Number,Father_Address,Mother_Address,Child_Position_in_Family,Number_of_Siblings,Madien_Language,Last_School_Attended,Admission_Class,Date_of_Entry,Attended_Interview,Registration_Number,Special_Education,Medical_Exceptions,Local_Authority,Others)values('" + strName + "'," +
                "@dStudent_Image,@dFirst_Name,@dSecond_Name,@dSurname,@dGender,@dMarital_Status,@dPhone_Number,@dDate_of_Birth,@dAge,@dAddress,@dLGA,@dState,@Nationality," +
                "@dReligion,@dDisability,@dBlood_Group,@dFather_Name,@dMother_Name,@dFather_Occupation,@dMother_Occupation,@dFather_Phone_Number,@dMother_Phone_Number," +
                "@dFather_Address,@dMother_Address,@dChild_Position_in_Family,@dNumber_of_Siblings,@dMadien_Language,@dLast_School_Attended,@dAdmission_Class,@dDate_of_Entry," +
                "@dAttended_Interview,@dRegistration_Number,@dSpecial_Education,@dMedical_Exceptions,@dLocal_Authority,@dOthers)";

            // Create Sql Command                 
            insertCommand = new MySqlCommand(insertQuery, connection);

            //Add parameters to the command                                                                              
            insertCommand.Parameters.AddWithValue("@dStudent_Image", imgByteArr);
            insertCommand.Parameters.AddWithValue("@dFirst_Name", firstname.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dSecond_Name", secondname.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dSurname", surname.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dGender", gender.ToUpper());
            insertCommand.Parameters.AddWithValue("@dMarital_Status", maritalstatus.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dPhone_Number", phonenumber.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dDate_of_Birth", dateofbirth.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dAge", age.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dAddress", address.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dLGA", lga.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dState", state.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@Nationality", nationality.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dReligion", religion.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dDisability", disability.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dBlood_Group", bloodgroup.Text.ToUpper());

            //FAMILY INFORMATION
            insertCommand.Parameters.AddWithValue("@dFather_Name", fathername.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dMother_Name", mothername.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dFather_Occupation", fatheroccupation.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dMother_Occupation", motheroccupation.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dFather_Phone_Number", fatherphonenumber.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dMother_Phone_Number", motherphonenumber.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dFather_Address", fatheraddress.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dMother_Address", motheraddress.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dChild_Position_in_Family", childpositioninfamily.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dNumber_of_Siblings", numberofsiblings.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dMadien_Language", madienlanguage.Text.ToUpper());

            //ADMISSION INFORMATION
            insertCommand.Parameters.AddWithValue("@dLast_School_Attended", lastschoolattended.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dAdmission_Class", admissionclass.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dDate_of_Entry", proposeddateofentry.Text.ToUpper());
            insertCommand.Parameters.AddWithValue("@dAttended_Interview", attended_interview.ToUpper());
            insertCommand.Parameters.AddWithValue("@dRegistration_Number", registrationNo.Text.ToUpper());

            //OTHER INFORMATION
            insertCommand.Parameters.AddWithValue("@dSpecial_Education", special_education);
            insertCommand.Parameters.AddWithValue("@dMedical_Exceptions", medical_exceptions);
            insertCommand.Parameters.AddWithValue("@dLocal_Authority", local_authority);
            insertCommand.Parameters.AddWithValue("@dOthers", "");


            try
            {
                connection.Open();
                int result = insertCommand.ExecuteNonQuery();
                if (result > 0)
                {
                    notify.IsOpen = true;
                    showing.Text = "STUDENT INFORAMTION ACCEPTED";
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


        //Generate registration number 
        string regno;
        string aab = "S";  string aaa = "O1"; string bbb = "IO2"; string ccc = "CO1A"; string ddd = "SCO1C";
        private void Generate_regno_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection Sconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");

            MySqlCommand Scommand;

            string SQuery = "SELECT Id FROM studentdetails ORDER BY Id ASC";

            Scommand = new MySqlCommand(SQuery, Sconnection);
                        
            try
            {                
                Sconnection.Open();
                MySqlDataReader reader = Scommand.ExecuteReader();                
                while (reader.Read())
                {
                    regno = reader["Id"].ToString();                    
                }                
                registrationNo.Text = regno;                
                //if (reader.Read())
                //{
                //    registrationNo.Text = reader["id"].ToString();
                //}
                if (registrationNo.Text == "")
                {                    
                    registrationNo.Text = 1 + "00001";
                }
                if (registrationNo.Text.Length == 5)
                {
                    registrationNo.Text = regno + aab;
                    MessageBox.Show("REG NO. HAS BEEN CALLED");
                    generate_regno.IsEnabled = false;
                }
                if (registrationNo.Text.Length == 4)
                {
                    registrationNo.Text = regno + aaa;
                    MessageBox.Show("REG NO. HAS BEEN CALLED");
                    generate_regno.IsEnabled = false;
                }
                if (registrationNo.Text.Length == 3)
                {
                    registrationNo.Text = regno + bbb;
                    MessageBox.Show("REG NO. HAS BEEN CALLED");
                    generate_regno.IsEnabled = false;
                }
                if (registrationNo.Text.Length == 2)
                {
                    registrationNo.Text = regno + ccc;
                    MessageBox.Show("REG NO. HAS BEEN CALLED");
                    generate_regno.IsEnabled = false;
                }
                if (registrationNo.Text.Length == 1)
                {
                    registrationNo.Text = regno + ddd;
                    MessageBox.Show("REG NO. HAS BEEN CALLED");
                    generate_regno.IsEnabled = false;
                }
                else
                {                                        
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                Sconnection.Close();
            }            
        }        
        
    }
}
