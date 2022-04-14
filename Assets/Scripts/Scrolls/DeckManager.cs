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

            _standardDeck.AddScroll(new UnknownPotion());
            _standardDeck.AddScroll(new AttackDown());
            _standardDeck.AddScroll(new AttackUp());
            _standardDeck.AddScroll(new HealingWord());
            _standardDeck.AddScroll(new IntelligenceDown());
            _standardDeck.AddScroll(new IntelligenceUp());
            _standardDeck.AddScroll(new SpeedDown());
            _standardDeck.AddScroll(new SpeedUp());

            _standardDeck.AddScroll(new BulrogsTaxes());
            _standardDeck.AddScroll(new Casino());
            _standardDeck.AddScroll(new NoHitChallenge());
            
            _standardDeck.AddScroll(new Spinning());
            _standardDeck.AddScroll(new TopUpPerspective());
            _standardDeck.AddScroll(new TeleporterMalfunction());
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

            Time.timeScale = 0;
            GameManager.Instance.Player.Input.Disable();
            GameManager.Instance.UIManager.DisablePausePanel = true;
            _castScrollUI.gameObject.SetActive(true);
        }

        #endregion

        private void OnScrollCast(Scroll scroll)
        {
            GameManager.Instance.Player.Input.Enable();
            Time.timeScale = 1;
            GameManager.Instance.UIManager.DisablePausePanel = false;
        }
        
        public void OnRoomEnter(Room entering)
        {
            Debug.Log(ScrollLoader.GetDisplayName("PoisonGas"));
            if (!entering.WasVisited && entering is CombatRoom)
            {
                DisplayStandardDeck();
            } 
        }
    }
}