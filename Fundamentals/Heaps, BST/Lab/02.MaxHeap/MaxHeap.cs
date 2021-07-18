namespace _02.MaxHeap
{
    using System;
    using System.Collections.Generic;

    public class MaxHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> heap;

        public MaxHeap()
        {
            this.heap = new List<T>();
        }

        public int Size { get { return this.heap.Count; } }

        public void Add(T element)
        {
            this.heap.Add(element);
            this.HeapifyUp(this.Size - 1);
        }

        public T Peek()
        {
            return this.heap[0];
        }

        private void HeapifyUp(int index)
        {
            var parentIndex = (index - 1) / 2;

            while (index > 0 && this.IsGreater(index, parentIndex))
            {
                this.Swap(index, parentIndex);
                index = parentIndex;
                parentIndex = (index - 1) / 2;
            }
        }

        private bool IsGreater(int index, int parentIndex)
        {
            if (this.heap[parentIndex].CompareTo(this.heap[index]) < 0)
            {
                return true;
            }

            return false;
        }

        private void Swap(int index, int parentIndex)
        {
            var parent = this.heap[parentIndex];
            this.heap[parentIndex] = this.heap[index]; 
            this.heap[index] = parent;
        }
    }
}
