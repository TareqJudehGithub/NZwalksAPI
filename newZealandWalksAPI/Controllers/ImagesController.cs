using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using newZealandWalksAPI.Models.Domain;
using newZealandWalksAPI.Models.DTO;
using newZealandWalksAPI.Repositories;

namespace newZealandWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {

        #region Fields
        private readonly IImageRepository _imageRepository;
        #endregion

        #region Constructors
        public ImagesController(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }
        #endregion
        // POST: /api/Images/Upload
        [HttpPost]
        [Route("ImageUpload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDTO imageUploadRequest)
        {
            // Check file extension and file size
            ValidateFileUpload(imageUploadRequest);

            if (ModelState.IsValid)
            {
                // Convert DTO to domain model
                var imageDomainModel = new Image
                {
                    File = imageUploadRequest.File,
                    FileName = imageUploadRequest.FileName,
                    FileExtension = Path.GetExtension(imageUploadRequest.File.FileName),
                    FileSizeInBytes = imageUploadRequest.File.Length,
                    FileDescription = imageUploadRequest.FileDescription
                };

                // User repository to upload image
                await _imageRepository.ImageUpload(imageDomainModel);

                return Ok(imageDomainModel);
            }

            // Error uploading file
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDTO imageUploadRequest)
        {
            // Check file name
            if (imageUploadRequest.FileName == null)
            {
                ModelState.AddModelError(key: "file", errorMessage: "FileName cannot be empty!");
            }

            var allowedExtension = new string[] { ".jpg", ".png", ".jpeg" };
            // Check file extension format
            if (!allowedExtension.Contains(Path.GetExtension(imageUploadRequest.File.FileName)))
            {
                ModelState.AddModelError(key: "file", errorMessage: "Invalid file extension!");
            }

            // Check file size - don't allow files that are greater than 10mg  
            if (imageUploadRequest.File.Length > 10485760)
            {
                ModelState.AddModelError(key: "file", errorMessage: "File size should be less than 10mg");
            }

        }

    }
}
