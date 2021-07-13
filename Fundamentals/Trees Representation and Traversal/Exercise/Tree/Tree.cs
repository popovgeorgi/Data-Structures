namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> children;

        public Tree()
        {
            this.children = new List<Tree<T>>();
        }

        public Tree(T key, params Tree<T>[] children)
        {
            this.children = new List<Tree<T>>();
            this.Key = key;
            foreach (var child in children)
            {
                this.children.Add(child);
                child.Parent = this;
            }
        }

        public T Key { get; private set; }

        public Tree<T> Parent { get; private set; }

        public IReadOnlyCollection<Tree<T>> Children
            => this.children.AsReadOnly();

        public void AddChild(T parentNode, Tree<T> child)
        {
            var parent = this.FindBfs(parentNode);
            child.Parent = parent;
            parent.children.Add(child);
        }

        public void AddParent(Tree<T> parent)
        {
            throw new NotImplementedException();
        }

        public string GetAsString()
        {
            var output = new List<string>();
            this.DfsStringGenerator(this, 0, output);

            return string.Join("\r\n", output);
        }

        public Tree<T> GetDeepestLeftomostNode()
        {
            var queue = new Queue<Tree<T>>();
            this.DfsDeepestNode(this, queue);
            return queue.Dequeue();
        }

        public List<T> GetLeafKeys()
        {
            var list = new List<T>();
            this.DfsFindLeafKeys(this, list);

            return list;
        }

        public List<T> GetMiddleKeys()
        {
            var list = new List<T>();
            this.DfsGetMiddleNodes(this, list);

            return list;
        }

        public List<T> GetLongestPath()
        {
            var results = new Dictionary<T[], int>();
            var list = new List<T>();

            this.DfsLongestPath(this, list, results);

            var maxValue = results.Max(x => x.Value);
            var descendingGroups = results.GroupBy(r => r.Value);

            var output = new List<T>();

            if (descendingGroups.Where(g => g.Key == maxValue).All(x => x.Key > 1))
            {
                var array = results.FirstOrDefault().Key;
                output = this.FillArrayInAList(array);
            }
            else
            {
                var array = results.Where(r => r.Value == maxValue).FirstOrDefault().Key;
                output = this.FillArrayInAList(array);
            }

            return output;
        }

        public List<List<T>> PathsWithGivenSum(int sum)
        {
            var list = new List<T>();
            var results = new List<List<T>>();
            this.DfsPathGivenSum(this, list, sum, results);

            return results;
        }

        public List<Tree<T>> SubTreesWithGivenSum(int sum)
        {
            var subtrees = new List<Tree<T>>();
            this.DfsSubtrees(this, subtrees);


            var output = new List<Tree<T>>();
            foreach (var subtree in subtrees)
            {
                var currentSum = BfsSum(subtree);

                if (currentSum == sum)
                {
                    output.Add(subtree);
                }
            }

            return output;
        }

        private int BfsSum(Tree<T> subtree)
        {
            var sum = 0;

            var queue = new Queue<Tree<T>>();
            queue.Enqueue(subtree);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                sum += int.Parse(node.Key.ToString());

                foreach (var child in node.children)
                {
                    queue.Enqueue(child);
                }
            }

            return sum;
        }

        private List<T> FillArrayInAList(T[] array)
        {
            var output = new List<T>();
            for (int i = 0; i < array.Length; i++)
            {
                output.Add(array[i]);
            }

            return output;
        }


        private void DfsStringGenerator(Tree<T> node, int level, List<string> output)
        {
            output.Add(new string(' ', level) + node.Key.ToString());

            foreach (var child in node.children)
            {
                this.DfsStringGenerator(child, level + 2, output);
            }
        }


        private Tree<T> FindBfs(T parentNode)
        {
            var queue = new Queue<Tree<T>>();
            queue.Enqueue(this);

            while (queue.Count != 0)
            {
                var element = queue.Dequeue();
                if (element.Key.Equals(parentNode))
                {
                    return element;
                }

                foreach (var child in element.children)
                {
                    queue.Enqueue(child);
                }
            }

            throw new ArgumentNullException();
        }

        private void DfsDeepestNode(Tree<T> node, Queue<Tree<T>> queue)
        {
            foreach (var child in node.children)
            {
                this.DfsDeepestNode(child, queue);
            }

            queue.Enqueue(node);
        }

        private void DfsFindLeafKeys(Tree<T> node, List<T> list)
        {
            foreach (var child in node.children)
            {
                this.DfsFindLeafKeys(child, list);
            }

            if (node.children.Count == 0)
            {
                list.Add(node.Key);
            }
        }

        private void DfsGetMiddleNodes(Tree<T> node, List<T> list)
        {
            foreach (var child in node.children)
            {
                this.DfsGetMiddleNodes(child, list);
            }

            if (node.Parent != null && node.children.Count > 0)
            {
                list.Add(node.Key);
            }
        }

        private void DfsLongestPath(Tree<T> node, List<T> list, Dictionary<T[], int> results)
        {
            list.Add(node.Key);

            foreach (var child in node.children)
            {
                this.DfsLongestPath(child, list, results);
            }

            var currentPath = new T[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                currentPath[i] = list[i];
            }
            results.Add(currentPath, list.Count - 1);

            list.RemoveAt(list.Count - 1);
        }

        private void DfsPathGivenSum(Tree<T> node, List<T> list, int sum, List<List<T>> results)
        {
            list.Add(node.Key);

            foreach (var child in node.children)
            {
                this.DfsPathGivenSum(child, list, sum, results);
            }

            Func<T, int> selector = str => int.Parse(str.ToString());
            var currentPathSum = list.Sum(selector);
            if (currentPathSum == sum)
            {
                results.Add(list.ToList());
            }

            list.RemoveAt(list.Count - 1);
        }

        private void DfsSubtrees(Tree<T> node, List<Tree<T>> subtrees)
        {
            subtrees.Add(node);

            foreach (var child in node.children)
            {
                this.DfsSubtrees(child, subtrees);
            }
        }
    }
}
