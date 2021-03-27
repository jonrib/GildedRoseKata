using GildedRoseKata.Factories;
using GildedRoseKata.Model;
using GildedRoseKata.Model.Context;
using GildedRoseKata.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GildedRoseKata.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemUpdaterService _service;

        public ItemController(IItemUpdaterService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetItems()
        {
            return new ObjectResult(_service.GetAll());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetItem(int id)
        {
            var item = _service.Find(id);
            if (item == null)
                return NotFound();
            return new ObjectResult(item as Item);
        }

        [HttpPost]
        public IActionResult PostItem([FromBody] Item item, ItemTypes type)
        {
            var actualItem = ItemFactory.CreateItemByType(type);
            actualItem.Name = item.Name;
            actualItem.SellIn = item.SellIn;
            actualItem.Quality = item.Quality;
            _service.Add(actualItem);
            return CreatedAtAction(nameof(GetItem), new { id = actualItem.Id }, item);
        }

        [HttpPost]
        [Route("[controller]/UpdateItems")]
        public IActionResult UpdateItems()
        {
            _service.UpdateQuality();
            return GetItems();
        }
    }
}
