namespace _01.BinaryTree
{
    using System;
    using System.Collections.Generic;

    public class Node<T> : IAbstractBinaryTree<T>
    {
        public Node(T value
            , IAbstractBinaryTree<T> leftChild
            , IAbstractBinaryTree<T> rightChild)
        {
            this.Value = value;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
        }

        public T Value { get; private set; }

        public IAbstractBinaryTree<T> LeftChild { get; private set; }

        public IAbstractBinaryTree<T> RightChild { get; private set; }

        public string AsIndentedPreOrder(int indent)
        {
            var result = new List<string>();

            this.IndentOrderDfs(this, indent, result);

            return String.Join("\r\n", result);
        }

        private void IndentOrderDfs(IAbstractBinaryTree<T> node, int indent, List<string> result)
        {
            result.Add(new string(' ', indent) + node.Value);

            if (node.LeftChild != null)
            {
                this.IndentOrderDfs(node.LeftChild, indent + 2, result);
            }
            if (node.RightChild != null)
            {
                this.IndentOrderDfs(node.RightChild, indent + 2, result);
            }
        }

        public List<IAbstractBinaryTree<T>> InOrder()
        {
            var result = new List<IAbstractBinaryTree<T>>();

            this.InOrderDfs(this, result);

            return result;
        }
        
        private void InOrderDfs(IAbstractBinaryTree<T> node, List<IAbstractBinaryTree<T>> result)
        {
            if (node.LeftChild != null)
            {
                this.InOrderDfs(node.LeftChild, result);

                result.Add(node);
            }
            if (node.RightChild != null)
            {
                this.InOrderDfs(node.RightChild, result);
            }

            if (node.RightChild == null && node.LeftChild == null)
            {
                result.Add(node);
            }
        }

        public List<IAbstractBinaryTree<T>> PostOrder()
        {
            var result = new List<IAbstractBinaryTree<T>>();

            this.PostOrderDfs(this, result);

            return result;
        }

        private void PostOrderDfs(IAbstractBinaryTree<T> node, List<IAbstractBinaryTree<T>> result)
        {
            if (node.LeftChild != null)
            {
                this.PostOrderDfs(node.LeftChild, result);
            }
            if (node.RightChild != null)
            {
                this.PostOrderDfs(node.RightChild, result);
            }

            result.Add(node);
        }

        public List<IAbstractBinaryTree<T>> PreOrder()
        {
            var result = new List<IAbstractBinaryTree<T>>();

            this.PreOrderDfs(this, result);

            return result;
        }

        private void PreOrderDfs(IAbstractBinaryTree<T> node, List<IAbstractBinaryTree<T>> result)
        {
            result.Add(node);

            if (node.LeftChild != null)
            {
                this.PreOrderDfs(node.LeftChild, result);
            }
            if (node.RightChild != null)
            {
                this.PreOrderDfs(node.RightChild, result);
            }
        }

        public void ForEachInOrder(Action<T> action)
        {
            var result = this.InOrder();

            foreach (var node in result)
            {
                action.Invoke(node.Value);
            }
        }
    }
}
