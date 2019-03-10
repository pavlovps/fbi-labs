using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using pavlovLab.Models;
using pavlovLab.Storage;

namespace pavlovLab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabController : ControllerBase
    {
        private static IStorage<LabData> _storage;

        public LabController(IStorage<LabData> storage)
        {
            _storage = storage;
        }

        [HttpGet]
        public ActionResult<IEnumerable<LabData>> Get()
        {
            return Ok(_storage.All);
        }

        [HttpGet("{id}")]
        public ActionResult<LabData> Get(int id)
        {
            if (_storage.Count <= id) return NotFound("No such");

            return Ok(_storage[id]);
        }

        [HttpPost]
        public IActionResult Post([FromBody] LabData value)
        {
            var validationResult = value.Validate();

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            _storage.Add(value);

            return Ok($"{value.ToString()} has been added");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] LabData value)
        {
            if (_storage.Count <= id) return NotFound("No such");

            var validationResult = value.Validate();

            if (!validationResult.IsValid) return BadRequest(validationResult.Errors);

            var previousValue = _storage[id];
            _storage[id] = value;

            return Ok($"{previousValue.ToString()} has been updated to {value.ToString()}");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_storage.Count <= id) return NotFound("No such");

            var valueToRemove = _storage[id];
            _storage.RemoveAt(id);

            return Ok($"{valueToRemove.ToString()} has been removed");
        }
    }
}
