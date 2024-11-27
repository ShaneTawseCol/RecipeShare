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
    /// <summary>
    /// Interaction logic for SettingPage.xamla
    /// </summary>
    public partial class SettingPage : Window
    {
        public SettingPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            mainPage.Show();
            this.Close();
        }

        private void btn_SaveSettings_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Settings saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
