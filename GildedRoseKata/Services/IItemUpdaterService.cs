using GildedRoseKata.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GildedRoseKata.Services
{
    public interface IItemUpdaterService
    {
        void UpdateQuality();
        Item Find(int id);
        void Add(RegularItem item);
        IList<Item> GetAll();
    }
}
