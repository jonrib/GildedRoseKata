using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GildedRoseKata.Factories;
using GildedRoseKata.Model;
using GildedRoseKata.Services;
using NUnit.Framework;

namespace GildedRoseKata.Tests
{
    public class ItemUpdateTests
    {
        [Test]
        public void TestItemValueDecrease()
        {
            //At the end of each day our system lowers both values quality and sell by for every item
            Item myItem = ItemFactory.CreateItemByType(ItemTypes.Regular);
            (myItem.Name, myItem.SellIn, myItem.Quality) = ("Regular Item", 100, 50);
            IList<Item> items = new List<Item> { myItem };
            ItemUpdaterService app = new(items);
            app.UpdateQuality();
            foreach (Item item in items)
            {
                Assert.AreEqual(item.Quality, 49);
                Assert.AreEqual(item.SellIn, 99);
            }
        }

        [Test]
        public void TestItemDegradesTwiceAsFast()
        {
            //Once the sell by date has passed, Quality degrades twice as fast
            Item myItem = ItemFactory.CreateItemByType(ItemTypes.Regular);
            (myItem.Name, myItem.SellIn, myItem.Quality) = ("Regular Item", 0, 48);
            IList<Item> items = new List<Item> { myItem };
            ItemUpdaterService app = new(items);
            app.UpdateQuality();
            foreach (Item item in items)
            {
                Assert.AreEqual(item.Quality, 46);
                Assert.AreEqual(item.SellIn, -1);
            }
            app.UpdateQuality();
            foreach (Item item in items)
            {
                Assert.AreEqual(item.Quality, 44);
                Assert.AreEqual(item.SellIn, -2);
            }
        }

        [Test]
        public void TestItemQualityNotNegative()
        {
            //The Quality of an item is never negative
            Item myItem = ItemFactory.CreateItemByType(ItemTypes.Regular);
            (myItem.Name, myItem.SellIn, myItem.Quality) = ("Regular Item", 0, 1);
            IList<Item> items = new List<Item> { myItem };
            ItemUpdaterService app = new(items);
            app.UpdateQuality();
            foreach (Item item in items)
            {
                Assert.AreEqual(item.Quality, 0);
                Assert.AreEqual(item.SellIn, -1);
            }
            app.UpdateQuality();
            foreach (Item item in items)
            {
                Assert.AreEqual(item.Quality, 0);
                Assert.AreEqual(item.SellIn, -2);
            }
        }

        [Test]
        public void TestItemAgedBrie()
        {
            //"Aged Brie" actually increases in Quality the older it gets
            Item myItem = ItemFactory.CreateItemByType(ItemTypes.Aged);
            (myItem.Name, myItem.SellIn, myItem.Quality) = ("Aged Brie", 1, 1);
            IList<Item> items = new List<Item> { myItem };
            ItemUpdaterService app = new(items);
            app.UpdateQuality();
            foreach (Item item in items)
            {
                Assert.AreEqual(item.Quality, 2);
                Assert.AreEqual(item.SellIn, 0);
            }
            app.UpdateQuality();
            foreach (Item item in items)
            {
                Assert.AreEqual(item.Quality, 4);
                Assert.AreEqual(item.SellIn, -1);
            }
        }

        [Test]
        public void TestItemQualityMax()
        {
            //The Quality of an item is never more than 50
            Item myItem = ItemFactory.CreateItemByType(ItemTypes.Aged);
            (myItem.Name, myItem.SellIn, myItem.Quality) = ("Aged Brie", 1, 48);
            IList<Item> items = new List<Item> { myItem };
            ItemUpdaterService app = new(items);
            app.UpdateQuality();
            foreach (Item item in items)
            {
                Assert.AreEqual(item.Quality, 49);
                Assert.AreEqual(item.SellIn, 0);
            }
            app.UpdateQuality();
            foreach (Item item in items)
            {
                Assert.AreEqual(item.Quality, 50);
                Assert.AreEqual(item.SellIn, -1);
            }
            app.UpdateQuality();
            foreach (Item item in items)
            {
                Assert.AreEqual(item.Quality, 50);
                Assert.AreEqual(item.SellIn, -2);
            }
        }

