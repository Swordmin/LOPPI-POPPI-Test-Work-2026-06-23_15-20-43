using System;

namespace MessageBox.Presenter
{
    public interface IMessageBoxPresenter
    {
        void Show(string message, Action onClose = null);
    }
}
