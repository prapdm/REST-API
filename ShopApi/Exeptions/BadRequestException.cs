using System;

namespace ShopApi.Exeptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {

        }

    }
}
