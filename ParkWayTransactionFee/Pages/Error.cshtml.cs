using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ParkWayTransactionFee.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class ErrorModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public float Amount { get; set; }

        [BindProperty(SupportsGet = true)]
        public float Charge { get; set; }

        public void OnGet()
        {
        }
    }
}
