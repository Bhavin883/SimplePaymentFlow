using AutoFixture;
using FluentAssertions;
using SimplePaymentFlow.Domain;
using SimplePaymentFlow.Persistence;
using SimplePaymentFlow.Tests.UseCaseTests.Helper;
using SimplePaymentFlow.UseCases.UnlockPumpUseCase;
using Xunit;

namespace SimplePaymentFlow.Tests.UseCaseTests
{
    [Collection("Sequential")]
    public class UnlockPumpUseCaseTests
    {
        private readonly AppDbContext _dbContext;
        private readonly UnlockPumpUseCase _unlockPumpUseCase;
        private readonly Fixture _fixture = new();

        public UnlockPumpUseCaseTests()
        {
            _dbContext = ContextGenerator.GenerateContext();
            _unlockPumpUseCase = new UnlockPumpUseCase(_dbContext);
        }

        [Fact]
        public async Task Handle_WhenRequestedWithValidRequest_ShouldReturnSuccess()
        {
            //Arrange
            int request = 1;
            AddData(_dbContext);

            //Act
            var result = await _unlockPumpUseCase.Handle(request);

            //Assert
            result.Succeeded.Should().BeTrue();
        }

        [Fact]
        public async Task Handle_WhenRequestedWithInValidRequest_ShouldReturnSuccessFalse()
        {
            //Arrange
            int request = 0;
            AddData(_dbContext);

            //Act
            var result = await _unlockPumpUseCase.Handle(request);

            //Assert
            result.Succeeded.Should().BeFalse();
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

            _dbContext.Sites.AddRange(sites);
            _dbContext.Pumps.AddRange(pumps);
            _dbContext.SaveChanges();

        }
    }
}
