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
        static private void Options()
        {
            var cr = Console.ReadKey().KeyChar;

            var delPath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\API\\bin\\Debug\\token.json";

            switch (cr)
            {
                case '1':
                    string startPath = AppDomain.CurrentDomain.BaseDirectory;

                    var imagePath = startPath + "..\\..\\..\\API\\bin\\Debug\\Marcus-Roberto-Google-Play-Google-Drive.ico";
                    var programmPath = startPath + "..\\..\\..\\API\\bin\\Debug\\API.exe";

                    var key = Registry.ClassesRoot.CreateSubKey(@"Directory\shell\Google Drive");
                    var key2 = Registry.ClassesRoot.CreateSubKey(@"*\shell\Google Drive");

                    key.SetValue("Icon", imagePath);
                    key.CreateSubKey("command").SetValue("", programmPath + " \"%1\"");

                    key2.SetValue("Icon", imagePath);
                    key2.CreateSubKey("command").SetValue("", programmPath + " \"%1\"");

                    Console.WriteLine("\nГотово! Попробуйте загрузить файл, а так же предоставьте доступ приложению на гугл диске");
                    break;
                case '2':
                    try
                    {
                        Directory.Delete(delPath, true);

                        Console.WriteLine("\nАккаунт удалён");
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("\n" + ex.Message);
                    }
                    break;
                case '3':
                    try
                    {

                        Registry.ClassesRoot.CreateSubKey(@"Directory\shell\Google Drive").DeleteSubKey("command");
                        Registry.ClassesRoot.CreateSubKey(@"Directory\shell").DeleteSubKey("Google Drive");

                        Registry.ClassesRoot.CreateSubKey(@"*\shell\Google Drive").DeleteSubKey("command");
                        Registry.ClassesRoot.CreateSubKey(@"*\shell\").DeleteSubKey("Google Drive");

                        Console.WriteLine("\nПриложение удалено");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("\n" + ex.Message);
                    }
                    break;
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Нажмите  1 для получения возможности загружать файлы на гугл диск");

            Console.WriteLine("Нажмитие 2 что бы удалить аккаунт с приложения, и ограничить права доступа");

            Console.WriteLine("Нажмитие 3 что бы удалить приложение");

            while (true)
            {
                Options();
            }
        }
    }
}
