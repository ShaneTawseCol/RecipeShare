using System.Windows;
using System.Data.SQLite;
using System;

namespace RecipeShare
{
    /// <summary>
    /// Interaction logic for SettingPage.xaml
    /// </summary>
    public partial class SettingPage : Window
    {
        private SQLiteConnection sqliteConnection;

        public SettingPage()
        {
            InitializeComponent();
            InitializeDatabase();
            LoadSettings();
        }

        // Initialize SQLite database for settings
        private void InitializeDatabase()
        {
            string dbPath = "Data Source=settingsDB.db;Version=3;";
            sqliteConnection = new SQLiteConnection(dbPath);
            sqliteConnection.Open();

            string createSettingsTableQuery = @"CREATE TABLE IF NOT EXISTS Settings (
                                                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                                EmailNotifications BOOLEAN,
                                                TextNotifications BOOLEAN,
                                                DeviceNotifications BOOLEAN)";
            using (SQLiteCommand command = new SQLiteCommand(createSettingsTableQuery, sqliteConnection))
            {
                command.ExecuteNonQuery();
            }
        }

        // Load settings from the database
        private void LoadSettings()
        {
            string query = "SELECT EmailNotifications, TextNotifications, DeviceNotifications FROM Settings WHERE Id = 1";
            using (SQLiteCommand command = new SQLiteCommand(query, sqliteConnection))
            {
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        EmailRBOn.IsChecked = reader.GetBoolean(0);
                        EmailRBOff.IsChecked = !reader.GetBoolean(0);
                        TextRbOn.IsChecked = reader.GetBoolean(1);
                        TextRbOff.IsChecked = !reader.GetBoolean(1);
                        DeviceRbOn.IsChecked = reader.GetBoolean(2);
                        DevicebBOff.IsChecked = !reader.GetBoolean(2);
                    }
                }
            }
        }

        // Save settings to the database
        private void btn_SaveSettings_Click(object sender, RoutedEventArgs e)
        {
            bool emailNotifications = EmailRBOn.IsChecked == true;
            bool textNotifications = TextRbOn.IsChecked == true;
            bool deviceNotifications = DeviceRbOn.IsChecked == true;

            string query = @"INSERT OR REPLACE INTO Settings (Id, EmailNotifications, TextNotifications, DeviceNotifications)
                             VALUES (1, @EmailNotifications, @TextNotifications, @DeviceNotifications)";
            using (SQLiteCommand command = new SQLiteCommand(query, sqliteConnection))
            {
                command.Parameters.AddWithValue("@EmailNotifications", emailNotifications);
                command.Parameters.AddWithValue("@TextNotifications", textNotifications);
                command.Parameters.AddWithValue("@DeviceNotifications", deviceNotifications);
                command.ExecuteNonQuery();
            }
            MessageBox.Show("Settings saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Navigate to Main Page
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            mainPage.Show();
            this.Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (sqliteConnection != null)
            {
                sqliteConnection.Close();
            }
        }
    }
}
