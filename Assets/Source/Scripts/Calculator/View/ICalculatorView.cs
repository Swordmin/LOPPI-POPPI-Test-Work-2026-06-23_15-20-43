using System;
using System.Collections.Generic;

namespace Calculator.View
{
    public interface ICalculatorView
    {
        event Action OnResultClicked;
        event Action<string> OnExpressionChanged;

        void SetExpression(string expression);

        void ShowHistory(IReadOnlyList<string> history);
    }
}
