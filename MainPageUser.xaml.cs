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

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(SearchTextBox.Text))
            {
                PlaceholderText.Visibility = Visibility.Visible;
            }
            else
            {
                PlaceholderText.Visibility = Visibility.Collapsed;
            }
        }


        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string keyword = SearchTextBox.Text.ToLower();


            var recipes = new[]
            {
                "Vegetarian Pizza",
                "Grilled Meat Steak",
                "Cupcakes",
                "Spaghetti Carbonara",
                "Macarons",
                "Pumpkin Spice Cake",
                "Vegan Tacos",
                "Thai Green Curry"
            };


            var searchResults = recipes.Where(r => r.ToLower().Contains(keyword)).ToList();

            if (searchResults.Count > 0)
            {
                MessageBox.Show($"Found Recipes:\n{string.Join("\n", searchResults)}", "Search Results");
            }
            else
            {
                MessageBox.Show("No recipes found. Try another keyword.", "Search Results");
            }

            SearchTextBox.Text = string.Empty;
        }

        private void LoadCommunityFeed()
        {
            CommunityFeedList.Items.Add("New: Pumpkin Spice Cake 🎃");
            CommunityFeedList.Items.Add("Popular: Vegan Tacos 🌮");
            CommunityFeedList.Items.Add("Trending: Thai Green Curry 🍛");
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
            RecipeUploadPage uploadPage = new RecipeUploadPage();
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
