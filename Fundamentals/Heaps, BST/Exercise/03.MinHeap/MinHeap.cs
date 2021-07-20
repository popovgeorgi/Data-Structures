namespace _03.MinHeap
{
    using System;
    using System.Collections.Generic;

    public class MinHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> elements;

        public MinHeap()
        {
            this.elements = new List<T>();
        }

        public int Size => this.elements.Count;

        public T Dequeue()
        {
            var result = this.Peek();
            this.elements[0] = this.elements[this.Size - 1];
            this.elements.RemoveAt(this.Size - 1);
            this.HeapifyDown(0);
            return result;
        }

        private void HeapifyDown(int index)
        {
            bool leftChildExists = (2 * index + 1) < this.Size;
            bool rightChildExists = (2 * index + 2) < this.Size;

            while (leftChildExists || rightChildExists)
            {
                if (leftChildExists && rightChildExists)
                {
                    if (this.elements[2 * index + 1].CompareTo(this.elements[2 * index + 2]) < 0)
                    {
                        if (this.elements[index].CompareTo(this.elements[2 * index + 1]) > 0)
                        {
                            this.Swap(2 * index + 1, index);
                            index = 2 * index + 1;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (this.elements[index].CompareTo(this.elements[2 * index + 2]) > 0)
                        {
                            this.Swap(2 * index + 2, index);
                            index = 2 * index + 2;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                else if (leftChildExists)
                {
                    if (this.elements[index].CompareTo(this.elements[2 * index + 1]) > 0)
                    {
                        this.Swap(2 * index + 1, index);
                        index = 2 * index + 1;
                    }
                    else
                    {
                        return;
                    }
                }
                else if (rightChildExists)
                {
                    if (this.elements[index].CompareTo(this.elements[2 * index + 2]) > 0)
                    {
                        this.Swap(2 * index + 2, index);
                        index = 2 * index + 2;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }

                leftChildExists = (2 * index + 1) < this.Size;
                rightChildExists = (2 * index + 2) < this.Size;
            }

            
        }

        public void Add(T element)
        {
            this.elements.Add(element);
            this.HeapifyUp(this.Size - 1);
        }

        private void HeapifyUp(int index)
        {
            var parentIndex = (index - 1) / 2; 

            while (index > 0)
            {
                if (this.elements[index].CompareTo(this.elements[parentIndex]) < 0)
                {
                    this.Swap(index, parentIndex);
                    index = parentIndex;
                    parentIndex = (index - 1) / 2;
                }
                else
                {
                    return;
                }
            }
        }

        private void Swap(int index, int parentIndex)
        {
            var temp = this.elements[parentIndex];
            this.elements[parentIndex] = this.elements[index];
            this.elements[index] = temp;
        }

        public T Peek()
        {
            return this.elements[0];
        }
    }
}
