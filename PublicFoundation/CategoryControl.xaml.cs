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

namespace PublicFoundation
{
    /// <summary>
    /// Interaction logic for CategoryControl.xaml
    /// </summary>
    public partial class CategoryControl : UserControl
    {
        public static List<App.Category> category = new List<App.Category>();
        int delet = 0;
        public CategoryControl()
        {
            InitializeComponent();
            //gridcategory.
            //gridcategory.ItemsSource = MainWindow.category;
            UploadCategory();
        }

        public void UploadCategory()
        {
            using (var db = new App.FenContext())
            {
                category.Clear();
                var categoryname = db.categories.ToList();
                gridcategory.ItemsSource = categoryname;
                foreach (var categ in categoryname)
                    category.Add(categ);
            }
        }

        private void buttonaddcategory_Click(object sender, RoutedEventArgs e)
        {
            AddCategoryWindow ac = new AddCategoryWindow();
            ac.ShowDialog();
            UploadCategory();
        }

        private void gridcategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int n = (gridcategory.SelectedIndex==-1)? n = 0 : n = gridcategory.SelectedIndex;
                if (category.Count != 0)
                {
                    var categor = category[n];
                    categorynamebox.Text = categor.title;
                    categorabbrevbox.Text = categor.abbreviation;
                    //
                    delet = categor.id;
                }

            }
            catch (Exception exp) { MessageBox.Show("Произошла ошибка при выборе категории :" + exp.Message, "Ошибка"); }
        }

        private void buttondeletecategory_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult ds = MessageBox.Show("Удалить выбранную категорию?", "Подтверждение удаления", MessageBoxButton.YesNo);
            switch (ds)
            {
                case MessageBoxResult.Yes:
                    try
                    {
                        using (var db = new App.FenContext())
                        {
                            var d = db.categories.Where(x => x.id == delet).FirstOrDefault();
                            db.categories.Remove(d);
                            db.SaveChanges();
                        }
                    }
                    catch (Microsoft.EntityFrameworkCore.DbUpdateException) { MessageBox.Show("Категория используется", "Ошибка"); }
                    UploadCategory();
                    break;
                case MessageBoxResult.No:
                    break;
                default:
                    break;
            }
        }

        private void buttoneditcategory_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult ds = MessageBox.Show("Изменить выбранную категорию?", "Подтверждение изменения", MessageBoxButton.YesNo);
            switch (ds)
            {
                case MessageBoxResult.Yes:
                    using (var db = new App.FenContext())
                    {
                        var d = db.categories.Where(x => x.id == delet).FirstOrDefault();

                        d.title = categorynamebox.Text;
                        d.abbreviation = categorabbrevbox.Text;
                        db.SaveChanges();
                    }
                    UploadCategory();
                    break;
                case MessageBoxResult.No:
                    break;
                default:
                    break;
            }
        }
    }
}
