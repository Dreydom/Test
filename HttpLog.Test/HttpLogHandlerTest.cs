using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HttpLog.Test
{
    [TestClass]
    public class HttpLogHandlerTest
    {
        [TestMethod]
        public void HttpLogHandler_Process_BookingcomHttpResult_ClearSecureData()
        {
            //Arrange
            var bookingHttpResult = new HttpResult
            {
                Url = "http://test.com?user=max&pass=123456",
                RequestBody = "<auth><user>max</user><pass>123456</pass></auth>",
                ResponseBody = "<auth user='max' pass='123456'>"
            };
            var httpLogHandler = new HttpLogHandler();

            //Act
            httpLogHandler.Process( bookingHttpResult.Url, bookingHttpResult.RequestBody, bookingHttpResult.ResponseBody, "default");

            //Assert
            Assert.AreEqual( "http://test.com?user=XXX&pass=123456", httpLogHandler.CurrentLog.Url );
            Assert.AreEqual( "<auth><user>XXX</user><pass>123456</pass></auth>", httpLogHandler.CurrentLog.RequestBody );
            Assert.AreEqual( "<auth user='XXX' pass='123456'>", httpLogHandler.CurrentLog.ResponseBody );
        }
    }
}
