using Microsoft.AspNetCore.Mvc;
using SimplePaymentFlow.UseCases.GetSitesUseCase;
using SimplePaymentFlow.UseCases.LockPumpUseCase;
using SimplePaymentFlow.UseCases.GetReceiptUseCase;
using SimplePaymentFlow.UseCases.UnlockPumpUseCase;

namespace SimplePaymentFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IGetSitesUseCase _getSitesUseCase;
        private readonly IUnlockPumpUseCase _unlockPumpUseCase;
        private readonly ILockPumpUseCase _lockPumpUseCase;
        private readonly IGetReceiptUseCase _getReceiptUseCase;
        public PaymentController(IGetSitesUseCase getSitesuseCase, IUnlockPumpUseCase unlockPumpUseCase, 
            ILockPumpUseCase lockPumpUseCase, IGetReceiptUseCase getReceiptUseCase)
        {
            _getSitesUseCase = getSitesuseCase;
            _unlockPumpUseCase = unlockPumpUseCase;
            _lockPumpUseCase = lockPumpUseCase;
            _getReceiptUseCase = getReceiptUseCase;
        }

        [HttpGet("FindSites")]
        public IActionResult FindSites([FromQuery] string? siteName)
        {
            var sites = _getSitesUseCase.Handle(siteName!);
            return Ok(sites.Result.Sites);
        }

        [HttpPost("UnLockPump")]
        public IActionResult UnLockPump([FromQuery] int id)
        {
            var res = _unlockPumpUseCase.Handle(id);
            if (res.Result.Succeeded)
                return Ok(res);
            return BadRequest();
        }

        [HttpPost("LockPump")]
        public IActionResult LockPump([FromQuery] int id)
        {
            var res = _lockPumpUseCase.Handle(id);
            if (res.Result.Succeeded)
                return Ok(res);
            return BadRequest();
        }

        [HttpGet("Receipt")]
        public IActionResult GetReceipt([FromQuery] int? id)
        {
            var sites = _getReceiptUseCase.Handle(id);
            return Ok(sites.Result.Receipts);
        }
    }
}
