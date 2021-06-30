namespace CourseManagement.Services.Utils.Word
{
    using DocumentFormat.OpenXml;
    using DocumentFormat.OpenXml.Packaging;
    using DocumentFormat.OpenXml.Wordprocessing;
    using System.IO;

    internal class WordService : IWordService
    {

        public byte[] GenerateWordFile(string title, string content)
        {
            using (MemoryStream mem = new MemoryStream())
            {
                // Create Document
                using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(mem, WordprocessingDocumentType.Document, true))
                {
                    // Add a main document part. 
                    MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();

                    // Create the document structure and add some text.
                    mainPart.Document = new Document();
                    Body docBody = new Body();

                    // Add your docx content here
                    Paragraph p = new Paragraph();
                    Run r = new Run();
                    Text t = new Text("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Praesent quam augue, tempus id metus in, laoreet viverra quam. Sed vulputate risus lacus, et dapibus orci porttitor non.");
                    r.Append(t);
                    p.Append(r);
                    docBody.Append(p);

                    mainPart.Document.Body = docBody;
                }

                // Download File
                //Context.Response.AppendHeader("Content-Disposition", String.Format("attachment;filename=\"0}.docx\"", MyDocxTitle));
                //mem.Position = 0;
                //mem.CopyTo(Context.Response.OutputStream);
                //Context.Response.Flush();
                //Context.Response.End();

                return mem.ToArray();
            }

            throw new System.NotImplementedException();
        }
    }

    public interface IWordService
    {
        public byte[] GenerateWordFile(string title, string content);
    }
}
