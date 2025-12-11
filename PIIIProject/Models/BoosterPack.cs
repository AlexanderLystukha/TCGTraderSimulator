using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIIIProject.Models
{
    internal class BoosterPack
    {
        #region Data Members
        public Card[] _cardsInPack;
        private string _boosterImage;
        private Random _rand = new Random();

        #region Constructor
        public BoosterPack(int packSize)
        {
            _cardsInPack = new Card[packSize];
            BoosterImage = $"../../../Images/Boosterpack/booster{_rand.Next(1, 6)}.png";
            PopulateBooster();
            
        }
        #endregion

        #region Properties
        /// <summary>
        /// Represents the name of the booster pack (mostly used for binding.)
        /// </summary>
        public string Name
        {
            get { return "Booster Pack"; }
        }

        /// <summary>
        /// Represents the price of a booster pack (mostly used for binding.)
        /// </summary>
        public double Price
        {
            get { return 5; }
        }

        /// <summary>
        /// Represents the image for a booster pack.
        /// </summary>
        public string BoosterImage
        {
            get { return _boosterImage; }
            private set
            {
                _boosterImage = value;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adds the cards to the booster pack.
        /// </summary>
        private void PopulateBooster()
        {
            for (int i = 0; i < _cardsInPack.Length; i++)
            {
                _cardsInPack[i] = new Card(GetCardType());
            }
        }
        /// <summary>
        /// Gets the card type for a card.
        /// </summary>
        /// <returns>The card type.</returns>
        public string GetCardType()
        {
            Random rnd = new Random();

            int typeValue = rnd.Next(1, 11);

            if (typeValue < 7)
                return "Common";
            else if (typeValue >= 7 && typeValue <= 9)
                return "Holographic";
            else
                return "FullArt";
        }
        #endregion

        #endregion
    }
}
