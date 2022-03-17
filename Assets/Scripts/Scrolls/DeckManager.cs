using System.Collections.Generic;
using System.Linq;
using Levels.Rooms;
using Managers;
using Scrolls.BossScrolls;
using Scrolls.StandardScrolls;
using UI;
using UnityEngine;

namespace Scrolls
{
    public class DeckManager
    {
        #region Fields

        private CastScrollUI _castScrollUI;
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

            _castScrollUI = GameManager.Instance.UIManager.CastScrollUI;
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

            _standardDeck.Scrolls.ForEach(scroll => scroll.ActivateEvent += OnScrollCast);
        }

        private void DisplayStandardDeck()
        {
            //TODO Get amount to draw from Difficulty manager or something like this
            _castScrollUI.DrawnScrolls = _standardDeck.Draw(3).Cast<Scroll>().ToList();

            _castScrollUI.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        #endregion

        private void OnScrollCast(Scroll scroll)
        {
            Time.timeScale = 1;
        }
        
        public void OnRoomEnter(Room entering)
        {
            if (!entering.WasVisited && entering is CombatRoom)
            {
                DisplayStandardDeck();
            } 
        }
    }
}