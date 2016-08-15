using HomeSiteDomain.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeSite.Models.Repository
{
    public interface IHomeRepository : IRepository<ContentUpdate, int>, IDisposable
    {
    }
}