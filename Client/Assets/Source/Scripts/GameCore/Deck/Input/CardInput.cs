using Source.Scripts.UI.Windows.EditDeck;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Source.Scripts.GameCore.Deck.Input
{
    public class CardInput : MonoBehaviour, IBeginDragHandler, IDragHandler , IEndDragHandler
    {
        [SerializeField] private CardView _cardView;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private Transform _canvas;
        public int Id => _cardView.Id;

        private void Awake() => 
            _canvas = GetComponentInParent<Canvas>().transform;

        public void OnBeginDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = false;
            _cardView.transform.SetParent(_canvas);
            _cardView.transform.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData) =>
            _cardView.transform.position = eventData.pointerCurrentRaycast.worldPosition;

        public void OnEndDrag(PointerEventData eventData)
        {
            _cardView.transform.SetParent(transform);
            _canvasGroup.blocksRaycasts = true;
            _cardView.transform.localPosition = Vector3.zero;
        }
    }
}