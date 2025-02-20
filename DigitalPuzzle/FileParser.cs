namespace DigitalPuzzle
{
    static class FileParser
    {
        public static string[] ParseFile(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var fragments = new List<string>();

            foreach (var line in lines)
            {
                if (line.Length == 6)
                {
                    fragments.Add(line);
                }
            }

            return fragments.ToArray();
        }
    }
}