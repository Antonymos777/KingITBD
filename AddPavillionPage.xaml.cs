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
    /// Логика взаимодействия для AddPavillionPage.xaml
    /// </summary>
    public partial class AddPavillionPage : Page
    {
        private Pavilions _currentPavilion = new Pavilions();
        private bool _isCreate = true;

        public AddPavillionPage(Pavilions selectedPavilion)
        {
            InitializeComponent();
            MallsComboBox.ItemsSource = KursEntities.GetContext().Malls.ToList();
            PavilionStatusesComboBox.ItemsSource = KursEntities.GetContext().PavilionStatuses.ToList();

            if (selectedPavilion != null)
            {
                _currentPavilion = selectedPavilion;
                _isCreate = false;
            }
            DataContext = _currentPavilion;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_isCreate == true)
            {
                KursEntities.GetContext().Pavilions.Add(_currentPavilion);
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
        private void PavilionStatusesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
