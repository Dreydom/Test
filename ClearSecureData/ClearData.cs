using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ClearSecureData
{
    public class ClearData
    {
        public enum SecureStringFormat {
            /// <summary>
            /// строки формата "http://test.com?user=XXX&amp;pass=XXXXXX"
            /// </summary>
            urlget,
            /// <summary>
            /// строки формата "http://test.com/users/XXX/info"
            /// </summary>
            urlrest,
            /// <summary>
            /// строки формата "&lt;auth&gt;&lt;user&gt;XXX&lt;/user&gt;&lt;pass&gt;XXXXXX&lt;/pass&gt;&lt;/auth&gt;"
            /// </summary>
            xmlelementvalue,
            /// <summary>
            /// строки формата "&lt;auth user='XXX' pass='XXXXXX'&gt;"
            /// </summary>
            xmlattribute,
            /// <summary>
            /// json строки формата "{ user: 'XXX', pass:'XXXXXX' }"
            /// </summary>
            json,
            /// <summary>
            /// json строки формата "{user: {value:'XXX'}, pass:{value:'XXXXXX'}}"
            /// </summary>
            jsonvalue
        }
        /// <summary>
        /// 1) string 
        /// 2) key 
        /// 3) format (urlget|urlrest|xmlelementvalue|xmlattribute|json|jsonvalue)
        /// </summary>
        public static string Clear( string secureString, string secureKeys, SecureStringFormat secureStringFormat )
        {
            char delimiter = ',';
            string[] SecureStringsArray = secureKeys.Split(delimiter);
            foreach (string secureKey in SecureStringsArray)
            {
                string regex = "";
                switch (secureStringFormat)
                {
                    case SecureStringFormat.urlget: //http://test.com?user=XXX&pass=XXXXXX
                        regex = @"((?<=(\?|\&)\s*" + secureKey + @"\s*=\s*)([^\&|\n]*))";
                        break;
                    case SecureStringFormat.urlrest: //http://test.com/users/XXX/info
                        regex = @"((?<=/" + secureKey + @"\s*\/)([^\/|\n]*))";
                        break;
                    case SecureStringFormat.xmlelementvalue: //<auth><user>XXX</user><pass>XXXXXX</pass></auth>
                        regex = @"((?<=<\s*" + secureKey + @"\s*>)([^\<]*))";
                        break;
                    case SecureStringFormat.xmlattribute: //<auth user='XXX' pass='XXXXXX'>
                        regex = @"((?<=<.*\s+" + secureKey + @"\s*=\s*')([^']*))";
                        break;
                    case SecureStringFormat.json: //{ user: 'XXX', pass:'XXXXXX' }
                        regex = @"(?<={[^}]+?" + secureKey + @"\s*:\s*')([^']*)";
                        break;
                    case SecureStringFormat.jsonvalue: //{user: {value:'XXX'}, pass:{value:'XXXXXX'}}
                        regex = @"(?<={[\s\S]*?" + secureKey + @"\s*?:\s*?{[^}]*value\s*:\s*')([^']*)";
                        break;
                    default:
                        regex = @"((?<=(\?|\&)\s*" + secureKey + @"\s*=\s*)([^\&|\n]*))|" +
                                @"((?<=<\s*" + secureKey + @"\s*>)([^\<]*))|" +
                                @"((?<=/" + secureKey + @"\s*\/)([^\/|\n]*))|" +
                                @"((?<=<.*\s+" + secureKey + @"\s*=\s*')([^']*))|" +
                                @"(?<={[\s\S]+?" + secureKey + @"\s*:\s*')([^']*)|" +
                                @"(?<={[\s\S]*?" + secureKey + @"\s*?:\s*?{[^}]*value\s*:\s*')([^']*)";
                        break;
                }
                foreach (Match matches in Regex.Matches(secureString, regex))
                {
                    //Console.WriteLine("'{0}' найдено на позиции {1}.", matches.Value, matches.Index);
                    string asterisks = "";
                    for (int i = 0; i < matches.Value.Length; i++) asterisks += "X";
                    secureString = secureString.Remove(matches.Index, matches.Length).Insert(matches.Index, asterisks);
                }
            }
            return secureString;
        }
    }
}
