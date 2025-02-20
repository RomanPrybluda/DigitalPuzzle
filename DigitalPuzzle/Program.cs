namespace DigitalPuzzle
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter the full file path (example C:/fragment.txt) or type 'q' to quit:");
                string? filePath = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(filePath))
                    continue;

                if (filePath.ToLower() == "q")
                    break;

                if (!File.Exists(filePath))
                {
                    Console.WriteLine("\nFile not found. Try again or type 'q' to quit:");
                    continue;
                }

                string[] fragments = FileParser.ParseFile(filePath);

                if (fragments.Length == 0)
                {
                    Console.WriteLine("\nNo valid fragments found in the file.");
                    continue;
                }

                string longestSequence = SequenceBuilder.FindLongestSequence(fragments);

                Console.WriteLine("\nLongest sequence: " + longestSequence);
                Console.WriteLine("Length of longest sequence: " + longestSequence.Length + "\n");
            }
        }
    }
}