using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSpam.data
{
    static class readFiles
    {
        public static List<string> getTokensFromFolder(string folderPath)
        {
            List<string> tokens= new List<string>();
            foreach (string file in Directory.EnumerateFiles(folderPath, "*.txt"))
            {
                string contents = File.ReadAllText(file);
                contents = contents.Replace("\n", " ").Replace("\r", " ").Replace("\t", " ");
                foreach (string item in contents.Split(' '))
                {
                    if (!String.IsNullOrEmpty(item)&&item.Length>1&&!isNumber(item))
                    {
                        tokens.Add(processString(item));
                    }
                }
            }
            return tokens;
    }
        static string processString(string token)
        {
            char[] trimChars = { ' ' };
            return token.Trim(trimChars);
        }
        static bool isNumber(string token)
        {
            return int.TryParse(token, out int a);
        }
    }
}
