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
using Microsoft.Win32;
using PIIIProject.Models;
using static System.Net.Mime.MediaTypeNames;

namespace PIIIProject.Views
{
    /// <summary>
    /// Interaction logic for CustomCardWindow.xaml
    /// </summary>
    public partial class CustomCardWindow : Window
    {
        /// <summary>
        /// The source of the image of the user chose.
        /// </summary>
        private string _cardSource;
        public CustomCardWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sends the user back to the main menu.
        /// </summary>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            PromptWindow promptWdw = new PromptWindow("YES", "NO", "Are you sure you want to leave? The current card being created will be discarded.", "Leaving Creator");
            promptWdw.ShowDialog();
            string result = promptWdw._selectedButton;

            if (result == "YES")
            {
                MainMenu mainMenu = new MainMenu(Profile._pathToSaveTo);
                Profile.SaveContent();
                mainMenu.Show();
                this.Close();

            }
        }
        
        /// <summary>
        /// Checks if all the fields were set and creates the card the user made.
        /// </summary>
        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            const int CharLimit = 15;
            StringBuilder sb = new StringBuilder();
            bool respectsConditions = false;
            RadioButton elementalTypeButton = null;
            RadioButton cardTypeButton = null;

            foreach (UIElement element in rdbContainer.Children)
            {
                if (element is RadioButton r && r.IsChecked == true)
                {
                    respectsConditions = true;
                    elementalTypeButton = r;
                }
            }


            if (!respectsConditions)
            {
                sb.AppendLine("Please select an elemental type.");
            }

            respectsConditions = false;

            foreach (UIElement element in rdbCardType.Children)
            {
                if (element is RadioButton r && r.IsChecked == true)
                {
                    respectsConditions = true;
                    cardTypeButton = r;
                }
            }

            if (!respectsConditions)
            {
                sb.AppendLine("Please select a card type.");
            }

            if (string.IsNullOrWhiteSpace(cardNameTxtBox.Text))
                sb.AppendLine("Please choose a name for the card.");
            else if (cardNameTxtBox.Text.Length > CharLimit)
                sb.AppendLine("Name of card must be less than 15 characters.");


            if (_cardSource == null)
                sb.AppendLine("Please choose an image for the card.");

            if (sb.Length != 0)
            {
                PromptWindow pw = new PromptWindow("OK", "", sb.ToString(), "Error while creating card.");

                pw.ShowDialog();
            }
            else
            {
                Profile._userCards.Add(new Card(cardNameTxtBox.Text, cardTypeButton.Content.ToString(), elementalTypeButton.Content.ToString() , _cardSource, true));

                PromptWindow pw = new PromptWindow("OK", "", "Press OK to continue." , "Card has been successfully created.");

                pw.ShowDialog();

                ResetBtn_Click(sender, e);
            }

        }

        /// <summary>
        /// Opens a file dialog for the user to choose an image for the card.
        /// </summary>
        private void ChooseImageBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image File | *.png;*.jpg";

            if (openFileDialog.ShowDialog() == true)
            {
                Uri uri = new Uri(openFileDialog.FileName, UriKind.Absolute);
                ImageSource imgSource = new BitmapImage(uri);
                cardIMG.Source = imgSource;

                _cardSource = openFileDialog.FileName;
            }
            else
            {
                PromptWindow pw = new PromptWindow("OK", "", "Error loading file. Please try again.", "File Error.");
            }

        }

        /// <summary>
        /// Resets the form for the user.
        /// </summary>
        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (UIElement element in rdbCardType.Children)
            {
                if (element is RadioButton r)
                    r.IsChecked = false;
            }

            cardNameTxtBox.Text = string.Empty;

            foreach (UIElement element in rdbContainer.Children)
            {
                if (element is RadioButton r)
                    r.IsChecked = false;
            }

            cardIMG.Source = null;
        }
    }
}
