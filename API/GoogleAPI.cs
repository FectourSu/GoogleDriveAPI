using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace API
{

    class GoogleAPI
    {
        /// <summary>
        /// Подключение к GoogleAPI
        /// </summary>
        static string[] Scopes = { DriveService.Scope.Drive };
        static string ApplicationName = "Macross";
        public DriveService ConnectApi()
        {
            UserCredential credential;
            string startPath = AppDomain.CurrentDomain.BaseDirectory;
            using (var stream = new FileStream(startPath + "credentials.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(startPath + "token.json", true)).Result;
            }
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            return service;
        }
        public async void AsyncConnectApi()
        {
            await Task.Run(() => ConnectApi());
            Console.WriteLine("Подключение к Google Drive успешно выполнено!");
        }
    }
}
