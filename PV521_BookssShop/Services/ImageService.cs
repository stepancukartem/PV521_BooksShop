namespace PV521_BookssShop.Services
{
    public class ImageService
    {
        private readonly IWebHostEnvironment _environment;

        public ImageService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string?> SaveImageAsync(IFormFile? image)
        {
            if (image == null || image.Length == 0)
                return null;

            string folder = Path.Combine(
                _environment.WebRootPath ?? Path.Combine(_environment.ContentRootPath, "wwwroot"),
                "images");

            Directory.CreateDirectory(folder);

            string extension = Path.GetExtension(image.FileName);
            string fileName = $"{Guid.NewGuid()}{extension}";

            string filePath = Path.Combine(folder, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await image.CopyToAsync(stream);

            return $"/images/{fileName}";
        }

        public void DeleteImage(string? imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                return;

            string fileName = Path.GetFileName(imagePath);

            string filePath = Path.Combine(
                _environment.WebRootPath ?? Path.Combine(_environment.ContentRootPath, "wwwroot"),
                "images",
                fileName);

            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}