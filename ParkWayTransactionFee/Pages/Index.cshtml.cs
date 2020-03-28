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
        public IndexModel(IOptions<List<Fee>> feeOptions)
        {
            _fee = feeOptions ?? throw new ArgumentNullException(nameof(feeOptions));
        }


        [BindProperty]
        public float Amount { get; set; }

        public float ActualAmount { get; set; }

        [BindProperty]
        public float FeeCharge { get; set; }
        [BindProperty]
        public float ActualFeeCharge { get; set; }
        public float DebitAmount { get; set; }
        [BindProperty]
        public float TransferAmount { get; set; }

        public IActionResult OnPost()
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if(Amount % 1 != 0)
                    {
                        ActualAmount = Amount;
                        int val = (int)Math.Ceiling(Amount);
                        Amount = val;
                    }
                    else
                    {
                        ActualAmount = Amount;
                    }
                    
                    FeeCharge = _fee.Value.Where(c => Amount <= c.maxAmount && Amount >= c.minAmount)
                                         .Select(c => c.feeAmount).FirstOrDefault();
                    ActualFeeCharge = FeeCharge;

                    if (FeeCharge != 00)
                    {
                        TransferAmount = ActualAmount - FeeCharge;

                        FeeCharge = _fee.Value.Where(c => TransferAmount <= c.maxAmount && TransferAmount >= c.minAmount)
                                         .Select(c => c.feeAmount).FirstOrDefault();
                        DebitAmount = TransferAmount + FeeCharge;

                        //if this code is uncommeented and the return statement below is commented this should help remedy the disparity.

                        //if(ActualFeeCharge == FeeCharge)
                        //{
                        //    return RedirectToPage("Acceptance", new { ActualAmount = ActualAmount, Charge = FeeCharge, TransferAmt = TransferAmount, DebitAmount =DebitAmount });

                        //}
                        //else
                        //{

                        //    return RedirectToPage("Acceptance", new { ActualAmount = ActualAmount, Charge = ActualFeeCharge, TransferAmt = TransferAmount, DebitAmount = TransferAmount + ActualFeeCharge });
                        //}
                        return RedirectToPage("Acceptance", new { ActualAmount = ActualAmount, Charge = FeeCharge, TransferAmt = TransferAmount, DebitAmount = DebitAmount });

                    }
                    else
                    {
                        return RedirectToPage("Error", new { Amount = Amount, Charge = FeeCharge });
                    }
                }
                catch (Exception)
                {
                    return Page();
                }
            }
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
