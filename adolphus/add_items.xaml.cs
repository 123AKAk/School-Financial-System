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
    /// Interaction logic for add_items.xaml
    /// </summary>
    public partial class add_items : Page
    {
        public add_items()
        {
            InitializeComponent();
            this.GetItems();
            this.GetStaffItems();
        }        

        MySqlConnection con;
        MySqlDataAdapter daa;
        DataSet dss;
        MySqlCommandBuilder scmbl;

        //MySql connection
        MySqlConnection connection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
        MySqlCommand insertCommand;
        MySqlCommand insertCommanda;

        private void GetItems()
        {
            //Gets data and inserts into datagrid
            try
            {
                string sql = "SELECT * FROM items WHERE Status IS NULL OR Status = ''";
                con = dtba.connectToDb();
                dss = new DataSet();
                DataTable dtt = new DataTable();
                daa = new MySqlDataAdapter(sql, con);
                daa.Fill(dss);
                itemsdatagrid.ItemsSource = dss.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }        

        private void GetStaffItems()
        {
            //Gets data and inserts into datagrid
            try
            {
                string sql = "SELECT * FROM staffitem WHERE Status IS NULL OR Status = ''";
                con = dtba.connectToDb();
                dss = new DataSet();
                DataTable dtt = new DataTable();
                daa = new MySqlDataAdapter(sql, con);
                daa.Fill(dss);
                itemsdatagrida.ItemsSource = dss.Tables[0].DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        string itemName2, modea;
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataGrid dg = (DataGrid)sender;
                DataRowView rowSelected = dg.SelectedItem as DataRowView;
                if (rowSelected != null)
                {
                    idds.Text = rowSelected[0].ToString();
                    itemname.Text = rowSelected[1].ToString();
                    itemtype.Text = rowSelected[3].ToString();
                    itemName2 = rowSelected[1].ToString();
                    modea = "Payement_Type_of_" + itemname.Text;

                    updateitem.IsEnabled = true;
                    deletetitem.IsEnabled = true;
                }
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
            else if (e.Column.Header.ToString() == "post")
            {
                e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }
            else if (e.Column.Header.ToString() == "Status")
            {
                e.Cancel = true;   // For not to include 
                e.Column.IsReadOnly = true; // Makes the column as read only
            }
        }

        private void Additem_Click(object sender, RoutedEventArgs e)
        {
            string modea;
            if (itemname.Text == "")
            {
                notify.IsOpen = true;
                showing.Text = "ITEM NAME FEILD IS EMPTY";
            }           
            else
            {                
                if (itemtype.Text == "Student_Item")
                {
                    modea = "Payement_Type_of_"+itemname.Text;

                    string insertQuerya = "ALTER TABLE studitemdisplay ADD COLUMN " + itemname.Text+" INT(11), ADD COLUMN "+modea+" VARCHAR(1000)";

                    insertCommanda = new MySqlCommand(insertQuerya, connection);

                    try
                    {
                        connection.Open();
                        int resulta = insertCommanda.ExecuteNonQuery();
                        
                        string insertQuery = "INSERT INTO items(Item_Name,Mode_of_Payement,post)values(@dItem_Name,@dMode_of_Payement,@dpost)";

                        insertCommand = new MySqlCommand(insertQuery, connection);

                        insertCommand.Parameters.AddWithValue("@dItem_Name", itemname.Text);
                        insertCommand.Parameters.AddWithValue("@dMode_of_Payement", "Payement_Type_of_"+itemname.Text);
                        insertCommand.Parameters.AddWithValue("@dpost", itemtype.Text);

                        try
                        {
                            int result = insertCommand.ExecuteNonQuery();
                            if (result > 0)
                            {
                                notify.IsOpen = true;
                                showing.Text = "ITEM ACCEPTED";

                                itemname.Clear();

                                GetItems();
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
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
                else if(itemtype.Text == "Teacher_Item")
                {
                    modea = "Payement_Type_of_" + itemname.Text;

                    string insertQuerya = "ALTER TABLE staffitemdisplay ADD COLUMN " + itemname.Text + " INT(11) NULL, ADD COLUMN " + modea + " VARCHAR(1000) NULL";                   

                    insertCommanda = new MySqlCommand(insertQuerya, connection);

                    try
                    {
                        connection.Open();
                        int resulta = insertCommanda.ExecuteNonQuery();
                       
                        string insertQuery = "INSERT INTO staffitem(Item_Name,Mode_of_Payement,post)values(@dItem_Name,@dMode_of_Payement,@dpost)";                        

                        insertCommand = new MySqlCommand(insertQuery, connection);

                        insertCommand.Parameters.AddWithValue("@dItem_Name", itemname.Text);
                        insertCommand.Parameters.AddWithValue("@dMode_of_Payement", "Payement_Type_of_" + itemname.Text);
                        insertCommand.Parameters.AddWithValue("@dpost", itemtype.Text);

                        try
                        {
                            int result = insertCommand.ExecuteNonQuery();
                            if (result > 0)
                            {
                                notify.IsOpen = true;
                                showing.Text = "ITEM ACCEPTED";

                                itemname.Clear();

                                GetStaffItems();
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
                    showing.Text = "SELECT ITEM TYPE";
                }
            }
        }

        private void Updateitem_Click(object sender, RoutedEventArgs e)
        {
            string modeb;
            if (idds.Text != "")
            {
                if (itemtype.Text == "Student_Item")
                {
                    MySqlConnection sconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
                    MySqlCommand scommand;
                    MySqlCommand acommand;

                    MessageBox.Show("ALTER TABLE studitemdisplay CHANGE COLUMN " + itemName2 + " " + " " + " " + itemname.Text + "");

                    modeb = "Payement_Type_of_" + itemname.Text;

                    string sQuery = "UPDATE items SET Item_Name="+ itemname.Text + " AND Mode_of_Payement="+ modeb + " WHERE Id=" + idds.Text + "";

                    string aQuery = "ALTER TABLE studitemdisplay CHANGE " + itemName2 + " " + itemname.Text + " INT(11)";

                    scommand = new MySqlCommand(sQuery, sconnection);                    

                    acommand = new MySqlCommand(aQuery, sconnection);
                    try
                    {
                        sconnection.Open();
                        int result = scommand.ExecuteNonQuery();
                        int resulta = acommand.ExecuteNonQuery();
                        if (result > 0)
                        {
                            if (resulta > 0)
                            {
                                notify.IsOpen = true;
                                showing.Text = "STUDENT ITEM UPDATED";

                                GetItems();
                            }
                            else
                            {
                                MessageBox.Show("Error!!! UNABLE TO UPDATE STUDENT ITEM");
                            }
                        }
                        else
                        {
                            notify.IsOpen = true;
                            showing.Text = "Error!!! UNABLE TO UPDATE STUDENT ITEM";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    finally
                    {
                        sconnection.Close();
                    }
                }
                else if (itemtype.Text == "Teacher_Item")
                {
                    MySqlConnection sconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
                    MySqlCommand scommand;
                    MySqlCommand acommand;

                    modeb = "Payement_Type_of_" + itemname.Text;

                    string sQuery = "UPDATE staffitem SET Item_Name=@dItem_Name AND Mode_of_Payement=@dMode_of_Payement WHERE Id=" + idds.Text + "";

                    string aQuery = "ALTER TABLE staffitemdisplay CHANGE COLUMN " + itemName2 + " " + itemname.Text + "";

                    scommand = new MySqlCommand(sQuery, sconnection);
                    scommand.Parameters.AddWithValue("@dItem_Name", itemname.Text);
                    scommand.Parameters.AddWithValue("@dMode_of_Payement", modeb);

                    acommand = new MySqlCommand(aQuery, sconnection);
                    try
                    {
                        sconnection.Open();
                        int result = scommand.ExecuteNonQuery();
                        int resulta = acommand.ExecuteNonQuery();
                        if (result > 0)
                        {
                            if (resulta > 0)
                            {
                                notify.IsOpen = true;
                                showing.Text = "STAFF ITEM UPDATED";

                                GetItems();
                            }
                            else
                            {
                                MessageBox.Show("Error!!! UNABLE TO UPDATE STAFF ITEM");
                            }
                        }
                        else
                        {
                            notify.IsOpen = true;
                            showing.Text = "Error!!! UNABLE TO UPDATE STAFF ITEM";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    finally
                    {
                        sconnection.Close();
                    }
                }                
            }
            else
            {
                notify.IsOpen = true;
                showing.Text = "SELECT ITEM FROM THE DATA-GRID BEFORE PROCEEDING";
            }
        }

        private void Deletetitem_Click(object sender, RoutedEventArgs e)
        {
            string modeb;
            if (idds.Text != "")
            {
                if (itemtype.Text == "Student_Item")
                {                
                    MySqlConnection sconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
                    MySqlCommand scommand;
                    //MySqlCommand acommand;
                    
                    modeb = "Payement_Type_of_" + itemname.Text;

                    //string sQuery = "DELETE FROM items WHERE id=" + idds.Text + "";
                    string sQuery = "UPDATE items SET Status=@dNature WHERE Id=" + idds.Text + "";

                    //string aQuery = "ALTER TABLE studitemdisplay DROP COLUMN " + itemname.Text + ", DROP COLUMN " + modeb + "";

                    scommand = new MySqlCommand(sQuery, sconnection);
                    scommand.Parameters.AddWithValue("@dNature", "Removed");

                    //acommand = new MySqlCommand(aQuery, sconnection);                    
                    try
                    {
                        sconnection.Open();
                        int result = scommand.ExecuteNonQuery();
                        //int resulta = acommand.ExecuteNonQuery();
                        if (result > 0)
                        {
                            notify.IsOpen = true;
                            showing.Text = "STUDENT ITEM DROPED";

                            GetItems();
                        }
                        else
                        {
                            notify.IsOpen = true;
                            showing.Text = "Error!!! UNABLE TO DELETE STUDENT ITEM";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    finally
                    {
                        sconnection.Close();
                    }
                }
                else if (itemtype.Text == "Teacher_Item")
                {
                    MySqlConnection sconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");

                    MySqlCommand scommand;
                    //MySqlCommand acommand;

                    modeb = "Payement_Type_of_" + itemname.Text;

                    //string sQuery = "DELETE FROM staffitem WHERE id=" + idds.Text + "";
                    string sQuery = "UPDATE staffitem SET Status=@dNature WHERE Id=" + idds.Text + "";

                    //string aQuery = "ALTER TABLE staffitemdisplay DROP COLUMN " + itemname.Text + ", DROP COLUMN " + modeb + "";

                    scommand = new MySqlCommand(sQuery, sconnection);
                    scommand.Parameters.AddWithValue("@dNature", "Removed");
                
                    //acommand = new MySqlCommand(aQuery, sconnection);

                    try
                    {
                        sconnection.Open();
                        int result = scommand.ExecuteNonQuery();
                        //int resulta = acommand.ExecuteNonQuery();
                        if (result > 0)
                        {
                            notify.IsOpen = true;
                            showing.Text = "STAFF ITEM DROPED";

                            GetStaffItems();
                        }
                        else
                        {
                            notify.IsOpen = true;
                            showing.Text = "Error!!! UNABLE TO DELETE STAFF ITEM";
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    finally
                    {
                        sconnection.Close();
                    }
                }
                else
                {

                }
            }
            else
            {
                notify.IsOpen = true;
                showing.Text = "SELECT ITEM FROM THE DATA-GRID BEFORE PROCEEDING";
            }
        }
    }
}
