using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using newZealandWalksAPI.Models.DTO;

namespace newZealandWalksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        // POST: /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDTO imageUploadRequest)
        {
            ValidateFileUpload(imageUploadRequest);
            if (ModelState.IsValid)
            {
                // User repository to upload image
                return Ok();
            }

            // Error uploading file
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDTO imageUploadRequest)
        {
            var allowedExtension = new string[] { ".jpg", "png", "jpeg" };

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
