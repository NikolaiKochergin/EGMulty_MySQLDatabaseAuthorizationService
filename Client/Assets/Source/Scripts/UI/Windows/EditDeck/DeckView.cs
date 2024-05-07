using System.Collections.Generic;
using Source.Scripts.GameCore.Deck;
using Source.Scripts.GameCore.Deck.StaticData;
using UnityEngine;

namespace Source.Scripts.UI.Windows.EditDeck
{
    public class DeckView : MonoBehaviour
    {
        [SerializeField] private Card _cardPrefab;
        [SerializeField] private Transform _cardsContainer;

        public void Display(IEnumerable<CardInfo> cards)
        {
            foreach (CardInfo card in cards)
                Instantiate(_cardPrefab, _cardsContainer)
                    .Initialize(card);
        }
    }
}