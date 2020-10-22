using Google.Apis.Docs.v1.Data;
using System.Text.Json;
using System;
using System.IO;

namespace GDocContent
{
    class formatUtil
    {
        public static void Format(Document doc)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string content = path + "\\content.txt";
            string jsonPath = path + "\\results.json";

            // save json to desktop
            string jsonString = JsonSerializer.Serialize(doc);

            if (!File.Exists(jsonPath))
            {
                File.WriteAllText(jsonPath, jsonString);
            }

            // skim through json results for content
            foreach (var item in doc.Body.Content)
            {
                if (item.Paragraph != null)
                {
                    if (item.Paragraph.ParagraphStyle.NamedStyleType == "NORMAL_TEXT")
                    {
                        File.AppendAllText(content, "<p>" + item.Paragraph.Elements[0].TextRun.Content + "</p>");
                    }
                    else if (item.Paragraph.ParagraphStyle.NamedStyleType == "HEADING_1")
                    {
                        File.AppendAllText(content, "<h1>" + item.Paragraph.Elements[0].TextRun.Content + "</h1>");
                    }
                    else
                    {
                        File.AppendAllText(content, "Must be something else");
                    }
                }
            }
        }
    }
}
