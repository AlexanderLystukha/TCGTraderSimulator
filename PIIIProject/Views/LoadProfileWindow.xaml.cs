using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PIIIProject.Models;

namespace PIIIProject.Views
{
    /// <summary>
    /// Interaction logic for LoadProfileWindow.xaml
    /// </summary>
    public partial class LoadProfileWindow : Window
    {
        private const string ProfilePath = "../../../Profiles/";


        public LoadProfileWindow()
        {
            InitializeComponent();

            LoadProfileNames();
        }

        /// <summary>
        /// Loads the users content using the Profile class.
        /// </summary>
        private void BtnLoadProfile_Click(object sender, RoutedEventArgs e)
        {
            string fileToLoad = "";

            if (sender is Button b)
            {
                 fileToLoad = $"{ProfilePath}{b.Content}";
            }


            if (System.IO.Path.GetFileNameWithoutExtension(fileToLoad) != "Empty")
            {
                Profile.LoadContent(fileToLoad);

                MainMenu mainMenu = new MainMenu(fileToLoad);
                mainMenu.Show();
                this.Close();
            }
            else
            {
                PromptWindow promptWindow = new PromptWindow("OK", "", "No profile created in this slot.", "Empty Profile.");
                promptWindow.ShowDialog();
            }
               
        }

        /// <summary>
        /// Loads the profile names unto the buttons.
        /// </summary>
        private void LoadProfileNames()
        {
            //loads the profile names onto the buttons
            List<string> profiles = new List<string>();

            foreach (string file in Directory.EnumerateFiles(ProfilePath))
            {
                string filename = System.IO.Path.GetFileName(file);
                profiles.Add(System.IO.Path.GetFileNameWithoutExtension(filename));
            }

            for (int i = 0; i < profiles1.Children.Count; i++)
            {
                if (profiles1.Children[i] is Button b && i < profiles.Count)
                    b.Content = profiles[i];
            }

            if (profiles.Count > profiles1.Children.Count)
            {
                for (int i = 4; i < profiles.Count; i++)
                {
                    if (profiles2.Children[i - 4] is Button b && i < profiles.Count)
                        b.Content = profiles[i];
                }
            }
        }

        /// <summary>
        /// Resets the buttons text back to Empty.
        /// </summary>
        private void ResetProfileNames()
        {
            for (int i = 0; i < profiles1.Children.Count; i++)
            {
                if (profiles1.Children[i] is Button b)
                    b.Content = "Empty";
            }

            for (int i = 0; i < profiles2.Children.Count; i++)
            {
                if (profiles2.Children[i] is Button b)
                    b.Content = "Empty";
            }
        }
        /// <summary>
        /// Sends the user back to the starting window.
        /// </summary>
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();

            mw.Show();
            this.Close();
        }

        /// <summary>
        /// Deletes the profile the user chose.
        /// </summary>
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (File.Exists($"{ProfilePath}{profileToDelete.Text}.csv"))
                {
                    File.Delete($"{ProfilePath}{profileToDelete.Text}.csv");
                }
                else
                {
                    PromptWindow promptWindow = new PromptWindow("OK", "", "Invalid name for profile. Please try again..", "Invalid Data.");
                    promptWindow.ShowDialog();
                }
            }
            catch
            {
                PromptWindow promptWindow = new PromptWindow("OK", "", "This error happens when you try to interact the same profile twice. Please load another profile and try again.", "File Duplication.");
                promptWindow.ShowDialog();
            }

            ResetProfileNames();

            LoadProfileNames();

            profileToDelete.Text = string.Empty;
        }
    }
}
