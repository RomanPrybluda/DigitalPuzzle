using System.Text;

namespace DigitalPuzzle
{
    static class EulerianPathSolver
    {
        public static string FindLongestSequence(Fragment[] fragments)
        {

            var (inDegree, outDegree, adj) = BuildGraph(fragments);
            int startNode = FindStartNode(inDegree, outDegree, adj);

            List<Fragment> path = new List<Fragment>();

            void DFS(int node)
            {

                while (adj[node].Count > 0)
                {
                    Fragment edge = adj[node].Last();
                    adj[node].RemoveAt(adj[node].Count - 1);
                    DFS(edge.End);
                    path.Add(edge);
                }
            }

            DFS(startNode);
            path.Reverse();

            return MergeFragments(path.ToArray());
        }

        private static (Dictionary<int, int> inDegree, Dictionary<int, int> outDegree, Dictionary<int, List<Fragment>> adj)
            BuildGraph(Fragment[] fragments)
        {
            var inDegree = new Dictionary<int, int>();
            var outDegree = new Dictionary<int, int>();
            var adj = new Dictionary<int, List<Fragment>>();

            foreach (var frag in fragments)
            {
                if (!adj.ContainsKey(frag.Start))
                    adj[frag.Start] = new List<Fragment>();
                if (!adj.ContainsKey(frag.End))
                    adj[frag.End] = new List<Fragment>();

                adj[frag.Start].Add(frag);
                outDegree[frag.Start] = outDegree.GetValueOrDefault(frag.Start, 0) + 1;
                inDegree[frag.End] = inDegree.GetValueOrDefault(frag.End, 0) + 1;
            }

            return (inDegree, outDegree, adj);
        }

        private static int FindStartNode(
            Dictionary<int, int> inDegree,
            Dictionary<int, int> outDegree,
            Dictionary<int, List<Fragment>> adj)
        {

            foreach (var node in adj.Keys)
            {
                if (outDegree.GetValueOrDefault(node, 0) - inDegree.GetValueOrDefault(node, 0) == 1)
                    return node;
            }

            foreach (var node in adj.Keys)
            {
                if (adj[node].Count > 0)
                    return node;
            }
            return adj.Keys.First();
        }

        private static string MergeFragments(Fragment[] path)
        {
            if (path.Length == 0) return "";

            StringBuilder result = new StringBuilder(path[0].Value);
            for (int i = 1; i < path.Length; i++)
            {
                result.Append(path[i].Value.Substring(2));
            }
            return result.ToString();
        }
    }
}
