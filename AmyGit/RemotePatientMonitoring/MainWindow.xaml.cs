using System.Windows;
using System;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;

namespace RecipeShare
{
    public partial class MainWindow : Window
    {
        private SQLiteConnection sqliteConnection;

        public MainWindow()
        {
            InitializeComponent();
            InitializeDatabase();
        }

        // Initialize SQLite database
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
        }

        // Login button click event handler
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            string encryptedPassword = EncryptPassword(password);

            if (VerifyLogin(username, encryptedPassword))
            {
                MainPage mainPage = new MainPage();
                mainPage.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Create User button click event handler
        private void CreateUserButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Username and Password cannot be empty", "Registration Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string encryptedPassword = EncryptPassword(password);

            if (CreateUserInDatabase(username, encryptedPassword))
            {
                MessageBox.Show("User created successfully!", "Registration Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                UsernameTextBox.Clear();
                PasswordBox.Clear();
            }
            else
            {
                MessageBox.Show("Username already exists. Please choose a different one.", "Registration Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Encrypt the password using SHA256
        private string EncryptPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        // Verify the username and encrypted password from the database
        private bool VerifyLogin(string username, string encryptedPassword)
        {
            string query = "SELECT COUNT(1) FROM Users WHERE Username = @username AND Password = @password";
            using (SQLiteCommand command = new SQLiteCommand(query, sqliteConnection))
            {
                command.Parameters.AddWithValue("@username", username);
                command.Parameters.AddWithValue("@password", encryptedPassword);
                int count = Convert.ToInt32(command.ExecuteScalar());
                return count == 1;
            }
        }

        // Create a new user in the database
        private bool CreateUserInDatabase(string username, string encryptedPassword)
        {
            string insertQuery = "INSERT INTO Users (Username, Password) VALUES (@username, @password)";
            try
            {
                using (SQLiteCommand command = new SQLiteCommand(insertQuery, sqliteConnection))
                {
                    command.Parameters.AddWithValue("@username", username);
                    command.Parameters.AddWithValue("@password", encryptedPassword);
                    command.ExecuteNonQuery();
                }
                return true;
            }
            catch (SQLiteException ex)
            {
                // Handle exception if username already exists
                if (ex.ResultCode == SQLiteErrorCode.Constraint)
                {
                    return false; // Username already exists
                }
                throw; // Rethrow other exceptions
            }
        }

        // Dispose connection when the window closes
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