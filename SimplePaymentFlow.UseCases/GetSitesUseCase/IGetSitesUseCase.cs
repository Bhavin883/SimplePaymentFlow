using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimplePaymentFlow.UseCases.GetSitesUseCase
{
    public interface IGetSitesUseCase
    {
        Task<GetSitesUseCaseResponse> Handle(string request);
    }
}
