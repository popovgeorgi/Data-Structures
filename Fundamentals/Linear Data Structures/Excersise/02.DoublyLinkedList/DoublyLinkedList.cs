namespace Problem02.DoublyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class DoublyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> head;
        private Node<T> last;

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            var newHead = new Node<T>(item);

            if (this.Count == 0)
            {
                this.head = newHead;
                this.last = newHead;
            }
            else
            {
                if (this.Count == 1)
                {
                    this.last.Previous = newHead;
                }
                newHead.Next = this.head;
                this.head.Previous = newHead;
                this.head = newHead;
            }

            this.Count++;
        }

        public void AddLast(T item)
        {
            var newNode = new Node<T>(item);

            if (Count == 0)
            {
                this.head = newNode;
                this.last = newNode;
            }
            else
            {
                if (this.Count == 1)
                {
                    this.head.Next = newNode;
                }
                this.last.Next = newNode;
                newNode.Previous = this.last;
                this.last = newNode;
            }

            this.Count++;
        }

        public T GetFirst()
        {
            this.ThrowExceptionIfNoElements();

            return this.head.Item;
        }

        public T GetLast()
        {
            this.ThrowExceptionIfNoElements();

            return this.last.Item;
        }

        public T RemoveFirst()
        {
            this.ThrowExceptionIfNoElements();

            var first = this.head.Item;

            var oldHead = this.head;
            var newHead = this.head.Next;
            this.head = newHead;
            if (this.head != null)
            {
                this.head.Previous = null;
            }
            else
            {
                this.head = null;
                this.last = null;
                this.Count--;
                return first;
            }

            this.Count--;

            return oldHead.Item;
        }

        public T RemoveLast()
        {
            this.ThrowExceptionIfNoElements();

            var last = this.last.Item;

            var newLast = this.last.Previous;
            this.last = newLast;
            if (newLast != null)
            {
                newLast.Next = null;
            }
            else
            {
                this.head = null;
                this.last = null;
                this.Count--;
                return last;
            }

            this.Count--;

            return last;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentNode = this.head;
            while (currentNode != null)
            {
                yield return currentNode.Item;
                currentNode = currentNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void ThrowExceptionIfNoElements()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
        }
    }
}