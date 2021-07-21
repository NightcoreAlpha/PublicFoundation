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
    /// Логика взаимодействия для UserControlService.xaml
    /// </summary>
    public partial class UserControlService : UserControl
    {
        public static int idservice = 0;
        public static int idmember = 0;
        public UserControlService()
        {
            InitializeComponent();
            UploudMembersForService();
        }
        public void UploudMembersForService()
        {
            using (var db = new App.FenContext())
            {
                //var service = db.services.ToList();
                var service = from elem in db.members
                              select new
                              {
                                  id = elem.id,
                                  regnom = elem.registration_number,
                                  membername = elem.first_name + " " + elem.middle_name + " " + elem.last_name
                              };
                              //join memb in db.members on serv.invalid_id equals memb.id
                gridmemberview.ItemsSource = service.ToList();
            }
        }
        public void UploadService()
        {
            using(var db = new App.FenContext())
            {
                //var service = db.services.ToList();
                var service = from serv in db.services.Where(x=> x.invalid_id == idmember)
                              join memb in db.members on serv.invalid_id equals memb.id
                              select new
                              {
                                  id = serv.id,
                                  data = serv.data,
                                  regnom = memb.registration_number,
                                  invalid_id = memb.first_name + " " + memb.middle_name + " " + memb.last_name
                              };
                gridinvalid.ItemsSource = service.ToList();
            }
        }

        public void UploadServiceLine()
        {
            using (var db = new App.FenContext())
            {
                //var service = db.services.ToList();
                var service = from servline in db.servicelines.Where(x=> x.service_id == idservice)
                              //join serv in db.services on servline.service_id equals idservice
                              //group servline by servline.service_id into 


                              select new
                              {
                                  id = servline.id,
                                  service_id = servline.service_id,
                                  title = servline.title,
                                  description = servline.description,
                                  estimation = servline.estimation,
                                  issued_by = servline.issued_by
                              };
                gridallservise.ItemsSource = service.ToList();
            }
        }

        private void gridinvalid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (gridinvalid.SelectedIndex != -1)
                {
                    idservice = (int)gridinvalid.SelectedItem.GetType().GetProperty("id").GetValue(gridinvalid.SelectedItem, null);

                    //idmembers = idd;
                    //idmembers = idmember.GetType().GetProperty("id").GetValue(idmember,null);

                    //selind = (gridinvalid.SelectedIndex == -1) ? selind = 0 : selind = gridinvalid.SelectedIndex;
                    UploadServiceLine();
                }
                /*if (memberslist.Count != 0)
                {
                    var categor = memberslist[selind];
                    id = categor.id;
                }*/
                
            }
            catch (Exception exp) { MessageBox.Show("Произошла ошибка при выборе услуги :" + exp.Message, "Ошибка"); }

        }

        private void gridmemberview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (gridmemberview.SelectedIndex != -1)
                {
                    idmember = (int)gridmemberview.SelectedItem.GetType().GetProperty("id").GetValue(gridmemberview.SelectedItem, null);

                    //idmembers = idd;
                    //idmembers = idmember.GetType().GetProperty("id").GetValue(idmember,null);

                    //selind = (gridinvalid.SelectedIndex == -1) ? selind = 0 : selind = gridinvalid.SelectedIndex;
                    UploadService();
                    gridallservise.ItemsSource = null;
                }
                /*if (memberslist.Count != 0)
                {
                    var categor = memberslist[selind];
                    id = categor.id;
                }*/

            }
            catch (Exception exp) { MessageBox.Show("Произошла ошибка при выборе объекта :" + exp.Message, "Ошибка"); }

        }

        private void addbutton_Click(object sender, RoutedEventArgs e)
        {
            if (gridmemberview.SelectedIndex != -1)
            {
                AddServiceWindow asw = new AddServiceWindow();
                asw.ShowDialog();
                //UploadService();
            }else { MessageBox.Show("Не выбран объект для добавления", "Ошибка"); }
        }
    }
}
