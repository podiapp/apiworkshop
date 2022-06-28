using ApiWorkshop.Application.Domain.Entities;
using ApiWorkshop.Application.Domain.Filters;
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
        public async Task CreateGift_ShouldCreateGift_WhenAllValid()
        {
            // Arrange
            GiftRequest giftRequest = _fixture.Build<GiftRequest>().Create();

            // Act
            var response = await _sut.Create(giftRequest);

            // Assert
            await _giftBaseRepository.Received(1).SaveChangesAsync();
            _giftBaseRepository.Received(1).Insert(Arg.Any<Gift>());
            giftRequest.Name.Equals(response.Data?.Name);
        }
        [Fact]
        public async Task UpdateGift_ShouldUpdateGift_WhenAllValid()
        {
            // Arrange
            GiftRequest giftRequest = _fixture.Build<GiftRequest>().Create();
            Guid id = Guid.NewGuid();
            var gift = _fixture
                .Build<Gift>()
                .With(g => g.Id, id)
                .Without(x => x.PrizeDraws)
                .Create();

            _giftBaseRepository.Where(id).Returns(gift);

            // Act
            var response = await _sut.Update(id, giftRequest);

            // Assert
            await _giftBaseRepository.Received(1).SaveChangesAsync();
            _giftBaseRepository.Received(1).Update(Arg.Any<Gift>());
            giftRequest.Name.Equals(response.Data?.Name);
        }

        [Fact]
        public async Task DeleteGift_ShouldDeleteGift_WhenAllValid()
        {
            // Arrange
            GiftRequest giftRequest = _fixture.Build<GiftRequest>().Create();
            Guid id = Guid.NewGuid();
            var gift = _fixture
                .Build<Gift>()
                .With(g => g.Id, id)
                .Without(x => x.PrizeDraws)
                .Create();

            _giftBaseRepository.Where(id).Returns(gift);

            // Act
            await _sut.Delete(id, giftRequest);

            // Assert
            await _giftBaseRepository.Received(1).SaveChangesAsync();
            await _giftBaseRepository.Received(1).Delete(id);
        }

        [Fact]
        public async Task ReadGiftById_ShouldReadGift_WhenAllValid()
        {
            // Arrange
            Guid id = Guid.NewGuid();
            var gift = _fixture
                .Build<Gift>()
                .With(g => g.Id, id)
                .Without(x => x.PrizeDraws)
                .Create();

            _giftBaseRepository.Where(id).Returns(gift);

            // Act
            var response = await _sut.ReadById(id);

            // Assert

            await _giftBaseRepository.Received(1).Where(Arg.Any<Guid>());
            gift.Name.Equals(response.Data?.Name);
        }
        [Fact]
        public void ReadGift_ShouldReadGift_WhenAllValid()
        {
            //Arrange
            GiftFilter filter = new();
            List<Gift> gifts = _fixture.Build<List<Gift>>().Create();

            //Act
            var response = _sut.Read(filter);

            //Assert
            //await _giftBaseRepository.Received(1).Where(Arg.Any<List<Gift>>());
            gifts.Equals(response.Data);

        }

    }
}