using UnityEngine;

namespace Source.Scripts.UI.Windows
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        private void Awake() => 
            _canvas.worldCamera = Camera.main;
        
#if UNITY_EDITOR
        private void Reset() => 
            _canvas.worldCamera = Camera.main;
#endif
    }
}