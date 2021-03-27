using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Moved the Item class to a more suitable namespace
namespace GildedRoseKata.Model
{
    public class Item
    {
        public string Name { get; set; }
        public int SellIn { get; set; }
        public int Quality { get; set; }

        public override string ToString()
        {
            return this.Name + ", " + this.SellIn + ", " + this.Quality;
        }
    }
}
