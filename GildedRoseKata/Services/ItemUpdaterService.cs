using GildedRoseKata.Factories;
using GildedRoseKata.Model;
using GildedRoseKata.Model.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Originally was GildedRose, refactored it to be a service class with an appropriate name
namespace GildedRoseKata.Services
{
    public class ItemUpdaterService : IItemUpdaterService
    {
        IList<Item> Items;
        RegularItemContext _context;
        public ItemUpdaterService(RegularItemContext context)
        {
            this._context = context;
            Items = new List<Item>();
            foreach (var item in context.RegularItems.ToList())
            {
                Items.Add(item);
            }
        }

        public ItemUpdaterService(IList<Item> Items)
        {
            this.Items = Items;
        }

        public RegularItem Add(Item item, ItemTypes type)
        {
            //Did not implement any validation as it was not really a part of the task
            var actualItem = ItemFactory.CreateItemByType(type);
            actualItem.Name = item.Name;
            actualItem.SellIn = item.SellIn;
            actualItem.Quality = item.Quality;
            _context.RegularItems.Add(actualItem);
            _context.SaveChanges();
            return actualItem;
        }

        public Item Find(int id)
        {
            return this.Items.Where((item) => { return id == (item as RegularItem).Id; }).FirstOrDefault();
        }

        public IList<Item> GetAll()
        {
           return this.Items;
        }

        public void UpdateQuality()
        {
            //Update each item
            foreach (Item item in Items)
            {
                //Check if item is wrapped by our new class
                if (item is RegularItem)
                    (item as RegularItem).UpdateProperties();
            }
            if (_context != null)
                _context.SaveChanges();
        }
    }
}
