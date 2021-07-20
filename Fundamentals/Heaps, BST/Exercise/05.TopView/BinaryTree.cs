namespace _05.TopView
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
        where T : IComparable<T>
    {
        public BinaryTree(T value, BinaryTree<T> left, BinaryTree<T> right)
        {
            this.Value = value;
            this.LeftChild = left;
            this.RightChild = right;
        }

        public T Value { get; set; }

        public BinaryTree<T> LeftChild { get; set; }

        public BinaryTree<T> RightChild { get; set; }

        public List<T> TopView()
        {
            var right = new List<T>();
            var left = new List<T>();

            this.TopViewLeft(this, left);
            this.TopViewRight(this, right);

            return right.Concat(left).ToList();
        }

        private void TopViewRight(BinaryTree<T> tree, List<T> result)
        {
            result.Add(tree.Value);

            if (tree.RightChild != null)
            {
                this.TopViewRight(tree.RightChild, result);
            }
        }

        private void TopViewLeft(BinaryTree<T> tree, List<T> result)
        {
            if (tree.LeftChild != null)
            {
                result.Add(tree.LeftChild.Value);

                this.TopViewLeft(tree.LeftChild, result);
            }
        }
    }
}
