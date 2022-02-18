using System;

namespace ShopApi.Exeptions
{
    public class NotFoundExeption : Exception
    {
        public NotFoundExeption(string messege) : base(messege) //wywołanie konstruktora bazowego z klasy Exception
        {
        }
    }
}
