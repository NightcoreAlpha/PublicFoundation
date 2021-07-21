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
    /// Логика взаимодействия для UserControlMembers.xaml
    /// </summary>
    public partial class UserControlMembers : UserControl
    {
        public static bool edit = false;
        public static int id = 0, idmembers = 0;
        public static int selind = 0;
        public static List<App.Member> memberslist = new List<App.Member>();
        public UserControlMembers()
        {
            InitializeComponent();
            UploadMembers();
        }
        public void UploadMembers()
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
                var memb = db.members.ToList();
                memberslist.Clear();
                foreach (var m in memb)
                    memberslist.Add(m);
            }
        }
        private void addbox_Click(object sender, RoutedEventArgs e)
        {
            edit = false;
            AddMemberWindow am = new AddMemberWindow();
            am.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            am.ShowDialog();
            UploadMembers();
        }
        public class Member
        {
            public int id { get; set; }
            public string registration_number { get; set; }
            public string first_name { get; set; }
            public string middle_name { get; set; }
            public string last_name { get; set; }
            public int age { get; set; }
            public string gender { get; set; }
            public string bday { get; set; }
            public string phone_number { get; set; }
            public int disability_group { get; set; }
            public string category_id { get; set; }
            public string status { get; set; }
            public string entry_date { get; set; }
            public string residence_street { get; set; }
            public string residence_apartment_number { get; set; }
            public string residence_house_number { get; set; }
            public string residence_locality { get; set; }
            public string residence_district { get; set; }
            public string residence_region { get; set; }
            public string residence_postal_code { get; set; }
            public string actual_district { get; set; }
            public string passport_series { get; set; }
            public string passport_number { get; set; }
            public string passport_issued_by { get; set; }
            public string passport_issued_date { get; set; }
            public string passport_division_code { get; set; }
            public bool gosuslugi_registration_availability { get; set; }
            public string inn { get; set; }
            public string snils { get; set; }
            public string pension_certificate_number { get; set; }
            public string vtek_number { get; set; }
        }
        private void editbox_Click(object sender, RoutedEventArgs e)
        {
            if (gridinvalid.SelectedIndex != -1)
            {
                edit = true;
                AddMemberWindow am = new AddMemberWindow();
                am.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                am.ShowDialog();
                UploadMembers();
            }
            else MessageBox.Show("Не выбран обект изменения", "Ошибка");
        }
        private void deletebox_Click(object sender, RoutedEventArgs e)
        {
            if (gridinvalid.SelectedIndex != -1)
            {
                MessageBoxResult ds = MessageBox.Show("Удалить выбранный объект?", "Подтверждение удаления", MessageBoxButton.YesNo);
                switch (ds)
                {
                    case MessageBoxResult.Yes:
                        using (var db = new App.FenContext())
                        {
                            var deletemember = db.members.Where(x => x.id == UserControlMembers.idmembers).FirstOrDefault();
                            db.members.Remove(deletemember);
                            db.SaveChanges();
                        }
                        UploadMembers();
                        break;
                    case MessageBoxResult.No:
                        break;
                    default:
                        break;
                }
            }
            else MessageBox.Show("Не выбран объект для удаления","Ошибка");
        }
        private void gridinvalid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (gridinvalid.SelectedIndex != -1)
                {
                    idmembers = (int)gridinvalid.SelectedItem.GetType().GetProperty("id").GetValue(gridinvalid.SelectedItem, null);
                    //idmembers = idmember.GetType().GetProperty("id").GetValue(idmember,null);
                    selind = (gridinvalid.SelectedIndex == -1) ? selind = 0 : selind = gridinvalid.SelectedIndex;
                }
                if (memberslist.Count != 0)
                {
                    var categor = memberslist[selind];
                    id = categor.id;
                }
            }
            catch (Exception exp) { MessageBox.Show("Произошла ошибка при выборе категории :" + exp.Message, "Ошибка"); }
        }
    }
}
