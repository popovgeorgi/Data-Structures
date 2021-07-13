namespace Tree
{
    using System;
    using System.Collections.Generic;

    public class Node<T> : IAbstractTree<T>
    {
        private readonly List<Node<T>> children;
        private bool isRootDeleted;

        public Node(T value)
        {
            this.Value = value;
            this.Parent = null;
            this.children = new List<Node<T>>();
        }

        public Node(T value, params Node<T>[] children)
            : this(value)
        {
            this.Value = value;

            foreach (var child in children)
            {
                child.Parent = this;
                this.children.Add(child);
            }
        }

        public T Value { get; private set; }
        public Node<T> Parent { get; private set; }
        public IReadOnlyCollection<Node<T>> Children => this.children.AsReadOnly();

        public ICollection<T> OrderBfs()
        {
            if (this.isRootDeleted)
            {
                return new List<T>();
            }

            var result = new List<T>();
            var queue = new Queue<Node<T>>();

            queue.Enqueue(this);

            while (queue.Count != 0)
            {
                var element = queue.Dequeue();
                result.Add(element.Value);

                foreach (var child in element.children)
                {
                    queue.Enqueue(child);
                }
            }

            return result;
        }

        public ICollection<T> OrderDfs()
        {
            if (this.isRootDeleted)
            {
                return new List<T>();
            }

            var result = new List<T>();

            this.Dfs(this, result);

            return result;
        }

        private void Dfs(Node<T> root, List<T> result)
        {
            foreach (var child in root.children)
            {
                this.Dfs(child, result);
            }

            result.Add(root.Value);
        }

        public void AddChild(T parentKey, Node<T> child)
        {
            var node = this.FindBfs(parentKey);

            var childNode = child;
            node.children.Add(childNode);
        }

        public void RemoveNode(T nodeKey)
        {
            var nodeToRemove = this.FindBfs(nodeKey);
            if (nodeToRemove == null)
            {
                throw new ArgumentNullException();
            }

            var parent = nodeToRemove.Parent;
            if (parent == null)
            {
                this.isRootDeleted = true;
            }
            else
            {
                parent.children.Remove(nodeToRemove);
            }
        }

        public void Swap(T firstKey, T secondKey)
        {
            var firstNode = this.FindBfs(firstKey);
            var secondNode = this.FindBfs(secondKey);

            if (firstNode == null || secondNode == null)
            {
                throw new ArgumentNullException();
            }

            var firstNodeParent = firstNode.Parent;
            if (firstNodeParent == null)
            {
                this.SwapRootNode(firstNode, secondNode);
                return;
            }

            var secondNodeParent = secondNode.Parent;
            if (secondNodeParent == null)
            {
                this.SwapRootNode(secondNode, firstNode);
                return;
            }

            var indexOfFirst = firstNodeParent.children.IndexOf(firstNode);
            var indexOfSecond = secondNodeParent.children.IndexOf(secondNode);

            firstNodeParent.children[indexOfFirst] = secondNode;
            secondNodeParent.children[indexOfSecond] = firstNode;
        }

        private Node<T> FindBfs(T parentValue)
        {
            var queue = new Queue<Node<T>>();
            queue.Enqueue(this);

            while (queue.Count != 0)
            {
                var currentNode = queue.Dequeue();
                if (currentNode.Value.Equals(parentValue))
                {
                    return currentNode;
                }

                foreach (var child in currentNode.children)
                {
                    queue.Enqueue(child);
                }
            }

            throw new ArgumentNullException();
        }
        private void SwapRootNode(Node<T> root, Node<T> node)
        {
            root.Value = node.Value;
            var nodeChildren = node.children;
            root.children.RemoveAll(x => 1 == 1);
            root.children.AddRange(nodeChildren);
        }
}
}
