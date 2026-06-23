using Calculator.Model;
using Calculator.View;
using MessageBox.Presenter;
using Debug = UnityEngine.Debug;

namespace Calculator.Presenter
{
    public class CalculatorPresenter
    {
        private readonly ICalculatorModel _model;
        private readonly ICalculatorView  _view;
        private readonly IMessageBoxPresenter _messageBox;

        private string _lastExpression = string.Empty;

        public CalculatorPresenter(
            ICalculatorModel model,
            ICalculatorView view,
            IMessageBoxPresenter messageBox)
        {
            _model = model;
            _view = view;
            _messageBox = messageBox;
        }

        public void Initialize()
        {
            _model.LoadState();
            _view.OnResultClicked += HandleResultClicked;
            _view.OnExpressionChanged += HandleExpressionChanged;

            _lastExpression = _model.CurrentExpression;
            _view.SetExpression(_model.CurrentExpression);
            _view.ShowHistory(_model.History);
        }

        public void Dispose()
        {
            _model.SaveState();
            _view.OnResultClicked -= HandleResultClicked;
            _view.OnExpressionChanged -= HandleExpressionChanged;
        }

        private void HandleExpressionChanged(string value)
        {
            _lastExpression = value;
            _model.SetExpression(value);
        }

        private void HandleResultClicked()
        {
            string entry = _model.Evaluate(_lastExpression);
            _view.ShowHistory(_model.History);

            bool isError = entry.EndsWith("=ERROR", System.StringComparison.OrdinalIgnoreCase);

            if (isError)
            {
                _view.SetExpression(string.Empty);

                _messageBox.Show("Please check the expression\nyou just entered", () =>
                {
                    _view.SetExpression(_lastExpression);
                });
            }
            else
            {
                _lastExpression = string.Empty;
                _model.SetExpression(string.Empty);
                _view.SetExpression(string.Empty);
            }
        }
    }
}
