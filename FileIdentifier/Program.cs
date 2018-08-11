using System;
using System.IO;

namespace FileIdentifier
{
    class Program
    {
        static void Main(string[] args)
        {
            string dir = Console.ReadLine();
            foreach(string f in Directory.GetFiles(dir, "*.dat"))
            {
                string s = File.ReadAllText(f).Substring(0, 30);
                if (s.StartsWith("<html") || s.ToLower().StartsWith("<!doctype html") || (s.StartsWith("<?xml") && s.Contains("<html")))
                    File.Move(f, Path.ChangeExtension(f, "html"));
                else if (s.Contains("PDF"))
                    File.Move(f, Path.ChangeExtension(f, "pdf"));
                else if (s.Contains("JFIF"))
                    File.Move(f, Path.ChangeExtension(f, "jpg"));
                else if (s.Contains("AVI"))
                    File.Move(f, Path.ChangeExtension(f, "avi"));
                else if (s.Contains("mp4"))
                    File.Move(f, Path.ChangeExtension(f, "mp4"));
            }
        }
    }
}
