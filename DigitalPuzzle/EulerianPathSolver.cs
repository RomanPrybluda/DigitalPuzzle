using System.Text;

namespace DigitalPuzzle
{
    static class EulerianPathSolver
    {
        public static string FindLongestSequence(Fragment[] fragments)
        {
            var (inDegree, outDegree, adj) = BuildGraph(fragments);
            var startNode = FindStartNode(inDegree, outDegree, adj);

            // Инициализируем стек и путь
            var edgeStack = new Stack<Fragment>();
            var result = new Stack<Fragment>();
            int current = startNode;

            // Алгоритм Хирхольцера (итеративная версия)
            while (edgeStack.Count > 0 || adj[current].Count > 0)
            {
                if (adj[current].Count == 0)
                {
                    result.Push(edgeStack.Pop());
                    current = result.Peek().Start;
                }
                else
                {
                    edgeStack.Push(adj[current].Last());
                    adj[current].RemoveAt(adj[current].Count - 1);
                    current = edgeStack.Peek().End;
                }
            }

            // Добавляем оставшиеся ребра
            while (edgeStack.Count > 0)
                result.Push(edgeStack.Pop());

            return MergeFragments(result.ToArray());
        }

        private static (Dictionary<int, int>, Dictionary<int, int>, Dictionary<int, List<Fragment>>)
            BuildGraph(Fragment[] fragments)
        {
            var inDegree = new Dictionary<int, int>();
            var outDegree = new Dictionary<int, int>();
            var adj = new Dictionary<int, List<Fragment>>();

            // Инициализация для всех возможных узлов
            foreach (var frag in fragments)
            {
                if (!adj.ContainsKey(frag.Start)) adj[frag.Start] = new List<Fragment>();
                if (!adj.ContainsKey(frag.End)) adj[frag.End] = new List<Fragment>();

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
            // Правило 1: Ищем вершину с outDegree = inDegree + 1
            foreach (var node in adj.Keys)
            {
                if (outDegree.GetValueOrDefault(node, 0) - inDegree.GetValueOrDefault(node, 0) == 1)
                    return node;
            }

            // Правило 2: Ищем первую вершину с исходящими ребрами
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
                // Убираем перекрывающиеся 2 цифры
                result.Append(path[i].Value.Substring(2));
            }

            return result.ToString();
        }
    }
}