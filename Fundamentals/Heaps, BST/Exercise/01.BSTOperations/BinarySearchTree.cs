namespace _01.BSTOperations
{
    using System;
    using System.Collections.Generic;

    public class BinarySearchTree<T> : IAbstractBinarySearchTree<T>
        where T : IComparable<T>
    {
        private int count = 0;
        public BinarySearchTree()
        {
        }

        public BinarySearchTree(Node<T> root)
        {
            this.Root = root;
            this.LeftChild = root.LeftChild;
            this.RightChild = root.RightChild;
        }

        public Node<T> Root { get; private set; }

        public Node<T> LeftChild { get; private set; }

        public Node<T> RightChild { get; private set; }

        public T Value => this.Root.Value;

        public int Count => this.count;

        public bool Contains(T element)
        {
            var currentNode = this.Root;

            while (element.CompareTo(currentNode.Value) != 0)
            {
                if (element.CompareTo(currentNode.Value) < 0)
                {
                    if (currentNode.LeftChild == null)
                    {
                        return false;
                    }
                    currentNode = currentNode.LeftChild;
                }
                else
                {
                    if (currentNode.RightChild == null)
                    {
                        return false;
                    }
                    currentNode = currentNode.RightChild;
                }

                if (currentNode.Value.Equals(element))
                {
                    return true;
                }
            }

            return false;
        }

        public void Insert(T element)
        {
            this.Insert(element, this.Root);
            this.count = this.count + 1;
        }

        public void Insert(T element, Node<T> node)
        {
            if (node == null)
            {
                node = new Node<T>(element, null, null);
                this.Root = node;
                return;
            }

            if (element.CompareTo(node.Value) < 0)
            {
                if (node.LeftChild == null)
                {
                    node.LeftChild = new Node<T>(element, null, null);
                    return;
                }
                this.Insert(element, node.LeftChild);
            }
            else
            {
                if (node.RightChild == null)
                {
                    node.RightChild = new Node<T>(element, null, null);
                    return;
                }
                this.Insert(element, node.RightChild);
            }
        }

        public IAbstractBinarySearchTree<T> Search(T element)
        {
            return this.Search(element, this.Root);
        }

        public IAbstractBinarySearchTree<T> Search(T element, Node<T> node)
        {
            if (node == null)
            {
                return null;
            }

            if (node.Value.Equals(element))
            {
                return new BinarySearchTree<T>(node);
            }

            if (element.CompareTo(node.Value) < 0)
            {
                return this.Search(element, node.LeftChild);
            }
            else
            {
                return this.Search(element, node.RightChild);
            }
        }

        public void EachInOrder(Action<T> action)
        {
            var elements = new List<T>();
            this.TraverseInOrder(this.Root, elements);
            foreach (var element in elements)
            {
                action.Invoke(element);
            }
        }

        public void TraverseInOrder(Node<T> node, List<T> elements)
        {
            if (node.LeftChild != null)
            {
                this.TraverseInOrder(node.LeftChild, elements);
            }

            elements.Add(node.Value);

            if (node.RightChild != null)
            {
                this.TraverseInOrder(node.RightChild, elements);
            }
        }

        public List<T> Range(T lower, T upper)
        {
            var result = new List<T>();

            this.ContainsRange(this.Root, result, lower, upper);
            return result;
        }

        public void ContainsRange(Node<T> node, List<T> result, T lower, T upper)
        {
            if (node == null)
            {
                return;
            }
            if (node.Value.CompareTo(lower) >= 0 && node.Value.CompareTo(upper) <= 0)
            {
                result.Add(node.Value);
                this.ContainsRange(node.LeftChild, result, lower, upper);
                this.ContainsRange(node.RightChild, result, lower, upper);
            }
            else if (lower.CompareTo(node.Value) < 0)
            {
                this.ContainsRange(node.LeftChild, result, lower, upper);
            }
            else
            {
                this.ContainsRange(node.RightChild, result, lower, upper);
            }
        }

        public void DeleteMin()
        {
            if (this.Root == null)
            {
                throw new InvalidOperationException();
            }
            this.DeleteMin(this.Root);
            this.count = this.count - 1;
        }

        private void DeleteMin(Node<T> node)
        {
            if (node.LeftChild.LeftChild != null)
            {
                this.DeleteMin(node.LeftChild);
            }
            else
            {
                if (node.LeftChild.RightChild != null)
                {
                    node.LeftChild = node.LeftChild.RightChild;
                }
                else
                {
                    node.LeftChild = null;
                }
            }
        }

        public void DeleteMax()
        {
            if (this.Root == null)
            {
                throw new InvalidOperationException();
            }
            this.DeleteMax(this.Root);
            this.count = this.count - 1;
        }

        private void DeleteMax(Node<T> node)
        {
            if (node.RightChild.RightChild != null)
            {
                this.DeleteMax(node.RightChild);
            }
            else
            {
                if (node.RightChild.LeftChild != null)
                {
                    node.RightChild = node.RightChild.LeftChild;
                }
                else
                {
                    node.RightChild = null;
                }
            }
        }

        public int GetRank(T element)
        {
            var list = new List<int>();
            this.GetRankRecursive(element, this.Root, list);

            return list.Count;
        }

        private void GetRankRecursive(T element, Node<T> node, List<int> list)
        {
            if (node == null)
            {
                return;
            }

            if (node.Value.CompareTo(element) > 0)
            {
                if (node.LeftChild != null)
                {
                    this.GetRankRecursive(element, node.LeftChild, list);
                }

                return;
            }

            list.Add(1);

            if (node.Value.CompareTo(element) < 0)
            {
                this.GetRankRecursive(element, node.LeftChild, list);
                this.GetRankRecursive(element, node.RightChild, list);
            }
        }
    }
}
