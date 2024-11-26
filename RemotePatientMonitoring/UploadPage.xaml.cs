using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace RecipeShare
{
    /// <summary>
    /// Interaction logic for RecipeUploadPage.xaml
    /// </summary>
    public partial class RecipeUploadPage : Window
    {
        public RecipeUploadPage()
        {
            InitializeComponent();
        }

        public void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            string filename = FileNameTextBox.Text;
            
                if (File.Exists(filename))
            {
                string name = Path.GetFileName(filename);
                string destinationFilename = Path.Combine("C:\\temp\\uploaded files\\", name);

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
    }
}
