
namespace _03.AVL
{
    using System;
    using System.Xml;

    public class AVL<T> where T : IComparable<T>
    {
        public Node<T> Root { get; private set; }

        public bool Contains(T item)
        {
            var node = this.Search(this.Root, item);
            return node != null;
        }

        public void Insert(T item)
        {
            this.Root = this.Insert(this.Root, item);
        }

        public void Delete(int value)
        {
            if (this.Root != null)
            {
                this.Root = this.Delete(this.Root, value);
            }
        }

        public Node<T> Delete(Node<T> root, int value)
        {
            if (root.Value.Equals(value))
            {
                //in case of leaf
                if (root.Left == null && root.Right == null)
                {
                    root = null;
                }
                else if (root.Left != null && root.Left == null)
                {
                    root = root.Left;
                }
                else if (root.Left == null && root.Right != null)
                {
                    root = root.Right;
                }
                else
                {
                    if (this.Root.Value.Equals(value))
                    {
                        // find largest node in left subtree
                        var largestNode = this.FindLargest(root.Left);
                        this.Delete(int.Parse(largestNode.Value.ToString()));
                        root.Value = largestNode.Value;
                    }
                    else
                    {
                        var newRoot = root.Left;
                        newRoot.Right = root.Right;
                        return newRoot;
                    }
                }

                return root;
            }

            if (value.CompareTo(root.Value) < 0)
            {
                root.Left = this.Delete(root.Left, value);
            }
            else if(value.CompareTo(root.Value) > 0)
            {
                root.Right = this.Delete(root.Right, value);
            }

            root = this.Balance(root);
            UpdateHeight(root);
            return root;
        }

        public void DeleteMin()
        {
            if (this.Root != null)
            {
                var minValue = this.FindMinValue(this.Root);
                this.Delete(int.Parse(minValue.ToString()));
            }
        }

        public void EachInOrder(Action<T> action)
        {
            this.EachInOrder(this.Root, action);
        }

        private Node<T> Insert(Node<T> node, T item)
        {
            if (node == null)
            {
                return new Node<T>(item);
            }

            int cmp = item.CompareTo(node.Value);
            if (cmp < 0)
            {
                node.Left = this.Insert(node.Left, item);
            }
            else if (cmp > 0)
            {
                node.Right = this.Insert(node.Right, item);
            }

            node = Balance(node);
            UpdateHeight(node);
            return node;
        }

        private Node<T> Balance(Node<T> node)
        {
            var balance = Height(node.Left) - Height(node.Right);
            if (balance > 1)
            {
                var childBalance = Height(node.Left.Left) - Height(node.Left.Right);
                if (childBalance < 0)
                {
                    node.Left = RotateLeft(node.Left);
                }

                node = RotateRight(node);
            }
            else if (balance < -1)
            {
                var childBalance = Height(node.Right.Left) - Height(node.Right.Right);
                if (childBalance > 0)
                {
                    node.Right = RotateRight(node.Right);
                }

                node = RotateLeft(node);
            }

            return node;
        }

        private T FindMinValue(Node<T> node)
        {
            if (node.Left == null)
            {
                return node.Value;
            }
            else
            {
                return this.FindMinValue(node.Left);
            }
        }

        private Node<T> FindLargest(Node<T> node)
        {
            if (node.Right == null)
            {
                return node;
            }
            else
            {
                return this.FindLargest(node.Right);
            }
        }

        private void UpdateHeight(Node<T> node)
        {
            node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
        }

        private Node<T> Search(Node<T> node, T item)
        {
            if (node == null)
            {
                return null;
            }

            int cmp = item.CompareTo(node.Value);
            if (cmp < 0)
            {
                return Search(node.Left, item);
            }
            else if (cmp > 0)
            {
                return Search(node.Right, item);
            }

            return node;
        }

        private void EachInOrder(Node<T> node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            this.EachInOrder(node.Left, action);
            action(node.Value);
            this.EachInOrder(node.Right, action);
        }

        private int Height(Node<T> node)
        {
            if (node == null)
            {
                return 0;
            }

            return node.Height;
        }

        private Node<T> RotateRight(Node<T> node)
        {
            var left = node.Left;
            node.Left = left.Right;
            left.Right = node;

            UpdateHeight(node);

            return left;
        }

        private Node<T> RotateLeft(Node<T> node)
        {
            var right = node.Right;
            node.Right = right.Left;
            right.Left = node;

            UpdateHeight(node);

            return right;
        }
    }
}
