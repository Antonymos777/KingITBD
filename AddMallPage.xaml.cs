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
    /// Логика взаимодействия для AddMallPage.xaml
    /// </summary>
    public partial class AddMallPage : Page
    {
       
            private Malls _currentMall = new Malls();

            public AddMallPage(Malls selectedMall)
            {
                InitializeComponent();

                StatusesComboBox.ItemsSource = KursEntities.GetContext().MallStatuses.ToList();
                CitiesComboBox.ItemsSource = KursEntities.GetContext().Cities.ToList();

                if (selectedMall != null)
                {
                    _currentMall = selectedMall;
                }

                DataContext = _currentMall;
            }

            private void SaveButton_Click(object sender, RoutedEventArgs e)
            {
                if (_currentMall.MallId == 0)
                {
                    KursEntities.GetContext().Malls.Add(_currentMall);
                }
                try
                {
                    KursEntities.GetContext().SaveChanges();
                    MessageBox.Show("Информация сохранена");
                    NavigationService.GoBack();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            private void GoBack_Click(object sender, RoutedEventArgs e)
            {
                NavigationService.GoBack();
            }

        private void StatusesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
