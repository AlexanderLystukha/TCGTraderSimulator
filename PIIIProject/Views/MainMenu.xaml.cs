using Microsoft.Win32;
using PIIIProject.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace PIIIProject.Views
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        public MainMenu(string profileName)
        {
            InitializeComponent();
            Profile._pathToSaveTo = $"../../../Profiles/{System.IO.Path.GetFileNameWithoutExtension(profileName)}.csv";

            profileName = System.IO.Path.GetFileNameWithoutExtension(profileName);


            playerName.Text = profileName;

            balanceCount.Text = Math.Round(Profile._moneyCount, 2).ToString();

            DisplayBestCard();
        }

        /// <summary>
        /// Sends the user to the market.
        /// </summary>
        private void MarketBtn_Click(object sender, RoutedEventArgs e)
        {
            MarketWindow marketWindow = new MarketWindow();

            marketWindow.Show();
            this.Close();
        }

        /// <summary>
        /// Sends the user back to the load profile window.
        /// </summary>
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            LoadProfileWindow loadProfileWindow = new LoadProfileWindow();

            loadProfileWindow.Show();
            this.Close();
        }

        /// <summary>
        /// Sends the user to the booster pack opening window.
        /// </summary>
        private void PacksBtn_Click(object sender, RoutedEventArgs e)
        {
            PackWindow packsWindow = new PackWindow();

            packsWindow.Show();
            this.Close();
        }

        /// <summary>
        /// Sends the user to their collection.
        /// </summary>
        private void CollectionBtn_Click(object sender, RoutedEventArgs e)
        {
            CollectionWindow collectionWindow = new CollectionWindow();

            collectionWindow.Show();
            this.Close();
        }

        /// <summary>
        /// Displays the users best card to the main menu.
        /// </summary>
        public void DisplayBestCard()
        {
            if (Profile._userCards.Count != 0)
            {
                int topValueIndex = 0;

                for (int i = 0; i < Profile._userCards.Count; i++)
                {
                    if (Profile._userCards[i].Price > Profile._userCards[topValueIndex].Price)
                        topValueIndex = i;
                }

                cardName.Text = Profile._userCards[topValueIndex].Name;
                valueBestCard.Text = Profile._userCards[topValueIndex].Price.ToString();

                Uri uri = new Uri(Profile._userCards[topValueIndex].CardSource, UriKind.Relative);
                ImageSource imgSource = new BitmapImage(uri);
                bestCardIMG.Source = imgSource;

            }
            else
            {
                cardName.Text = "No cards...";
                valueBestCard.Text = "No cards...";
            }
        }

        /// <summary>
        /// Sends the user to the custom card creation window.
        /// </summary>
        private void CustomCardBtn_Click(object sender, RoutedEventArgs e)
        {
            CustomCardWindow creatorWindow = new CustomCardWindow();

            creatorWindow.Show();
            this.Close();
        }
    }
}
