namespace _01.Loader
{
    using _01.Loader.Interfaces;
    using _01.Loader.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Loader : IBuffer
    {
        private List<IEntity> entities;

        public Loader()
        {
            this.entities = new List<IEntity>();
        }
        public int EntitiesCount => this.entities.Count;

        public void Add(IEntity entity)
        {
            entities.Add(entity);
        }

        public void Clear()
        {
            entities.Clear();
        }

        public bool Contains(IEntity entity)
        {
            return entities.Contains(entity);
        }

        public IEntity Extract(int id)
        {
            var entity = this.FindById(id);
            if (entity != null)
            {
                this.entities.Remove(entity);
            }

            return entity;
        }

        public IEntity Find(IEntity entity)
        {
            return this.entities.Find(x => x.Equals(entity));
        }

        public List<IEntity> GetAll()
        {
            return new List<IEntity>(entities);
        }

        public IEnumerator<IEntity> GetEnumerator()
        {
            return this.entities.GetEnumerator();
        }

        public void RemoveSold()
        {
            var withoutSold = new List<IEntity>();
            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i].Status != BaseEntityStatus.Sold)
                {
                    withoutSold.Add(entities[i]);
                }
            }

            this.entities = withoutSold;
        }

        public void Replace(IEntity oldEntity, IEntity newEntity)
        {
            var oldEntityIndex = this.entities.IndexOf(oldEntity);
            CheckValidIndex(oldEntityIndex, "Entity not found");

            entities[oldEntityIndex] = newEntity;
        }

        public List<IEntity> RetainAllFromTo(BaseEntityStatus lowerBound, BaseEntityStatus upperBound)
        {
            var result = new List<IEntity>();

            for (int i = 0; i < this.entities.Count; i++)
            {
                var current = this.entities[i].Status;

                if (current >= lowerBound && current <= upperBound)
                {
                    result.Add(this.entities[i]);
                }
            }

            return result;
        }

        public void Swap(IEntity first, IEntity second)
        {
            var firstIndex = this.entities.IndexOf(first);
            var secondIndex = this.entities.IndexOf(second);

            CheckValidIndex(firstIndex, "Entity not found");
            CheckValidIndex(secondIndex, "Entity not found");

            var temp = this.entities[firstIndex];
            this.entities[firstIndex] = this.entities[secondIndex];
            this.entities[secondIndex] = temp;
        }

        public IEntity[] ToArray()
        {
            return this.entities.ToArray();
        }

        public void UpdateAll(BaseEntityStatus oldStatus, BaseEntityStatus newStatus)
        {
            for (int i = 0; i < this.entities.Count; i++)
            {
                var current = this.entities[i];

                if (current.Status == oldStatus)
                {
                    current.Status = newStatus;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private void CheckValidIndex(int index, string message)
        {
            if (index < 0)
            {
                throw new InvalidOperationException(message);
            }
        }

        private IEntity FindById(int id)
        {
            for (int i = 0; i < entities.Count; i++)
            {
                if (entities[i].Id == id)
                {
                    return entities[i];
                }
            }

            return null;
        }
    }
}
