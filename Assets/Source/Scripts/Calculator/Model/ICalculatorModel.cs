namespace Calculator.Model
{
    public interface ICalculatorModel
    {
        string CurrentExpression { get; }
        System.Collections.Generic.IReadOnlyList<string> History { get; }
        string Evaluate(string expression);

        void SetExpression(string expression);
        void LoadState();
        void SaveState();
    }
}
