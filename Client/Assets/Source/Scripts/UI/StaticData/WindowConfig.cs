using System;
using Source.Scripts.UI.Windows;
using UnityEngine;

namespace Source.Scripts.UI.StaticData
{
    [Serializable]
    public class WindowConfig
    {
        [SerializeField] private WindowId _windowId;
        [SerializeField] private WindowBase _window;

        public WindowId WindowId => _windowId;
        public WindowBase Prefab => _window;
    }
}