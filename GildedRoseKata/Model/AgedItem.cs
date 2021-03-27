using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GildedRoseKata.Model
{
    public class AgedItem : RegularItem
    {
        //Aged items increase in quality with time
        public AgedItem()
        {
            this.DegradeSpeed = -1;
        }
    }
}
