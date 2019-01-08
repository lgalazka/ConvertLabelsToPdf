namespace ConvertLabelsToPdf
{
    interface IConverter
    {
        void SetDirectoryForSourceImages(string path);

        void GeneratePdfFromSourceImages(string targetDirectory, string targetPdfName);
    }
}