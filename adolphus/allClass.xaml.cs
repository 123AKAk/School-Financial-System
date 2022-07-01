using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace adolphus
{
    /// <summary
    /// Interaction logic for allClass.xaml
    /// </summary>
    public partial class allClass : Page
    {
        public allClass()
        {
            InitializeComponent();           
        }

        MySqlConnection con;
        MySqlDataAdapter daa;
        DataSet dss;
        MySqlCommandBuilder scmbl;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetSettings();
            GetStudData();
            getStudentItem();

            getClasses();
            getTerm();
            getSession();
        }

        private void GetStudData()
        {
            //Gets data and inserts into datagrid
            try
            {
                string sql = "SELECT * FROM fess ORDER BY Id DESC";
                con = dtba.connectToDb();
                dss = new DataSet();
                DataTable dtt = new DataTable();
                daa = new MySqlDataAdapter(sql, con);
                daa.Fill(dss);
                studdatagrid.ItemsSource = dss.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        string aterm, asession, ass3class, ass2class, ass1class, ajss3class, ajss2class, ajss1class;
        private void GetSettings()
        {
            MySqlConnection connection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
            MySqlCommand ccommand;

            var cQuery = "SELECT Term,Session,ss3class,ss2class,ss1class,jss3class,jss2class,jss1class FROM settings";

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
                    ass3class = readerc["ss3class"].ToString();
                    ass2class = readerc["ss2class"].ToString();
                    ass1class = readerc["ss1class"].ToString();
                    ajss3class = readerc["jss3class"].ToString();
                    ajss2class = readerc["jss2class"].ToString();
                    ajss1class = readerc["jss1class"].ToString();
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

        //AUTO GENERATING COLUMNS
        private void studdatagrid_AutoGeneratingColumn_1(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Id")
            {
                e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }
        }
        private void studdatagrid_AutoGeneratingColumn_2(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Id")
            {
                e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }
            if (e.Column.Header.ToString() == "Term")
            {
                e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }
            if (e.Column.Header.ToString() == "Notes")
            {
                e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }
            if (e.Column.Header.ToString() == "Session")
            {
                e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }
            if (e.Column.Header.ToString() == "Date")
            {
                e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }
        }

        string classes, done;
        public string[] classesa, donea;
        public void getClasses()
        {
            MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
            MySqlCommand ccommand;
            var cQuery = "SELECT Stud_Class FROM fess";
            ccommand = new MySqlCommand(cQuery, cconnection);
            try
            {
                cconnection.Open();
                MySqlDataReader readerc = ccommand.ExecuteReader();
                while (readerc.Read()) { classes += readerc["Stud_Class"].ToString() + ","; }
                classesa = classes.Split(',');
                HashSet<string> hset = new HashSet<string>(classesa);
                foreach (var n in hset) { done += n + ","; donea = done.Split(','); }
                for (int i = 0; i <= donea.Length - 3; i++)
                {                    
                    classe.Items.Add(donea[i].ToUpper());
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            finally { cconnection.Close(); }
        }
        string classesz, donez;
        public string[] classesaz, doneaz;
        public void getTerm()
        {
            MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
            MySqlCommand ccommand;
            var cQuery = "SELECT Term FROM fess";
            ccommand = new MySqlCommand(cQuery, cconnection);
            try
            {
                cconnection.Open();
                MySqlDataReader readerc = ccommand.ExecuteReader();
                while (readerc.Read()) { classesz += readerc["Term"].ToString() + ","; }
                classesaz = classesz.Split(',');
                HashSet<string> hset = new HashSet<string>(classesaz);
                foreach (var n in hset) { donez += n + ","; doneaz = donez.Split(','); }
                for (int i = 0; i <= doneaz.Length - 3; i++)
                {                
                    terma.Items.Add(doneaz[i].ToUpper());
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            finally { cconnection.Close(); }
        }

        string classesy, doney;
        public string[] classesay, doneay;
        public void getSession()
        {
            MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
            MySqlCommand ccommand;
            var cQuery = "SELECT Session FROM fess";
            ccommand = new MySqlCommand(cQuery, cconnection);
            try
            {
                cconnection.Open();
                MySqlDataReader readerc = ccommand.ExecuteReader();
                while (readerc.Read()) { classesy += readerc["Session"].ToString() + ","; }
                classesay = classesy.Split(',');
                HashSet<string> hset = new HashSet<string>(classesay);
                foreach (var n in hset) { doney += n + ","; doneay = doney.Split(','); }
                for (int i = 0; i <= doneay.Length - 3; i++)
                {                 
                    sessiona.Items.Add(doneay[i].ToUpper());
                }
            }
            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
            finally { cconnection.Close(); }
        }


        private void feesSelected(object sender, RoutedEventArgs e)
        {
            search.Visibility = Visibility.Visible;
            search2.Visibility = Visibility.Hidden;
        }

        private void itemSelected(object sender, RoutedEventArgs e)
        {
            search2.Visibility = Visibility.Visible;
            search.Visibility = Visibility.Hidden;
        }


        int solid = 0;
        //search for student data
        private void Search_Click(object sender, RoutedEventArgs e)
        {
            //fees
            if (studsearch.Text != "")
            {
                if (typesearch.Text != "")
                {
                    if (typesearch.Text == "Student Name")
                    {
                        try
                        {
                            string sqlQuery = "SELECT `Stud_Name`, `Stud_Class`, `School_Fees`, `Paid_School_Fees`, `Fees_owed`, `Date_Of_Insertion`, `Term`, `Stud_Reg`, `Session`, `Notes` FROM fess WHERE Stud_Name ='" + studsearch.Text + "'";
                            con = dtba.connectToDb();
                            dss = new DataSet();
                            DataTable dtt = new DataTable();
                            daa = new MySqlDataAdapter(sqlQuery, con);
                            daa.Fill(dss);
                            studdatagrid.ItemsSource = dss.Tables[0].DefaultView;

                            solid = 1;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else if (typesearch.Text == "Student Class")
                    {
                        if (terma.Text == "")
                        {
                            notify.IsOpen = true;
                            showing.Text = "SELECT THE TERM";
                        }
                        else if (sessiona.Text == "")
                        {
                            notify.IsOpen = true;
                            showing.Text = "SELECT THE SESSION";
                        }
                        else
                        {
                            try
                            {
                                string sqlQuery = "SELECT `Stud_Name`, `Stud_Class`, `School_Fees`, `Paid_School_Fees`, `Fees_owed`, `Date_Of_Insertion`, `Date`, `Term`, `Stud_Reg`, `Session`, `Notes` FROM fess WHERE Stud_Class ='" + classe.Text + "' AND Term ='" + terma.Text + "' AND Session ='" + sessiona.Text + "'";
                                con = dtba.connectToDb();
                                dss = new DataSet();
                                DataTable dtt = new DataTable();
                                daa = new MySqlDataAdapter(sqlQuery, con);
                                daa.Fill(dss);
                                studdatagrid.ItemsSource = dss.Tables[0].DefaultView;

                                solid = 1;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                    }
                    else if (typesearch.Text == "Student Reg No")
                    {
                        try
                        {
                            string sqlQuery = "SELECT `Stud_Name`, `Stud_Class`, `School_Fees`, `Paid_School_Fees`, `Fees_owed`, `Date_Of_Insertion`, `Date`, `Term`, `Stud_Reg`, `Session`, `Notes` FROM fess WHERE Stud_Reg ='" + studsearch.Text + "'";
                            con = dtba.connectToDb();
                            dss = new DataSet();
                            DataTable dtt = new DataTable();
                            daa = new MySqlDataAdapter(sqlQuery, con);
                            daa.Fill(dss);
                            studdatagrid.ItemsSource = dss.Tables[0].DefaultView;

                            solid = 1;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else if (typesearch.Text == "Date of Payement")
                    {
                        try
                        {
                            string sqlQuery = "SELECT `Stud_Name`, `Stud_Class`, `School_Fees`, `Paid_School_Fees`, `Fees_owed`, `Date_Of_Insertion`, `Date`, `Term`, `Stud_Reg`, `Session`, `Notes` FROM fess WHERE Date ='" + datepick.Text + "'";
                            con = dtba.connectToDb();
                            dss = new DataSet();
                            DataTable dtt = new DataTable();
                            daa = new MySqlDataAdapter(sqlQuery, con);
                            daa.Fill(dss);
                            studdatagrid.ItemsSource = dss.Tables[0].DefaultView;

                            solid = 1;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
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

        //search2 search
        string itemstoDisplay;
        public void getStudentItem()
        {
            MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
            var cQuery = "SELECT * FROM items WHERE Status = ''";
            MySqlCommand ccommand = new MySqlCommand(cQuery, cconnection);
            try
            {
                cconnection.Open();
                MySqlDataReader readerc = ccommand.ExecuteReader();
                while (readerc.Read())
                {
                    itemstoDisplay += readerc["Item_Name"].ToString() + ",";
                }
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
        private void Search2_Click(object sender, RoutedEventArgs e)
        {
            if (studsearch.Text != "")
            {
                if (typesearch.Text != "")
                {
                    if (typesearch.Text == "Student Name")
                    {
                        try
                        {
                            string sqlQuery = "SELECT `Stud_Name`, " + itemstoDisplay + " `Stud_Class`, `Stud_Reg`, `Term`, `Date`, `Session`, `Notes` FROM studitemdisplay WHERE Stud_Name ='" + studsearch.Text + "'";
                            con = dtba.connectToDb();
                            dss = new DataSet();
                            DataTable dtt = new DataTable();
                            daa = new MySqlDataAdapter(sqlQuery, con);
                            daa.Fill(dss);
                            studdatagrid.ItemsSource = dss.Tables[0].DefaultView;

                            solid = 1;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else if (typesearch.Text == "Student Class")
                    {
                        if (terma.Text == "")
                        {
                            notify.IsOpen = true;
                            showing.Text = "SELECT THE TERM";
                        }
                        else if (sessiona.Text == "")
                        {
                            notify.IsOpen = true;
                            showing.Text = "SELECT THE SESSION";
                        }
                        else
                        {
                            try
                            {
                                string sqlQuery = "SELECT `Stud_Name`, " + itemstoDisplay + " `Stud_Class`, `Stud_Reg`, `Term`, `Date`, `Session`, `Notes` FROM studitemdisplay WHERE Stud_Class ='" + classe.Text + "' AND Term ='" + terma.Text + "' AND Session ='" + sessiona.Text + "'";
                                con = dtba.connectToDb();
                                dss = new DataSet();
                                DataTable dtt = new DataTable();
                                daa = new MySqlDataAdapter(sqlQuery, con);
                                daa.Fill(dss);
                                studdatagrid.ItemsSource = dss.Tables[0].DefaultView;

                                solid = 1;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }
                        }
                    }
                    else if (typesearch.Text == "Student Reg No")
                    {
                        try
                        {
                            string sqlQuery = "SELECT `Stud_Name`, " + itemstoDisplay + " `Stud_Class`, `Stud_Reg`, `Term`, `Date`, `Session`, `Notes` FROM studitemdisplay WHERE Stud_Reg ='" + studsearch.Text + "'";
                            con = dtba.connectToDb();
                            dss = new DataSet();
                            DataTable dtt = new DataTable();
                            daa = new MySqlDataAdapter(sqlQuery, con);
                            daa.Fill(dss);
                            studdatagrid.ItemsSource = dss.Tables[0].DefaultView;

                            solid = 1;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else if (typesearch.Text == "Date of Payement")
                    {
                        try
                        {
                            string sqlQuery = "SELECT `Stud_Name`, " + itemstoDisplay + " `Stud_Class`, `Stud_Reg`, `Term`, `Date`, `Session`, `Notes` FROM studitemdisplay WHERE Date ='" + datepick.Text + "'";
                            con = dtba.connectToDb();
                            dss = new DataSet();
                            DataTable dtt = new DataTable();
                            daa = new MySqlDataAdapter(sqlQuery, con);
                            daa.Fill(dss);
                            studdatagrid.ItemsSource = dss.Tables[0].DefaultView;

                            solid = 1;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
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


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            getClasses();
            getTerm();
            getSession();
        }

        private void Viewall(object sender, RoutedEventArgs e)
        {
            otherdatagrid.Visibility = Visibility.Hidden;
            solid = 0;
            GetStudData();
        }
        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            //Date of Payement
            studsearch.Text = "Date of Payement";
            datepick.Visibility = Visibility.Visible;
            studsearch.Visibility = Visibility.Hidden;
        }

        private void ComboBoxItem_Selected_1(object sender, RoutedEventArgs e)
        {
            //Student Class
            studsearch.Text = "Student Class";
            datepick.Visibility = Visibility.Hidden;
            studsearch.Visibility = Visibility.Visible;
            classe.IsEnabled = true;
            terma.IsEnabled = true;
            sessiona.IsEnabled = true;
            studsearch.IsEnabled = false;
        }

        private void ComboBoxItem_Selected_2(object sender, RoutedEventArgs e)
        {
            //Student Name 
            studsearch.Text = "";
            classe.IsEnabled = false;
            terma.IsEnabled = false;
            sessiona.IsEnabled = false;
            studsearch.IsEnabled = true;
            studsearch.Visibility = Visibility.Visible;
            datepick.Visibility = Visibility.Hidden;
        }

        private void ComboBoxItem_Selected_3(object sender, RoutedEventArgs e)
        {
            //Student Reg      
            studsearch.Text = "";
            classe.IsEnabled = false;
            terma.IsEnabled = false;
            sessiona.IsEnabled = false;
            studsearch.IsEnabled = true;
            studsearch.Visibility = Visibility.Visible;
            datepick.Visibility = Visibility.Hidden;
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

        //exporting data grid to excel

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
                    System.IO.StreamWriter file1 = new System.IO.StreamWriter(@"schoolFeesDisplay.xls");
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

        //
        //#############
        //
        private void classSelect3(object sender, RoutedEventArgs e)
        {
            try
            {
                string sql = "SELECT * FROM fess WHERE Term='" + aterm + "' AND Session='" + asession + "' AND Stud_Class='SS3' AND Paid_School_Fees='" + ass3class + "' ORDER BY Id DESC";
                con = dtba.connectToDb();
                dss = new DataSet();
                DataTable dtt = new DataTable();
                daa = new MySqlDataAdapter(sql, con);
                daa.Fill(dss);
                paidallschoolfees.ItemsSource = dss.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void classSelect2(object sender, RoutedEventArgs e)
        {
            try
            {
                string sql = "SELECT * FROM fess WHERE Term='" + aterm + "' AND Session='" + asession + "' AND Stud_Class='SS2' AND Paid_School_Fees='" + ass2class + "' ORDER BY Id DESC";
                con = dtba.connectToDb();
                dss = new DataSet();
                DataTable dtt = new DataTable();
                daa = new MySqlDataAdapter(sql, con);
                daa.Fill(dss);
                paidallschoolfees.ItemsSource = dss.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void classSelect1(object sender, RoutedEventArgs e)
        {
            try
            {
                string sql = "SELECT * FROM fess WHERE Term='" + aterm + "' AND Session='" + asession + "' AND Stud_Class='SS1' AND Paid_School_Fees='" + ass1class + "' ORDER BY Id DESC";
                con = dtba.connectToDb();
                dss = new DataSet();
                DataTable dtt = new DataTable();
                daa = new MySqlDataAdapter(sql, con);
                daa.Fill(dss);
                paidallschoolfees.ItemsSource = dss.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void classSelectJ3(object sender, RoutedEventArgs e)
        {
            try
            {
                string sql = "SELECT * FROM fess WHERE Term='" + aterm + "' AND Session='" + asession + "' AND Stud_Class='JSS3' AND Paid_School_Fees='" + ajss3class + "' ORDER BY Id DESC";
                con = dtba.connectToDb();
                dss = new DataSet();
                DataTable dtt = new DataTable();
                daa = new MySqlDataAdapter(sql, con);
                daa.Fill(dss);
                paidallschoolfees.ItemsSource = dss.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void classSelectJ2(object sender, RoutedEventArgs e)
        {
            try
            {
                string sql = "SELECT * FROM fess WHERE Term='" + aterm + "' AND Session='" + asession + "' AND Stud_Class='JSS2' AND Paid_School_Fees='" + ajss2class + "' ORDER BY Id DESC";
                con = dtba.connectToDb();
                dss = new DataSet();
                DataTable dtt = new DataTable();
                daa = new MySqlDataAdapter(sql, con);
                daa.Fill(dss);
                paidallschoolfees.ItemsSource = dss.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void classSelectJ1(object sender, RoutedEventArgs e)
        {
            try
            {
                string sql = "SELECT * FROM fess WHERE Term='" + aterm + "' AND Session='" + asession + "' AND Stud_Class='JSS1' AND Paid_School_Fees='" + ajss1class + "' ORDER BY Id DESC";
                con = dtba.connectToDb();
                dss = new DataSet();
                DataTable dtt = new DataTable();
                daa = new MySqlDataAdapter(sql, con);
                daa.Fill(dss);
                paidallschoolfees.ItemsSource = dss.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //####
        private void aclassSelect3(object sender, RoutedEventArgs e)
        {
            try
            {
                string sql = "SELECT * FROM fess WHERE Term='" + aterm + "' AND Session='" + asession + "' AND Stud_Class='SS3' AND Paid_School_Fees<'" + ass3class + "' ORDER BY Id DESC";
                con = dtba.connectToDb();
                dss = new DataSet();
                DataTable dtt = new DataTable();
                daa = new MySqlDataAdapter(sql, con);
                daa.Fill(dss);
                allschoolfees.ItemsSource = dss.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void aclassSelect2(object sender, RoutedEventArgs e)
        {
            try
            {
                string sql = "SELECT * FROM fess WHERE Term='" + aterm + "' AND Session='" + asession + "' AND Stud_Class='SS2' AND Paid_School_Fees<'" + ass2class + "' ORDER BY Id DESC";
                con = dtba.connectToDb();
                dss = new DataSet();
                DataTable dtt = new DataTable();
                daa = new MySqlDataAdapter(sql, con);
                daa.Fill(dss);
                allschoolfees.ItemsSource = dss.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void aclassSelect1(object sender, RoutedEventArgs e)
        {
            try
            {
                string sql = "SELECT * FROM fess WHERE Term='" + aterm + "' AND Session='" + asession + "' AND Stud_Class='SS1' AND Paid_School_Fees<'" + ass1class + "' ORDER BY Id DESC";
                con = dtba.connectToDb();
                dss = new DataSet();
                DataTable dtt = new DataTable();
                daa = new MySqlDataAdapter(sql, con);
                daa.Fill(dss);
                allschoolfees.ItemsSource = dss.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void aclassSelectJ3(object sender, RoutedEventArgs e)
        {
            try
            {
                string sql = "SELECT * FROM fess WHERE Term='" + aterm + "' AND Session='" + asession + "' AND Stud_Class='JSS3' AND Paid_School_Fees<'" + ajss3class + "' ORDER BY Id DESC";
                con = dtba.connectToDb();
                dss = new DataSet();
                DataTable dtt = new DataTable();
                daa = new MySqlDataAdapter(sql, con);
                daa.Fill(dss);
                allschoolfees.ItemsSource = dss.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void aclassSelectJ2(object sender, RoutedEventArgs e)
        {
            try
            {
                string sql = "SELECT * FROM fess WHERE Term='" + aterm + "' AND Session='" + asession + "' AND Stud_Class='JSS2' AND Paid_School_Fees<'" + ajss2class + "' ORDER BY Id DESC";
                con = dtba.connectToDb();
                dss = new DataSet();
                DataTable dtt = new DataTable();
                daa = new MySqlDataAdapter(sql, con);
                daa.Fill(dss);
                allschoolfees.ItemsSource = dss.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void aclassSelectJ1(object sender, RoutedEventArgs e)
        {
            try
            {
                string sql = "SELECT * FROM fess WHERE Term='" + aterm + "' AND Session='" + asession + "' AND Stud_Class='JSS1' AND Paid_School_Fees<'" + ajss1class + "' ORDER BY Id DESC";
                con = dtba.connectToDb();
                dss = new DataSet();
                DataTable dtt = new DataTable();
                daa = new MySqlDataAdapter(sql, con);
                daa.Fill(dss);
                allschoolfees.ItemsSource = dss.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


    }
}