        [Test]
        public void TestItemSulfuras ()
        {
            //"Sulfuras", being a legendary item, never has to be sold or decreases in Quality
            Item myItem = ItemFactory.CreateItemByType(ItemTypes.Legendary);
            (myItem.Name, myItem.SellIn, myItem.Quality) = ("Sulfuras, Hand of Ragnaros", 100, 80);
            IList<Item> items = new List<Item> { myItem };
            ItemUpdaterService app = new(items);
            app.UpdateQuality();
            foreach (Item item in items)
            {
                Assert.AreEqual(item.Quality, 80);
                Assert.AreEqual(item.SellIn, 100);
            }
            app.UpdateQuality();
            foreach (Item item in items)
            {
                Assert.AreEqual(item.Quality, 80);
                Assert.AreEqual(item.SellIn, 100);
            }
            app.UpdateQuality();
            foreach (Item item in items)
            {
                Assert.AreEqual(item.Quality, 80);
                Assert.AreEqual(item.SellIn, 100);
            }
        }

        [Test]
        public void TestBackstagePassIncreaseBy2()
        {
            //"Backstage passes", like aged brie, increases in Quality as its SellIn value approaches;
            Item myItem = ItemFactory.CreateItemByType(ItemTypes.BackstagePass);
            (myItem.Name, myItem.SellIn, myItem.Quality) = ("Backstage passes to a TAFKAL80ETC concert", 12, 10);
            IList<Item> items = new List<Item> { myItem };
            ItemUpdaterService app = new(items);
            app.UpdateQuality();
            foreach (Item item in items)
            {
                Assert.AreEqual(item.Quality, 11);
                Assert.AreEqual(item.SellIn, 11);
            }
            app.UpdateQuality();
            foreach (Item item in items)
            {
                Assert.AreEqual(item.Quality, 12);
                Assert.AreEqual(item.SellIn, 10);
            }
            app.UpdateQuality();
            //Quality increases by 2 when there are 10 days or less
            foreach (Item item in items)
            {
                Assert.AreEqual(item.Quality, 14);
                Assert.AreEqual(item.SellIn, 9);
            }
        }

        [Test]
        public void TestBackstagePassIncreaseBy3()
        {
            //"Backstage passes", like aged brie, increases in Quality as its SellIn value approaches;
            Item myItem = ItemFactory.CreateItemByType(ItemTypes.BackstagePass);
            (myItem.Name, myItem.SellIn, myItem.Quality) = ("Backstage passes to a TAFKAL80ETC concert", 5, 10);
            IList<Item> items = new List<Item> { myItem };
            ItemUpdaterService app = new(items);
            app.UpdateQuality();
            //Quality increases by 3 when there are 5 days or less
            foreach (Item item in items)
            {
                Assert.AreEqual(item.Quality, 13);
                Assert.AreEqual(item.SellIn, 4);
            }
            app.UpdateQuality();
            foreach (Item item in items)
            {
                Assert.AreEqual(item.Quality, 16);
                Assert.AreEqual(item.SellIn, 3);
            }
        }

        [Test]
        public void TestBackstageDropTo0()
        {
            //Quality drops to 0 after the concert
            Item myItem = ItemFactory.CreateItemByType(ItemTypes.BackstagePass);
            (myItem.Name, myItem.SellIn, myItem.Quality) = ("Backstage passes to a TAFKAL80ETC concert", 1, 10);
            IList<Item> items = new List<Item> { myItem };
            ItemUpdaterService app = new(items);
            app.UpdateQuality();
            //Quality increases by 3 when there are 5 days or less
            foreach (Item item in items)
            {
                Assert.AreEqual(item.Quality, 13);
                Assert.AreEqual(item.SellIn, 0);
            }
            app.UpdateQuality();
            foreach (Item item in items)
            {
                Assert.AreEqual(item.Quality, 0);
                Assert.AreEqual(item.SellIn, -1);
            }
        }

        [Test]
        public void TestConjuredItem()
        {
            //"Conjured" items degrade in Quality twice as fast as normal items
            Item myItem = ItemFactory.CreateItemByType(ItemTypes.Conjured);
            (myItem.Name, myItem.SellIn, myItem.Quality) = ("Backstage passes to a TAFKAL80ETC concert", 5, 10);
            IList<Item> items = new List<Item> { myItem };
            ItemUpdaterService app = new(items);
            app.UpdateQuality();
            foreach (Item item in items)
            {
                Assert.AreEqual(item.Quality, 8);
                Assert.AreEqual(item.SellIn, 4);
            }
            app.UpdateQuality();
            foreach (Item item in items)
            {
                Assert.AreEqual(item.Quality, 6);
                Assert.AreEqual(item.SellIn, 3);
            }
        }
    }
}
