using System.Collections.Generic;
using Source.Scripts.UI.Windows;
using UnityEngine;

namespace Source.Scripts.UI.StaticData
{
    [CreateAssetMenu(fileName = "UIConfig", menuName = "Static Data/UI Config", order = 0)]
    public class UIConfig : ScriptableObject
    {
        [SerializeField] private UIRoot _uiRoot;
        [SerializeField] private List<WindowConfig> _windowsConfig;

        public UIRoot UIRoot => _uiRoot;
        public IEnumerable<WindowConfig> Windows => _windowsConfig;
    }
}