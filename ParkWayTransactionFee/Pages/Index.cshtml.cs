using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ParkWayTransactionFee.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IOptions<List<Fee>> _fee;
        public IndexModel(IOptions<List<Fee>> fee)
        {
            _fee = fee;
        }
        //[BindProperty]
        //public FeeCollection feeObj { get; set; }

        [BindProperty]
        public float Amount { get; set; }
        [BindProperty]
        public float Result { get; set; }

        public IActionResult OnPost()
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if(Amount > 0 && Amount <= 999999999)
                    {
                        int val = (int)Math.Ceiling(Amount);
                        Amount = val;
                    }

                    Result = _fee.Value.Where(c => Amount <= c.maxAmount && Amount >= c.minAmount)
                                         .Select(c => c.feeAmount).FirstOrDefault();

                    if (Result != 0)
                        return RedirectToPage("Success", new { Amount = Amount, Result = Result });
                    else
                        return RedirectToPage("Error", new { Amount = Amount, Result = Result });


                }
                catch (Exception)
                {
                    return Page();
                }
            }
            //var Fee = _fee.Value;

            return Page();
        }
    }

    public class Fee
    {
        public int minAmount { get; set; }
        public int maxAmount { get; set; }
        public int feeAmount { get; set; }
    }
    
}
