using PIIIProject.Models;
using System;
using System.CodeDom;
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

namespace PIIIProject.Views
{
    /// <summary>
    /// Interaction logic for MarketWindow.xaml
    /// </summary>
    /// 

    public partial class MarketWindow : Window
    {
        /// <summary>
        /// The instance of market used.
        /// </summary>
        private Market _market = new Market(20);
        public MarketWindow()
        {
            InitializeComponent();

            balanceCount.Text = Math.Round(Profile._moneyCount, 2).ToString();

            lbMarket.ItemsSource = _market._marketCards;           
        }

        /// <summary>
        /// Triggered when an item in the market is clicked, prompts the user to buy it.
        /// </summary>
        private void LbMarket_Click(object sender, MouseButtonEventArgs e)
        {
            // get the item that was clicked 
            ListBoxItem item = ItemsControl.ContainerFromElement(sender as ListBox, e.OriginalSource as DependencyObject) as ListBoxItem;


            if (item is not null) //this is needed because when dragging the scrollbar, it provokes the event
            {
                //get the card from the item that was clicked

                Card selectedCard = item.DataContext as Card;

                PromptWindow promptWdw = new PromptWindow("YES","NO", $"Would you like to buy this card?\n{selectedCard}", "Buy Card?");
                promptWdw.ShowDialog();
                string result = promptWdw._selectedButton;

                if ( result == "YES")
                {
                    if (selectedCard.Price <= Profile._moneyCount)
                    {
                        Profile._moneyCount -= selectedCard.Price;

                        Profile._userCards.Add(selectedCard);

                        _market.RemoveCard(selectedCard);

                        lbMarket.Items.Refresh();

                        balanceCount.Text = Profile._moneyCount.ToString();
                    }
                    else
                    {
                        PromptWindow promptWindow = new PromptWindow("OK", "", "Not enough money to buy card.", "Money missing.");
                        promptWindow.ShowDialog();

                    }
                }

            }

        }

        /// <summary>
        /// Sends the user back to the main menu.
        /// </summary>
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainMenu = new MainMenu(Profile._pathToSaveTo);

            Profile.SaveContent();

            mainMenu.Show();
            this.Close();
        }

        /// <summary>
        /// Sends the user to the booster pack shop.
        /// </summary>
        private void BoosterShopBtn_Click(object sender, RoutedEventArgs e)
        {
            PackMarketWindow packMarketWindow = new PackMarketWindow();

            packMarketWindow.Show();
            this.Close();
        }
    }
}
