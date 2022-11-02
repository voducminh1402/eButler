using BusinessLogic.Models;
using DataAccess.Repostiories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace eButler.Pages
{
    public class BeComeSupplierModel : PageModel
    {
        public IUserRepository userRepository;
        [BindProperty]
        public User User { get; set; }
        public BeComeSupplierModel(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            try
            {
                userRepository.AddNewSupplier(User);
                return RedirectToPage("/Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "User name is already exist!";
                return Page();
            }
        }
    }
}
