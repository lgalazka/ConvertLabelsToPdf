using System;
using System.IO;
using System.Linq;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace ConvertLabelsToPdf
{
    internal static class Converter
    {
        private static string _sourceDirectoryPath;

        public static bool IsConvertedSuccessfully { get; private set; }

        public static string ErrorMessage { get; private set; }

        public static void GeneratePdfFromSourceImages(string sourceDirectoryPath, string targetDirectoryPath, string targetPdfFileName)
        {
            string[] filesToIterateThrough = Directory.GetFiles(sourceDirectoryPath);

            filesToIterateThrough = filesToIterateThrough
                .Where(x =>
                    x.EndsWith(".jpg", StringComparison.CurrentCultureIgnoreCase)
                    || x.EndsWith(".jpeg", StringComparison.CurrentCultureIgnoreCase)
                    || x.EndsWith(".gif", StringComparison.CurrentCultureIgnoreCase)
                    || x.EndsWith(".png", StringComparison.CurrentCultureIgnoreCase))
                .ToArray();

            if (filesToIterateThrough.Length == 0)
            {
                ErrorMessage = $"There are no image files in directory: [{sourceDirectoryPath}]. No pdf file generated.";
                IsConvertedSuccessfully = false;

                return;
            }

            Console.WriteLine("Converting images to pdf ..");

            _sourceDirectoryPath = sourceDirectoryPath;

            var pdfDocument = new PdfDocument();

            foreach(string fileName in filesToIterateThrough)
            {
                Console.WriteLine($"Processing file: [{fileName}]");

                PdfPage page = pdfDocument.AddPage();
                DrawPage(page, fileName);
            }

            targetDirectoryPath = string.IsNullOrEmpty(targetDirectoryPath) ? sourceDirectoryPath : targetDirectoryPath;
            targetPdfFileName = (string.IsNullOrEmpty(targetPdfFileName) ? DateTime.Now.Ticks.ToString() : targetPdfFileName) + ".pdf";

            pdfDocument.Save(Path.Combine(targetDirectoryPath, targetPdfFileName));

            Console.WriteLine($"Target pdf file: [{targetPdfFileName}] generated!");

            IsConvertedSuccessfully = true;
        }

        private static void DrawPage(PdfPage page, string fileName)
        {
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XImage image = XImage.FromFile(Path.Combine(_sourceDirectoryPath, fileName));

            double width = page.Width;
            double height = page.Height;

            gfx.DrawImage(image, 0, 0, width, height );
        }
    }
}