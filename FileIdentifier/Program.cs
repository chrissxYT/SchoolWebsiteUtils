using System;
using System.IO;

namespace FileIdentifier
{
    class Program
    {
        static void Main(string[] args)
        {
            string dir = Console.ReadLine();
            foreach(string f in Directory.GetFiles(dir, "*.data"))
            {
                string s = File.ReadAllText(f);
                if (s.StartsWith("<html") || s.ToLower().StartsWith("<!doctype html") || (s.StartsWith("<?xml") && s.Contains("<html")))
                    File.Move(f, Path.ChangeExtension(f, "html"));
                else if (s.Substring(0, 10).Contains("PDF"))
                    File.Move(f, Path.ChangeExtension(f, "pdf"));
                else if (s.Substring(0, 10).Contains("JFIF"))
                    File.Move(f, Path.ChangeExtension(f, "jpg"));
                else if (s.Substring(5, 15).Contains("AVI"))
                    File.Move(f, Path.ChangeExtension(f, "avi"));
                else if (s.Substring(10, 20).Contains("mp4"))
                    File.Move(f, Path.ChangeExtension(f, "mp4"));
            }
        }
    }
}
