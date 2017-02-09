using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ClearSecureData.Test
{
    [TestClass]
    public class ClearDataTests
    {
        [TestMethod]
        public void ClearData_Clear_SecureDataInGetParams_ReturnEscapeSecureData()
        {
            //Arrange
            string secureString = "http://test.com?user=max&pass=123456";
            string secureGetParam = "pass";
            string secureFormat = "urlget";

            //Act
            string escapedString = ClearData.Clear( secureString, secureGetParam, secureFormat );

            //Assert
            Assert.AreEqual( "http://test.com?user=max&pass=XXXXXX", escapedString );
        }

        [TestMethod]
        public void ClearData_Clear_SecureDataInUrlRestText_ReturnEscapeSecureData()
        {
            //Arrange
            string secureString = "http://test.com/user/max/info";
            string secureXMLValue = "user";
            string secureFormat = "urlrest";

            //Act
            string escapedString = ClearData.Clear(secureString, secureXMLValue, secureFormat);

            //Assert
            Assert.AreEqual("http://test.com/user/XXX/info", escapedString);
        }
        [TestMethod]
        public void ClearData_Clear_SecureDataInXmlElementText_ReturnEscapeSecureData()
        {
            //Arrange
            string secureString = "<auth><user>max</user><pass>123456</pass></auth>";
            string secureXMLValue = "pass";
            string secureFormat = "xmlelementvalue";

            //Act
            string escapedString = ClearData.Clear(secureString, secureXMLValue, secureFormat);

            //Assert
            Assert.AreEqual("<auth><user>max</user><pass>XXXXXX</pass></auth>", escapedString);
        }
        [TestMethod]
        public void ClearData_Clear_SecureDataInXmlAttribute_ReturnEscapeSecureData()
        {
            //Arrange
            string secureString = "<auth user='max' pass='123456'>";
            string secureXMLAttr = "pass";
            string secureFormat = "xmlattribute";

            //Act
            string escapedString = ClearData.Clear(secureString, secureXMLAttr, secureFormat);

            //Assert
            Assert.AreEqual("<auth user='max' pass='XXXXXX'>", escapedString);
        }
        [TestMethod]
        public void ClearData_Clear_SecureDataInJson_ReturnEscapeSecureData()
        {
            //Arrange
            string secureString =
@"{
    user: 
'max',
    pass:
'123456'
}";
            string secureXMLAttr = "pass";
            string secureFormat = "json";
             
            //Act
            string escapedString = ClearData.Clear(secureString, secureXMLAttr, secureFormat);

            //Assert
            Assert.AreEqual(
@"{
    user: 
'max',
    pass:
'XXXXXX'
}", escapedString);
        }
        [TestMethod]
        public void ClearData_Clear_SecureDataInJsonValue_ReturnEscapeSecureData()
        {
            //Arrange
            string secureString =
@"{
    user: {
        value: 'max'
    },
    pass: {
        value: '123456'
    }
}";
            string secureXMLAttr = "pass";
            string secureFormat = "jsonvalue";

            //Act
            string escapedString = ClearData.Clear(secureString, secureXMLAttr, secureFormat);

            //Assert
            Assert.AreEqual(
@"{
    user: {
        value: 'max'
    },
    pass: {
        value: 'XXXXXX'
    }
}", escapedString);
        }
    }
}
