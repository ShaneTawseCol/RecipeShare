using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace RecipeShare
{
    public partial class RecipeUploadPage : Window
    {
        private const string DatabaseFilePath = "recipes.db";
        private const string ConnectionString = "Data Source=recipes.db;Version=3;";

        public RecipeUploadPage()
        {
            InitializeComponent();
            InitializeDatabase();
            LoadRecipesFromDatabase();
        }

        private void InitializeDatabase()
        {
            if (!File.Exists(DatabaseFilePath))
            {
                SQLiteConnection.CreateFile(DatabaseFilePath);
            }

            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string createTableQuery = @"CREATE TABLE IF NOT EXISTS Recipes (
                                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                            Name TEXT NOT NULL,
                                            Content TEXT NOT NULL)";
                using (var command = new SQLiteCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        // Load recipes from database
        private void LoadRecipesFromDatabase()
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string selectQuery = "SELECT Name FROM Recipes";
                using (var command = new SQLiteCommand(selectQuery, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        RecipesComboBox.Items.Add(reader["Name"].ToString());
                    }
                }
            }
        }

        // Save a new recipe to the database
        private void SaveRecipeToDatabase(string name, string content)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string insertQuery = "INSERT INTO Recipes (Name, Content) VALUES (@Name, @Content)";
                using (var command = new SQLiteCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@Content", content);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Get recipe content from the database
        private string GetRecipeContent(string name)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string selectQuery = "SELECT Content FROM Recipes WHERE Name = @Name";
                using (var command = new SQLiteCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader["Content"].ToString();
                        }
                    }
                }
            }
            return null;
        }

        // Remove a recipe from the database
        private void RemoveRecipeFromDatabase(string name)
        {
            using (var connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                string deleteQuery = "DELETE FROM Recipes WHERE Name = @Name";
                using (var command = new SQLiteCommand(deleteQuery, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Upload button click event handler
        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            string filename = FileNameTextBox.Text;

            if (File.Exists(filename))
            {
                string name = Path.GetFileName(filename);
                string destinationFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "uploaded_files", name);

                if (!Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "uploaded_files")))
                {
                    Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "uploaded_files"));
                }

                if (File.Exists(destinationFilename))
                {
                    MessageBox.Show("Destination file already uploaded.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                try
                {
                    File.Copy(filename, destinationFilename);
                    MessageBox.Show("File has been successfully copied.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Source file does not exist.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Save button click event handler
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string recipeName = RecipeNameTextBox.Text;
            string recipeContent = File.ReadAllText(FileNameTextBox.Text);

            if (!string.IsNullOrEmpty(recipeName) && !RecipesComboBox.Items.Contains(recipeName) && !string.IsNullOrEmpty(recipeContent))
            {
                SaveRecipeToDatabase(recipeName, recipeContent);
                RecipesComboBox.Items.Add(recipeName);
                MessageBox.Show("Recipe saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Recipe name already exists, is empty, or the content could not be read.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ComboBox selection changed event handler
        private void RecipesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RecipesComboBox.SelectedItem != null)
            {
                string selectedRecipeName = RecipesComboBox.SelectedItem.ToString();
                string recipeContent = GetRecipeContent(selectedRecipeName);
                if (!string.IsNullOrEmpty(recipeContent))
                {
                    FileNameTextBox.Text = recipeContent;
                }
            }
        }

        // Remove button click event handler
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (RecipesComboBox.SelectedItem != null)
            {
                string selectedRecipeName = RecipesComboBox.SelectedItem.ToString();
                RemoveRecipeFromDatabase(selectedRecipeName);
                RecipesComboBox.Items.Remove(selectedRecipeName);
                MessageBox.Show("Recipe removed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No recipe selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Navigate to Main Page
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainPage mainPage = new MainPage();
            mainPage.Show();
            this.Close();
        }

        // View button click event handler
        private void ViewRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (RecipesComboBox.SelectedItem != null)
            {
                string selectedRecipeName = RecipesComboBox.SelectedItem.ToString();
                string recipeContent = GetRecipeContent(selectedRecipeName);
                if (!string.IsNullOrEmpty(recipeContent))
                {
                    RecipeViewWindow viewWindow = new RecipeViewWindow
                    {
                        Owner = this
                    };
                    viewWindow.RecipeContentTextBox.Text = recipeContent;
                    viewWindow.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Failed to load the recipe content.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("No recipe selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
