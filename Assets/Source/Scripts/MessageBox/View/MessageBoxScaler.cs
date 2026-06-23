using UnityEngine;

namespace MessageBox
{
    public class MessageBoxScaler : MonoBehaviour
    {
        [SerializeField] private RectTransform _calculator;

        private RectTransform _rect;

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            _rect.sizeDelta = _calculator.sizeDelta;
        }
    }
}
