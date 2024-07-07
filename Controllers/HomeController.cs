using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;

namespace BankAppUsingControllers.Controllers
{
    [Controller]
    public class HomeController : Controller
    {
        private static readonly object _account = new {
            accountNumber = 1001,
            accountHolderName = "Example Name",
            currentBalance = 5000
        };

        [Route("/")]
        public IActionResult Index()
        {
            return Ok("Welcome to the Best Bank\n");
        }

        [Route("account-details")]
        public IActionResult AccountDetails()
        {
            return Json(_account);
        }

        [Route("account-statement")]
        public IActionResult AccountStatement()
        {
            return File("/statement.pdf", "application/pdf");
        }

        [Route("account-balance/{accountNumber?}")]
        public IActionResult AccountBalance()
        {
            if (HttpContext.Request.RouteValues.TryGetValue("accountNumber", out object? accountIdObject) &&
                accountIdObject is string accountId)
            {
                if (string.IsNullOrEmpty(accountId))
                {
                    return NotFound("Account Number should be supplied");
                }

                var account = new
                {
                    accountNumber = 1001,
                    accountHolderName = "Example Name",
                    currentBalance = 5000
                };

                if (Int32.TryParse(accountId, out int idInt))
                {
                    if (account.accountNumber.Equals(idInt))
                    {
                        return Ok(account.currentBalance);
                    }

                    return NotFound("Account Number should be 1001");
                }
                else
                {
                    return BadRequest("Account Number must be an integer");
                }
            }
            else
            {
                return NotFound("Account Number should be supplied");
            }

        }
    }
}
