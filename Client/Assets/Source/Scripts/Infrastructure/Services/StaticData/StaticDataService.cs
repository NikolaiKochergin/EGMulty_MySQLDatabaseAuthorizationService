using System.Collections.Generic;
using System.Linq;
using Source.Scripts.GameCore.Deck.StaticData;
using Source.Scripts.StaticData;
using Source.Scripts.UI.StaticData;
using Source.Scripts.UI.Windows;
using UnityEngine;

namespace Source.Scripts.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string UIConfigDataPath = "StaticData/UIConfig";
        private const string GameConfigPath = "StaticData/GameConfig";
        
        private GameConfig _gameConfig;
        private UIConfig _uiConfig;
        private Dictionary<WindowId, WindowBase> _windowsData;
        private Dictionary<string, CardInfo> _cardsData;

        public void Load()
        {
            _gameConfig = Resources.Load<GameConfig>(GameConfigPath);

            _cardsData = _gameConfig
                .Cards
                .ToDictionary(x => x.Id, x => x);
            
            _uiConfig = Resources.Load<UIConfig>(UIConfigDataPath);

            _windowsData = _uiConfig
                .Windows
                .ToDictionary(x => x.WindowId, x => x.Prefab);
        }

        public string ForMainUrl() => 
            _gameConfig.MainUrl;

        public UIRoot ForUIRoot() =>
            _uiConfig.UIRoot;

        public WindowBase ForWindow(WindowId id) =>
            _windowsData.GetValueOrDefault(id);

        public CardInfo ForCard(string id) =>
            _cardsData.GetValueOrDefault(id);

        public IReadOnlyList<CardInfo> ForCards() =>
            _gameConfig.Cards;
    }
}