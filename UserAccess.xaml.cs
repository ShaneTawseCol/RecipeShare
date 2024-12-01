using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;
using System.Windows.Controls;

namespace RecipeShare
{
    public partial class UserAccess : Window
    {
        private SQLiteConnection sqliteConnection;

        public UserAccess()
        {
            InitializeComponent();
            InitializeDatabase();
            LoadUserDetails();
        }

        private void MainPageButton_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            mainPage.Show();
            this.Close();
        }

        private void RecipeManagementButton_Click(object sender, RoutedEventArgs e)
        {
            RecipeManagement  recipeManagement = new RecipeManagement();
            recipeManagement.Show();
            this.Close();
        }

        private void UserExperienceButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                switch (radioButton.Margin.Top)
                {
                    case 154:
                        CheckBox1.IsChecked = false;
                        break;
                    case 174:
                        CheckBox2.IsChecked = false;
                        break;
                    case 194:
                        CheckBox3.IsChecked = false;
                        break;
                    case 214:
                        CheckBox4.IsChecked = false;
                        break;
                    case 234:
                        CheckBox5.IsChecked = false;
                        break;
                }
            }
        }

        private void LoadUserDetails()
        {
            try
            {
                string dbPath = "Data Source=recipeShareDB.db;Version=3;";
                sqliteConnection = new SQLiteConnection(dbPath);
                sqliteConnection.Open();

                string query = "SELECT Username, UsageTime FROM Users";
                List<string> userNames = new List<string>();
                List<int> usageTimes = new List<int>();

                using (SQLiteCommand command = new SQLiteCommand(query, sqliteConnection))
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        userNames.Add(reader["Username"].ToString());
                        usageTimes.Add(Convert.ToInt32(reader["UsageTime"]));
                    }
                }

                UserList.ItemsSource = userNames;
                UserMinutes.ItemsSource = usageTimes;
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show($"Error loading user details: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (sqliteConnection != null)
                {
                    sqliteConnection.Close();
                }
            }
        }

        private void InitializeDatabase()
        {
            string dbPath = "Data Source=recipeShareDB.db;Version=3;";
            sqliteConnection = new SQLiteConnection(dbPath);

            sqliteConnection.Open();

            string createTableQuery = @"CREATE TABLE IF NOT EXISTS Users (
                                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                        Username TEXT NOT NULL UNIQUE,
                                        Password TEXT NOT NULL)";
            using (SQLiteCommand command = new SQLiteCommand(createTableQuery, sqliteConnection))
            {
                command.ExecuteNonQuery();
            }

            // Add the UsageTime column if it doesn't exist
            string addColumnQuery = @"ALTER TABLE Users ADD COLUMN UsageTime INTEGER DEFAULT 0";
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(addColumnQuery, sqliteConnection))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (SQLiteException ex)
            {
                // Check if the error is because the column already exists
                if (ex.Message.Contains("duplicate column name"))
                {
                    // Column already exists, no further action needed
                }
                else
                {
                    // Handle other exceptions
                    throw;
                }
            }
        }
    }
}
