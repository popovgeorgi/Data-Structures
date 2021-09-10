namespace HashTable
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
    {
        private const int InitialCapacity = 16;

        private const float LoadFactor = 0.75f;

        private LinkedList<KeyValue<TKey, TValue>>[] slots;

        public int Count { get; private set; }

        public int Capacity => this.slots.Length;

        public HashTable()
        {
            this.slots = new LinkedList<KeyValue<TKey, TValue>>[InitialCapacity];
            this.Count = 0;
        }

        public HashTable(int capacity)
        {
            this.slots = new LinkedList<KeyValue<TKey, TValue>>[capacity];
            this.Count = 0;
        }

        public void Add(TKey key, TValue value)
        {
            this.GrowIfNeeded();
            int slotNumber = this.FindSlotNumber(key);
            
            if (this.slots[slotNumber] == null)
            {
                this.slots[slotNumber] = new LinkedList<KeyValue<TKey, TValue>>();
            }
            foreach (var element in this.slots[slotNumber])
            {
                if (element.Key.Equals(key))
                {
                    throw new ArgumentException("Key already exists: " + key);
                }
            }

            var newElement = new KeyValue<TKey, TValue>(key, value);
            this.slots[slotNumber].AddLast(newElement);
            this.Count++;
        }

        private int FindSlotNumber(TKey key)
        {
            int hashOfKey = key.GetHashCode();
            return Math.Abs(hashOfKey) % this.Capacity;
        }

        private void GrowIfNeeded()
        {
            if ((this.Count + 1) / this.Capacity > LoadFactor)
            {
                this.Grow();
            }
        }

        private void Grow()
        {
            var newHashTable = new HashTable<TKey, TValue>(this.Capacity * 2);
            foreach (var element in this)
            {
                newHashTable.Add(element.Key, element.Value);
            }

            this.slots = newHashTable.slots;
            this.Count = newHashTable.Count;
        }

        public bool AddOrReplace(TKey key, TValue value)
        {
            this.GrowIfNeeded();
            int slotNumber = this.FindSlotNumber(key);

            if (this.slots[slotNumber] == null)
            {
                this.slots[slotNumber] = new LinkedList<KeyValue<TKey, TValue>>();
            }
            foreach (var element in this.slots[slotNumber])
            {
                if (element.Key.Equals(key))
                {
                    element.Value = value;
                    return true;
                }
            }

            var newElement = new KeyValue<TKey, TValue>(key, value);
            this.slots[slotNumber].AddLast(newElement);
            this.Count++;
            return true;
        }

        public TValue Get(TKey key)
        {
            var slotNumber = this.FindSlotNumber(key);
            var slot = this.slots[slotNumber];
            if (slot != null)
            {
                foreach (var item in slot)
                {
                    if (item.Key.Equals(key))
                    {
                        return item.Value;
                    }
                }
            }

            throw new KeyNotFoundException("Key: " + key + " does not exist");
        }

        public TValue this[TKey key]
        {
            get
            {
                try
                {
                    var value = this.Get(key);
                    return value;
                }
                catch (Exception e)
                {
                    throw new KeyNotFoundException("Key " + key + " is missing");
                }
            }
            set
            {
                this.AddOrReplace(key, value);
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            TValue result = default;

            try
            {
                result = this.Get(key);
            }
            catch (Exception e)
            {
                value = default;
                return false;
            }

            value = result;
            return true;
        }

        public KeyValue<TKey, TValue> Find(TKey key)
        {
            var slotNumber = this.FindSlotNumber(key);

            if (this.slots[slotNumber] != null)
            {
                foreach (var item in this.slots[slotNumber])
                {
                    if (item.Key.Equals(key))
                    {
                        return item;
                    }
                }
            }

            return null;
        }

        public bool ContainsKey(TKey key)
        {
            var slotNumber = this.FindSlotNumber(key);

            if (this.slots[slotNumber] == null)
            {
                return false;
            }
            foreach (var keyValuePair in this.slots[slotNumber])
            {
                if (keyValuePair.Key.Equals(key))
                {
                    return true;
                }
            }

            return false;
        }

        public bool Remove(TKey key)
        {
            var slotNumber = this.FindSlotNumber(key);
            if (this.slots[slotNumber] != null)
            {
                var currentElement = this.slots[slotNumber].First;
                while (currentElement != null)
                {
                    if (currentElement.Value.Key.Equals(key))
                    {
                        this.slots[slotNumber].Remove(currentElement);
                        this.Count--;
                        return true;
                    }

                    currentElement = currentElement.Next;
                }
            }

            return false;
        }

        public void Clear()
        {
            this.slots = new LinkedList<KeyValue<TKey, TValue>>[InitialCapacity];
            this.Count = 0;
        }

        public IEnumerable<TKey> Keys => this.Select(element => element.Key);

        public IEnumerable<TValue> Values => this.Select(element => element.Value);

        public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
        {
            foreach (var slot in this.slots)
            {
                if (slot != null)
                {
                    foreach (var keyValuePair in slot)
                    {
                        yield return keyValuePair;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
