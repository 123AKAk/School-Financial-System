using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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
    /// Interaction logic for total_system.xaml
    /// </summary>
    public partial class total_system : Page
    {
        List<CheckBox> allAutoCreatedObjects = new List<CheckBox>();
        List<CheckBox> allAutoCreatedObjectsforstaff = new List<CheckBox>();

        const int WIDTH = 80;
        const int HEIGHT = 30;

        int currentX = 0;
        const int STEP = 4;        
        bool isButtonssOnScreen = false;

        int currentXs = 0;
        const int STEPs = 4;        
        bool isButtonssOnScreens = false;
        int calAll = 0;
        public static int setvalue;
        
        MySqlConnection con;
        MySqlDataAdapter daa;
        DataSet dss;        

        public total_system()
        {
            InitializeComponent();
        }


        string itemstoDisplay;
        public string[] itemstoDisplaya;

        string itemstoDisplaystaff;        
        public string[] itemstoDisplaystaffa;

        //AUTO GENERATING COLUMNS      
        private void totalrevenuedatagrid1_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "SUM(Others)")
            {
                e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }
        }

        private void totalrevenuedatagrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "SUM(Others)")
            {
                e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }
        }
        private void feesdatagrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "SUM(Others)")
            {
                e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }
        }

        //close stud fees grid 
        private void closegrid(object sender, RoutedEventArgs e)
        {
            itemgrid.Visibility = Visibility.Collapsed;
        }
        private void closegridx(object sender, RoutedEventArgs e)
        {
            itemgridx.Visibility = Visibility.Collapsed;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {                     
            
        }

        //analyse all objects based on dates
        private void analyse_Click(object sender, RoutedEventArgs e)
        {
            studRevnue.Text = "";
            staffExp.Text = "";
            totalSchoolFees.Text = "";
            totalrevenue.Text = "";
            netprofit.Text = "";
            studRevnue.Clear();
            totalSchoolFees.Text = "";
            grossprofit.Text = "";
            directExp.Text = "";
            admistrativeExp.Text = "";

            if (fromDate.Text == "")
            {
                MessageBox.Show("Start Date Cannot Be Empty");

                stafftem.IsEnabled = false;
                studtem.IsEnabled = false;
                viewFeesdetails.IsEnabled = false;
                viewExpdetails.IsEnabled = false;
                addtotal.IsEnabled = false;
            }
            else if (toDate.Text == "")
            {
                MessageBox.Show("End Date Cannot Be Empty");

                stafftem.IsEnabled = false;
                studtem.IsEnabled = false;
                viewFeesdetails.IsEnabled = false;
                viewExpdetails.IsEnabled = false;
                addtotal.IsEnabled = false;
            }            
            else
            {
                DateTime d1 = DateTime.ParseExact(fromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime d2 = DateTime.ParseExact(toDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                if (d1 > d2)
                {
                    MessageBox.Show("Start Date cannot be greater than End Date", "Warning:", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    createObject();
                    createStaffObject();
                    autoShowBtn();
                    autoShowBtnStaff();
                    GetExpData();
                    GetFeesData();

                    Totalexpendituredirecta();
                    Totalexpenditureindirecta();

                    stafftem.IsEnabled = true;
                    studtem.IsEnabled = true;
                    viewFeesdetails.IsEnabled = true;
                    viewExpdetails.IsEnabled = true;
                    addtotal.IsEnabled = true;
                }
            }            
        }

        public void getStudentItem()
        {
            MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");            
            MySqlCommand ccommand;            

            var cQuery = "SELECT * FROM items WHERE Status IS NULL OR Status = ''";

            ccommand = new MySqlCommand(cQuery, cconnection);

            try
            {
                //for ecommandc
                cconnection.Open();
                MySqlDataReader readerc = ccommand.ExecuteReader();
                while (readerc.Read())
                {
                    //id += readerc["id"].ToString() + ",";                    
                    itemstoDisplay += readerc["Item_Name"].ToString() + ",";
                }
                itemstoDisplaya = itemstoDisplay.Split(',');

                string sql = "SELECT SUM(Others) FROM studitemdisplay";
                con = dtba.connectToDb();
                dss = new DataSet();
                DataTable dtt = new DataTable();
                daa = new MySqlDataAdapter(sql, con);
                daa.Fill(dss);
                totalrevenuedatagrid1.ItemsSource = dss.Tables[0].DefaultView;                        
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cconnection.Close();                
            }
        }

        public void getStaffItem()
        {
            MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");

            MySqlCommand ccommand;

            var cQuery = "SELECT * FROM staffitem WHERE Status IS NULL OR Status = ''";

            ccommand = new MySqlCommand(cQuery, cconnection);

            try
            {
                //for ecommandc
                cconnection.Open();
                MySqlDataReader readerc = ccommand.ExecuteReader();
                while (readerc.Read())
                {
                    itemstoDisplaystaff += readerc["Item_Name"].ToString() + ",";
                }

                itemstoDisplaystaffa = itemstoDisplaystaff.Split(',');

                string sql = "SELECT SUM(Others) FROM staffitemdisplay";
                con = dtba.connectToDb();
                dss = new DataSet();
                DataTable dtt = new DataTable();
                daa = new MySqlDataAdapter(sql, con);
                daa.Fill(dss);
                totalrevenuedatagrid.ItemsSource = dss.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
              
        //creating object for student
        private void createObject()
        {
            string eyoakak = "";

            MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");            
            MySqlCommand ccommand;
            var cQuery = "SELECT * FROM items WHERE Status IS NULL OR Status = ''";
            ccommand = new MySqlCommand(cQuery, cconnection);
            try
            {
                cconnection.Open();
                MySqlDataReader readerc = ccommand.ExecuteReader();
                while (readerc.Read())
                {
                    eyoakak += readerc["Item_Name"].ToString() + "," + readerc["Mode_of_Payement"].ToString() + ",";
                }
                if (eyoakak == "")
                {
                    MessageBox.Show("There are no Student Items to Display");
                    return;
                }
                else
                {
                    getStudentItem();

                    for (int i = 0; i <= itemstoDisplaya.Length - 2; i++)
                    {
                        CheckBox CheckBoxa = new CheckBox();
                        CheckBoxa.Foreground = Brushes.Black;
                        CheckBoxa.Name = itemstoDisplaya[i];
                        CheckBoxa.Background = Brushes.LightGray;
                        CheckBoxa.BorderThickness = new Thickness(0, 0, 0, 0);
                        CheckBoxa.Content = itemstoDisplaya[i];
                        CheckBoxa.FontSize = 13;
                        CheckBoxa.Width = WIDTH; CheckBoxa.Height = HEIGHT;
                        CheckBoxa.Cursor = Cursors.Hand;

                        CheckBoxa.Padding = new Thickness(0, 0, 0, 0);
                        allAutoCreatedObjects.Add(CheckBoxa);

                        CheckBoxa.Checked += new RoutedEventHandler(CheckBoxstudentChecked);
                        CheckBoxa.Unchecked += new RoutedEventHandler(CheckBoxstudentUnChecked);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {                
                cconnection.Close();
            }
        }

        string nama, sags;

        //checked eventHandler for student object
        private void CheckBoxstudentChecked(object sender, RoutedEventArgs e)
        {
            var chekbtn = sender as CheckBox;
            //MessageBox.Show("" + nama);
            //if (nama == "")
            //{
                nama += "SUM(" + chekbtn.Name + "),";
                sags += ""+chekbtn.Name+"+";
            //}
            //else
            //{
            //    nama += ",SUM(" + chekbtn.Name + ")";
            //}

            GetStudData(nama, sags);
        }
        
        //Unchecked eventHandler for student object
        private void CheckBoxstudentUnChecked(object sender, RoutedEventArgs e)
        {
            var chekbtn = sender as CheckBox;            

            string figh = "SUM("+chekbtn.Name+"),";
            string mehn = ""+chekbtn.Name+"+";

            int index1 = nama.IndexOf(figh);
            int index2 = sags.IndexOf(mehn);

            if (index1 != -1)
            {                
                string result1 = nama.Remove(index1, figh.Length);                
                nama = result1;

                if (index2 != -1)
                {                    
                    string result2 = sags.Remove(index2, mehn.Length);
                    sags = result2;
                }
                else
                {
                    MessageBox.Show("Error Encoded Decimal: " + mehn);
                }
            }            
            else
            {
                MessageBox.Show("Error Encoded Decimal: "+nama);
            }            
            GetStudData(nama, sags);
        }       

        string namb, guys;
        //creating object for staff
        private void createStaffObject()
        {
            string eyoakak = "";

            MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
            MySqlCommand ccommand;
            var cQuery = "SELECT * FROM staffitem WHERE Status IS NULL OR Status = ''";
            ccommand = new MySqlCommand(cQuery, cconnection);
            try
            {
                cconnection.Open();
                MySqlDataReader readerc = ccommand.ExecuteReader();
                while (readerc.Read())
                {
                    eyoakak += readerc["Item_Name"].ToString() + "," + readerc["Mode_of_Payement"].ToString() + ",";
                }
                if (eyoakak == "")
                {
                    MessageBox.Show("There are no Staff Items to Display");
                    return;
                }
                else
                {
                    getStaffItem();
                    for (int i = 0; i <= itemstoDisplaystaffa.Length - 2; i++)
                    {
                        CheckBox CheckBoxb = new CheckBox();
                        CheckBoxb.Foreground = Brushes.Black;
                        CheckBoxb.Name = itemstoDisplaystaffa[i];
                        CheckBoxb.Background = Brushes.LightGray;
                        CheckBoxb.BorderThickness = new Thickness(0, 0, 0, 0);
                        CheckBoxb.Content = itemstoDisplaystaffa[i];
                        CheckBoxb.FontSize = 13;
                        CheckBoxb.Width = WIDTH; CheckBoxb.Height = HEIGHT;
                        CheckBoxb.Cursor = Cursors.Hand;

                        CheckBoxb.Padding = new Thickness(0, 0, 0, 0);
                        allAutoCreatedObjectsforstaff.Add(CheckBoxb);

                        CheckBoxb.Checked += new RoutedEventHandler(CheckBoxbstudentChecked);
                        CheckBoxb.Unchecked += new RoutedEventHandler(CheckBoxbstudentUnChecked);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            finally
            {
                cconnection.Close();
            }
        }
        
        //Unchecked eventHandler for staff object
        private void CheckBoxbstudentChecked(object sender, RoutedEventArgs e)
        {
            var chekbtnb = sender as CheckBox;           
            namb += "SUM(" + chekbtnb.Name + "),";
            guys += "" + chekbtnb.Name + "+";           
            GetStaffData(namb, guys);
        }
        //checked eventHandler for staff object
        private void CheckBoxbstudentUnChecked(object sender, RoutedEventArgs e)
        {                    
            var chekbtnb = sender as CheckBox;

            string figha = "SUM(" + chekbtnb.Name + "),";
            string mehna = "" + chekbtnb.Name + "+";

            int index1a = namb.IndexOf(figha);
            int index2a = guys.IndexOf(mehna);

            //removing a particular string from the other
            if (index1a != -1)
            {
                string result1 = namb.Remove(index1a, figha.Length);
                namb = result1;

                if (index2a != -1)
                {
                    string result2 = guys.Remove(index2a, mehna.Length);
                    guys = result2;
                }
                else
                {
                    MessageBox.Show("Erorr Encoded Decimal: " + mehna);
                }
            }
            else
            {
                MessageBox.Show("Erorr Encoded Decimal: " + nama);
            }            
            GetStaffData(namb, guys);
        }
        

        //showing the objects for students
        private void autoShowBtn()
        {
            if (!isButtonssOnScreen)
            {
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
                return;
            }
        }

        //showing the objects for staff
        private void autoShowBtnStaff()
        {
            if (!isButtonssOnScreens)
            {
                foreach (var items in allAutoCreatedObjectsforstaff)
                {
                    Canvas.SetLeft(items, STEPs * currentXs);
                    currentXs += 25;
                    MainCanvasStaff.Width = currentXs * STEPs;
                    MainCanvasStaff.Children.Add(items);
                }
                isButtonssOnScreens = true;
            }
            else
            {
                return;
            }
        }

        //getting stud items into datagrid
        string[] richie;
        string madd, gozz;
        private void GetStudData(string namam, string sags)
        {         
            try
            {
                string sql = "SELECT "+ namam + " SUM(Others) FROM studitemdisplay WHERE Date BETWEEN '" + fromDate.Text + "' AND '" + toDate.Text + "'";                
                con = dtba.connectToDb();
                dss = new DataSet();
                DataTable dtt = new DataTable();
                daa = new MySqlDataAdapter(sql, con);
                daa.Fill(dss);
                totalrevenuedatagrid1.ItemsSource = dss.Tables[0].DefaultView;                

                var cQuery = "SELECT SUM("+sags+ "Others) bald FROM studitemdisplay WHERE Date BETWEEN '" + fromDate.Text + "' AND '" + toDate.Text + "'";
                MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
                MySqlCommand ccommand;
                ccommand = new MySqlCommand(cQuery, cconnection);                
                try
                {
                    cconnection.Open();
                    MySqlDataReader readerc = ccommand.ExecuteReader();
                    while (readerc.Read())
                    {
                        madd += readerc["bald"].ToString() + ",";

                        richie = madd.Split(',');
                    }
                    for(int i = 0; i<richie.Length-1; i++)
                    {
                        gozz = richie[i];
                    }

                    studRevnue.Text = "";
                    studRevnue.Text = gozz.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    cconnection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //getting staff items into datagrid  
        string[] richiea;
        string madda, gozza;        
        private void GetStaffData(string namab, string guysa)
        {                     
            try
            {
                string sql = "SELECT " + namab + " SUM(Others) FROM staffitemdisplay WHERE Date BETWEEN '" + fromDate.Text + "' AND '" + toDate.Text + "'";
                con = dtba.connectToDb();
                dss = new DataSet();
                DataTable dtt = new DataTable();
                daa = new MySqlDataAdapter(sql, con);
                daa.Fill(dss);
                totalrevenuedatagrid.ItemsSource = dss.Tables[0].DefaultView;

                var cQuery = "SELECT SUM("+guysa+ "Others) gras FROM staffitemdisplay WHERE Date BETWEEN '" + fromDate.Text + "' AND '" + toDate.Text + "'";
                MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
                MySqlCommand ccommand;
                ccommand = new MySqlCommand(cQuery, cconnection);
                try
                {
                    cconnection.Open();
                    MySqlDataReader readerc = ccommand.ExecuteReader();
                    while (readerc.Read())
                    {
                        madda += readerc["gras"].ToString() + ",";

                        richiea = madda.Split(',');
                    }
                    for (int i = 0; i < richiea.Length - 1; i++)
                    {
                        gozza = richiea[i];
                    }
                    staffExp.Text = "";
                    staffExp.Text = gozza.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    cconnection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //shows school fees
        private void viewFeesdetails_Click(object sender, RoutedEventArgs e)
        {
            if (fromDate.Text == "" && toDate.Text == "")
            {
                MessageBox.Show("Select Start and End Date First");
            }
            else
            {
                try
                {
                    string sqlQuery = "SELECT `Stud_Name`, `Stud_Class`, `Stud_Reg`, `Term`, `Session`, `Date`, `Paid_School_Fees`, `Fees_owed`, `Date_Of_Insertion`, `Notes` FROM fess WHERE Date BETWEEN '" + fromDate.Text + "' AND '" + toDate.Text + "'";
                    con = dtba.connectToDb();
                    dss = new DataSet();
                    DataTable dtt = new DataTable();
                    daa = new MySqlDataAdapter(sqlQuery, con);
                    daa.Fill(dss);
                    schoolfeesGrid.ItemsSource = dss.Tables[0].DefaultView;

                    getTotalFees.Text = totalSchoolFees.Text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                itemgrid.Visibility = Visibility.Visible;
            }            
        }
        //shows expenditures based on date 
        private void viewExpdetails_Click(object sender, RoutedEventArgs e)
        {
            if (fromDate.Text == "" && toDate.Text == "")
            {
                MessageBox.Show("Select Start and End Date First");
            }
            else
            {
                try
                {
                    string sqlQuery = "SELECT `Expenditure_Category`, `Expenditure_Item`, `Amount`, `Notes`, `Payement_Type`, `Exp_Date` FROM exp WHERE Exp_Date BETWEEN '" + fromDate.Text + "' AND '" + toDate.Text + "'";
                    con = dtba.connectToDb();
                    dss = new DataSet();
                    DataTable dtt = new DataTable();
                    daa = new MySqlDataAdapter(sqlQuery, con);
                    daa.Fill(dss);
                    schoolExpGrid.ItemsSource = dss.Tables[0].DefaultView;

                    getTotalExp.Text = totalexp.Text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                itemgridx.Visibility = Visibility.Visible;
            }
        }

        //EXPENDITURE PART
        //
        //################
        //getting expenditure items\
        string expitemstoDisplay, expItemsNames;       
        public string[] expitemstoDisplaya;       
        public void geExpItem()
        {
            MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
            MySqlCommand ccommand;
            var cQuery = "SELECT * FROM expenditure WHERE Status=''";
            ccommand = new MySqlCommand(cQuery, cconnection);
            try
            {                
                cconnection.Open();
                MySqlDataReader readerc = ccommand.ExecuteReader();
                while (readerc.Read())
                {
                    expitemstoDisplay += "Expenditure_Category='"+readerc["Expenditure_Category"].ToString() + "' AND ";
                }
                expitemstoDisplaya = expitemstoDisplay.Split(',');
                expItemsNames = string.Join(",", expitemstoDisplaya);                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cconnection.Close();
            }
        }       

        //getting expenditure data into datagrid         
        string diexpen;
        private void GetExpData()
        {
            geExpItem();
            try
            {               
                var cQuery = "SELECT  SUM(Amount) pexen FROM exp WHERE " + expItemsNames + " Amount IS NOT NULL OR Amount != '' AND Status='' AND Exp_Date BETWEEN '"+fromDate.Text+ "' AND '" + toDate.Text + "'";

                MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");

                MySqlCommand ccommand;

                ccommand = new MySqlCommand(cQuery, cconnection);

                cconnection.Open();
                MySqlDataReader readerc = ccommand.ExecuteReader();
                while (readerc.Read())
                {
                    diexpen = readerc["pexen"].ToString();                    
                }
                totalexp.Text = "₦" + diexpen;                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //add revenue
        private void addtotal_Click(object sender, RoutedEventArgs e)
        {           

            if (studRevnue.Text=="")
            {
                studRevnue.Text = "0";
            }
            if (totalSchoolFees.Text == "")
            {
                totalSchoolFees.Text = "0";
            }
            if(grossprofit.Text == "")
            {
                grossprofit.Text = "0";
            }
            if (directExp.Text == "")
            {
                directExp.Text = "0";
            }
            if (admistrativeExp.Text == "")
            {
                admistrativeExp.Text = "0";
            }
            if (bdiexpen == "")
            {
                bdiexpen = "0";
            }
            if (diexpen == "")
            {
                diexpen = "0";
            }
            if (adiexpen == "")
            {
                adiexpen = "0";
            }

            int addRevenue, grossp, netP;            
            addRevenue = Convert.ToInt32(studRevnue.Text) + Convert.ToInt32(diexpen.ToString());
            totalrevenue.Text = "₦" + addRevenue.ToString();

            grossp = Convert.ToInt32(addRevenue.ToString()) - Convert.ToInt32(adiexpen.ToString());
            grossprofit.Text = "₦" + grossp.ToString();

            netP = Convert.ToInt32(grossp.ToString()) - Convert.ToInt32(bdiexpen.ToString());
            netprofit.Text = "₦" + netP.ToString();
        }

        private void assestRegiter_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Sorry Not Available at the Moment");
        }

        //get school fees total
        string[] bounce;
        string abounce, bbounce;
        private void GetFeesData()
        {
            try
            {               
                var cQuery = "SELECT SUM(Paid_School_Fees) sald FROM fess WHERE Date BETWEEN '" + fromDate.Text + "' AND '" + toDate.Text + "'";
                MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
                MySqlCommand ccommand;
                ccommand = new MySqlCommand(cQuery, cconnection);
                try
                {
                    cconnection.Open();
                    MySqlDataReader readerc = ccommand.ExecuteReader();
                    while (readerc.Read())
                    {
                        abounce += readerc["sald"].ToString() + ",";

                        bounce = abounce.Split(',');
                    }
                    for (int i = 0; i < bounce.Length - 1; i++)
                    {
                        bbounce = bounce[i];
                    }

                    totalSchoolFees.Text = "";
                    totalSchoolFees.Text = "₦"+ bbounce.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    cconnection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void totalrevenue_TextChanged(object sender, TextChangedEventArgs e)
        {
            //int addRevenue, atotalrevenue1, atotalrevenue;
            //atotalrevenue = Convert.ToInt32(totalrevenue.Text);
            //atotalrevenue1 = Convert.ToInt32(totalrevenue1.Text);
            //addRevenue = atotalrevenue + atotalrevenue1;
            //totalprofit_loss.Text = addRevenue.ToString();
        }

        private void totalrevenue1_TextChanged(object sender, TextChangedEventArgs e)
        {
            //int addRevenue, atotalrevenue1, atotalrevenue;
            //atotalrevenue = Convert.ToInt32(totalrevenue.Text);
            //atotalrevenue1 = Convert.ToInt32(totalrevenue1.Text);
            //addRevenue = atotalrevenue + atotalrevenue1;
            //totalprofit_loss.Text = addRevenue.ToString();
        }


        //********
        //
        //total amount for TotalexpenditureIndirect
        string bexpitemstoDisplay, bexpItemsNames;
        public string[] bexpitemstoDisplaya;
        public void bgeExpItem()
        {
            MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
            MySqlCommand ccommand;
            var cQuery = "SELECT * FROM expenditure WHERE Type_of_Expenditure='Indirect_Expenditure' AND Status = ''";
            ccommand = new MySqlCommand(cQuery, cconnection);
            try
            {
                cconnection.Open();
                MySqlDataReader readerc = ccommand.ExecuteReader();
                while (readerc.Read())
                {
                    bexpitemstoDisplay += "Expenditure_Category='" + readerc["Expenditure_Category"].ToString() + "', ";
                }
                if (string.IsNullOrEmpty(bexpitemstoDisplay))
                {
                    return;
                }
                else
                {
                    bexpitemstoDisplaya = bexpitemstoDisplay.Split(',');
                    bexpItemsNames = string.Join(" OR ", bexpitemstoDisplaya);
                    bexpItemsNames = bexpItemsNames.Remove(bexpItemsNames.Length - 4, 4);                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "3");
            }
            finally
            {
                cconnection.Close();
            }
        }

        //getting total expenditure data into textbox         
        string bdiexpen;
        private void Totalexpenditureindirecta()
        {
            bgeExpItem();
            if (string.IsNullOrEmpty(bexpitemstoDisplay))
            {
                return;
            }
            else
            {
                try
                {
                    var cQuery = "SELECT  SUM(Amount) yexen FROM exp WHERE (" + bexpItemsNames + ") AND Amount != '' AND Status='' AND Exp_Date BETWEEN '" + fromDate.Text + "' AND '" + toDate.Text + "'";

                    MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");

                    MySqlCommand ccommand;

                    ccommand = new MySqlCommand(cQuery, cconnection);

                    cconnection.Open();
                    MySqlDataReader readerc = ccommand.ExecuteReader();
                    while (readerc.Read())
                    {
                        bdiexpen = readerc["yexen"].ToString();
                    }
                    admistrativeExp.Text = "₦" + bdiexpen;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }


        //**********
        //
        //total amount for Totalexpendituredirect
        string aexpitemstoDisplay, aexpItemsNames;
        public string[] aexpitemstoDisplaya;
        public void ageExpItem()
        {
            MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
            MySqlCommand ccommand;
            var cQuery = "SELECT * FROM expenditure WHERE Type_of_Expenditure='Direct_Expenditure' AND Status = ''";
            ccommand = new MySqlCommand(cQuery, cconnection);
            try
            {
                cconnection.Open();
                MySqlDataReader readerc = ccommand.ExecuteReader();
                while (readerc.Read())
                {
                    aexpitemstoDisplay += "Expenditure_Category='" + readerc["Expenditure_Category"].ToString() + "', ";
                }
                if (string.IsNullOrEmpty(aexpitemstoDisplay))
                {
                    return;
                }
                else
                {
                    aexpitemstoDisplaya = aexpitemstoDisplay.Split(',');
                    aexpItemsNames = string.Join(" OR ", aexpitemstoDisplaya);
                    aexpItemsNames = aexpItemsNames.Remove(aexpItemsNames.Length - 4, 4);                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "2");
            }
            finally
            {
                cconnection.Close();
            }
        }

        //getting total expenditure data into textbox         
        string adiexpen;
        private void Totalexpendituredirecta()
        {
            ageExpItem();
            if (string.IsNullOrEmpty(aexpitemstoDisplay))
            {
                return;
            }
            else
            {
                try
                {
                    var cQuery = "SELECT  SUM(Amount) sexen FROM exp WHERE (" + aexpItemsNames + ") AND Amount != '' AND Status='' AND Exp_Date BETWEEN '" + fromDate.Text + "' AND '" + toDate.Text + "'";

                    MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");

                    MySqlCommand ccommand;

                    ccommand = new MySqlCommand(cQuery, cconnection);

                    cconnection.Open();
                    MySqlDataReader readerc = ccommand.ExecuteReader();
                    while (readerc.Read())
                    {
                        adiexpen = readerc["sexen"].ToString();
                    }
                    directExp.Text = "₦" + adiexpen;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }
    }
}
