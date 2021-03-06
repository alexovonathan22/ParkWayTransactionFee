﻿using System;
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
        public float Charge { get; set; }

        [BindProperty(SupportsGet = true)]
        public float ActualAmount { get; set; }

        [BindProperty(SupportsGet = true)]
        public float TransferAmt { get; set; }

        [BindProperty(SupportsGet = true)]
        public float DebitAmount { get; set; }
        public void OnGet()
        {

        }
    }
}