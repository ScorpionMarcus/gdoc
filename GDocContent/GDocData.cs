using Google.Apis.Auth.OAuth2;
using Google.Apis.Docs.v1;
using Google.Apis.Docs.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Runtime.CompilerServices;
using System.Linq;

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

            // Prints the title of the requested doc:
            // https://docs.google.com/document/d/195j9eDD3ccgjQRttHhJPymLJUCOUjs-jmwTrekvdjFE
            Document doc = request.Execute();

            string jsonString = JsonSerializer.Serialize(doc);

            // downloads the full json for review
            string path = @"c:\temp\test.json";

            if (!File.Exists(path))
            {
                File.WriteAllText(path, jsonString);
            }

            var count = 0;

            foreach (var item in doc.Body.Content)
            {
                if (item.Paragraph != null)
                {
                    Console.WriteLine(item.Paragraph.Elements[0].TextRun.Content);
                    count += 1;
                }
            }
        }
    }
}
