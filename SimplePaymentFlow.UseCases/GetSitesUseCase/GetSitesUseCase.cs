using Microsoft.EntityFrameworkCore;
using SimplePaymentFlow.Domain;
using SimplePaymentFlow.Persistence;
namespace SimplePaymentFlow.UseCases.GetSitesUseCase
{
    public class GetSitesUseCase : IGetSitesUseCase
    {
        private readonly AppDbContext _dbContext;
        public GetSitesUseCase(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetSitesUseCaseResponse> Handle(string request)
        {
            IEnumerable<Site> sites = null;
            if (string.IsNullOrEmpty(request))
            {
                sites = await _dbContext
               .Sites
               .Include(e => e.Pumps)
               .ToListAsync();
            }
            else
            {
                sites = await _dbContext
              .Sites.Where(e => e.Name.Contains(request))
              .Include(e => e.Pumps)
              .ToListAsync();
            }

            return new GetSitesUseCaseResponse
            {
                Sites = sites.ToList(),
            };
        }
    }
}
