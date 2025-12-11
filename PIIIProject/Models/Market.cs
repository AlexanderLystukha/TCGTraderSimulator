using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIIIProject.Models
{
    internal class Market
    {
        #region Data Members
        private readonly int MarketplaceSize;
        public List<Card> _marketCards;

        #region Constructor
        public Market(int marketSize)
        {
            MarketplaceSize = marketSize;
            _marketCards = new List<Card>();
            PopulateMarket();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Populates the market with a specified amount of cards. A list instead of an array because it makes the listbox logic a lot simpler.
        /// </summary>
        private void PopulateMarket()
        {
            for (int i = 0; i < MarketplaceSize; i++)
            {
                _marketCards.Add(new Card());
            }
        }

        /// <summary>
        /// Removes a card from the market..
        /// </summary>
        /// <param name="cardToRemove">The card to removes.</param>
        public void RemoveCard(Card cardToRemove)
        {
            _marketCards.Remove(cardToRemove);
        }
        #endregion
        #endregion
    }
}
