using SimplePaymentFlow.Domain;

namespace SimplePaymentFlow.UseCases.LockPumpUseCase
{
    public class LockPumpUseCaseResponse
    {
        public bool Succeeded { get; set; }
        public string? Result { get; set; }
    }
}