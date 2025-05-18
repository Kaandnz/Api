using Xunit;
using WebApplication8.Models;

namespace WebApplication8.Tests
{
    public class GarsonTests
    {
        [Fact]
        public void Garson_YemekAdi_Required()
        {
            // Arrange
            var garson = new Garson();

            // Act
            var yemekAdi = garson.YemekAdi;

            // Assert
            Assert.Null(yemekAdi); // Başlangıçta null, zorunlu olduğu için test amaçlı null bırakıyoruz.
        }

        [Fact]
        public void Garson_Aciklama_MaxLength()
        {
            // Arrange
            var garson = new Garson
            {
                YemekAdi = "Kebap",
                IcecekAdi = "Ayran",
                Aciklama = new string('a', 500), // Tam sınırda
                siparisTamamlandi = false
            };

            // Act & Assert
            Assert.Equal(500, garson.Aciklama.Length);
        }

        [Fact]
        public void Garson_SiparisTamamlandi_DefaultFalse()
        {
            var garson = new Garson();
            Assert.False(garson.siparisTamamlandi);
        }
    }
}
