using System;

namespace MessageBox.View
{
    public interface IMessageBoxView
    {
        event Action OnConfirmed;

        void Show(string message);
        void Hide();
    }
}
