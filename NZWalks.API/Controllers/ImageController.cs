using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        // POST: /api/image/upload
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> upload([FromForm] ImageUploadRequestDto request)
        {
            validateFileUpload(request);
            if(ModelState.IsValid)
            {
                //use repository to upload image
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
