using newZealandWalksAPI.Data;
using newZealandWalksAPI.Models.Domain;

namespace newZealandWalksAPI.Repositories
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly NZWalksDbContext _nzWalksDbContext;
        public ImageRepository(
            IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            NZWalksDbContext nZWalksDbContext
            )
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _nzWalksDbContext = nZWalksDbContext;
        }

        public async Task<Image> ImageUpload(Image image)
        {
            // Local path variable
            var localFilePath = Path
                .Combine(
                _webHostEnvironment.ContentRootPath,
                "Images",
                $"{image.FileName}{image.FileExtension}"
                );

            // Upload image to local path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            // file path   scheme://host/pathbase/imagesFolder/fileName/FileExtension
            var urlFilePath = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}{_httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath = urlFilePath;

            // Save to database - Add Image to Images table
            await _nzWalksDbContext.Images.AddAsync(image);
            await _nzWalksDbContext.SaveChangesAsync();

            return image;

        }
    }
}
