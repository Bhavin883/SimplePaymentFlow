using Microsoft.EntityFrameworkCore;
using SimplePaymentFlow.Domain;
using SimplePaymentFlow.Persistence;

namespace SimplePaymentFlow.UseCases.UnlockPumpUseCase
{
    public class UnlockPumpUseCase : IUnlockPumpUseCase
    {
        private readonly AppDbContext _dbContext;
        public UnlockPumpUseCase(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UnlockPumpUseCaseResponse> Handle(int request)
        {
            var pump = await _dbContext.Pumps.Where(m => m.Id.Equals(request)).FirstOrDefaultAsync();
            if (pump != null)
            {
                pump.Locked = false;
                await _dbContext.SaveChangesAsync();

                var insReceipt = new Receipt(DateTime.Now.ToString("fffssmmHHyyMMdd"), DateTime.Now, null, pump.Id);
                _dbContext.Add(insReceipt);
                await _dbContext.SaveChangesAsync();

                return new UnlockPumpUseCaseResponse
                {
                    Succeeded = true,
                    Result = "success"
                };
            }
            return new UnlockPumpUseCaseResponse
            {
                Succeeded = false,
                Result = "Pump not found"
            };

        }
    }
}
