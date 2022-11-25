using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SimplePaymentFlow.Domain;
using SimplePaymentFlow.Persistence;
using SimplePaymentFlow.Tests.UseCaseTests.Helper;
using SimplePaymentFlow.UseCases.GetSitesUseCase;
using Xunit;

namespace SimplePaymentFlow.Tests.UseCaseTests
{
    [Collection("Sequential")]
    public class GetSitesUseCaseTests
    {
        private readonly AppDbContext _dbContext;
        private readonly GetSitesUseCase _getSitesUseCase;
        private readonly Fixture _fixture = new();

        public GetSitesUseCaseTests()
        {
            _dbContext = ContextGenerator.GenerateContext();

            _getSitesUseCase = new GetSitesUseCase(_dbContext);
        }

        [Fact]
        public async Task Handle_WhenRequestedWithNullRequest_ShouldReturnAllSites()
        {
            //Arrange
            string request = "";
            AddSiteData(_dbContext);

            //Act
            var result = await _getSitesUseCase.Handle(request);
            var sites = result.Sites;

            //Assert
            sites!.Count.Should().BeGreaterThan(0);

        }

        [Fact]
        public async Task Handle_WhenRequestedWithValidRequest_ShouldReturnMatchedSites()
        {
            //Arrange
            string request = "101";
            AddSiteData(_dbContext);
            //Act
            var result = await _getSitesUseCase.Handle(request);
            var sites = result.Sites;

            //Assert
            sites[0].Name.Contains(request);

        }
        private static void AddSiteData(AppDbContext _dbContext)
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
