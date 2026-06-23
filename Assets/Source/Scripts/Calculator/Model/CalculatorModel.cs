using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Calculator.Model
{
    public class CalculatorModel : ICalculatorModel
    {
        private const string _keyExpression = "calc_expression";
        private const string _keyHistory = "calc_history";
        private const char _historySeparator = '\n';

        private static readonly Regex _validPattern = new Regex(@"^\d+\+\d+$", RegexOptions.Compiled);

        private string _currentExpression = string.Empty;
        private readonly List<string> _history = new List<string>();

        public string CurrentExpression => _currentExpression;
        public IReadOnlyList<string> History => _history.AsReadOnly();

        public string Evaluate(string expression)
        {
            string trimmed = expression?.Trim() ?? string.Empty;
            string entry;

            if (_validPattern.IsMatch(trimmed))
            {
                int plusIndex = trimmed.IndexOf('+');
                long a = long.Parse(trimmed.Substring(0, plusIndex));
                long b = long.Parse(trimmed.Substring(plusIndex + 1));
                long result = a + b;
                entry = $"{trimmed}={result}";
            }
            else
            {
                entry = $"{trimmed}=ERROR";
            }

            _history.Insert(0, entry);
            return entry;
        }

        public void SetExpression(string expression)
        {
            _currentExpression = expression ?? string.Empty;
        }

        public void LoadState()
        {
            _currentExpression = PlayerPrefs.GetString(_keyExpression, string.Empty);

            _history.Clear();
            string raw = PlayerPrefs.GetString(_keyHistory, string.Empty);
            if (!string.IsNullOrEmpty(raw))
            {
                string[] entries = raw.Split(_historySeparator);
                foreach (string entry in entries)
                    if (!string.IsNullOrEmpty(entry))
                        _history.Add(entry);
            }
        }

        public void SaveState()
        {
            PlayerPrefs.SetString(_keyExpression, _currentExpression);
            PlayerPrefs.SetString(_keyHistory, string.Join(_historySeparator.ToString(), _history));
            PlayerPrefs.Save();
        }
    }
}
