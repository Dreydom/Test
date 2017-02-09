using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClearSecureData;

namespace HttpLog
{
    public class HttpLogHandler
    {
        HttpResult _currentLog;
        public HttpResult CurrentLog { get { return _currentLog; } }
        public string Process( string url, string body, string response, string format)
        {
            var httpResult = new HttpResult
            {
                Url = ClearData.Clear( url, "user", format ),
                RequestBody = ClearData.Clear( body, "user", format ),
                ResponseBody = ClearData.Clear( response, "user", format )
            };
            Log( httpResult );
            return response;
        }
        /// <summary>
        /// Логирует данные запроса, они должны быть уже без данных которые нужно защищать
        /// </summary>
        /// <param name="result"></param>

        protected void Log( HttpResult result )
        {
            _currentLog = new HttpResult
            {
                Url = result.Url,
                RequestBody = result.RequestBody,
                ResponseBody = result.ResponseBody
            };

            var curr = _currentLog;
        }
    }
}
