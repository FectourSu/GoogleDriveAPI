using System.Collections.Generic;
using System.Threading.Tasks;
using File = Google.Apis.Drive.v3.Data.File;

namespace API
{
    class CreateFolderAPI
    {
        /// <summary>
        /// Создание папки на Google Drive (для дальнейшего помещения в неё загруженных файлов)
        /// </summary>
        public File CreateFolder(string name, string parentsId = "root")
        {
            GoogleAPI google = new GoogleAPI();
            var service = google.ConnectApi();
            var fileMet = new File()
            {
                Name = name,
                MimeType = "application/vnd.google-apps.folder",
                Parents = new List<string>() { parentsId }
            };
            var request = service.Files.Create(fileMet);
            request.Fields = "id, name";
            return request.Execute();
        }
        public async Task<File> AsyncCreateFolder(string name, string parentsId = "root")
        {
            return await Task.Run(() => CreateFolder(name, parentsId));
        }
    }
}
