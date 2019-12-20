using Google.Apis.Drive.v3;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using File = Google.Apis.Drive.v3.Data.File;

namespace API
{
    class FileUpload
    {
        /// <summary>
        /// Загрузка файла
        /// </summary>
        public static string GetMimeType(string fileName)   //Метод получающий тип файла
        {
            string mimeType = "application/unknown";
            string ext = Path.GetExtension(fileName).ToLower();
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }
        public  static File UploadFile(string uploadFile,  string parent)
        {
            var result = new GoogleAPI();
            if (System.IO.File.Exists(uploadFile))
            {
                File body = new File();
                body.Name = Path.GetFileName(uploadFile);
                body.MimeType = GetMimeType(uploadFile);
                body.Parents = new List<string>
                    {
                        parent
                    };
                FileStream stream = new FileStream(uploadFile, FileMode.Open);
                try
                {
                    var service = result.ConnectApi();
                    FilesResource.CreateMediaUpload request = service.Files.Create(body, stream, GetMimeType(uploadFile));
                    request.Fields = "size";
                    request.Upload();
                    Console.WriteLine(uploadFile);
                    Console.WriteLine("Размер: " + request.ResponseBody.Size / Math.Pow(1024, 2)  + "мегабайт");
                    var resultsize = request.ResponseBody.Size;
                    Console.WriteLine("Загружено");
                    return request.ResponseBody;
                }

                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error Occured");
                    return null;
                }
            }
            else
            {
                Console.WriteLine("Не загружено");
                return null;
            }
        }

        public async Task<File> AsyncUploadFile(string path, string parent = "root")
        {
            return await Task.Run(() => UploadFile(path, parent));
        }
    }
}
