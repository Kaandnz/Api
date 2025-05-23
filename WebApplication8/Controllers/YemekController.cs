﻿using Microsoft.AspNetCore.Mvc;
using WebApplication8.Models;
using WebApplication8.Repository;

namespace WebApplication8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YemekController : ControllerBase
    {
        private readonly IYemekRepository _yemekRepository;

        public YemekController(IYemekRepository yemekRepository)
        {
            _yemekRepository = yemekRepository;
        }

        [HttpGet("YemekListesi")]
        public ActionResult<IEnumerable<Yemek>> GetAllYemekler()
        {
            var yemekler = _yemekRepository.GetAll();
            return Ok(yemekler);
        }

        [HttpGet("YemekBul")]
        public ActionResult<Yemek> GetYemekById(int id)
        {
            var yemek = _yemekRepository.GetById(id);
            if (yemek == null)
            {
                return NotFound();
            }
            return Ok(yemek);
        }

        [HttpPost("YemekEkle")]
        public ActionResult AddYemek([FromBody] Yemek yemek)
        {
            _yemekRepository.Add(yemek);
            return CreatedAtAction(nameof(GetAllYemekler), new { id = yemek.Id }, yemek);
        }

        [HttpPut("YemegiGuncelle")]
        public ActionResult UpdateYemek(int id, [FromBody] Yemek yemek)
        {
            if (id != yemek.Id)
            {
                return BadRequest();
            }

            _yemekRepository.Update(yemek);
            return NoContent();
        }

        [HttpDelete("YemegiSil")]
        public ActionResult DeleteYemek(int id)
        {
            _yemekRepository.Delete(id);
            return NoContent();
        }
    }
}
