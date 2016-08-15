using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeSite.Infrastructure.Exceptions
{
    public class AlbumNotEmptyException : Exception
    {
        public AlbumNotEmptyException() : base() { }
        public AlbumNotEmptyException(string message) : base(message) { }
    }
}