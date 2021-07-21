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
using System.Windows.Shapes;

namespace PublicFoundation
{
    /// <summary>
    /// Interaction logic for AccountWIndow.xaml
    /// </summary>
    public partial class AccountWIndow : Window
    {
        public string goname { get; set; }
        public AccountWIndow()
        {
            InitializeComponent();
            try
            {
                using (var db = new App.FenContext())
                {
                    var newname = db.accounts.Where(x => x.id == 1).FirstOrDefault();
                    namebox.Text = newname.name_account;
                    //foreach(var n in newname)
                    //newname.name_account = namebox.Text;
                    //db.SaveChanges();
                }
            }catch (Exception) { MessageBox.Show("Некорректный ввод", "Ошибка"); }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var db = new App.FenContext())
                {
                    var newname = db.accounts.Where(x => x.id == 1).FirstOrDefault();
                    newname.name_account = namebox.Text;
                    db.SaveChanges();
                }
                Close();
            }
            catch (Exception) { MessageBox.Show("Некорректный ввод","Ошибка"); }
        }
    }
}
