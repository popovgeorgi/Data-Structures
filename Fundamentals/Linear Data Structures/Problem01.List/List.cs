namespace Problem01.List
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class List<T> : IAbstractList<T>
    {
        private const int DEFAULT_CAPACITY = 4;
        private T[] items;
        private int index = 0;

        public List()
            : this(DEFAULT_CAPACITY) {
        }

        public List(int capacity)
        {
            this.items = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                this.CheckIfExceptionMustBeThrown(this.items, index);
                return this.items[index];
            }
            set
            {
                this.CheckIfExceptionMustBeThrown(this.items, index);
                this.items[index] = value;
            }
        }

        public int Count => this.index;

        public void Add(T item)
        {
            this.ValidateSize();

            this.items[index++] = item;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < this.items.Length; i++)
            {
                if (this.items[i].Equals(item))
                {
                    return true;
                }
            }

            return false;
        }


        public int IndexOf(T item)
        {
            for (int i = 0; i < this.items.Length; i++)
            {
                if (this.items[i].Equals(item))
                {
                    return i;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            this.CheckIfExceptionMustBeThrown(this.items, index);
            this.ValidateSize();

            for (int i = this.index; i >= index; i--)
            {
                this.items[i] = this.items[i - 1];
            }

            this.items[index] = item;
            this.index++;
        }

        public bool Remove(T item)
        {
            var index = -1;

            for (int i = 0; i < this.items.Length; i++)
            {
                if (this.items[i].Equals(item))
                {
                    index = i;
                    break;
                }
            }

            if (index == -1)
            {
                return false;
            }

            this.RemoveAt(index);

            return true;
        }

        public void RemoveAt(int index)
        {
            this.CheckIfExceptionMustBeThrown(this.items, index);

            for (int i = index + 1; i < this.items.Length; i++)
            {
                this.items[i - 1] = this.items[i];
            }

            this.index--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var index = 0;

            foreach (T item in this.items)
            {
                if (this.Count < index++)
                {
                    yield return item;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void ValidateSize()
        {
            if (this.index == this.items.Length)
            {
                this.DoubleArraySize();
            }
        }

        private void DoubleArraySize()
        {
            var doubledArray = new T[this.items.Length * 2];
            for (int i = 0; i < this.items.Length; i++)
            {
                doubledArray[i] = this.items[i];
            }

            this.items = doubledArray;
        }

        private bool CheckIfIndexIsOutOfRange(T[] arr, int index)
        {
            if (index >= this.Count || index < 0)
            {
                return true;
            }

            return false;
        }

        private void CheckIfExceptionMustBeThrown(T[] arr, int index)
        {
            if (this.CheckIfIndexIsOutOfRange(arr, index))
            {
                throw new IndexOutOfRangeException();
            }
        }
    }
}