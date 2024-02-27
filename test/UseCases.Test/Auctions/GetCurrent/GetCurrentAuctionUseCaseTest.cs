using Auction.API.Contracts;
using Auction.API.Entities;
using Auction.API.Enums;
using Auction.API.UseCases.Auctions.GetCurrent;
using Bogus;
using FluentAssertions;
using Moq;

namespace UseCases.Test.Auctions.GetCurrent;
public class GetCurrentAuctionUseCaseTest
{
    [Fact]
    public void Success()
    {
        #region Arrange - Inicialização dos objetos e configurações

        var auction = new Faker<AuctionEntity>()
            .RuleFor(a => a.Id, f => f.Random.Number(1, 100))
            .RuleFor(a => a.Name, f => f.Lorem.Word())
            .RuleFor(a => a.Starts, f => f.Date.Past())
            .RuleFor(a => a.Ends, f => f.Date.Future())
            .RuleFor(a => a.Items, (f, auction) => new List<ItemEntity>
            {
                new ItemEntity
                {
                    Id = f.Random.Number(1, 100),
                    Name = f.Commerce.ProductName(),
                    Brand = f.Commerce.Department(),
                    BasePrice = f.Random.Decimal(1, 1000),
                    Status = f.PickRandom<Status>(),
                    AuctionId = auction.Id
                }
            }).Generate();

        var mock = new Mock<IAuctionRepository>();
        mock.Setup(m => m.GetCurrent()).Returns(auction);

        var useCase = new GetCurrentAuctionUseCase(mock.Object);

        #endregion

        #region Act - Execução do método a ser testado

        var result = useCase.Execute();

        #endregion

        #region Assert - Verificação do resultado

        result.Should().NotBeNull(); // Plugin - FluentAssertions
        auction.Id.Should().Be(auction.Id);
        auction.Name.Should().Be(auction.Name);

        #endregion
    }
}
