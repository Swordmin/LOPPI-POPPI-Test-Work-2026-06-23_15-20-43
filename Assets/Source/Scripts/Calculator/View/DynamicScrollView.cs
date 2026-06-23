using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Calculator
{
    [RequireComponent(typeof(RectTransform))]
    public class DynamicScrollView : MonoBehaviour
    {
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private TMP_Text _historyText;
        [SerializeField] private Scrollbar _verticalScrollbar;
        [SerializeField] private float _lineHeight = 24f;
        [SerializeField] private float _maxHeight = 300f;

        private RectTransform _rect;
        private RectTransform _contentRect;
        private LayoutElement _layoutElement;

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
            _contentRect = _scrollRect.content; 

            _layoutElement = gameObject.GetComponent<LayoutElement>();
            if (_layoutElement == null)
                _layoutElement = gameObject.AddComponent<LayoutElement>();
        }

        public void UpdateHeight()
        {
            _historyText.ForceMeshUpdate();

            int lineCount = _historyText.textInfo.lineCount;
            float contentHeight = lineCount * _lineHeight;
            bool needsScroll = contentHeight > _maxHeight;

            if (_contentRect != null)
                _contentRect.sizeDelta = new Vector2(_contentRect.sizeDelta.x, contentHeight);

            float viewportHeight = needsScroll ? _maxHeight : contentHeight;
            _rect.sizeDelta = new Vector2(_rect.sizeDelta.x, viewportHeight);

            _scrollRect.vertical = needsScroll;

            if (_verticalScrollbar != null)
            {
                _verticalScrollbar.gameObject.SetActive(needsScroll);
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(
                _rect.parent.GetComponent<RectTransform>());
        }
    }
}