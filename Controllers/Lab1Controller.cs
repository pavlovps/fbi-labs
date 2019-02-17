using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using pavlovLab.Models;

namespace pavlovLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Lab1Controller : ControllerBase
    {
        private static List<Lab1Data> _memCache = new List<Lab1Data>();

        [HttpGet]
        public ActionResult<IEnumerable<Lab1Data>> Get()
        {
            return _memCache;
        }

        [HttpGet("{id}")]
        public ActionResult<Lab1Data> Get(int id)
        {
            if (_memCache.Count <= id) throw new IndexOutOfRangeException("Нет такого у нас");

            return _memCache[id];
        }

        [HttpPost]
        public void Post([FromBody] Lab1Data value)
        {
            _memCache.Add(value);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Lab1Data value)
        {
            if (_memCache.Count <= id) throw new IndexOutOfRangeException("Нет такого у нас");

            _memCache[id] = value;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            if (_memCache.Count <= id) throw new IndexOutOfRangeException("Нет такого у нас");

            _memCache.RemoveAt(id);
        }
    }
}
