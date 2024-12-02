using System.Windows;
using System.Windows.Media.Animation;

namespace RecipeShare
{
    public partial class MainPage : Window
    {
        public MainPage()
        {
            InitializeComponent();
            StartImageCarousel();
        }

        private void StartImageCarousel()
        {
            Storyboard storyboard = (Storyboard)this.FindResource("ImageAnimation");
            storyboard.Begin();
        }

        private void MainPageUser_Click(object sender, RoutedEventArgs e)
        {
            MainPageUser mainPageUser = new MainPageUser();
            mainPageUser.Show();
            this.Close(); // Navigates to MainPageUser
        }

        private void SettingPage_Click(object sender, RoutedEventArgs e)
        {
            SettingPage settings = new SettingPage();
            settings.Show();
            this.Close(); // Navigates to SettingPage
        }

        private void ProfilePage_Click(object sender, RoutedEventArgs e)
        {
            ProfilePage profile = new ProfilePage();
            profile.Show();
            this.Close(); // Navigates to ProfilePage
        }

        private void UploadPage_Click(object sender, RoutedEventArgs e)
        {
            RecipeUploadPage upload = new RecipeUploadPage();
            upload.Show();
            this.Close(); // Navigates to UploadPage
        }
    }
}
