using Microsoft.AspNetCore.Mvc;
using WebApplication8.Models;
using WebApplication8.Repository;

namespace WebApplication8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IcecekController : ControllerBase
    {
        private readonly IIcecekRepository _icecekRepository;

        public IcecekController(IIcecekRepository icecekRepository)
        {
            _icecekRepository = icecekRepository;
        }

        [HttpGet("IcecekListesi")]
        public ActionResult<IEnumerable<Icecek>> GetAllIcecekler()
        {
            var icecekler = _icecekRepository.GetAll();
            return Ok(icecekler);
        }

        [HttpGet("IcecegiBul")]
        public ActionResult<Icecek> GetIcecekById(int id)
        {
            var icecek = _icecekRepository.GetById(id);
            if (icecek == null)
            {
                return NotFound();
            }
            return Ok(icecek);
        }

        [HttpPost("IcecekEkle")]
        public ActionResult AddIcecek([FromBody] Icecek icecek)
        {
            _icecekRepository.Add(icecek);
            return CreatedAtAction(nameof(GetAllIcecekler), new { id = icecek.Id }, icecek);
        }

        [HttpPut("IcecegiGuncelle")]
        public ActionResult UpdateIcecek(int id, [FromBody] Icecek icecek)
        {
            if (id != icecek.Id)
            {
                return BadRequest();
            }

            _icecekRepository.Update(icecek);
            return NoContent();
        }

        [HttpDelete("IcecekSil")]
        public ActionResult DeleteIcecek(int id)
        {
            _icecekRepository.Delete(id);
            return NoContent();
        }
    }
}
