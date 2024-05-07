using Reflex.Attributes;
using Source.Scripts.GameCore.Deck.Service;

namespace Source.Scripts.UI.Elements
{
    public class SaveDeckButton : ButtonBase
    {
        private IDeckService _deck;

        [Inject]
        private void Construct(IDeckService deck) => 
            _deck = deck;

        private void Awake() => 
            AddListener(OnButtonClicked);

        private void OnDestroy() => 
            RemoveListener(OnButtonClicked);

        private void OnButtonClicked() => 
            _deck.UpdateSelectedCards();
    }
}