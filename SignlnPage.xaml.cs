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
    public partial class SignlnPage : Page
    {
        
        public SignlnPage()
        {
            InitializeComponent();
        }
        
        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            string login = loginTextBox.Text;
            string password = "";

            if (passwordTextBox.Visibility == Visibility.Visible)
            {
                password = passwordTextBox.Password;
            }
            else
            {
                password = passwordShowTextBox.Text;
            }

            var CurrentUser = KursEntities.GetContext().Users.FirstOrDefault(a => a.Login.ToLower() == login.ToLower() && a.Password == password);

          
            if (CurrentUser != null)
            {
                MessageBox.Show($"Вы вошли как {CurrentUser.Roles.RoleName}");

                NavigationService.Navigate(new ManagerCPage());
            }
            if (CurrentUser == null)
            {
                MessageBox.Show("Пользователь не существует");
            }
        }
        private void CheckBox_Show(object sender, RoutedEventArgs e)
        {
            passwordShowTextBox.Text = passwordTextBox.Password;
            passwordTextBox.Visibility = Visibility.Collapsed;
            passwordShowTextBox.Visibility = Visibility.Visible;
        }

        private void CheckBox_Hide(object sender, RoutedEventArgs e)
        {
            passwordTextBox.Password = passwordShowTextBox.Text;
            passwordShowTextBox.Visibility = Visibility.Collapsed;
            passwordTextBox.Visibility = Visibility.Visible;
        }
    }
}
