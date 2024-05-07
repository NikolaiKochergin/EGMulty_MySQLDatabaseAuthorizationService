using Source.Scripts.UI.Windows.EditDeck;
using UnityEngine;

namespace Source.Scripts.GameCore.Deck.Input
{
    public class CardSelector : MonoBehaviour
    {
        [SerializeField] private DeckView _selectedDeck;
        [SerializeField] private DeckView _availableDeck;

        private Card _selectedCard;


    }
}