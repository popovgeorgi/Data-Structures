namespace Tree
{
    using System.Collections.Generic;

    interface IAbstractTree<T>
    {
        T Value { get; }
        Node<T> Parent { get; }
        IReadOnlyCollection<Node<T>> Children { get; }

        ICollection<T> OrderBfs();

        ICollection<T> OrderDfs();

        void AddChild(T parentKey, Node<T> child);

        void RemoveNode(T nodeKey);

        void Swap(T firstKey, T secondKey);
    }
}
