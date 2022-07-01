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

namespace adolphus
{
    /// <summary>
    /// Interaction logic for allencrypt.xaml
    /// </summary>
    public partial class allencrypt : Window
    {
        encrypt tdl = new encrypt();
        public allencrypt()
        {
            InitializeComponent();
            DataContext = tdl;
            allow_access();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //allow_access();
        }
        private void deny_access()
        {
            MessageBox.Show("eweee");
            bool exists = System.IO.Directory.Exists(myfolder);
            if (!exists)
            {
                DirectoryInfo di = System.IO.Directory.CreateDirectory(myfolder);                
            }
            else
            {                
            }
            DirectoryInfo dInfo = new DirectoryInfo(myfolder);
            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            dSecurity.AddAccessRule(new FileSystemAccessRule(
                    new SecurityIdentifier(WellKnownSidType.WorldSid, null),                    
                    FileSystemRights.Read,                                        
                    AccessControlType.Allow));
            dInfo.SetAccessControl(dSecurity);
        }
        private void allow_access()
        {
            MessageBox.Show("Gud");
            bool exists = System.IO.Directory.Exists(myfolder);
            if (!exists)
            {
                DirectoryInfo di = System.IO.Directory.CreateDirectory(myfolder);                
            }
            else
            {                
            }
            DirectoryInfo dInfo = new DirectoryInfo(myfolder);
            DirectorySecurity dSecurity = dInfo.GetAccessControl();
            var sid = new SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid, null);

            dSecurity.AddAccessRule(
               new FileSystemAccessRule(
                   sid,
                   FileSystemRights.Modify,
                   InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit,
                   PropagationFlags.None,
                   AccessControlType.Allow));
        }
        private void drag(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
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
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            tdl.Save();
            deny_access();
        }

        //view information about the file, like size, datemoved, name, type of file etc
        private void ViewInfo(object sender, RoutedEventArgs e)
        {
            allow_access();
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
        }
        private void Restore_Click(object sender, RoutedEventArgs e)
        {
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
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }        
    }
}
