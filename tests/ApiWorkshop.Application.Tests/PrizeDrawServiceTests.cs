using ApiWorkshop.Application.Domain.Entities;
using ApiWorkshop.Application.Domain.Interfaces;
using ApiWorkshop.Application.Services;

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
            .Without(x => x.Gift)
            .Create();

        _giftRepository.Where(Arg.Any<Func<Gift, bool>>()).Returns(gifts);

        // Act
        var response = await _sut.Draw(name);

        // Assert
        await _prizeDrawRepository.Received(1).SaveChangesAsync();
        _giftRepository.Received(1).Where(Arg.Any<Func<Gift, bool>>());
        response.Name.Should().Be(name);
        response.GiftId.Should().NotBeEmpty();
    }
}
