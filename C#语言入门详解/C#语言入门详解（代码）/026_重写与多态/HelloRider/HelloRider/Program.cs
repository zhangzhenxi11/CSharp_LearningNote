namespace HelloRider {
    class Program {
        static void Main(string[] args) {
            var values = Enumerable.Range(0, 10).ToArray();
            var bst = GetTree(values, 0, values.Length - 1);
            DFS(bst);
            Console.WriteLine("=====");
            BFS(bst);
        }

        static Node GetTree(int[] values, int li, int hi) {//二叉树
            if (li > hi) return null;
            var mi = li + (hi - li) / 2;
            var node = new Node(values[mi]);
            node.Left = GetTree(values, li, mi - 1);
            node.Right = GetTree(values, mi + 1, hi);
            return node;
        }

        static void DFS(Node node) {//深度优先遍历DFS
            if (node == null) return;
            DFS(node.Left);
            Console.WriteLine(node.Value);
            DFS(node.Right);
        }

        static void BFS(Node root) {//广度优先遍历BFS
            var q = new Queue<Node>();  //Queue表示先进先出的对象集合
            q.Enqueue(root);    //Enqueue将一个对象添加到 Queue<T> 的末尾。
            while (q.Count > 0) {
                var node = q.Dequeue();
                Console.WriteLine(node.Value);
                if (node.Left != null) q.Enqueue(node.Left);
                if (node.Right != null) q.Enqueue(node.Right);
            }
        }
    }

    class Node {//节点
        public int Value { get; set; } //prop快速创建属性
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node(int value) { //Alt+Enter创建构造器
            Value = value;
        }
    }
}