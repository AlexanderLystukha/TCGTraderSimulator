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
    /// Interaction logic for CollectionWindow.xaml
    /// </summary>
    public partial class CollectionWindow : Window
    {
        /// <summary>
        /// The cards the user selected to sell.
        /// </summary>
        private List<Card> cardsToSell = new List<Card>();

        public CollectionWindow()
        {
            InitializeComponent();

            lbCollection.ItemsSource = Profile._userCards.Where(obj => obj.IsCustom == false).ToList();
        }

        /// <summary>
        /// Triggers when the user clicks an item in their collection. Adds it to the list of items to save.
        /// </summary>
        private void LbCollection_Click(object sender, MouseButtonEventArgs e)
        {
            // get the item that was clicked 
            ListBoxItem item = ItemsControl.ContainerFromElement(sender as ListBox, e.OriginalSource as DependencyObject) as ListBoxItem;

            if (item is not null) //this is needed because when dragging the scrollbar, it prokes the event
            {

                //get the card from the item that was clicked
                Card selectedCard = item.DataContext as Card;

                if (!selectedCard.IsCustom)
                {
                    if (!cardsToSell.Contains(selectedCard))
                        cardsToSell.Add(selectedCard);

                    SellBtn.Visibility = Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// Prompts the user to sell the card they've clicked.
        /// </summary>
        private void SellBtn_Click(object sender, RoutedEventArgs e)
        {
            if(cardsToSell.Count != 0)
            {
                StringBuilder sb = new StringBuilder();

                foreach (Card card in cardsToSell)
                {
                    sb.Append(card + "\n\n");
                }

                PromptWindow promptWindow = new PromptWindow("YES", "NO", $"{sb}", "Sell these cards?");
                promptWindow.ShowDialog();

                string result = promptWindow._selectedButton;

                if (result == "YES")
                {
                    double profit = 0;
                    foreach (Card card in cardsToSell)
                    {
                        profit += card.Price;
                        Profile._userCards.Remove(card);
                    }

                    Profile._moneyCount += profit;
                }

                lbCollection.ItemsSource = Profile._userCards;

                lbCollection.Items.Refresh();

                cardsToSell.Clear();

            }
            else
            {
                PromptWindow promptWindow = new PromptWindow("OK", "", "You haven't selected any cards to be sold.", "No cards to sell!");
                promptWindow.ShowDialog();
            }
        }

        /// <summary>
        /// Sends the user back to the main menu.
        /// </summary>
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mn = new MainMenu(System.IO.Path.GetFileNameWithoutExtension(Profile._pathToSaveTo));

            Profile.SaveContent();

            mn.Show();
            this.Close();
        }

        /// <summary>
        /// Switches the collections of the user from custom to regular and vice-versa.
        /// </summary>
        private void BtnSwitchCollections_Click(object sender, RoutedEventArgs e)
        {
            if (cardShopBtn.Content.ToString() == "Custom Collection")
            {
                SellBtn.Visibility = Visibility.Hidden;

                cardShopBtn.Content = "Normal Collection";

                lbCollection.ItemsSource = Profile._userCards.Where(obj => obj.IsCustom).ToList();

                lbCollection.Items.Refresh();
            }
            else
            {
                SellBtn.Visibility= Visibility.Visible;

                cardShopBtn.Content = "Custom Collection";

                lbCollection.ItemsSource = Profile._userCards.Where(obj => obj.IsCustom == false).ToList();

                lbCollection.Items.Refresh();
            }
        }
    }
}
