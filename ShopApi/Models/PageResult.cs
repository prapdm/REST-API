using System;
using System.Collections.Generic;

namespace ShopApi.Models
{
    public class PageResult<T> // klasa generyczna z parametrem T, inne typy tez mogly by skorzystac z tej klasy
    {
        public List<T> Items { get; set; }
        public int Totalpages { get; set; }
        public int ItemFrom { get; set; }
        public int ItemsTo { get; set; }
        public int TotalItemsCount { get; set; }

        public PageResult(List<T> items, int totalCount, int pageSize, int pageNumber)
        {
            Items = items;
            TotalItemsCount = totalCount;
            ItemFrom = pageSize * (pageNumber - 1) + 1;
            ItemsTo = ItemFrom + pageSize - 1;
            Totalpages = (int)Math.Ceiling(totalCount / (double)pageSize);
        }
    }
}
