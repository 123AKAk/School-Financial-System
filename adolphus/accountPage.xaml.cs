using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
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
using System.Management;
using System.Management.Instrumentation;
using MySql.Data.MySqlClient;

namespace adolphus
{
    /// <summary>
    /// Interaction logic for accountPage.xaml
    /// </summary>
    public partial class accountPage : Page
    {
        encrypt tdl = new encrypt();
        int mainId;

        //MySql connection
        MySqlConnection connection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
        MySqlCommand insertCommand, insertCommanda;

        public accountPage()
        {
            mainId = mainLogin.userId;
            InitializeComponent();
            DataContext = tdl;
            //allow_access();

            getImage();
        }

        private void getImage()
        {
            MySqlConnection cconnection = new MySqlConnection("server=localhost;database=eyosms;uid=root;pwd=;");
            MySqlCommand ccommand;
            var cQuery = "SELECT * FROM login WHERE Id='" + mainId + "'";
            ccommand = new MySqlCommand(cQuery, cconnection);
            try
            {
                cconnection.Open();
                MySqlDataReader readerc = ccommand.ExecuteReader();
                while (readerc.Read())
                {
                    uname.Text = readerc["Username"].ToString();
                    email.Text = readerc["Email"].ToString();
                    password.Password = readerc["Password"].ToString();

                    //Store binary data read from the database in a byte array
                    byte[] blob = (byte[])readerc["Image"];
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
                    image.Source = bi;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            allencrypt openWin = new allencrypt();
            openWin.ShowDialog();
        }

        private void openfolder(object sender, RoutedEventArgs e)
        {
            if(System.IO.File.Exists(""))
            {
                MessageBox.Show("You Haven't Restored any File Yet");
            }
            else
            {
                OpenFileDialog opfd = new OpenFileDialog();
                string initDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                initDir = System.IO.Path.Combine(initDir, "School_Management_System");
                opfd.InitialDirectory = initDir;                
                opfd.RestoreDirectory = true;
                opfd.ShowDialog();
            }            
        }
               
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //allow_access();
        }       
        
        private void allow_access()
        {
            FileSecurity fileSecurity = File.GetAccessControl(myfolder);
            AuthorizationRuleCollection rules = fileSecurity.GetAccessRules(true, true, typeof(NTAccount));
            foreach (FileSystemAccessRule rule in rules)
            {
                string name = rule.IdentityReference.Value;
                //MessageBox.Show(name);
                if (rule.FileSystemRights != FileSystemRights.FullControl)
                {
                    FileSecurity newFileSecurity = File.GetAccessControl(myfolder);
                    FileSystemAccessRule newRule = new FileSystemAccessRule(name, FileSystemRights.FullControl, AccessControlType.Allow);
                    newFileSecurity.AddAccessRule(newRule);
                    File.SetAccessControl(myfolder, newFileSecurity);
                }
            }
            ch = new DirectoryInfo(myfolder);
            ch.Attributes = FileAttributes.Normal;

            FileInfo file = new FileInfo(myfolder);
            file.IsReadOnly = false;
        }        
        DirectoryInfo ch;
        string myfolder = Directory.GetCurrentDirectory() + "\\encryptedFiles\\";

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            allow_access();
            try
            {
                ch = new DirectoryInfo(myfolder);
                ch.Attributes = FileAttributes.Normal;

                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = true;
                openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
                if (openFileDialog.ShowDialog() == true)
                {

                    string[] afilename = openFileDialog.SafeFileNames;
                    string[] MoveFrom = openFileDialog.FileNames;
                    //string MoveTo = "C://Users//User//Videos//adolphus//adolphus//encryptedFiles";                    

                    foreach (string files in MoveFrom)
                    {
                        Directory.Move(files, System.IO.Path.Combine(myfolder, System.IO.Path.GetFileName(files)));
                    }

                    foreach (string filesname in afilename)
                    {
                        tdl.Additem(filesname.Trim());

                        lvToDo.Items.Refresh();
                    }
                    ch = new DirectoryInfo(myfolder);
                    ch.Attributes = FileAttributes.Encrypted;
                    ch.Attributes = FileAttributes.Hidden;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Problem Deh", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            tdl.Save();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            tdl.Save();            
        }

        //view information about the file, like size, datemoved, name, type of file etc
        private void ViewInfo(object sender, RoutedEventArgs e)
        {
            //allow_access();
            try
            {
                string getFilefolder = Directory.GetCurrentDirectory() + "\\encryptedFiles\\" + lvToDo.SelectedItem.ToString();
                var fi1 = new FileInfo(getFilefolder);
                MessageBox.Show("File Information:\n\n" + "Name:- " + fi1.Name + "\nExtension:- " + fi1.Extension + "\n" + "Last Access Time:- " + fi1.LastAccessTime + "\n" + "Last Write Time:- " + fi1.LastWriteTime + "\n" + "Creation Time:- " + fi1.CreationTime + "\n" + "Length:- " + fi1.Length + "\n" + "Attibutes:- " + fi1.Attributes + "\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        private void lvToDo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ViewInfo(sender, e);
        }

        private void openFile_Click(object sender, RoutedEventArgs e)
        {
            if (lvToDo.SelectedItem != null)
            {
                string mainFilepath = Directory.GetCurrentDirectory() + "\\encryptedFiles\\" + lvToDo.SelectedItem.ToString();
                try
                {
                    System.Diagnostics.Process.Start(mainFilepath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            allow_access();
            if (lvToDo.SelectedItem != null)
            {
                MessageBoxResult del = MessageBox.Show("Delete this Item?\n\nThis File will be in the Recycle Bin, make sure you delete it permanently ", "Remove?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (del == MessageBoxResult.Yes)
                {
                    string movementfolder = Directory.GetCurrentDirectory() + "\\encryptedFiles\\";
                    string delFile = System.IO.Path.Combine(movementfolder, lvToDo.SelectedItem.ToString());
                    System.IO.FileInfo fia = new System.IO.FileInfo(@delFile);
                    try
                    {
                        fia.Delete();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    }
                    //MessageBox.Show(lvToDo.SelectedItem.ToString());
                    tdl.DeleteItem(lvToDo.SelectedItem as ToDoItem);
                }
                lvToDo.Items.Refresh();
            }
            tdl.Save();
        }
        private void Restore_Click(object sender, RoutedEventArgs e)
        {
            allow_access();
            try
            {
                if (lvToDo.SelectedItem != null)
                {
                    MessageBoxResult del = MessageBox.Show("Restore this Item?\n\nThis File will be Restored to the Application Document Folder", "Remove?", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (del == MessageBoxResult.Yes)
                    {
                        string restoreDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                        restoreDir = System.IO.Path.Combine(restoreDir, "School_Management_System");

                        string movementfolder = Directory.GetCurrentDirectory() + "\\encryptedFiles\\";

                        if (Directory.Exists(restoreDir))
                        {
                            string[] fileList = System.IO.Directory.GetFiles(movementfolder, lvToDo.SelectedItem.ToString());
                            foreach (string file in fileList)
                            {
                                string fileToMove = file;
                                string moveTo = System.IO.Path.Combine(restoreDir, lvToDo.SelectedItem.ToString());
                                File.Move(@fileToMove, @moveTo);
                            }
                            //string[] fileList = System.IO.Directory.GetFiles(movementfolder, lvToDo.SelectedItem.ToString());
                            //foreach (string file in fileList)
                            //{
                            //    string fileToMove = movementfolder + file;
                            //    string moveTo = restoreDir + file;

                            //    File.Move(fileToMove, moveTo);
                            //}                       
                        }
                        else
                        {
                            System.IO.Directory.CreateDirectory(restoreDir);
                            string[] fileList = System.IO.Directory.GetFiles(movementfolder, lvToDo.SelectedItem.ToString());
                            foreach (string file in fileList)
                            {
                                string fileToMove = file;
                                string moveTo = System.IO.Path.Combine(restoreDir, lvToDo.SelectedItem.ToString());
                                File.Move(@fileToMove, @moveTo);
                            }
                        }
                        tdl.DeleteItem(lvToDo.SelectedItem as ToDoItem);
                    }
                    lvToDo.Items.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            tdl.Save();
        }

        string astrName, aimageName;
        int doneimg = 0;
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
                    aimageName = fldlg.FileName;

                    if (aimageName == "")
                    {
                        MessageBox.Show("YOU DID NOT CHOOSE AN IMAGE");
                    }
                    else
                    {
                        astrName = fldlg.SafeFileName;
                        ImageSourceConverter isc = new ImageSourceConverter();
                        image.SetValue(Image.SourceProperty, isc.ConvertFromString(aimageName));
                    }
                    doneimg = 1;
                }
                fldlg = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Image Feild Cannot be empty", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void updaeInfo(object sender, RoutedEventArgs e)
        {
            if(doneimg != 0)
            {
                FileStream fs = new FileStream(aimageName, FileMode.Open, FileAccess.Read);
                byte[] imgByteArr = new byte[fs.Length];
                fs.Read(imgByteArr, 0, Convert.ToInt32(fs.Length));
                fs.Close();

                //create insert query     
                string insertQuery = "UPDATE login SET Image=@dImage,Username=@dUsername,Email=@dEmail,Password=@dPassword,imagepath='" + astrName + "' WHERE Id='" + mainId + "'";
                insertCommand = new MySqlCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@dImage", imgByteArr);
                insertCommand.Parameters.AddWithValue("@dUsername", uname.Text);
                insertCommand.Parameters.AddWithValue("@dEmail", email.Text);
                insertCommand.Parameters.AddWithValue("@dPassword", password.Password);
                try
                {
                    connection.Open();
                    int result = insertCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        notify.IsOpen = true;
                        showing.Text = "Updated Successfully";                        
                    }
                    else
                    {
                        notify.IsOpen = true;
                        showing.Text = "Error!!! Unable to Update";
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
                //create insert query     
                string insertQuery = "UPDATE login SET Username=@dUsername,Email=@dEmail,Password=@dPassword WHERE Id='" + mainId + "'";
                insertCommand = new MySqlCommand(insertQuery, connection);                
                insertCommand.Parameters.AddWithValue("@dUsername", uname.Text);
                insertCommand.Parameters.AddWithValue("@dEmail", email.Text);
                insertCommand.Parameters.AddWithValue("@dPassword", password.Password);
                try
                {
                    connection.Open();
                    int result = insertCommand.ExecuteNonQuery();
                    if (result > 0)
                    {
                        notify.IsOpen = true;
                        showing.Text = "Updated Successfully";
                    }
                    else
                    {
                        notify.IsOpen = true;
                        showing.Text = "Error!!! Unable to Update";
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
}
