namespace DigitalPuzzle
{
    static class FileParser
    {
        public static int[][][] ParseFile(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var parsedData = new List<int[][]>();

            foreach (var line in lines)
            {
                if (line.Length < 6) continue;

                var puzzle = new int[3][]
                {
                new int[] { int.Parse(line.Substring(0, 2)) },
                new int[] { int.Parse(line.Substring(2, 2)) },
                new int[] { int.Parse(line.Substring(4, 2)) }
                };

                parsedData.Add(puzzle);
            }

            return parsedData.ToArray();
        }
    }
}
