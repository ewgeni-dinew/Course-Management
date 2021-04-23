namespace CourseManagement.Services.Utils
{
    using DinkToPdf;
    using DinkToPdf.Contracts;
    using System;

    public class PdfService : IPdfService
    {
        private readonly IConverter _converter;

        public PdfService(IConverter converter)
        {
            _converter = converter;
        }
        public byte[] GeneratePdfFile()
        {
            try
            {
                var html = $@"
                        <!DOCTYPE html>
                        <html lang=""en"">
                        <head>
                            This is the header of this document.
                        </head>
                        <body>
                            <h1>This is the heading for demonstration purposes only.</h1>
                            <p>This is a line of text for demonstration purposes only.</p>
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
                Console.WriteLine(ex.Message);
                throw;
            }
            
        }
    }

    public interface IPdfService
    {
        public byte[] GeneratePdfFile();
    }
}
