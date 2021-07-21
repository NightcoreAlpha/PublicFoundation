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
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace PublicFoundation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string goname { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            using (var db = new App.FenContext())
            {
                var accountname = db.accounts.ToList();
                foreach (var an in accountname)
                    accountbox.Text = an.name_account;
            }
        }
        public void cb(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            AccountWIndow aw = new AccountWIndow();
            aw.Owner = this;
            goname = accountbox.Text;
            aw.ShowDialog();
        }
        private void ButtonPopUpLogout_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
            ButtonCloseMenu.Visibility = Visibility.Visible;
        }
        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenMenu.Visibility = Visibility.Visible;
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        private void lw1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lw1.SelectedIndex;
            //MoveCursorMenu(index);
            switch (index)
            {
                case 0:
                    gridplace1.Children.Clear();
                    gridplace1.Children.Add(new UserControl1());
                    break;
                case 1:
                    gridplace1.Children.Clear();
                    gridplace1.Children.Add(new UserControlMembers());
                    break;
                case 2:
                    gridplace1.Children.Clear();
                    gridplace1.Children.Add(new UserControlLists());
                    break;
                case 3:
                    gridplace1.Children.Clear();
                    gridplace1.Children.Add(new UserControlService());
                    break;
                case 4:
                    gridplace1.Children.Clear();
                    gridplace1.Children.Add(new CategoryControl());
                    break;
                default:
                    break;
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            gridplace1.Children.Clear();
            gridplace1.Children.Add(new UserControl1());
        }
        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            /*if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }else if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }*/
        }
    }
}
