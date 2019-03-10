using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using pavlovLab.Models;
using System;
using System.Threading;

namespace pavlovLab.Storage
{
    public class FileStorage : MemCache, IStorage<LabData>
    {
        private object _lock = new object();
        private Timer _timer;

        public string FileName { get; }
        public int FlushPeriod { get; }

        public FileStorage(string fileName, int flushPeriod)
        {
            FileName = fileName;
            FlushPeriod = flushPeriod;

            Load();

            _timer = new Timer((x) => Flush(), null, flushPeriod, flushPeriod);
        }

        public new LabData this[int index] 
        { 
            get => base[index];
            set
            {
                lock (_lock)
                {
                    base[index] = value;
                }
            }
        }

        public new void Add(LabData value)
        {
            lock (_lock)
            {
                base.Add(value);
            }
        }

        public new void Clear()
        {
            lock (_lock)
            {
                base.Clear();
            }
        }

        public new void Remove(LabData value)
        {
            lock (_lock)
            {
                base.Remove(value);
            }
        }

        public new void RemoveAt(int index)
        {
            lock (_lock)
            {
                base.RemoveAt(index);
            }
        }

        private void Load()
        {
            lock (_lock)
            {
                if (File.Exists(FileName))
                {
                    var allLines = File.ReadAllText(FileName);

                    try
                    {
                        var deserialized = JsonConvert.DeserializeObject<List<LabData>>(allLines);

                        if (deserialized != null)
                        {
                            foreach (var labData in deserialized)
                            {
                                base.Add(labData);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new FileLoadException($"Cannot load data from file {FileName}:\r\n{ex.Message}");
                    }
                }
            }
        }

        private void Flush()
        {
            lock (_lock)
            {
                var serializedContents = JsonConvert.SerializeObject(All);

                File.WriteAllText(FileName, serializedContents);
            }
        }
    }
}