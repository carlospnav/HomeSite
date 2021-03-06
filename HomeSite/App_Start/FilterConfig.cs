﻿using HomeSite.Infrastructure;
using System.Web;
using System.Web.Mvc;

namespace HomeSite
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorNLogger());
            filters.Add(new RequireHttpsAttribute());
        }
    }
}
