using System.ComponentModel.DataAnnotations;


namespace WebApplication8.Models
{
    public class Garson
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Yemek adı gereklidir.")]

        public string YemekAdi { get; set; }

        [Required(ErrorMessage = "İçecek adı gereklidir.")]
     
        public string IcecekAdi { get; set; }

        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir.")]
        public string Aciklama { get; set; }

        [Required(ErrorMessage = "Sipariş tamamlanma durumu gereklidir.")]
        public bool siparisTamamlandi { get; set; }
    }
}
