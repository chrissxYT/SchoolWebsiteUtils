using System;
using System.IO;
using System.Linq;
using System.Net;

namespace WebsiteDownloader
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
