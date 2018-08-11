using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

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
                File.WriteAllBytes(s.ids[0] + ".dat", s.bytes);
                File.WriteAllBytes(s.ids[0] + ".ids", s.ids.to_byte_array());
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

        static byte[] to_byte_array(this List<short> l)
        {
            if(BitConverter.IsLittleEndian)
            {
                byte[] b = new byte[2 * l.Count];
                for (int i = 0; i < l.Count; i++)
                    Array.Copy(BitConverter.GetBytes(l[i]), 0, b, i * 2, 2);
                return b;
            }
            else
            {
                byte[] b = new byte[2 * l.Count];
                for (int i = 0; i < l.Count; i++)
                {
                    byte[] c = BitConverter.GetBytes(l[i]);
                    Array.Reverse(c); //uses less memory than c.Reverse() from Linq
                    Array.Copy(c, 0, b, i * 2, 2);
                }
                return b;
            }
        }

        static void check(short num)
        {
            try
            {
                HttpClient hc = new HttpClient();
                hc.Timeout = new TimeSpan(0, 0, 2); //if you have a REALLY bad internet connection with a REALLY high ping set this higher
                HttpResponseMessage r = hc.GetAsync(pics + num).Result;
                byte[] content = r.Content.ReadAsByteArrayAsync().Result;
                int i = known_sites.FindIndex((s) => arrequ(s.bytes, content));
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

        static bool arrequ(byte[] left, byte[] right)
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
        public List<short> ids;
        public byte[] bytes;

        public id_site(short id, byte[] bytes)
        {
            ids = new List<short>();
            ids.Add(id);
            this.bytes = bytes;
        }
    }
}
