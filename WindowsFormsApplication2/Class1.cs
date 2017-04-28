using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WindowsFormsApplication2
{
    public  class Class1
    {
        public readonly static string fileUrl = System.Environment.CurrentDirectory + "\\skxfpz.txt";
        public static string Content()
        {
            if (!File.Exists(fileUrl))
            {
                return "";
            }
            StreamReader reader = new StreamReader(fileUrl, System.Text.Encoding.Default);
            string content = reader.ReadToEnd();
            reader.Close();
            return content;
        }
    }
}
