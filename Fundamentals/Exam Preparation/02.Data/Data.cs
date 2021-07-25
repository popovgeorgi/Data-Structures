namespace _02.Data
{
    using _02.Data.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Data : IRepository
    {
        private PriorityQueue<IEntity> priorityQueue;
        private Dictionary<int, IEntity> dictionary;
        private Dictionary<int, List<IEntity>> parentDictionary;

        public Data()
        {
            this.priorityQueue = new PriorityQueue<IEntity>();
            this.dictionary = new Dictionary<int, IEntity>();
            this.parentDictionary = new Dictionary<int, List<IEntity>>();
        }
        public int Size => this.priorityQueue.Size;

        public void Add(IEntity entity)
        {
            this.priorityQueue.Add(entity);
            dictionary.Add(entity.Id, entity);

            if (!parentDictionary.ContainsKey((int)entity.ParentId))
            {
                parentDictionary.Add((int)entity.ParentId, new List<IEntity>());
            }

            parentDictionary[(int)entity.ParentId].Add(entity);
        }

        public IRepository Copy()
        {
            return new Data
            {
                dictionary = this.dictionary,
                priorityQueue = this.priorityQueue,
            };
        }

        public IEntity DequeueMostRecent()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Operation on empty Data");
            }

            var entity = this.priorityQueue.Dequeue();
            dictionary.Remove(entity.Id);
            return entity;
        }

        public List<IEntity> GetAll()
        {
            return new List<IEntity>(this.priorityQueue.AsList);
        }

        public List<IEntity> GetAllByType(string type)
        {
            if (!(type == "User" || type == "StoreClient" || type == "Invoice"))
            {
                throw new InvalidOperationException("Invalid type: " + type);
            }

            return this.priorityQueue.AsList.Where(x => x.GetType().Name == type).ToList();
        }

        public IEntity GetById(int id)
        {
            if (!dictionary.ContainsKey(id))
            {
                return null;
            }
            return this.dictionary[id];
        }

        public List<IEntity> GetByParentId(int parentId)
        {
            if (!parentDictionary.ContainsKey(parentId))
            {
                return new List<IEntity>();
            }

            return parentDictionary[parentId];
        }

        public IEntity PeekMostRecent()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Operation on empty Data");
            }
            return this.priorityQueue.Peek();
        }
    }
}
