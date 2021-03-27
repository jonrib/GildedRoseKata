using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GildedRoseKata.Model
{
    public class BackstagePassItem : AgedItem
    {
        //day count when quality starts increasing by 2
        private const int QualityTwoIncreaseDays = 10;
        //day count when quality starts increasing by 3
        private const int QualityThreeIncreaseDays = 5;
        protected override void UpdateQuality()
        {
            //Perhaps this could be split to different methods or simplified in some other way...
            if (this.SellIn == 0)
                this.Quality = MinQuality;
            else if (this.SellIn <= QualityThreeIncreaseDays)
                this.Quality += 3;
            else if (this.SellIn <= QualityTwoIncreaseDays)
                this.Quality += 2;
            else
                base.UpdateQuality();
        }
    }
}
