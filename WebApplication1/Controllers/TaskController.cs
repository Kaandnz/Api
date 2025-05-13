using Microsoft.AspNetCore.Mvc;
using WebApplication8.Models;
using WebApplication8.Repository;

namespace WebApplication8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IGarsonRepository _garsonRepository;
        private readonly IGenericRepository<Garson> _repository;
        private readonly IYemekRepository _yemekRepository;
        private readonly IIcecekRepository _icecekRepository;

        public TaskController(
            IGenericRepository<Garson> repository,
            IGarsonRepository garsonRepository,
            IYemekRepository yemekRepository,
            IIcecekRepository icecekRepository)
        {
            _repository = repository;
            _garsonRepository = garsonRepository;
            _yemekRepository = yemekRepository;
            _icecekRepository = icecekRepository;
        }

        [HttpGet("Siparis Listesi")]
        public ActionResult<IEnumerable<Garson>> Get()
        {
            var garsonlar = _repository.GetAll();

            var siparisler = garsonlar.Select(g =>
                $"Musteri numarasi: {g.Id} | " +
                $"Siparis ettiği ürün: {g.YemekAdi} | " +
                $"Sipariş ettiği içecek: {g.IcecekAdi} | " +
                $"Açıklama: {g.Aciklama} | " +
                $"Siparis Tamamlandi mi: {(g.siparisTamamlandi ? "Evet" : "Hayır")} | "
            ).ToList();

            return Ok(siparisler);
        }

        [HttpGet("Siparis Ara")]
        public ActionResult<object> Get(int id)
        {
            var garson = _repository.GetById(id);

            if (garson == null)
            {
                return NotFound();
            }

            var siparis = new
            {
                MusteriNumarasi = garson.Id,
                YemekAdi = garson.YemekAdi,
                IcecekAdi = garson.IcecekAdi,
                Aciklama = garson.Aciklama,
                SiparisTamamlandi = garson.siparisTamamlandi ? "Evet" : "Hayır"
            };

            return Ok(siparis);
        }

        [HttpPost("Siparis Ekle")]
        public ActionResult Post([FromBody] Garson garson)
        {
            
            var yemekAdiValid = _yemekRepository.GetAll().Any(y => y.YemekAdi.Equals(garson.YemekAdi, StringComparison.OrdinalIgnoreCase));
            if (!yemekAdiValid)
            {
                ModelState.AddModelError("YemekAdi", "Geçerli bir yemek adı girin. (Veritabanında bulunan yemek adlarından birini seçin)");
            }

            var icecekAdiValid = _icecekRepository.GetAll().Any(i => i.IcecekAdi.Equals(garson.IcecekAdi, StringComparison.OrdinalIgnoreCase));
            if (!icecekAdiValid)
            {
                ModelState.AddModelError("IcecekAdi", "Geçerli bir içecek adı girin. (Veritabanında bulunan içecek adlarından birini seçin)");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }


            _repository.Add(garson);
            return CreatedAtAction(nameof(Get), new { id = garson.Id }, garson);
        }


        [HttpPut("Siparis Guncelle")]
        public ActionResult Put(int id, [FromBody] Garson garson)
        {
            if (id != garson.Id)
            {
                return BadRequest();
            }

            var yemekAdiValid = _yemekRepository.GetAll().Any(y => y.YemekAdi.Equals(garson.YemekAdi, StringComparison.OrdinalIgnoreCase));
            if (!yemekAdiValid)
            {
                ModelState.AddModelError("YemekAdi", "Geçerli bir yemek adı girin. (Veritabanında bulunan yemek adlarından birini seçin)");
            }

            var icecekAdiValid = _icecekRepository.GetAll().Any(i => i.IcecekAdi.Equals(garson.IcecekAdi, StringComparison.OrdinalIgnoreCase));
            if (!icecekAdiValid)
            {
                ModelState.AddModelError("IcecekAdi", "Geçerli bir içecek adı girin. (Veritabanında bulunan içecek adlarından birini seçin)");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Update(garson);
            return NoContent();
        }

        [HttpDelete("Siparis Sil")]
        public ActionResult Delete(int id)
        {
            _repository.Delete(id);
            return NoContent();
        }

        //[HttpGet("Tamamlanan Siparisler")]
        //public ActionResult<IEnumerable<object>> GetComplatedGarsons()
        //{
        //    var garsonlar = _garsonRepository.GetComplatedGarsons();
        //    if (garsonlar == null || !garsonlar.Any())
        //    {
        //        return NotFound("Hiç tamamlanan sipariş bulunamadı.");
        //    }

        //    var siparisler = garsonlar.Select(g => new
        //    {
        //        MusteriNumarasi = g.Id,
        //        YemekAdi = g.YemekAdi,
        //        IcecekAdi = g.IcecekAdi,
        //        Aciklama = g.Aciklama,
        //        SiparisTamamlandi = g.siparisTamamlandi ? "Evet" : "Hayır"
        //    }).ToList();

        //    return Ok(siparisler);
        //}

        //[HttpGet("Tamamlanmayan Siparisler")]
        //public ActionResult<IEnumerable<object>> GetUnfinishedGarsons()
        //{
        //    var garsonlar = _garsonRepository.GetUnfinishedGarsons();
        //    if (garsonlar == null || !garsonlar.Any())
        //    {
        //        return NotFound("Hiç tamamlanmayan sipariş bulunamadı.");
        //    }

        //    var siparisler = garsonlar.Select(g => new
        //    {
        //        MusteriNumarasi = g.Id,
        //        YemekAdi = g.YemekAdi,
        //        IcecekAdi = g.IcecekAdi,
        //        Aciklama = g.Aciklama,
        //        SiparisTamamlandi = g.siparisTamamlandi ? "Evet" : "Hayır"
        //    }).ToList();

        //    return Ok(siparisler);
        //}
    }
}
