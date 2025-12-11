using PIIIProject.Views;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PIIIProject.Models
{
    static internal class Profile
    {
        #region Static Members
        /// <summary>
        /// The cards the user possesses. 
        /// </summary>
        public static List<Card> _userCards = new List<Card>();

        /// <summary>
        /// The amount of boosters the user has.
        /// </summary>
        public static int _boosterCount;

        /// <summary>
        /// The amount of money the user has.
        /// </summary>
        public static double _moneyCount = 10;

        /// <summary>
        /// The path of the .csv file where the users data is stored.
        /// </summary>
        public static string _pathToSaveTo;

        #region Methods
        /// <summary>
        /// Loads the content of a .csv file into the class. 
        /// </summary>
        /// <param name="filePath">The path of the .csv file.</param>
        public static void LoadContent(string filePath)
        {
            _pathToSaveTo = filePath;
            _boosterCount = 0;
            _moneyCount = 10;
            _userCards.Clear();

            try
            {
                string[] contents = File.ReadAllLines($"{filePath}.csv");

                _userCards.Clear();

                for (int i = 1; i < contents.Length; i++)
                {
                    string[] info = contents[i].Split(";");

                    if (i == 1)
                    {
                        _userCards.Add(new Card(info[0], double.Parse(info[1]), int.Parse(info[2]), info[3], info[4], bool.Parse(info[5]), info[6]));
                        _boosterCount = int.Parse(info[7]);
                        _moneyCount = double.Parse(info[8]); //when cards are glitched when money count is bad
                    }
                    else
                    {
                        _userCards.Add(new Card(info[0], double.Parse(info[1]), int.Parse(info[2]), info[3], info[4], bool.Parse(info[5]), info[6]));
                    }

                }
            }
            catch (IOException ex)
            {
                PromptWindow promptWindow = new PromptWindow("OK", "", "This error happens when you try to load the same profile twice. Please load another profile and try again.", "File Duplication.");
            }
            

        }

        /// <summary>
        /// Saves the content stored in the class into the user's .csv file.
        /// </summary>
        public static void SaveContent()
        {
            if (_userCards.Count != 0)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("name;price;quality;cardtype;elementaltype;iscustom;source;booster;balance;");//have to use semicolons since doubles use commas to seperate the number and the decimals

                sb.AppendLine($"{_userCards[0].Name};{_userCards[0].Price};{_userCards[0].Quality};{_userCards[0].CardType};{_userCards[0].ElementalType};{_userCards[0].IsCustom};{_userCards[0].CardSource};{_boosterCount};{_moneyCount}");


                for (int i = 1; i < _userCards.Count; i++)
                {
                    sb.AppendLine($"{_userCards[i].Name};{_userCards[i].Price};{_userCards[i].Quality};{_userCards[i].CardType};{_userCards[i].ElementalType};{_userCards[i].IsCustom};{_userCards[i].CardSource};{_boosterCount};{_moneyCount}");
                }

                File.WriteAllText(_pathToSaveTo,sb.ToString());
            }
        }
        #endregion

        #endregion
    }
}
