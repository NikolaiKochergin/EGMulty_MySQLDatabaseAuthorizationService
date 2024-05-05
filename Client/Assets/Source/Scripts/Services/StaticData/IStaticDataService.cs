using Source.Scripts.UI.StaticData;
using Source.Scripts.UI.Windows;

namespace Source.Scripts.Services.StaticData
{
    public interface IStaticDataService
    {
        void Load();
        UIRoot ForUIRoot();
        WindowBase ForWindow(WindowId id);
    }
}