using System.Collections.Generic;
using Source.Scripts.GameCore.Deck.StaticData;
using Source.Scripts.UI.StaticData;
using Source.Scripts.UI.Windows;

namespace Source.Scripts.Infrastructure.Services.StaticData
{
    public interface IStaticDataService
    {
        void Load();
        UIRoot ForUIRoot();
        WindowBase ForWindow(WindowId id);
        CardInfo ForCard(string id);
        IReadOnlyList<CardInfo> ForCards();
        string ForMainUrl();
    }
}