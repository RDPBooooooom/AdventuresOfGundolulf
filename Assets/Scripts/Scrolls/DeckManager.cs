using System.Collections.Generic;
using Levels.Rooms;
using Scrolls.BossScrolls;
using Scrolls.StandardScrolls;
using UnityEngine;
using UnityEngine.UI;

namespace Scrolls
{
    public class DeckManager
    {
        #region Fields

        private Deck<StandardScroll> _standardDeck;
        private Deck<BossScroll> _bossDeck;

        #endregion

        #region Properties
        
        #endregion

        #region Constructor

        public DeckManager()
        {
            _standardDeck = new Deck<StandardScroll>();
            _bossDeck = new Deck<BossScroll>();
        }

        #endregion

        #region Decks

        public void LoadDecks()
        {
            //TODO: Load deck from MainMenu(or where ever)
            _standardDeck.AddScroll(new HelloDarkness());
            _standardDeck.AddScroll(new PoisonGas());
            _standardDeck.AddScroll(new BroughtASwordToAMagicFight());
            
            _standardDeck.InitDeck();
            //_bossDeck.AddScroll();
        }

        private void DisplayStandardDeck()
        {
            //TODO Get amount to draw from Difficulty manager or something like this
            _standardDeck.Draw(3)[0].Activate();
            
        }

        #endregion
        
        public void OnRoomEnter(Room entering)
        {
            if (!entering.WasVisited && entering is CombatRoom)
            {
                DisplayStandardDeck();
            } 
        }
    }
}