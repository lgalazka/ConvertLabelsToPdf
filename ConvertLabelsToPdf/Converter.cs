namespace ConvertLabelsToPdf
{
    internal static class Converter
    {
        public static bool IsConvertedSuccessfully { get; private set; }

        public static string ErrorMessage { get; private set; }

        public static void GeneratePdfFromSourceImages(string sourceDirectoryPath, string targetDirectoryPath, string targetPdfFileName)
        {
            ErrorMessage = "";
            IsConvertedSuccessfully = true;
        }
    }
}