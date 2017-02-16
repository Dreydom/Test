using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ClearSecureData.ClearData;

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
            var secureParams = new SecureParams()
            {
                UrlFormat = SecureStringFormat.urlget,
                UrlKey = "user,pass",
                BodyFormat = SecureStringFormat.xmlelementvalue,
                BodyKey = "user,pass",
                ResponseFormat = SecureStringFormat.xmlattribute,
                ResponseKey = "user,pass"
            };

            //Act
            httpLogHandler.Process(bookingHttpResult.Url, bookingHttpResult.RequestBody, bookingHttpResult.ResponseBody, secureParams);

            //Assert
            Assert.AreEqual("http://test.com?user=XXX&pass=XXXXXX", httpLogHandler.CurrentLog.Url);
            Assert.AreEqual("<auth><user>XXX</user><pass>XXXXXX</pass></auth>", httpLogHandler.CurrentLog.RequestBody);
            Assert.AreEqual("<auth user='XXX' pass='XXXXXX'>", httpLogHandler.CurrentLog.ResponseBody);
        }

        [TestMethod]
        public void HttpLogHandler_Process_ostrovokHttpResult_ClearSecureData()
        {
            //Arrange
            var ostrovokHttpResult = new HttpResult
            {
                Url = "http://test.com/users/max/info",
                RequestBody = @"{
    user:'max',
    pass:'123456'
}",
                ResponseBody = @"{
    user:{
        value:'max'
    },
    pass:{
        value:'123456'
    }
}"
            };
            var httpLogHandler = new HttpLogHandler();
            var secureParams = new SecureParams()
            {
                UrlFormat = SecureStringFormat.urlrest,
                UrlKey = "user,pass",
                BodyFormat = SecureStringFormat.json,
                BodyKey = "user,pass",
                ResponseFormat = SecureStringFormat.jsonvalue,
                ResponseKey = "user,pass"
            };

            //Act
            httpLogHandler.Process(ostrovokHttpResult.Url, ostrovokHttpResult.RequestBody, ostrovokHttpResult.ResponseBody, secureParams);

            //Assert
            Assert.AreEqual("http://test.com/users/max/info", httpLogHandler.CurrentLog.Url);
            Assert.AreEqual(@"{
    user:'XXX',
    pass:'XXXXXX'
}", httpLogHandler.CurrentLog.RequestBody);
            Assert.AreEqual(@"{
    user:{
        value:'XXX'
    },
    pass:{
        value:'XXXXXX'
    }
}", httpLogHandler.CurrentLog.ResponseBody);
        }


        [TestMethod]
        public void HttpLogHandler_Process_agodaHttpResult_ClearSecureData()
        {
            //Arrange
            var agodaHttpResult = new HttpResult
            {
                Url = "http://test.com?user=max&pass=123456",
                RequestBody = @"
<auth>
    <user>max</user>
    <pass>123456</pass>
</auth>",
                ResponseBody = "<auth user='max' pass='123456'>"
            };
            var httpLogHandler = new HttpLogHandler();
            var secureParams = new SecureParams()
            {
                UrlFormat = SecureStringFormat.urlget,
                UrlKey = "user,pass",
                BodyFormat = SecureStringFormat.xmlelementvalue,
                BodyKey = "user,pass",
                ResponseFormat = SecureStringFormat.xmlattribute,
                ResponseKey = "user,pass"
            };

            //Act
            httpLogHandler.Process(agodaHttpResult.Url, agodaHttpResult.RequestBody, agodaHttpResult.ResponseBody, secureParams);

            //Assert
            Assert.AreEqual("http://test.com?user=XXX&pass=XXXXXX", httpLogHandler.CurrentLog.Url);
            Assert.AreEqual(@"
<auth>
    <user>XXX</user>
    <pass>XXXXXX</pass>
</auth>", httpLogHandler.CurrentLog.RequestBody);
            Assert.AreEqual("<auth user='XXX' pass='XXXXXX'>", httpLogHandler.CurrentLog.ResponseBody);
        }

        [TestMethod]
        public void HttpLogHandler_Process_expediaHttpResult_ClearSecureData()
        {
            //Arrange
            var expediaHttpResult = new HttpResult
            {
                Url = "http://test.com/users/max/info",
                RequestBody = @"
{
       user : 'max',
       pass : '123456'
}
",
                ResponseBody = @"
{
       user : {
             value : 'max'
       },
       pass : {
             value : '123456'
       }
}
"
            };
            var httpLogHandler = new HttpLogHandler();
            var secureParams = new SecureParams()
            {
                UrlFormat = SecureStringFormat.urlrest,
                UrlKey = "users",
                BodyFormat = SecureStringFormat.json,
                BodyKey = "user",
                ResponseFormat = SecureStringFormat.jsonvalue,
                ResponseKey = "user"
            };

            //Act
            httpLogHandler.Process(expediaHttpResult.Url, expediaHttpResult.RequestBody, expediaHttpResult.ResponseBody, secureParams);

            //Assert
            Assert.AreEqual("http://test.com/users/XXX/info", httpLogHandler.CurrentLog.Url);
            Assert.AreEqual(@"
{
       user : 'XXX',
       pass : '123456'
}
", httpLogHandler.CurrentLog.RequestBody);
            Assert.AreEqual(@"
{
       user : {
             value : 'XXX'
       },
       pass : {
             value : '123456'
       }
}
", httpLogHandler.CurrentLog.ResponseBody);
        }
    }
}
