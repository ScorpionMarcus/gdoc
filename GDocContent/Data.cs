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
    class Data
    {
        static string[] Scopes = { DocsService.Scope.DocumentsReadonly };
        static string ApplicationName = "Google Docs API .NET Quickstart";

        public static void Retrieve()
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

            formatUtil.Format(doc);
        }
    }
}
