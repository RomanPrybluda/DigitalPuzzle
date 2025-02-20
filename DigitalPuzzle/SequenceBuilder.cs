namespace DigitalPuzzle
{
    static class SequenceBuilder
    {
        public static string FindLongestSequence(string[] fragments)
        {

            var graph = new Dictionary<string, List<string>>();
            foreach (var fragment in fragments)
            {
                string start = fragment.Substring(0, 2);
                if (!graph.ContainsKey(start))
                {
                    graph[start] = new List<string>();
                }
                graph[start].Add(fragment);
            }


            string longestSequence = "";
            foreach (var fragment in fragments)
            {
                var visited = new HashSet<string>();
                string sequence = DFS(fragment, visited, graph);
                if (sequence.Length > longestSequence.Length)
                {
                    longestSequence = sequence;
                }
            }

            return longestSequence;
        }

        private static string DFS(string currentFragment, HashSet<string> visited, Dictionary<string, List<string>> graph)
        {
            visited.Add(currentFragment);
            string longestSequence = currentFragment;


            string suffix = currentFragment.Substring(currentFragment.Length - 2, 2);

            if (graph.ContainsKey(suffix))
            {
                foreach (var nextFragment in graph[suffix])
                {
                    if (!visited.Contains(nextFragment))
                    {
                        string sequence = DFS(nextFragment, new HashSet<string>(visited), graph);
                        string combinedSequence = currentFragment + sequence.Substring(2); // Убираем перекрывающиеся цифры
                        if (combinedSequence.Length > longestSequence.Length)
                        {
                            longestSequence = combinedSequence;
                        }
                    }
                }
            }

            return longestSequence;
        }
    }
}