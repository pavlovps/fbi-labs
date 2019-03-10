using System.Collections.Generic;
using pavlovLab.Models;

namespace pavlovLab.Storage
{
   public class MemCache : IStorage<LabData>
   {
       private static List<LabData> _memCache = new List<LabData>();
       public void Add(LabData value)
       {
           _memCache.Add(value);
       }

       public List<LabData> All
       {
           get { return _memCache; }
       }
       
       public void Clear()
       {
           _memCache.Clear();
       }

       public LabData Get(int index)
       {
           return _memCache[index];
       }

       public void RemoveAt(int index)
       {
           _memCache.RemoveAt(index);
       }

       public LabData this[int index]
       {
           get
           {
               return _memCache[index];
           }
           set
           {
               _memCache[index] = value;
           }
       }

       public void Remove(LabData value)
       {
           _memCache.Remove(value);
       }
       public int Count
       {
           get { return _memCache.Count; }
       }
   }
}