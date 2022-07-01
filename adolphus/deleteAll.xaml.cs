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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace adolphus
{
    /// <summary>
    /// Interaction logic for deleteAll.xaml
    /// </summary>
    public partial class deleteAll : Page
    {
        public deleteAll()
        {
            InitializeComponent();
        }

        MySqlConnection con;
        MySqlDataAdapter daa;
        DataSet dss;

        MySqlConnection connection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
        MySqlCommand insertCommand, insertCommanda;

        private void GetStudData()
        {
            //Gets data and inserts into datagrid
            try
            {
                string sql = "SELECT Id,Student_Image,First_Name,Second_Name,Surname,Student_Class,Registration_Number FROM studentdetails WHERE reme !='' ORDER BY Id DESC";
                con = dtba.connectToDb();
                dss = new DataSet();
                DataTable dtt = new DataTable();
                daa = new MySqlDataAdapter(sql, con);
                daa.Fill(dss);
                studdatagrid.ItemsSource = dss.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+"1", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GetStaffData()
        {
            //Gets data and inserts into datagrid
            try
            {
                string sql = "SELECT Id,Staff_Image,First_Name,Second_Name,Surname,Phone_Number FROM staffdetails WHERE reme !='' ORDER BY Id DESC";
                con = dtba.connectToDb();
                dss = new DataSet();
                DataTable dtt = new DataTable();
                daa = new MySqlDataAdapter(sql, con);
                daa.Fill(dss);
                staffdataGrid.ItemsSource = dss.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "2", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void GetStudItemData()
        {
            //Gets data and inserts into datagrid
            try
            {
                string sql = "SELECT * FROM items WHERE Status !='' ORDER BY Id DESC";
                con = dtba.connectToDb();
                dss = new DataSet();
                DataTable dtt = new DataTable();
                daa = new MySqlDataAdapter(sql, con);
                daa.Fill(dss);
                studentItems.ItemsSource = dss.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "3", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void GetStaffItemData()
        {
            //Gets data and inserts into datagrid
            try
            {
                string sql = "SELECT * FROM staffitem WHERE Status !='' ORDER BY Id DESC";
                con = dtba.connectToDb();
                dss = new DataSet();
                DataTable dtt = new DataTable();
                daa = new MySqlDataAdapter(sql, con);
                daa.Fill(dss);
                staffItems.ItemsSource = dss.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "4", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void GetExpData()
        {
            //Gets data and inserts into datagrid
            try
            {
                string sql = "SELECT * FROM expenditure WHERE Status !='' ORDER BY Id DESC";
                con = dtba.connectToDb();
                dss = new DataSet();
                DataTable dtt = new DataTable();
                daa = new MySqlDataAdapter(sql, con);
                daa.Fill(dss);
                expenditureGrid.ItemsSource = dss.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "5", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GetUserData()
        {
            //Gets data and inserts into datagrid
            try
            {
                string sql = "SELECT * FROM login WHERE Status !='' ORDER BY Id DESC";
                con = dtba.connectToDb();
                dss = new DataSet();
                DataTable dtt = new DataTable();
                daa = new MySqlDataAdapter(sql, con);
                daa.Fill(dss);
                userGrid.ItemsSource = dss.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "6", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            GetStudData();
            GetStaffData();
            GetStudItemData();
            GetStaffItemData();
            GetExpData();
            GetUserData();
        }

        //students starts here
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
        }

        private void studdatagrid_AutoGeneratingColumnuser(object sender, DataGridAutoGeneratingColumnEventArgs e)
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
            else if (e.Column.Header.ToString() == "Password")
            {
                e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }
            else if (e.Column.Header.ToString() == "Status")
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

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid dg = (DataGrid)sender;
                DataRowView rowSelected = dg.SelectedItem as DataRowView;
                if (rowSelected != null)
                {
                    idds.Text = rowSelected[0].ToString();                    
                    //x_data.Text = rowSelected[6].ToString();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "1", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void deleteStud(object sender, RoutedEventArgs e)
        {
            if (idds.Text != "")
            {
                string sQuery = "DELETE FROM `studentdetails` WHERE Id='" + idds.Text + "'";
                insertCommand = new MySqlCommand(sQuery, connection);
                try
                {
                    connection.Open();
                    int result = insertCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        notify.IsOpen = true;
                        showing.Text = "STUDENT DELETED";
                        GetStudItemData();
                    }
                    else
                    {
                        notify.IsOpen = true;
                        showing.Text = "Error!!! UNABLE TO DELETE STUDENT";
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
                showing.Text = "SELECT EXPENDITURE CATEGORY FROM THE EXPENDITURE DATA-GRID BEFORE PROCEEDING";
            }
        }

        private void restoreStud(object sender, RoutedEventArgs e)
        {
            if (idds.Text != "")
            {
                //create insert query                                            
                string insertQuery = "UPDATE studentdetails SET reme=@dreme WHERE Id='" + idds.Text + "'"; ;
                insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@dreme", null);
                try
                {
                    connection.Open();
                    int result = insertCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        notify.IsOpen = true;
                        showing.Text = "STUDENT RESTORED";
                        GetStudData();                        
                    }
                    else
                    {
                        notify.IsOpen = true;
                        showing.Text = "Error!!! UNABLE TO RESTORE STUDENT";
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning); }
                finally { connection.Close(); }
            }
            else
            {
                notify.IsOpen = true;
                showing.Text = "SELECT STUDENT FROM THE DATA-GRID BEFORE PROCEEDING ";
            }
        }
        //students ends here
        //
        //$$$$$$$$$$$$$$$$$

        //^^^^^^^^^^^^^^
        //
        //staff starts here
        private void staffdatagrid_AutoGeneratingColumn_1(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Id")
            {
                e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }
            if (e.Column.Header.ToString() == "Staff_Image")
            {
                e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }
        }
        private void DataGrid2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid dg = (DataGrid)sender;
                DataRowView rowSelected = dg.SelectedItem as DataRowView;
                if (rowSelected != null)
                {
                    idds.Text = rowSelected[0].ToString();
                    //x_data.Text = rowSelected[6].ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "2", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void deletea(object sender, RoutedEventArgs e)
        {
            if (idds.Text != "")
            {
                string sQuery = "DELETE FROM `staffdetails` WHERE Id='" + idds.Text + "'";
                insertCommand = new MySqlCommand(sQuery, connection);
                try
                {
                    connection.Open();
                    int result = insertCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        notify.IsOpen = true;
                        showing.Text = "STAFF DELETED";
                        GetStaffData();
                    }
                    else
                    {
                        notify.IsOpen = true;
                        showing.Text = "Error!!! UNABLE TO DELETE STAFF";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "2", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                notify.IsOpen = true;
                showing.Text = "SELECT STAFF FROM THE DATA-GRID BEFORE PROCEEDING";
            }
        }
        private void restorea(object sender, RoutedEventArgs e)
        {
            if (idds.Text != "")
            {
                //create insert query                                            
                string insertQuery = "UPDATE staffdetails SET reme=@dreme WHERE Id='" + idds.Text + "'"; ;
                insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@dreme", null);
                try
                {
                    connection.Open();
                    int result = insertCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        notify.IsOpen = true;
                        showing.Text = "STAFF RESTORED";
                        GetStaffData();                        
                    }
                    else
                    {
                        notify.IsOpen = true;
                        showing.Text = "Error!!! UNABLE TO RESTORE STAFF";
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning); }
                finally { connection.Close(); }
            }
            else
            {
                notify.IsOpen = true;
                showing.Text = "SELECT STAFF FROM THE DATA-GRID BEFORE PROCEEDING ";
            }
        }
        //staff ends here
        //
        //$$$$$$$$$$$$$$$

        //^^^^^^^^^^^^^^
        //
        //stud items starts here
        private void studItemsdatagrid_AutoGeneratingColumn_1(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Id")
            {
                e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }
            if (e.Column.Header.ToString() == "Mode_of_Payement")
            {
                e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }
            if (e.Column.Header.ToString() == "post")
            {
                e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }
            if (e.Column.Header.ToString() == "Status")
            {
                e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }
        }
        private void DataGrid3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid dg = (DataGrid)sender;
                DataRowView rowSelected = dg.SelectedItem as DataRowView;
                if (rowSelected != null)
                {
                    idds.Text = rowSelected[0].ToString();
                    x_data.Text = rowSelected[1].ToString();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "3", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void deleteitemsa(object sender, RoutedEventArgs e)
        {
            string modea;

            if (idds.Text != "")
            {
                modea = "Payement_Type_of_"+x_data.Text;

                string sQuery = "DELETE FROM `items` WHERE Id='" + idds.Text + "'";

                string aQuery = "ALTER TABLE studitemdisplay DROP COLUMN " + x_data.Text + ", DROP COLUMN " + modea + "";

                insertCommand = new MySqlCommand(sQuery, connection);
                insertCommanda = new MySqlCommand(aQuery, connection);

                try
                {
                    connection.Open();
                    int result = insertCommand.ExecuteNonQuery();                    
                    if (result > 0)
                    {
                        int resulta = insertCommanda.ExecuteNonQuery();
                        if (resulta > 0)
                        {
                            notify.IsOpen = true;
                            showing.Text = "STUDENT ITEM DELETED";                            
                        }
                        else
                        {
                            notify.IsOpen = true;
                            showing.Text = "STUDENT ITEM DELETED";
                        }
                        GetStudItemData();
                    }
                    else
                    {
                        notify.IsOpen = true;
                        showing.Text = "Serious Error!!! UNABLE TO DELETE STUDENT ITEM";
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + "3", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                notify.IsOpen = true;
                showing.Text = "SELECT STAFF ITEM FROM THE DATA-GRID BEFORE PROCEEDING";
            }
        }
        private void restoritemsea(object sender, RoutedEventArgs e)
        {
            if (idds.Text != "")
            {
                //create insert query                                            
                string insertQuery = "UPDATE items SET Status=@dStatus WHERE Id='" + idds.Text + "'"; ;
                insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@dStatus", null);
                try
                {
                    connection.Open();
                    int result = insertCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        notify.IsOpen = true;
                        showing.Text = "STUDENT ITEM RESTORED";
                        GetStudItemData();
                    }
                    else
                    {
                        notify.IsOpen = true;
                        showing.Text = "Error!!! UNABLE TO RESTORE STUDENT ITEM";
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning); }
                finally { connection.Close(); }
            }
            else
            {
                notify.IsOpen = true;
                showing.Text = "SELECT STUDENT ITEM FROM THE ITEMS DATA-GRID BEFORE PROCEEDING ";
            }
        }
        //stud items ends here
        //
        //$$$$$$$$$$$$$$$

        //^^^^^^^^^^^^^^
        //
        //staff items starts here
        private void staffItemsdatagrid_AutoGeneratingColumn_1(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "Id")
            {
                e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }
            if (e.Column.Header.ToString() == "Mode_of_Payement")
            {
                e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }
            if (e.Column.Header.ToString() == "post")
            {
                e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }
            if (e.Column.Header.ToString() == "Status")
            {
                e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }
        }
        private void DataGrid4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid dg = (DataGrid)sender;
                DataRowView rowSelected = dg.SelectedItem as DataRowView;
                if (rowSelected != null)
                {
                    idds.Text = rowSelected[0].ToString();
                    x_data.Text = rowSelected[1].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "4", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void deleteitemsb(object sender, RoutedEventArgs e)
        {
            string modeb;

            if (idds.Text != "")
            {
                modeb = "Payement_Type_of_" + x_data.Text;

                string sQuery = "DELETE FROM `staffitem` WHERE Id='" + idds.Text + "'";

                string aQuery = "ALTER TABLE staffitemdisplay DROP COLUMN " + x_data.Text + ", DROP COLUMN " + modeb + "";                

                insertCommand = new MySqlCommand(sQuery, connection);
                insertCommanda = new MySqlCommand(aQuery, connection);

                try
                {
                    connection.Open();
                    int result = insertCommand.ExecuteNonQuery();
                    
                    if (result > 0)
                    {
                        int resulta = insertCommanda.ExecuteNonQuery();
                        if (resulta > 0)
                        {
                            notify.IsOpen = true;
                            showing.Text = "STAFF ITEM DELETED";
                            GetStaffItemData();
                        }
                        else
                        {
                            notify.IsOpen = true;
                            showing.Text = "STAFF ITEM DELETED";
                        }
                        GetStaffItemData();
                    }
                    else
                    {
                        notify.IsOpen = true;
                        showing.Text = "Serious Error!!! UNABLE TO DELETE STAFF ITEM";
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + "4", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                notify.IsOpen = true;
                showing.Text = "SELECT STAFF ITEM FROM THE DATA-GRID BEFORE PROCEEDING";
            }            
        }
        private void restoreitemsb(object sender, RoutedEventArgs e)
        {
            if (idds.Text != "")
            {
                //create insert query                                            
                string insertQuery = "UPDATE staffitem SET Status=@dStatus WHERE Id='" + idds.Text + "'"; ;
                insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@dStatus", null);
                try
                {
                    connection.Open();
                    int result = insertCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        notify.IsOpen = true;
                        showing.Text = "STAFF ITEM RESTORED";
                        GetStaffItemData();
                    }
                    else
                    {
                        notify.IsOpen = true;
                        showing.Text = "Error!!! UNABLE TO RESTORE STAFF ITEM";
                    }
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning); }
                finally { connection.Close(); }
            }
            else
            {
                notify.IsOpen = true;
                showing.Text = "SELECT STAFF ITEM FROM THE DATA-GRID BEFORE PROCEEDING ";
            }
        }
        //staff items ends here
        //
        //$$$$$$$$$$$$$$$

        //^^^^^^^^^^^^^^
        //
        //expenditure items starts here
        private void expenDatagrid_AutoGeneratingColumn_1(object sender, DataGridAutoGeneratingColumnEventArgs e)
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
            if (e.Column.Header.ToString() == "Status")
            {
                e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }
        }
        private void DataGrid5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid dg = (DataGrid)sender;
                DataRowView rowSelected = dg.SelectedItem as DataRowView;
                if (rowSelected != null)
                {
                    idds.Text = rowSelected[0].ToString();
                    x_data.Text = rowSelected[2].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "5", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void deleteexp(object sender, RoutedEventArgs e)
        {
            if (x_data.Text != "")
            {
                string sQuery = "DELETE FROM `expenditure` WHERE Expenditure_Category='" + x_data.Text + "'";
                string aQuery = "DELETE FROM `exp` WHERE Expenditure_Category='" + x_data.Text + "'";

                insertCommand = new MySqlCommand(sQuery, connection);
                insertCommanda = new MySqlCommand(aQuery, connection);
                try
                {
                    connection.Open();
                    int result = insertCommand.ExecuteNonQuery();
                    int resulta = insertCommanda.ExecuteNonQuery();
                    if (result > 0)
                    {
                        if (resulta > 0)
                        {
                            notify.IsOpen = true;
                            showing.Text = "EXPENDITURE DELETED";
                            GetExpData();
                        }
                        else
                        {
                            notify.IsOpen = true;
                            showing.Text = "Error!!! UNABLE TO DELETE EXPENDITURE";
                        }
                    }
                    else
                    {
                        notify.IsOpen = true;
                        showing.Text = "Serious Error!!! UNABLE TO DELETE EXPENDITURE";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "5", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                notify.IsOpen = true;
                showing.Text = "SELECT EXPENDITURE FROM THE EXPENDITURE DATA-GRID BEFORE PROCEEDING";
            }
        }
        

        private void restoreexp(object sender, RoutedEventArgs e)
        {
            if (idds.Text != "")
            {
                string eQuery = "UPDATE `expenditure` SET Status=@Status WHERE Expenditure_Category='" + x_data.Text + "'";
                insertCommand = new MySqlCommand(eQuery, connection);
                insertCommand.Parameters.AddWithValue("@Status", null);
                try
                {
                    connection.Open();
                    int result = insertCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        string aQuery = "UPDATE `exp` SET Status=@Status WHERE Expenditure_Category='" + x_data.Text + "'";
                        insertCommanda = new MySqlCommand(aQuery, connection);
                        insertCommanda.Parameters.AddWithValue("@Status", null);
                        try
                        {
                            int resulta = insertCommanda.ExecuteNonQuery();
                            if (resulta > 0)
                            {
                                notify.IsOpen = true;
                                showing.Text = "EXPENDITURE CATEGORY AND ITEMS RESTORED";
                                GetExpData();
                            }
                            else
                            {
                                notify.IsOpen = true;
                                showing.Text = "ERROR!!! UNABLE TO RESTORE EXPENDITURE";
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Error" + "5", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                }
                catch (Exception ex)
                {MessageBox.Show(ex.Message, "Error" + "5", MessageBoxButton.OK, MessageBoxImage.Warning);}
                finally { connection.Close(); }
            }
            else
            {
                notify.IsOpen = true;
                showing.Text = "SELECT EXPENDITURE FROM THE DATA-GRID BEFORE PROCEEDING ";
            }
        }

        //expenditure items ends here
        //
        //$$$$$$$$$$$$$$$

        private void deleteuser(object sender, RoutedEventArgs e)
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
                        showing.Text = "Staff Deleted Successfully";
                        GetUserData();
                    }
                    else
                    {
                        notify.IsOpen = true;
                        showing.Text = "Error!!! Unable to Delete Staff";
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

        private void restoreuser(object sender, RoutedEventArgs e)
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
                        showing.Text = "Restored Successfully";
                        GetUserData();
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
                showing.Text = "Select staff from the Data Grid before proceeding";
            }
        }

    }
}
