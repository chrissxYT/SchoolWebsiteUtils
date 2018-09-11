using System.IO;
using static System.Console;
using static System.IO.File;
using static System.IO.Path;
using static System.Text.Encoding;

namespace FileIdentifier
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] files = Directory.GetFiles(ReadLine(), "*.dat");
            foreach(string f in files)
            {
                string s = readutf8(f, 30);
                string t = s.ToLower();
                string u = readutf8(f, 100).ToLower();
                if (t.StartsWith("<html") || t.StartsWith("<!doctype html") || (t.StartsWith("<?xml") && (u.Contains("<html") || u.Contains("<!doctype html"))))
                    Move(f, ChangeExtension(f, "html"));
                else if (s.Contains("PDF"))
                    Move(f, ChangeExtension(f, "pdf"));
                else if (s.Contains("JFIF"))
                    Move(f, ChangeExtension(f, "jpg"));
                else if (s.Contains("AVI"))
                    Move(f, ChangeExtension(f, "avi"));
                else if (s.Contains("mp4"))
                    Move(f, ChangeExtension(f, "mp4"));
                else if (s.StartsWith("PNG"))
                    Move(f, ChangeExtension(f, "png"));
            }
        }

        static string readutf8(string file, int len)
        {
            FileStream fs = Open(file, FileMode.Open, FileAccess.Read);
            byte[] bfr = new byte[len];
            fs.Read(bfr, 0, len);
            fs.Close();
            return UTF8.GetString(bfr);
        }
    }
}
