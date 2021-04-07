using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private IImageService _imageService;
        public ImageController(IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost("InsertImage")]
        public IActionResult InsertImage([FromForm] CarImage carImage, [FromForm(Name = "image")] IFormFile formFile)
        {
            var result = _imageService.Add(carImage, formFile);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("DeleteImage")]
        public IActionResult DeleteImage([FromForm] int id)
        {
            var carImage = _imageService.GetById(id).Data;
            if (carImage == null)
            {
                return NotFound();
            }

            var result = _imageService.Delete(carImage);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("UpdateImage")]
        public IActionResult UpdateImage([FromForm] int id, [FromForm(Name = "image")] IFormFile formFile)
        {
            var carImage = _imageService.GetById(id).Data;
            var result = _imageService.Update(carImage, formFile);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
