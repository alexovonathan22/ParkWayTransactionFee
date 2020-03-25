using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ParkWayTransactionFee.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public FeeCollection feeObj { get; set; }

        [BindProperty]
        public int Amount { get; set; }
        [BindProperty]
        public int Result { get; set; }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid) 
            {
                string FileLoc = @"C:\Users\HP\source\repos\ParkWayTransactionFee\ParkWayTransactionFee\fees.config.json";
                FeeCollection feeJson = null;

                try
                {
                    string readFromJson;
                    using (var reader = new StreamReader(FileLoc))
                    {
                        readFromJson =  reader.ReadToEnd();
                        feeJson = JsonConvert.DeserializeObject<FeeCollection>(readFromJson);
                    }

                    Result = feeJson.Fees.Where(c => Amount <= c.maxAmount && Amount >= c.minAmount).Select(c => c.feeAmount).FirstOrDefault();


                    //ViewData["FeeCharge"] = Result;   
                    if(Result != 0)
                        return RedirectToPage("Success", new { Amount = Amount, Result = Result});
                    else
                        return RedirectToPage("Error", new { Amount = Amount, Result = Result });


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

    public class FeeCollection
    {
        public List<Fee> Fees { get; set; }

    }
}
