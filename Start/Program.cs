using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Start
{
    class Program
    {
        static void Main(string[] args)
        {
            string startPath = AppDomain.CurrentDomain.BaseDirectory;

            var imagePath = startPath + "..\\..\\..\\API\\bin\\Debug\\Marcus-Roberto-Google-Play-Google-Drive.ico";
            var programmPath = startPath + "..\\..\\..\\API\\bin\\Debug\\API.exe";

            var key = Registry.ClassesRoot.CreateSubKey(@"Directory\shell\Google Drive");
            var key2 = Registry.ClassesRoot.CreateSubKey(@"*\shell\Google Drive");

            key.SetValue("Icon", imagePath);
            key.CreateSubKey("command").SetValue("", programmPath + " \"%1\"");
            key2.SetValue("Icon", imagePath);
            key2.CreateSubKey("command").SetValue("", programmPath + " \"%1\"");
        }
    }
}
