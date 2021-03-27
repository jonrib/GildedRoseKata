using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GildedRoseKata.Model
{
    public class ConjuredItem : RegularItem
    {
        //Simplest way I came up with to decrease quality twice as fast
        public ConjuredItem()
        {
            this.DegradeSpeed *= 2;
        }
    }
}
