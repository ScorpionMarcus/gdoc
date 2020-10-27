using Google.Apis.Docs.v1.Data;
using System.Text.Json;
using System;
using System.IO;

namespace GDocContent
{
    class fileUtil
    {
        public static void createFile(Document doc)
        {
            Random rnd = new Random();
            int job = rnd.Next(1, 50000);

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string content = path + $"\\content-{job}.txt";
            string jsonPath = path + $"\\someResults-{job}.json";

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
                    if (item.Paragraph.ParagraphStyle.NamedStyleType == "NORMAL_TEXT" && item.Paragraph.Elements[0].TextRun.Content != "\n")
                    {
                        //File.AppendAllText(content, "<p>" + formatUtil.format(doc, item.Paragraph.Elements[0].TextRun.Content) + "</p>");
                        File.AppendAllText(content, "<p>" + item.Paragraph.Elements[0].TextRun.Content + "</p>");
                    }
                    else if (item.Paragraph.ParagraphStyle.NamedStyleType == "HEADING_1")
                    {
                        File.AppendAllText(content, "<h1>" + item.Paragraph.Elements[0].TextRun.Content + "</h1>");
                    }
                    else if (item.Paragraph.ParagraphStyle.NamedStyleType == "HEADING_2")
                    {
                        File.AppendAllText(content, "<h2>" + item.Paragraph.Elements[0].TextRun.Content + "</h2>");
                    }
                    else if (item.Paragraph.ParagraphStyle.NamedStyleType == "HEADING_3")
                    {
                        File.AppendAllText(content, "<h3>" + item.Paragraph.Elements[0].TextRun.Content + "</h3>");
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
    }
}
