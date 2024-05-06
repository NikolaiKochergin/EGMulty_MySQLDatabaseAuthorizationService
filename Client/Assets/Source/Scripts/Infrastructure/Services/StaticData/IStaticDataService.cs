using Source.Scripts.GameCore.Deck.StaticData;
using Source.Scripts.UI.Windows;

namespace Source.Scripts.Infrastructure.Services.StaticData
{
    public interface IStaticDataService
    {
        void Load();
        UIRoot ForUIRoot();
        WindowBase ForWindow(WindowId id);
        public CardInfo ForCard(int id);
        string ForMainUrl();
    }
}