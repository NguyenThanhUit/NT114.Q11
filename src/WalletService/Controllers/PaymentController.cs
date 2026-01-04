using Microsoft.AspNetCore.Mvc;
using VNPAY.NET;
using VNPAY.NET.Enums;
using VNPAY.NET.Models;
using VNPAY.NET.Utilities;

namespace Backend_API_Testing.Controllers
{
    [ApiController]
    [Route("api/vnpay")]
    public class VnpayController : ControllerBase
    {
        private readonly IVnpay _vnpay;
        private readonly IConfiguration _configuration;

        public VnpayController(IVnpay vnPayservice, IConfiguration configuration)
        {
            _vnpay = vnPayservice;
            _configuration = configuration;

            try
            {
                var tmnCode = _configuration["Vnpay:TmnCode"];
                var hashSecret = _configuration["Vnpay:HashSecret"];
                var baseUrl = _configuration["Vnpay:BaseUrl"];
                var callbackUrl = _configuration["Vnpay:CallbackUrl"];

                Console.WriteLine($"[VNPAY INIT] TmnCode: {tmnCode}");
                Console.WriteLine($"[VNPAY INIT] HashSecret: {(string.IsNullOrEmpty(hashSecret) ? "NULL" : "SET")}");
                Console.WriteLine($"[VNPAY INIT] BaseUrl: {baseUrl}");
                Console.WriteLine($"[VNPAY INIT] CallbackUrl: {callbackUrl}");

                _vnpay.Initialize(tmnCode!, hashSecret!, baseUrl!, callbackUrl!);

                Console.WriteLine("[VNPAY INIT] Initialization successful!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[VNPAY INIT] Initialization failed: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }

        [HttpGet("CreatePaymentUrl")]
        public ActionResult<string> CreatePaymentUrl(double money, string description)
        {
            try
            {
                var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1";

                Console.WriteLine($"[VNPAY REQUEST] Client IP: {ipAddress}");
                Console.WriteLine($"[VNPAY REQUEST] Amount: {money}, Description: {description}");

                var request = new PaymentRequest
                {
                    PaymentId = DateTime.Now.Ticks,
                    Money = money,
                    Description = description,
                    IpAddress = ipAddress,
                    BankCode = BankCode.ANY,
                    CreatedDate = DateTime.Now,
                    Currency = Currency.VND,
                    Language = DisplayLanguage.Vietnamese
                };

                Console.WriteLine($"[VNPAY REQUEST] PaymentRequest object: {System.Text.Json.JsonSerializer.Serialize(request)}");

                var paymentUrl = _vnpay.GetPaymentUrl(request);

                Console.WriteLine($"[VNPAY RESPONSE] Generated Payment URL: {paymentUrl}");

                return Created(paymentUrl, paymentUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[VNPAY ERROR] CreatePaymentUrl: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("IpnAction")]
        public IActionResult IpnAction()
        {
            if (!Request.QueryString.HasValue)
            {
                Console.WriteLine("[VNPAY IPN] No query string found.");
                return Ok(new { RspCode = "99", Message = "No Query" });
            }

            try
            {
                Console.WriteLine("[VNPAY IPN] Raw Query String:");
                foreach (var key in Request.Query.Keys)
                    Console.WriteLine($"{key}: {Request.Query[key]}");

                var paymentResult = _vnpay.GetPaymentResult(Request.Query);

                Console.WriteLine("[VNPAY IPN] Parsed Payment Result:");
                Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(paymentResult));

                return paymentResult.IsSuccess
                    ? Ok(new { RspCode = "00", Message = "Confirm Success" })
                    : Ok(new { RspCode = "01", Message = "Payment Failed" });
            }
            catch (Exception ex)
            {
                Console.WriteLine("[VNPAY IPN] Exception: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
                return Ok(new { RspCode = "97", Message = "Exception Error" });
            }
        }

        [HttpGet("Callback")]
        public IActionResult Callback()
        {
            if (!Request.QueryString.HasValue)
            {
                Console.WriteLine("[VNPAY CALLBACK] No query string found.");
                return Redirect("https://nguyenth4nh.id.vn/recharge/result?success=false&error=noquery");
            }

            try
            {
                Console.WriteLine("[VNPAY CALLBACK] Raw Query String:");
                foreach (var key in Request.Query.Keys)
                    Console.WriteLine($"{key}: {Request.Query[key]}");

                var paymentResult = _vnpay.GetPaymentResult(Request.Query);

                Console.WriteLine("[VNPAY CALLBACK] Parsed Payment Result:");
                Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(paymentResult));

                var paymentId = Request.Query["vnp_TxnRef"];
                var isSuccess = paymentResult.IsSuccess ? "true" : "false";
                var redirectUrl = $"https://app.nguyenth4nh.id.vn/recharge/result?paymentId={paymentId}&success={isSuccess}";

                Console.WriteLine($"[VNPAY CALLBACK] Redirecting to: {redirectUrl}");

                return Redirect(redirectUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine("[VNPAY CALLBACK] Exception: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
                return Redirect("https://nguyenth4nh.id.vn/recharge/result?success=false&error=exception");
            }
        }
    }
}
