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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace adolphus
{
    /// <summary>
    /// Interaction logic for study.xaml
    /// </summary>
    public partial class study : Page
    {
        int idds;
        public study()
        {
            InitializeComponent();
            idds = Settings1.Default.studentId;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //displays print out
            MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");

            MySqlCommand ccommand;

            var cQuery = "SELECT * FROM studentdetails WHERE Id = "+ idds + "";

            ccommand = new MySqlCommand(cQuery, cconnection);

            try
            {
                //for ecommandc
                cconnection.Open();
                MySqlDataReader readerc = ccommand.ExecuteReader();
                while (readerc.Read())
                {                    
                    firstname.Text = readerc["First_Name"].ToString();secondname.Text = readerc["Second_Name"].ToString();surname.Text = readerc["Surname"].ToString();
                    gender.Text = readerc["Gender"].ToString();maritalstatus.Text = readerc["Marital_Status"].ToString();phonenumber.Text = readerc["Phone_Number"].ToString();
                    dateofbirth.Text = readerc["Date_of_Birth"].ToString();age.Text = readerc["Age"].ToString();address.Text = readerc["Address"].ToString();
                    lga.Text = readerc["LGA"].ToString();state.Text = readerc["State"].ToString();nationality.Text = readerc["Nationality"].ToString();
                    religion.Text = readerc["Religion"].ToString();disability.Text = readerc["Disability"].ToString();bloodgroup.Text = readerc["Blood_Group"].ToString();
                    fathername.Text = readerc["Father_Name"].ToString();mothername.Text = readerc["Mother_Name"].ToString();
                    fatheroccupation.Text = readerc["Father_Occupation"].ToString();motheroccupation.Text = readerc["Mother_Occupation"].ToString();
                    fatherphonenumber.Text = readerc["Father_Phone_Number"].ToString();motherphonenumber.Text = readerc["Mother_Phone_Number"].ToString();
                    fatheraddress.Text = readerc["Father_Address"].ToString();motheraddress.Text = readerc["Mother_Address"].ToString();
                    childpositioninfamily.Text = readerc["Child_Position_in_Family"].ToString();numberofsiblings.Text = readerc["Number_of_Siblings"].ToString();
                    madienlanguage.Text = readerc["Madien_Language"].ToString();lastschoolattended.Text = readerc["Last_School_Attended"].ToString();
                    registrationNo.Text = readerc["Registration_Number"].ToString();admissionclass.Text = readerc["Admission_Class"].ToString();
                    proposeddateofentry.Text = readerc["Date_of_Entry"].ToString();

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
                    studimage.Source = bi;
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }       
    }
}
