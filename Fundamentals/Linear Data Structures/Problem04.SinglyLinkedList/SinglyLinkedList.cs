namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> head;
        private Node<T> last;

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            if (this.Count == 0)
            {
                this.head = new Node<T>(item);
                this.last = new Node<T>(item);
            }
            else
            {
                var newHead = new Node<T>(item);
                newHead.Next = this.head;
                this.head = newHead;
            }

            this.Count++;
        }

        public void AddLast(T item)
        {
            if (Count == 0)
            {
                var newNode = new Node<T>(item);
                this.head = newNode;
                this.last = newNode;
            }
            else
            {
                var newLast = new Node<T>(item);
                this.last.Next = newLast;
                this.last = newLast;
            }

            this.Count++;
        }

        public T GetFirst()
        {
            this.ThrowExceptionIfNoElements();

            return this.head.Value;
        }

        public T GetLast()
        {
            this.ThrowExceptionIfNoElements();

            return this.last.Value;
        }

        public T RemoveFirst()
        {
            this.ThrowExceptionIfNoElements();

            var oldHead = this.head;
            var newHead = this.head.Next;
            this.head = newHead;

            this.Count--;

            return oldHead.Value;
        }

        public T RemoveLast()
        {
            this.ThrowExceptionIfNoElements();

            var last = this.last.Value;

            if (this.Count == 1)
            {
                this.head = null;
                this.last = null;
                this.Count--;
                return last;
            }

            var currentNode = this.head;
            Node<T> preLast = null;

            while (currentNode.Next != null)
            {
                preLast = currentNode;
                currentNode = currentNode.Next;
            }

            this.last = preLast;
            this.last.Next = null;

            this.Count--;

            return last;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentNode = this.head;
            while (currentNode != null)
            {
                yield return currentNode.Value;
                currentNode = currentNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void ThrowExceptionIfNoElements()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
        }
    }
}