namespace _02.Data.Interfaces
{
    using _02.Data.Models;
    using System;
    using System.Collections.Generic;

    public interface IEntity : IComparable, IComparable<IEntity>
    {
        int Id { get; set; }

        int? ParentId { get; set; }

        BaseEntityStatus Status { get; set; }

        List<IEntity> Children { get; }

        void AddChild(IEntity child);
    }
}
