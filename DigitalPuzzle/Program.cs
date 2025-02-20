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

                int[][][] fragments = FileParser.ParseFile(filePath);

                string longestSequence = SequenceBuilder.FindLongestSequence(fragments);

                Console.WriteLine("\nLongest sequence: " + longestSequence + "\n");

            }
        }
    }
}