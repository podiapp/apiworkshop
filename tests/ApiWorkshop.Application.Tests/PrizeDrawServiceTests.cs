using ApiWorkshop.Application.Domain.Entities;
using ApiWorkshop.Application.Domain.Filters;
using ApiWorkshop.Application.Domain.Interfaces;
using ApiWorkshop.Application.Services;
using NSubstitute;
using NSubstitute.ReceivedExtensions;

namespace ApiWorkshop.Application.Tests;

public class PrizeDrawServiceTests
{
    private readonly PrizeDrawService _sut;
    private readonly IBaseRepository<PrizeDraw> _prizeDrawRepository = Substitute.For<IBaseRepository<PrizeDraw>>();
    private readonly IBaseRepository<Gift> _giftRepository = Substitute.For<IBaseRepository<Gift>>();
    private readonly IFixture _fixture = new Fixture();

    public PrizeDrawServiceTests()
        => _sut = new(_prizeDrawRepository, _giftRepository);

    [Fact]
    public async Task Draw_ShouldDrawGift_WhenParametersAreAllValid()
    {
        // Arrange
        string name = "Lucas";
        IQueryable<Gift> gifts = Enumerable.Range(0, 10)
            .Select(x => _fixture.Build<Gift>().Without(x => x.PrizeDraws).Create())
            .AsQueryable();

        PrizeDraw? prizeDraw = _fixture
            .Build<PrizeDraw>()
            .With(x => x.Name, name)
            .With(x => x.Gift, gifts.FirstOrDefault())
            .Create();

        _giftRepository.Where(Arg.Any<Func<Gift, bool>>()).Returns(gifts);
        _prizeDrawRepository.Where(Arg.Any<Guid>()).Returns(prizeDraw);

        // Act
        var response = await _sut.Draw(name);

        // Assert
        await _prizeDrawRepository.Received(1).SaveChangesAsync();
        _giftRepository.Received(1).Where(Arg.Any<Func<Gift, bool>>());
        response.Data!.Name.Should().Be(name);
        response.Data!.Gift.Should().NotBeEmpty();
    }

    [Fact]
    public void GetDraw_ShouldGetListDrawGift_WhenParametersAreAllValid()
    {
        // Arrange
        PrizeDrawFilter filter = new();
        List<Gift> gifts = Enumerable.Range(0, 10)
            .Select(x => _fixture.Build<Gift>().Without(x => x.PrizeDraws).Create())
            .ToList();

        IQueryable<PrizeDraw> prizes = Enumerable.Range(0, 10)
            .Select(i => _fixture.Build<PrizeDraw>().With(x => x.Gift, gifts.ElementAt(i)).Create())
            .AsQueryable();

        _prizeDrawRepository.Where().Returns(prizes);

        // Act
        var response = _sut.Get(filter);

        // Assert
        _prizeDrawRepository.Received(1).Where();
        response.Data!.Data.Should().NotBeNullOrEmpty();
        prizes.Count().Should().Be(response.Data!.Data!.Count);
    }

    [Fact]
    public async Task Reset_ShouldDeleteAllPrizeDraws_WhenParametersAreAllValid()
    {
        // Arrange
        IQueryable<PrizeDraw> prizes = Enumerable.Range(0, 10)
            .Select(i => _fixture.Build<PrizeDraw>().Without(x => x.Gift).Create())
            .AsQueryable();

        _prizeDrawRepository.Where().Returns(prizes);

        // Act
        await _sut.Reset();

        // Assert
        _prizeDrawRepository.Received(1).Where();
        _prizeDrawRepository.Received(1).DeleteRange(Arg.Any<List<PrizeDraw>>());
        await _prizeDrawRepository.Received(1).SaveChangesAsync();
    }
}
