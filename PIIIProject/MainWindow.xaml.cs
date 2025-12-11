using PIIIProject.Models;
using PIIIProject.Views;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PIIIProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sends the user to the load profile window
        /// </summary>
        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadProfileWindow window = new LoadProfileWindow();

            window.Show();
            this.Close();
        }

        /// <summary>
        /// Creates a new profile with the name the user chose.
        /// </summary>
        private void BtnNewProfile_Click(object sender, RoutedEventArgs e)
        {
            List<string> fileInDirectory = Directory.EnumerateFiles("../../../Profiles").ToList();

            if (string.IsNullOrWhiteSpace(profileName.Text))
            {
                PromptWindow promptWindow = new PromptWindow("OK", "", "Profile must have a name.", "Missing Data.");
                promptWindow.ShowDialog();
            }
            else if (File.Exists($"../../../Profiles/{profileName.Text}.csv"))
            {
                PromptWindow promptWindow = new PromptWindow("OK", "", "This profile already exists. Please try another name", "Existing profile found!");
                promptWindow.ShowDialog();

            }
            else if(fileInDirectory.Count == 8)
            {
                PromptWindow promptWindow = new PromptWindow("OK", "", "Maximum limit of profiles reached. Please delete profiles to continue.", "Limit reached.");
                promptWindow.ShowDialog();
            }
            else
            {
                MainMenu menu = new MainMenu(profileName.Text);

                File.Create($"../../../Profiles/{profileName.Text}.csv");

                menu.Show();
                this.Close();
            }
        }
    }
}