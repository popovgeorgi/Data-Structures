namespace _01.Hierarchy
{
    using System;
    using System.Collections.Generic;
    using System.Collections;

    public class Hierarchy<T> : IHierarchy<T>
    {
        private readonly Node<T> root;
        private readonly Dictionary<T, Node<T>> nodesByValue;

        public Hierarchy(T root)
        {
            this.nodesByValue = new Dictionary<T, Node<T>>();
            var rootNode = new Node<T>(root);
            nodesByValue.Add(root, rootNode);
            this.root = rootNode;
        }

        public int Count => this.nodesByValue.Count;

        public void Add(T element, T child)
        {
            if (!this.nodesByValue.ContainsKey(element))
            {
                throw new ArgumentException("Element does not exist in the tree!");
            }
            if (this.nodesByValue.ContainsKey(child))
            {
                throw new ArgumentException("Child already exists!");
            }

            var parent = this.nodesByValue[element];
            var elementNode = this.nodesByValue[element];
            var newElement = new Node<T>(child);
            newElement.Parent = elementNode;
            if (!this.nodesByValue.ContainsKey(child))
            {
                this.nodesByValue.Add(child, newElement);
            }
            newElement.Parent = parent;
            parent.Children.Add(newElement);
        }

        public void Remove(T element)
        {
            if (this.root.Value.Equals(element))
            {
                throw new InvalidOperationException("It is the root element!");
            }
            if (!this.nodesByValue.ContainsKey(element))
            {
                throw new ArgumentException("Element does not exist in the tree!");
            }

            if (!this.nodesByValue.ContainsKey(element))
            {

            }
            var elementNode = this.nodesByValue[element];
            this.nodesByValue.Remove(element);
            var elementChildren = elementNode.Children;
            var parent = elementNode.Parent;
            elementNode.Parent = null;
            parent.Children.Remove(elementNode);

            foreach (var item in elementChildren)
            {
                item.Parent = parent;
                parent.Children.Add(item);
            }
        }

        public IEnumerable<T> GetChildren(T element)
        {
            if (!this.nodesByValue.ContainsKey(element))
            {
                throw new ArgumentException("Element does not exist in the tree!");
            }
            var elementNode = this.nodesByValue[element];
            var result = new List<T>();
            foreach (var item in elementNode.Children)
            {
                result.Add(item.Value);
            }
            return result;
        }

        public T GetParent(T element)
        {
            if (!this.nodesByValue.ContainsKey(element))
            {
                throw new ArgumentException("Element does not exist in the tree!");
            }
            var elementNodeParent = this.nodesByValue[element].Parent;
            if (elementNodeParent == null)
            {
                return default(T);
            }
            return elementNodeParent.Value;
        }

        public bool Contains(T element)
        {
            return this.ContainsElement(element);
        }

        public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
        {
            var result = new List<T>();

            foreach (var item in this.nodesByValue.Keys)
            {
                if (other.nodesByValue.ContainsKey(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var queue = new Queue<Node<T>>();
            queue.Enqueue(this.root);

            while (queue.Count != 0)
            {
                var currentElement = queue.Dequeue();
                yield return currentElement.Value;

                foreach (var child in currentElement.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private bool ContainsElement(T value)
        {
            if (!this.nodesByValue.ContainsKey(value))
            {
                return false;
            }
            return true;
        }
    }
}