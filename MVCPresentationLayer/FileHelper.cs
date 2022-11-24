namespace MVCPresentationLayer
{
    public class FileHelper
    {
        public static bool IsValidFile(string fileName)
        {
            return fileName.Contains(".jpg") || fileName.Contains(".png") || fileName.Contains(".jpeg") || fileName.Contains(".gif");
        }
    }
}

