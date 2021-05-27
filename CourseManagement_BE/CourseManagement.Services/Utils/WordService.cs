namespace CourseManagement.Services.Utils
{
    using Microsoft.Office.Interop.Word;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;

    public class WordService : IWordService
    {
        public byte[] GenerateWordFile(string title, string content)
        {
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

            foreach (var c in content.Split('\n'))
            {
                paragraph = doc.Content.Paragraphs.Add(Missing.Value);
                paragraph.Range.Text = c;
                paragraph.Range.Font.Size = 12;
                paragraph.Range.Font.Bold = 0;
                paragraph.Range.Font.Name = "Times New Roman (Headings CS)";
                paragraph.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                paragraph.Shading.ForegroundPatternColor = WdColor.wdColorWhite;
                paragraph.Range.InsertParagraphAfter();
            }

            var docStr = doc.WordOpenXML;

            doc.Close(0);
            word.Quit();
            Marshal.ReleaseComObject(doc);

            return Encoding.UTF8.GetBytes(docStr);
        }
    }

    public interface IWordService
    {
        public byte[] GenerateWordFile(string title, string content);
    }
}
