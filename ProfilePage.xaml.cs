using System;
using System.Collections.Generic;
using System.Data.SQLite;
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
        private SQLiteConnection dbConnection;

        public ProfilePage()
        {
            InitializeComponent();
            InitializeDatabase();
            LoadPreferences();
        }

        private void InitializeDatabase()
        {
            dbConnection = new SQLiteConnection("Data Source=preferences.db;Version=3;");
            dbConnection.Open();

            string createTableQuery = @"CREATE TABLE IF NOT EXISTS Preferences (
                                        Id INTEGER PRIMARY KEY,
                                        Vegetarian BOOLEAN,
                                        Vegan BOOLEAN,
                                        GlutenFree BOOLEAN,
                                        DairyFree BOOLEAN,
                                        NutFree BOOLEAN)";
            SQLiteCommand command = new SQLiteCommand(createTableQuery, dbConnection);
            command.ExecuteNonQuery();
        }

        private void LoadPreferences()
        {
            string selectQuery = @"SELECT Vegetarian, Vegan, GlutenFree, DairyFree, NutFree FROM Preferences WHERE Id = 1";
            SQLiteCommand command = new SQLiteCommand(selectQuery, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                VegetarianCheckBox.IsChecked = reader.GetBoolean(0);
                VeganCheckBox.IsChecked = reader.GetBoolean(1);
                GlutenFreeCheckBox.IsChecked = reader.GetBoolean(2);
                DairyFreeCheckBox.IsChecked = reader.GetBoolean(3);
                NutFreeCheckBox.IsChecked = reader.GetBoolean(4);
            }

            reader.Close();
        }

        private void GoToHomePage_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            mainPage.Show();
            this.Close();
        }

        private void SavePreferences_Click(object sender, RoutedEventArgs e)
        {
            bool vegetarian = (bool)VegetarianCheckBox.IsChecked;
            bool vegan = (bool)VeganCheckBox.IsChecked;
            bool glutenFree = (bool)GlutenFreeCheckBox.IsChecked;
            bool dairyFree = (bool)DairyFreeCheckBox.IsChecked;
            bool nutFree = (bool)NutFreeCheckBox.IsChecked;

            string insertQuery = @"INSERT OR REPLACE INTO Preferences (Id, Vegetarian, Vegan, GlutenFree, DairyFree, NutFree) 
                                   VALUES (1, @Vegetarian, @Vegan, @GlutenFree, @DairyFree, @NutFree)";
            SQLiteCommand command = new SQLiteCommand(insertQuery, dbConnection);
            command.Parameters.AddWithValue("@Vegetarian", vegetarian);
            command.Parameters.AddWithValue("@Vegan", vegan);
            command.Parameters.AddWithValue("@GlutenFree", glutenFree);
            command.Parameters.AddWithValue("@DairyFree", dairyFree);
            command.Parameters.AddWithValue("@NutFree", nutFree);

            command.ExecuteNonQuery();

            MessageBox.Show("Preferences saved");
        }
    }
}
