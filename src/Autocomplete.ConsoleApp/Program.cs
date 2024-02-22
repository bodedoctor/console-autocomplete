namespace Autocomplete.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new[]
            {
                "biscuits",
                "hello home",
                "hello world",
                "hello wombats from another world",
                "this is a test",
                "this is how you eat a biscuit"
            };

            var result = ConsoleUtility.ReadAutocomplete(options);

            Console.WriteLine();
            Console.WriteLine($"Result: {result}");
        }
    }
}