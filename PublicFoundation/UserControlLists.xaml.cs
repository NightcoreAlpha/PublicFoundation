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
    /// Логика взаимодействия для UserControlLists.xaml
    /// </summary>
    public partial class UserControlLists : UserControl
    {
        public UserControlLists()
        {
            InitializeComponent();
            UploadMembersList();
        }
        public void UploadMembersList()
        {
            using (var db = new App.FenContext())
            {
                var invalods = from m in db.members
                               join categor in db.categories on m.category_id equals categor.id
                               select new
                               {
                                   id = m.id,
                                   registration_number = m.registration_number,
                                   first_name = m.first_name,
                                   middle_name = m.middle_name,
                                   last_name = m.last_name,
                                   age = m.age,
                                   gender = m.gender,
                                   bday = m.bday,
                                   phone_number = m.phone_number,
                                   disability_group = m.disability_group,
                                   category_id = categor.abbreviation,//categor[categorybox.SelectedIndex].id,
                                   status = m.status,
                                   entry_date = m.entry_date,
                                   residence_street = m.residence_street,
                                   residence_apartment_number = m.residence_apartment_number,
                                   residence_house_number = m.residence_house_number,
                                   residence_locality = m.residence_locality,
                                   residence_district = m.residence_district,
                                   residence_region = m.residence_region,
                                   residence_postal_code = m.residence_postal_code,
                                   actual_district = m.actual_district,
                                   passport_series = m.passport_series,
                                   passport_number = m.passport_number,
                                   passport_issued_by = m.passport_issued_by,
                                   passport_issued_date = m.passport_issued_date,
                                   passport_division_code = m.passport_division_code,
                                   gosuslugi_registration_availability = m.gosuslugi_registration_availability,
                                   inn = m.inn,
                                   snils = m.snils,
                                   pension_certificate_number = m.pension_certificate_number,
                                   vtek_number = m.vtek_number
                               };
                gridinvalid.ItemsSource = invalods.ToList();
            }
        }
        private void gridinvalid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void fullbutton_Click(object sender, RoutedEventArgs e)
        {
            UploadMembersList();
        }

        private void lifebutton_Click(object sender, RoutedEventArgs e)
        {
            using(var db = new App.FenContext())
            {
                var lifemembers = db.members.Where(x=> x.status == "Жив");
                gridinvalid.ItemsSource = lifemembers.ToList();
            }
        }
    }
}
