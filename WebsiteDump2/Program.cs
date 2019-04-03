using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteDump2
{
    class Program
    {
        static void Main(string[] args)
        {
            Stream s = File.Open("urls.txt", FileMode.Create, FileAccess.Write);
            List<string> htmls = new List<string>()
            {
                "https://gympeg.de"
            };
            List<int> urls = new List<int>();
            while (htmls.Count > 0)
            {
                html(htmls[0], s, urls, htmls);
                htmls.RemoveAt(0);
            }
            s.Close();
            Console.ReadLine();
        }

        static void html(string url, Stream s, List<int> urls, List<string> htmls)
        {
            try
            {
                HtmlWeb hw = new HtmlWeb();
                HtmlDocument doc = hw.Load(url);
                foreach (HtmlNode link in doc.DocumentNode.SelectNodes("//a[@href]"))
                    foreach (HtmlAttribute ha in link.Attributes.AttributesWithName("href"))
                        if (!urls.Contains(ha.Value.GetHashCode()) && (
                            ha.Value.StartsWith("http://www.gympeg.de") ||
                            ha.Value.StartsWith("http://gympeg.de") ||
                            ha.Value.StartsWith("https://www.gympeg.de") ||
                            ha.Value.StartsWith("https://gympeg.de") ||
                            ha.Value.StartsWith("http://www.gymnasium-pegnitz.de") ||
                            ha.Value.StartsWith("http://gymnasium-pegnitz.de") ||
                            ha.Value.StartsWith("https://www.gymnasium-pegnitz.de") ||
                            ha.Value.StartsWith("https://gymnasium-pegnitz.de")))
                        {
                            Console.WriteLine(ha.Value);
                            s.Write(Encoding.UTF8.GetBytes(ha.Value), 0,
                                Encoding.UTF8.GetByteCount(ha.Value));
                            s.WriteByte((byte)'\n');
                            urls.Add(ha.Value.GetHashCode());
                            if (!ha.Value.ToLower().EndsWith(".jpg")
                             && !ha.Value.ToLower().EndsWith(".png")
                             && !ha.Value.ToLower().EndsWith(".pdf")
                             && !ha.Value.ToLower().EndsWith(".docx")
                             && !ha.Value.ToLower().EndsWith(".pptx"))
                                htmls.Add(ha.Value);
                        }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
