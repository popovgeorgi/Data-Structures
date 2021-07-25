using System;
using System.Collections.Generic;

namespace _02.Data
{
    public class PriorityQueue<T>
        where T : IComparable<T>
    {
        private List<T> heap = new List<T>();

        public int Size { get { return this.heap.Count; } }

        public List<T> AsList
        { 
            get 
            {
                return this.heap;
            } 
        }

        public T Dequeue()
        {
            var topElement = this.Peek();
            this.heap[0] = this.heap[this.Size - 1];
            this.heap.RemoveAt(this.Size - 1);
            if (this.heap.Count != 0)
            {
                this.HeapifyDownRecursive(0);
            }

            return topElement;
        }

        private void HeapifyDownRecursive(int index)
        {
            var leftChildIndex = 2 * index + 1;
            var rightChildIndex = 2 * index + 2;
            var maxChildIndex = leftChildIndex;

            if (leftChildIndex >= this.heap.Count) return;

            if ((rightChildIndex < this.heap.Count) && this.heap[leftChildIndex].CompareTo(this.heap[rightChildIndex]) < 0)
            {
                maxChildIndex = rightChildIndex;
            }

            if (this.heap[index].CompareTo(this.heap[maxChildIndex]) < 0)
            {
                this.Swap(maxChildIndex, index);
                this.HeapifyDownRecursive(maxChildIndex);
            }
        }

        private void HeapifyDownIterative(int index)
        {
            var currentElement = this.heap[index];

            var leftChildIndex = 2 * index + 1;
            var rightChildIndex = 2 * index + 2;

            var leftChild = default(T);
            var rightChild = default(T);

            while (leftChildIndex < this.Size || rightChildIndex < this.Size)
            {
                if (leftChildIndex < this.Size && rightChildIndex < this.Size)
                {
                    leftChild = this.heap[leftChildIndex];
                    rightChild = this.heap[rightChildIndex];

                    if (leftChild.CompareTo(rightChild) > 0)
                    {
                        if (currentElement.CompareTo(leftChild) < 0)
                        {
                            this.Swap(leftChildIndex, index);
                            index = leftChildIndex;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        if (currentElement.CompareTo(rightChild) < 0)
                        {
                            this.Swap(rightChildIndex, index);
                            index = rightChildIndex;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else if (leftChildIndex < this.Size)
                {
                    leftChild = this.heap[leftChildIndex];

                    if (currentElement.CompareTo(leftChild) < 0)
                    {
                        this.Swap(leftChildIndex, index);
                        index = leftChildIndex;
                    }

                    break;
                }
                else if (rightChildIndex < this.Size)
                {
                    rightChild = this.heap[rightChildIndex];

                    if (currentElement.CompareTo(rightChild) < 0)
                    {
                        this.Swap(rightChildIndex, index);
                        index = rightChildIndex;
                    }

                    break;
                }

                leftChildIndex = 2 * index + 1;
                rightChildIndex = 2 * index + 2;
            }
        }

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
