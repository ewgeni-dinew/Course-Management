namespace CourseManagement.Services.Utils.Word
{
    using DocumentFormat.OpenXml;
    using DocumentFormat.OpenXml.Packaging;
    using DocumentFormat.OpenXml.Wordprocessing;
    using System.IO;

    public class WordService : IWordService
    {

        public byte[] GenerateWordFile(string title, string content)
        {
            var stream = new MemoryStream();

            //Create Document
            using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(stream, WordprocessingDocumentType.Document, true))
            {
                //Add a main document part
                var mainPart = wordDocument.AddMainDocumentPart();

                //Create the document structure
                mainPart.Document = new Document();
                var docBody = new Body();

                //Append header
                AppendParagraph(title, docBody);

                //Add course content to paragraphs
                foreach (var c in content.Split('\n'))
                {
                    AppendParagraph(c, docBody);
                }

                mainPart.Document.Body = docBody;
            }

            var res = stream.ToArray();

            stream.Dispose();

            return res;
        }

        private void AppendParagraph(string content, Body docBody)
        {
            var paragraph = new Paragraph();
            var run = new Run();
            var text = new Text(content);

            run.Append(text);
            paragraph.Append(run);

            docBody.Append(paragraph);
        }
    }

    public interface IWordService
    {
        public byte[] GenerateWordFile(string title, string content);
    }
}
