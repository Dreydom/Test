using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClearSecureData;
using static HttpLog.HttpLogHandler;

namespace HttpLog
{
    public class HttpLogHandler
    {
        HttpResult _currentLog;
        public HttpResult CurrentLog { get { return _currentLog; } }
        public string Process( string url, string body, string response, SecureParams Params )
        {
            var httpResult = new HttpResult
            {
                Url = ClearData.Clear( url, Params.UrlKey , Params.UrlFormat),
                RequestBody = ClearData.Clear( body, Params.BodyKey, Params.BodyFormat ),
                ResponseBody = ClearData.Clear( response, Params.ResponseKey, Params.ResponseFormat )
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
