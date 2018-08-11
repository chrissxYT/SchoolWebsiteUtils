using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;

namespace WebsiteDump
{
    static class Program
    {
        static List<id_site> known_sites = new List<id_site>();
        static string pics = "https://gymnasium-pegnitz.de/eigene/bildergalerie/show_pictures.php?link=";
        static string sites = "https://gympeg.de/index.php?id=";

        static void Main(string[] args)
        {
            for (short i = 0; i < 10000; i++)
            {
                check(i);
            }
            GC.Collect();
            foreach (id_site s in known_sites)
            {
                File.WriteAllBytes(s.ids.First() + ".data", s.bytes);
                File.WriteAllLines(s.ids.First() + ".ids", s.ids.to_string_array());
            }
        }

        static string[] to_string_array<T>(this List<T> l)
        {
            string[] s = new string[l.Count];
            for(int i = 0; i < l.Count; i++)
            {
                s[i] = l[i].ToString();
            }
            return s;
        }

        static void check(int num)
        {
            try
            {
                HttpClient hc = new HttpClient();
                HttpResponseMessage r = hc.GetAsync(pics + num).Result;
                byte[] content = r.Content.ReadAsByteArrayAsync().Result;
                int i = known_sites.FindIndex((s) => arr_equ(s.bytes, content));
                if (i == -1)
                {
                    known_sites.Add(new id_site(num, content));
                }
                else
                {
                    known_sites[i].ids.Add(num);
                }
                Console.WriteLine(num);
            }
            catch(Exception e)
            {
                Console.WriteLine(num + " has " + e.GetType());
                check(num);
            }
        }

        static bool arr_equ(byte[] left, byte[] right)
        {
            if (left.LongLength != right.LongLength)
                return false;
            for(long i = 0; i < left.LongLength; i++)
            {
                if (left[i] != right[i])
                    return false;
            }
            return true;
        }
    }

    struct id_site
    {
        public List<int> ids;
        public byte[] bytes;

        public id_site(int id, byte[] bytes)
        {
            ids = new List<int>();
            ids.Add(id);
            this.bytes = bytes;
        }
    }
}
