using System;
using System.Configuration;
using System.Windows;

namespace RecipeShare
{
    public partial class MainPage : Window
    {
        public MainPage()
        {
            InitializeComponent();
        }


        private void MainPageUser_Click(object sender, RoutedEventArgs e)
        {
            MainPageUser mainPageUser = new MainPageUser();           
            mainPageUser.Show();
            this.Close();// Navigates to Page2
        }



        private void SettingPage_Click(object sender, RoutedEventArgs e)
        {
            SettingPage settings = new SettingPage();
            settings.Show();
            this.Close();// Navigates to Page3
        }

        private void ProfilePage_Click(object sender, RoutedEventArgs e)
        {
            ProfilePage profile = new ProfilePage();
           profile.Show();
            this.Close();// Navigates to Page4
        }

        private void UploadPage_Click(object sender, RoutedEventArgs e)
        {
            UploadPage upload = new UploadPage();
            upload.Show();
            this.Close();// Navigates to Page5
        }

        //private void MyVideo_MediaEnded(object sender, RoutedEventArgs e) { MyVideo.Position = TimeSpan.Zero; MyVideo.Play(); }//Future ADS
    }
}
