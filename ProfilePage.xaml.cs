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

namespace RecipeShare
{
    public partial class ProfilePage : Window
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        private void GoToHomePage_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            mainPage.Show();
            this.Close();
        }

        private void SavePreferences_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Preferences saved");
        }
    }
}
