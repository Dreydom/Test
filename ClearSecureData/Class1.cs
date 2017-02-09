using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ClearSecureData
{
    public class ClearData
    {
        public static string Clear(string json, string attr)
        {
            string regex = @"((?<=\b" + attr + @"\s*(:|=)\s*\')([^\']*))|" +
                            @"((?<=<\s*" + attr + @"\s*>)([^\<]*))|" +
                            @"((?<=/" + attr + @"\s*\/)([^\/|\n]*))|" +
                            @"((?<=(\?|\&)\s*" + attr + @"\s*=\s*)([^\&|\n]*))";
            foreach (Match matches in Regex.Matches(json, regex))
            {
                //Console.WriteLine("'{0}' найдено на позиции {1}.", matches.Value, matches.Index);
                string asterisks = "";
                for (int i = 0; i < matches.Value.Length; i++) asterisks += "X";
                json = json.Remove(matches.Index, matches.Length).Insert(matches.Index, asterisks);
            }
            return json;
        }
    }
}
