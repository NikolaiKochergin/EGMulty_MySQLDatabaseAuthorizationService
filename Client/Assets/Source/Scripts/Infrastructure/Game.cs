using Reflex.Core;
using Source.Scripts.Services.StaticData;
using Source.Scripts.UI.Factory;
using Source.Scripts.UI.Services.Windows;
using Source.Scripts.UI.StaticData;

namespace Source.Scripts.Infrastructure
{
    public class Game
    {
        private readonly IStaticDataService _staticData;
        private readonly IUIFactory _uiFactory;
        private readonly IWindowService _windows;

        public Game(Container container)
        {
            _staticData = container.Single<IStaticDataService>();
            _uiFactory = container.Single<IUIFactory>();
            _windows = container.Single<IWindowService>();
        }
        
        public void Start()
        {
            _staticData.Load();
            _uiFactory.CreateUIRoot();
            _windows.OpenWindow(WindowId.Authorization);
        }
    }
}