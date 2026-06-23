using Calculator.Model;
using Calculator.Presenter;
using Calculator.View;
using MessageBox.Presenter;
using MessageBox.View;
using UnityEngine;

namespace Calculator.Infrastructure
{
    public class CalculatorBootstrapper : MonoBehaviour
    {
        [Header("Calculator Module")]
        [SerializeField] private CalculatorView _calculatorView;

        [Header("MessageBox Module")]
        [SerializeField] private MessageBoxView _messageBoxView;

        private CalculatorPresenter _calculatorPresenter;

        private void Awake()  
        {
            ICalculatorModel model = new CalculatorModel();
            IMessageBoxPresenter msgPresenter = new MessageBoxPresenter(_messageBoxView);
            _calculatorPresenter = new CalculatorPresenter(model, _calculatorView, msgPresenter);
            _calculatorPresenter.Initialize();
        }

        private void OnApplicationQuit()
        {
            _calculatorPresenter?.Dispose();
        }
    }
}
