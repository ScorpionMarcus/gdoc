using Google.Apis.Auth.OAuth2;
using Google.Apis.Docs.v1;
using Google.Apis.Docs.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Threading;

namespace GDocContent
{
    class GDocData
    {
        static string[] Scopes = { DocsService.Scope.DocumentsReadonly };
        static string ApplicationName = "Google Docs API .NET Quickstart";

        public static void Run()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Docs API service.
            var service = new DocsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define request parameters.
            String documentId = "1qAnB3vcKDsSuwFmRB0gtDyepFx60t1CdI9hbKAvCXQc";
            DocumentsResource.GetRequest request = service.Documents.Get(documentId);

            // https://docs.google.com/document/d/195j9eDD3ccgjQRttHhJPymLJUCOUjs-jmwTrekvdjFE
            Document doc = request.Execute();

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\test.txt";

            Console.WriteLine("Creating file test.txt located at " + path);
            
            // Skims through json results for content
            foreach (var item in doc.Body.Content)
            {
                if (item.Paragraph != null)
                {
                    File.AppendAllText(path, item.Paragraph.Elements[0].TextRun.Content);
                }
            }
        }
    }
}
