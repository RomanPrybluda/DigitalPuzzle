namespace DigitalPuzzle
{
    static class FileParser
    {
        public static Fragment[] ParseFragments(string filePath)
        {
            return File.ReadAllLines(filePath)
                .Where(line => line.Length == 6)
                .Select(line => new Fragment
                {
                    Value = line,
                    Start = int.Parse(line.Substring(0, 2)),
                    End = int.Parse(line.Substring(4, 2))
                })
                .ToArray();
        }
    }
}