using System.Collections.Generic;
using Source.Scripts.GameCore.Deck.StaticData;
using UnityEngine;

namespace Source.Scripts.UI.Windows.EditDeck
{
    public class DeckView : MonoBehaviour
    {
        [SerializeField] private CardView _cardViewPrefab;
        [SerializeField] private Transform _cardsContainer;

        public void Display(IEnumerable<CardInfo> cards)
        {
            foreach (CardInfo card in cards) 
                Instantiate(_cardViewPrefab, _cardsContainer).Display(card);
        }
    }
}