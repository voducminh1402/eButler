using DataAccess.Repostiories;
using eButler.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace eButler.Pages
{
    public class CheckoutSuccessModel : PageModel
    {
        private readonly ICheckOutRepository checkOutRepository;
        public CheckoutSuccessModel(ICheckOutRepository checkOutRepository)
        {
            this.checkOutRepository = checkOutRepository;
        }

        public async Task<IActionResult> OnGetAsync(string status) 
        {
            if (status == "COMPLETED")
            {
                checkOutRepository.SetCheckOutStatus(true);
            }
            return Page();
        }
        public IActionResult OnPost(string status)
        {
            return Page();
        }
    }
}
