using System;
using System.ComponentModel;
using System.IO;
using System.Net;

namespace PictureDump
{
    static class Program
    {
        static string last(this string[] s) => s[s.Length - 1];

        static void Main(string[] args)
        {
            ServicePointManager.ServerCertificateValidationCallback =
                (sender, cert, chain, sslPolicyErrors) => true;
            Console.Write("List: ");
            string file = Console.ReadLine();
            WebClient wc = new WebClient();
            wc.DownloadProgressChanged += new DownloadProgressChangedEventHandler((sdr, e) =>
            {
                if(Console.CursorLeft > 3) Console.CursorLeft -= 4;
                Console.Write(e.ProgressPercentage.ToString("d3") + "%");
            });
            foreach (string s in File.ReadAllLines(file))
            {
                if (s != "" && s[0] == 'h')
                {
                    string url = s.Split('"')[0].Split(' ')[0];
                    string name = url.Split('/').last();
                    Console.Write(name + " 000%");
                    wc.DownloadFileAsync(new Uri(url), name);
                    while (wc.IsBusy) ;
                    Console.WriteLine();
                }
            }
        }
    }
}
