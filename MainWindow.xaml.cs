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
        private const string adminPassword = "cats";
        private readonly string encryptedAdminPassword;

        public MainWindow()
        {
            InitializeComponent();
            InitializeDatabase();
            encryptedAdminPassword = EncryptPassword(adminPassword); // Encrypt the admin password on initialization
        }

        private void AdminLoginbtn_Click(object sender, RoutedEventArgs e)
        {
            string inputPassword = AdminPasswordBox.Password; // Get the password from PasswordBox
            string encryptedInputPassword = EncryptPassword(inputPassword);

            if (encryptedInputPassword == encryptedAdminPassword)
            {
                UserAccess userAccess = new UserAccess();
                userAccess.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid admin password", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

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

        // Initialize SQLite database and ensure the UsageTime column is present
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
            string insertQuery = "INSERT INTO Users (Username, Password, UsageTime) VALUES (@username, @password, 0)";
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
