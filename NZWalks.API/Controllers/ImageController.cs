using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImageController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        // POST: /api/image/upload
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> upload([FromForm] ImageUploadRequestDto request)
        {
            validateFileUpload(request);

            if(ModelState.IsValid)
            {
                //convert DTO in domain mode
                var imageDomainMode = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription,
                };

                //use repository to upload image
                await imageRepository.Upload(imageDomainMode);
                
                return Ok(imageDomainMode);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        private void validateFileUpload(ImageUploadRequestDto request)
        {            
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }

            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10MB, please upload a smaller size file.");
            }
        }
    }
}
