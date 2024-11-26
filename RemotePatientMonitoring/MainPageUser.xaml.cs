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
    /// Interaction logic for MainPageUser.xaml
    /// </summary>
    public partial class MainPageUser : Window
    {
        public MainPageUser()
        {
            InitializeComponent();
            LoadCommunityFeed();
        }

        private void LoadCommunityFeed()
        {
            CommunityFeedList.Items.Add("New: Pumpkin Spice Cake 🎃");
            CommunityFeedList.Items.Add("Popular: Vegan Tacos 🌮");
            CommunityFeedList.Items.Add("Trending: Thai Green Curry 🍛");
        }

        private void ViewFeaturedRecipe_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Viewing full recipe for Spaghetti Carbonara");
        }

        private void GoToProfile_Click(object sender, RoutedEventArgs e)
        {
            ProfilePage profilePage = new ProfilePage();
            profilePage.Show();
            this.Close();
        }

        private void GoToSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingPage settingsPage = new SettingPage();
            settingsPage.Show();
            this.Close();
        }

        private void GoToUpload_Click(object sender, RoutedEventArgs e)
        {
            UploadPage uploadPage = new UploadPage();
            uploadPage.Show();
            this.Close();
        }

        private void GoToMainPage_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            mainPage.Show();
            this.Close();
        }

    }
}
