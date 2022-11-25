using Microsoft.EntityFrameworkCore;
using SimplePaymentFlow.Domain;
using SimplePaymentFlow.Persistence;

namespace SimplePaymentFlow.UseCases.LockPumpUseCase
{
    public class LockPumpUseCase : ILockPumpUseCase
    {
        private readonly AppDbContext _dbContext;
        public LockPumpUseCase(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<LockPumpUseCaseResponse> Handle(int request)
        {
            var receipt = await _dbContext.Receipts.Where(m => (m.PumpId.Equals(request)) && (m.End == null)).OrderByDescending(d=>d.Start).FirstOrDefaultAsync();
            if (receipt != null)
            {
                receipt.End = DateTime.Now;
                await _dbContext.SaveChangesAsync();

                var pump = await _dbContext.Pumps.Where(m => m.Id.Equals(request)).FirstOrDefaultAsync();
                if (pump != null)
                {
                    pump.Locked = true;
                    await _dbContext.SaveChangesAsync();
                }

                return new LockPumpUseCaseResponse
                {
                    Succeeded = true,
                    Result = "success"
                };
            }
           
            return new LockPumpUseCaseResponse
            {
                Succeeded = false,
                Result = "Receipt not found"
            };

        }
    }
}
