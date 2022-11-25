using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePaymentFlow.UseCases.UnlockPumpUseCase
{
    public interface IUnlockPumpUseCase
    {
        Task<UnlockPumpUseCaseResponse> Handle(int request);
    }
}
