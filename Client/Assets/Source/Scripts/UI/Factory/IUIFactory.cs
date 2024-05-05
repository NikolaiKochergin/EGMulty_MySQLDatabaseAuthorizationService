using Source.Scripts.UI.StaticData;
using Source.Scripts.UI.Windows;

namespace Source.Scripts.UI.Factory
{
    public interface IUIFactory
    {
        void CreateUIRoot();
        WindowBase CreateWindow(WindowId id);
        void Cleanup();
    }
}