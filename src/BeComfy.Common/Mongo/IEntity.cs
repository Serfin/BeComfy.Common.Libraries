using System;

namespace BeComfy.Common.Mongo
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}