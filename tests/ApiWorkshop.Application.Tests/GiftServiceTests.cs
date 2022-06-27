using ApiWorkshop.Application.Domain.Entities;
using ApiWorkshop.Application.Domain.Interfaces;
using ApiWorkshop.Application.Domain.Requests;
using ApiWorkshop.Application.Services;

namespace ApiWorkshop.Application.Tests
{
    public class GiftServiceTests
    {
        private readonly GiftService _sut;
        private readonly IBaseRepository<Gift> _giftBaseRepository = Substitute.For<IBaseRepository<Gift>>();
        private readonly IFixture _fixture = new Fixture();

        public GiftServiceTests()
        {
            _sut = new GiftService(_giftBaseRepository);
        }


        [Fact]
        public async Task InsertGift_ShouldCreateGift_WhenAllValid()
        {
            // Arrange
            GiftRequest giftRequest = _fixture.Build<GiftRequest>().Create();

            // Act
            var response = await _sut.Insert(giftRequest);

            // Assert
            await _giftBaseRepository.Received(1).SaveChangesAsync();
            giftRequest.Name.Equals(response.Name);
        }
    }
}