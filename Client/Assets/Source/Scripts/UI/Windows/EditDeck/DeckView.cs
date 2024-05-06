using System.Collections.Generic;
using Source.Scripts.GameCore.Deck.StaticData;
using UnityEngine;

namespace Source.Scripts.UI.Windows.EditDeck
{
    public class DeckView : MonoBehaviour
    {
        [SerializeField] private CardView _cardViewPrefab;
        [SerializeField] private Transform _cardsContainer;

        private readonly Dictionary<int, CardView> _cardViews = new Dictionary<int, CardView>();

        public void Display(IEnumerable<CardInfo> cards)
        {
            foreach (CardInfo card in cards)
                CreateCardView(card);
        }

        public void Add(CardView cardView) => 
            _cardViews.Add(cardView.Id, cardView);

        public void Remove(CardView cardView) => 
            _cardViews.Remove(cardView.Id);

        private void CreateCardView(CardInfo cardInfo)
        {
            CardView card = Instantiate(_cardViewPrefab, _cardsContainer);
            card.Display(cardInfo);
            _cardViews.Add(card.Id, card);
        }
    }
}