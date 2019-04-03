using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PicDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("List: ");
            string l = Console.ReadLine();
            Console.Write("Dir: ");
            string d = Console.ReadLine();
            Directory.CreateDirectory(d);
            foreach (string s in File.ReadAllLines(l))
                if(s.ToLower().EndsWith(".jpg"))
                {
                    Console.WriteLine(s);
                    try
                    {
                        new WebClient().DownloadFile(s,
                            Path.Combine(d, s.Split('/').Last()));
                    }
                    catch { }
                }
        }
    }
}
