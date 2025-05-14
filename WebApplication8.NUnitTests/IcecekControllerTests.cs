using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;

namespace WebApplication8.NUnitTests;

[TestFixture]
public class IcecekControllerTests
{
    private WebApplicationFactory<WebApplication1.Program> _factory = null!;
    private HttpClient _client = null!;

    [OneTimeSetUp]
    public void Setup()
    {
        _factory = new WebApplicationFactory<WebApplication1.Program>();
        _client = _factory.CreateClient();
    }

    [Test]
    public async Task Icecek_Listesi_Doner200()
    {
        var res = await _client.GetAsync("/api/Icecek/IcecekListesi");
        Assert.That(res.StatusCode, Is.EqualTo(HttpStatusCode.OK));
    }

    [Test]
    public async Task Hatali_IcecekAdiyla_SiparisEkle_Doner400()
    {
        const string body = """
            { "yemekAdi":"Pizza",
              "icecekAdi":"Yanlis",
              "aciklama":"test",
              "siparisTamamlandi":false }
            """;

        var res = await _client.PostAsync(
            "/api/Task/SiparisEkle",
            new StringContent(body, Encoding.UTF8, "application/json"));

        Assert.That(res.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
    }
}
