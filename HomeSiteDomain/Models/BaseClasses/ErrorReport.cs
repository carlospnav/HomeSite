using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeSiteDomain.Models.BaseClasses
{
    public class ErrorReport
    {
        public int ErrorReportId { get; set; }

        public DateTime CreatedDate { get; set; }

        public string ErrorMessage { get; set; }

        public string ErrorSource { get; set; }

        public string ErrorStackTrace { get; set; }
    }
}
