using System;
using System.IO;
using System.Linq;

namespace ConvertLabelsToPdf
{
    internal static class Converter
    {
        public static bool IsConvertedSuccessfully { get; private set; }

        public static string ErrorMessage { get; private set; }

        public static void GeneratePdfFromSourceImages(string sourceDirectoryPath, string targetDirectoryPath, string targetPdfFileName)
        {
            string[] filesToIterateThrough = Directory.GetFiles(sourceDirectoryPath);

            filesToIterateThrough = filesToIterateThrough.Where(x => x.EndsWith(".jpg") || x.EndsWith(".jpeg") || x.EndsWith(".gif") || x.EndsWith(".png")).ToArray();

            if (filesToIterateThrough.Length == 0)
            {
                ErrorMessage = $"There are no image files in directory: [{sourceDirectoryPath}]. No pdf file generated.";
                IsConvertedSuccessfully = false;

                return;
            }

            Console.WriteLine("Starting converting images into pdf ..");

            foreach(string fileName in filesToIterateThrough)
            {
                Console.WriteLine($"Processing file: [{fileName}]");
            }

            Console.WriteLine($"Target pdf file: [{targetPdfFileName}] generated!");

            IsConvertedSuccessfully = true;
        }
    }
}