namespace DigitalPuzzle
{
    static class SequenceBuilder
    {
        public static string FindLongestSequence(FragmentData[] fragments)
        {
            var graph = new Dictionary<int, List<FragmentData>>();
            foreach (var frag in fragments)
            {
                if (!graph.ContainsKey(frag.StartCode))
                    graph[frag.StartCode] = new List<FragmentData>();

                graph[frag.StartCode].Add(frag);
            }

            string longestSequence = "";
            foreach (var frag in fragments)
            {
                var visited = new HashSet<FragmentData>();
                string sequence = DFS(frag, visited, graph);
                if (sequence.Length > longestSequence.Length)
                    longestSequence = sequence;
            }

            return longestSequence;
        }

        private static string DFS(FragmentData current, HashSet<FragmentData> visited, Dictionary<int, List<FragmentData>> graph)
        {
            visited.Add(current);
            string bestPath = current.FullFragment;

            if (graph.TryGetValue(current.EndCode, out var nextFragments))
            {
                foreach (var next in nextFragments)
                {
                    if (!visited.Contains(next))
                    {
                        string path = DFS(next, new HashSet<FragmentData>(visited), graph);
                        string merged = MergeFragments(current.FullFragment, path);
                        if (merged.Length > bestPath.Length)
                            bestPath = merged;
                    }
                }
            }

            return bestPath;
        }

        private static string MergeFragments(string current, string next)
            => current + next.Substring(2);
    }
}