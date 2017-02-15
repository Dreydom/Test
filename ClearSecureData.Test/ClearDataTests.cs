using Microsoft.VisualStudio.TestTools.UnitTesting;
using static ClearSecureData.ClearData;

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
            SecureStringFormat secureFormat = SecureStringFormat.urlget;

            //Act
            string escapedString = Clear( secureString, secureGetParam, secureFormat );

            //Assert
            Assert.AreEqual( "http://test.com?user=max&pass=XXXXXX", escapedString );
        }

        [TestMethod]
        public void ClearData_Clear_SecureDataInUrlRestText_ReturnEscapeSecureData()
        {
            //Arrange
            string secureString = "http://test.com/user/max/info";
            string secureXMLValue = "user";
            SecureStringFormat secureFormat = SecureStringFormat.urlrest;

            //Act
            string escapedString = Clear(secureString, secureXMLValue, secureFormat);

            //Assert
            Assert.AreEqual("http://test.com/user/XXX/info", escapedString);
        }

        [TestMethod]
        public void ClearData_Clear_SecureDataInXmlElementText_ReturnEscapeSecureData()
        {
            //Arrange
            string secureString = "<auth><user>max</user><pass>123456</pass></auth>";
            string secureXMLValue = "pass";
            SecureStringFormat secureFormat = SecureStringFormat.xmlelementvalue;

            //Act
            string escapedString = Clear(secureString, secureXMLValue, secureFormat);

            //Assert
            Assert.AreEqual("<auth><user>max</user><pass>XXXXXX</pass></auth>", escapedString);
        }

        [TestMethod]
        public void ClearData_Clear_SecureDataInXmlAttribute_ReturnEscapeSecureData()
        {
            //Arrange
            string secureString = "<auth user='max' pass='123456'>";
            string secureXMLAttr = "pass";
            SecureStringFormat secureFormat = SecureStringFormat.xmlattribute;

            //Act
            string escapedString = Clear(secureString, secureXMLAttr, secureFormat);

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
            string secureXMLAttr = "user";
            SecureStringFormat secureFormat = SecureStringFormat.json;
             
            //Act
            string escapedString = Clear(secureString, secureXMLAttr, secureFormat);

            //Assert
            Assert.AreEqual(
@"{
    user: 
'XXX',
    pass:
'123456'
}", escapedString);
        }

        [TestMethod]
        public void ClearData_Clear_SecureDataInJsonValue_ReturnEscapeSecureData()
        {
            //Arrange
            string secureString =
@"{
    user: {
        key: '32',
        value: 'max'
    },
    password: {
        value: '123456'
    }
}";
            string secureXMLAttr = "user";
            SecureStringFormat secureFormat = SecureStringFormat.jsonvalue;

            //Act
            string escapedString = Clear(secureString, secureXMLAttr, secureFormat);

            //Assert
            Assert.AreEqual(
@"{
    user: {
        key: '32',
        value: 'XXX'
    },
    password: {
        value: '123456'
    }
}", escapedString);
        }
    }
}
