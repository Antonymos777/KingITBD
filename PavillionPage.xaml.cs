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
    /// Логика взаимодействия для PavillionPage.xaml
    /// </summary>
    public partial class PavillionPage : Page
    {
        public PavillionPage()
        {
            InitializeComponent();
            DGridPavilions.ItemsSource = KursEntities.GetContext().Pavilions.ToList();
            var MallsList = KursEntities.GetContext().Malls.ToList();
            DGridPavilions.ItemsSource = MallsList;

            var MallsNames = KursEntities.GetContext().Malls.ToList();
            MallsNames.Insert(0, new Malls { MallName = "Все ТЦ" });

            MallsComboBox.ItemsSource = MallsNames;
            MallsComboBox.SelectedIndex = 0;
        }
        private void MallsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MallsComboBox.ItemsSource = KursEntities.GetContext().Malls.ToList();
            var currentPavilions = KursEntities.GetContext().Pavilions.ToList();
            if (MallsComboBox.SelectedIndex > 0)
            {
                var selectedMall = MallsComboBox.SelectedItem as Malls;
                currentPavilions = currentPavilions.Where(m => m.Malls.MallName.ToLower().Contains(selectedMall.MallName.ToLower())).ToList();
            }
            DGridPavilions.ItemsSource = currentPavilions;
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var PavilionsToDelete = DGridPavilions.SelectedItems.Cast<Pavilions>().ToList();
            KursEntities.GetContext().Pavilions.RemoveRange(PavilionsToDelete);
            try
            {
                KursEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные удалены");
                DGridPavilions.ItemsSource = KursEntities.GetContext().Pavilions.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPavillionPage(null));
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddPavillionPage((sender as Button).DataContext as Pavilions));
        }
    }
}
