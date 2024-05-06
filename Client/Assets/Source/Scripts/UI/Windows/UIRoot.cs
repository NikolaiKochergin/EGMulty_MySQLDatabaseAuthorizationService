using UnityEngine;

namespace Source.Scripts.UI.Windows
{
    [ExecuteAlways]
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        private void Awake() => 
            _canvas.worldCamera = Camera.main;
    }
}