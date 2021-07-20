namespace _02.LowestCommonAncestor
{
    using System;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
        where T : IComparable<T>
    {
        public BinaryTree(
            T value,
            BinaryTree<T> leftChild,
            BinaryTree<T> rightChild)
        {
            this.Value = value;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
        }

        public T Value { get; set; }

        public BinaryTree<T> LeftChild { get; set; }

        public BinaryTree<T> RightChild { get; set; }

        public BinaryTree<T> Parent { get; set; }

        public T FindLowestCommonAncestor(T first, T second)
        {
            return this.LowestCommon(first, second, this);
        }

        public T LowestCommon(T first, T second, BinaryTree<T> tree)
        {
            if (tree.Value.CompareTo(first) > 0 && tree.Value.CompareTo(second) < 0)
            {
                return tree.Value;
            }
            else if (tree.Value.CompareTo(first) < 0)
            {
                return this.LowestCommon(first, second, tree.RightChild);
            }
            else
            {
                return this.LowestCommon(first, second, tree.LeftChild);
            }
        }
    }
}
