using System;
using MessageBox.View;

namespace MessageBox.Presenter
{
    public class MessageBoxPresenter : IMessageBoxPresenter
    {
        private readonly IMessageBoxView _view;
        private Action _onClose;

        public MessageBoxPresenter(IMessageBoxView view)
        {
            _view = view;
            _view.OnConfirmed += HandleConfirmed;
        }

        public void Show(string message, Action onClose = null)
        {
            _onClose = onClose;
            _view.Show(message);
        }

        private void HandleConfirmed()
        {
            _view.Hide();
            _onClose?.Invoke();
            _onClose = null;
        }
    }
}
