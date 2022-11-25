using Microsoft.EntityFrameworkCore;
using SimplePaymentFlow.Domain;
using SimplePaymentFlow.Persistence;

namespace SimplePaymentFlow.UseCases.GetReceiptUseCase
{
    public class GetReceiptUseCase : IGetReceiptUseCase
    {
        private readonly AppDbContext _dbContext;
        public GetReceiptUseCase(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetReceiptUseCaseResponse> Handle(int? request)
        {
            IEnumerable<Receipt> receipt = null;
            if (request == null)
                receipt = await _dbContext.Receipts.OrderByDescending(d => d.End).ToListAsync();
            else
                receipt = await _dbContext.Receipts.Where(m => m.PumpId.Equals(request)).OrderByDescending(d => d.End).ToListAsync();

            return new GetReceiptUseCaseResponse
            {
                Receipts = receipt.ToList()
            };

        }
    }
}
