using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HttpLog
{
    public class HttpResult
    {
        public string Url { get; set; }

        public string RequestBody { get; set; }

        public string ResponseBody { get; set; }

        public override string ToString()
        {
            return String.Format( "Url:{0}, \nRequestBody:{1}, \nResponseBody:{2}", Url, RequestBody, ResponseBody );
        }
    }
}
