using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Threading;

namespace API
{
    class FilePrint
    {
        /// <summary>
        /// Метод вывода
        /// </summary>
        public static async void Print(string[] args)
        {
            var obj = new GoogleAPI();
            obj.AsyncConnectApi();
            if (System.IO.File.Exists(args[0]))
            {
                var obj2 = new FileUpload();
                for (int i = 0; i < args.Length; i++)
                {
                    Stopwatch start = new Stopwatch();
                    start.Start();
                    await obj2.AsyncUploadFile(args[i], "root");
                    start.Stop();
                    Console.WriteLine(start.Elapsed);
                    Thread.Sleep(2000);
                }
                Environment.Exit(0);
            }
            else
            {
                var obj3 = new UploadTemp();
                for (int i = 0; i < args.Length; i++)
                {
                    Stopwatch start = new Stopwatch();
                    start.Start();
                    await obj3.UploadDirectoryAsync(args[i], "root");
                    start.Stop();
                    Console.WriteLine(start.Elapsed);
                    Thread.Sleep(2000);
                }
                Environment.Exit(0);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            FilePrint.Print(args);
            Console.ReadKey();
        }
    }
}
