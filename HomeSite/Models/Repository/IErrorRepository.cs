using HomeSiteDomain.Models.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeSite.Models.Repository
{
    public interface IErrorRepository : IRepository<ErrorReport, int>, IDisposable
    {
        IEnumerable<ErrorReport> GetLastX(int numberOfErrors);
    }
}
