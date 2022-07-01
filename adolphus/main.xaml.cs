using System;
using System.Collections.Generic;
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
    /// Interaction logic for main.xaml
    /// </summary>
    public partial class main : Window
    {
        public main()
        {
            InitializeComponent();
            load();
        }

        void load()
        {
            username.Text = Settings1.Default.nameS;            
        }

        private void drag(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void restore(object sender, RoutedEventArgs e)
        {

        }

        private void minimise(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void maximise(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            {
                this.WindowState = WindowState.Maximized;

                maxia.Visibility = Visibility.Hidden;
                resto.Visibility = Visibility.Visible;
                maxi.ToolTip = "Restore Down";
            }
            else if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;

                resto.Visibility = Visibility.Hidden;
                maxia.Visibility = Visibility.Visible;
                maxi.ToolTip = "Maximise";
            }
        }

        private void closea(object sender, RoutedEventArgs e)
        {
            this.Close();
            main win = new main();
            win.Close();
        }

        private void up_operate(object sender, MouseEventArgs e)
        {
            var brush = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#28808080");
            clo.Background = brush; 
        }

        private void up_operate_leave(object sender, MouseEventArgs e)
        {
            clo.Background = Brushes.Transparent;  
        }

        private void up_operate1(object sender, MouseEventArgs e)
        {
            var brush = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#28808080");
            maxi.Background = brush;
        }

        private void up_operate_leave1(object sender, MouseEventArgs e)
        {          
            maxi.Background = Brushes.Transparent;
        }

        private void up_operate2(object sender, MouseEventArgs e)
        {
            var brush = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#28808080");
            mini.Background = brush;            
        }

        private void up_operate_leave2(object sender, MouseEventArgs e)
        {        
            mini.Background = Brushes.Transparent;        
        }

        private void previouspage(object sender, RoutedEventArgs e)
        {
            login objMainWindow = new login();
            objMainWindow.Show();
            this.Close();
        }

        private void button1_MouseEnter(object sender, MouseEventArgs e)
        {
           
        }

        private void button1_MouseLeave(object sender, MouseEventArgs e)
        {
            
        }

        private void button2_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void button2_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void button3_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void button3_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void button4_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void button4_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void button5_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void button5_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void button6_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void button6_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        void gene_color()
        {
            var colora = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FFC9C9C9");           
            buttona.Foreground = colora;
            buttonb.Foreground = colora;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //setting background for class name "block"
            var block_background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FFF9F9F9");
            block.Background = block_background;

            page_text.Text = "Add Student";

            var brusha = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF303642");
            button_Copy.Background = brusha;

            var brush = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF303642");
            button.Background = brush;           
           
            gene_color();

            container.Visibility = Visibility.Visible;
            defaultcontain.Visibility = Visibility.Hidden;

            addstudent objOpen = new addstudent();
            mainframe.NavigationService.Navigate(objOpen);
            
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            //setting background for class name "block"
            var block_background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FFF9F9F9");
            block.Background = block_background;

            page_text.Text = "View, Edit Student Info";

            var brusha = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF303642");
            button_Copy.Background = brusha;

            var brush = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF303642");
            button.Background = brush;            

            gene_color();

            container.Visibility = Visibility.Visible;
            defaultcontain.Visibility = Visibility.Hidden;

            view_edit_student objOpen = new view_edit_student();
            mainframe.NavigationService.Navigate(objOpen);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            //setting background for class name "block"
            var block_background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FFF9F9F9");
            block.Background = block_background;

            page_text.Text = "Expenditures";

            var brusha = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF303642");
            button_Copy.Background = brusha;

            var brush = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF303642");
            button.Background = brush;            

            gene_color();

            container.Visibility = Visibility.Visible;
            defaultcontain.Visibility = Visibility.Hidden;

            expenditure objOpen = new expenditure();
            mainframe.NavigationService.Navigate(objOpen);

            //mainframe.Source = new Uri("http://localhost:81/srms/srms/find-result.php", UriKind.Absolute);
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            //setting background for class name "block"
            var block_background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FFF9F9F9");
            block.Background = block_background;

            page_text.Text = "Add Teachers";

            var brusha = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF303642");
            button_Copy.Background = brusha;

            var brush = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF303642");
            button.Background = brush;

            gene_color();

            container.Visibility = Visibility.Visible;
            defaultcontain.Visibility = Visibility.Hidden;

            teachers objOpen = new teachers();
            mainframe.NavigationService.Navigate(objOpen);            
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            //setting background for class name "block"
            var block_background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FFF9F9F9");
            block.Background = block_background;

            page_text.Text = "View, Edit Staffs";

            var brusha = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF303642");
            button_Copy.Background = brusha;

            var brush = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF303642");
            button.Background = brush;            

            gene_color();

            container.Visibility = Visibility.Visible;
            defaultcontain.Visibility = Visibility.Hidden;

            view_edit_teachers objOpen = new view_edit_teachers();
            mainframe.NavigationService.Navigate(objOpen);            
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            //setting background for class name "block"
            var block_background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FFF9F9F9");
            block.Background = block_background;

            page_text.Text = "Add new Items";

            var brusha = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF303642");
            button_Copy.Background = brusha;

            var brush = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF303642");
            button.Background = brush;            

            gene_color();

            container.Visibility = Visibility.Visible;
            defaultcontain.Visibility = Visibility.Hidden;

            add_items objOpen = new add_items();
            mainframe.NavigationService.Navigate(objOpen);         
        }

        private void textBlock_Cop1_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }

        private void textBlock_Cop1_MouseLeave(object sender, MouseEventArgs e)
        {
            
        }

        private void dashboard_click(object sender, RoutedEventArgs e)
        {
            //setting background for class name "block"
            var block_background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#00000000");
            block.Background = block_background;            

            //onclick changes background of border
            var brush = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FFFFFFFF");
            button.Background = brush;

            var brusha = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF303642");
            button_Copy.Background = brusha;           

            //onclick color changes to blue black color
            var color = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF6E7697");
            buttona.Foreground = color;            
            buttonb.Foreground = color;


            //normal color
            var colora = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FFC9C9C9");
            button_Copya.Foreground = colora;
            button_Copyb.Foreground = colora;

            defaultcontain.Visibility = Visibility.Visible;
            container.Visibility = Visibility.Hidden;
        }

        private void profile_click(object sender, RoutedEventArgs e)
        {
            //setting background for class name "block"
            var block_background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FFF9F9F9");
            block.Background = block_background;

            page_text.Text = "Calculate";

            var brusha = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FFFFFFFF");
            button_Copy.Background = brusha;

            var brush = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF303642");
            button.Background = brush;

            var brushb = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF303642");
            button_Copy1.Background = brushb;

            var color = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF6E7697");
            button_Copya.Foreground = color;
            button_Copyb.Foreground = color;

            var colora = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FFC9C9C9");            
            buttona.Foreground = colora;
            buttonb.Foreground = colora;

            container.Visibility = Visibility.Visible;
            defaultcontain.Visibility = Visibility.Hidden;

            total_system objOpen = new total_system();
            mainframe.NavigationService.Navigate(objOpen);

        }

        private void website_click(object sender, RoutedEventArgs e)
        {
            //setting background for class name "block"
            var block_background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FFF9F9F9");
            block.Background = block_background;

            var brusha = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF303642");
            button_Copy.Background = brusha;

            var brush = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF303642");
            button.Background = brush;                   

            var colora = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FFC9C9C9");            
            buttona.Foreground = colora;
            buttonb.Foreground = colora;
        }

        private void return_click(object sender, RoutedEventArgs e)
        {
            //setting background for class name "block"
            var block_background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#00000000");
            block.Background = block_background;

            //onclick changes background of border
            var brush = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FFFFFFFF");
            button.Background = brush;

            var brusha = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF303642");
            button_Copy.Background = brusha;            

            //onclick color changes to blue black color
            var color = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF6E7697");
            buttona.Foreground = color;
            buttonb.Foreground = color;

            //normal color
            var colora = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FFC9C9C9");
            button_Copya.Foreground = colora;
            button_Copyb.Foreground = colora;

            container.Visibility = Visibility.Hidden;
            defaultcontain.Visibility = Visibility.Visible;
        }

        private void return_MouseEnter(object sender, MouseEventArgs e)
        {
            var brush = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF2B70B4");
            returna.Foreground = brush;
        }

        private void return_MouseLeave(object sender, MouseEventArgs e)
        {
            var brush = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF6E7697");
            returna.Foreground = brush;
        }

        private void logout(object sender, RoutedEventArgs e)
        {
            //setting background for class name "block"
            var block_background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FFF9F9F9");
            block.Background = block_background;

            var brusha = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF303642");
            button_Copy.Background = brusha;

            var brush = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF303642");
            button.Background = brush;                      

            var color = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF6E7697");
            button_Copy1c.Foreground = color;
            button_Copy1d.Foreground = color;

            var colora = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FFC9C9C9");            
            buttona.Foreground = colora;
            buttonb.Foreground = colora;            

            login win = new login();
            win.Show();

            this.Close();
        }

        private void settings_Click(object sender, RoutedEventArgs e)
        {
            settings_page win = new settings_page();
            win.Show();

            this.Close();
        }

        private void classs_Click(object sender, RoutedEventArgs e)
        {
            //classa win = new classa();
            //win.Show();            
        }

        private void Window_GotFocus(object sender, RoutedEventArgs e)
        {
            //classa win = new classa();
            //win.Close();
        }

        private void promotion_Click(object sender, RoutedEventArgs e)
        {
            //promotion win = new promotion();
            //win.Show();
        }

        private void dele_Click(object sender, RoutedEventArgs e)
        {
            //setting background for class name "block"
            var block_background = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FFF9F9F9");
            block.Background = block_background;

            page_text.Text = "Deletion";

            var brusha = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FFFFFFFF");
            button_Copy.Background = brusha;

            var brush = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF303642");
            button.Background = brush;

            var brushb = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF303642");
            button_Copy1.Background = brushb;

            var color = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FF6E7697");
            button_Copya.Foreground = color;
            button_Copyb.Foreground = color;

            var colora = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#FFC9C9C9");
            buttona.Foreground = colora;
            buttonb.Foreground = colora;

            container.Visibility = Visibility.Visible;
            defaultcontain.Visibility = Visibility.Hidden;

            deleteAll objOpen = new deleteAll();
            mainframe.NavigationService.Navigate(objOpen);
        }
    }
}
