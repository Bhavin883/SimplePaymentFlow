using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePaymentFlow.UseCases.GetReceiptUseCase
{
    public interface IGetReceiptUseCase
    {
        Task<GetReceiptUseCaseResponse> Handle(int? request);
    }
}
