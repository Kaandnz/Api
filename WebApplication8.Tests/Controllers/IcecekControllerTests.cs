using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using WebApplication8.Models;
using WebApplication8.Tests.TestHelpes;
using Xunit;

namespace WebApplication8.Tests.Controllers;

public class IcecekControllerTests : IClassFixture<MockedAppFactory>
{
    private readonly HttpClient _client;
    private readonly MockedAppFactory _factory;

    public IcecekControllerTests(MockedAppFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact(DisplayName = "GET /api/Icecek/Icecek Listesi deneme → 200 & beklenen json")]
    public async Task Icecek_Listesi_Basarili_Doner()
    {
        // Arrange – mock repo sabit veri döndürsün
        var beklenen = new[]
        {
            new Icecek { Id = 1, IcecekAdi = "Kola" },
            new Icecek { Id = 2, IcecekAdi = "Ayran" }
        };
        _factory.IcecekMock.Setup(r => r.GetAll()).Returns(beklenen);

        // Act
        var response = await _client.GetAsync("/api/Icecek/Icecek Listesi deneme");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var body = await response.Content.ReadFromJsonAsync<Icecek[]>();
        body.Should().BeEquivalentTo(beklenen);
    }
}
