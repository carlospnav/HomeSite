using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using HomeSiteDomain.Models.About;

namespace HomeSite.Models.Repository
{
    class HomeSiteContextInitializer : CreateDatabaseIfNotExists<HomeSiteContext>
    {
        
    }
}