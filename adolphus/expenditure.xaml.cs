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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace adolphus
{
    /// <summary>
    /// Interaction logic for expenditure.xaml
    /// </summary>
    public partial class expenditure : Page
    {
        public expenditure()
        {
            InitializeComponent();            
        }

        MySqlConnection con;
        MySqlDataAdapter daa;
        DataSet dss;

        MySqlDataAdapter daaz;
        DataSet dssz;

        MySqlDataAdapter daay;
        DataSet dssy;

        //used for updating the data tables
        MySqlCommandBuilder scmbl;

        //MySql connection
        MySqlConnection connection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
        MySqlCommand insertCommand;
        MySqlCommand insertCommanda;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.GetExpenditureCapitalData();
            this.GetExpenditureDirectData();
            this.GetExpenditureIndirectData();
            this.getcategorydata();

            TotalexpenditureCapitala();
            Totalexpendituredirecta();
            Totalexpenditureindirecta();
        }

        private void GetExpenditureCapitalData()
        {
            //Gets data and inserts into datagrid
            try
            {
                string sql = "SELECT Expenditure_Category,Type_of_Expenditure FROM expenditure WHERE Type_of_Expenditure='Capital_Expenditure' AND Status = ''";
                con = dtba.connectToDb();
                dss = new DataSet();
                DataTable dtt = new DataTable();
                daa = new MySqlDataAdapter(sql, con);
                daa.Fill(dss);
                expenditureCapitaldatagrid.ItemsSource = dss.Tables[0].DefaultView;                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void GetExpenditureDirectData()
        {
            //Gets data and inserts into datagrid
            try
            {
                string sql = "SELECT Expenditure_Category,Type_of_Expenditure FROM expenditure WHERE Type_of_Expenditure='Direct_Expenditure' AND Status = ''";
                con = dtba.connectToDb();
                dssz = new DataSet();
                DataTable dtt = new DataTable();
                daaz = new MySqlDataAdapter(sql, con);
                daaz.Fill(dssz);
                expendituredirectdatagrid.ItemsSource = dssz.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void GetExpenditureIndirectData()
        {
            //Gets data and inserts into datagrid
            try
            {
                string sql = "SELECT Expenditure_Category,Type_of_Expenditure FROM expenditure WHERE Type_of_Expenditure='Indirect_Expenditure' AND Status = ''";
                con = dtba.connectToDb();
                dssz = new DataSet();
                DataTable dtt = new DataTable();
                daaz = new MySqlDataAdapter(sql, con);
                daaz.Fill(dssz);
                expenditureindirectdatagrid.ItemsSource = dssz.Tables[0].DefaultView;
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
            if (e.Column.Header.ToString() == "Type_of_Expenditure")
            {
                e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }
        }

        private void getItems()
        {
            if (getText.Text != "")
            {
                //Gets data and inserts into datagrid
                try
                {                   
                    string sql = "SELECT Id,Expenditure_Item,Amount,Notes,Payement_Type,Exp_Date FROM exp WHERE Expenditure_Category='" + getText.Text+ "' AND Status = '' ";
                    con = dtba.connectToDb();
                    dssy = new DataSet();
                    DataTable dtt = new DataTable();
                    daay = new MySqlDataAdapter(sql, con);
                    daay.Fill(dssy);
                    dataGrid.ItemsSource = dssy.Tables[0].DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                notify.IsOpen = true;
                showing.Text = "SELECT EXPENDITURE CATEGORY FROM THE DATA-GRID BEFORE PROCEEDING";
            }
        }

        //$$$
        //$$$
        //onstartup get all categories: code starts here
        string cate_gory;
        public string[] cate_gorya;
        private void getcategorydata()
        {
            MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");

            MySqlCommand ccommand;

            var cQuery = "SELECT * FROM expenditure WHERE Status = ''";

            ccommand = new MySqlCommand(cQuery, cconnection);            
            try
            {
                //for ecommandc
                cconnection.Open();
                MySqlDataReader readerc = ccommand.ExecuteReader();
                while (readerc.Read())
                {
                    cate_gory += readerc["Expenditure_Category"].ToString() + ",";
                }

                cate_gorya = cate_gory.Split(',');
                //MessageBox.Show("ODB: - " + cate_gory);
                for(int i =0; i< cate_gorya.Length-2; i++)
                {
                    expenditurecategory.Items.Remove(cate_gorya[i]);
                    expenditurecategory.Items.Add(cate_gorya[i]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //onstartup get all categories: code ends here
        //$$$
        //$$$

        private void Viewall_Click(object sender, RoutedEventArgs e)
        {
            GetExpenditureCapitalData();
            GetExpenditureDirectData();
            GetExpenditureIndirectData();
        }

        private void add_cat_Click(object sender, RoutedEventArgs e)
        {
            expenditure_cat_name.Text = "";
            expendituretype.Text = "";

            grida.Visibility = Visibility.Visible;
            gridb.Visibility = Visibility.Collapsed;

            DoubleAnimation badass = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                AutoReverse = false
            };
            grida.BeginAnimation(OpacityProperty, badass);
            grida.Opacity = 100;

            addexpenditureCategory.IsEnabled = true;
            updateexpenditureCategory.IsEnabled = false;
            deleteexpenditureCategory.IsEnabled = false;

        }

        private void add_exp_Click(object sender, RoutedEventArgs e)
        {
            expenditurename.Text = "";
            valuesofexpenditure.Text = "";
            notes.Text = "";
            date.Text = "";
            payementType.Text = "";

            grida.Visibility = Visibility.Collapsed;
            gridb.Visibility = Visibility.Visible;
          
            getcategorydata();

            DoubleAnimation badass = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                AutoReverse = false
            };
            gridb.BeginAnimation(OpacityProperty, badass);
            gridb.Opacity = 100;            
        }
              
        //*
        //*
        //*
        //all the functionalities for the add expenditure category starts here 
        private void Addexpenditurecategory_Click(object sender, RoutedEventArgs e)
        {
            if (expenditure_cat_name.Text == "")
            {
                notify.IsOpen = true;
                showing.Text = "EXPENDITURE CATEGORY NAME FEILD IS EMPTY";
            }
            else if (expendituretype.Text == "")
            {
                notify.IsOpen = true;
                showing.Text = "SELECT EXPENDITURE TYPE";
            }
            else
            {
                string aQuery = "SELECT * FROM expenditure WHERE Expenditure_Category='"+expenditure_cat_name.Text+ "'";

                insertCommand = new MySqlCommand(aQuery, connection);

                //parameter
                insertCommand.Parameters.AddWithValue("@dExpenditure_Category", expenditurename.Text);
                try
                {
                    connection.Open();
                    MySqlDataReader reader = insertCommand.ExecuteReader();
                    //SqlDataReader readerc = ecommandc.ExecuteReader();
                    //if (readerc.Read())
                    if (reader.Read())
                    {
                        MessageBox.Show("SORRY CAN'T ACCEPT DUPLICATE EXPENDITURE CATEGORY", "Error", MessageBoxButton.OK, MessageBoxImage.Error);                        
                    }
                    else
                    {
                        connection.Close();

                        //create insert query
                        string insertQuery = "INSERT INTO expenditure(Type_of_Expenditure,Expenditure_Category)values(@dType_of_Expenditure,@dExpenditure_Category)";

                        // Create Sql Command                 
                        insertCommand = new MySqlCommand(insertQuery, connection);

                        //Add parameters to the command
                        insertCommand.Parameters.AddWithValue("@dType_of_Expenditure", expendituretype.Text);
                        insertCommand.Parameters.AddWithValue("@dExpenditure_Category", expenditure_cat_name.Text);

                        try
                        {
                            connection.Open();
                            int result = insertCommand.ExecuteNonQuery();
                            if (result > 0)
                            {
                                notify.IsOpen = true;
                                showing.Text = "EXPENDITURE CATEGORY ACCEPTED";

                                expendituretype.Text = "";
                                expenditure_cat_name.Clear();

                                GetExpenditureCapitalData();
                                GetExpenditureDirectData();
                                GetExpenditureIndirectData();
                                getcategorydata();
                            }
                            else
                            {
                                notify.IsOpen = true;
                                showing.Text = "ERROR!!! UNABLE TO ENTER CATEGORY";
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Errorss", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    notify.IsOpen = true;
                    showing.Text = ex.Message;
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void Updateexpenditurecategory_Click(object sender, RoutedEventArgs e)
        {
            string eyoakak = "";

            //#######$
            //Checking if there is an expenditure category in the exp table
            MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
            MySqlCommand ccommand;
            var cQuery = "SELECT Expenditure_Category FROM exp WHERE Expenditure_Category='" + getText.Text + "' AND Status = ''";
            ccommand = new MySqlCommand(cQuery, cconnection);
            cconnection.Open();
            MySqlDataReader readerc = ccommand.ExecuteReader();
            while (readerc.Read())
            {
                eyoakak += readerc["Expenditure_Category"].ToString();
            }
            //#######$

            string eQuery = "UPDATE `expenditure` SET Expenditure_Category=@Expenditure_Category,Type_of_Expenditure=@Type_of_Expenditure WHERE Expenditure_Category='" + getText.Text+"'";

            insertCommand = new MySqlCommand(eQuery, connection);
   
            insertCommand.Parameters.AddWithValue("@Expenditure_Category", expenditure_cat_name.Text);
            insertCommand.Parameters.AddWithValue("@Type_of_Expenditure", expendituretype.Text);

            try
            {
                connection.Open();
                int result = insertCommand.ExecuteNonQuery();              
                if (result > 0)
                {
                    GetExpenditureCapitalData();
                    GetExpenditureDirectData();
                    GetExpenditureIndirectData();

                    string aQuery = "UPDATE `exp` SET Expenditure_Category=@Expenditure_Category WHERE Expenditure_Category='" + getText.Text + "'";

                    insertCommanda = new MySqlCommand(aQuery, connection);

                    //Add parameters to the command                                                                             
                    insertCommanda.Parameters.AddWithValue("@Expenditure_Category", expenditure_cat_name.Text);

                    try
                    {
                        if (eyoakak == "")
                        {
                            notify.IsOpen = true;
                            showing.Text = "EXPENDITURE CATEGORY AND ITEMS UPDATED";
                            return;
                        }
                        else
                        {
                            int resulta = insertCommanda.ExecuteNonQuery();
                            if (resulta > 0)
                            {
                                notify.IsOpen = true;
                                showing.Text = "EXPENDITURE CATEGORY AND ITEMS UPDATED";

                                GetExpenditureCapitalData();
                                GetExpenditureDirectData();
                                GetExpenditureIndirectData();
                                getcategorydata();
                            }
                            else
                            {
                                notify.IsOpen = true;
                                showing.Text = "ERROR!!! UNABLE TO UPDATE ITEM CATEGORY";

                                GetExpenditureCapitalData();
                                GetExpenditureDirectData();
                                GetExpenditureIndirectData();
                                getcategorydata();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    notify.IsOpen = true;
                    showing.Text = "ERROR!!! UNABLE TO UPDATE CATEGORY";
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

        private void Deleteexpenditurecategory_Click(object sender, RoutedEventArgs e)
        {
            string eyoakak = "";

            //#######$
            //Checking if there is an expenditure category in the exp table
            MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
            MySqlCommand ccommand;
            var cQuery = "SELECT Expenditure_Category FROM exp WHERE Expenditure_Category='" + getText.Text + "' AND Status = ''";
            ccommand = new MySqlCommand(cQuery, cconnection);          
            cconnection.Open();
            MySqlDataReader readerc = ccommand.ExecuteReader();
            while (readerc.Read())
            {
                eyoakak += readerc["Expenditure_Category"].ToString();
            }
            //#######$

            string eQuery = "UPDATE `expenditure` SET Status=@Status WHERE Expenditure_Category='" + getText.Text + "'";
            insertCommand = new MySqlCommand(eQuery, connection);
            insertCommand.Parameters.AddWithValue("@Status", "Removed");            
            try
            {
                connection.Open();
                int result = insertCommand.ExecuteNonQuery();
                if (result > 0)
                {
                    GetExpenditureCapitalData();
                    GetExpenditureDirectData();
                    GetExpenditureIndirectData();

                    string aQuery = "UPDATE `exp` SET Status=@Status WHERE Expenditure_Category='" + getText.Text + "'";                
                    insertCommanda = new MySqlCommand(aQuery, connection);
                    insertCommanda.Parameters.AddWithValue("@Status", "Removed");
                    try
                    {
                        if (eyoakak == "")
                        {
                            notify.IsOpen = true;
                            showing.Text = "EXPENDITURE CATEGORY DELETED";

                            expenditure_cat_name.Text = "";
                            expendituretype.Text = "";
                            return;
                        }
                        else
                        {
                            int resulta = insertCommanda.ExecuteNonQuery();
                            if (resulta > 0)
                            {
                                notify.IsOpen = true;
                                showing.Text = "EXPENDITURE CATEGORY AND ITEMS DELETED";

                                expenditure_cat_name.Text = "";
                                expendituretype.Text = "";

                                GetExpenditureCapitalData();
                                GetExpenditureDirectData();
                                GetExpenditureIndirectData();
                                getcategorydata();
                            }
                            else
                            {
                                notify.IsOpen = true;
                                showing.Text = "ERROR!!! UNABLE TO DELETE EXPENDITURE CATEGORY";

                                GetExpenditureCapitalData();
                                GetExpenditureDirectData();
                                GetExpenditureIndirectData();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    notify.IsOpen = true;
                    showing.Text = "ERROR!!! UNABLE TO UPDATE CATEGORY";
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

                TotalexpenditureCapitala();
                Totalexpendituredirecta();
                Totalexpenditureindirecta();
            }
        }
        //all the functionalities for the add expenditure category énds here 
        //*
        //*
        //*


        //Capital Expenditure datagrid selection
        private void DataGridCAPITAL_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            expendituredirectdatagrid.UnselectAllCells();
            expendituredirectdatagrid.Items.Refresh();

            expenditureindirectdatagrid.UnselectAllCells();
            expenditureindirectdatagrid.Items.Refresh();

            try
            {
                DataGrid dg = (DataGrid)sender;
                DataRowView rowSelected = dg.SelectedItem as DataRowView;
                if (rowSelected != null)
                {
                    //idds.Text = rowSelected[0].ToString();
                    getText.Text = rowSelected[0].ToString();
                    expenditure_cat_name.Text = rowSelected[0].ToString();
                    expendituretype.Text = rowSelected[1].ToString();

                    getItems();

                    grida.Visibility = Visibility.Visible;
                    gridb.Visibility = Visibility.Collapsed;


                    DoubleAnimation badass = new DoubleAnimation
                    {
                        From = 0,
                        To = 1,
                        Duration = new Duration(TimeSpan.FromSeconds(1)),
                        AutoReverse = false
                    };
                    grida.BeginAnimation(OpacityProperty, badass);
                    grida.Opacity = 100;
                    
                    updateexpenditure.IsEnabled = true;
                    deleteexpenditure.IsEnabled = true;

                    addexpenditureCategory.IsEnabled = false;
                    updateexpenditureCategory.IsEnabled = true;
                    deleteexpenditureCategory.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //Direct Expenditure datagrid selection
        private void DataGridDirect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            expenditureCapitaldatagrid.UnselectAllCells();
            expenditureCapitaldatagrid.Items.Refresh();

            expenditureindirectdatagrid.UnselectAllCells();
            expenditureindirectdatagrid.Items.Refresh();
            try
            {
                DataGrid dg = (DataGrid)sender;
                DataRowView rowSelected = dg.SelectedItem as DataRowView;
                if (rowSelected != null)
                {
                    //idds.Text = rowSelected[0].ToString();
                    getText.Text = rowSelected[0].ToString();
                    expenditure_cat_name.Text = rowSelected[0].ToString();
                    expendituretype.Text = rowSelected[1].ToString();

                    getItems();

                    grida.Visibility = Visibility.Visible;
                    gridb.Visibility = Visibility.Collapsed;

                    DoubleAnimation badass = new DoubleAnimation
                    {
                        From = 0,
                        To = 1,
                        Duration = new Duration(TimeSpan.FromSeconds(1)),
                        AutoReverse = false
                    };
                    grida.BeginAnimation(OpacityProperty, badass);
                    grida.Opacity = 100;

                    updateexpenditure.IsEnabled = true;
                    deleteexpenditure.IsEnabled = true;

                    addexpenditureCategory.IsEnabled = false;
                    updateexpenditureCategory.IsEnabled = true;
                    deleteexpenditureCategory.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //Indirect Expenditure datagrid selection
        private void DataGridIndirect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            expenditureCapitaldatagrid.UnselectAllCells();
            expenditureCapitaldatagrid.Items.Refresh();

            expendituredirectdatagrid.UnselectAllCells();
            expendituredirectdatagrid.Items.Refresh();

            try
            {
                DataGrid dg = (DataGrid)sender;
                DataRowView rowSelected = dg.SelectedItem as DataRowView;
                if (rowSelected != null)
                {
                    //idds.Text = rowSelected[0].ToString();
                    getText.Text = rowSelected[0].ToString();
                    expenditure_cat_name.Text = rowSelected[0].ToString();
                    expendituretype.Text = rowSelected[1].ToString();

                    getItems();

                    grida.Visibility = Visibility.Visible;
                    gridb.Visibility = Visibility.Collapsed;

                    DoubleAnimation badass = new DoubleAnimation
                    {
                        From = 0,
                        To = 1,
                        Duration = new Duration(TimeSpan.FromSeconds(1)),
                        AutoReverse = false
                    };
                    grida.BeginAnimation(OpacityProperty, badass);
                    grida.Opacity = 100;

                    updateexpenditure.IsEnabled = true;
                    deleteexpenditure.IsEnabled = true;

                    addexpenditureCategory.IsEnabled = false;
                    updateexpenditureCategory.IsEnabled = true;
                    deleteexpenditureCategory.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void valuesofexpenditure_TextChanged(object sender, TextChangedEventArgs e)
        {
            int parsedValue;
            if (valuesofexpenditure.Text == "")
            {

            }
            else if (!int.TryParse(valuesofexpenditure.Text, out parsedValue))
            {
                MessageBox.Show("Expenditure Values Feild is a number only Feild", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
        }

        //*
        //*
        //all the functionalities for the add expenditure item starts here 
        private void Addexpenditure_Click(object sender, RoutedEventArgs e)
        {            
            if (expenditurename.Text == "")
            {
                notify.IsOpen = true;
                showing.Text = "EXPENDITURE NAME FEILD IS EMPTY";
            }
            else if (expenditurecategory.Text == "")
            {
                notify.IsOpen = true;
                showing.Text = "SELECT EXPENDITURE CATEGORY";
            }
            else if (valuesofexpenditure.Text == "")
            {
                notify.IsOpen = true;
                showing.Text = "INPUT VALUES OF EXPENDITURE";
            }
            else if (payementType.Text == "")
            {
                notify.IsOpen = true;
                showing.Text = "CHOOSE DATE OF EXPENDITURE";
                payementType.Text = "_";
            }
            else if (date.Text.Equals(""))
            {
                notify.IsOpen = true;
                showing.Text = "CHOOSE DATE OF EXPENDITURE";                
            }           
            else
            {
                //create insert query
                string insertQuery = "INSERT INTO exp(Expenditure_Category,Expenditure_Item,Amount,Notes,Payement_Type,Exp_Date)values(@dExpenditure_Category,@dExpenditure_Item,@dAmount,@dNotes,@dPayement_Type,@dExp_Date)";

                // Create Sql Command                 
                insertCommand = new MySqlCommand(insertQuery, connection);

                //Add parameters to the command                                                                                                          
                insertCommand.Parameters.AddWithValue("@dExpenditure_Category", expenditurecategory.Text);
                insertCommand.Parameters.AddWithValue("@dExpenditure_Item", expenditurename.Text);
                insertCommand.Parameters.AddWithValue("@dAmount", valuesofexpenditure.Text);
                insertCommand.Parameters.AddWithValue("@dNotes", notes.Text);
                insertCommand.Parameters.AddWithValue("@dPayement_Type", payementType.Text);
                insertCommand.Parameters.AddWithValue("@dExp_Date", date.Text);

                try
                {
                    connection.Open();
                    int result = insertCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        notify.IsOpen = true;
                        showing.Text = "EXPENDITURE ITEM ACCEPTED";

                        expenditurename.Clear();                        
                        valuesofexpenditure.Clear();
                        expenditurecategory.Text = "";
                        notes.Text = "";
                        payementType.Text = "";
                        date.Text = "";

                        GetExpenditureCapitalData();
                        GetExpenditureDirectData();
                        GetExpenditureIndirectData();

                        TotalexpenditureCapitala();
                        Totalexpendituredirecta();
                        Totalexpenditureindirecta();
                    }
                    else
                    {
                        notify.IsOpen = true;
                        showing.Text = "ERROR!!! UNABLE TO ENTER EXPENDITURE ITEM";
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

        private void Updateexpenditure_Click(object sender, RoutedEventArgs e)
        {
            if(getText.Text != "No Category Selected")
            {
                try
                {
                    scmbl = new MySqlCommandBuilder(daay);
                    daay.Update(dssy);
                    MessageBox.Show("EXPENDITURE ITEM UPDATED", "Updated", MessageBoxButton.OK, MessageBoxImage.Information);

                    TotalexpenditureCapitala();
                    Totalexpendituredirecta();
                    Totalexpenditureindirecta();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {

            }
        }

        private void Deleteexpenditure_Click(object sender, RoutedEventArgs e)
        {
            if (getText.Text != "" & getItem.Text != "")
            {               
                string sQuery = "DELETE FROM `exp` WHERE Id='"+ getItem.Text+"'";

                insertCommand = new MySqlCommand(sQuery, connection);
                try
                {
                    connection.Open();
                    int result = insertCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        notify.IsOpen = true;
                        showing.Text = "EXPENDITURE ITEM DELETED";

                        getItems();

                        TotalexpenditureCapitala();
                        Totalexpendituredirecta();
                        Totalexpenditureindirecta();
                    }
                    else
                    {
                        notify.IsOpen = true;
                        showing.Text = "Error!!! UNABLE TO DELETE EXPENDITURE ITEM";
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
            else
            {
                notify.IsOpen = true;
                showing.Text = "SELECT EXPENDITURE ITEM FROM THE DATA-GRID BEFORE PROCEEDING";
            }
        }

        //all the functionalities for the add expenditure item énds here 
        //*
        //*

        

        private void closegrid(object sender, RoutedEventArgs e)
        {
            itemgrid.Visibility = Visibility.Collapsed;
        }

        private void openGrid(object sender, RoutedEventArgs e)
        {
            itemgrid.Visibility = Visibility.Visible;
        }

        private void itemGrid_Selection(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid dg = (DataGrid)sender;
                DataRowView rowSelected = dg.SelectedItem as DataRowView;
                if (rowSelected != null)
                {          
                    getItem.Text = rowSelected[0].ToString();                                      
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        //EXPENDITURE PART
        //
        //################
        //getting expenditure items\
        string expitemstoDisplay, expItemsNames;
        public string[] expitemstoDisplaya;
        public void geExpItema()
        {
            MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
            MySqlCommand ccommand;
            var cQuery = "SELECT * FROM expenditure WHERE Type_of_Expenditure='Capital_Expenditure'";
            ccommand = new MySqlCommand(cQuery, cconnection);
            try
            {
                cconnection.Open();
                MySqlDataReader readerc = ccommand.ExecuteReader();
                while (readerc.Read())
                {
                    expitemstoDisplay += "Expenditure_Category='" + readerc["Expenditure_Category"].ToString() + "', ";
                }
                if (string.IsNullOrEmpty(expitemstoDisplay))
                {
                    return;
                }
                else
                {
                    expitemstoDisplaya = expitemstoDisplay.Split(',');
                    expItemsNames = string.Join(" OR ", expitemstoDisplaya);
                    expItemsNames = expItemsNames.Remove(expItemsNames.Length - 4, 4);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+"1");
            }
            finally
            {
                cconnection.Close();
            }
        }
        
        //getting total expenditure data into textbox         
        string diexpen;
        private void TotalexpenditureCapitala()
        {            
            geExpItema();
            if (string.IsNullOrEmpty(expitemstoDisplay))
            {
                return;
            }
            else
            {
                try
                {
                    var cQuery = "SELECT  SUM(Amount) AS aexen FROM exp WHERE (" + expItemsNames + ") AND Amount != '' AND Status=''";
                    //var cQuery = "SELECT  SUM(Amount) aexen FROM exp WHERE Expenditure_Category='Purchase_Of_Power_Source' AND  Amount != ''";

                    MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");

                    MySqlCommand ccommand;

                    ccommand = new MySqlCommand(cQuery, cconnection);

                    cconnection.Open();
                    MySqlDataReader readerc = ccommand.ExecuteReader();
                    while (readerc.Read())
                    {
                        diexpen = readerc["aexen"].ToString();
                    }
                    TotalexpenditureCapital.Text = "" + diexpen;
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
                    var cQuery = "SELECT  SUM(Amount) sexen FROM exp WHERE (" + aexpItemsNames + ") AND Amount != '' AND Status=''";

                    MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");

                    MySqlCommand ccommand;

                    ccommand = new MySqlCommand(cQuery, cconnection);

                    cconnection.Open();
                    MySqlDataReader readerc = ccommand.ExecuteReader();
                    while (readerc.Read())
                    {
                        adiexpen = readerc["sexen"].ToString();
                    }
                    Totalexpendituredirect.Text = "" + adiexpen;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
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
                if(string.IsNullOrEmpty(bexpitemstoDisplay))
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
                    var cQuery = "SELECT  SUM(Amount) yexen FROM exp WHERE (" + bexpItemsNames + ") AND Amount != '' AND Status=''";

                    MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");

                    MySqlCommand ccommand;

                    ccommand = new MySqlCommand(cQuery, cconnection);

                    cconnection.Open();
                    MySqlDataReader readerc = ccommand.ExecuteReader();
                    while (readerc.Read())
                    {
                        bdiexpen = readerc["yexen"].ToString();
                    }
                    Totalexpenditureindirect.Text = "" + bdiexpen;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }        
    }
}
