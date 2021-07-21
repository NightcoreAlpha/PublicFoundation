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
    /// Логика взаимодействия для AddMemberWindow.xaml
    /// </summary>
    public partial class AddMemberWindow : Window
    {
        int id = 0;
        List<App.Category> categor = new List<App.Category>();
        public AddMemberWindow()
        {
            InitializeComponent();
            categor.Clear();
            using (var db = new App.FenContext())
            {
                var categories = db.categories.ToList();
                foreach (var c in categories)
                {
                    categorybox.Items.Add(c.abbreviation);
                    categor.Add(c);
                }
            }
            genderbox.Items.Add("Муж");
            genderbox.Items.Add("Жен");
            gruppabox.Items.Add(1);
            gruppabox.Items.Add(2);
            gruppabox.Items.Add(3);
            statusbox.Items.Add("Жив");
            statusbox.Items.Add("Не жив");
            statusbox.Items.Add("Переехал");
            if (UserControlMembers.edit == true)
            {
                addbutton.Content = "Изменить";
                //
                using (var db = new App.FenContext())
                {
                    var members = db.members.Where(x => x.id == UserControlMembers.idmembers).FirstOrDefault();
                    //
                    //var members = UserControlMembers.memberslist;
                    int idcateg = members.category_id;
                    var c = categor.Where(x => x.id == idcateg);
                    regbox.Text = members.registration_number;
                    namebox.Text = members.first_name;
                    fambox.Text = members.middle_name;
                    otchbox.Text = members.last_name;
                    //DateTime.Now.Year - bdaybox.SelectedDate.Value.Year ,
                    genderbox.SelectedItem = members.gender;
                    bdaybox.Text = members.bday;
                    telbox.Text = members.phone_number;
                    gruppabox.SelectedItem = members.disability_group;
                    //
                    foreach (var categ in c)
                    {
                        categorybox.SelectedItem = categ.abbreviation;
                    }
                    //
                    statusbox.SelectedItem = members.status;
                    inbox.Text = Convert.ToDateTime(members.entry_date).ToShortDateString();
                    streetbox.Text = members.residence_street;
                    kvbox.Text = members.residence_apartment_number;
                    homebox.Text = members.residence_house_number;
                    localitybox.Text = members.residence_locality;
                    districtbox.Text = members.residence_district;
                    regionbox.Text = members.residence_region;
                    indexbox.Text = members.residence_postal_code;
                    new TextRange(actualbox.Document.ContentStart, actualbox.Document.ContentEnd).Text = members.actual_district;
                    seriabox.Text = members.passport_series;
                    nombox.Text = members.passport_number;
                    new TextRange(vidanbox.Document.ContentStart, vidanbox.Document.ContentEnd).Text = members.passport_issued_by;
                    datavidachibox.Text = members.passport_issued_date;
                    podrazdelbox.Text = members.passport_division_code;
                    gosuslbox.IsChecked = members.gosuslugi_registration_availability;
                    innbox.Text = members.inn;
                    snilsbox.Text = members.snils;
                    nompensbox.Text = members.pension_certificate_number;
                    vtekbox.Text = members.vtek_number;
                }
            }
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {if (e.ChangedButton == MouseButton.Left)
                this.DragMove(); }
        private void closebutton_Click(object sender, RoutedEventArgs e)
        { Close(); }
        private void addbutton_Click(object sender, RoutedEventArgs e)
        {
            if (UserControlMembers.edit == false)
            {
                MessageBoxResult ds = MessageBox.Show("Добавить?", "Подтверждение добавления", MessageBoxButton.YesNo);
                switch (ds)
                {
                    case MessageBoxResult.Yes:
                        try
                        {
                            using (var db = new App.FenContext())
                            {
                                App.Member members = new App.Member()
                                {
                                    registration_number = regbox.Text,
                                    first_name = namebox.Text,
                                    middle_name = fambox.Text,
                                    last_name = otchbox.Text,
                                    age = DateTime.Now.Year - bdaybox.SelectedDate.Value.Year,
                                    gender = genderbox.Text,
                                    bday = bdaybox.SelectedDate.Value.ToShortDateString(),
                                    phone_number = telbox.Text,
                                    disability_group = int.Parse(gruppabox.Text),
                                    category_id = categor[categorybox.SelectedIndex].id,
                                    status = statusbox.Text,
                                    entry_date = inbox.SelectedDate.Value.ToShortDateString(),
                                    residence_street = streetbox.Text,
                                    residence_apartment_number = kvbox.Text,
                                    residence_house_number = homebox.Text,
                                    residence_locality = localitybox.Text,
                                    residence_district = districtbox.Text,
                                    residence_region = regionbox.Text,
                                    residence_postal_code = indexbox.Text,
                                    actual_district = new TextRange(actualbox.Document.ContentStart, actualbox.Document.ContentEnd).Text,
                                    passport_series = seriabox.Text,
                                    passport_number = nombox.Text,
                                    passport_issued_by = new TextRange(vidanbox.Document.ContentStart, vidanbox.Document.ContentEnd).Text,
                                    passport_issued_date = datavidachibox.Text,
                                    passport_division_code = podrazdelbox.Text,
                                    gosuslugi_registration_availability = gosuslbox.IsChecked.Value,
                                    inn = innbox.Text,
                                    snils = snilsbox.Text,
                                    pension_certificate_number = nompensbox.Text,
                                    vtek_number = vtekbox.Text
                                };
                                db.members.Add(members);
                                db.SaveChanges();
                                Close();
                            }
                        }
                        catch (Exception exp) { MessageBox.Show("Не удалось добавить в список: " + exp.Message, "Ошибка"); }
                        break;
                    case MessageBoxResult.No:
                        break;
                    default:
                        break;
                }
            }else if(UserControlMembers.edit == true)
            {
                MessageBoxResult ds = MessageBox.Show("Изменить?", "Подтверждение изменения", MessageBoxButton.YesNo);
                switch (ds)
                {
                    case MessageBoxResult.Yes:
                        try
                        {
                            using (var db = new App.FenContext())
                            {
                                var memb = db.members.Where(x => x.id == UserControlMembers.idmembers).FirstOrDefault();

                                memb.registration_number = regbox.Text;
                                memb.first_name = namebox.Text;
                                memb.middle_name = fambox.Text;
                                memb.last_name = otchbox.Text;
                                memb.age = DateTime.Now.Year - bdaybox.SelectedDate.Value.Year;
                                memb.gender = genderbox.Text;
                                memb.bday = bdaybox.SelectedDate.Value.ToShortDateString();
                                memb.phone_number = telbox.Text;
                                memb.disability_group = int.Parse(gruppabox.Text);
                                memb.category_id = categor[categorybox.SelectedIndex].id;
                                memb.status = statusbox.Text;
                                memb.entry_date = inbox.SelectedDate.Value.ToShortDateString();
                                memb.residence_street = streetbox.Text;
                                memb.residence_apartment_number = kvbox.Text;
                                memb.residence_house_number = homebox.Text;
                                memb.residence_locality = localitybox.Text;
                                memb.residence_district = districtbox.Text;
                                memb.residence_region = regionbox.Text;
                                memb.residence_postal_code = indexbox.Text;
                                memb.actual_district = new TextRange(actualbox.Document.ContentStart, actualbox.Document.ContentEnd).Text;
                                memb.passport_series = seriabox.Text;
                                memb.passport_number = nombox.Text;
                                memb.passport_issued_by = new TextRange(vidanbox.Document.ContentStart, vidanbox.Document.ContentEnd).Text;
                                memb.passport_issued_date = datavidachibox.Text;
                                memb.passport_division_code = podrazdelbox.Text;
                                memb.gosuslugi_registration_availability = gosuslbox.IsChecked.Value;
                                memb.inn = innbox.Text;
                                memb.snils = snilsbox.Text;
                                memb.pension_certificate_number = nompensbox.Text;
                                memb.vtek_number = vtekbox.Text;
                                db.SaveChanges();
                                Close();
                            }
                        }
                        catch (Exception exp) { MessageBox.Show("Не удалось добавить в список: " + exp.Message, "Ошибка"); }
                        break;
                    case MessageBoxResult.No:
                        break;
                    default:
                        break;
                }
            }
       }
    }
}
