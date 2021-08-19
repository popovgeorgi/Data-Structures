namespace AVLTree
{
    using System;

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

            int comparator = item.CompareTo(node.Value);
            if (comparator < 0)
            {
                node.Left = this.Insert(node.Left, item);
            }
            else if (comparator > 0)
            {
                node.Right = this.Insert(node.Right, item);
            }

            this.UpdateHeight(node);
            return this.Balance(node);
        }

        private Node<T> Balance(Node<T> node)
        {
            int balance = Height(node.Left) - Height(node.Right);
            if (balance == -2) // right is too heavy
            {
                balance = Height(node.Right.Left) - Height(node.Right.Right);
                if (balance > 0)
                {
                    return this.RightLeftImbalance(node);
                }
                else
                {
                    return this.RightRightImbalance(node);
                }
            }
            else if (balance == 2) //left is too heavy
            {
                balance = Height(node.Left.Left) - Height(node.Left.Right);
                if (balance > 0)
                {
                    return this.LeftLeftImbalance(node);
                }
                else
                {
                    return this.LeftRightImbalance(node);
                }
            }

            return node;
        }

        private Node<T> RightLeftImbalance(Node<T> node)
        {
            // rotate right child to right
            node.Right = this.RotateToRight(node.Right);
            //rotate main node to left
            var newNode = this.RotateToLeft(node);

            return newNode;
        }

        private Node<T> RightRightImbalance(Node<T> node)
        {
            return this.RotateToLeft(node);
        }

        private Node<T> RotateToLeft(Node<T> node)
        {
            var newNode = node.Right;
            node.Right = newNode.Left;
            newNode.Left = node;

            this.UpdateHeight(node);
            this.UpdateHeight(newNode);

            return newNode;
        }

        private Node<T> RotateToRight(Node<T> node)
        {
            var newNode = node.Left;
            node.Left = newNode.Right;
            newNode.Right = node;

            this.UpdateHeight(node);
            this.UpdateHeight(newNode);

            return newNode;
        }

        private Node<T> LeftLeftImbalance(Node<T> node)
        {
            return this.RotateToRight(node);
        }

        private Node<T> LeftRightImbalance(Node<T> node)
        {
            // rotate left child to left
            node.Left = this.RotateToLeft(node.Left);
            // rotate main node to right
            var newNode = this.RotateToRight(node);
            this.UpdateHeight(newNode);

            return newNode;
        }

        private static int Height(Node<T> node)
        {
            if (node == null)
            {
                return 0;
            }

            return node.Height;
        }

        private void UpdateHeight(Node<T> node)
        {
            int leftHeight = 0;
            int rightHeight = 0;

            if (node.Left != null)
            {
                leftHeight = node.Left.Height;
            }
            if (node.Right != null)
            {
                rightHeight = node.Right.Height;
            }

            node.Height = Math.Max(leftHeight, rightHeight) + 1;
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
    }
}
