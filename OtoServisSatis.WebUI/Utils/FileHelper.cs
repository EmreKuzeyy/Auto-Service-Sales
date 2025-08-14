namespace OtoServisSatis.WebUI.Utils
{
    public class FileHelper
    {
        public static async Task<string> FileLoaderAsync(IFormFile formFile, string filePath = "/Img/")
        {
            var fileName = "";
            if (formFile != null && formFile.Length > 0)
            {
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);
                string directory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath.TrimStart('/'), fileName);

                // Klasör yoksa oluştur
                var folderPath = Path.GetDirectoryName(directory);
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                using var stream = new FileStream(directory, FileMode.Create);
                await formFile.CopyToAsync(stream);
            }
            return fileName;
        }


        public static void DeleteFile(string fileName, string filePath = "/Img/Vehicles/")
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filePath.TrimStart('/'), fileName);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
        }

    }
}
