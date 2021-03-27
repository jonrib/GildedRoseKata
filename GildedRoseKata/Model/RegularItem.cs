using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GildedRoseKata.Model
{
    public class RegularItem : Item
    {
        //key is only used for web api purposes and not for the actual task...
        public int Id { get; set; }
        //Maximum quality an item can reach
        public int MaxQuality = 50;
        //Minimum quality an item can have
        public int MinQuality = 0;
        //How much item quality is decreasing after each update
        protected int DegradeSpeed = 1;
        //Updates item's SellIn and Quality as if one day has passed
        public void UpdateProperties()
        {
            UpdateQuality();
            ValidateQuality();
            UpdateSellIn();
        }
        //checks if quality is within min/max range and fixes it accordingly
        protected virtual void ValidateQuality()
        {
            if (this.Quality < MinQuality)
                this.Quality = MinQuality;
            else if (this.Quality > MaxQuality)
                this.Quality = MaxQuality;
        }
        public RegularItem()
        {
            this.Name = "";
            this.Quality = MinQuality;
            this.SellIn = 0;
        }
        //Updates a regular item's quality
        protected virtual void UpdateQuality()
        {
            if (this.SellIn > 0)
            {
                this.Quality -= DegradeSpeed;
            }
            else
            {
                this.Quality -= DegradeSpeed*2;
            }
        }
        //Updates a regular item's sell in
        protected virtual void UpdateSellIn()
        {
            this.SellIn--;
        }

    }
}
