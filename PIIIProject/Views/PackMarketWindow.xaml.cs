using PIIIProject.Models;
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

namespace PIIIProject.Views
{
    /// <summary>
    /// Interaction logic for PackMarketWindow.xaml
    /// </summary>
    public partial class PackMarketWindow : Window
    {
        private const int MarketSize = 10;

        /// <summary>
        /// The booster pack shop packs.
        /// </summary>
        private List<BoosterPack> _boosterPacks = new List<BoosterPack>();
        public PackMarketWindow()
        {
            InitializeComponent();

            balanceCount.Text = Math.Round(Profile._moneyCount, 2).ToString();

            PopulatePacks();

            lbPackMarket.ItemsSource = _boosterPacks;
        }

        /// <summary>
        /// Adds boosters to the market list.
        /// </summary>
        private void PopulatePacks()
        {
            for (int i = 0; i < MarketSize; i++)
            {
                _boosterPacks.Add(new BoosterPack(5));
            }
        }

        /// <summary>
        /// Triggers when the user clicks on a pack, prompts them to buy it.
        /// </summary>
        private void LbPackMarket_Click(object sender, MouseButtonEventArgs e)
        {
            // get the item that was clicked 
            ListBoxItem item = ItemsControl.ContainerFromElement(sender as ListBox, e.OriginalSource as DependencyObject) as ListBoxItem;

            if (item is not null) //this is needed because when dragging the scrollbar, it prokes the event
            {

                //get the card from the item that was clicked
                BoosterPack selectedPack = item.DataContext as BoosterPack;

                PromptWindow promptWindow = new PromptWindow("YES", "NO", "", "Buy this Pack?");
                promptWindow.ShowDialog();

                string result = promptWindow._selectedButton;

                if (result == "YES")
                {            

                    if (selectedPack.Price <= Profile._moneyCount)
                    {
                        Profile._moneyCount -= selectedPack.Price;

                        Profile._boosterCount++;

                        _boosterPacks.Remove(selectedPack);

                        lbPackMarket.Items.Refresh();

                        balanceCount.Text = Math.Round(Profile._moneyCount, 2).ToString();
                    }
                    else
                    {
                        PromptWindow noMoney = new PromptWindow("OK", "", "You don't have enough money to buy this pack.", "You're broke!");
                        noMoney.ShowDialog();
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
    }
}
