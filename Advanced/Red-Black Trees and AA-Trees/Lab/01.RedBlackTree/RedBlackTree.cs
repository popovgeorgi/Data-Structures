namespace _01.RedBlackTree
{
    using System;
    using System.Collections.Generic;

    public partial class RedBlackTree<T>
        : IBinarySearchTree<T> where T : IComparable
    {
        private int totalCount = 0;

        private Node root;

        private Node addedNode;

        private RedBlackTree(Node node)
        {
            this.PreOrderCopy(node);
        }

        public RedBlackTree()
        {
        }

        public void Insert(T element)
        {
            if (this.root == null)
            {
                this.root = new Node(element);
                this.root.Color = Color.Black;
                this.totalCount++;
                return;
            }
            this.root = this.Insert(element, this.root);
            this.totalCount++;

            this.Rebalance(this.addedNode);
        }

        private Node Insert(T element, Node node)
        {
            if (node == null)
            {
                var newlyAddedNode = new Node(element);
                return newlyAddedNode;
            }
            else if (element.CompareTo(node.Value) < 0)
            {
                var newNode = this.Insert(element, node.Left);
                if (node.Left == null)
                {
                    node.Left = newNode;
                    newNode.Parent = node;
                    this.addedNode = newNode;
                }
            }
            else if (element.CompareTo(node.Value) > 0)
            {
                var newNode = this.Insert(element, node.Right);
                if (node.Right == null)
                {
                    node.Right = newNode;
                    newNode.Parent = node;
                    this.addedNode = newNode;
                }
            }

            node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);
            return node;
        }

        private void Rebalance(Node node)
        {
            if (node == this.root)
            {
                node.Color = Color.Black;
                return;
            }

            var parentNode = node.Parent;
            var grandFather = node.Parent?.Parent;

            if (this.DoubleRed(node))
            {
                var uncle = this.GetUncle(node);

                if (uncle == null || uncle.Color == Color.Black)
                {
                    var newGrandFather = this.Restructure(grandFather);
                    if (grandFather == this.root)
                    {
                        this.root = newGrandFather;
                    }
                    else if (newGrandFather.Value.CompareTo(newGrandFather.Parent.Value) >= 0)
                    {
                        newGrandFather.Parent.Right = newGrandFather;
                    }
                    else if (newGrandFather.Value.CompareTo(newGrandFather.Parent.Value) < 0)
                    {
                        newGrandFather.Parent.Left = newGrandFather;
                    }

                    parentNode.Recolor();
                    grandFather.Recolor();

                    this.Rebalance(newGrandFather);
                }
                else
                {
                    this.Colorize(parentNode, uncle, grandFather);
                    this.Rebalance(grandFather);
                }
            }
        }

        private Node GetUncle(Node node)
        {
            Node parent = node.Parent;
            Node grandParent = parent?.Parent;

            if (parent.Value.CompareTo(grandParent.Value) < 0)
            {
                return grandParent.Right;
            }
            else
            {
                return grandParent.Left;
            }
        }

        private void Colorize(Node parentNode, Node uncle, Node grandFather)
        {
            parentNode.Recolor();
            uncle.Recolor();
            grandFather.Recolor();
        }

        private bool DoubleRed(Node node)
        {
            if (node.Color == Color.Red && node.Parent?.Color == Color.Red)
            {
                return true;
            }

            return false;
        }

        private Node Restructure(Node grandFather)
        {
            if (this.RightImbalance(grandFather))
            {
                var rightChild = grandFather.Right;

                if (rightChild.Right.Color == Color.Red)
                {
                    return this.RightRightImbalance(grandFather);
                }
                else if (rightChild.Left.Color == Color.Red)
                {
                    return this.RightLeftImbalance(grandFather);
                }
            }
            else
            {
                var leftChild = grandFather.Left;

                if (leftChild.Left.Color == Color.Red)
                {
                    return this.LeftLeftImbalance(grandFather);
                }
                else if (leftChild.Right.Color == Color.Red)
                {
                    return this.LeftRightImbalance(grandFather);
                }
            }

            return grandFather;
        }

        private bool RightImbalance(Node grandFather)
        {
            if (grandFather.Left?.Color == Color.Black || grandFather.Left == null)
            {
                return true;
            }

            return false;
        }

        private Node LeftRightImbalance(Node node)
        {
            node.Left = this.RotateToLeft(node.Left);
            var newNode = this.RotateToRight(node);

            return newNode;
        }

        private Node LeftLeftImbalance(Node node)
        {
            return this.RotateToRight(node);
        }

        private Node RightLeftImbalance(Node node)
        {
            node.Right = this.RotateToRight(node.Right);
            var newNode = this.RotateToLeft(node);

            return newNode;
        }

        private Node RightRightImbalance(Node node)
        {
            return this.RotateToLeft(node);
        }

        private Node RotateToLeft(Node node)
        {
            var newNode = node.Right;
            if (newNode.Left != null)
                newNode.Left.Parent = node;
            node.Right = newNode.Left;
            newNode.Left = node;
            newNode.Parent = node.Parent;
            node.Parent = newNode;

            return newNode;
        }

        private Node RotateToRight(Node node)
        {
            var newNode = node.Left;
            if (newNode.Right != null)
                newNode.Right.Parent = node;
            node.Left = newNode.Right;
            newNode.Right = node;
            newNode.Parent = node.Parent;
            node.Parent = newNode;

            return newNode;
        }

        private void SwapColour(Node node)
        {
            if (node.Color == Color.Black)
            {
                node.Color = Color.Red;
            }
            else
            {
                node.Color = Color.Black;
            }
        }

        public bool Contains(T element)
        {
            Node current = this.FindElement(element);

            return current != null;
        }

        public void EachInOrder(Action<T> action)
        {
            this.EachInOrder(this.root, action);
        }

        public IBinarySearchTree<T> Search(T element)
        {
            Node current = this.FindElement(element);

            return new RedBlackTree<T>(current);
        }

        public void DeleteMin()
        {
            if (this.root == null)
            {
                throw new InvalidOperationException();
            }

            this.root = this.DeleteMin(this.root);
        }

        public T Select(int rank)
        {
            Node node = this.Select(rank, this.root);
            if (node == null)
            {
                throw new InvalidOperationException();
            }

            return node.Value;
        }

        public IEnumerable<T> Range(T startRange, T endRange)
        {
            Queue<T> queue = new Queue<T>();

            this.Range(this.root, queue, startRange, endRange);

            return queue;
        }

        public void Delete(T element)
        {
            if (this.root == null)
            {
                throw new InvalidOperationException();
            }
            this.root = this.Delete(element, this.root);
        }

        public void DeleteMax()
        {
            if (this.root == null)
            {
                throw new InvalidOperationException();
            }

            this.root = this.DeleteMax(this.root);
        }

        public int Count()
        {
            return this.totalCount;
        }

        public int Rank(T element)
        {
            return this.Rank(element, this.root);
        }

        public T Ceiling(T element)
        {

            return this.Select(this.Rank(element) + 1);
        }

        public T Floor(T element)
        {
            return this.Select(this.Rank(element) - 1);
        }

        private Node DeleteMin(Node node)
        {
            if (node.Left == null)
            {
                return node.Right;
            }

            node.Left = this.DeleteMin(node.Left);
            this.totalCount--;
            node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);

            return node;    
        }

        public void DFS()
        {
            this.DFS(this.root, 3);
        }

        private void DFS(Node node, int indent)
        {
            Console.BackgroundColor = ConsoleColor.White;
            if (node == null) return;

            this.DFS(node.Left, indent + 3);    
            Console.BackgroundColor = ConsoleColor.White;
            Console.Write(new string(' ', indent));
            Console.WriteLine(node);
            this.DFS(node.Right, indent + 3);
        }


        private Node FindElement(T element)
        {
            Node current = this.root;

            while (current != null)
            {
                if (current.Value.CompareTo(element) > 0)
                {
                    current = current.Left;
                }
                else if (current.Value.CompareTo(element) < 0)
                {
                    current = current.Right;
                }
                else
                {
                    break;
                }
            }

            return current;
        }

        private void PreOrderCopy(Node node)
        {
            if (node == null)
            {
                return;
            }

            this.Insert(node.Value);
            this.PreOrderCopy(node.Left);
            this.PreOrderCopy(node.Right);
        }

        

        private void Range(Node node, Queue<T> queue, T startRange, T endRange)
        {
            if (node == null)
            {
                return;
            }

            int nodeInLowerRange = startRange.CompareTo(node.Value);
            int nodeInHigherRange = endRange.CompareTo(node.Value);

            if (nodeInLowerRange < 0)
            {
                this.Range(node.Left, queue, startRange, endRange);
            }
            if (nodeInLowerRange <= 0 && nodeInHigherRange >= 0)
            {
                queue.Enqueue(node.Value);
            }
            if (nodeInHigherRange > 0)
            {
                this.Range(node.Right, queue, startRange, endRange);
            }
        }

        private void EachInOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }

            this.EachInOrder(node.Left, action);
            action(node.Value);
            this.EachInOrder(node.Right, action);
        }

        private int Count(Node node)
        {
            if (node == null)
            {
                return 0;
            }

            return node.Count;
        }

        private Node Delete(T element, Node node)
        {
            if (node == null)
            {
                return null;
            }

            int compare = element.CompareTo(node.Value);

            if (compare < 0)
            {
                node.Left = this.Delete(element, node.Left);
            }
            else if (compare > 0)
            {
                node.Right = this.Delete(element, node.Right);
            }
            else
            {
                if (node.Right == null)
                {
                    return node.Left;
                }
                if (node.Left == null)
                {
                    return node.Right;
                }

                Node temp = node;
                node = this.FindMin(temp.Right);
                node.Right = this.DeleteMin(temp.Right);
                node.Left = temp.Left;

            }
            node.Count = this.Count(node.Left) + this.Count(node.Right) + 1;

            return node;
        }

        private Node FindMin(Node node)
        {
            if (node.Left == null)
            {
                return node;
            }

            return this.FindMin(node.Left);
        }

        private Node DeleteMax(Node node)
        {
            if (node.Right == null)
            {
                return node.Left;
            }

            node.Right = this.DeleteMax(node.Right);
            node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);

            return node;
        }


        private int Rank(T element, Node node)
        {
            if (node == null)
            {
                return 0;
            }

            int compare = element.CompareTo(node.Value);

            if (compare < 0)
            {
                return this.Rank(element, node.Left);
            }

            if (compare > 0)
            {
                return 1 + this.Count(node.Left) + this.Rank(element, node.Right);
            }

            return this.Count(node.Left);
        }

        private Node Select(int rank, Node node)
        {
            if (node == null)
            {
                return null;
            }

            int leftCount = this.Count(node.Left);
            if (leftCount == rank)
            {
                return node;
            }

            if (leftCount > rank)
            {
                return this.Select(rank, node.Left);
            }

            return this.Select(rank - (leftCount + 1), node.Right);
        }

    }
}