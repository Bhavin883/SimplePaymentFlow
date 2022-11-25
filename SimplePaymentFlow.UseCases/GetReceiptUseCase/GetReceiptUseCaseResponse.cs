using SimplePaymentFlow.Domain;

namespace SimplePaymentFlow.UseCases.GetReceiptUseCase
{
    public class GetReceiptUseCaseResponse
    {
        public List<Receipt>? Receipts { get; set; }
    }
}