using System.Collections.Generic;
using System.Linq;
using Scrolls.BossScrolls;
using Scrolls.StandardScrolls;
using UnityEngine;
using Utils;

namespace Scrolls
{
    public class Deck<T> where T : Scroll
    {
        #region Fields

        private List<T> _deckPile;
        private List<T> _discardPile;

        #endregion

        #region Properties

        public List<T> Scrolls { get; private set; }

        #endregion

        #region Constructor

        public Deck()
        {
            Scrolls = new List<T>();
            _deckPile = new List<T>();
            _discardPile = new List<T>();
        }

        public Deck(List<T> scrolls)
        {
            Scrolls = scrolls;
            _deckPile = new List<T>();
            _discardPile = new List<T>();
        }

        #endregion

        // Manages Cards in the Deck

        #region Card Management

        public void AddScroll(T scroll)
        {
            Scrolls.Add(scroll);
            scroll.ActivateEvent += OnScrollActivation;
        }

        public void AddScroll(List<T> scrolls)
        {
            Scrolls.AddRange(scrolls);
            Scrolls.ForEach(scroll => scroll.ActivateEvent += OnScrollActivation);
        }

        public void RemoveScroll(T scroll)
        {
            Scrolls.Remove(scroll);
            scroll.ActivateEvent -= OnScrollActivation;
        }

        public void InitDeck()
        {
            _deckPile.AddRange(Scrolls);
        }

        #endregion

        // Manages the position of the Cards in the Deck

        #region Deck Management

        public void Shuffle()
        {
            _deckPile = ListUtils.ShuffleList(_deckPile);
        }

        public List<T> Draw(int numberToDraw)
        {
            if (_deckPile.Count == 0) ResetDeckPile();
            if (_deckPile.Count < numberToDraw) numberToDraw = _deckPile.Count;

            return _deckPile.GetRange(0, numberToDraw);
        }

        private void OnScrollActivation(Scroll used)
        {
            _deckPile.Remove((T) used);
            _discardPile.Add((T) used);
        }

        public void ResetDeckPile()
        {
            _deckPile.AddRange(_discardPile);
            _discardPile.Clear();
            Shuffle();
        }

        #endregion
    }
}