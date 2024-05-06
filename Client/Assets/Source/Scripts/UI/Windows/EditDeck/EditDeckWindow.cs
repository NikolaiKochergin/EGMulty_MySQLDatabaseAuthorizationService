using Reflex.Attributes;
using Source.Scripts.GameCore.Deck.Service;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI.Windows.EditDeck
{
    public class EditDeckWindow : WindowBase
    {
        [SerializeField] private DeckView _selectedCardsDeck;
        [SerializeField] private DeckView _availableCardsDeck;
        [SerializeField] private Button _saveButton;
        
        private IDeckService _deck;

        [Inject]
        private void Construct(IDeckService deck) => 
            _deck = deck;

        protected override void Initialize()
        {
            base.Initialize();
            if (_deck.IsLoaded)
                DisplayCards();
            else
                _deck.Updated += OnDeckUpdated;
        }

        private void OnDeckUpdated()
        {
            _deck.Updated -= OnDeckUpdated;
            DisplayCards();
        }

        private void DisplayCards()
        {
            _selectedCardsDeck.Display(_deck.SelectedCards);
            _availableCardsDeck.Display(_deck.AvailableCards);
        }
    }
}