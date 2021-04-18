﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Entities.Concrete;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarImagesController : ControllerBase
    {
       ICarImageService _carImageService;

       public CarImagesController(ICarImageService carImageService)
       {
           _carImageService = carImageService;
       }
       [HttpPost("add")]
       public IActionResult Add([FromForm(Name = ("ImagePath"))] IFormFile file, [FromForm] CarImage carImage)
       {
           var result = _carImageService.Add(file, carImage);
           if (result.Success)
           {
               return Ok(result);
           }
           return BadRequest(result);
       }
       [HttpDelete("delete")]
       public IActionResult Delete([FromForm(Name = ("Id"))] int Id)
       {

           var carImage = _carImageService.GetById(Id).Data;

           var result = _carImageService.Delete(carImage);
           if (result.Success)
           {
               return Ok(result);
           }
           return BadRequest(result);
       }
       [HttpPut("update")]
       public IActionResult Update([FromForm(Name = ("ImagePath"))] IFormFile file, [FromForm(Name = ("Id"))] int Id)
       {
           var carImage = _carImageService.GetById(Id).Data;
           var result = _carImageService.Update(file, carImage);
           if (result.Success)
           {
               return Ok(result);
           }
           return BadRequest(result);
       }
    }
}