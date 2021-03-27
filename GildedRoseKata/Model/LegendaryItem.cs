using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GildedRoseKata.Model
{
    public class LegendaryItem : RegularItem
    {
        //Legendary items never have to be sold or decrease in quality
        public LegendaryItem()
        {
            //Legendary items have a fixed quality
            this.MaxQuality = 80;
        }
        protected override void UpdateQuality()
        {
        }
        protected override void UpdateSellIn()
        {
        }
    }
}
