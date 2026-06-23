using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MessageBox.View
{
    public class MessageBoxView : MonoBehaviour, IMessageBoxView
    {
        [SerializeField] private GameObject _panel;
        [SerializeField] private TMP_Text _messageText;
        [SerializeField] private Button _confirmBtn;

        public event Action OnConfirmed;

        private void Start()
        {
            _confirmBtn.onClick.AddListener(() => OnConfirmed?.Invoke());
            _panel.SetActive(false);
        }

        public void Show(string message)
        {
            _messageText.text = message;
            _panel.SetActive(true);
        }

        public void Hide()
        {
            _panel.SetActive(false);
        }
    }
}
