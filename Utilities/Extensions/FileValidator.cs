namespace FruitSimulation.Utilities.Extensions
{
    public static class FileValidator
    {
        public static bool CheckFileType(this IFormFile file,string type)
        {
            if (file.ContentType.Contains(type))
            {
                return true;
            }

            return false;
        }

        public static async Task<string> CreateFileAsync(this IFormFile file,params string[] paths)
        {
            string fileName = string.Concat(Guid.NewGuid().ToString(), file.FileName);

            string path = string.Empty;

            foreach (var way in paths)
            {
                path = Path.Combine(path, way); 
            }

            path = Path.Combine(path,fileName);

            using (FileStream fileStream =  new FileStream(path,FileMode.CreateNew))
            {
                await file.CopyToAsync(fileStream);
            }

            return fileName;
        }


        public static void DeleteFile(this string fileName,params string[] paths)
        {
            string path = string.Empty;

            foreach (var way in paths)
            {
                path = Path.Combine(path, way);
            }

            path = Path.Combine(path, fileName);

            File.Delete(path);
        }
    }
}
