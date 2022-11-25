using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePaymentFlow.UseCases.LockPumpUseCase
{
    public interface ILockPumpUseCase
    {
        Task<LockPumpUseCaseResponse> Handle(int request);
    }
}
