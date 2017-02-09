using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ClearSecureData
{
    public class ClearData
    {
        /// <summary>
        /// 1) string 
        /// 2) key 
        /// 3) format (urlget|urlrest|xmlelementvalue|xmlattribute|json|jsonvalue)
        /// </summary>
        public static string Clear( string secureString, string secureKey, string secureStringFormat )
        {
            string regex;
            switch (secureStringFormat)
            {
                case "urlget": //http://test.com?user=XXX&pass=XXXXXX
                    regex = @"((?<=(\?|\&)\s*" + secureKey + @"\s*=\s*)([^\&|\n]*))"; 
                    break;
                case "urlrest": //http://test.com/users/XXX/info
                    regex = @"((?<=/" + secureKey + @"\s*\/)([^\/|\n]*))";
                    break;
                case "xmlelementvalue": //<auth><user>XXX</user><pass>XXXXXX</pass></auth>
                    regex = @"((?<=<\s*" + secureKey + @"\s*>)([^\<]*))";
                    break;
                case "xmlattribute": //<auth user='XXX' pass='XXXXXX'>
                    regex = @"((?<=<.*\s+" + secureKey + @"\s*=\s*')([^']*))";
                    break;
                case "json": //{ user: 'XXX', pass:'XXXXXX' }
                    regex = @"(?<={[\s\S]+?" + secureKey + @"\s*:\s*')([^']*)";
                    break;
                case "jsonvalue": //{user: {value:'XXX'}, pass:{value:'XXXXXX'}}
                    regex = @"(?<={[\s\S]+?" + secureKey + @"\s*:[\s\S]+?{[\s\S]+?value\s*:\s*')([^']*)";
                    break;
                default:
                    regex = @"((?<=(\?|\&)\s*" + secureKey + @"\s*=\s*)([^\&|\n]*))|" +
                            @"((?<=<\s*" + secureKey + @"\s*>)([^\<]*))|" +
                            @"((?<=/" + secureKey + @"\s*\/)([^\/|\n]*))|" +
                            @"((?<=<.*\s+" + secureKey + @"\s*=\s*')([^']*))|" +
                            @"(?<={[\s\S]+?" + secureKey + @"\s*:\s*')([^']*)|" +
                            @"(?<={[\s\S]+?" + secureKey + @"\s*:[\s\S]+?{[\s\S]+?value\s*:\s*')([^']*)";
                    break;
            }
            foreach ( Match matches in Regex.Matches( secureString, regex , RegexOptions.Multiline) )
            {
                //Console.WriteLine("'{0}' найдено на позиции {1}.", matches.Value, matches.Index);
                string asterisks = "";
                for ( int i = 0; i < matches.Value.Length; i++ ) asterisks += "X";
                secureString = secureString.Remove( matches.Index, matches.Length ).Insert( matches.Index, asterisks );
            }
            return secureString;
        }
    }
}
