using System;
using System.Collections.Generic;
using Source.Scripts.GameCore.Deck.StaticData;

namespace Source.Scripts.GameCore.Deck.Service
{
    public interface IDeckService
    {
        void Initialize();
        IReadOnlyList<CardInfo> SelectedCards { get; }
        IReadOnlyList<CardInfo> AvailableCards { get; }
        bool IsLoaded { get; }
        event Action Updated;
        bool TrySelect(int cardId);
        void UpdateSelectedCards();
        bool TryUnselect(int cardId);
    }
}