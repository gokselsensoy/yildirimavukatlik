using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LawyerController : ControllerBase
    {
        ILawyerService _lawyerService;
        public LawyerController(ILawyerService lawyerService)
        {
            _lawyerService = lawyerService;
        }

        [HttpGet("getall")]
        //[Authorize()]
        public IActionResult GetAll()
        {
            var result = _lawyerService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Lawyer lawyer)
        {
            var result = _lawyerService.Add(lawyer);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("delete")]
        public IActionResult Delete(Lawyer lawyer)
        {
            var result = _lawyerService.Delete(lawyer);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("update")]
        public IActionResult Update(Lawyer lawyer)
        {
            var result = _lawyerService.Update(lawyer);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getlawyerdetails")]
        public IActionResult GetDetails(int lawyerId)
        {
            var result = _lawyerService.GetLawyerDetails(lawyerId);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
