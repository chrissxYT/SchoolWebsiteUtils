using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using static System.Console;
using static System.GC;
using static System.IO.File;

namespace WebsiteDump
{
    static class Program
    {
        static List<id_site> known_sites = new List<id_site>();
        static string pics = "https://gymnasium-pegnitz.de/eigene/bildergalerie/show_pictures.php?link={0}";
        static string sites = "https://gympeg.de/index.php?id={0}";
        static string pics2 = "https://gympeg.de/assets/images/SlideShow/Gympeg{0}.jpg";

        static readonly int max = 1000; //max checked url int + 1
        static string formatted_url(short num) //formats the url for checking
        {
            return string.Format(pics2, num.ToString("D3"));
        }

        static void Main(string[] args)
        {
            for (short i = 0; i < max; i++)
            {
                check(i);
            }
            Collect();
            foreach (id_site s in known_sites)
            {
                WriteAllBytes(s.main_id + ".dat", s.content);
                s.close();
            }
        }

        static void check(short num)
        {
            try
            {
                HttpClient hc = new HttpClient();
                hc.Timeout = new TimeSpan(0, 0, 5); //if you have a REALLY bad internet connection with a REALLY high ping set this higher
                HttpResponseMessage r = hc.GetAsync(formatted_url(num)).Result;
                byte[] content = r.Content.ReadAsByteArrayAsync().Result;
                int i = known_sites.FindIndex((s) => arrequ(s.content, content));
                if (i == -1)
                {
                    known_sites.Add(new id_site(num, content));
                }
                else
                {
                    known_sites[i].addid(num);
                }
                WriteLine(num);
            }
            catch(Exception e)
            {
                WriteLine(num + " has " + e.GetType());
                check(num);
            }
        }

        static bool arrequ(byte[] left, byte[] right)
        {
            if (left.LongLength != right.LongLength)
                return false;
            for (long i = 0; i < left.LongLength; i++)
                if (left[i] != right[i])
                    return false;
            return true;
        }
    }

    class id_site
    {
        public short main_id;
        Stream s;
        public byte[] content;

        public id_site(short id, byte[] bytes)
        {
            addid(id);
            main_id = id;
            content = bytes;
            s = Open(id + ".ids", FileMode.Create, FileAccess.Write);
        }

        public void addid(short id)
        {
            s.WriteByte((byte)id);
            s.WriteByte((byte)(id >> 8));
        }

        public void close()
        {
            s.Close();
            s = null;
            content = null;
        }
    }
}
