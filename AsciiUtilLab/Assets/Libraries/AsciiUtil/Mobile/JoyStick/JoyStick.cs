using UnityEngine;
using UnityEngine.EventSystems;

namespace AsciiUtil
{
    public class JoyStick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [SerializeField]
        private RectTransform background;
        [SerializeField]
        private RectTransform handle;
        [SerializeField]
        private Camera targetCamera;
        [SerializeField]
        private Canvas canvas;
        [SerializeField]
        private float handleRange = 1;
        [SerializeField]
        private float deadZone = 0;
        private RectTransform baseRect;
        private Vector2 inputDirection;
        public Vector2 InputDirection => inputDirection;
        public bool IsInput => inputDirection.sqrMagnitude > 0;

        private void Start()
        {
            baseRect = GetComponent<RectTransform>();
            targetCamera ??= Camera.main;
            canvas ??= GetComponent<Canvas>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
            background.gameObject.SetActive(true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            background.gameObject.SetActive(false);
            inputDirection = Vector2.zero;
        }

        public void OnDrag(PointerEventData eventData)
        {
            targetCamera = null;
            if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
            {
                targetCamera = canvas.worldCamera;
            }

            Vector2 position = RectTransformUtility.WorldToScreenPoint(targetCamera, background.position);
            Vector2 radius = background.sizeDelta / 2;
            inputDirection = (eventData.position - position) / (radius * canvas.scaleFactor);
            inputDirection = HandleInput(inputDirection);
            handle.anchoredPosition = inputDirection * radius * handleRange;
        }

        private Vector2 HandleInput(Vector2 inputDirection)
        {
            float sqrMagnitude = inputDirection.sqrMagnitude;
            if (sqrMagnitude < deadZone) return Vector2.zero;
            if (sqrMagnitude < 1) return inputDirection;
            return inputDirection.normalized;
        }

        private Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
        {
            Vector2 localPoint;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(baseRect, screenPosition, targetCamera, out localPoint))
            {
                Vector2 pivotOffset = baseRect.pivot * baseRect.sizeDelta;
                return localPoint - (background.anchorMax * baseRect.sizeDelta) + pivotOffset;
            }
            return Vector2.zero;
        }
    }
}
