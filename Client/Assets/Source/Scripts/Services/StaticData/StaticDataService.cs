using System.Collections.Generic;
using System.Linq;
using Source.Scripts.UI.StaticData;
using Source.Scripts.UI.Windows;
using UnityEngine;

namespace Source.Scripts.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string UIConfigDataPath = "StaticData/UIConfig";
        
        private UIConfig _uiConfig;
        private Dictionary<WindowId, WindowBase> _windowsData;

        public void Load()
        {
            _uiConfig = Resources.Load<UIConfig>(UIConfigDataPath);

            _windowsData = _uiConfig
                .Windows
                .ToDictionary(x => x.WindowId, x => x.Prefab);
        }

        public UIRoot ForUIRoot() =>
            _uiConfig.UIRoot;

        public WindowBase ForWindow(WindowId id) =>
            _windowsData.GetValueOrDefault(id);
    }
}