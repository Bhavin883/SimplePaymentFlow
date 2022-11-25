using Microsoft.EntityFrameworkCore;
using SimplePaymentFlow.Domain;
using SimplePaymentFlow.Persistence;

namespace SimplePaymentFlow.Tests.UseCaseTests.Helper
{
    internal static class ContextGenerator
    {
        public static AppDbContext GenerateContext()
        {
            var dbContextOptionBuilder = new DbContextOptionsBuilder<AppDbContext>();
            dbContextOptionBuilder.UseInMemoryDatabase(databaseName: "SimplePaymentFlowDbTest");
            return new AppDbContext(dbContextOptionBuilder.Options);
        }
        
    }
}
