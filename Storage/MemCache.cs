using System;
using System.Collections.Generic;
using System.Linq;
using pavlovLab.Models;

namespace pavlovLab.Storage
{
    public class MemCache : IStorage<LabData>
    {
        private object _sync = new object();
        private List<LabData> _memCache = new List<LabData>();
        public LabData this[Guid id] 
        { 
            get
            {
                lock (_sync)
                {
                    if (!Has(id)) throw new IncorrectLabDataException($"No LabData with id {id}");

                    return _memCache.Single(x => x.Id == id);
                }
            }
            set
            {
                if (id == Guid.Empty) throw new IncorrectLabDataException("Cannot request LabData with an empty id");

                lock (_sync)
                {
                    if (Has(id))
                    {
                        RemoveAt(id);
                    }

                    value.Id = id;
                    _memCache.Add(value);
                }
            }
        }

        public System.Collections.Generic.List<LabData> All => _memCache.Select(x => x).ToList();

        public void Add(LabData value)
        {
            if (value.Id != Guid.Empty) throw new IncorrectLabDataException($"Cannot add value with predefined id {value.Id}");

            value.Id = Guid.NewGuid();
            this[value.Id] = value;
        }

        public bool Has(Guid id)
        {
            return _memCache.Any(x => x.Id == id);
        }

        public void RemoveAt(Guid id)
        {
            lock (_sync)
            {
                _memCache.RemoveAll(x => x.Id == id);
            }
        }
    }
}