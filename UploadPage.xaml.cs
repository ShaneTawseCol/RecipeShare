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
    /// Interaction logic for RecipeUploadPage.xaml
    /// </summary>
    public partial class RecipeUploadPage : Window
    {
        public UploadPage()
        {
            InitializeComponent();
        }

        public void btnUpload_Click(object sender, RoutedEventArgs e)
    {
    string filename = FileNameTextBox.Text;
    if (File.Exists(filename))
    {
        // TODO: Show an error message box to user indicating destination file already uploaded
        return;
    }

    string name = Path.GetFileName(filename);
    string destinationFilename = Path.Combine("C:\\temp\\uploaded files\\", name); 

    File.Copy(filename, destinationFilename);

    // TODO: Show information or message box indicating file has copied
    }
    }
}
