using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows;

namespace RecipeShare
{
	public partial class RecipeManagement : Window
	{
		private const string DatabaseFilePath = "recipes.db";
		private const string ConnectionString = "Data Source=recipes.db;Version=3;";
		private SQLiteConnection sqliteConnection;

		public RecipeManagement()
		{
			InitializeComponent();
			LoadRecipes();
		}

		private void LoadRecipes()
		{
			try
			{
				if (!System.IO.File.Exists(DatabaseFilePath))
				{
					MessageBox.Show("Database file not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
					return;
				}

				sqliteConnection = new SQLiteConnection(ConnectionString);
				sqliteConnection.Open();

				string query = "SELECT Name FROM Recipes";
				List<string> recipes = new List<string>();

				using (SQLiteCommand command = new SQLiteCommand(query, sqliteConnection))
				using (SQLiteDataReader reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						recipes.Add(reader["Name"].ToString());
					}
				}

				RecipeListBox.ItemsSource = recipes;
			}
			catch (SQLiteException ex)
			{
				MessageBox.Show($"Error loading recipes: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
			finally
			{
				if (sqliteConnection != null)
				{
					sqliteConnection.Close();
				}
			}
		}

		private void BackButton_Click(object sender, RoutedEventArgs e)
		{
			UserAccess userAccess = new UserAccess();
			userAccess.Show();
			this.Close();
		}

		private void LoginButton_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mainWindow = new MainWindow();
			mainWindow.Show();
			this.Close();
		}

		private void DeleteRecipeButton_Click(object sender, RoutedEventArgs e)
		{
			if (RecipeListBox.SelectedItem != null)
			{
				string selectedRecipe = RecipeListBox.SelectedItem.ToString();
				try
				{
					sqliteConnection = new SQLiteConnection(ConnectionString);
					sqliteConnection.Open();

					string deleteQuery = "DELETE FROM Recipes WHERE Name = @Name";
					using (SQLiteCommand command = new SQLiteCommand(deleteQuery, sqliteConnection))
					{
						command.Parameters.AddWithValue("@Name", selectedRecipe);
						command.ExecuteNonQuery();
					}

					LoadRecipes();
				}
				catch (SQLiteException ex)
				{
					MessageBox.Show($"Error deleting recipe: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				}
				finally
				{
					if (sqliteConnection != null)
					{
						sqliteConnection.Close();
					}
				}
			}
			else
			{
				MessageBox.Show("Please select a recipe to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
			}
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
