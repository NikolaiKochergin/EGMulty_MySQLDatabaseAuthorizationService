using Reflex.Attributes;
using Source.Scripts.UI.Extensions;
using Source.Scripts.UI.Services.Windows;
using Source.Scripts.UI.StaticData;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI.Elements
{
    public class OpenWindowButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private WindowId _windowId;
        
        private IWindowService _windows;

        [Inject]
        private void Construct(IWindowService windows) => 
            _windows = windows;

        private void Awake() => 
            _button.AddListener(OnButtonClicked);

        private void OnDestroy() => 
            _button.RemoveListener(OnButtonClicked);

        private void OnButtonClicked() => 
            _windows.OpenWindow(_windowId);

#if UNITY_EDITOR
        public void Reset() => 
            _button = GetComponent<Button>();
#endif
    }
}