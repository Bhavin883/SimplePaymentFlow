using SimplePaymentFlow.Domain;

namespace SimplePaymentFlow.UseCases.UnlockPumpUseCase
{
    public class UnlockPumpUseCaseResponse
    {
        public bool Succeeded { get; set; }
        public string? Result { get; set; }
    }
}