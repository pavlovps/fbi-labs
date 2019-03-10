using System.Collections.Generic;

namespace pavlovLab.Storage
{
    public interface IStorage<T> where T : class
   {
       List<T> All { get; }
       T this[int index] { get; set; }
       void Add(T value);
       void RemoveAt(int index);
       void Remove(T value);
       void Clear();
       int Count { get; }
   }
}