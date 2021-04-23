using Microsoft.Office.Interop.Word;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CourseManagement.Services.Utils
{
    public class WordService
    {
        public byte[] GenerateWordFile(string title, string content)
        {
            using (var memoryStream = new MemoryStream())
            {
                using (var wordDoc = WordprocessingDocument.Create(mem, DocumentFormat.OpenXml.WordprocessingDocumentType.Document, true))
                {
                }
            }

            var word = new Application();
            word.Documents.Add();
            word.Visible = false;
            word.DisplayAlerts = WdAlertLevel.wdAlertsNone;
            var doc = word.ActiveDocument;

            var paragraph = doc.Content.Paragraphs.Add(Missing.Value);
            paragraph.Range.Text = title;
            paragraph.Range.Font.Size = 22;
            paragraph.Range.Font.Bold = 1;
            paragraph.Range.Font.Name = "Times New Roman (Headings CS)";
            paragraph.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
            paragraph.Range.InsertParagraphAfter();

            paragraph = doc.Content.Paragraphs.Add(Missing.Value);
            paragraph.Range.Text = "I am able to handle multiple tasks on a daily basis. I use a creative approach to problem solve. I am a dependable person who is great at time management. I am always energetic and eager to learn new skills. I am flexible in my working hours, being able to work evenings and weekends. I am hardworking and always the last to leave the office in the evening.";
            paragraph.Range.Font.Size = 12;
            paragraph.Range.Font.Bold = 0;
            paragraph.Range.Font.Name = "Times New Roman (Headings CS)";
            paragraph.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
            paragraph.Shading.ForegroundPatternColor = WdColor.wdColorWhite;
            paragraph.Range.InsertParagraphAfter();


            doc.Close(0);
            word.Quit();
            Marshal.ReleaseComObject(doc);
        }
    }
}
