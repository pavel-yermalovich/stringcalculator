using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class Calculator
    {
        private readonly List<string> _delimiters = new List<string> { ",", "\n" }; 
        
        public int Add(string input)
        {
            if (input == string.Empty)
                return 0;

            const string delimiterPrefix = "//";

            if (input.StartsWith(delimiterPrefix))
            {
                ParseDelimiters(input, delimiterPrefix);
                input = input.Substring(input.IndexOf('\n') + 1);
            }

            var numbers = input.Split(_delimiters.ToArray(), StringSplitOptions.None)
                .Select(int.Parse)
                .Where(n => n <= 1000).ToList();

            ValidateNegatives(numbers);

            return numbers.Sum();
        }

        private void ValidateNegatives(IEnumerable<int> numbers)
        {
            var negatives = numbers.Where(n => n < 0).ToList();
            if (negatives.Any())
                throw new ArgumentException("negatives not allowed: " + string.Join(", ", negatives));
        }

        private void ParseDelimiters(string input, string delimiterPrefix)
        {
            var regex = new Regex(@"\[(.*?)\]");
            var matches = regex.Matches(input);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    _delimiters.Add(match.Groups[1].ToString());
                }
            }
            else
            {
                _delimiters.Add(input[delimiterPrefix.Length].ToString());
            }
        }
    }
}
