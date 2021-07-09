namespace Problem02.Stack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Stack<T> : IAbstractStack<T>
    {
        private Node<T> head;

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

        public T Peek()
        {
            this.ThrowExceptionIfNoElements();

            return this.head.Value;
        }

        public T Pop()
        {
            this.ThrowExceptionIfNoElements();

            var oldHead = this.head;
            var newHead = this.head.Next;
            this.head = newHead;
            this.Count--;
            return oldHead.Value;

        }

        public void Push(T item)
        {
            if (this.Count == 0)
            {
                this.head = new Node<T>(item);
            }
            else
            {
                var newHead = new Node<T>(item);
                newHead.Next = this.head;
                this.head = newHead;
            }

            this.Count++;
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