using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ParkWayTransactionFee
{
    public class SuccessModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Result { get; set; }
        [BindProperty(SupportsGet = true)]
        public int Amount { get; set; }
        public void OnGet()
        {

        }
    }
}