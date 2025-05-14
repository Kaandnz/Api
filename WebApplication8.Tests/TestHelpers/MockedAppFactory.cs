using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using WebApplication8;
using WebApplication8.Models;
using WebApplication8.Repository;

namespace WebApplication8.Tests.TestHelpes;

/// <summary>
/// API’yi in‑memory ayağa kaldırır ve gerçek Mongo repo’larının yerine Moq nesneleri enjekte eder.
/// Testler bu fabrikanın property’leri üzerinden mock’lara erişip davranış tanımlar.
/// </summary>
public sealed class MockedAppFactory : WebApplicationFactory<Program>
{
    public Mock<IIcecekRepository> IcecekMock { get; } = new();
    public Mock<IYemekRepository> YemekMock { get; } = new();
    public Mock<IGenericRepository<Garson>> GarsonGenericMock { get; } = new();
    public Mock<IGarsonRepository> GarsonRepoMock { get; } = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Önce gerçek kayıtları sök
            services.RemoveAll<IIcecekRepository>();
            services.RemoveAll<IYemekRepository>();
            services.RemoveAll<IGenericRepository<Garson>>();
            services.RemoveAll<IGarsonRepository>();

            // Sonra mock’ları ekle
            services.AddSingleton(IcecekMock.Object);
            services.AddSingleton(YemekMock.Object);
            services.AddSingleton(GarsonGenericMock.Object);
            services.AddSingleton(GarsonRepoMock.Object);
        });
    }
}
