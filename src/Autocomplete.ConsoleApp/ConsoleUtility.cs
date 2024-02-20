using System.Globalization;
using System.Text;

namespace Autocomplete.ConsoleApp
{
    internal class ConsoleUtility
    {
        public static string ReadAutocomplete(string[] commands)
        {
            var builder = new StringBuilder();
            var input = Console.ReadKey(intercept: true);
            while (input.Key != ConsoleKey.Enter)
            {
                if (input.Key == ConsoleKey.Tab)
                {
                    HandleTabInput(builder, commands);
                }
                else
                {
                    HandleKeyInput(builder, commands, input);
                }

                input = Console.ReadKey(intercept: true);
            }
            Console.Write(input.KeyChar);
            return builder.ToString();
        }

        private static void ClearCurrentLine()
        {
            var currentLine = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLine);
        }

        private static void HandleTabInput(StringBuilder builder, IEnumerable<string> data)
        {
            var currentInput = builder.ToString();
            var matches = data
                .Where(item => item != currentInput && item.StartsWith(currentInput, true, CultureInfo.InvariantCulture))
                .ToArray();

            if (!matches.Any())
                return;

            if (matches.Count() == 1)
            {
                ClearCurrentLine();
                builder.Clear();

                var match = matches.First();
                Console.Write(match);
                builder.Append(match);
            }
            else
            {
                ClearCurrentLine();
                builder.Clear();

                for (var i = 0; i < matches.Count() - 1; i++)
                {
                    Console.Write($"{matches[i]}\t");
                }
                Console.Write($"{matches[matches.Count() - 1]}");

                Console.WriteLine();

                var longestMatch = GetLongestMatch(matches);
                Console.Write(longestMatch);
                builder.Append(longestMatch);
            }
        }

        private static string GetLongestMatch(IEnumerable<string> values)
        {
            if (values == null || !values.Any())
                return string.Empty;

            var shortest = values.OrderBy(v => v.Length).First();
            var length = 1;
            var longestMatch = shortest.Substring(0, length);

            while (values.All(v => v.StartsWith(longestMatch)))
                longestMatch = shortest.Substring(0, ++length);

            return longestMatch.Substring(0, length - 1);
        }

        private static void HandleKeyInput(StringBuilder builder, IEnumerable<string> data, ConsoleKeyInfo input)
        {
            var currentInput = builder.ToString();
            if (input.Key == ConsoleKey.Backspace && currentInput.Length > 0)
            {
                builder.Remove(builder.Length - 1, 1);
                ClearCurrentLine();

                currentInput = currentInput.Remove(currentInput.Length - 1);
                Console.Write(currentInput);
            }
            else
            {
                var key = input.KeyChar;
                builder.Append(key);
                Console.Write(key);
            }
        }
    }
}
