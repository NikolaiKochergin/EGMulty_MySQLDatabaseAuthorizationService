using Reflex.Core;
using Source.Scripts.UI.Factory;
using Source.Scripts.UI.Services.Windows;
using Source.Scripts.UI.Windows;

namespace Source.Scripts.Infrastructure
{
    public class Game
    {
        private readonly IUIFactory _uiFactory;
        private readonly IWindowService _windows;

        public Game(Container container)
        {
            _uiFactory = container.Single<IUIFactory>();
            _windows = container.Single<IWindowService>();
        }
        
        public void Start()
        {
            _uiFactory.CreateUIRoot();
            _windows.OpenWindow(WindowId.Authorization);
        }
    }
}