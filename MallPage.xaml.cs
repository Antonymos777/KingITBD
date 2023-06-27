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

namespace KursBD
{
    /// <summary>
    /// Логика взаимодействия для MallPage.xaml
    /// </summary>
    public partial class MallPage : Page
    {
        public MallPage()
        {
            InitializeComponent();
            var MallsList = KursEntities.GetContext().Malls.ToList();
            DGridMalls.ItemsSource = MallsList;

            var StatusesList = KursEntities.GetContext().MallStatuses.ToList();
            StatusesList.Insert(0, new MallStatuses { MallStatus = "Все статусы" });

            StatusesComboBox.ItemsSource = StatusesList;
            StatusesComboBox.SelectedIndex = 0;

            var CitiesList = KursEntities.GetContext().Cities.ToList();
            CitiesList.Insert(0, new Cities { CityName = "Все города" });

            CitiesComboBox.ItemsSource = CitiesList;
            CitiesComboBox.SelectedIndex = 0;
        }
        private void StatusesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentMalls = KursEntities.GetContext().Malls.ToList();

            if (StatusesComboBox.SelectedIndex > 0)
            {
                var selectedStatus = StatusesComboBox.SelectedItem as MallStatuses;
                currentMalls = currentMalls.Where(m => m.MallStatuses.MallStatus.ToLower().Contains(selectedStatus.MallStatus.ToLower())).ToList();
            }
            DGridMalls.ItemsSource = currentMalls;
        }

        private void CitiesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentMalls = KursEntities.GetContext().Malls.ToList();

            if (CitiesComboBox.SelectedIndex > 0)
            {
                var selectedCities = CitiesComboBox.SelectedItem as Cities;
                currentMalls = currentMalls.Where(m => m.Cities.CityName.ToLower().Contains(selectedCities.CityName.ToLower())).ToList();
            }
            DGridMalls.ItemsSource = currentMalls;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddMallPage(null));
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var MallsToDelete = DGridMalls.SelectedItems.Cast<Malls>().ToList();
            KursEntities.GetContext().Malls.RemoveRange(MallsToDelete);
            try
            {
                KursEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные удалены");
                DGridMalls.ItemsSource = KursEntities.GetContext().Malls.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddMallPage((sender as Button).DataContext as Malls));
        }
    }
}
