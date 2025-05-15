using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApplication8.Controllers;
using WebApplication8.Models;
using WebApplication8.Repository;

public class IcecekControllerTests
{
    private readonly IcecekController _sut;          // System-Under-Test
    private readonly Mock<IIcecekRepository> _repo;

    public IcecekControllerTests()
    {
        _repo = new Mock<IIcecekRepository>();
        _sut = new IcecekController(_repo.Object);
    }

    [Fact(DisplayName = "GET /Icecek Listesi 200 döner")]
    public void GetAllIcecekler_Ok()
    {
        // arrange
        var liste = new[]
        {
            new Icecek {Id = 1, IcecekAdi = "Kola"},
            new Icecek {Id = 2, IcecekAdi = "Ayran"}
        };
        _repo.Setup(r => r.GetAll()).Returns(liste);

        // act
        var sonuc = _sut.GetAllIcecekler();

        // assert
        sonuc.Result.Should().BeOfType<OkObjectResult>()
              .Which.Value.Should().BeEquivalentTo(liste);
    }

    [Fact(DisplayName = "GET /Icecegi Bul 404 döner")]
    public void GetIcecekById_NotFound()
    {
        _repo.Setup(r => r.GetById(5)).Returns((Icecek?)null);

        var sonuc = _sut.GetIcecekById(5);

        sonuc.Result.Should().BeOfType<NotFoundResult>();
    }
}
