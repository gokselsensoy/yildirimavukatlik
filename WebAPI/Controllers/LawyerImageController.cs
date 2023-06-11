using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LawyerImageController : ControllerBase
    {
        ILawyerImageService _lawyerImageService;
        public LawyerImageController(ILawyerImageService lawyerImageService)
        {
            _lawyerImageService = lawyerImageService;
        }
        [HttpPost("add")]
        public IActionResult Add([FromForm] int lawyerId, [FromForm] IFormFile lawyerImage)
        {
            var result = _lawyerImageService.Add(lawyerImage, lawyerId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(LawyerImage lawyerImage)
        {
            var lawyerDeleteImage = _lawyerImageService.GetByImageId(lawyerImage.Id).Data;
            var result = _lawyerImageService.Delete(lawyerDeleteImage);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update([FromForm] IFormFile file, [FromForm] LawyerImage lawyerImage)
        {
            var result = _lawyerImageService.Update(file, lawyerImage);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _lawyerImageService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbylawyerid")]
        public IActionResult GetByLawyerId(int lawyerId)
        {
            var result = _lawyerImageService.GetByLawyerId(lawyerId);
            if (result.Success)
            {
                return Ok(result);
            }
            return Ok(result);
        }

        [HttpGet("getbyimageid")]
        public IActionResult GetByImageId(int imageId)
        {
            var result = _lawyerImageService.GetByImageId(imageId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
