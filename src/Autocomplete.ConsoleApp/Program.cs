namespace Autocomplete.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var commands = new[]
            {
                "biscuits",
                "hello home",
                "hello world",
                "hello wombats form another world",
                "this is a test",
                "this is how you eat a biscuit"
            };

            var commandStr = ConsoleUtility.ReadAutocomplete(commands);

            Console.WriteLine();
            Console.WriteLine($"Result: {commandStr}");
        }
    }
}