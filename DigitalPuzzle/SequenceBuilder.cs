using System.Text;

namespace DigitalPuzzle
{
    static class SequenceBuilder
    {
        public static string FindLongestSequence(int[][][] fragments)
        {
            var graph = BuildGraph(fragments);
            string longestPath = "";

            foreach (var fragment in fragments)
            {
                string path = DFS(fragment, new HashSet<int[][]>(fragments), graph);
                if (path.Length > longestPath.Length)
                {
                    longestPath = path;
                }
            }

            return longestPath;
        }

        private static Dictionary<int, List<int[][]>> BuildGraph(int[][][] fragments)
        {
            var graph = new Dictionary<int, List<int[][]>>();

            foreach (var fragment in fragments)
            {
                int conNode = fragment[2][0];

                if (!graph.ContainsKey(conNode))
                    graph[conNode] = new List<int[][]>();

                graph[conNode].Add(fragment);
            }
            return graph;
        }

        private static string DFS(int[][] current, HashSet<int[][]> unused, Dictionary<int, List<int[][]>> graph)
        {
            unused.Remove(current);
            var sequence = new StringBuilder(string.Join("", current.SelectMany(x => x)));
            int conNode = current[2][0];

            while (graph.ContainsKey(conNode))
            {
                int[][]? nextFragment = graph[conNode].FirstOrDefault(unused.Contains);
                if (nextFragment == null) break;

                unused.Remove(nextFragment);
                sequence.Append(string.Join("", nextFragment.SelectMany(x => x)));
                conNode = nextFragment[2][0];
            }

            return sequence.ToString();
        }
    }
}
