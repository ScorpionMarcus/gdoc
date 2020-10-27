using Google.Apis.Docs.v1.Data;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace GDocContent
{
    class formatUtil
    {
        public static string format(Document doc, string item)
        {
            if (doc.Body.Content[0].Paragraph.Elements[0].TextRun.TextStyle.Bold != null)
            {
                item = $"<strong>{item}</strong>";
            }

            if (doc.Body.Content[0].Paragraph.Elements[0].TextRun.TextStyle.Italic != null)
            {
                item = $"<em>{item}</em>";
            }

            return item;
        }
    }
}
