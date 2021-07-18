namespace _04.BinarySearchTree
{
    using System;

    public class BinarySearchTree<T> : IAbstractBinarySearchTree<T>
        where T : IComparable<T>
    {
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

        public bool Contains(T element)
        {
            return this.Contains(element, this.Root);
        }

        private bool Contains(T element, Node<T> node)
        {
            if (node == null)
            {
                return false;
            }

            if (element.Equals(node.Value))
            {
                return true;
            }

            if (element.CompareTo(node.Value) < 0)
            {
                return this.Contains(element, node.LeftChild);
            }
            else
            {
                return this.Contains(element, node.RightChild);
            }
        }

        public void Insert(T element)
        {
            this.Insert(element, this.Root);
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
    }
}
