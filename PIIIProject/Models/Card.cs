using System.Windows.Input;

namespace PIIIProject.Models
{
    internal class Card
    {
        #region Data Members
        private string _name;
        private double _price;
        private int _quality;
        private string _cardType;
        private string _elementalType;
        private string _source;
        private bool _isCustom = false;
        private Random _rand = new Random();

        #region Constructors
        public Card(string cardType)
        {
            Name = GetRandomName();
            Quality = _rand.Next(1, 11);
            CardType = cardType;
            ElementalType = GetElementalType();
            Price = GetPrice(); 
            CardSource = GetSource();
        }

        public Card()
        {
            Name = GetRandomName();
            CardType = GetCardType();
            Quality = _rand.Next(1, 11);
            ElementalType = GetElementalType();
            Price = GetPrice();
            CardSource = GetSource();
        }

        public Card(string name, double price, int quality, string cardType, string elementalType, bool isCustom, string source)
        {
            Name = name;
            Price = price;
            Quality = quality;
            CardType = cardType;
            ElementalType = elementalType;
            IsCustom = isCustom;
            CardSource = source;
        }

        public Card(string name, string cardType, string elementalType, string source, bool isCustom)
        {
            Name = name;
            CardType = cardType;
            ElementalType = elementalType;
            CardSource = source;
            Quality = _rand.Next(1, 11);
            Price = GetPrice();
            IsCustom = isCustom;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Represents the source for the image of the card (which is not relative to the name.)
        /// </summary>
        public string CardSource
        {
            get { return _source; }
            private set
            {
                _source = value;
            }
        }

        /// <summary>
        /// Represents the name of the card.
        /// </summary>
        public string Name 
        { 
            get { return _name; } 
            private set
            {
                _name = value;
            }
        }

        /// <summary>
        /// Represents the price of the card.
        /// </summary>
        public double Price
        {
            get { return _price; }
            private set
            {
                _price = value;
            }
        }

        /// <summary>
        /// Represents the quality of the card.
        /// </summary>
        public int Quality
        {
            get { return _quality; }
            private set
            {
                _quality = value;
            }
        }

        /// <summary>
        /// Represents the type (which relates to its rarety) of the card.
        /// </summary>
        public string CardType
        {
            get { return _cardType; }
            private set
            {
                if (value.ToLower() != "common" && value.ToLower() != "holographic" && value.ToLower() != "fullart" && string.IsNullOrEmpty(value))
                    throw new ArgumentException("Type cannot be empty, null and must be either common, holographic or fullart");

                _cardType = value;
            }
        }

        /// <summary>
        /// Represents the elemental type of the card.
        /// </summary>
        public string ElementalType
        {
            get { return _elementalType; }
            private set
            { 
                _elementalType = value;
            }
        }

        /// <summary>
        /// If the card was made by the user or not.
        /// </summary>
        public bool IsCustom
        {
            get { return _isCustom; }
            set { _isCustom = value; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns a multiplier for the price that depends on the quality of the card.
        /// </summary>
        /// <returns>The multiplier.</returns>
        public double GetQualityModifier()
        {
            switch (Quality)
            {
                case 1:
                    return 0.2;
                case 2:
                    return 0.3;
                case 3:
                    return 0.4;
                case 4:
                    return 0.5;
                case 5:
                    return 0.6;
                case 6:
                    return 0.7;
                case 7:
                    return 1;
                case 8:
                    return 1.5;
                case 9:
                    return 3;
                case 10:
                    return 6;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Gets the type of the card.
        /// </summary>
        /// <returns>The type of the card.</returns>
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

        /// <summary>
        /// Gets the price of a card.
        /// </summary>
        /// <returns>The price of the card.</returns>
        public double GetPrice()
        {
            int basePrice = 0;

            if (CardType.ToLower() == "common")
                basePrice = 2;

            if (CardType.ToLower() == "holographic")
                basePrice = 7;

            if (CardType.ToLower() == "fullart")
                basePrice = 12;

            return Math.Round(basePrice * GetQualityModifier(), 2);
        }

        /// <summary>
        /// Gets the elemental type of the card.
        /// </summary>
        /// <returns>The cards type.</returns>
        public string GetElementalType()
        {
            int typeValue = _rand.Next(1, 6);

            switch (typeValue)
            {
                case 1:
                    return "Fire";
                case 2:
                    return "Water";
                case 3:
                     return "Grass";
                case 4:
                     return "Light";
                case 5:
                     return "Darkness";
                default:
                    return "Secret";
            }
        }

        /// <summary>
        /// Gets the image source of the card depending on its type.
        /// </summary>
        /// <returns>The image source.</returns>
        public string GetSource()
        {
            if (CardType.ToLower() == "fullart")
                return $"../../../Images/PokemonCards/FullArt/fa{_rand.Next(1, 6)}.jpg";
            else if (CardType.ToLower() == "holographic")
                return $"../../../Images/PokemonCards/Holographic/h{_rand.Next(1, 11)}.jpg";
            else if (CardType.ToLower() == "common")
                return $"../../../Images/PokemonCards/Common/c{_rand.Next(1, 20)}.jpg";

            return "None";
        }

        /// <summary>
        /// Gets a name from a random pool of pokemon names.
        /// </summary>
        /// <returns>A random name.</returns>
        public string GetRandomName()
        {
            string[] pokemonNames = new string[]
            {
                "Bulbasaur", "Ivysaur", "Venusaur", "Charmander", "Charmeleon", "Charizard",
                "Squirtle", "Wartortle", "Blastoise", "Caterpie", "Metapod", "Butterfree",
                "Weedle", "Kakuna", "Beedrill", "Pidgey", "Pidgeotto", "Pidgeot",
                "Rattata", "Raticate", "Spearow", "Fearow", "Ekans", "Arbok",
                "Pikachu", "Raichu", "Sandshrew", "Sandslash", "Nidoran♀", "Nidorina", "Nidoqueen",
                "Nidoran♂", "Nidorino", "Nidoking", "Clefairy", "Clefable", "Vulpix", "Ninetales",
                "Jigglypuff", "Wigglytuff", "Zubat", "Golbat", "Oddish", "Gloom", "Vileplume",
                "Paras", "Parasect", "Venonat", "Venomoth", "Diglett", "Dugtrio", "Meowth", "Persian",
                "Psyduck", "Golduck", "Mankey", "Primeape", "Growlithe", "Arcanine", "Poliwag",
                "Poliwhirl", "Poliwrath", "Abra", "Kadabra", "Alakazam", "Machop", "Machoke",
                "Machamp", "Bellsprout", "Weepinbell", "Victreebel", "Tentacool", "Tentacruel",
                "Geodude", "Graveler", "Golem", "Ponyta", "Rapidash", "Slowpoke", "Slowbro",
                "Magnemite", "Magneton", "Farfetch’d", "Doduo", "Dodrio", "Seel", "Dewgong",
                "Grimer", "Muk", "Shellder", "Cloyster", "Gastly", "Haunter", "Gengar", "Onix",
                "Drowzee", "Hypno", "Krabby", "Kingler", "Voltorb", "Electrode", "Exeggcute",
                "Exeggutor", "Cubone", "Marowak", "Hitmonlee", "Hitmonchan", "Lickitung", "Koffing",
                "Weezing", "Rhyhorn", "Rhydon", "Chansey", "Tangela", "Kangaskhan", "Horsea",
                "Seadra", "Goldeen", "Seaking", "Staryu", "Starmie", "Mr. Mime", "Scyther",
                "Jynx", "Electabuzz", "Magmar", "Pinsir", "Tauros", "Magikarp", "Gyarados",
                "Lapras", "Ditto", "Eevee", "Vaporeon", "Jolteon", "Flareon", "Porygon",
                "Omanyte", "Omastar", "Kabuto", "Kabutops", "Aerodactyl", "Snorlax", "Articuno",
                "Zapdos", "Moltres", "Dratini", "Dragonair", "Dragonite", "Mewtwo", "Mew"
            }; 

            
            return pokemonNames[_rand.Next(0, pokemonNames.Length)];
        }

        public override string ToString()
        {
            return $"Name: {Name}, Card Type: {CardType}, Elemental Type {ElementalType}, Value: {Price}";
        }
        #endregion

        #endregion
    }
}
