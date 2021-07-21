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
    /// Interaction logic for AddCategoryWindow.xaml
    /// </summary>
    public partial class AddCategoryWindow : Window
    {
        public AddCategoryWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (var db = new App.FenContext())
            {
                App.Category cat = new App.Category
                {
                    title = titlebox.Text,
                    abbreviation = abbrevbox.Text
                };
                db.categories.Add(cat);
                db.SaveChanges();
            }
            Close();
            
        }
        /*
         * var persons = from per in loginconnect.persons
                                  join co in loginconnect.countrys on per.id_country equals co.id
                                  orderby per.id
                                  join pr in loginconnect.professions on per.id_prof equals pr.id
                                  join ma in loginconnect.machines on per.id_m equals ma.id into mash
                                  from submash in mash.DefaultIfEmpty()
                                  select new
                                  {
        */
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
