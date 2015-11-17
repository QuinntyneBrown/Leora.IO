

using System;
using System.Collections.Generic;

namespace FarApi.Resources.Core.Base.Models
{


    public class FeedbackBase
    {

        public DateTime ReportedDateTime { get; set; }

        public DateTime? IncidentDateTime { get; set; }

        public dynamic Type { get; set; }

        public string Remarks { get; set; }

        public string Data { get; set; }

        public string ScreenshotImage { get; set; }
 
        public string ClientIpAddress { get; set; }

        public decimal? Longitude { get; set; }

        public decimal? Latitude { get; set; }

        public int Severity { get; set; }

        public string Module { get; set; }

        public dynamic Error { get; set; }

        public dynamic PreviousError { get; set; }

        public IList<dynamic> Events { get; set; }

        public IList<dynamic> BrowserFeatures { get; set; }

        public string UserAgentDetails { get; set; }

        public string CetarisOnlyUser { get; set; }

        public IList<dynamic> History { get; set; }

        public dynamic Settings { get; set; }

        public string ReferenceNumber { get; set; }

        public string ClientVersion { get; set; }


        public string AdditionalInfo { get; set; }
    }
}
