namespace CourseManagement.Services.Utils
{
    using System;
    using System.Text;
    using DinkToPdf;
    using DinkToPdf.Contracts;

    public class PdfService : IPdfService
    {
        private readonly IConverter _converter;

        public PdfService(IConverter converter)
        {
            this._converter = converter;
        }

        public byte[] GeneratePdfFile(string title, string content)
        {
            try
            {
                var paragraphs = new StringBuilder();

                //proper format of paragraphs
                foreach (var c in content.Split('\n'))
                    paragraphs.AppendLine($"<p>{c}</p>");

                var html = $@"
                        <!DOCTYPE html>
                        <html lang=""en"">
                        <head>
                            Powered by Boreas.
                        </head>
                        <body>
                            <h1>{title}</h1>
                            {paragraphs}
                        </body>
                        </html>
                        ";

                var globalSettings = new GlobalSettings
                {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings { Top = 25, Bottom = 25 }
                };

                var objectSettings = new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = html
                };

                var webSettings = new WebSettings
                {
                    DefaultEncoding = "utf-8"
                };

                var headerSettings = new HeaderSettings
                {
                    FontSize = 15,
                    FontName = "Ariel",
                    Right = "Page [page] of [toPage]",
                    Line = true
                };

                var footerSettings = new FooterSettings
                {
                    FontSize = 12,
                    FontName = "Ariel",
                    Center = "This is for demonstration purposes only.",
                    Line = true
                };

                objectSettings.HeaderSettings = headerSettings;
                objectSettings.FooterSettings = footerSettings;
                objectSettings.WebSettings = webSettings;

                var htmlToPdfDocument = new HtmlToPdfDocument()
                {
                    GlobalSettings = globalSettings,
                    Objects = { objectSettings },
                };

                return _converter.Convert(htmlToPdfDocument);
            }
            catch (Exception ex)
            {
                //TODO throw new PDF exception
                throw ex;
            }
        }
    }

    public interface IPdfService
    {
        public byte[] GeneratePdfFile(string title, string content);
    }
}
