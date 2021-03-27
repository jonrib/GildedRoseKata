using GildedRoseKata.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GildedRoseKata.Factories
{
    //All possible types for items
    public enum ItemTypes
    {
        Regular,
        Aged,
        Legendary,
        BackstagePass,
        Conjured
    }
    public class ItemFactory
    {
        //Since factory doesn't really need to be initialised a static method was used
        //Returns an Item of a specific class based on type give
        public static RegularItem CreateItemByType(ItemTypes type)
        {
            return type switch
            {
                ItemTypes.Regular => new RegularItem(),
                ItemTypes.Aged => new AgedItem(),
                ItemTypes.Legendary => new LegendaryItem(),
                ItemTypes.BackstagePass => new BackstagePassItem(),
                ItemTypes.Conjured => new ConjuredItem(),
                _ => new RegularItem(),
            };
        }
    }
}
