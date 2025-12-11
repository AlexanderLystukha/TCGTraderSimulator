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
    /// Interaction logic for PackWindow.xaml
    /// </summary>
    public partial class PackWindow : Window
    {
        /// <summary>
        /// The users pack inventory.
        /// </summary>
        private List<BoosterPack> _userPack = new List<BoosterPack>();
        public PackWindow()
        {
            InitializeComponent();

            PopulateUserPack();

            lbUserPacks.ItemsSource = _userPack;
        }

        /// <summary>
        /// Populates the users inventory with packs.
        /// </summary>
        private void PopulateUserPack()
        {
            for (int i = 0; i < Profile._boosterCount; i++)
            {
                _userPack.Add(new BoosterPack(5));
            }
        }

        /// <summary>
        /// Opens the booster pack on double click.
        /// </summary>
        private void LbUserPacks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BoosterPack selectedPack = lbUserPacks.SelectedItem as BoosterPack;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < selectedPack._cardsInPack.Length; i++)
            {
                sb.Append(selectedPack._cardsInPack[i] + "\n\n");

                Profile._userCards.Add(selectedPack._cardsInPack[i]);
            }

            Profile._boosterCount--;

            _userPack.Remove(selectedPack); 

            lbUserPacks.Items.Refresh();

            PromptWindow promptWindow = new PromptWindow("OK", "", $"{sb.ToString()}", "Opened booster pack! Contents:");
            promptWindow.ShowDialog();

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
