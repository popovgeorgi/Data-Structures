namespace Problem03.ReversedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ReversedList<T> : IAbstractList<T>
    {
        private const int DefaultCapacity = 4;

        private T[] items;

        public ReversedList()
            : this(DefaultCapacity) { }

        public ReversedList(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            this.items = new T[capacity];
        }

        public T this[int index]
        {
            get
            {
                this.CheckIfExceptionMustBeThrown(this.items, index);
                return this.items[this.Count - index - 1];
            }
            set
            {
                this.CheckIfExceptionMustBeThrown(this.items, index);
                this.items[index] = value;
            }
        }

        public int Count { get; private set; }

        public void Add(T item)
        {
            this.ValidateSize();

            this.items[this.Count] = item;
            this.Count++;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < this.Count; i++)
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
            for (int i = 0; i < this.Count; i++)
            {
                if (this.items[i].Equals(item))
                {
                    return this.Count - i - 1;
                }
            }

            return -1;
        }

        public void Insert(int index, T item)
        {
            this.CheckIfExceptionMustBeThrown(this.items, index);
            this.ValidateSize();

            for (int i = this.Count; i > index; i--)
            {
                this.items[i] = this.items[i - 1];
            }

            this.items[index] = item;
            this.Count++;
        }

        public bool Remove(T item)
        {
            var index = -1;
            
            for (int i = 0; i < this.Count; i++)
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

            for (int i = index; i < this.Count - 1; i++)
            {
                this.items[i] = this.items[i + 1];  
            }

            this.items[this.Count - 1] = default(T);
            this.Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = this.Count - 1; i >= 0; i--)
            {
                yield return this.items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void ValidateSize()
        {
            if (this.Count == this.items.Length)
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