using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    class UploadTemp
    {
        /// <summary>
        /// Метод загрузки папки
        /// </summary>
        public async Task UploadDirectoryAsync(string dirPath, string parent = "root")
        {
            CreateFolderAPI google = new CreateFolderAPI();
            string dirName = dirPath.Split('\\').Last();
            var dirRespons = await google.AsyncCreateFolder(dirName, parent);
            FileInfo[] files = null;
            DirectoryInfo[] subDirectories = null;
            try
            {
                files = new DirectoryInfo(dirPath).GetFiles();
                subDirectories = new DirectoryInfo(dirPath).GetDirectories();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            if (files != null || subDirectories != null)
            {
                FileUpload file = new FileUpload();
                foreach (var item in files)
                {
                    var fileRespons = await file.AsyncUploadFile(item.FullName, dirRespons.Id);
                }
                foreach (var item in subDirectories)
                {
                    await UploadDirectoryAsync(item.FullName, dirRespons.Id);
                }
            }
        }
    }
}
