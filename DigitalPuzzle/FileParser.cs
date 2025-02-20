namespace DigitalPuzzle
{
    static class FileParser
    {
        public static FragmentData[] ParseFile(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var fragments = new List<FragmentData>();

            foreach (var line in lines)
            {
                if (line.Length != 6) continue;

                int start = int.Parse(line.Substring(0, 2));
                int end = int.Parse(line.Substring(4, 2));

                fragments.Add(new FragmentData
                {
                    FullFragment = line,
                    StartCode = start,
                    EndCode = end
                });
            }

            return fragments.ToArray();
        }
    }
}