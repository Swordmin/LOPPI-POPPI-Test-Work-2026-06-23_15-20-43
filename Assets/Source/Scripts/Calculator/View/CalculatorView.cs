using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Calculator.View
{
    public class CalculatorView : MonoBehaviour, ICalculatorView
    {
        public event Action OnResultClicked;
        public event Action<string> OnExpressionChanged;

        [SerializeField] private TMP_InputField _expressionInput;
        [SerializeField] private TMP_Text _historyText;
        [SerializeField] private Button _resultButton;
        [SerializeField] private ScrollRect _historyScrollRect;
        [SerializeField] private DynamicScrollView _dynamicScrollView;

        private bool _suppressChangeEvent;

        private void OnEnable()
        {
            _resultButton.onClick.AddListener(ResultClick);
            _expressionInput.onValueChanged.AddListener(InputHandle);
        }

        private void OnDestroy()
        {
            _resultButton.onClick.AddListener(ResultClick);
            _expressionInput.onValueChanged.RemoveListener(InputHandle);
        }

        public void SetExpression(string expression)
        {
            _suppressChangeEvent = true;
            _expressionInput.SetTextWithoutNotify(expression);
            _suppressChangeEvent = false;
        }

        public void ShowHistory(IReadOnlyList<string> history)
        {
            var sb = new StringBuilder();
            foreach (string entry in history)
            {
                sb.AppendLine(entry);
            }
            _historyText.text = sb.ToString().TrimEnd();

            Canvas.ForceUpdateCanvases();
            _dynamicScrollView.UpdateHeight();
            _historyScrollRect.verticalNormalizedPosition = 1f;
        }

        private void ResultClick() 
        {
            OnResultClicked?.Invoke();
        }

        private void InputHandle(string text) 
        {
            if (!_suppressChangeEvent)
                OnExpressionChanged?.Invoke(text);
        }
    }
}
