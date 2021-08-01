namespace _01._BrowserHistory
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using _01._BrowserHistory.Interfaces;

    public class BrowserHistory : IHistory
    {
        private DoublyLinkedList<ILink> history;
        private List<ILink> historyAsList;
        private Dictionary<string, ILink> dictionary;

        public BrowserHistory()
        {
            this.history = new DoublyLinkedList<ILink>();
            this.historyAsList = new List<ILink>();
            this.dictionary = new Dictionary<string, ILink>();
        }
        public int Size
        {
            get
            {
                if (this.history.Count < this.historyAsList.Count)
                {
                    return this.history.Count;
                }

                return this.historyAsList.Count;
            }
        }

        public void Clear()
        {
            this.history = new DoublyLinkedList<ILink>();
            this.historyAsList = new List<ILink>();
            this.dictionary = new Dictionary<string, ILink>();
        }

        public bool Contains(ILink link)
        {
            if (dictionary.ContainsValue(link))
            {
                return true;
            }

            return false;
        }

        public ILink DeleteFirst()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException();
            }
            return this.history.RemoveLast();
        }

        public ILink DeleteLast()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException();
            }
            return this.history.RemoveFirst();
        }

        // 0(1)
        public ILink GetByUrl(string url)
        {
            if (!this.dictionary.ContainsKey(url))
            {
                return null;
            }
            else
            {
                return this.dictionary[url];
            }

        }

        public ILink LastVisited()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException();
            }
            return this.history.GetFirst();
        }

        public void Open(ILink link)
        {
            this.history.AddFirst(link);
            this.historyAsList.Add(link);
            if (!this.dictionary.ContainsKey(link.Url))
            {
                this.dictionary.Add(link.Url, link);
            }
        }

        // O(n)
        public int RemoveLinks(string url)
        {
            int count = 0;
            for (int i = 0; i < this.historyAsList.Count; i++)
            {
                var current = this.historyAsList[i];
                if (current.Url.Contains(url))
                {
                    this.historyAsList.Remove(current);
                    i--;
                    count++;
                }
            }

            if (count == 0)
            {
                throw new InvalidOperationException();
            }

            return count;
        }

        public ILink[] ToArray()
        {
            var res = new ILink[this.historyAsList.Count];
            int index = 0;

            for (int i = this.historyAsList.Count - 1; i >= 0; i--)
            {
                res[index] = this.historyAsList[i];
                index++;
            }

            return res;
        }

        public List<ILink> ToList()
        {
            var res = new List<ILink>();

            for (int i = this.historyAsList.Count - 1; i >= 0; i--)
            {
                res.Add(historyAsList[i]);
            }

            return res;
        }

        // O(n)
        public string ViewHistory()
        {
            if (this.Size == 0)
            {
                return "Browser history is empty!";
            }
            var sb = new StringBuilder();

            for (int i = this.historyAsList.Count - 1; i >= 0; i--)
            {
                sb.AppendLine(historyAsList[i].ToString());
            }

            return sb.ToString();
        }
    }
}
