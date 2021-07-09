namespace Problem03.Queue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Queue<T> : IAbstractQueue<T>
    {
        private Node<T> head;
        private Node<T> last;

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            var currentNode = this.head;
            while (currentNode != null)
            {
                if (currentNode.Value.Equals(item))
                {
                    return true;
                }

                currentNode = currentNode.Next;
            }

            return false;
        }

        public T Dequeue()
        {
            this.ThrowExceptionIfNoElements();

            var oldHead = this.head;
            var newHead = this.head.Next;
            this.head = newHead;

            this.Count--;

            return oldHead.Value;
        }

        public void Enqueue(T item)
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

        public T Peek()
        {
            this.ThrowExceptionIfNoElements();

            return this.head.Value;
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