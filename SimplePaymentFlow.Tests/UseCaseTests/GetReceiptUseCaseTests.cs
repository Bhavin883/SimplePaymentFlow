using FluentAssertions;
using SimplePaymentFlow.Domain;
using SimplePaymentFlow.Persistence;
using SimplePaymentFlow.Tests.UseCaseTests.Helper;
using SimplePaymentFlow.UseCases.GetReceiptUseCase;
using Xunit;

namespace SimplePaymentFlow.Tests.UseCaseTests
{
    [Collection("Sequential")]
    public class GetReceiptUseCaseTests
    {
        private readonly AppDbContext _dbContext;
        private readonly GetReceiptUseCase _getReceiptUseCase;

        public GetReceiptUseCaseTests()
        {
            _dbContext = ContextGenerator.GenerateContext();
            _dbContext.Database.EnsureDeleted();
            _dbContext.Database.EnsureCreated();
            _getReceiptUseCase = new GetReceiptUseCase(_dbContext);
        }

        [Fact]
        public async Task Handle_WhenRequestedWithNullRequest_ShouldReturnAllReceipts()
        {
            //Arrange
            int? request = null;
            AddData(_dbContext);

            //Act
            var result = await _getReceiptUseCase.Handle(request);
            var receipts = result.Receipts;

            //Assert
            receipts!.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public async Task Handle_WhenRequestedWithValidRequest_ShouldReturnMatchedReceipts()
        {
            //Arrange
            int request = 1;
            AddData(_dbContext);

            //Act
            var result = await _getReceiptUseCase.Handle(request);
            var receipts = result.Receipts;

            //Assert
            receipts[0].PumpId.Should().Be(request);

        }
        private static void AddData(AppDbContext _dbContext)
        {
            if (_dbContext.Sites.Count() > 0)
                return;

            var sites = new List<Site>
            {
            new Site(id: 101, name: "Site 101"),
            new Site(id: 102, name: "Site 102"),
            new Site(id: 103, name: "Site 103"),
            new Site(id: 104, name: "Site 104"),
            new Site(id: 105, name: "Site 105"),

            };
            var pumps = new List<Pump> {
            new Pump(1,"Pump1",true,101),
            new Pump(2,"Pump2",true,101),
            new Pump(3,"Pump3",true,102),
            new Pump(4,"Pump4",true,102),
            new Pump(5,"Pump5",true,103),
            new Pump(6,"Pump6",true,103),
            new Pump(7,"Pump7",true,104),
            new Pump(8,"Pump8",true,104),
            new Pump(9,"Pump9",true,105),
            new Pump(10,"Pump10",true,105),

            };

            var receipts = new List<Receipt> {
                new Receipt("1", DateTime.Now.AddDays(-1), DateTime.Now, 1),
                new Receipt("2", DateTime.Now.AddDays(-1), DateTime.Now, 2),
                new Receipt("3", DateTime.Now.AddDays(-1), DateTime.Now, 3),
                new Receipt("4", DateTime.Now.AddDays(-1), DateTime.Now, 4),
                new Receipt("5", DateTime.Now.AddDays(-1), DateTime.Now, 5),
                new Receipt("6", DateTime.Now.AddDays(-1), DateTime.Now, 6),
                new Receipt("7", DateTime.Now.AddDays(-1), DateTime.Now, 7),
                new Receipt("8", DateTime.Now.AddDays(-1), DateTime.Now, 8),

        };

            _dbContext.Sites.AddRange(sites);
            _dbContext.Pumps.AddRange(pumps);
            _dbContext.Receipts.AddRange(receipts);
            _dbContext.SaveChanges();

        }

    }
}
